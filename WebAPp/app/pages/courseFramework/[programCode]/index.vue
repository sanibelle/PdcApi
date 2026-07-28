<script setup lang="ts">
  import MinimalForm from '~/components/modules/courseFramework/MinimalForm.vue';
  const { t } = useI18n();
  const localePath = useLocalePath();
  const route = useRoute();
  const programCode = route.params.programCode as string;

  defineI18nRoute({
    paths: {
      fr: `/plansCadres/[programCode]`,
    },
  });

  const { fetchProgramByCode } = useProgramOfStudyClient();
  const { fetchCourseFrameworks } = useCourseFrameworkClient();
  const programOfStudy = ref<ProgramOfStudy>();
  const courseFrameworks = ref<CourseFramework[]>([]);

  onMounted(async () => {
    try {
      programOfStudy.value = await fetchProgramByCode(programCode);
      courseFrameworks.value = await fetchCourseFrameworks(programCode);
    } catch (e) {
      console.error(e);
      // TODO manage error (e.g., show a notification or redirect)
      alert('ERREUR');
    }
  });

  const handleSubmitted = async (courseFramework: CourseFramework) => {
    courseFrameworks.value.unshift(courseFramework);
    modal.close();
  };

  const modal = useModal();

  const handleCreateCourseFrameworkClick = () => {
    modal.open({
      title: t('formTitle'),
      hideFooter: true,
      component: MinimalForm,
      componentProps: {
        programCode,
        onSubmitted: handleSubmitted,
      },
    });
  };
</script>

<template>
  <h1>
    {{ t('title') }}
  </h1>
  <section>
    <div class="flex-center">
      <CommonAtomsAButton
        data-testid="create-course-framework-btn"
        @click="handleCreateCourseFrameworkClick"
      >
        {{ t('createButton') }}
      </CommonAtomsAButton>
    </div>
  </section>
  <div v-if="courseFrameworks.length === 0">{{ t('noCourseFrameworksYet') }}</div>
  <table v-else>
    <thead>
      <tr>
        <th>{{ t('code') }}</th>
        <th>{{ t('name') }}</th>
        <th>{{ t('action') }}</th>
      </tr>
    </thead>
    <tbody>
      <tr
        v-for="courseFramework in courseFrameworks"
        :key="courseFramework.courseCode"
        class="flex-center"
      >
        <td>{{ courseFramework.courseCode }}</td>
        <td>{{ courseFramework.name }}</td>
        <td>
          <NuxtLink :to="localePath({ name: 'courseFramework-courseFrameworkCode', params: { programCode: programCode, courseFrameworkCode: courseFramework.courseCode } })">
            CLIKCMEEEEEEEEEE GOOOOOOOOOOOO
          </NuxtLink>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<i18n lang="json">
{
  "fr": {
    "title": "Gestion des plans cadres",
    "formTitle": "Créer un plan cadre",
    "createButton": "Ajouter un plan cadre",
    "backToList": "Retour à la liste",
    "loading": "Chargement...",
    "code": "Code",
    "name": "Nom",
    "noCourseFrameworksYet": "Aucun plan de cours pour l'instant.",
    "action": "Actions"
  }
}
</i18n>

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
