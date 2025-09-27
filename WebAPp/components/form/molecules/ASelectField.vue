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
});

// Get the error message from VeeValidate
const errorMessage = ref('');

const model = defineModel<string | number | null>({
  required: true,
})
</script>

<template>
  <div>
    <FormAtomsABaseLabel :for-id="id" :required="required" v-if="label">
      {{ label }}
    </FormAtomsABaseLabel>
    <FormAtomsASelect v-bind="$attrs" :name="name" :options="options" :placeholder="placeholder" :disabled="disabled"
      :rules="rules" v-model="model" @update:error-message="errorMessage = $event" />
    @update:error-message="errorMessage = $event" />
    <FormAtomsAErrorMessage :message="errorMessage" />
    <FormAtomsAHint :hint="hint" />
  </div>
</template>
