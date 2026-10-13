<script setup lang="ts">
  import { useField } from 'vee-validate';
  defineOptions({
    inheritAttrs: false,
  });

  const { t } = useI18n();

  const attrs = useAttrs();
  const props = defineProps({
    name: {
      type: String,
      required: true,
    },
    placeholder: {
      type: String,
      default: '',
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    rules: {
      type: [String, Object],
      default: '',
    },
    options: {
      type: Array as () => (string | number | object)[],
      required: true,
    },
    required: {
      type: Boolean,
      default: false,
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

  const emit = defineEmits(['update:errorMessage']);

  type SelectValue = string | number | undefined | null | object | (string | number | object)[];

  const model = defineModel<SelectValue>({
    required: true,
  });

  const { value, errorMessage, handleBlur, setValue, handleChange } = useField<SelectValue>(props.name, props.rules, {
    validateOnMount: false,
    initialValue: model.value,
    validateOnValueUpdate: false,
  });

  watch(
    () => model.value,
    async (newVal) => {
      // Devrait prévenir la recursion infinie
      if (newVal !== value.value) {
        await setValue(newVal);
      }
    },
  );

  const onChange = async (): Promise<void> => {
    handleChange(model.value, !!errorMessage.value);
  };

  watch(errorMessage, (newErrorMessage) => {
    emit('update:errorMessage', newErrorMessage);
  });
</script>

<template>
  <div
    class="select-wrapper"
    :class="{ 'is-disabled': disabled }"
  >
    <VueMultiselect
      v-bind="attrs"
      v-model="model"
      :name="name"
      :disabled="disabled"
      :options="options"
      :multiple="multiple"
      :label="trackBy"
      :track-by="trackBy"
      :placeholder="placeholder"
      :searchable="searchable"
      :allow-empty="!required"
      :select-label="t('selectLabel')"
      :selected-label="t('selectedLabel')"
      :deselect-label="t('deselectLabel')"
      :class="{ error: errorMessage }"
      :hide-selected="required"
      @select="onChange"
      @remove="onChange"
      @close="handleBlur"
    >
      <template #noResult>
        <span>{{ t('noResult') }}</span>
      </template>
      <template #noOptions>
        <span>{{ t('noOptions') }}</span>
      </template>
    </VueMultiselect>
  </div>
</template>

<style scoped>
  :deep(.multiselect.error) {
    border-color: #ef4444 !important;
    box-shadow: 0 0 0 1px rgb(239, 68, 68);
  }
</style>

<i18n lang="json">
{
  "fr": {
    "selectLabel": "✚",
    "selectedLabel": "Sélectionné",
    "deselectLabel": "✖",
    "noResult": "Aucun résultat",
    "noOptions": "Aucune option"
  }
}
</i18n>
