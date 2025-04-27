<script setup lang="ts">
import type { Step1 } from '~/types/calendar/creationSteps/step1';

const { handleStep1 } = inject<Step1>('step1')!;
</script>

<template>
  <button @click="handleStep1">test</button>
</template>
