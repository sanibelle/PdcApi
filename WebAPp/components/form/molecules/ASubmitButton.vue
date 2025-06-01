<script setup lang="ts">
defineProps({
  isDisabled: {
    type: Boolean,
    default: false,
  },
  isSubmitting: {
    type: Boolean,
    default: false,
  },
});

const { t } = useI18n();
</script>

<template>
  <div class="wrapper">
    <FormAtomsAButton :is-disabled="isDisabled || isSubmitting">
      <template v-if="isSubmitting">
        {{ t('submiting') }}
      </template>
      <template v-else>
        {{ t('submit') }}
      </template>
    </FormAtomsAButton>
  </div>
</template>

<i18n>
{
    "fr": {
    "submit": "Soumettre",
    "submiting": "...",
    }
}
</i18n>

<style scoped>
.wrapper {
  height: 30px;
}

:deep(button) {
  min-width: 7rem;
}
</style>
