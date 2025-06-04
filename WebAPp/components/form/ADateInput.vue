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
    type: Date,
    default: null,
  },
  max: {
    type: Date,
    default: null,
  },
  modelValue: {
    type: Date,
    default: undefined,
  },
});

const emit = defineEmits(['update:modelValue']);

// Computed validation rules based on props
const validationRules = computed(() => {
  const rules = ['date'];

  if (props.required) {
    rules.push(`required`);
  }
  return rules.join('|');
});
</script>

<template>
  <FormMoleculesADateField :name="name" :label="label" type="date" :disabled="disabled" :required="required"
    :rules="validationRules" :hint="hint" :modelValue="modelValue" :max="max" :min="min"
    @update:modelValue="$emit('update:modelValue', $event)" />
</template>
