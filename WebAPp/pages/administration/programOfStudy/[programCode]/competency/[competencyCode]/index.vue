<script setup lang="ts">
const { t } = useI18n();
const route = useRoute();
const programCode = route.params.programCode as string;
const competencyCode = route.params.competencyCode as string;

defineI18nRoute({
  paths: {
    fr: `/administration/programme/[programCode]/competency/[competencyCode]`,
  },
});

const { fetchCompetencyByCode } = useCompetencyClient();
const competency = ref<Competency>();

onMounted(async () => {
  competency.value = await fetchCompetencyByCode(programCode, competencyCode);
});

// TODO la modale est toujours utile??
const handleSubmitted = (competency: Competency) => {
  upsertCompetencyModal.close()
};

const upsertCompetencyModal = useModal();
</script>

<template>
  <h1>
    {{ t('title') }}
  </h1>
  <CommonAVersion v-if="competency?.versionNumber" :version-number="competency?.versionNumber"
    :is-draft="competency?.isDraft" />
  <section v-if="competency">
    <FormTemplatesDetailedCompetency @submitted="handleSubmitted" :competency="competency"
      :program-code="programCode" />
  </section>

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
    "noCompetenciesYet": "Aucune compétence ajoutée pour ce programme."
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

.loading,
.error {
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
