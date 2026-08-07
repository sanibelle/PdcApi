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
      default: '🔍',
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
    focusOnMount: {
      type: Boolean,
      default: false,
    },
  });

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

  const model = defineModel<string | number | undefined>({
    required: true,
  });
</script>

<template>
  <FormATextInput
    v-model="model"
    :name="props.name"
    :label="props.label"
    :placeholder="props.placeholder"
    :disabled="props.disabled"
    :required="props.required"
    :rules="validationRules"
    :hint="props.hint"
    :error-message="props.errorMessage"
    :focus-on-mount="props.focusOnMount"
  >
    <slot />
  </FormATextInput>
</template>
