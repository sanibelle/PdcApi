<script setup lang="ts">
  const generatedId = useId();

  const props = defineProps({
    id: {
      type: String,
      required: false,
      default: undefined,
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
    focusOnMount: {
      type: Boolean,
      default: false,
    },
  });

  const model = defineModel<string | number | boolean | undefined | null>({
    required: true,
  });

  const error = toRef(props.errorMessage);

  const inputId = computed(() => props.id || generatedId);

  const slots = useSlots();
</script>

<template>
  <div class="flex">
    <FormAtomsABaseLabel
      :for-id="inputId"
      :required="required"
      v-if="label"
    >
      {{ label }}
    </FormAtomsABaseLabel>
    <FormAtomsACheckboxInput
      v-if="type === 'checkbox'"
      v-bind="$attrs"
      :id="inputId"
      :name="name"
      :placeholder="placeholder"
      :disabled="disabled"
      :rules="rules"
      v-model="model as any"
      @update:error-message="error = $event"
    />
    <FormAtomsABaseInput
      v-else
      v-bind="$attrs"
      :id="inputId"
      :name="name"
      :placeholder="placeholder"
      :disabled="disabled"
      :rules="rules"
      :focus-on-mount="focusOnMount"
      v-model="model as any"
      @update:error-message="error = $event"
    />
    <div
      v-if="slots.default"
      class="slots"
    >
      <slot></slot>
    </div>
    <FormAtomsAHint
      v-if="hint"
      :text="hint"
    />
  </div>
  <FormAtomsAErrorMessage :message="error" />
</template>

<style scoped lang="scss">
  .flex {
    display: flex;
  }
  .slots {
    margin-left: 0.15rem;
  }
</style>
