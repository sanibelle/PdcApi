<script setup lang="ts">
import '~/assets/css/form.css'
import { useForm } from 'vee-validate';

const { t } = useI18n();

const emit = defineEmits(['submited']);
const codeExistingErrorMessage = ref('');

// Form values
const competency = reactive<Partial<Competency>>({
  isMandatory: false,
  code: '',
  statementOfCompetency: ''
});

// Form values
const { handleSubmit, isSubmitting } = useForm<Competency>({
  validateOnMount: false,
});

const props = defineProps({
  programCode: {
    type: String,
    default: () => ({}),
  },
});

const { createCompetency } = useCompetencyClient();
const onSubmit = handleSubmit(async () => {
  try {
    emit('submited', await createCompetency(props.programCode, competency as Competency));
  } catch (e) {
    if (e instanceof DuplicateException) {
      codeExistingErrorMessage.value = t('codeExistingErrorMessage');
    }
    else {
      console.error('Error creating competency:', e);
    }
  }
});
</script>

<template>
  <div class="form">
    {{ competency }}
    <form @submit="onSubmit" class="form-container">
      <FormATextInput name="code" :label="t('competencyCode')" placeholder="Ex : 00SU" :min="3" :max="50"
        :required="true" v-model="competency.code" :errorMessage="codeExistingErrorMessage" />
      <FormATextInput name="statementOfCompetency" :label="t('statementOfCompetency')" :placeholder="t('statementOfCompetencyPlaceholder')" 
        :required="true" v-model="competency.statementOfCompetency" />
      <FormACheckboxInput name="isMandatory" :label="competency.isMandatory ? t('mandatoryCompetency') : t('optionnalCompetency')" :hint="t('mandatoryCompetencyHint')" 
        :required="true" v-model="competency.isMandatory" />
      <div class="modal-footer">
        <FormMoleculesASubmitButton :isSubmitting="isSubmitting" id="submit-competency" />
      </div>
    </form>
  </div>
</template>

<i18n>
{
  "fr": {
    "title": "Créer une compétence",
    "competencyCode": "Code de compétence",
    "statementOfCompetency": "Énoncé de compétence",
    "statementOfCompetencyPlaceholder": "Ex : Effectuer le développement d'applications Web transactionnelles",
    "competencyDescriptionPlaceholder": "Description détaillée de la compétence",
    "codeExistingErrorMessage": "Le code de compétence existe déjà.",
    "optionnalCompetency": "Compétence optionnelle",
    "mandatoryCompetency": "Compétence obligatoire",
    "mandatoryCompetencyHint": "Lorsque coché, la compétence est obligatoire pour le programme, sinon elle est optionnelle.",
  }
}
</i18n>

<style scoped>
.form {
  padding: 0.5rem;
}
</style>
