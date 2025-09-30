import type { Unit } from '../common/Unit';
import type { ProgramType } from '../enum/ProgramType';

type ProgramOfStudy = {
  code: string;
  specificUnits: Unit; 
  optionalUnits: Unit;
  generalUnits: Unit;
  complementaryUnits: Unit;
  name: string;
  programType: ProgramType;
  monthsDuration: number;
  specificDurationHours: number;
  totalDurationHours: number;
  publishedOn: Date;
};

export type { ProgramOfStudy };
