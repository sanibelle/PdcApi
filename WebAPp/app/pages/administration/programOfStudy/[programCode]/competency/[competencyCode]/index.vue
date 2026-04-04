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

  const isSubmitting = ref(false);

  const handleSubmitted = (c: Competency) => {
    competency.value = c;
    editMode.value = false;
  };

  const { publishChangeRecord } = useChangeRecordClient();
  const handlePublishChangeRecord = async () => {
    if (!competency.value) return;

    try {
      isSubmitting.value = true;
      await publishChangeRecord(competency.value.changeRecordId!);
      competency.value!.isDraft = false;
      showPublishChangeRecordModal.close();
    } catch (error) {
      console.error('Error publishing changeRecord:', error);
      console.log('oyooo');
      alert(t('errorWhenPublishingChangeRecord'));
    } finally {
      isSubmitting.value = false;
    }
  };

  const showPublishChangeRecordModal = useModal();
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
            {{ t('editButton') }}
          </CommonAtomsAButton>
          <CommonAtomsAButton
            :is-submitting="isSubmitting"
            data-testid="approve-this-change-record-button"
            @click="showPublishChangeRecordModal.open()"
          >
            {{ t('publishChangeRecord') }}
          </CommonAtomsAButton>
        </div>
      </template>
    </section>
    <CommonTemplateAModal
      v-model="showPublishChangeRecordModal.isOpen.value"
      :title="t('modalTitle')"
      :disable-submit-button="isSubmitting"
      :close-on-confirm="false"
      @confirm="handlePublishChangeRecord"
    >
      <p>{{ t('approveChangeRecordText') }}</p>
    </CommonTemplateAModal>
  </div>
</template>

<i18n lang="json">
{
  "fr": {
    "publishChangeRecord": "Publier cette version",
    "approveChangeRecordText": "En publiant cette version, il ne sera plus possible d'ajouter ou de supprimer d'éléments, seule la modification sera possible.",
    "modalTitle": "Publier cette version",
    "title": "Compétences ministérielles",
    "editButton": "Modifier la compétence",
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
