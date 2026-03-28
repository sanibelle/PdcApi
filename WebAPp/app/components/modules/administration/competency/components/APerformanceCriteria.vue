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
    :name="`competency.competencyElements[${competencyElementIndex}].performanceCriterias[${performanceCriteriaIndex}].value`"
    :min="3"
    :max="100"
    :required="true"
    :focus-on-mount="performanceCriteriaIndex === indexToFocus"
    v-model="model.value"
  >
    <CommonAtomsAButton
      :aria-label="t('deleteButtonText')"
      :data-testid="`delete-performance-criteria-button-${competencyElementIndex}-${performanceCriteriaIndex}`"
      @click="() => emit('deleteRow', competencyElementIndex, performanceCriteriaIndex)"
      :preventDefault="true"
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
