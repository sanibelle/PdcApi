export type CourseFramework = {
  code: string;
  name: string;
  theoryHours: number;
  laboratoryHours: number;
  personalWorkHours: number;
  semester: number;
  competencies: Competency[];
};

export type CourseFrameworkWeighting = {
  theoryHours: Changeable<number>;
  laboratoryHours: Changeable<number>;
  personnalWorkHours: Changeable<number>;
};

export type TrackedCourseFrameworkCompetency = Omit<Competency, 'isOptionnal'> & {
  isOptional: boolean;
};

export type TrackedCourseFramework = {
  id: string;
  code: Changeable<string>;
  name: Changeable<string>;
  semester: Changeable<number>;
  createdOn: string;
  weighting: CourseFrameworkWeighting;
  changeRecordNumber: number;
  isDraft: boolean;
  competencies: TrackedCourseFrameworkCompetency[];
};
