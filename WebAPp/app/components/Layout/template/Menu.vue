<script setup lang="ts">
  const { t } = useI18n();
  const authStore = useAuthStore();
  const programs = ref<MenuItem[]>([]);
  const courseFrameworks = ref<MenuItem[]>([]);
  const { fetchPrograms } = useProgramOfStudyClient();
  const { fetchCourseFrameworks } = useCourseFrameworkClient();
  onMounted(async () => {
    await authStore.authenticate();
    const result = await fetchPrograms();
    programs.value = result.map((program) => ({ id: program.code, name: program.name, shortName: program.code }));
  });

  const { setProgramCode, setCourseFrameworkCode } = useNavigationStore();
  const { programCode, courseFrameworkCode } = storeToRefs(useNavigationStore());
  const handleProgramClick = async (program: MenuItem) => {
    setProgramCode(program.id);
    const courseFrameworkResult = await fetchCourseFrameworks(program.id);
    courseFrameworks.value = courseFrameworkResult.map((framework) => ({ id: framework.code, name: framework.name, shortName: framework.code }));
  };
</script>
<template>
  <div class="wrapper">
    <LayoutOrganismsSection
      :slug="t('programOfStudy')"
      :items="programs"
      :selected-item="programCode"
      @on-select-item-click="handleProgramClick"
      @on-remove-item-click="setProgramCode(null)"
    >
      <LayoutOrganismsSection
        v-if="programCode"
        :slug="t('courseFramework')"
        :items="courseFrameworks"
        :selected-item="courseFrameworkCode"
        @on-select-item-click="setCourseFrameworkCode"
        @on-remove-item-click="setCourseFrameworkCode(null)"
      ></LayoutOrganismsSection>
    </LayoutOrganismsSection>
  </div>
</template>

<style scoped lang="scss">
  .wrapper {
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
    align-items: center;
    justify-content: start;
    width: 100%;
  }
</style>

<i18n lang="json">
{
  "fr": {
    "programOfStudy": "Programmes d'études",
    "courseFramework": "Plans cadres",
    "favorites": "Favoris"
  }
}
</i18n>
