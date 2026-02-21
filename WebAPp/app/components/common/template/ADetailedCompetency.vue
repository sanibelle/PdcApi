<script setup lang="ts">
const { t } = useI18n();

const props = defineProps({
  competency: {
    type: Object as PropType<Competency>,
    required: true,
  }
});

const activeComment = ref<string | null>(null);

function toggleComment(id: string) {
  activeComment.value = activeComment.value === id ? null : id;
}

function closeComment() {
  activeComment.value = null;
}
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

    <template v-for="(competencyElement, index) in competency.competencyElements.sort(x => +x.position)"
      :key="competencyElement.id">
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

/* Criterion items */
.criterion {
  padding: 6px 10px;
  font-size: 13px;
  color: #333;
  position: relative;
  cursor: default;

  &.has-comment {
    background: #fff9c4;
    border-left: 2px solid #f5c242;
    cursor: pointer;
    transition: background 0.15s ease;

    &:hover {
      background: #fff3a0;
    }
  }
}

/* Small yellow triangle indicator */
.comment-indicator {
  position: absolute;
  top: 0;
  right: 0;
  width: 0;
  height: 0;
  border-top: 10px solid #f5c242;
  border-left: 10px solid transparent;
}

/* Word-style comment bubble */
.comment-bubble {
  position: absolute;
  right: -220px;
  top: 0;
  width: 200px;
  background: #fff;
  border: 1px solid #d0d0d0;
  border-left: 3px solid #f5c242;
  padding: 8px 10px;
  font-size: 11.5px;
  color: #555;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.12);
  line-height: 1.4;
  z-index: 10;
  cursor: default;

  &::before {
    content: '';
    position: absolute;
    left: -10px;
    top: 10px;
    width: 0;
    height: 0;
    border-top: 6px solid transparent;
    border-bottom: 6px solid transparent;
    border-right: 8px solid #d0d0d0;
  }

  &::after {
    content: '';
    position: absolute;
    left: -7px;
    top: 10px;
    width: 0;
    height: 0;
    border-top: 6px solid transparent;
    border-bottom: 6px solid transparent;
    border-right: 8px solid #fff;
  }
}

.comment-author {
  font-weight: bold;
  color: #333;
  margin-bottom: 2px;
}

.comment-date {
  font-size: 10px;
  color: #999;
  margin-bottom: 4px;
}

.comment-text {
  color: #555;
}

.comment-close {
  position: absolute;
  top: 4px;
  right: 6px;
  background: none;
  border: none;
  font-size: 14px;
  color: #999;
  cursor: pointer;
  line-height: 1;

  &:hover {
    color: #333;
  }
}

/* Transition */
.comment-fade-enter-active,
.comment-fade-leave-active {
  transition: opacity 0.15s ease, transform 0.15s ease;
}

.comment-fade-enter-from,
.comment-fade-leave-to {
  opacity: 0;
  transform: translateX(-4px);
}
</style>