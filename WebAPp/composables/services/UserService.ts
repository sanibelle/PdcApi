import { useApi } from './ApiClient';
import type { User } from '~/types/Security/User';

// TODO add use keyword to the function
export const useUser = () => {
  const fetchUser = async (): Promise<User> => {
    const api = useApi();
    const user = await api.Get<User>('/Auth/profile');
    if (user === null) {
      throw new Error('User not found or not authenticated');
    }
    return user;
  };

  return { fetchUser };
};
