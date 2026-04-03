<script setup lang="ts">
  import '~/assets/css/form.css';

  const { t } = useI18n();

  const emit = defineEmits<{
    (e: 'deleteRow', index: number): void;
  }>();
  defineProps({
    index: {
      type: Number,
      required: true,
    },
    indexToFocus: {
      type: Number,
      default: -1,
    },
  });

  const model = defineModel<CompetencyElement>({
    required: true,
  });
</script>

<template>
  <FormATextInput
    v-model="model.value"
    :name="`competency.competencyElements[${index}].value`"
    :min="3"
    :max="100"
    :required="true"
    :focus-on-mount="index === indexToFocus"
  >
    <CommonAtomsAButton
      :data-testid="`delete-competency-element-button-${index}`"
      :aria-label="t('deleteCompetencyElement')"
      :prevent-default="true"
      @click="() => emit('deleteRow', index)"
    >
      -
    </CommonAtomsAButton>
  </FormATextInput>
</template>

<i18n lang="json">
{
  "fr": {
    "deleteCompetencyElement": "Supprimer cet élément de compétence"
  }
}
</i18n>
