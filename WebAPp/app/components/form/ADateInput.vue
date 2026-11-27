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
  });

  // Computed validation rules based on props
  const validationRules = computed(() => {
    const rules: Record<string, boolean | number | string[]> = {};

    if (props.required) rules.required = true;
    rules.date = true;
    return rules;
  });

  const model = defineModel<Date | undefined>({
    required: true,
  });
</script>

<template>
  <FormMoleculesADateField
    :name="name"
    :label="label"
    type="date"
    :disabled="disabled"
    :required="required"
    :rules="validationRules"
    :hint="hint"
    v-model="model"
    :max="max"
    :min="min"
  />
</template>
