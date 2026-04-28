<script setup lang="ts">
  const { t } = useI18n();

  const props = defineProps({
    competency: {
      type: Object as PropType<Competency>,
      required: true,
    },
  });

  const changeDetails = computed(() => props.competency.changeDetails || []);
  const isDraft = computed(() => props.competency.isDraft);
</script>

<template>
  <ModulesAdministrationCompetencyComponentsTheCompetencyHeader :competency="competency" />
  <CommonTemplateAPanel>
    <template #header-col-left>
      {{ t('statementOfCompetency') }}
    </template>
    <template #header-col-right>
      {{ t('realisationContext') }}
    </template>
    <template #content>
      <CommonMoleculesARow>
        <template #col-left>
          {{ competency.statementOfCompetency }}
        </template>
        <template #col-right>
          <ModulesAdministrationComplementaryInformationWrapper
            v-for="(context, index) in competency.realisationContexts"
            :key="context.id"
            v-model="context.complementaryInformations"
            :is-view-only="false"
            :changeable-id="context.id"
          >
            <template
              v-if="!isDraft"
              #action-btn
            >
              <ModulesChangeableEditButton v-model="competency.realisationContexts[index]!" />
            </template>
            <ModulesChangeableDisplay
              :change-details="changeDetails"
              :changeable="context"
            />
          </ModulesAdministrationComplementaryInformationWrapper>
        </template>
      </CommonMoleculesARow>
    </template>
  </CommonTemplateAPanel>
  <!-- Competency elements -->
  <div class="competencies">
    <CommonTemplateAPanel>
      <template #header-col-left>
        {{ t('competencyElements') }}
      </template>
      <template #header-col-right>
        {{ t('performanceCriteria') }}
      </template>
      <template #content>
        <CommonMoleculesARow
          v-for="(competencyElement, ceIndex) in competency.competencyElements"
          :key="competencyElement.id"
        >
          <template #col-left>
            <ModulesAdministrationComplementaryInformationWrapper
              v-model="competencyElement.complementaryInformations"
              :is-view-only="false"
              :changeable-id="competencyElement.id"
            >
              <template
                v-if="!isDraft"
                #action-btn
              >
                <ModulesChangeableEditButton v-model="competency.competencyElements[ceIndex]!" />
              </template>
              <ModulesChangeableDisplay
                :change-details="changeDetails"
                :changeable="competencyElement"
              />
            </ModulesAdministrationComplementaryInformationWrapper>
          </template>
          <template #col-right>
            <div
              v-for="(performanceCriteria, pcIndex) in competencyElement.performanceCriterias"
              :key="performanceCriteria.id"
              class="criterion"
            >
              <ModulesAdministrationComplementaryInformationWrapper
                v-model="performanceCriteria.complementaryInformations"
                :is-view-only="false"
                :changeable-id="performanceCriteria.id"
              >
                <template
                  v-if="!isDraft"
                  #action-btn
                >
                  <ModulesChangeableEditButton v-model="competency.competencyElements[ceIndex]!.performanceCriterias[pcIndex]!" />
                </template>
                <ModulesChangeableDisplay
                  :change-details="changeDetails"
                  :changeable="performanceCriteria"
                />
              </ModulesAdministrationComplementaryInformationWrapper>
            </div>
          </template>
        </CommonMoleculesARow>
      </template>
    </CommonTemplateAPanel>
  </div>
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
