const authStore = useAuthStore();

<script setup lang="ts">
const { t } = useI18n();
defineI18nRoute({
  paths: {
    fr: '/connexion',
  },
});
const authStore = useAuthStore();
authStore.authenticate();
</script>

<template>Connexion en cours</template>
