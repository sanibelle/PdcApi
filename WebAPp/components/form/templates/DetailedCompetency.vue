<script setup lang="ts">
import '~/assets/css/form.css'
import { useForm } from 'vee-validate';

const { t } = useI18n();

const emit = defineEmits(['submitted']);
const codeExistingErrorMessage = ref('');

const { handleSubmit, isSubmitting } = useForm<Competency>({
  validateOnMount: false,
});

const props = defineProps({
  programCode: {
    type: String,
    default: '',
  },
  competency: {
    type: Object as () => Partial<Competency>,
      default: () => ({
        isMandatory: false,
        code: '',
        statementOfCompetency: '',
      realisationContexts: [],
    }),
  },
});

const competency = computed({
  get: () => props.competency,
  set: (value) => {
    emit('submitted', value)
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
  competency.value.realisationContexts?.push({
    value: '',
    position: competency.value.realisationContexts.length + 1,
  });
};

const removeRealisationContextRow = (index: number) => {
  competency.value.realisationContexts?.splice(index, 1);
  // Update positions after removal
  competency.value.realisationContexts?.forEach((context, idx) => {
    context.position = idx +1;
  });
};
</script>

<template>
  <div class="form">
    <form @submit="onSubmit" class="form-container">
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
        <div class="row" v-for="(realisationContext, index) in competency.realisationContexts" :key="index">
          <ul>
            <li>
              <FormATextInput :name="`competency.realisationContexts[${index}].value`" :min="3" :max="100"
                :required="true" v-model="realisationContext.value" />
              <CommonAtomsAButton @click.prevent="removeRealisationContextRow(index)" :preventDefault="true">-
              </CommonAtomsAButton>
            </li>
          </ul>
        </div>
        <CommonAtomsAButton @click.prevent="addRealisationContextRow" :preventDefault="true">+</CommonAtomsAButton>
      </div>
      <div class="row-container flex-row">
        <h2>{{ t('competencyElement') }}</h2>
        <h2>{{ t('performanceCriteria') }}</h2>
      </div>
      rendu ici
      <!-- <div>
        <div class="row-container flex-row">
          <div class="row" v-for="(realisationContext, index) in competency.realisationContexts" :key="index">
            <ul>
              <li>
                <FormATextInput :name="`competency.realisationContexts[${index}].text`" :min="3" :max="100"
                :required="true" v-model="realisationContext.text" />
                <FormANumberInput :name="`competency.realisationContexts[${index}].position`" hidden="true" v-model="realisationContext.position" />
                <CommonAtomsAButton @click.prevent="removeRealisationContextRow(index)" :preventDefault="true">-</CommonAtomsAButton>
                {{ realisationContext }}
              </li>
            </ul>
          </div>
          <CommonAtomsAButton @click.prevent="addRealisationContextRow" :preventDefault="true">+</CommonAtomsAButton>
        </div>
      </div> -->
      <div class="buttons">
        <FormMoleculesASubmitButton :isSubmitting="isSubmitting">
          {{ t('applyModification') }}
        </FormMoleculesASubmitButton>
        <FormMoleculesASubmitButton :isSubmitting="isSubmitting">
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
    "approveThisVersion": "Approuver cette version (il ne sera plus possible d'ajouter ou de modifier d'éléments sur cette version, seul la modification sera possible)"
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
