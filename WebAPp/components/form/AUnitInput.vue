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
  }
});

const unit = defineModel<Unit>({
  default: () => ({ wholeUnit: null, numerator: null, denominator: null }),
})


const validationRules = computed(() => {
  const rules = [];
  if (props.required) {
    rules.push('required');
  }
  return rules.join('|');
});

const numeratorAndDenominatorRules = computed(() => {
  const rules = [];
  if (unit.value?.denominator || unit.value?.numerator) {
    rules.push('required');
  }
  return rules.join('|');
});

const options: SelectOption[] = [
  { label: '-', value: null },
  { label: '1', value: 1 },
  { label: '2', value: 2 },
  { label: '3', value: 3 },
];

const denominatorOption: SelectOption[] = options.filter((x) => x.value !== 1);

const numeratorOption = computed(() =>
  options.filter((x) => {
    if (unit.value?.denominator && typeof x.value === 'number') {
      return x.value < unit.value.denominator;
    }
    return true;
  })
);
</script>

<template>
  <div>
    <FormAtomsABaseLabel forId="">
      {{ label }}
    </FormAtomsABaseLabel>
    <div class="flex">
      <FormANumberInput :name="`${name}.wholeUnit`" :label="t('wholeUnit')" :disabled="props.disabled"
        :required="props.required" :integer="true" :rules="validationRules" v-model="unit.wholeUnit" />
      <FormMoleculesASelectField :name="`${name}.numerator`" :label="t('numerator')" type="text"
        :disabled="props.disabled" :required="numeratorAndDenominatorRules.length > 0"
        :rules="numeratorAndDenominatorRules" :options="numeratorOption" v-model="unit.numerator" />
      <FormMoleculesASelectField :name="`${name}.denominator`" :label="t('denominator')" type="text"
        :required="numeratorAndDenominatorRules.length > 0" :disabled="props.disabled"
        :rules="numeratorAndDenominatorRules" :options="denominatorOption" v-model="unit.denominator" />
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
