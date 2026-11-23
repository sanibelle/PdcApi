export type Changeable<T = string> = {
  id?: string;
  value: T;
  position?: number | null;
  complementaryInformations?: ComplementaryInformation[];
};
