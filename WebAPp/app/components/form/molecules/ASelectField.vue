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
      type: Array as PropType<(string | number | object)[]>,
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

  // Get the error message from VeeValidate
  const errorMessage = ref('');

  const model = defineModel<string | number | undefined | null | object | (string | number | object)[]>({
    required: true,
  });
</script>

<template>
  <div>
    <FormAtomsABaseLabel
      v-if="label"
      :for-id="id"
      :required="required"
    >
      {{ label }}
    </FormAtomsABaseLabel>
    <FormAtomsASelect
      v-bind="$attrs"
      v-model="model"
      :name="name"
      :options="options"
      :placeholder="placeholder"
      :disabled="disabled"
      :rules="rules"
      :multiple="multiple"
      :track-by="trackBy"
      :searchable="searchable"
      @update:error-message="errorMessage = $event"
    />
    <FormAtomsAErrorMessage :message="errorMessage" />
    <FormAtomsAHint :hint="hint" />
  </div>
</template>
