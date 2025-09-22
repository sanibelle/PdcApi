import { useApi } from './ApiClient';

export const useUserClient = () => {
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
