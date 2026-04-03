<script setup lang="ts">
  import '~/assets/css/form.css';
  import { useForm } from 'vee-validate';

  const { t } = useI18n();

  const emit = defineEmits(['submitted']);
  const codeExistingErrorMessage = ref('');
  const addElementPlaceholderRef = ref('');
  const competencyElementIndexToFocus = ref(-1);
  const realisationContextIndexToFocus = ref(-1);
  const performanceCriteriaIndexToFocus = ref(-1);

  const { handleSubmit, isSubmitting } = useForm<Competency>({
    validateOnMount: false,
  });

  const competency = defineModel<Competency>('competency', {
    required: true,
  });

  const props = defineProps({
    programCode: {
      type: String,
      default: '',
    },
  });

  const isOptionnal = computed({
    get: () => !competency.value.isMandatory,
    set: (value) => {
      competency.value.isMandatory = !value;
    },
  });

  const { updateCompetency } = useCompetencyClient();

  const onSubmit = handleSubmit(async () => {
    codeExistingErrorMessage.value = '';
    try {
      emit('submitted', await updateCompetency(props.programCode, competency.value as Competency));
    } catch (e) {
      if (e instanceof DuplicateException) {
        codeExistingErrorMessage.value = t('codeExistingErrorMessage');
      } else {
        console.error('Error creating competency:', e);
      }
    }
  });

  const addRealisationContextRow = () => {
    if (!competency.value.realisationContexts) {
      competency.value.realisationContexts = [];
    }
    competency.value.realisationContexts.push({
      value: '',
      position: competency.value.realisationContexts.length + 1,
      complementaryInformations: [],
    });
    realisationContextIndexToFocus.value = competency.value.realisationContexts.length - 1;
  };

  const removeRealisationContextRow = (index: number) => {
    competency.value.realisationContexts?.splice(index, 1);
    // Update positions after removal
    competency.value.realisationContexts?.forEach((context, idx) => {
      context.position = idx + 1;
    });
  };

  const addCompetencyElementRow = () => {
    if (!competency.value.competencyElements) {
      competency.value.competencyElements = [];
    }
    competency.value.competencyElements.push({
      value: '',
      position: competency.value.competencyElements.length + 1,
      complementaryInformations: [],
      performanceCriterias: [],
    });
    competencyElementIndexToFocus.value = competency.value.competencyElements.length - 1;
    performanceCriteriaIndexToFocus.value = -1;
  };

  const removeCompetencyElementRow = (index: number) => {
    competency.value.competencyElements?.splice(index, 1);
    // Update positions after removal
    competency.value.competencyElements?.forEach((context, idx) => {
      context.position = idx + 1;
    });
  };

  const addPerformanceCriteriaRow = (competencyElementIndex: number) => {
    competencyElementIndexToFocus.value = -1;
    if (!competency.value.competencyElements[competencyElementIndex]?.performanceCriterias) {
      competency.value.competencyElements[competencyElementIndex]!.performanceCriterias = [];
    }
    competency.value.competencyElements[competencyElementIndex]!.performanceCriterias.push({
      value: '',
      position: competency.value.competencyElements[competencyElementIndex]!.performanceCriterias.length + 1,
      complementaryInformations: [],
    });
    performanceCriteriaIndexToFocus.value = competency.value.competencyElements[competencyElementIndex]!.performanceCriterias.length - 1;
  };

  const removePerformanceCriteriaRow = (competencyElementIndex: number, performanceCriteriaIndex: number) => {
    competency.value.competencyElements[competencyElementIndex]!.performanceCriterias?.splice(performanceCriteriaIndex, 1);
    // Update positions after removal
    competency.value.competencyElements[competencyElementIndex]!.performanceCriterias?.forEach((context, idx) => {
      context.position = idx + 1;
    });
  };
</script>

