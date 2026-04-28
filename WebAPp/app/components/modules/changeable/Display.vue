<script setup lang="ts">
  const props = defineProps({
    changeDetails: {
      type: Array as PropType<ChangeDetail[]>,
      default: () => [],
    },
    changeable: {
      type: Object as PropType<Changeable>,
      required: true,
    },
  });

  const changeDetail = computed(() => props.changeDetails.find((x) => x.changeableId == props.changeable.id));
  const isDeleted = computed(() => changeDetail.value && changeDetail.value.changeType == ChangeType.Delete);
  const isAdded = computed(() => changeDetail.value && changeDetail.value.changeType == ChangeType.Add);
  const isModified = computed(() => changeDetail.value && changeDetail.value.changeType == ChangeType.Update);
</script>

<template>
  <CommonMoleculesADeletedText v-if="isDeleted">
    {{ changeable.value }}
  </CommonMoleculesADeletedText>
  <CommonMoleculesACreatedText v-else-if="isAdded">
    {{ changeable.value }}
  </CommonMoleculesACreatedText>
  <CommonOrganismAModifedText v-else-if="isModified">
    <template #created>
      {{ changeable.value }}
    </template>
    <template #deleted>
      {{ changeDetail?.oldValue }}
    </template>
  </CommonOrganismAModifedText>
  <CommonAtomsAText v-else>
    {{ changeable.value }}
  </CommonAtomsAText>
</template>

<i18n lang="json">
{
  "fr": {
    "title": "Modification mineure"
  }
}
</i18n>
