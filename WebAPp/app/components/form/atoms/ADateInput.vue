<script setup lang="ts">
import { useField } from 'vee-validate';

const props = defineProps({
  id: {
    type: String,
    required: true,
  },
  name: {
    type: String,
    required: true,
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
  min: {
    type: Date,
    default: null,
  },
  max: {
    type: Date,
    default: null,
  }
});

const emit = defineEmits(['update:errorMessage']);

// Use VeeValidate's useField to handle validation
const { value, handleChange, setValue, errorMessage } = useField(props.name, props.rules);

const model = defineModel<Date | undefined>({
  required: true,
})

watch(
  () => model.value,
  (newValue) => {
    if (newValue !== value.value) {
      setValue(newValue);
    }
  }
);

const onChange = (date: Date) => {
  handleChange(date, !!errorMessage.value);
};

watch(
  errorMessage,
  (newErrorMessage) => {
    emit('update:errorMessage', newErrorMessage);
  }
);
</script>

<template>
  <VueDatePicker v-model="model" :enable-time-picker="false" :teleport="true" :auto-apply="true"
    :class="{ 'is-invalid': errorMessage }" :min-date="min" :max-date="max" @update:model-value="onChange"
    :data-testid="name" />
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

  .base-input.error {
    border-color: #ef4444;
  }
}
</style>
