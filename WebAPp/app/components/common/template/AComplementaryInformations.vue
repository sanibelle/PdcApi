<script setup lang="ts">
  import type { EditableComplementaryInformation } from '~~/shared/types/common/ComplementaryInformation';

  const { t } = useI18n();

  const complementaryInformations = defineModel<EditableComplementaryInformation[]>();
  const props = defineProps({
    isViewOnly: {
      default: true,
      type: Boolean,
    },
    changeableId: {
      type: String,
      required: false,
    },
  });

  const hasComments = computed(() => complementaryInformations.value && complementaryInformations.value.length > 0);
  const isHovered = ref(false);
  const commentRef = ref<HTMLDivElement | null>(null);
  const showForm = ref(false);

  const scrollToComment = () => {
    commentRef.value?.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
  };

  const handleShowFormClick = () => {
    showForm.value = !showForm.value;
    comment.value = '';
  };

  const onCancelClick = () => {
    showForm.value = false;
  };

  const { createComplementaryInformation, deleteComplementaryInformation, updateComplementaryInformation } = useComplementaryInformationClient();
  const comment = ref('');

  const onAddClick = async () => {
    try {
      if (props.changeableId) {
        const newItem = await createComplementaryInformation(props.changeableId, { text: comment.value });
        if (complementaryInformations.value) {
          complementaryInformations.value.push(newItem);
        } else {
          complementaryInformations.value = [newItem];
        }
      } else {
        // TODO error management with nice modal
        alert(t('errorAddingCommentMissingChangeableId'));
      }
    } catch (error) {
      // TODO error management with nice modal
      alert(t('errorWhenAddingComplementaryInformation'));
      console.error('Error creating complementary information:', error);
    }
    showForm.value = false;
  };

  const onEditClick = async (complementaryInformation: EditableComplementaryInformation) => {
    try {
      if (props.changeableId) {
        const updatedItem = await updateComplementaryInformation(complementaryInformation);
        const itemToUpdate = complementaryInformations.value?.find((x) => x.id === updatedItem.id);
        if (itemToUpdate) {
          itemToUpdate.text = updatedItem.text;
          itemToUpdate.writtenOnVersion = updatedItem.writtenOnVersion;
          complementaryInformation.isInEdit = false;
        } else {
          // TODO error management with nice modal
          alert(t('errorWhenUpdatingComplementaryInformation'));
        }
      } else {
        alert(t('errorWhenUpdatingComplementaryInformation'));
      }
    } catch (error) {
      alert(t('errorWhenUpdatingComplementaryInformation'));
      console.error('Error updating complementary information:', error);
    }
    showForm.value = false;
  };

  const onDeleteClick = async (id: string | undefined) => {
    //TODO ajouter une validation qui demande si on est certain.
    try {
      if (!id) {
        // TODO error management with nice modal
        alert(t('errorWhenDeletingComplementaryInformation'));
        console.error('Error deleting complementary information: id is undefined');
        return;
      }
      await deleteComplementaryInformation(id);
      if (complementaryInformations.value) {
        complementaryInformations.value = complementaryInformations.value.filter((item) => item.id !== id);
      }
    } catch (error) {
      // TODO error management with nice modal
      alert(t('errorWhenDeletingComplementaryInformation'));
      console.error('Error deleting complementary information:', error);
    }
  };

  const showComplementaryInformationEditForm = (complementaryInformation: EditableComplementaryInformation) => {
    complementaryInformation.isInEdit = true;
    complementaryInformation.originalText = complementaryInformation.text;
  };

  const hideComplementaryInformationEditForm = (complementaryInformation: EditableComplementaryInformation) => {
    complementaryInformation.isInEdit = false;
    complementaryInformation.text = complementaryInformation.originalText ?? '';
  };

  //TODO move that to an utils folder
  const truncateText = (text: string | null | undefined, maxLength: number) => {
    if (!text) {
      return '';
    }

    if (text.length <= maxLength) {
      return text;
    }
    return text.substring(0, maxLength) + '...';
  };
</script>

<template>
  <div>
    <div
      :class="{ 'has-comment': hasComments, highlight: isHovered && hasComments, commentable: true }"
      @mouseenter="
        isHovered = true;
        scrollToComment();
      "
      @mouseleave="isHovered = false"
    >
      <span
        v-if="!isViewOnly"
        class="add-comment-btn"
        @click="handleShowFormClick"
      ></span>
      <slot></slot>
      <Transition name="slide-fade">
        <div
          class="add-comment-form"
          v-if="showForm"
        >
          <FormATextAreaInput
            :focus-on-mount="true"
            v-model="comment"
            name="comment"
            :required="true"
            :max="1000"
            :placeholder="t('addComment')"
          />
          <div class="add-comment-actions">
            <CommonAtomsAButton @click="onAddClick">{{ t('submit') }}</CommonAtomsAButton>
            <CommonAtomsAButton @click="onCancelClick">{{ t('cancel') }}</CommonAtomsAButton>
          </div>
        </div>
      </Transition>
    </div>
    <Teleport to="#comments-panel">
      <div
        class="comment"
        :class="{ highlight: isHovered }"
        v-if="hasComments"
        @mouseenter="isHovered = true"
        @mouseleave="isHovered = false"
        ref="commentRef"
      >
        <div
          class="comment-entry"
          v-for="complementaryInformation of complementaryInformations"
          :key="complementaryInformation.id"
        >
          <div class="comment-header">
            <div class="comment-meta">
              <div class="comment-author">{{ truncateText(complementaryInformation.createdBy?.userName, 20) || t('unknownUser') }}</div>
              <div class="comment-date">
                <CommonMoleculesADate :date="complementaryInformation.createdOn" />
              </div>
            </div>
            <!-- // TODO reutiliser le funky close button -->
            <template v-if="!isViewOnly && !complementaryInformation.isInEdit">
              <button
                @click="onDeleteClick(complementaryInformation.id)"
                class="btn-delete"
              >
                ✕
              </button>
              <button
                @click="showComplementaryInformationEditForm(complementaryInformation)"
                class="btn-edit"
              >
                ✎
              </button>
            </template>
          </div>
          <FormATextAreaInput
            v-if="complementaryInformation.isInEdit"
            v-model="complementaryInformation.text"
            name="comment-edit"
            :required="true"
            :max="1000"
            class="comment-text"
          />
          <div
            v-else
            class="comment-text"
          >
            {{ complementaryInformation.text }}
          </div>
          <div
            v-if="complementaryInformation.isInEdit"
            class="flex-between"
          >
            <CommonAtomsAButton @click="hideComplementaryInformationEditForm(complementaryInformation)">
              {{ t('cancel') }}
            </CommonAtomsAButton>
            <CommonAtomsAButton @click="onEditClick(complementaryInformation)">
              {{ t('submit') }}
            </CommonAtomsAButton>
          </div>
        </div>
      </div>
    </Teleport>
  </div>
