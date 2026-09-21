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
  const { fetchCompetencies } = useCompetencyClient();
  const { fetchCourseFrameworks } = useCourseFrameworkClient();
  const competencies = ref<Competency[]>([]);
  const programOfStudy = ref<ProgramOfStudy>();
  const courseFrameworks = ref<CourseFramework[]>([]);

  onMounted(async () => {
    try {
      programOfStudy.value = await fetchProgramByCode(programCode);
      competencies.value = await fetchCompetencies(programCode);
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
        competencies: competencies.value,
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
        :key="courseFramework.code"
        class="flex-center"
      >
        <td>{{ courseFramework.code }}</td>
        <td>{{ courseFramework.name }}</td>
        <td>
          <NuxtLink :to="localePath({ name: 'courseFramework-courseCode', params: { programCode: programCode, courseCode: courseFramework.courseCode } })">CLIKCMEEEEEEEEEE GOOOOOOOOOOOO</NuxtLink>
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

<style scoped></style>
