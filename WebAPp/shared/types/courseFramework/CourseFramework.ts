export type CourseFramework = {
  courseCode: string;
  units?: Unit | null;
  name: string;
  competencyElements: CompetencyElement[];
  isDraft: boolean;
  changeRecordNumber?: number;
  changeRecordId?: string;
};
