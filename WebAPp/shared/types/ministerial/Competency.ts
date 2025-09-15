export type Competency = {
  code: string;
  units?: Unit | null;
  isMandatory: boolean;
  isOptionnal: boolean;
  statementOfCompetency: string;
  realisationContexts: RealisationContext[];
  competencyElements: CompetencyElement[];
  isDraft: boolean;
  versionNumber?: number;
  versionId?: string;
};