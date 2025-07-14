// composables/services/Competency.ts
import { useApi } from './ApiClient';
import type { Competency } from '~/shared/types/ministerial/Competency';

export const useCompetencyClient = () => {
  const fetchCompetencies = async (programCode: string): Promise<Competency[]> => {
    const api = useApi();
    const competencies = await api.Get<Competency[]>(`/programofstudy/${programCode}/competency`);
    if (competencies == null) {
      throw new Error('Failed to fetch competencies - server returned null response');
    }
    return competencies;
  };

  const createCompetency = async (programCode: string, competency: Competency): Promise<Competency> => {
    const api = useApi();
    const createdCompetency = await api.Post<Competency>(`/programofstudy/${programCode}/competency`, competency);

    if (createdCompetency == null) {
      throw new Error('Failed to create competency - server returned null response');
    }
    return createdCompetency;
  };

  return { fetchCompetencies, createCompetency };
};
