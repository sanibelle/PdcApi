<script setup lang="ts">
import '~/assets/css/form.css'
import { useForm } from 'vee-validate';

const { t } = useI18n();

const emit = defineEmits(['submited']);
const codeExistingErrorMessage = ref('');

// Form values
const programOfStudy = reactive<Partial<ProgramOfStudy>>({
  specificUnits: {},
  optionalUnits: {},
  complementaryUnits: {
    wholeUnit: 6,
  },
  generalUnits: {
    denominator: 3,
    numerator: 2,
    wholeUnit: 16,
  },
});
const { handleSubmit, isSubmitting } = useForm<ProgramOfStudy>({
  validateOnMount: false,
});

const { createProgram } = useProgramOfStudy();
const onSubmit = handleSubmit(async () => {

  try {
    emit('submited', await createProgram(programOfStudy as ProgramOfStudy));
  } catch (e) {
    if (e instanceof DuplicateException) {
      codeExistingErrorMessage.value = t('codeExistingErrorMessage');
    }
    else { // todo handle error... dans api service?
      console.error('Error fetching programs:', e);
    }
  }
});

const options: SelectOption[] = Object.entries(ProgramType)
  .filter(([key, value]) => typeof value === 'number')
  .map(([key, value]) => ({
    value,
    label: t(`programType.${key}`),
  }));
</script>

<template>
  <div class="user-form">
    <form @submit="onSubmit" class="form-container">
      <FormATextInput name="name" :label="t('programName')" :placeholder="t('programNamePlaceholder')" :min="2"
        :max="50" v-model="programOfStudy.name" :required="true" />
      <FormATextInput name="code" :label="t('programCode')" placeholder="Ex : 300.A1" :min="3" :max="50"
        :required="true" v-model="programOfStudy.code" :errorMessage="codeExistingErrorMessage" />
      <FormASelectInput name="programType" :placeholder="t('programTypePlaceholder')" :label="t('programType')"
        :required="true" :options="options" v-model="programOfStudy.programType" />
      <FormANumberInput name="monthsDuration" :label="t('monthsDuration')" :min="0"
        v-model="programOfStudy.monthsDuration" :required="true" />
      <FormANumberInput name="specificDurationHours" :label="t('specificDurationHours')" :min="0"
        v-model="programOfStudy.specificDurationHours" :hint="t('specificDurationHoursHint')" :required="true" />
      <FormANumberInput name="totalDurationHours" :label="t('totalDurationHours')" :min="0"
        v-model="programOfStudy.totalDurationHours" :hint="t('totalDurationHoursHint')" :required="true" />
      <FormADateInput name="publishedOn" :label="t('publishedOn')" :hint="t('publishedOnHint')"
        v-model="programOfStudy.publishedOn" :required="true" :max="new Date()" />
      <FormAUnitInput name="specificUnits" :label="t('specificUnits')" v-model="programOfStudy.specificUnits"
        :required="true" :hint="t('specificUnitsHint')" />
      <FormAUnitInput name="optionalUnits" :label="t('optionalUnits')" v-model="programOfStudy.optionalUnits"
        :required="true" :hint="t('optionalUnitsHint')" />
      <FormAUnitInput name="generalUnits" :label="t('generalUnits')" v-model="programOfStudy.generalUnits"
        :required="true" />
      <FormAUnitInput name="complementaryUnits" :label="t('complementaryUnits')"
        v-model="programOfStudy.complementaryUnits" />
      <div class="modal-footer">
        <FormMoleculesASubmitButton :isSubmitting="isSubmitting" id="submit-program" />
      </div>

    </form>
  </div>
</template>

<i18n>
{
  "fr": {
    "title": "Programme d'étude",
    "programName": "Titre du programme",
    "programCode": "Code du programme",
    "programType": "Type de programme",
    "programTypePlaceholder":"Sélectionner un type de programme",
    "programNamePlaceholder":"Ex : Sciences humaines",
    "monthsDuration": "Durée en mois",
    "totalDurationHours": "Durée totale en heures",
    "specificDurationHours": "Durée spécifique en heures",
    "specificDurationHoursHint": "Nombre d'heures spécifiques au programme",
    "totalDurationHoursHint": "Nombre d'heures spécifiques au programme et à la formation générale",
    "publishedOn": "Publié le",
    "publishedOnHint": "Date de publication du programme",
    "specificUnits": "Unités obligatoires spécifiques au programme",
    "optionalUnits": "Unités optionnelles spécifiques au programme",
    "generalUnits": "Unités des cours généraux (ex: philosophie, français)",
    "complementaryUnits": "Unités des complémentaires",
    "specificUnitsHint": "Indiquer le nombre d'unités maximales",
    "optionalUnitsHint": "Indiquer le nombre d'unités maximales",
    "codeExistingErrorMessage": "Le code de programme existe déjà.",
    "programType.DEC": "Technique",
    "programType.AEC": "Attestation d'études collégiales",
    "programType.PREU": "Préuniversitaire",
  }
}
</i18n>

<style scoped>
.user-form {
  padding: 0.5rem;
}

.form-title {
  margin-bottom: 24px;
  font-size: 24px;
  font-weight: 600;
  text-align: center;
}

.form-container {
  display: flex;
  flex-direction: column;
}

.form-actions {
  display: flex;
  justify-content: space-between;
  margin-top: 24px;
}
</style>
