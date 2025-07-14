// composables/services/ProgramService.ts
import { useApi } from './ApiClient';

export const useProgramOfStudyClient = () => {
  const fetchPrograms = async (): Promise<ProgramOfStudy[]> => {
    const api = useApi();
    const programs = await api.Get<ProgramOfStudy[]>('/ProgramOfStudy');
    if (programs == null) {
      throw new Error('Failed to create program - server returned null response');
    }
    return programs;
  };

  const fetchProgramByCode = async (code: string): Promise<ProgramOfStudy> => {
    const api = useApi();
    const program = await api.Get<ProgramOfStudy>(`/ProgramOfStudy/${code}`);
    if (program == null) {
      throw new Error(`Program with code ${code} not found`);
    }
    return program;
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

  return { fetchPrograms, fetchProgramByCode, createProgram };
};
