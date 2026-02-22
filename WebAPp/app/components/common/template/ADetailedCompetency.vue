<script setup lang="ts">
const { t } = useI18n();

const props = defineProps({
  competency: {
    type: Object as PropType<Competency>,
    required: true,
  }
});
</script>

<template>
  <div class="top-header">
    <span class="left">{{ competency.isMandatory ? t('mandatoryCompetency') : t('optionalCompetency') }} </span>
    <div class="code">{{ t('code') }} : {{ competency.code }}</div>
  </div>

  <div class="obj-std-row">
    <span class="obj">{{ t('objective') }}</span>
    <span class="std">{{ t('standard') }}</span>
  </div>

  <div class="panel">
    <div class="panel-header">
      <div class="col col-left">{{ t('statementOfCompetency') }}</div>
      <div class="col col-right">{{ t('realisationContext') }}</div>
    </div>
    <div class="panel-row">
      <div class="col col-left">{{ competency.statementOfCompetency }}</div>
      <div class="col col-right">
        <div v-for="context in competency.realisationContexts" :key="context.id" class="criterion">
          <CommonTemplateAComplementaryInformations
          :complementary-informations="context.complementaryInformations">
          {{ context.value }}
        </CommonTemplateAComplementaryInformations>
        </div>
      </div>
    </div>
  </div>
  <div class="panel main">
    <div class="panel-header">
      <div class="col col-left">{{ t('competencyElements') }}</div>
      <div class="col col-right">{{ t('performanceCriteria') }}</div>
    </div>

    <template v-for="(competencyElement, index) in competency.competencyElements.sort((a, b) => +a.position - +b.position)" :key="competencyElement.id">
      <div class="panel-row">
        <div class="col col-left">
          <CommonTemplateAComplementaryInformations
          :complementary-informations="competencyElement.complementaryInformations">
          {{ index + 1 }}. {{ competencyElement.value }}
        </CommonTemplateAComplementaryInformations>
      </div>
        <div class="col col-right">
          <div v-for="performanceCriteria in competencyElement.performanceCriterias" :key="performanceCriteria.id"
            class="criterion">
            <CommonTemplateAComplementaryInformations
              :complementary-informations="performanceCriteria.complementaryInformations">
              {{ performanceCriteria.value }}
            </CommonTemplateAComplementaryInformations>
          </div>
        </div>
      </div>
    </template>
  </div>
</template>

<i18n lang="json">{
  "fr": {
    "optionalCompetency": "Compétence optionnelle",
    "mandatoryCompetency": "Compétence obligatoire",
    "code": "Code",
    "objective": "Objectif",
    "standard": "Standard",
    "competencyElements": "Éléments de compétence",
    "performanceCriteria": "Critères de performance",
    "statementOfCompetency": "Énoncé de la compétence",
    "realisationContext": "Contexte de réalisation"
  }
}</i18n>

<style lang="scss" scoped>
.top-header {
  display: flex;
  justify-content: space-between;
  align-items: baseline;
  background: #d6e6f5;
  border: 1px solid #a0c0de;
  padding: 4px 14px;
  font-weight: bold;

  .left {
    color: #3a7abf;
  }

  .code {
    font-size: 0.9em;
    font-weight: bold;
    color: #333;
  }
}

.obj-std-row {
  display: flex;
  justify-content: space-between;
  padding: 4px 14px;
  font-weight: bold;
  color: #3a7abf;
  font-style: italic;
}

.panel {
  border: 1px solid #bbb;
  overflow: visible;
}

.panel-header {
  display: flex;
  background: linear-gradient(180deg, #6aaed6 0%, #3a7abf 100%);
  color: #fff;
  font-weight: bold;

  .col {
    padding: 0.5em 0.5em;
  }

  .col-left {
    width: 45%;
  }

  .col-right {
    width: 55%;
  }
}

.panel-row {
  display: flex;
  border-top: 1px solid #bbb;

  .col {
    padding: 8px 12px;
  }

  .col-left {
    width: 45%;
    font-weight: bold;
    font-size: 13px;
  }

  .col-right {
    width: 55%;
    font-size: 13px;
    display: flex;
    flex-direction: column;
    gap: 6px;
  }
}

.criterion {
  padding: 6px 10px;
  font-size: 13px;
  color: #333;
  position: relative;
  cursor: default;
}
</style>