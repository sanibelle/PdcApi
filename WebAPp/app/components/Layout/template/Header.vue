<script setup lang="ts">
  const { t } = useI18n();
  const authStore = useAuthStore();
  onMounted(async () => {
    await authStore.authenticate();
  });
</script>
<template>
  <header>
    <div class="wrapper">
      <div class="logo">
        <NuxtLink to="/">plano</NuxtLink>
      </div>
      <ClientOnly>
        <template v-if="authStore.isAuthenticated">
          <div>
            <button @click="authStore.logout">{{ t('logout') }}</button>
          </div>
          <div>
            <p>{{ t('contact') }}</p>
          </div>
        </template>
        <template v-else>
          <button @click="authStore.authenticate">{{ t('login') }}</button>
        </template>
      </ClientOnly>
    </div>
  </header>
</template>

<style scoped>
  header {
    display: flex;
    align-items: center;
    justify-content: space-between;
    padding: 0 1rem;
    margin: 0.5rem;
    background-color: black;
    border-radius: 35px;
  }

  .wrapper {
    padding: 0 1rem;
    color: white;
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 100%;
  }
</style>

<i18n lang="json">
{
  "fr": {
    "contact": "contact",
    "login": "Se connecter",
    "logout": "Déconnexion"
  }
}
</i18n>
