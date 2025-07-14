<script setup lang="ts">
const { t } = useI18n();
const route = useRoute();
const programCode = route.params.code as string;

defineI18nRoute({
  paths: {
    fr: `/administration/programme/[code]`,
  },
});

const { fetchProgramByCode } = useProgramOfStudyClient();
const { fetchCompetencies } = useCompetencyClient();
const programsOfStudy = ref<ProgramOfStudy>();
const competencies = ref<Competency[]>([]);

onMounted(async () => {
  programsOfStudy.value = await fetchProgramByCode(programCode);
  competencies.value = await fetchCompetencies(programCode);
});

const handleSubmitted = (programOfStudy: ProgramOfStudy) => {
  upsertCompetencyModal.close()
};

const upsertCompetencyModal = useModal();
</script>

<template>
<h1>
    {{ t('title') }}
  </h1>
  <section>
    <div class="flex-center">
      <CommonAtomsAButton @click="upsertCompetencyModal.open()" id="create-program-btn">{{ t('createButton') }}</CommonAtomsAButton>
    </div>
  </section>
  <div v-if="competencies.length === 0">{{ t('noCompetenciesYet') }}</div>
  <table v-else>
    <thead>
      <tr>
        <th>{{ t('code') }}</th>
        <th>{{ t('statementOfCompetency') }}</th>
        <th>{{ t('action') }}</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="competency in competencies" :key="competency.code" class="flex-center">
        <td>{{ competency.code }}</td>
        <td>{{ competency.statementOfCompetency }}</td>
        <td>    <NuxtLink :to="$localePath(`/administration/programOfStudy/${competency.code}/competency/${competency.code}`)">CLIKCMEEEEEEEEEE GOOOOOOOOOOOO</NuxtLink></td>
      </tr>
    </tbody>
  </table>
  <CommonAModal v-model="upsertCompetencyModal.isOpen.value" :title="t('title')" :hide-footer="true">
    <FormTemplatesCompetency @submited="handleSubmitted" :program="null" :isEdit="false" :program-code="programCode" />
  </CommonAModal>
</template>

<i18n lang="json">{
  "fr": {
    "title": "Gestion des compétences ministérielles",
    "createButton": "Ajouter une compétence",
    "backToList": "Retour à la liste",
    "loading": "Chargement...",
    "programNotFound": "Programme non trouvé",
    "code": "Code",
    "statementOfCompetency": "Énoncé de compétence",
    "monthsDuration": "Durée en mois",
    "publishedOn": "Publié le",
    "programType.DEC": "Technique",
    "programType.AEC": "Attestation d'études collégiales", 
    "programType.PREU": "Préuniversitaire",
    "noCompetenciesYet": "Aucune compétence ajoutées pour ce programme."
  }
}</i18n>

<style scoped>
.back-link {
  color: #3b82f6;
  text-decoration: none;
  font-weight: 500;
}

.back-link:hover {
  text-decoration: underline;
}

.loading, .error {
  padding: 1rem;
  text-align: center;
  font-size: 1.1rem;
}

.error {
  color: #ef4444;
}

.program-details {
  background: #f8fafc;
  padding: 1.5rem;
  border-radius: 8px;
  margin-top: 1rem;
}

.detail-item {
  margin-bottom: 0.75rem;
  font-size: 1rem;
}

.detail-item strong {
  color: #374151;
  margin-right: 0.5rem;
}
</style>
