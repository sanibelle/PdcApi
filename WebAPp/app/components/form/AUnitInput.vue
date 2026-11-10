<script setup lang="ts">
  const { t } = useI18n();

  const props = defineProps({
    name: {
      type: String,
      required: true,
    },
    label: {
      type: String,
      default: '',
    },
    hint: {
      type: String,
      default: '',
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    required: {
      type: Boolean,
      default: false,
    },
  });

  const unit = defineModel<Unit>({
    default: () => ({ wholeUnit: null, numerator: null, denominator: null }),
  });

  const validationRules = computed(() => {
    const rules = [];
    if (props.required) {
      rules.push('required');
    }
    return rules.join('|');
  });

  const numeratorOptions = [1, 2];

  const denominatorOption = [3];

  watch(
    () => unit.value.numerator,
    (newValue) => {
      if (newValue) {
        unit.value.denominator = 3;
      } else {
        unit.value.denominator = null;
      }
    },
  );

  watch(
    () => unit.value.denominator,
    (newValue) => {
      if (!newValue && unit.value.numerator) {
        unit.value.numerator = null;
      } else if (newValue && !unit.value.numerator) {
        unit.value.numerator = 1;
      }
    },
  );
</script>

<template>
  <div>
    <FormAtomsABaseLabel for-id="">
      {{ label }}
    </FormAtomsABaseLabel>
    <div class="flex">
      <FormANumberInput
        v-model="unit.wholeUnit"
        :name="`${name}.wholeUnit`"
        :label="t('wholeUnit')"
        :disabled="props.disabled"
        :required="props.required"
        :integer="true"
        :rules="validationRules"
      />
      <FormMoleculesASelectField
        v-model="unit.numerator"
        :name="`${name}.numerator`"
        :label="t('numerator')"
        :disabled="props.disabled"
        :options="numeratorOptions"
      />
      <FormMoleculesASelectField
        v-model="unit.denominator"
        :name="`${name}.denominator`"
        :label="t('denominator')"
        :disabled="props.disabled"
        :options="denominatorOption"
      />
    </div>
    <FormAtomsAHint :hint="hint" />
  </div>
</template>

<i18n>
  {
    "fr": {
      "denominator": "Dénominateur",
      "numerator": "Numérateur",
      "wholeUnit": "Unité entière"
    },
  }
</i18n>

<style scoped>
  .flex {
    display: flex;
    align-items: center;
    gap: 0.5rem;
    justify-content: flex-start;

    &:not(:last-child) {
      margin-right: 0.5rem;
    }

    div {
      flex-grow: 1;

      &:first-child {
        flex-grow: 1;
      }
    }
  }
</style>
