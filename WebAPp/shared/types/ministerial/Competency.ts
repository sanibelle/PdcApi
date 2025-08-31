import type { Unit } from '../common/Unit';

export type Competency = {
  code: string;
  units?: Unit | null;
  isMandatory: boolean;
  isOptionnal: boolean;
  statementOfCompetency: string;
  realisationContexts: RealisationContext[];
  competencyElements: CompetencyElement[];
  versionNumber: number;
  isDraft: boolean;
  versionId: string;
};