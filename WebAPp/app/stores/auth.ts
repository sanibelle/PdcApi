import { useApi } from '~/composables/services/ApiClient';
export const useAuthStore = defineStore(
  'auth',
  () => {
    const { fetchUser } = useUserClient();
    const user = ref<User | null>(null);

    const isAuthenticated = computed(() => user.value !== null);

    const authenticate = async () => {
      user.value = await fetchUser();
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
