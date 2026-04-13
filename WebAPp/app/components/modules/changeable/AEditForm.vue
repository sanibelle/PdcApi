<script setup lang="ts">
  const { t } = useI18n();

  const model = defineModel<Changeable>();
  const localModel = ref<Changeable>({ ...model.value! });

  const emits = defineEmits(['close']);

  const { handleSubmit, isSubmitting } = useForm<Changeable>({
    validateOnMount: false,
  });

  const { updateChangeable } = useChangeableClient();
  const onSubmit = handleSubmit(async () => {
    try {
      if (model.value) {
        model.value = await updateChangeable(localModel.value.id!, localModel.value);
        emits('close');
      }
    } catch (error) {
      alert(t('errorWhenUpdatingchangeable'));
      console.error('Error updating changeable:', error);
    }
  });
</script>

<template>
  <form
    v-if="model"
    class="edit-complementary-information"
    @submit.prevent="onSubmit"
  >
    <p class="note">{{ t('title') }}</p>
    <FormATextAreaInput
      v-model="localModel.value"
      :name="'comment-edit'"
      :required="true"
      :max="1000"
      class="comment-text"
    />
    <div class="flex">
      <FormMoleculesASubmitButton
        :is-submitting="isSubmitting"
        data-testid="submit-button"
      >
        {{ t('submit') }}
      </FormMoleculesASubmitButton>
      <CommonAtomsAButton @click="emits('close')">
        {{ t('cancel') }}
      </CommonAtomsAButton>
    </div>
  </form>
</template>

<i18n lang="json">
{
  "fr": {
    "title": "Cette modifcation ne sera pas suivie par le système de suivi des changements.",
    "errorWhenUpdatingchangeable": "Une erreur est survenue lors de la mise à jour.",
    "cancel": "Annuler",
    "submit": "Mettre à jour"
  }
}
</i18n>

<style scoped>
  .note {
    margin-bottom: 1rem;
    font-style: italic;
    color: #666;
    font-size: 0.875rem;
  }
</style>
