<script setup lang="ts">
  const { t } = useI18n();

  const props = defineProps({
    changeRecordNumber: {
      type: Number,
      required: true,
    },
    isDraft: {
      type: Boolean,
      default: false,
    },
  });
  const showChangeHistory = ref(false);
  const changeRecordNumberToCompare = ref(props.changeRecordNumber);
  const nextIsDisabled = computed(() => changeRecordNumberToCompare.value == props.changeRecordNumber);
  const previousIsDisabled = computed(() => changeRecordNumberToCompare.value == 2);

  /**
   * Emits the `update:changeNumberToCompare` event when the change number to compare is updated.
   *
   * @param value - The selected change number, or `null` if we do not want to show the change history.
   *
   */
  const emit = defineEmits<{
    (e: 'update:changeNumberToCompare', value: number | null): void;
  }>();

  watch(
    () => showChangeHistory.value,
    (newValue) => {
      if (newValue) {
        changeRecordNumberToCompare.value = props.changeRecordNumber;
        emit('update:changeNumberToCompare', changeRecordNumberToCompare.value);
      } else {
        emit('update:changeNumberToCompare', null);
      }
    },
  );
  watch(
    () => changeRecordNumberToCompare.value,
    () => {
      if (showChangeHistory.value) {
        emit('update:changeNumberToCompare', changeRecordNumberToCompare.value);
      }
    },
  );
</script>

<template>
  <div
    class="changeRecord-badge"
    :class="{ draft: isDraft }"
  >
    <CommonAtomsAButton
      v-if="showChangeHistory"
      :is-disabled="previousIsDisabled"
      @click="changeRecordNumberToCompare--"
    >
      ◀
    </CommonAtomsAButton>
    <span
      v-if="showChangeHistory"
      class="changeRecord-text"
    >
      v{{ changeRecordNumberToCompare }}
    </span>
    <FormACheckboxInput
      v-if="changeRecordNumber > 1"
      v-model="showChangeHistory"
      :name="'showChangeHistory'"
      :label="t('showChangeHistory')"
    />
    <CommonAtomsAButton
      v-if="showChangeHistory"
      :is-disabled="nextIsDisabled"
      @click="changeRecordNumberToCompare++"
    >
      ▶
    </CommonAtomsAButton>
    <span class="changeRecord-text">v{{ changeRecordNumber }}</span>
    <div
      v-if="isDraft"
      class="draft-label"
    >
      {{ t('draft') }}
      <br />
      <template v-if="changeRecordNumber !== 1">{{ t('notv1') }}</template>
      <div
        class="changeRecord-badge"
        :class="{ draft: isDraft }"
      ></div>
    </div>
    <span
      v-else
      class="draft-label"
    >
      {{ t('notdraft') }}
      <br />
      {{ t('newVersion') }}
    </span>
  </div>
</template>

<i18n>
{
    "fr": {
        "draft": "Cette version est en cours de rédaction.",
        "notv1": "Tous les changements effectués à cette version seront suivis et auront un impact dans tous les documents qui y sont liés.",
        "notdraft": "Cette version est approuvée. Seules les modifications mineures sont possibles (ex : corriger les fautes de frappe).",
        "newVersion": "Pour effectuer des modifications majeures, veuillez créer une nouvelle version.",
        "showChangeHistory": "Afficher l'historique des changements",
    }
}
</i18n>

<style scoped>
  .changeRecord-badge {
    display: inline-flex;
    align-items: center;
    gap: 0.25rem;
    padding: 0.25rem 0.5rem;
    border-radius: 4px;
    background-color: #e5e7eb;
    color: #374151;
    font-size: 0.875rem;
    font-weight: 500;
  }

  .changeRecord-badge.draft {
    background-color: #fed7aa;
    color: #ea580c;
  }

  .changeRecord-text {
    font-weight: 600;
  }

  .draft-label {
    font-size: 0.6rem;
    text-transform: uppercase;
    font-style: italic;
    letter-spacing: 0.05em;
  }
</style>
