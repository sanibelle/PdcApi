<script setup>
const { t } = useI18n();
defineI18nRoute({
  paths: {
    fr: '/gestion',
  },
});
const { fetchPrograms } = useProgramOfStudy();
const programsOfSudy = ref([]);
onMounted(async () => {
  programsOfSudy.value = await fetchPrograms();
});
</script>

<template>
  <h1>
    {{ t('title') }}
  </h1>
  <div v-if="programsOfSudy.length === 0">{{ t('noProgramsYet') }}</div>
  <div v-for="program in programsOfSudy" :key="program.id">
    <h2>{{ program.name }}</h2>
    <p>{{ program.description }}</p>
  </div>
</template>

<i18n lang="json">
{
  "fr": {
    "title": "Programmes d'études",
    "noProgramsYet": "Aucun programme d'études disponible pour le moment."
  }
}
</i18n>
