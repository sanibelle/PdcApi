<script setup lang="ts">
  import '~/assets/css/form.css';
  import { useForm } from 'vee-validate';

  const { t } = useI18n();

  const emit = defineEmits(['submitted']);
  const codeExistingErrorMessage = ref('');

  const courseFramework = reactive<Partial<CourseFramework>>({
    courseCode: '',
    name: '',
  });

  const { handleSubmit, isSubmitting } = useForm<CourseFramework>({
    validateOnMount: false,
  });

  const props = defineProps({
    programCode: {
      type: String,
      default: '',
    },
  });

  const { createCourseFramework } = useCourseFrameworkClient();

  const onSubmit = handleSubmit(async () => {
    try {
      // TODO valider qui crée la compétence.... Le parent?
      emit('submitted', await createCourseFramework(props.programCode, courseFramework));
    } catch (e) {
      if (e instanceof DuplicateException) {
        codeExistingErrorMessage.value = t('codeExistingErrorMessage');
      } else {
        console.error('Error creating competency:', e);
      }
    }
  });

  watch(
    () => courseFramework.courseCode,
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
        v-model="courseFramework.courseCode"
        name="code"
        :label="t('code')"
        placeholder="Ex : 00SU"
        :min="3"
        :max="50"
        :required="true"
        :error-message="codeExistingErrorMessage"
      />
      <FormATextInput
        v-model="courseFramework.name"
        name="name"
        :label="t('name')"
        :placeholder="t('namePlaceholder')"
        :required="true"
      />
      <div class="modal-footer">
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
    "title": "Créer un plan cadre",
    "formTitle": "Créer un plan cadre",
    "name": "Nom du cours",
    "code": "Code du cours",
    "statementOfCompetency": "Énoncé de plan cadre",
    "statementOfCompetencyPlaceholder": "Ex : Effectuer le développement d'applications Web transactionnelles",
    "competencyDescriptionPlaceholder": "Description détaillée du plan cadre",
    "codeExistingErrorMessage": "Le code de plan cadre existe déjà.",
    "optionnalCompetency": "Compétence optionnelle",
    "mandatoryCompetency": "Compétence obligatoire"
  }
}
</i18n>

<style scoped>
  .form {
    padding: 0.5rem;
  }
</style>
