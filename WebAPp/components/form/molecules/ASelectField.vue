<script setup lang="ts">

defineProps({
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
  options: {
    type: Array as PropType<SelectOption[]>,
    required: true,
  },
  modelValue: {
    type: [String, Number, null],
    default: '',
  },
});

const emit = defineEmits(['update:modelValue']);

// Get the error message from VeeValidate
const errorMessage = ref('');
</script>

<template>
  <div>
    <FormAtomsABaseLabel :for-id="id" :required="required" v-if="label">
      {{ label }}
    </FormAtomsABaseLabel>
    <FormAtomsASelect v-bind="$attrs" :name="name" :options="options" :placeholder="placeholder" :disabled="disabled"
      :rules="rules" :modelValue="modelValue" @update:modelValue="emit('update:modelValue', $event)"
      @update:error-message="errorMessage = $event" />
    <FormAtomsAErrorMessage :message="errorMessage" />
    <FormAtomsAHint :hint="hint" />
  </div>
</template>
