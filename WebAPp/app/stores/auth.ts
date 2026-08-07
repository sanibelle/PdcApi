import { useApi } from '~/composables/services/ApiClient';
export const useAuthStore = defineStore(
  'auth',
  () => {
    const timeBetweenUserRefetch = useRuntimeConfig().public.timeBetweenUserRefetch as number;

    let lastFetch: number = 0;
    const { fetchUser } = useUserClient();
    const user = ref<User | null>(null);

    const isAuthenticated = computed(() => user.value !== null);

    const authenticate = async () => {
      if (isAuthenticated && Date.now() - lastFetch < timeBetweenUserRefetch) return; // Kindof a cache
      user.value = await fetchUser();
      lastFetch = Date.now();
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
