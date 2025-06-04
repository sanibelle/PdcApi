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
            Bonjour, {{ authStore.user?.name }}!
            <button @click="authStore.logout">Logout</button>
          </div>
          <div v-else>
            <button @click="authStore.login">Login</button>
          </div>
        </div>
      </ClientOnly>
    </header>

    <!-- Main content slot -->
    <div class="wrapper flex-grow">
      <slot />
    </div>

    <footer class="bg-gray-900 text-white py-8">POC semaines plan de cours</footer>
  </div>
</template>
