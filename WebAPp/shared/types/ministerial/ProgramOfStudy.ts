import type { Unit } from '../common/Unit';
import type { ProgramType } from '../enum/ProgramType';

type ProgramOfStudy = {
  code: string;
  specificUnits: Unit | Partial<Unit>;
  optionalUnits: Unit | Partial<Unit>;
  generalUnits: Unit | Partial<Unit>;
  complementaryUnits: Unit | Partial<Unit>;
  name: string;
  programType: ProgramType;
  monthsDuration: number;
  specificDurationHours: number;
  totalDurationHours: number;
  publishedOn: Date;
};

export type { ProgramOfStudy };
