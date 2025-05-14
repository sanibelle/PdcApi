<script setup lang="ts">
import type { ProgramOfStudy } from '~/types/ministerial/ProgramOfStudy';

const { t } = useI18n();
defineI18nRoute({
  paths: {
    fr: '/gestion',
  },
});

const { fetchPrograms } = useProgramOfStudy();
const programsOfSudy = ref<ProgramOfStudy[]>([]);

onMounted(async () => {
  programsOfSudy.value = await fetchPrograms();
});

const handleSubmit = async (programOfStudy: ProgramOfStudy) => {
  try {
    programsOfSudy.value.unshift(programOfStudy);
    upsertProgramOfStudyModal.close()
  } catch (error) {
    console.error('Error fetching programs:', error);
  }
};

const upsertProgramOfStudyModal = useModal();
</script>

<template>
  <h1>
    {{ t('title') }}
  </h1>
  <section>
    <div class="scholar-row" style="display: flex; align-items: center; margin-bottom: 1rem">
      <button @click="upsertProgramOfStudyModal.open()" class="scholar-btn">
        {{ t('createButton') }}
      </button>
    </div>
  </section>
  <div v-if="programsOfSudy.length === 0">{{ t('noProgramsYet') }}</div>
  <div v-for="program in programsOfSudy" :key="program.code">
    <h2>{{ program.code }} - {{ program.name }}</h2>
    <p></p>
  </div>

  <CommonAModal v-model="upsertProgramOfStudyModal.isOpen.value" :title="t('title')" :hide-footer="true">
    <ProgramOfStudyForm @submit="handleSubmit" :program="null" :isEdit="false" />
  </CommonAModal>
</template>

<i18n lang="json">{
  "fr": {
    "title": "Programmes d'études",
    "noProgramsYet": "Aucun programme d'études disponible pour le moment.",
    "createButton": "Créer un programme d'études"
  }
}</i18n>

<style scoped>
.scholar-row {
  display: flex;
  align-items: center;
  margin-bottom: 1rem;
}

.scholar-btn {
  background-color: #4caf50;
  /* Green */
  border: none;
  color: white;
  padding: 10px 20px;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 16px;
  margin: 4px 2px;
  cursor: pointer;
  border-radius: 4px;
}
</style>
