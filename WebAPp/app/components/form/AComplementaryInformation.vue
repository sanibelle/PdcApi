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
      complementaryInformation.value!.writtenOnVersion = updatedItem.writtenOnVersion;
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
    <div class="flex-between">
      <FormMoleculesASubmitButton
        :is-submitting="isSubmitting"
        data-testid="submit-draft-button"
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

<style lang="scss" scoped>
  .comment {
    top: -1px;
    background: #fff;
    border: 1px solid #d0d0d0;
    border-left: 3px solid #f5c242;
    box-shadow: 0 1px 6px rgba(0, 0, 0, 0.1);
    transition: margin 0.3s ease;

    .flex-between {
      display: flex;
      justify-content: space-between;
      margin-top: 8px;
    }

    &.highlight {
      background: #f7b10059;
      box-shadow:
        0 0 0 2px #f5c242,
        0 2px 8px rgba(0, 0, 0, 0.15);
      margin-left: -2px;
    }

    .comment-entry {
      padding: 5px 6px;
      &:hover .btn-delete {
        opacity: 1;
      }
      &:hover .btn-edit {
        opacity: 1;
      }
    }

    .comment-header {
      display: flex;
      justify-content: space-between;
      align-items: flex-start;
    }

    .comment-meta {
      display: flex;
      flex-direction: column;
    }

    .comment-author {
      font-weight: 600;
      color: #333;
      font-size: 0.75rem;
      margin-bottom: 1px;
    }

    .comment-date {
      font-size: 0.6rem;
      color: #999;
      margin-bottom: 5px;
    }

    .comment-text {
      font-size: 0.7rem;
      color: #555;
    }

    .btn-delete {
      opacity: 0;
      transition: opacity 0.15s ease;
      background: none;
      border: none;
      cursor: pointer;
      padding: 2px 4px;
      border-radius: 3px;
      line-height: 1;

      &:hover {
        color: #c0392b;
        background: #fdecea;
      }
    }

    .btn-edit {
      opacity: 0;
      transition: opacity 0.15s ease;
      background: none;
      border: none;
      cursor: pointer;
      padding: 2px 4px;
      border-radius: 3px;
      line-height: 1;

      &:hover {
        color: #c0392b;
        background: #fdecea;
      }
    }
  }
</style>
