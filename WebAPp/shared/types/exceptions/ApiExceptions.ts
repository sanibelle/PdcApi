export class ApiError extends Error {
  statusCode: number;
  constructor(message: string, statusCode: number) {
    super(message);
    this.statusCode = statusCode;
    this.name = 'ApiError';
  }
}

export class UnauthorizedException extends ApiError {
  constructor(message = 'Unauthorized: Please log in to continue') {
    super(message, 401);
    this.name = 'UnauthorizedException';
  }
}

export class ForbiddenException extends ApiError {
  constructor(message = 'Forbidden: You do not have permission to access this resource') {
    super(message, 403);
    this.name = 'ForbiddenException';
  }
}

export class NotFoundException extends ApiError {
  constructor(message = 'Resource not found') {
    super(message, 404);
    this.name = 'NotFoundException';
  }
}

export class DuplicateException extends ApiError {
  constructor(message = 'Duplicate resource found') {
    super(message, 409);
    this.name = 'DuplicateException';
  }
}

export class ValidationException extends ApiError {
  errors?: Record<string, string[]>;

  constructor(message = 'Validation error', errors?: Record<string, string[]>) {
    super(message, 422);
    this.name = 'ValidationException';
    this.errors = errors;
  }
}

export class ServerException extends ApiError {
  constructor(message = 'Server error occurred. Please try again later.') {
    super(message, 500);
    this.name = 'ServerException';
  }
}
