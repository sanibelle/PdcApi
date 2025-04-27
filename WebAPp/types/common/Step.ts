export interface Step {
  id: number;
  title: string;
  isSelected: boolean;
  isDisabled: boolean;
  isCompleted: boolean;
  content: Component;
}
