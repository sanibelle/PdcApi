<script setup lang="ts">
  import TheForm from '~/components/modules/administration/programOfStudy/Form.vue';
  const { t } = useI18n();
  defineI18nRoute({
    paths: {
      fr: `/plansCadres`,
    },
  });

  const localePath = useLocalePath();

  const { fetchPrograms } = useProgramOfStudyClient();
  const programsOfStudy = ref<ProgramOfStudy[]>([]);

  onMounted(async () => {
    programsOfStudy.value = await fetchPrograms();
  });
</script>

<template>
  <h1>
    {{ t('title') }}
  </h1>
  <section></section>
  <div v-if="programsOfStudy.length === 0">{{ t('noProgramsYet') }}</div>
  <table>
    <thead>
      <tr>
        <th>{{ t('code') }}</th>
        <th>{{ t('name') }}</th>
        <th>{{ t('action') }}</th>
      </tr>
    </thead>
    <tbody>
      <tr
        v-for="program in programsOfStudy"
        :key="program.code"
        class="flex-center"
      >
        <td>{{ program.code }}</td>
        <td>{{ program.name }}</td>
        <td>
          <NuxtLink :to="localePath({ name: 'courseFramework-programCode', params: { programCode: program.code } })">CLIKCMEEEEEEEEEE GOOOOOOOOOOOO</NuxtLink>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<i18n lang="json">
{
  "fr": {
    "title": "Plans cadres",
    "noProgramsYet": "Aucun plan cadre disponible pour le moment. La création d'un plan cadre est réservée aux administrateurs.",
    "code": "Code",
    "name": "Nom",
    "action": "Actions"
  }
}
</i18n>

<style scoped>
  .flex-center {
    display: flex;
    align-items: center;
    margin-bottom: 1rem;
  }
</style>
