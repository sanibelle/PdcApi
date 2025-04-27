import { ref, computed } from 'vue';
import { useApi } from '~/composables/services/ApiClient';
import { fetchUser } from '~/composables/services/UserService';
import type { User } from '~/types/Security/User';

export const useAuthStore = defineStore('auth', () => {
  const config = useRuntimeConfig();
  const user = ref<User | null>(null);

  const isAuthenticated = computed(() => user.value !== null);

  const login = async () => {
    // fetchUser should redirect to login page if not authenticated
    user.value = await fetchUser();
  };

  const authenticate = async () => {
    user.value = await fetchUser();
  };

  const logout = async () => {
    await useApi().Get('logout');
  };

  return {
    user,
    isAuthenticated,
    login,
    logout,
    authenticate,
  };
});
