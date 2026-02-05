<script setup lang="ts">
defineProps({
  forId: {
    type: String,
    required: true,
  },
  required: {
    type: Boolean,
    default: false,
  },
});
</script>

<template>
  <label :for="forId" class="base-label" :class="{ required: required }">
    <slot></slot>
  </label>
</template>

<style scoped>
.base-label {
  display: block;
  margin-bottom: 4px;
  font-size: 14px;
  font-weight: 500;
  color: #1e293b;
}

.required::after {
  content: '*';
  color: #ef4444;
  margin-left: 2px;
}
</style>
