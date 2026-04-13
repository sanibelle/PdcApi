<script setup lang="ts">
  import AEditForm from './AEditForm.vue';

  const { t } = useI18n();

  const model = defineModel<Changeable>({
    required: true,
  });

  const { open, close } = useModal();

  const handleShowForm = () => {
    open({
      title: t('title'),
      component: AEditForm,
      hideFooter: true,
      componentProps: {
        modelValue: model,
        'onUpdate:modelValue': (val: Changeable) => {
          model.value = val;
        },
        onClose: close,
      },
    });
  };
</script>

<template>
  <CommonMoleculesAEditButton @click="handleShowForm" />
</template>

<i18n lang="json">
{
  "fr": {
    "title": "Modification mineure"
  }
}
</i18n>
