export type GlobalModalOptions = {
  title?: string;
  content?: string;
  component?: Component; // will replace the content
  componentProps?: Record<string, unknown>; // props to pass to the component
  // Behaviour
  closeOnConfirm?: boolean;
  hideFooter?: boolean;
  confirmLabel?: string;
  cancelLabel?: string;
  // Callbacks
  onConfirm?: () => void | Promise<void>;
  onCancel?: () => void;
};
