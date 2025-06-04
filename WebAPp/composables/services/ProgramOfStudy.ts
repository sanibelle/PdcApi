// composables/services/ProgramService.ts
import { useApi } from './ApiClient';

export const useProgramOfStudy = () => {
  const fetchPrograms = async (): Promise<ProgramOfStudy[]> => {
    const api = useApi();
    const programs = await api.Get<ProgramOfStudy[]>('/ProgramOfStudy');
    if (programs == null) {
      throw new Error('Failed to create program - server returned null response');
    }
    return programs;
  };

  const createProgram = async (program: ProgramOfStudy): Promise<ProgramOfStudy> => {
    const api = useApi();
    const preparedProgram: ProgramOfStudy = {
      ...program,
      programType: +program.programType,
      publishedOn: toDateOnly(program.publishedOn) as any,
    };
    const createdProgram = await api.Post<ProgramOfStudy>('/ProgramOfStudy', preparedProgram);

    if (createdProgram == null) {
      throw new Error('Programs not found or not authenticated');
    }
    return createdProgram;
  };

  return { fetchPrograms, createProgram };
};
