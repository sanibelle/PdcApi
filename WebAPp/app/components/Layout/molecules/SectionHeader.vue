<script setup lang="ts">
  const props = defineProps({
    slug: {
      type: String,
      required: true,
    },
  });
</script>
<template>
  <div class="wrapper">
    <LayoutAtomsSectionHeaderTitle :slug="props.slug" />
    <slot />
  </div>
</template>

<style scoped lang="scss">
  .wrapper {
    font-weight: bold;
    font-size: 0.75rem;
  }
</style>
