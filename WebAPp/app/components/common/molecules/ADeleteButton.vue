<script setup lang="ts">
  defineProps({
    isDisabled: {
      type: Boolean,
      default: false,
    },
  });

  const emit = defineEmits(['click']);
</script>

<template>
  <button
    :disabled="isDisabled"
    class="btn-delete"
    @click="emit('click', $event)"
  >
    ✕
  </button>
</template>

<style lang="scss" scoped>
  .btn-delete {
    transition: opacity 0.15s ease;
    background: none;
    border: none;
    cursor: pointer;
    padding: 2px 4px;
    border-radius: 3px;
    line-height: 1;

    &:hover {
      color: #c0392b;
      background: #fdecea;
    }
  }
</style>
