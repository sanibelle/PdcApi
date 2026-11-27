<script setup lang="ts">
  const { t } = useI18n();
  defineProps({
    isDraft: {
      type: Boolean,
      required: true,
    },
    courseFramework: {
      type: Object as PropType<TrackedCourseFramework>,
      required: true,
    },
  });
</script>

<template>
  <CommonOrganismsATopRow>
    <template #left>
      <div class="left-col">
        <div class="name-code">
          <ModulesAdministrationComplementaryInformationWrapper
            v-model="courseFramework.name.complementaryInformations"
            :is-view-only="false"
            :changeable-id="courseFramework.name.id"
          >
            <template
              v-if="!isDraft"
              #action-btn
            >
              <ModulesChangeableEditButton v-model="courseFramework.name" />
            </template>
            <ModulesChangeableDisplay
              :change-details="courseFramework.changeDetails"
              :changeable="courseFramework.name"
            />
          </ModulesAdministrationComplementaryInformationWrapper>
          <span>-</span>
          <ModulesAdministrationComplementaryInformationWrapper
            v-model="courseFramework.code.complementaryInformations"
            :is-view-only="false"
            :changeable-id="courseFramework.code.id"
          >
            <template
              v-if="!isDraft"
              #action-btn
            >
              <ModulesChangeableEditButton v-model="courseFramework.code" />
            </template>
            <ModulesChangeableDisplay
              :change-details="courseFramework.changeDetails"
              :changeable="courseFramework.code"
            />
          </ModulesAdministrationComplementaryInformationWrapper>
        </div>
        <div class="flex-row">
          <CommonAtomsAText>{{ t('createdOn') }} :</CommonAtomsAText>
          <CommonMoleculesADate :date="courseFramework.createdOn"></CommonMoleculesADate>
        </div>
      </div>
    </template>
    <template #right>
      <div class="flex-row">
        <CommonAtomsAText>{{ t('weighting') }} :</CommonAtomsAText>
        <CommonOrganismsAWeighting
          :is-draft="courseFramework.isDraft"
          :change-details="courseFramework.changeDetails"
          :weighting="courseFramework.weighting"
        />
      </div>
      <div class="flex-row">
        <CommonAtomsAText>{{ t('units') }} :</CommonAtomsAText>
        <CommonOrganismsAUnit :weighting="courseFramework.weighting" />
      </div>
      <div class="flex-row">
        <CommonAtomsAText>{{ t('semester') }} :</CommonAtomsAText>
        <CommonAtomsAText>
          <ModulesAdministrationComplementaryInformationWrapper
            v-model="courseFramework.semester.complementaryInformations"
            :is-view-only="false"
            :changeable-id="courseFramework.semester.id"
          >
            <template
              v-if="!isDraft"
              #action-btn
            >
              <ModulesChangeableEditButton v-model="courseFramework.semester" />
            </template>
            <ModulesChangeableDisplay
              :change-details="courseFramework.changeDetails"
              :changeable="courseFramework.semester"
            />
          </ModulesAdministrationComplementaryInformationWrapper>
        </CommonAtomsAText>
      </div>
    </template>
  </CommonOrganismsATopRow>
</template>

<i18n lang="json">
{
  "fr": {
    "weighting": "Pondération",
    "mandatoryCompetency": "Compétence obligatoire",
    "code": "Code",
    "semester": "Session",
    "createdOn": "Créé le",
    "units": "Unités"
  }
}
</i18n>
<style scoped lang="scss">
  .left-col {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 5rem;
  }

  .name-code {
    display: flex;
    align-items: center;
    gap: 0.5rem;
    font-size: 1.5rem;
  }

  .flex-row {
    display: flex;
    align-items: center;
    gap: 0.5rem;
  }
</style>
