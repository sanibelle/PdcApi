<template>
  <div class="created">
    <CommonAtomsAText>
      <slot />
    </CommonAtomsAText>
  </div>
</template>

<style scoped>
  .created {
    background-color: #dcfce7;
    color: #166534;
    border: 1px solid rgba(22, 101, 52, 0.16);
    border-left: 3px solid rgba(22, 101, 52, 0.35);
    border-radius: 4px;
    box-shadow:
      inset 0 1px 0 rgba(255, 255, 255, 0.35),
      0 1px 2px rgba(22, 101, 52, 0.08);
    transition:
      border-color 0.2s ease,
      box-shadow 0.2s ease;
  }
</style>
