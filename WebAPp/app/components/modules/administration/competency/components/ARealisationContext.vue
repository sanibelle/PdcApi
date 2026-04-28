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

  const model = defineModel<RealisationContext>({
    required: true,
  });
</script>

<template>
  <FormATextInput
    v-model="model.value"
    :focus-on-mount="index === indexToFocus"
    :name="`competency.realisationContexts[${index}].value`"
    :min="3"
    :max="100"
    :required="true"
  >
    <CommonAtomsAButton
      :data-testid="`delete-realisation-context-button-${index}`"
      :aria-label="t('deleteRealisationContext')"
      :prevent-default="true"
      @click.prevent="() => emit('deleteRow', index)"
    >
      -
    </CommonAtomsAButton>
  </FormATextInput>
</template>

<i18n lang="json">
{
  "fr": {
    "deleteRealisationContext": "Supprimer ce contexte de réalisation"
  }
}
</i18n>
