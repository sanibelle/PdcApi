<script setup lang="ts">
  const props = defineProps({
    slug: {
      type: String,
      required: true,
    },
    displayOnlyWhenAuthenticated: {
      type: Boolean,
      default: false,
    },
    isActive: {
      type: Boolean,
      default: false,
    },
  });
</script>
<template>
  <div
    class="badge"
    :class="{ 'is-active': isActive }"
  >
    {{ props.slug }}
  </div>
</template>

<style scoped lang="scss">
  .badge {
    background: #fff;
    color: #111;
    border-radius: 999px;
    padding: 0.25rem 0.25rem;
    margin: 0.25rem 0.25rem;
  }

  .is-active {
    font-weight: bold;
  }
</style>
