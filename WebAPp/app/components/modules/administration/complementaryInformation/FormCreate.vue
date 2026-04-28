<script setup lang="ts">
  import type { ComplementaryInformation, EditableComplementaryInformation } from '...';

  const { t } = useI18n();
  const props = defineProps<{ changeableId?: string }>();
  const emit = defineEmits<{ added: [item: EditableComplementaryInformation]; cancel: [] }>();

  const comment = ref('');
  const { createComplementaryInformation } = useComplementaryInformationClient();

  const { handleSubmit, isSubmitting } = useForm<ComplementaryInformation>({
    validateOnMount: false,
  });

  const onSubmit = handleSubmit(async () => {
    if (!props.changeableId) {
      alert(t('errorAddingCommentMissingChangeableId'));
      return;
    }
    try {
      const newItem = await createComplementaryInformation(props.changeableId, { text: comment.value });
      emit('added', newItem);
    } catch (e) {
      alert(t('errorWhenAddingComplementaryInformation'));
      console.error(e);
    }
  });
</script>

<template>
  <form
    class="add-comment-form"
    @submit="onSubmit"
  >
    <FormATextAreaInput
      v-model="comment"
      :focus-on-mount="true"
      name="comment"
      :required="true"
      :max="1000"
      :placeholder="t('addComment')"
    />
    <div class="add-comment-actions">
      <FormMoleculesASubmitButton
        :is-submitting="isSubmitting"
        data-testid="submit-button"
      >
        {{ t('submit') }}
      </FormMoleculesASubmitButton>
      <CommonAtomsAButton
        class="cancel"
        @click="emit('cancel')"
      >
        {{ t('cancel') }}
      </CommonAtomsAButton>
    </div>
  </form>
</template>

<style lang="scss" scoped>
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
</style>
