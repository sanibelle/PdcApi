<script setup lang="ts">
  import type { NuxtError } from '#app';

  defineProps({
    error: {
      type: Object as () => NuxtError,
      required: true,
    },
  });
  //TODO
  const handleError = () => clearError({ redirect: '/' });
</script>

<template>
  <div>
    <h2>{{ error?.status }}</h2>
    <button @click="handleError">Retourner à la page d'accueil</button>
  </div>
</template>
