<script setup lang="ts">
defineProps({
  message: {
    type: String,
    default: '',
  },
});

const ref = useTemplateRef('ref');

const  scrollTo = (element: HTMLParagraphElement  | null) => { 
  element?.parentElement?.scrollIntoView({ behavior: 'smooth' }) 
}

</script>

<template>
  
  <transition name="fade" @after-enter="scrollTo(ref)">
    <p v-if="message" ref="ref" class="error-message" role="alert" aria-live="assertive">{{ message }}</p>
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
