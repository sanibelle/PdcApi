<script setup lang="ts">
  import '~/assets/css/form.css';
  import { useForm } from 'vee-validate';

  const { t } = useI18n();

  const emit = defineEmits(['submitted', 'close']);
  const codeExistingErrorMessage = ref('');
  const submitAnotherAfterThisOne = ref(false);

  defineProps({
    programCode: {
      type: String,
      default: '',
    },
    competencies: {
      type: Array as PropType<Competency[]>,
      required: true,
    },
  });

  const courseFramework = reactive<Partial<CourseFramework>>({
    competencies: [],
  });

  const { handleSubmit, isSubmitting } = useForm<CourseFramework>({
    validateOnMount: false,
  });

  const { createCourseFramework } = useCourseFrameworkClient();

  const onSubmit = handleSubmit(async () => {
    try {
      const newCourseFramework = await createCourseFramework(courseFramework as CourseFramework);
      emit('submitted', newCourseFramework);
      if (submitAnotherAfterThisOne.value) {
        courseFramework.code = '';
        courseFramework.name = '';
        courseFramework.semester = undefined;
        courseFramework.theoryHours = undefined;
        courseFramework.laboratoryHours = undefined;
        courseFramework.personalWorkHours = undefined;
        courseFramework.competencies = [];
      } else {
        emit('close');
      }
    } catch (e) {
      if (e instanceof DuplicateException) {
        codeExistingErrorMessage.value = t('codeExistingErrorMessage');
      } else {
        console.error('Error creating competency:', e);
      }
    }
  });

  watch(
    () => courseFramework.code,
    () => {
      codeExistingErrorMessage.value = '';
    },
  );
</script>

<template>
  <div class="form">
    <form
      class="form-container"
      @submit.prevent="onSubmit"
    >
      <FormATextInput
        v-model="courseFramework.code"
        name="code"
        :label="t('code')"
        :placeholder="t('codePlaceholder')"
        :hint="t('codeHint')"
        :min="3"
        :max="50"
        :required="true"
        :trimmed="true"
        :error-message="codeExistingErrorMessage"
      />
      <FormATextInput
        v-model="courseFramework.name"
        name="name"
        :label="t('name')"
        :required="true"
      />
      <FormANumberInput
        v-model="courseFramework.semester"
        name="semester"
        :label="t('semester')"
        :required="true"
        :min="1"
      />
      <FormANumberInput
        v-model="courseFramework.theoryHours"
        name="theoryHours"
        :label="t('theoryHours')"
        :required="true"
        :min="1"
      />
      <FormANumberInput
        v-model="courseFramework.laboratoryHours"
        name="laboratoryHours"
        :label="t('laboratoryHours')"
        :required="true"
        :min="1"
      />
      <FormANumberInput
        v-model="courseFramework.personalWorkHours"
        name="personalWorkHours"
        :label="t('personalWorkHours')"
        :required="true"
        :min="1"
      />
      <FormASelectInput
        v-model="courseFramework.competencies"
        name="competencies"
        :multiple="true"
        :options="competencies as any /*TODO changer le type dans le drop down*/"
        :label="t('targettedCompetencies')"
        :required="true"
        :track-by="'statementOfCompetency'"
        :searchable="true"
        :min="1"
      />
      <div class="modal-footer">
        <FormACheckboxInput
          v-model="submitAnotherAfterThisOne"
          name="submitAnotherAfterThisOne"
          :label="t('submitAnotherAfterThisOne')"
          data-testid="submit-competency"
        />
        <FormMoleculesASubmitButton
          :is-submitting="isSubmitting"
          data-testid="submit-competency"
        />
      </div>
    </form>
  </div>
</template>

<i18n lang="json">
{
  "fr": {
    "name": "Titre du cours",
    "code": "Numéro du cours",
    "codeHint": "Programme-Code-Établissement",
    "codePlaceholder": "XXX-XXX-XX",
    "codeExistingErrorMessage": "Le code de plan cadre existe déjà.",
    "theoryHours": "Heures de théorie",
    "laboratoryHours": "Heures de laboratoire",
    "personalWorkHours": "Heures de travail personnel",
    "semester": "Semestre (session)",
    "targettedCompetencies": "Compétences visées",
    "submitAnotherAfterThisOne": "Créer un autre"
  }
}
</i18n>

<style lang="scss" scoped>
  .form {
    padding: 0.5rem;
  }

  .modal-footer {
    display: flex;
    align-items: center;
    justify-content: flex-end;
    :deep(> div) {
      flex-grow: 1;
    }
  }
</style>
