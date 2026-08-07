<script setup lang="ts">
  const route = useRoute();
  const props = defineProps({
    to: {
      type: String,
      required: true,
    },
    slug: {
      type: String,
      required: true,
    },
    displayOnlyWhenAuthenticated: {
      type: Boolean,
      default: false,
    },
  });

  // removes the ___fr at the end of the route name
  const routerLinkExactActive = computed(() => route.name?.toString().split('___')[0] === props.to);
  const routerLinkActive = computed(() => route.name?.toString().split('___')[0]?.includes(props.to));

  const authStore = useAuthStore();
  onMounted(async () => {
    await authStore.authenticate();
  });

  // TODO afficher un badge en fonction des droits de l'utilisateur (admin, enseignant, étudiant)
</script>
<template>
  <LayoutAtomsNavLink
    v-if="!props.displayOnlyWhenAuthenticated || authStore.isAuthenticated"
    :to="props.to"
  >
    <div
      class="badge"
      :class="{ 'router-link-active': routerLinkActive, 'router-link-exact-active': routerLinkExactActive }"
    >
      <span class="link">
        {{ props.slug }}
      </span>
    </div>
  </LayoutAtomsNavLink>
</template>

<style scoped lang="scss">
  .link {
    font-size: 1rem;
    text-align: center;
    white-space: nowrap;
  }
  .badge {
    font-size: 1rem;
    text-align: center;
    white-space: nowrap;

    background: #fff;
    color: #111;
    border: none;
    box-shadow: 0 2px 6px rgb(0 0 0 / 0.06);

    &:hover {
      box-shadow: 0 4px 12px rgb(0 0 0 / 0.1);
    }
    &:focus-visible {
      outline: 2px solid #111;
      outline-offset: 2px;
    }

    border-radius: 999px;
    padding: 1rem 2.5rem;
    min-width: 160px;
  }
  .router-link-active {
    font-weight: bold;
  }
  .router-link-exact-active {
    font-weight: bold;
    color: #3b82f6; /* Alexis : On met une couleur au link exactement actif? */
  }
</style>
