export type ComplementaryInformation = {
  id?: string;
  text: string;
  writtenOnVersion?: number;
  // TODO fix the DTO or the type for createdBy
  createdBy?: string;
};
