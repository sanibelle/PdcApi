<script setup lang="ts">
  const { t } = useI18n();
  const authStore = useAuthStore();
  onMounted(async () => {
    await authStore.authenticate();
  });
</script>
<template>
  <div class="wrapper">
    <LayoutMoleculesNavBadge
      to="administration"
      :slug="t('administration')"
    />
    <!-- TODO creer une page qui affiche les devis ministeriels et c'est tout -->
    <LayoutMoleculesNavBadge
      :to="'administration-programOfStudy'"
      :slug="t('programOfStudy')"
    />
    <LayoutMoleculesNavBadge
      to="courseFramework"
      :slug="t('courseFramework')"
      :display-only-when-authenticated="true"
    />
  </div>
</template>

<style scoped lang="scss">
  .wrapper {
    display: flex;
    gap: 1rem;
    align-items: center;
    justify-content: start;
    width: 100%;
  }
</style>

<i18n lang="json">
{
  "fr": {
    "administration": "Administration",
    "courseFramework": "Plans cadres",
    "programOfStudy": "Programmes d'études"
  }
}
</i18n>
