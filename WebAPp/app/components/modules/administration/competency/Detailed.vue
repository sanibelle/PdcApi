<script setup lang="ts">
  const { t } = useI18n();

  defineProps({
    competency: {
      type: Object as PropType<Competency>,
      required: true,
    },
  });
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
          <CommonTemplateAComplementaryInformationable
            v-for="context in competency.realisationContexts"
            :key="context.id"
            v-model="context.complementaryInformations"
            :is-view-only="false"
            :changeable-id="context.id"
            class="criterion"
          >
            {{ context.value }}
          </CommonTemplateAComplementaryInformationable>
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
        <template
          v-for="(competencyElement, index) in competency.competencyElements.toSorted((a, b) => +a.position - +b.position)"
          :key="competencyElement.id"
        >
          <CommonMoleculesARow>
            <template #col-left>
              <CommonTemplateAComplementaryInformationable
                v-model="competencyElement.complementaryInformations"
                :is-view-only="false"
                :changeable-id="competencyElement.id"
              >
                {{ index + 1 }}. {{ competencyElement.value }}
              </CommonTemplateAComplementaryInformationable>
            </template>
            <template #col-right>
              <div
                v-for="performanceCriteria in competencyElement.performanceCriterias"
                :key="performanceCriteria.id"
                class="criterion"
              >
                <CommonTemplateAComplementaryInformationable
                  v-model="performanceCriteria.complementaryInformations"
                  :is-view-only="false"
                  :changeable-id="performanceCriteria.id"
                >
                  {{ performanceCriteria.value }}
                </CommonTemplateAComplementaryInformationable>
              </div>
            </template>
          </CommonMoleculesARow>
        </template>
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

<style lang="scss" scoped>
  .criterion {
    padding: 6px 10px;
    font-size: 13px;
    color: #333;
    position: relative;
    cursor: default;
  }
</style>
