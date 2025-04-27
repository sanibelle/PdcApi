// composables/services/ProgramService.ts
import type { ProgramOfStudy } from '~/types/Ministerial/ProgramOfStudy';
import { useApi } from './ApiClient';

// Use named export instead of default export
export const useProgramOfStudy = () => {
  const fetchPrograms = async (): Promise<ProgramOfStudy> => {
    const api = useApi();
    const programs = await api.Get<ProgramOfStudy>('/ProgramOfStudy');
    if (programs == null) {
      throw new Error('Programs not found or not authenticated');
    }
    return programs;
  };

  return { fetchPrograms };
};
