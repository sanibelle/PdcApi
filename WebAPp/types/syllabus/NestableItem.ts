export default interface NestableItem {
  name: string;
  id: number;
  children?: NestableItem[];
  isCompleted?: boolean;
}
