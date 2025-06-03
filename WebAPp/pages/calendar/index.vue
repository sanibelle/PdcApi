<script setup lang="ts">
import ASemesterCalendar from '~/components/calendar/ASemesterCalendar.vue';
</script>

<template>
  <ASemesterCalendar :semester="SemesterEnum.Winter" :duration="5" :year="2025">
  </ASemesterCalendar>
</template>
