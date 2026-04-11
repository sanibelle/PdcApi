<script setup lang="ts">
  import type { ComplementaryInformation, EditableComplementaryInformation } from '~~/shared/types/common/ComplementaryInformation';

  const { t } = useI18n();

  const complementaryInformation = defineModel<EditableComplementaryInformation>();
  const emit = defineEmits(['cancel']);

  const { updateComplementaryInformation } = useComplementaryInformationClient();

  const { handleSubmit, isSubmitting } = useForm<ComplementaryInformation>({
    validateOnMount: false,
  });

  const onSubmit = handleSubmit(async () => {
    try {
      const updatedItem = await updateComplementaryInformation(complementaryInformation.value!);
      complementaryInformation.value!.text = updatedItem.text;
      complementaryInformation.value!.changeRecordNumber = updatedItem.changeRecordNumber;
      complementaryInformation.value!.isInEdit = false;
    } catch (error) {
      alert(t('errorWhenUpdatingComplementaryInformation'));
      console.error('Error updating complementary information:', error);
    }
  });
</script>

<template>
  <form
    class="edit-complementary-information"
    @submit.prevent="onSubmit"
  >
    <FormATextAreaInput
      v-model="complementaryInformation!.text"
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
      <CommonAtomsAButton
        :class="'cancel'"
        @click="emit('cancel')"
      >
        {{ t('cancel') }}
      </CommonAtomsAButton>
    </div>
  </form>
</template>

<i18n lang="json">
{
  "fr": {
    "unknownUser": "Utilisateur inconnu",
    "cancel": "Annuler",
    "submit": "Envoyer",
    "addComment": "Ajouter un commentaire...",
    "errorWhenDeletingComplementaryInformation": "Une erreur est survenue lors de la suppression du commentaire.",
    "errorWhenUpdatingComplementaryInformation": "Une erreur est survenue lors de la mise à jour du commentaire."
  }
}
</i18n>
