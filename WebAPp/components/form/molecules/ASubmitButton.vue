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
    <CommonAtomsAButton 
      :is-disabled="isDisabled || isSubmitting" 
      v-bind="$attrs"
    >
      <template v-if="isSubmitting">
        {{ t('submiting') }}
      </template>
      <template v-else>
        {{ t('submit') }}
      </template>
    </CommonAtomsAButton>
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
  background-color: hsl(128, 56%, 29%);
  &:hover {
    background-color: hsl(128, 56%, 39%);
  }
  min-width: 7rem;
}
</style>
