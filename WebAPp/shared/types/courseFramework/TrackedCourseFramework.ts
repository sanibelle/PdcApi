export type TrackedCourseFramework = {
  id: string;
  code: Changeable<string>;
  name: Changeable<string>;
  semester: Changeable<number>;
  createdOn: string;
  weighting: TrackedWeighting;
  changeRecordNumber: number;
  isDraft: boolean;
  competencies: Competency[];
  changeDetails: ChangeDetail[];
  changeRecordId: string;
};
