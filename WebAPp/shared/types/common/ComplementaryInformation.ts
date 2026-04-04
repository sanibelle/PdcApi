export type ComplementaryInformation = {
  id?: string;
  text: string;
  changeRecord?: number;
  createdBy?: User;
  createdOn?: string;
};

export type EditableComplementaryInformation = {
  isInEdit?: boolean;
  originalText?: string;
} & ComplementaryInformation;