<template>
  <div class="form">
    <form
      v-if="competency"
      class="form-container"
      @submit="onSubmit"
    >
      <CommonOrganismATopRow>
        <template #left>
          <div class="flex">
            <FormACheckboxInput
              v-model="competency.isMandatory"
              name="isMandatory"
              :label="t('mandatoryCompetency')"
              :required="true"
            />
            <FormACheckboxInput
              v-model="isOptionnal"
              name="isOptionnal"
              :label="t('optionnalCompetency')"
              :required="true"
            />
          </div>
        </template>
        <template #right>
          <FormATextInput
            v-model="competency.code"
            name="code"
            :label="t('competencyCode')"
            placeholder="Ex : 00SU"
            :min="3"
            :max="50"
            :required="true"
            :error-message="codeExistingErrorMessage"
          />
        </template>
        <template #content>
          <span class="obj">{{ t('objective') }}</span>
          <span class="std">{{ t('standard') }}</span>
        </template>
      </CommonOrganismATopRow>
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
              <FormATextInput
                v-model="competency.statementOfCompetency"
                name="statementOfCompetency"
                :placeholder="t('statementOfCompetencyPlaceholder')"
                :required="true"
              />
            </template>
            <template #col-right>
              <CommonTemplateAComplementaryInformationable
                v-for="(realisationContext, index) in competency.realisationContexts"
                :key="index"
                v-model="realisationContext.complementaryInformations!"
                :index="index"
                :name="`competency.realisationContexts[${index}]`"
              >
                <ModulesAdministrationCompetencyComponentsARealisationContext
                  v-model="realisationContext!"
                  :index="index"
                  :index-to-focus="realisationContextIndexToFocus"
                  @delete-row="removeRealisationContextRow"
                />
              </CommonTemplateAComplementaryInformationable>
              <div
                data-testid="add-realisation-context"
                @click="addRealisationContextRow"
              >
                <FormATextInput
                  v-model="addElementPlaceholderRef"
                  :name="`add-realisation-context-input`"
                />
              </div>
            </template>
          </CommonMoleculesARow>
        </template>
      </CommonTemplateAPanel>
      <br />
      <!-- Competency elements -->
      <CommonTemplateAPanel>
        <template #header-col-left>
          {{ t('competencyElement') }}
        </template>
        <template #header-col-right>
          {{ t('performanceCriteria') }}
        </template>
        <template #content>
          <CommonMoleculesARow
            v-for="(competencyElement, competencyElementIndex) in competency.competencyElements"
            :key="competencyElementIndex"
            class="row"
          >
            <template #col-left>
              <CommonTemplateAComplementaryInformationable
                v-model="competencyElement.complementaryInformations!"
                :index="competencyElementIndex"
                :name="`competency.competencyElements[${competencyElementIndex}]`"
              >
                <ModulesAdministrationCompetencyComponentsACompetencyElement
                  v-model="competencyElement!"
                  :index="competencyElementIndex"
                  :index-to-focus="competencyElementIndexToFocus"
                  @delete-row="removeCompetencyElementRow"
                />
              </CommonTemplateAComplementaryInformationable>
            </template>
            <template #col-right>
              <CommonTemplateAComplementaryInformationable
                v-for="(performanceCriteria, performanceCriteriaIndex) in competencyElement.performanceCriterias"
                :key="performanceCriteriaIndex"
                v-model="performanceCriteria.complementaryInformations!"
                :index="performanceCriteriaIndex"
                :name="`competency.competencyElements[${competencyElementIndex}].performanceCriterias[${performanceCriteriaIndex}]`"
              >
                <ModulesAdministrationCompetencyComponentsAPerformanceCriteria
                  v-model="performanceCriteria!"
                  :competency-element-index="competencyElementIndex"
                  :index-to-focus="performanceCriteriaIndexToFocus"
                  :performance-criteria-index="performanceCriteriaIndex"
                  @delete-row="removePerformanceCriteriaRow(competencyElementIndex, performanceCriteriaIndex)"
                />
              </CommonTemplateAComplementaryInformationable>
              <div
                :data-testid="`add-performance-criteria-${competencyElementIndex}`"
                @click.prevent="addPerformanceCriteriaRow(competencyElementIndex)"
              >
                <FormATextInput
                  v-model="addElementPlaceholderRef"
                  :name="`add-performance-criteria-input`"
                />
              </div>
            </template>
          </CommonMoleculesARow>
          <!-- add new row for competency element -->
          <CommonMoleculesARow>
            <template #col-left>
              <div
                :data-testid="`add-competency-element`"
                @click.prevent="addCompetencyElementRow"
              >
                <FormATextInput
                  v-model="addElementPlaceholderRef"
                  :name="`add-competency-element-input`"
                />
              </div>
            </template>
          </CommonMoleculesARow>
        </template>
      </CommonTemplateAPanel>
      <div class="buttons">
        <FormMoleculesASubmitButton
          :is-submitting="isSubmitting"
          data-testid="submit-draft-button"
        >
          {{ t('applyModification') }}
        </FormMoleculesASubmitButton>
        <FormMoleculesASubmitButton
          :is-submitting="isSubmitting"
          data-testid="approve-this-version-button"
        >
          {{ t('approveThisVersion') }}
        </FormMoleculesASubmitButton>
      </div>
    </form>
  </div>
</template>

<i18n>
{
  "fr": {
    "title": "Créer une compétence",
    "competencyCode": "Code de compétence",
    "competencyElement": "Élément de compétence",
    "performanceCriteria": "Critères de performance",
    "realisationContext": "Contexte de réalisation",
    "statementOfCompetency": "Énoncé de compétence",
    "statementOfCompetencyPlaceholder": "Ex : Effectuer le développement d'applications Web transactionnelles",
    "competencyDescriptionPlaceholder": "Description détaillée de la compétence",
    "codeExistingErrorMessage": "Le code de compétence existe déjà.",
    "optionnalCompetency": "Compétence optionnelle",
    "mandatoryCompetency": "Compétence obligatoire",
    "applyModification": "Appliquer les modifications",
    "approveThisVersion": "Approuver cette version (il ne sera plus possible d'ajouter ou de modifier d'éléments sur cette version, seul la modification sera possible)",
    "addRealisationContext": "Ajouter un contexte de réalisation",
    "addCompetencyElement": "Ajouter un élément de compétence",
    "addPerformanceCriteria": "Ajouter un critère de performance",
    "objective": "Objectif",
    "standard": "Standard"
  }
}
</i18n>

<style scoped>
  .form {
    padding: 0.5rem;
  }

  .top-row {
    display: flex;
    gap: 1rem;
    background-color: #10b981;
    /* emerald-500 */
    padding: 1rem;
    border-radius: 8px;
    margin-bottom: 1.5rem;
  }

  .buttons {
    display: flex;
    gap: 1rem;
    justify-content: flex-end;
    margin-top: 1.5rem;
  }
</style>
