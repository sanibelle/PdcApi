<script setup lang="ts">
  const { t } = useI18n();

  const props = defineProps({
    weighting: {
      type: Object as PropType<TrackedWeighting>,
      required: true,
    },
  });

  const unit = computed(() => {
    const totalHours = props.weighting.theoryHours.value + props.weighting.laboratoryHours.value;
    if (totalHours === 0) return 0;
    return (totalHours / 3).toFixed(2);
  });
</script>

<template>
  <CommonAtomsAText>{{ unit }}</CommonAtomsAText>
</template>

<i18n>
{
    "fr": {
    }
}
</i18n>

<style scoped>
  .weighting {
    display: flex;
    gap: 1rem;
  }
</style>
