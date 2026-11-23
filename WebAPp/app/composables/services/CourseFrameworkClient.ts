import { useApi } from './ApiClient';

export const useCourseFrameworkClient = () => {
  const fetchCourseFrameworks = async (programCode: string): Promise<CourseFramework[]> => {
    const api = useApi();
    const courseFrameworks = await api.Get<CourseFramework[]>(`/programOfStudy/${programCode}/courseFramework`);
    if (courseFrameworks == null) {
      throw new Error('Failed to create program - server returned null response');
    }
    return courseFrameworks;
  };

  const fetchCourseFrameworkByCode = async (code: string): Promise<TrackedCourseFramework> => {
    const api = useApi();
    const courseFramework = await api.Get<CourseFramework>(`/programOfStudy/${code}/courseFramework`);
    if (courseFramework == null) {
      throw new Error(`Program with code ${code} not found`);
    }
    return courseFramework;
  };

  const createCourseFramework = async (courseFramework: CourseFramework, programCode: string): Promise<TrackedCourseFramework> => {
    const api = useApi();
    const preparedCourseFramework: CourseFramework = {
      ...courseFramework,
    };
    const createdCourseFramework = await api.Post<TrackedCourseFramework, CourseFramework>(`/programOfStudy/${programCode}/courseFramework`, preparedCourseFramework);

    if (createdCourseFramework == null) {
      throw new Error('Course frameworks not found or not authenticated');
    }
    return createdCourseFramework;
  };

  return { fetchCourseFrameworks, fetchCourseFrameworkByCode, createCourseFramework };
};
