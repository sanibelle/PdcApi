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
</script>

<template>
  <h1>
    {{ t('title') }}
  </h1>
  <section>
    <div class="flex-center">
      {{ courseFramework?.name }}
    </div>
  </section>
</template>

<i18n lang="json">
{
  "fr": {
    "title": "Plan de cours"
  }
}
</i18n>

<style scoped></style>
