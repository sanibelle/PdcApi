<script setup lang="ts">
  import MinimalForm from '~/components/modules/courseFramework/MinimalForm.vue';
  const { t } = useI18n();
  const localePath = useLocalePath();
  const route = useRoute();
  const programCode = route.params.programCode as string;

  const courseFrameworkCode = route.params.courseFrameworkCode as string;

  defineI18nRoute({
    paths: {
      fr: `/plansCadres/[programCode]/[courseFrameworkCode]`,
    },
  });

  const { fetchCompetencies } = useCompetencyClient();
  const { fetchCourseFrameworkByCode } = useCourseFrameworkClient();
  const competencies = ref<Competency[]>([]);
  const courseFramework = ref<TrackedCourseFramework>();
  const editMode = ref(false);

  onMounted(async () => {
    try {
      courseFramework.value = await fetchCourseFrameworkByCode(courseFrameworkCode);
      competencies.value = await fetchCompetencies(programCode);
    } catch (e) {
      console.error(e);
      // TODO manage error (e.g., show a notification or redirect)
      alert('ERREUR');
    }
  });

  const isSubmitting = ref(false);

  const handleSubmitted = (c: TrackedCourseFramework) => {
    courseFramework.value = c;
    editMode.value = false;
  };

  const handleFetchTrackedCourseFrameworkByChangeRecordNumber = async (changeRecordNumber: number | null) => {
    if (changeRecordNumber) {
      //TODO courseFramework.value = await fetchTrackedCourseFrameworkByCode(programCode, courseFrameworkCode, changeRecordNumber);
    } else {
      courseFramework.value = await fetchCourseFrameworkByCode(courseFrameworkCode);
    }
  };

  const openEditMode = async () => {
    await handleFetchTrackedCourseFrameworkByChangeRecordNumber(null);
    editMode.value = true;
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
    if (!courseFramework.value) return;

    try {
      isSubmitting.value = true;
      await publishChangeRecord(courseFramework.value.changeRecordId!);
      courseFramework.value!.isDraft = false;
      modal.close();
    } catch (error) {
      console.error('Error publishing changeRecord:', error);
      alert(t('errorWhenPublishingChangeRecord'));
    } finally {
      isSubmitting.value = false;
    }
  };
</script>

<template>
  <div>
    <h1>
      {{ t('title') }}
    </h1>
    <CommonOrganismsAChangeRecord
      v-if="courseFramework?.changeRecordNumber"
      :change-record-number="courseFramework?.changeRecordNumber"
      :is-draft="courseFramework?.isDraft"
      @update:change-number-to-compare="handleFetchTrackedCourseFrameworkByChangeRecordNumber"
    />
    <section v-if="courseFramework">
      <template v-if="editMode">
        <!-- <ModulesAdministrationCourseFrameworkDetailedForm
          :courseFramework="courseFramework"
          :program-code="programCode"
          @submitted="handleSubmitted"
        /> -->
      </template>
      <template v-else>
        <ModulesCourseFrameworkDetailed :courseFramework="courseFramework" />
        <div class="flex">
          <CommonAtomsAButton
            data-testid="edit-button"
            @click="openEditMode()"
          >
            {{ courseFramework.isDraft ? t('editDraft') : t('createANewDraft') }}
          </CommonAtomsAButton>
          <CommonAtomsAButton
            v-if="courseFramework.isDraft"
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
    "title": "Plan de cours",
    "publishChangeRecord": "Publier cette version",
    "minorEditButton": "Effectuer une correction mineure",
    "approveChangeRecordText": "En publiant cette version, il ne sera plus possible d'ajouter ou de supprimer d'éléments, seule la modification sera possible.",
    "modalTitle": "Publier cette version",
    "createANewDraft": "Créer une nouvelle version",
    "editDraft": "Modifier ce brouillon",
    "errorWhenPublishingChangeRecord": "Une erreur est survenue lors de la publication de la version. Veuillez réessayer."
  }
}
</i18n>

<style scoped>
  h1 {
    display: flex;
  }
</style>