</template>

<i18n lang="json">
{
  "fr": {
    "unknownUser": "Utilisateur inconnu",
    "cancel": "Annuler",
    "submit": "Envoyer",
    "addComment": "Ajouter un commentaire...",
    "errorAddingCommentMissingChangeableId": "Erreur lors de l'ajout du commentaire : changeableId manquant.",
    "errorWhenAddingComplementaryInformation": "Une erreur est survenue lors de l'ajout du commentaire.",
    "errorWhenDeletingComplementaryInformation": "Une erreur est survenue lors de la suppression du commentaire.",
    "errorWhenUpdatingComplementaryInformation": "Une erreur est survenue lors de la mise à jour du commentaire."
  }
}
</i18n>

<style lang="scss" scoped>
  .slide-fade-enter-active {
    transition: all 0.3s cubic-bezier(1, 0.5, 0.8, 1);
  }

  .slide-fade-leave-active {
    transition: all 0.3s cubic-bezier(1, 0.5, 0.8, 1);
  }

  .slide-fade-enter-from,
  .slide-fade-leave-to {
    transform: translateY(-20px);
    opacity: 0;
  }
  .add-comment-form {
    background: #fff;
    border: 1px solid #d0d0d0;
    border-left: 3px solid #f5c242;
    border-radius: 4px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    padding: 10px 12px;
    position: absolute;
    top: 100%;
    width: 100%;
    left: 0;
    z-index: 10;

    textarea {
      width: 100%;
      min-height: 60px;
      border: 1px solid #ddd;
      border-radius: 4px;
      padding: 8px;
      font-family: inherit;
      font-size: inherit;
      resize: vertical;
      outline: none;

      &:focus {
        border-color: #f5c242;
      }

      &::placeholder {
        color: #aaa;
      }
    }

    .add-comment-actions {
      display: flex;
      justify-content: flex-end;
      gap: 6px;
      margin-top: 8px;
    }

    .btn-cancel {
      padding: 4px 12px;
      font-size: 11.5px;
      border: 1px solid #ccc;
      border-radius: 4px;
      background: #fff;
      color: #666;
      cursor: pointer;
      transition: background 0.1s ease;

      &:hover {
        background: #f0f0f0;
      }
    }

    .btn-submit {
      padding: 4px 12px;
      font-size: 11.5px;
      border: none;
      border-radius: 4px;
      background: #f5c242;
      color: #333;
      font-weight: 600;
      cursor: pointer;
      transition: background 0.1s ease;

      &:hover {
        background: #e6b230;
      }
    }
  }

  .commentable {
    position: relative;

    &:hover .add-comment-btn {
      opacity: 1;
    }
  }

  .add-comment-btn {
    position: absolute;
    top: 10px;
    right: 0px;
    transform: translateY(-50%);
    width: 20px;
    height: 20px;
    border-radius: 50%;
    background: #e6b230;
    border: none;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    opacity: 0;
    transition:
      opacity 0.15s ease,
      transform 0.15s ease;
    box-shadow: 0 1px 4px rgba(0, 0, 0, 0.15);

    &:hover {
      transform: translateY(-50%) scale(1.1);
    }

    &::before {
      content: '';
      width: 14px;
      height: 14px;
      background-color: #fff;
      -webkit-mask-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='currentColor'%3E%3Cpath d='M20 2H4c-1.1 0-2 .9-2 2v18l4-4h14c1.1 0 2-.9 2-2V4c0-1.1-.9-2-2-2z'/%3E%3C/svg%3E");
      mask-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='currentColor'%3E%3Cpath d='M20 2H4c-1.1 0-2 .9-2 2v18l4-4h14c1.1 0 2-.9 2-2V4c0-1.1-.9-2-2-2z'/%3E%3C/svg%3E");
      mask-size: contain;
      mask-repeat: no-repeat;
      mask-position: center;
    }
  }

  .has-comment {
    background: #fff9c4;
    border-left: 2px solid #f5c242;
    transition: all 0.3s ease;

    &.highlight {
      background: #f7b10059;
      box-shadow: 0 0 0 2px #f5c242;
      margin-left: -2px;
      margin-right: 2px;
      width: calc(100% + 2px);
    }
  }

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
