<script setup lang="ts">
const authStore = useAuthStore();
</script>

<template>
  <div class="min-h-screen bg-gray-100 flex flex-col space-between">
    <!-- Header -->
    <header class="bg-white shadow-md">
      POC semaines plan de cours
      <NuxtLink to="/">Home</NuxtLink>
      <ClientOnly>
        <div>
          <div v-if="authStore.isAuthenticated">
            Bonjour, {{ authStore.user?.userName }}!
            <button @click="authStore.logout">Logout</button>
          </div>
          <div v-else>
            <button @click="authStore.login">Login</button>
          </div>
        </div>
      </ClientOnly>
    </header>
    <div class="wrapper">

      <!-- Main content slot -->
      <div>
        <slot />
      </div>
      <CommonTemplateTheComplementaryInformationsContainer />
    </div>

    <footer class="bg-gray-900 text-white py-8">POC semaines plan de cours</footer>
  </div>
</template>

<style lang="scss" scoped>
.wrapper {
  display: flex;
  align-items: flex-start;
  gap: 24px;
  padding: 24px;
  margin: auto;
}

.wrapper > div:first-child {
  flex: 1;
  min-width: 0;
}
</style>