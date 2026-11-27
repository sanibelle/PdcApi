<script setup lang="ts">
  const { t } = useI18n();

  const props = defineProps({
    courseFramework: {
      type: Object as PropType<TrackedCourseFramework>,
      required: true,
    },
  });

  const changeDetails = computed(() => props.courseFramework.changeDetails || []);
  const isDraft = computed(() => props.courseFramework.isDraft);
</script>

<template>
  <ModulesCourseFrameworkComponentsTheHeader
    :isDraft="isDraft"
    :courseFramework="courseFramework"
  />
</template>

<i18n lang="json">
{
  "fr": {
    "competencyElements": "Éléments de compétence",
    "performanceCriteria": "Critères de performance",
    "statementOfCompetency": "Énoncé de la compétence",
    "realisationContext": "Contexte de réalisation"
  }
}
</i18n>
