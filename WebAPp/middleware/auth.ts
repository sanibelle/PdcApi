import { defineNuxtRouteMiddleware, navigateTo } from '#app';

export default defineNuxtRouteMiddleware(async (to) => {
  const authStore = useAuthStore();

  // Skip middleware for public routes
  if (to.meta.public) {
    return;
  }

  // The login fetches /user/me route and then sets the user in the store
  // If the user is not authenticated, redirect to the login page
  if (!authStore.isAuthenticated) {
    await authStore.login();
  }
});
