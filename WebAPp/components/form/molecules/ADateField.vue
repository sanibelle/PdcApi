<script setup lang="ts">

const props = defineProps({
  id: {
    type: String,
    required: false,
    default: () => `input-${Math.random().toString(36).slice(2, 11)}`,
  },
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
  rules: {
    type: [String, Object, Function],
    default: '',
  },
  hint: {
    type: String,
    default: '',
  },
  modelValue: {
    type: Date,
    default: undefined,
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

defineEmits(['update:modelValue']);
const errorMessage = ref('');

</script>

<template>
  <div>
    <FormAtomsABaseLabel :for-id="id" :required="required" v-if="label">
      {{ label }}
    </FormAtomsABaseLabel>
    <FormAtomsADateInput :id="id" :name="name" :placeholder="placeholder" :disabled="disabled" :rules="rules"
      :modelValue="modelValue" @update:modelValue="$emit('update:modelValue', $event)" :max="max" :min="min"
      @update:error-message="errorMessage = $event" :date-only="true" />
    <FormAtomsAErrorMessage :message="errorMessage" />
    <FormAtomsAHint :hint="hint" />
  </div>
</template>
