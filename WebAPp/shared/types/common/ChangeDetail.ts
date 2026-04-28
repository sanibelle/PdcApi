import type { ChangeType } from '../enum/ChangeType';

export type ChangeDetail = {
  id?: string;
  oldValue?: string;
  changeableId: string;
  changeType: ChangeType;
};
