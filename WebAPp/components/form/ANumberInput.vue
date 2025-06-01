<script setup lang="ts">

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
  integer: {
    type: Boolean,
    default: false,
  },
  modelValue: {
    type: [String, Number], // lorsque vide, c'est un String
    default: '',
  },
});

const emit = defineEmits(['update:modelValue']);

// Computed validation rules based on props
const validationRules = computed(() => {
  const rules = ['numeric'];

  if (props.required) {
    rules.push('required');
  }

  if (props.integer) {
    rules.push('integer');
  }

  if (props.min !== null) {
    rules.push(`min_value:${props.min}`);
  }

  if (props.max !== null) {
    rules.push(`max_value:${props.max}`);
  }

  return rules.join('|');
});
</script>

<template>
  <FormMoleculesAFormField :name="name" :label="label" type="number" :placeholder="placeholder" :disabled="disabled"
    :required="required" :rules="validationRules" :hint="hint" :modelValue="modelValue"
    @update:modelValue="$emit('update:modelValue', +$event)" />
</template>
