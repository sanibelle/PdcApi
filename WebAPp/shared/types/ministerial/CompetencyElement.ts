export type CompetencyElement = Changeable & {
  position: number;
  performanceCriterias?: PerformanceCriteria[];
  complementaryInformations?: ComplementaryInformation[];
};