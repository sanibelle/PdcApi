<script setup lang="ts">
  import type { PropType } from 'vue';

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
      type: Array as PropType<SelectOption[] | object[]>,
      required: true,
    },
    multiple: {
      type: Boolean,
      default: false,
    },
    searchable: {
      type: Boolean,
      default: false,
    },
    trackBy: {
      type: String,
      default: '',
    },
  });

  // Computed validation rules based on props
  const validationRules = computed(() => {
    const rules: Record<string, boolean | number | string[]> = {};
    if (props.required) rules.required = true;
    return rules;
  });

  const model = defineModel<number | string | undefined | null | object | (string | number | object)[]>({
    required: true,
  });
</script>

<template>
  <FormMoleculesASelectField
    v-model="model"
    :name="name"
    :label="label"
    :placeholder="placeholder"
    :disabled="disabled"
    :required="required"
    :rules="validationRules"
    :hint="hint"
    :options="options"
    :multiple="multiple"
    :track-by="trackBy"
    :searchable="searchable"
  />
</template>
