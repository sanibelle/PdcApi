<template>
  <div class="deleted">
    <CommonAtomsAText>
      <slot />
    </CommonAtomsAText>
  </div>
</template>

<style>
  .deleted {
    text-decoration: line-through;
    background-color: #fee2e2;
    color: #991b1b;
    border: 1px solid rgba(153, 27, 27, 0.16);
    border-left: 3px solid rgba(153, 27, 27, 0.35);
    border-radius: 4px;
    box-shadow:
      inset 0 1px 0 rgba(255, 255, 255, 0.35),
      0 1px 2px rgba(153, 27, 27, 0.08);
    transition:
      border-color 0.2s ease,
      box-shadow 0.2s ease;
  }
</style>
