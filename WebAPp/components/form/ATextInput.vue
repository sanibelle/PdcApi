<script setup>
import { ref, computed, watch } from 'vue';

const props = defineProps({
  name: {
    type: String,
    required: true,
  },
  label: {
    type: String,
    default: '',
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
  hint: {
    type: String,
    default: '',
  },
  min: {
    type: Number,
    default: null,
  },
  max: {
    type: Number,
    default: null,
  },
  pattern: {
    type: String,
    default: null,
  },
  errorMessage: {
    type: String,
    default: '',
  },
  modelValue: {
    type: String,
    default: '',
  },
});

const emit = defineEmits(['update:modelValue']);

const validationRules = computed(() => {
  const rules = [];

  if (props.required) {
    rules.push('required');
  }

  if (props.min) {
    rules.push(`min:${props.min}`);
  }

  if (props.max) {
    rules.push(`max:${props.max}`);
  }

  if (props.pattern) {
    rules.push(`regex:${props.pattern}`);
  }

  return rules.join('|');
});
</script>

<template>
  <FormMoleculesAFormField :name="name" :label="label" type="text" :placeholder="placeholder" :disabled="disabled"
    :required="required" :rules="validationRules" :hint="hint" :modelValue="modelValue" :error-message="errorMessage"
    @update:modelValue="$emit('update:modelValue', $event)" />
</template>
