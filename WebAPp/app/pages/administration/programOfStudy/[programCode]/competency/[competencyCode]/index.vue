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

  const { publishVersion } = useVersionClient();
  const handlePublishVersion = async () => {
    if (!competency.value) return;

    try {
      isSubmitting.value = true;
      await publishVersion(competency.value.versionId!);
      competency.value!.isDraft = false;
      showPublishVersionModal.close();
    } catch (error) {
      console.error('Error publishing version:', error);
      console.log('oyooo');
      alert(t('errorWhenPublishingVersion'));
    } finally {
      isSubmitting.value = false;
    }
  };

  const showPublishVersionModal = useModal();
</script>

<template>
  <div class="wrapper">
    <h1>
      {{ t('title') }}
    </h1>
    <CommonOrganismAVersion
      v-if="competency?.versionNumber"
      :version-number="competency?.versionNumber"
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
            data-testid="approve-this-version-button"
            @click="showPublishVersionModal.open()"
          >
            {{ t('publishVersion') }}
          </CommonAtomsAButton>
        </div>
      </template>
    </section>
    <CommonTemplateAModal
      v-model="showPublishVersionModal.isOpen.value"
      :title="t('modalTitle')"
      :disable-submit-button="isSubmitting"
      :close-on-confirm="false"
      @confirm="handlePublishVersion"
    >
      <p>{{ t('approveVersionText') }}</p>
    </CommonTemplateAModal>
  </div>
</template>

<i18n lang="json">
{
  "fr": {
    "publishVersion": "Publier cette version",
    "approveVersionText": "En publiant cette version, il ne sera plus possible d'ajouter ou de supprimer d'éléments, seule la modification sera possible.",
    "modalTitle": "Publier cette version",
    "title": "Compétences ministérielles",
    "editButton": "Modifier la compétence",
    "errorWhenPublishingVersion": "Une erreur est survenue lors de la publication de la version. Veuillez réessayer."
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
