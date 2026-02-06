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
});

const model = defineModel<number | undefined | null>({
  required: true,
})

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
    :required="required" :rules="validationRules" :hint="hint" v-model="model" />
</template>
