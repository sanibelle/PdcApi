export type CourseFramework = {
  code: string;
  name: string;
  theoryHours: number;
  laboratoryHours: number;
  personalWorkHours: number;
  semester: number;
  competencies: Competency[];
};
