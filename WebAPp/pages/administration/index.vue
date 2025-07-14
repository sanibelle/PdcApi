<script setup lang="ts">

useI18n();

defineI18nRoute({
  paths: {
    fr: '/gestion',
  },
});
const localePath = useLocalePath();

onMounted(() => {
  const router = useRouter();
  const routes = router.getRoutes();
  console.log('All routes:', routes.map(r => ({ name: r.name, path: r.path })));
});
const upsertProgramOfStudyModal = useModal();
</script>

<template>
    <NuxtLink :to="localePath('/administration/programOfStudy')">GOOOOO</NuxtLink>
</template>
