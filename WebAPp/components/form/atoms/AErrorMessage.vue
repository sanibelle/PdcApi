<script setup lang="ts">
defineProps({
  message: {
    type: String,
    default: '',
  },
});

const errorRef = ref<HTMLParagraphElement | null>(null)

const scrollTo = async () => {
  await nextTick()
  errorRef.value?.parentElement?.scrollIntoView({ behavior: 'smooth' })
}
</script>

<template>
  <transition name="fade" @after-enter="scrollTo">
    <p v-if="message" ref="errorRef" class="error-message" role="alert" aria-live="assertive">{{ message }}</p>
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
