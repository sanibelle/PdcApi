<script setup lang="ts">
  import EditForm from './EditForm.vue';

  const { t } = useI18n();

  const model = defineModel<Changeable<String | Number>>({
    required: true,
  });

  // TODO seuls les admins on accès à la modification mineure

  const { open, close } = useModal();

  const handleShowForm = () => {
    open({
      title: t('title'),
      component: EditForm,
      hideFooter: true,
      componentProps: {
        modelValue: model,
        'onUpdate:modelValue': (val: Changeable<String | Number>) => {
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
