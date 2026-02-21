<script setup lang="ts">
const { t } = useI18n();
const route = useRoute();
const programCode = route.params.programCode as string;
const competencyCode = route.params.competencyCode as string;

defineI18nRoute({
  paths: {
    fr: `/administration/programme/[programCode]/competence/[competencyCode]`,
  },
});

const { fetchCompetencyByCode } = useCompetencyClient();
const competency = ref<Competency>();
const editMode = ref(false);

onMounted(async () => {
  competency.value = await fetchCompetencyByCode(programCode, competencyCode);
});

// TODO la modale est toujours utile??
const handleSubmitted = (c: Competency) => {
  competency.value = c;
  editMode.value = false;
};

const upsertCompetencyModal = useModal();
</script>

<template>
  <div class="wrapper">
    <h1>
      {{ t('title') }}
    </h1>
    <CommonAVersion v-if="competency?.versionNumber" :version-number="competency?.versionNumber"
      :is-draft="competency?.isDraft" />
    <template v-if="editMode">
      <section v-if="competency">
        <FormTemplatesDetailedCompetency @submitted="handleSubmitted" :competency="competency"
          :program-code="programCode" />
      </section>
    </template>
    <template v-else-if="competency">
      <CommonTemplateADetailedCompetency :competency="competency" />
      <CommonAtomsAButton @click="editMode = true">
        {{ t('editButton') }}
      </CommonAtomsAButton>
    </template>
  </div>
</template>

<i18n lang="json">{
  "fr": {
    "title": "Compétences ministérielles",
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
    "noCompetenciesYet": "Aucune compétence ajoutée pour ce programme.",
    "editButton": "Modifier la compétence"
  }
}</i18n>

<style scoped>
.wrapper {
  display: flex;
  flex-direction: column;
  gap: .5rem;
  max-width: 900px;
}
</style>
