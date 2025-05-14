// composables/services/ProgramService.ts
import type { ProgramOfStudy } from '~/types/ministerial/ProgramOfStudy';
import { useApi } from './ApiClient';

export const useProgramOfStudy = () => {
  const fetchPrograms = async (): Promise<ProgramOfStudy[]> => {
    const api = useApi();
    const programs = await api.Get<ProgramOfStudy[]>('/ProgramOfStudy');
    if (programs == null) {
      throw new Error('Programs not found or not authenticated');
    }
    return programs;
  };

  const createProgram = async (program: ProgramOfStudy): Promise<ProgramOfStudy> => {
    const api = useApi();
    const preparedProgram: ProgramOfStudy = {
      ...program,
      programType: +program.programType,
      publishedOn: formatDate(program.publishedOn) as any,
    };
    const createdProgram = await api.Post<ProgramOfStudy>('/programOfStudy', preparedProgram);

    if (createdProgram == null) {
      throw new Error('Programs not found or not authenticated');
    }
    return createdProgram;
  };

  return { fetchPrograms, createProgram };
};

const formatDate = (date: Date): string => {
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, '0'); // months are 0-indexed
  const day = String(date.getDate()).padStart(2, '0');

  return `${year}-${month}-${day}`;
};
