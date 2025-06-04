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
  options: {
    type: Array<SelectOption>,
    required: true
  },
  modelValue: {
    type: [String, Number],
    default: '',
  },
});

const emit = defineEmits(['update:modelValue']);

// Computed validation rules based on props
const validationRules = computed(() => {
  const rules = [];
  if (props.required) {
    rules.push('required');
  }
  return rules.join('|');
});
</script>

<template>
  <FormMoleculesASelectField :name="name" :label="label" type="text" :placeholder="placeholder" :disabled="disabled"
    :required="required" :rules="validationRules" :hint="hint" :options="options" :modelValue="modelValue"
    @update:modelValue="$emit('update:modelValue', $event)" />
</template>
