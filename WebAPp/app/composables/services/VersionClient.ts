import { useApi } from './ApiClient';

export const useVersionClient = () => {
  const api = useApi();
  const publishVersion = async (versionId: string): Promise<void> => {
    await api.Post(`/versions/${versionId}/publish`, {});
  };

  return { publishVersion };
};
