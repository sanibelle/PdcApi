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

  const fetchCompetencyByCode = async (programofstudyCode: string, competencyCode: string): Promise<Competency> => {
    const api = useApi();
    const competency = await api.Get<Competency>(`/programofstudy/${competencyCode}/competency/${competencyCode}`);
    if (competency == null) {
      throw new Error('Failed to fetch competency - server returned null response');
    }
    return competency;
  };

  const createCompetency = async (programCode: string, competency: Competency): Promise<Competency> => {
    const api = useApi();
    const createdCompetency = await api.Post<Competency>(`/programofstudy/${programCode}/competency`, competency);

    if (createdCompetency == null) {
      throw new Error('Failed to create competency - server returned null response');
    }
    return createdCompetency;
  };

  const updateCompetency = async (programCode: string, competency: Competency): Promise<Competency> => {
    const api = useApi();
    const updatedCompetency = await api.Put<Competency>(`/programofstudy/${programCode}/competency/${competency.code}`, competency);

    if (updatedCompetency == null) {
      throw new Error('Failed to update competency - server returned null response');
    }
    return updatedCompetency;
  };

  return { fetchCompetencies, createCompetency, fetchCompetencyByCode, updateCompetency };
};
