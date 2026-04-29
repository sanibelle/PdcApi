<template>
  <div class="modified flex-row">
    <CommonMoleculesADeletedText>
      <slot name="deleted" />
    </CommonMoleculesADeletedText>
    <CommonMoleculesACreatedText>
      <slot name="created" />
    </CommonMoleculesACreatedText>
  </div>
</template>

<style scoped>
  .flex-row {
    display: flex;
    flex-direction: row;
  }
  .flex-row > * {
    flex: 1;
  }
  :deep(.created) {
    border: 1px solid rgba(22, 101, 52, 0.16);
    border-right: 3px solid rgba(22, 101, 52, 0.35);
    border-radius: 0 4px 4px 0;
  }
  :deep(.deleted) {
    border-radius: 4px 0 0 4px;
  }
</style>
