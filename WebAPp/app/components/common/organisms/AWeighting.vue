<script setup lang="ts">
  const { t } = useI18n();

  const props = defineProps({
    isDraft: {
      type: Boolean,
      required: true,
    },
    weighting: {
      type: Object as PropType<TrackedWeighting>,
      required: true,
    },
    changeDetails: {
      type: Array as PropType<ChangeDetail[]>,
      required: true,
    },
  });
</script>

<template>
  <div class="weighting">
    <ModulesAdministrationComplementaryInformationWrapper
      v-model="weighting.theoryHours.complementaryInformations"
      :is-view-only="false"
      :changeable-id="weighting.theoryHours.id"
    >
      <template
        v-if="!isDraft"
        #action-btn
      >
        <ModulesChangeableEditButton v-model="weighting.theoryHours" />
      </template>
      <ModulesChangeableDisplay
        :change-details="changeDetails"
        :changeable="weighting.theoryHours"
      />
    </ModulesAdministrationComplementaryInformationWrapper>
    <span>-</span>
    <ModulesAdministrationComplementaryInformationWrapper
      v-model="weighting.laboratoryHours.complementaryInformations"
      :is-view-only="false"
      :changeable-id="weighting.laboratoryHours.id"
    >
      <template
        v-if="!isDraft"
        #action-btn
      >
        <ModulesChangeableEditButton v-model="weighting.laboratoryHours" />
      </template>
      <ModulesChangeableDisplay
        :change-details="changeDetails"
        :changeable="weighting.laboratoryHours"
      />
    </ModulesAdministrationComplementaryInformationWrapper>
    <span>-</span>
    <ModulesAdministrationComplementaryInformationWrapper
      v-model="weighting.personnalWorkHours.complementaryInformations"
      :is-view-only="false"
      :changeable-id="weighting.personnalWorkHours.id"
    >
      <template
        v-if="!isDraft"
        #action-btn
      >
        <ModulesChangeableEditButton v-model="weighting.personnalWorkHours" />
      </template>
      <ModulesChangeableDisplay
        :change-details="changeDetails"
        :changeable="weighting.personnalWorkHours"
      />
    </ModulesAdministrationComplementaryInformationWrapper>
  </div>
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
