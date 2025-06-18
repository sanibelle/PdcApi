<script setup lang="ts">
import { useField } from 'vee-validate';

const props = defineProps({
  name: {
    type: String,
    required: true,
  },
  type: {
    type: String,
    default: 'text',
  },
  placeholder: {
    type: String,
    default: '',
  },
  disabled: {
    type: Boolean,
    default: false,
  },
  rules: {
    type: [String, Object],
    default: '',
  },
  modelValue: {
    type: [String, Number],
    default: '',
  },
});

const emit = defineEmits(['update:modelValue', 'update:errorMessage']);

const { value, errorMessage, handleBlur, setValue, handleChange } = useField(props.name, props.rules, {
  validateOnMount: false,
  initialValue: props.modelValue,
  validateOnValueUpdate: false,
});


watch(
  () => props.modelValue,
  (newValue) => {
    if (newValue !== value.value) {
      setValue(newValue);
    }
  }
);

const onChange = (event: Event) => {
  handleChange(event, !!errorMessage.value);
  const target = event.target as HTMLInputElement;
  emit('update:modelValue', target.value);
};

const onBlur = (event: Event) => {
  handleBlur(event, true);
};

watch(
  errorMessage,
  (newErrorMessage) => {
    emit('update:errorMessage', newErrorMessage);
  }
);
</script>

<template>
  <input :name="name" :value="value" :type="type" :placeholder="placeholder" :disabled="disabled"
    class="base-input" :class="{ error: errorMessage }" @input="onChange" @blur="onBlur" />
</template>

<style scoped>
.base-input {
  width: 100%;
  padding: 8px 12px;
  border: 1px solid #e2e8f0;
  border-radius: 4px;
  font-size: 14px;
  transition: border-color 0.2s ease;

  &:focus {
    outline: none;
    border-color: #3b82f6;
    box-shadow: 0 0 0 1px rgba(59, 130, 246, 0.5);
  }

  &:disabled {
    background-color: #f1f5f9;
    cursor: not-allowed;
  }

  &.error {
    border-color: #ef4444;
  }
}
</style>
