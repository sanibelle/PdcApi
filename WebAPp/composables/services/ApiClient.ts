// server.ts
import {
  ForbiddenException,
  NotFoundException,
  ServerException,
  UnauthorizedException,
  ValidationException,
} from '~/types/Exceptions/ApiExceptions';

export interface Options<T = any> {
  data?: T;
  noRedirectOnLogin?: boolean;
}

interface InternalOptions extends Options {
  method: 'GET' | 'POST' | 'PUT' | 'PATCH' | 'DELETE';
}

export class ApiClient {
  private baseURL: string;

  constructor() {
    // TODO from config
    this.baseURL = useRuntimeConfig().public.apiBaseUrl as string;
  }

  private NavigateToLoginPage = async (url: string, options: InternalOptions) => {
    if (!options.noRedirectOnLogin) {
      await navigateTo(url, { external: true });
    }
  };

  private async SendRequest<T>(url: string, options: InternalOptions): Promise<T | null> {
    const response = await $fetch<T>(this.baseURL + url, {
      method: options.method as any,
      body: options.data ? JSON.stringify(options.data) : undefined,
      credentials: 'include',
      redirect: 'manual',
      headers: {
        'Content-Type': 'application/json',
        Accept: 'application/json',
      },
      onResponse: async ({ response }) => {
        // A redirect should only happend for a login request in our api.
        if (response.status >= 300 && response.status < 400) {
          const location = response.headers.get('location');
          if (location) {
            await this.NavigateToLoginPage(location, options);
          }
        }
      },
      onResponseError: async ({ response }) => {
        try {
          this.HandleResponseStatus(response.status);
        } catch (e) {
          // if UnauthorizedException, redirect to login page
          if (e instanceof UnauthorizedException || e instanceof ForbiddenException) {
            await this.NavigateToLoginPage(`${this.baseURL}/auth/login`, options);
          } else {
            // TODO handle other exceptions
            console.error(e);
          }
        }
      },
    });
    return response as T;
  }

  // GET request
  async Get<T>(url: string, options: Options = {}): Promise<T | null> {
    return this.SendRequest<T>(url, { ...options, method: 'GET' });
  }

  async Post<ResponseData, Payload = ResponseData>(
    url: string,
    data: Payload,
    options: Options
  ): Promise<ResponseData | null> {
    return this.SendRequest<ResponseData>(url, { ...options, method: 'POST', data });
  }

  async Put<ResponseData, Payload = ResponseData>(
    url: string,
    data: Payload,
    options: Options
  ): Promise<ResponseData | null> {
    return this.SendRequest<ResponseData>(url, { ...options, method: 'PUT', data });
  }

  async Patch<ResponseData, Payload = ResponseData>(
    url: string,
    data: Payload,
    options: Options
  ): Promise<ResponseData | null> {
    return this.SendRequest<ResponseData>(url, { ...options, method: 'PATCH', data });
  }

  async Delete(url: string, options: Options) {
    await this.SendRequest(url, { ...options, method: 'DELETE' });
  }

  // TODO passer le message d'erreur dans la fct
  private HandleResponseStatus = (status: number) => {
    switch (status) {
      case 400:
      case 422:
        throw new ValidationException();
      case 401:
        throw new UnauthorizedException();
      case 403:
        throw new ForbiddenException();
      case 404:
        throw new NotFoundException();
      case 500:
        throw new ServerException();
    }
  };
}

// Composable for use in components
export function useApi() {
  return new ApiClient();
}
