<script setup lang="ts">
  const { t } = useI18n();
  defineProps({
    competency: {
      type: Object as PropType<Competency>,
      required: true,
    },
  });
</script>

<template>
  <CommonOrganismATopRow>
    <template #left>
      {{ competency.isMandatory ? t('mandatoryCompetency') : t('optionalCompetency') }}
    </template>
    <template #right>{{ t('code') }} : {{ competency.code }}</template>
    <template #content>
      <span class="obj">{{ t('objective') }}</span>
      <span class="std">{{ t('standard') }}</span>
    </template>
  </CommonOrganismATopRow>
</template>

<i18n lang="json">
{
  "fr": {
    "optionalCompetency": "Compétence optionnelle",
    "mandatoryCompetency": "Compétence obligatoire",
    "code": "Code",
    "objective": "Objectif",
    "standard": "Standard"
  }
}
</i18n>
