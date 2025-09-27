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
});
const model = defineModel<boolean | undefined>({
  required: true,
})

const validationRules = computed(() => {
  const rules: any[] = [];
  // no required rules for the checkbox input since it can be false by default
  return rules.join('|');
});
</script>

<template>
  <FormMoleculesAFormField :name="name" :label="label" type="checkbox" :placeholder="placeholder" :disabled="disabled"
    :required="required" :rules="validationRules" :hint="hint" v-model="model" />
</template>
