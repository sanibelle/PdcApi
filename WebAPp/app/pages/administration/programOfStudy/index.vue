<script setup lang="ts">

const { t } = useI18n();
defineI18nRoute({
  paths: {
    fr: `/administration/programmes`,
  },
});

const localePath = useLocalePath();

const { fetchPrograms } = useProgramOfStudyClient();
const programsOfStudy = ref<ProgramOfStudy[]>([]);

onMounted(async () => {
  programsOfStudy.value = await fetchPrograms();
});

const handleSubmitted = (programOfStudy: ProgramOfStudy) => {
  programsOfStudy.value.unshift(programOfStudy);
  upsertProgramOfStudyModal.close()
};

const upsertProgramOfStudyModal = useModal();
</script>

<template>
  <h1>
    {{ t('title') }}
  </h1>
  <section>
    <div class="flex-center">
      <CommonAtomsAButton @click="upsertProgramOfStudyModal.open()" id="create-program-btn">{{ t('createButton') }}</CommonAtomsAButton>
    </div>
  </section>
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
      <tr v-for="program in programsOfStudy" :key="program.code" class="flex-center">
        <td>{{ program.code }}</td>
        <td>{{ program.name }}</td>
        <td>          
          <NuxtLink :to="localePath({ name: 'administration-programOfStudy-programCode', params: { programCode: program.code } })">
            CLIKCMEEEEEEEEEE GOOOOOOOOOOOO
          </NuxtLink>
        </td>
      </tr>
    </tbody>
  </table>

  <CommonAModal v-model="upsertProgramOfStudyModal.isOpen.value" :title="t('title')" :hide-footer="true">
    <FormTemplatesProgramOfStudy @submitted="handleSubmitted" :program="null" :isEdit="false" />
  </CommonAModal>
</template>

<i18n lang="json">{
  "fr": {
    "title": "Programmes d'études",
    "noProgramsYet": "Aucun programme d'études disponible pour le moment.",
    "createButton": "Créer un programme d'études",
    "code": "Code",
    "name": "Nom",
    "action": "Actions"
  }
}</i18n>

<style scoped>
.flex-center {
  display: flex;
  align-items: center;
  margin-bottom: 1rem;
}
</style>
