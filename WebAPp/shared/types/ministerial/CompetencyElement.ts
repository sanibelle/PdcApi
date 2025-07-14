export type CompetencyElement = {
  id?: string;
  position: number;
  text: string;
  performanceCriterias?: PerformanceCriteria[];
  complementaryInformations?: ComplementaryInformation[];
};