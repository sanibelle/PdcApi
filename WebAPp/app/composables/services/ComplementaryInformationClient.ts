import { useApi } from './ApiClient';

export const useComplementaryInformationClient = () => {
  const createComplementaryInformation = async (id: string, complementaryInformation: ComplementaryInformation): Promise<ComplementaryInformation> => {
    const api = useApi();
    const preparedComplementaryInformation: ComplementaryInformation = {
      text: complementaryInformation.text,
    };
    const createdComplementaryInformation = await api.Post<ComplementaryInformation>(`/changeable/${id}/complementaryInformation`, preparedComplementaryInformation);

    if (createdComplementaryInformation == null) {
      throw new Error('Complementary information not found or not authenticated');
    }
    return createdComplementaryInformation;
  };

  const deleteComplementaryInformation = async (id: string): Promise<void> => {
    const api = useApi();
    await api.Delete(`/complementaryInformation/${id}`);
  };

  const updateComplementaryInformation = async (id: string, complementaryInformation: ComplementaryInformation): Promise<ComplementaryInformation> => {
    const api = useApi();
    const preparedComplementaryInformation: ComplementaryInformation = {
      text: complementaryInformation.text,
    };
    const updatedComplementaryInformation = await api.Put<ComplementaryInformation>(`/complementaryInformation/${id}`, preparedComplementaryInformation);
    if (updatedComplementaryInformation == null) {
      throw new Error('Complementary information not found or not authenticated');
    }
    return updatedComplementaryInformation;
  };

  return { createComplementaryInformation, deleteComplementaryInformation };
};
