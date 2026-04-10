import type { ModalState } from '~~/shared/types/common/ModalState';

const state = reactive<ModalState>({
  isOpen: false,
  isSubmitting: false,
  options: {},
});

export function useModal() {
  const open = (options: GlobalModalOptions = {}) => {
    Object.assign(state.options, options, {
      component: options.component ? markRaw(options.component) : undefined,
    });
    state.isOpen = true;
  };

  const close = () => {
    state.isOpen = false;
    state.options.onCancel?.();
  };

  const confirm = async () => {
    if (!state.options.onConfirm) {
      state.isOpen = false;
      return;
    }

    try {
      state.isSubmitting = true;
      await state.options.onConfirm();
      if (state.options.closeOnConfirm !== false) {
        state.isOpen = false;
      }
    } finally {
      state.isSubmitting = false;
    }
  };

  return {
    // Expose state as readonly for the GlobalModal component
    state: readonly(state),
    open,
    close,
    confirm,
  };
}
