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
  type: {
    type: String,
    default: 'text',
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
  errorMessage: {
    type: String,
    default: '',
  },
  hint: {
    type: String,
    default: '',
  },
});

const model = defineModel<string | number | boolean | undefined>({
  required: true,
})

const error = toRef(props.errorMessage);

</script>

<template>
  <div>
    <FormAtomsABaseLabel :for-id="id" :required="required" v-if="label">
      {{ label }}
    </FormAtomsABaseLabel>
    <FormAtomsACheckboxInput v-if="type === 'checkbox'" v-bind="$attrs" :id="id" :name="name" :placeholder="placeholder"
      :disabled="disabled" :rules="rules" v-model="model as any" @update:error-message="error = $event" />
    <FormAtomsABaseInput v-else v-bind="$attrs" :id="id" :name="name" :placeholder="placeholder" :disabled="disabled"
      :rules="rules" v-model="model as any" @update:error-message="error = $event" />
    <FormAtomsAErrorMessage :message="error" />
    <FormAtomsAHint v-if="hint" :text="hint" />
  </div>
</template>
