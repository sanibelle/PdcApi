<script setup lang="ts">
  const { t } = useI18n();

  const props = defineProps({
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
          <CommonTemplateAComplementaryInformations
            v-model="context.complementaryInformations"
            v-for="context in competency.realisationContexts"
            :key="context.id"
            class="criterion"
          >
            {{ context.value }}
          </CommonTemplateAComplementaryInformations>
        </template>
      </CommonMoleculesARow>
    </template>
  </CommonTemplateAPanel>
  <!-- Competency elements -->
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
            <CommonTemplateAComplementaryInformations v-model="competencyElement.complementaryInformations">{{ index + 1 }}. {{ competencyElement.value }}</CommonTemplateAComplementaryInformations>
          </template>
          <template #col-right>
            <div
              v-for="performanceCriteria in competencyElement.performanceCriterias"
              :key="performanceCriteria.id"
              class="criterion"
            >
              <CommonTemplateAComplementaryInformations v-model="performanceCriteria.complementaryInformations">
                {{ performanceCriteria.value }}
              </CommonTemplateAComplementaryInformations>
            </div>
          </template>
        </CommonMoleculesARow>
      </template>
    </template>
  </CommonTemplateAPanel>
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
