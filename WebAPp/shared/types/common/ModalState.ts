import type { GlobalModalOptions } from './ModalOptions';

export type ModalState = {
  isOpen: boolean;
  isSubmitting: boolean;
  options: GlobalModalOptions;
};
