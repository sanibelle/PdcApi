<script setup lang="ts">
import ASemesterCalendar from '~/components/calendar/ASemesterCalendar.vue';
import { SemesterEnum } from '~/shared/types/enum/SemesterEnum';
</script>

<template>
  <ASemesterCalendar :semester="SemesterEnum.Winter" :duration="5" :year="2025">
  </ASemesterCalendar>
</template>
