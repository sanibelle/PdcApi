<script setup>
import { useField } from 'vee-validate';

const props = defineProps({
  id: {
    type: String,
    required: false,
    default: () => `input-${Math.random().toString(36).slice(2, 11)}`,
  },
  name: {
    type: String,
    required: true,
  },
  label: {
    type: String,
    default: '',
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
  required: {
    type: Boolean,
    default: false,
  },
  rules: {
    type: [String, Object, Function],
    default: '',
  },
  errorMessage: {
    type: String,
    default: '',
  },
  modelValue: {
    type: [String, Number],
    default: '',
  },
  hint: {
    type: String,
    default: '',
  },
});

defineEmits(['update:modelValue']);
const errorMessage = ref(props.errorMessage, 'errorMessage');

</script>

<template>
  <div>
    <FormAtomsABaseLabel :for-id="id" :required="required" v-if="label">
      {{ label }}
    </FormAtomsABaseLabel>
    <FormAtomsABaseInput :id="id" :name="name" :type="type" :placeholder="placeholder" :disabled="disabled"
      :rules="rules" :modelValue="modelValue" @update:modelValue="$emit('update:modelValue', $event)"
      @update:error-message="errorMessage = $event" />
    <FormAtomsAHint :hint="hint" />
    <FormAtomsAErrorMessage :message="errorMessage" />
  </div>
</template>
