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
    trimmed: {
      type: Boolean,
      default: false,
    },
    pattern: {
      type: String,
      default: null,
    },
    patternMessage: {
      type: String,
      default: null,
    },
    errorMessage: {
      type: String,
      default: '',
    },
    focusOnMount: {
      type: Boolean,
      default: false,
    },
  });

  const validationRules = computed(() => {
    const rules: Record<string, boolean | number | string[]> = {};

    if (props.required) rules.required = true;
    if (props.min) rules.min = props.min;
    if (props.max) rules.max = props.max;
    if (props.trimmed) rules.trimmed = true;

    if (props.pattern) {
      if (props.patternMessage) {
        rules.patternWithMessage = [props.pattern, props.patternMessage];
      } else {
        rules.regex = [props.pattern];
      }
    }
    return rules;
  });

  const model = defineModel<string | number | undefined>({
    required: true,
  });
</script>

<template>
  <FormMoleculesAFormField
    v-model="model"
    :name="name"
    :label="label"
    type="text"
    :placeholder="placeholder"
    :disabled="disabled"
    :required="required"
    :rules="validationRules"
    :hint="hint"
    :error-message="errorMessage"
    :focus-on-mount="focusOnMount"
  >
    <slot />
  </FormMoleculesAFormField>
</template>
