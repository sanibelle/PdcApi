<script setup lang="ts">

const { handleStep1 } = inject<Step1>('step1')!;
</script>

<template>
  <button @click="handleStep1">test</button>
</template>
