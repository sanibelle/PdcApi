<script setup lang="ts">

const weeks: Week[] = [
  {
    day: 1,
    dates: [new Date(2024, 0, 4)],
  },
  {
    day: 2,
    dates: [new Date(2024, 0, 11), new Date(2024, 0, 12), new Date(2024, 0, 13)],
  },
  {
    day: 3,
    dates: [new Date(2024, 0, 30), new Date(2024, 1, 2), new Date(2024, 1, 3)],
  },
];
</script>

<template>
  <h1>Mes semaines</h1>
  <section id="services" class="py-20 bg-white">
    <template v-for="(week, index) in weeks" :key="index">
      <AWeek :week="week" />
    </template>
  </section>
</template>
