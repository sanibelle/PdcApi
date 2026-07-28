<script setup lang="ts">
  const localePath = useLocalePath();
</script>

<template>
  <div>
    <nav>
      <NuxtLink :to="localePath('syllabus')">Syllabus</NuxtLink>
      <NuxtLink :to="localePath('calendar')">Calendar</NuxtLink>
      <NuxtLink :to="localePath('management-createSemesterCalendar')">Creer Semestre</NuxtLink>
      <NuxtLink :to="localePath('syllabus-create')">Créer un plan de cours</NuxtLink>
      <NuxtLink :to="localePath('administration')">Administration</NuxtLink>
      <NuxtLink :to="localePath('courseFramework')">Plans cadres</NuxtLink>
    </nav>
  </div>
</template>
