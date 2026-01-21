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

  const fetchUsers = async (): Promise<User[]> => {
    const api = useApi();
    const users = await api.Get<User[]>('/user');
    if (users === null) {
      throw new Error('Users not found or not authenticated');
    }
    return users;
  };

  const fetchRoles = async (): Promise<string[]> => {
    const api = useApi();
    let roles = await api.Get<string[]>('/role');
    if (roles === null) {
      throw new Error('Users not found or not authenticated');
    }
    roles = roles.sort((a, b) => a.localeCompare(b));
    return roles;
  };

  const updateUserRoles = async (userId: string, roles: string[]): Promise<User> => {
    const api = useApi();
    const updatedRoles = await api.Put<User, string[]>(`/user/${userId}/roles`, roles);
    if (updatedRoles === null) {
      throw new Error('Failed to update user roles');
    }
    return updatedRoles;
  };

  return { fetchUser, fetchUsers, fetchRoles, updateUserRoles };
};
