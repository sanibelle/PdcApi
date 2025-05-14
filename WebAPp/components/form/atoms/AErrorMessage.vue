<script setup lang="ts">
defineProps({
  message: {
    type: String,
    default: '',
  },
});
</script>

<template>
  <transition name="fade">
    <p v-if="message" class="error-message">{{ message }}</p>
  </transition>
</template>

<style scoped>
.error-message {
  margin-top: 4px;
  font-size: 12px;
  color: #ef4444;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
