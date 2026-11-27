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
  });

  const validationRules = computed(() => {
    const rules: Record<string, boolean | number | string[]> = {};

    if (props.required) rules.required = true;
    if (props.integer) rules.integer = true;
    if (props.min !== null) rules.min = props.min;
    if (props.max !== null) rules.max = props.max;
    return rules;
  });
</script>

<template>
  <FormMoleculesAFormField
    v-model="model"
    :name="name"
    :label="label"
    :type="'number'"
    :placeholder="placeholder"
    :disabled="disabled"
    :required="required"
    :rules="validationRules"
    :hint="hint"
  />
</template>
