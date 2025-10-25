<script setup lang="ts">
import '~/assets/css/form.css'
import { useForm } from 'vee-validate';

const { t } = useI18n();

const emit = defineEmits(['submitted']);
const codeExistingErrorMessage = ref('');

const { handleSubmit, isSubmitting } = useForm<Competency>({
  validateOnMount: false,
});

const competency = defineModel<Competency>('competency', {
  required: true,
})

const props = defineProps({
  programCode: {
    type: String,
    default: '',
  }
});

const isOptionnal = computed({
  get: () => !competency.value.isMandatory,
  set: (value) => {
    competency.value.isMandatory = !value;
  }
});

const { updateCompetency } = useCompetencyClient();

const onSubmit = handleSubmit(async () => {
  try {
    emit('submitted', await updateCompetency(props.programCode, competency.value as Competency));
  } catch (e) {
    if (e instanceof DuplicateException) {
      codeExistingErrorMessage.value = t('codeExistingErrorMessage');
    }
    else {
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
    performanceCriterias: []
  });
  addPerformanceCriteriaRow(competency.value.competencyElements.length - 1);
};

const removeCompetencyElementRow = (index: number) => {
  competency.value.competencyElements?.splice(index, 1);
  // Update positions after removal
  competency.value.competencyElements?.forEach((context, idx) => {
    context.position = idx + 1;
  });
};

const addPerformanceCriteriaRow = (competencyElementIndex: number) => {
  if (!competency.value.competencyElements[competencyElementIndex]?.performanceCriterias) {
    competency.value.competencyElements[competencyElementIndex]!.performanceCriterias = [];
  }
  competency.value.competencyElements[competencyElementIndex]!.performanceCriterias.push({
    value: '',
    position: competency.value.competencyElements[competencyElementIndex]!.performanceCriterias.length + 1,
    complementaryInformations: [],
  });
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
    <form @submit="onSubmit" class="form-container" v-if="competency">
      <div class="top-row">
        <FormATextInput name="code" :label="t('competencyCode')" placeholder="Ex : 00SU" :min="3" :max="50"
          :required="true" v-model="competency.code" :errorMessage="codeExistingErrorMessage" />
        <FormATextInput name="statementOfCompetency" :label="t('statementOfCompetency')"
          :placeholder="t('statementOfCompetencyPlaceholder')" :required="true"
          v-model="competency.statementOfCompetency" />
        <FormACheckboxInput name="isMandatory" :label="t('mandatoryCompetency')" :required="true"
          v-model="competency.isMandatory" />
        <FormACheckboxInput name="isOptionnal" :label="t('optionnalCompetency')" :required="true"
          v-model="isOptionnal" />
      </div>
      <div class="row-container">
        <h2>{{ t('realisationContext') }}</h2>
        <div class="row" v-for="(_, index) in competency.realisationContexts" :key="index">
          <FormCommonAComplementaryInformations :index="index"
            v-model="competency.realisationContexts[index]!.complementaryInformations!"
            :name="`competency.realisationContexts[${index}]`">
            <FormMinisterialARealisationContext :index="index" v-model="competency.realisationContexts[index]!"
              @deleteRow="removeRealisationContextRow" />
          </FormCommonAComplementaryInformations>
        </div>
        <CommonAtomsAButton @click.prevent="addRealisationContextRow" :preventDefault="true"
          :aria-label="t('addRealisationContext')" data-testid="add-realisation-context">+</CommonAtomsAButton>
      </div>
      <div class="row-container flex-row">
        <h2>{{ t('competencyElement') }}</h2>
        <div class="row" v-for="(_, competencyElementIndex) in competency.competencyElements"
          :key="competencyElementIndex">
          <FormCommonAComplementaryInformations :index="competencyElementIndex"
            v-model="competency.competencyElements[competencyElementIndex]!.complementaryInformations!"
            :name="`competency.competencyElements[${competencyElementIndex}]`">
            <FormMinisterialACompetencyElement :index="competencyElementIndex"
              v-model="competency.competencyElements[competencyElementIndex]!"
              @deleteRow="removeCompetencyElementRow" />
          </FormCommonAComplementaryInformations>
          <div>
            <h2>{{ t('performanceCriteria') }}</h2>
            <div class="row"
              v-for="(_, performanceCriteriaIndex) in competency.competencyElements[competencyElementIndex]!.performanceCriterias"
              :key="performanceCriteriaIndex">
              <FormCommonAComplementaryInformations :index="performanceCriteriaIndex"
                v-model="competency.competencyElements[competencyElementIndex]!.performanceCriterias[performanceCriteriaIndex]!.complementaryInformations!"
                :name="`competency.competencyElements[${competencyElementIndex}].performanceCriterias[${performanceCriteriaIndex}]`">
                <FormMinisterialAPerformanceCriteria :competencyElementIndex="competencyElementIndex"
                  :performanceCriteriaIndex="performanceCriteriaIndex"
                  v-model="competency.competencyElements[competencyElementIndex]!.performanceCriterias[performanceCriteriaIndex]!"
                  @deleteRow="removePerformanceCriteriaRow(competencyElementIndex, performanceCriteriaIndex)" />
              </FormCommonAComplementaryInformations>
            </div>
            <CommonAtomsAButton @click.prevent="addPerformanceCriteriaRow(competencyElementIndex)"
              :preventDefault="true" :aria-label="t('addPerformanceCriteria')"
              data-testid="`add-performance-criteria-${competencyElementIndex}`">+
            </CommonAtomsAButton>
          </div>
        </div>
        <CommonAtomsAButton @click.prevent="addCompetencyElementRow" :preventDefault="true"
          :aria-label="t('addCompetencyElement')" data-testid="add-competency-element">
          +</CommonAtomsAButton>
      </div>
      <div class="buttons">
        <FormMoleculesASubmitButton :isSubmitting="isSubmitting" data-testid="submit-draft-button">
          {{ t('applyModification') }}
        </FormMoleculesASubmitButton>
        <FormMoleculesASubmitButton :isSubmitting="isSubmitting" data-testid="approve-this-version-button">
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
    "addPerformanceCriteria": "Ajouter un critère de performance"
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

.top-row>* {
  flex: 1;
}

.row {
  display: flex;
  gap: 1rem;
}

.buttons {
  display: flex;
  gap: 1rem;
  justify-content: flex-end;
  margin-top: 1.5rem;
}
</style>
