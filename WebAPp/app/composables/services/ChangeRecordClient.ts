import { useApi } from './ApiClient';

export const useChangeRecordClient = () => {
  const api = useApi();
  const publishChangeRecord = async (changeRecordId: string): Promise<void> => {
    await api.Post(`/changeRecord/publish/${changeRecordId}`, {});
  };

  return { publishChangeRecord };
};
