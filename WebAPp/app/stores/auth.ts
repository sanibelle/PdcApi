import { useApi } from '~/composables/services/ApiClient';
export const useAuthStore = defineStore(
  'auth',
  () => {
    let inFlight: Promise<User | null> | null = null;
    const timeBetweenUserRefetch = useRuntimeConfig().public.timeBetweenUserRefetch as number;

    let lastFetch: number = 0;
    const { fetchUser } = useUserClient();
    const user = ref<User | null>(null);

    const isAuthenticated = computed(() => user.value !== null);

    const authenticate = async () => {
      if (isAuthenticated.value && Date.now() - lastFetch < timeBetweenUserRefetch) return; // Kindof a cache
      // Prevents multiple simultaneous fetches of the user data
      if (inFlight) {
        await inFlight;
        return;
      }
      try {
        inFlight = fetchUser();
        user.value = await inFlight;
        lastFetch = Date.now();
      } finally {
        inFlight = null;
      }
    };

    const logout = async () => {
      user.value = null;
      await useApi().Get('/auth/logout', { followRedirect: true });
    };

    return {
      user,
      isAuthenticated,
      logout,
      authenticate,
    };
  },
  {
    persist: {
      storage: piniaPluginPersistedstate.sessionStorage(),
    },
  },
);
