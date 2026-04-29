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

  const { fetchTrackedCompetencyByCode, fetchCompetencyByCode } = useCompetencyClient();
  const competency = ref<Competency>();
  const editMode = ref(false);

  onMounted(async () => {
    competency.value = await fetchCompetencyByCode(programCode, competencyCode);
  });

  const handleFetchTrackedCompetencyByChangeRecordNumber = async (changeRecordNumber: number | null) => {
    if (changeRecordNumber) {
      competency.value = await fetchTrackedCompetencyByCode(programCode, competencyCode, changeRecordNumber);
    } else {
      competency.value = await fetchCompetencyByCode(programCode, competencyCode);
    }
  };

  const isSubmitting = ref(false);

  const handleSubmitted = (c: Competency) => {
    competency.value = c;
    editMode.value = false;
  };

  const handleMinorEditClick = () => {
    alert("Fonctionnalité à implémenter : permettre la modification mineure d'une version approuvée (ex : corriger les fautes de frappe).");
  };

  const handleCreateNewDraftClick = () => {
    alert('TODO');
  };

  const modal = useModal();
  const handleOpenPublishModal = () => {
    modal.open({
      title: t('modalTitle'),
      content: t('approveChangeRecordText'),
      confirmLabel: t('publishChangeRecord'),
      onConfirm: handlePublishChangeRecord,
    });
  };

  const { publishChangeRecord } = useChangeRecordClient();
  const handlePublishChangeRecord = async () => {
    if (!competency.value) return;

    try {
      isSubmitting.value = true;
      await publishChangeRecord(competency.value.changeRecordId!);
      competency.value!.isDraft = false;
      modal.close();
    } catch (error) {
      console.error('Error publishing changeRecord:', error);
      console.log('oyooo');
      alert(t('errorWhenPublishingChangeRecord'));
    } finally {
      isSubmitting.value = false;
    }
  };
</script>

<template>
  <div class="wrapper">
    <h1>
      {{ t('title') }}
    </h1>
    <CommonOrganismAChangeRecord
      v-if="competency?.changeRecordNumber"
      :change-record-number="competency?.changeRecordNumber"
      :is-draft="competency?.isDraft"
      @update:change-number-to-compare="handleFetchTrackedCompetencyByChangeRecordNumber"
    />
    <section v-if="competency">
      <template v-if="editMode">
        <ModulesAdministrationCompetencyDetailedForm
          :competency="competency"
          :program-code="programCode"
          @submitted="handleSubmitted"
        />
      </template>
      <template v-else>
        <ModulesAdministrationCompetencyDetailed :competency="competency" />
        <div class="flex">
          <CommonAtomsAButton
            data-testid="edit-button"
            @click="editMode = true"
          >
            {{ competency.isDraft ? t('editDraft') : t('createANewDraft') }}
          </CommonAtomsAButton>
          <CommonAtomsAButton
            v-if="competency.isDraft"
            :is-submitting="isSubmitting"
            data-testid="approve-this-change-record-button"
            @click="handleOpenPublishModal()"
          >
            {{ t('publishChangeRecord') }}
          </CommonAtomsAButton>
        </div>
      </template>
    </section>
  </div>
</template>

<i18n lang="json">
{
  "fr": {
    "publishChangeRecord": "Publier cette version",
    "minorEditButton": "Effectuer une correction mineure",
    "approveChangeRecordText": "En publiant cette version, il ne sera plus possible d'ajouter ou de supprimer d'éléments, seule la modification sera possible.",
    "modalTitle": "Publier cette version",
    "createANewDraft": "Créer une nouvelle version",
    "title": "Compétences ministérielles",
    "editDraft": "Modifier ce brouillon",
    "errorWhenPublishingChangeRecord": "Une erreur est survenue lors de la publication de la version. Veuillez réessayer."
  }
}
</i18n>

<style scoped>
  .wrapper {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
    max-width: 900px;
  }
</style>
