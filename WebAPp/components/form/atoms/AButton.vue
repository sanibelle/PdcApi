<script setup lang="ts">
defineProps({
  isDisabled: {
    type: Boolean,
    default: false,
  },
});
</script>

<template>
  <button type="submit" class="button" :disabled="isDisabled">
    <slot></slot>
  </button>
</template>

<style scoped>
button {
  padding: 8px 16px;
  border: none;
  border-radius: 4px;
  background-color: #3b82f6;
  color: white;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.2s ease;

  &:hover {
    background-color: #2563eb;
  }

  &:disabled {
    background-color: #93c5fd;
    cursor: not-allowed;
  }
}
</style>
