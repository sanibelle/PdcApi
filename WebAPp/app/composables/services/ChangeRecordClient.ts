import { useApi } from './ApiClient';

export const useChangeRecordClient = () => {
  const api = useApi();
  const publishChangeRecord = async (changeRecordId: string): Promise<void> => {
    await api.Post(`/changeRecords/publish/${changeRecordId}`, {});
  };

  return { publishChangeRecord };
};
