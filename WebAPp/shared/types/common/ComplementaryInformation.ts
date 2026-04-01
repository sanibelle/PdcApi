export type ComplementaryInformation = {
  id?: string;
  text: string;
  writtenOnVersion?: number;
  createdBy?: User;
  createdOn?: string;
};

export type EditableComplementaryInformation = {
  isInEdit?: boolean;
  originalText?: string;
} & ComplementaryInformation;
