<script setup lang="ts">
  const localePath = useLocalePath();
  const props = defineProps({
    to: {
      type: String,
      required: true,
    },
  });
  console.log('props.to', props.to);
</script>
<template>
  <NuxtLink
    class="link"
    :to="localePath(props.to)"
  >
    <slot />
  </NuxtLink>
</template>

<style scoped></style>
