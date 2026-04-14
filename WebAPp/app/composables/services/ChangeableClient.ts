import { useApi } from './ApiClient';

export const useChangeableClient = () => {
  const updateChangeable = async (id: string, changeableInput: Changeable): Promise<Changeable> => {
    const api = useApi();
    const changeable: Changeable = {
      value: changeableInput.value,
      id: changeableInput.id,
    };
    const updatedChangeable = await api.Put<Changeable>(`/changeable/${id}`, changeable);

    if (updatedChangeable == null) {
      throw new Error('Changeable not found or not authenticated');
    }
    return updatedChangeable;
  };

  return { updateChangeable };
};
