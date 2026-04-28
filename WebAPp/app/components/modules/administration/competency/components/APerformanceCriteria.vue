<script setup lang="ts">
  import '~/assets/css/form.css';
  const { t } = useI18n();

  const emit = defineEmits<{
    (e: 'deleteRow', competencyElementIndex: number, performanceCriteriaIndex: number): void;
  }>();

  defineProps({
    competencyElementIndex: {
      type: Number,
      required: true,
    },
    performanceCriteriaIndex: {
      type: Number,
      required: true,
    },
    indexToFocus: {
      type: Number,
      default: -1,
    },
  });

  const model = defineModel<PerformanceCriteria>({
    required: true,
  });
</script>

<template>
  <FormATextInput
    v-model="model.value"
    :name="`competencyElements[${competencyElementIndex}].performanceCriterias[${performanceCriteriaIndex}].value`"
    :min="3"
    :max="100"
    :required="true"
    :focus-on-mount="performanceCriteriaIndex === indexToFocus"
  >
    <CommonAtomsAButton
      :aria-label="t('deleteButtonText')"
      :data-testid="`delete-performance-criteria-button-${competencyElementIndex}-${performanceCriteriaIndex}`"
      :prevent-default="true"
      @click="() => emit('deleteRow', competencyElementIndex, performanceCriteriaIndex)"
    >
      -
    </CommonAtomsAButton>
  </FormATextInput>
</template>

<i18n lang="json">
{
  "fr": {
    "deleteButtonText": "Supprimer un critère de performance"
  }
}
</i18n>
