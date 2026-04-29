export type Competency = {
  code: string;
  units?: Unit | null;
  isMandatory: boolean;
  isOptionnal: boolean;
  statementOfCompetency: string;
  realisationContexts: RealisationContext[];
  competencyElements: CompetencyElement[];
  changeDetails: ChangeDetail[];
  isDraft: boolean;
  changeRecordNumber?: number;
  changeRecordId?: string;
};
