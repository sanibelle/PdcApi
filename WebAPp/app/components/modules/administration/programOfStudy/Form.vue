<script setup lang="ts">
  import '~/assets/css/form.css';
  import { useForm } from 'vee-validate';

  const { t } = useI18n();

  const emit = defineEmits(['submitted']);
  const codeExistingErrorMessage = ref('');

  // Form values
  const programOfStudy = reactive<Partial<ProgramOfStudy>>({
    specificUnits: {
      denominator: null,
      numerator: null,
      wholeUnit: null,
    },
    optionalUnits: {
      denominator: null,
      numerator: null,
      wholeUnit: null,
    },
    complementaryUnits: {
      wholeUnit: 6,
      numerator: null,
      denominator: null,
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

  const { createProgram } = useProgramOfStudyClient();
  const onSubmit = handleSubmit(async () => {
    try {
      emit('submitted', await createProgram(programOfStudy as ProgramOfStudy));
    } catch (e) {
      if (e instanceof DuplicateException) {
        codeExistingErrorMessage.value = t('codeExistingErrorMessage');
      } else {
        // todo handle error... dans api service?
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
  <div class="form">
    <form
      class="form-container"
      @submit="onSubmit"
    >
      <FormATextInput
        v-model="programOfStudy.name"
        name="name"
        :label="t('programName')"
        :placeholder="t('programNamePlaceholder')"
        :min="2"
        :max="50"
        :required="true"
      />
      <FormATextInput
        v-model="programOfStudy.code"
        name="code"
        :trimmed="true"
        :label="t('programCode')"
        placeholder="Ex : 300.A1"
        :min="3"
        :max="50"
        :required="true"
        :error-message="codeExistingErrorMessage"
      />
      <FormASelectInput
        v-model="programOfStudy.programType"
        name="programType"
        :placeholder="t('programTypePlaceholder')"
        :label="t('programType')"
        :required="true"
        :options="options"
      />
      <FormANumberInput
        v-model="programOfStudy.monthsDuration"
        name="monthsDuration"
        :label="t('monthsDuration')"
        :min="0"
        :required="true"
      />
      <FormANumberInput
        v-model="programOfStudy.specificDurationHours"
        name="specificDurationHours"
        :label="t('specificDurationHours')"
        :min="0"
        :hint="t('specificDurationHoursHint')"
        :required="true"
      />
      <FormANumberInput
        v-model="programOfStudy.totalDurationHours"
        name="totalDurationHours"
        :label="t('totalDurationHours')"
        :min="0"
        :hint="t('totalDurationHoursHint')"
        :required="true"
      />
      <FormADateInput
        v-model="programOfStudy.publishedOn"
        name="publishedOn"
        :label="t('publishedOn')"
        :hint="t('publishedOnHint')"
        :required="true"
        :max="new Date()"
      />
      <FormAUnitInput
        v-model="programOfStudy.specificUnits"
        name="specificUnits"
        :label="t('specificUnits')"
        :required="true"
        :hint="t('specificUnitsHint')"
      />
      <FormAUnitInput
        v-model="programOfStudy.optionalUnits"
        name="optionalUnits"
        :label="t('optionalUnits')"
        :required="true"
        :hint="t('optionalUnitsHint')"
      />
      <FormAUnitInput
        v-model="programOfStudy.generalUnits"
        name="generalUnits"
        :label="t('generalUnits')"
      />
      <FormAUnitInput
        v-model="programOfStudy.complementaryUnits"
        name="complementaryUnits"
        :label="t('complementaryUnits')"
      />
      <div class="modal-footer">
        <FormMoleculesASubmitButton
          id="submit-program"
          :is-submitting="isSubmitting"
        />
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
  .form {
    padding: 0.5rem;
  }
</style>
