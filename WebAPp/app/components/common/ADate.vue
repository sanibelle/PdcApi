<script setup lang="ts">
defineProps<{
  date: Date;
  displayMonth: boolean;
}>();
</script>

<template>
  <span>
    {{
      date.toLocaleDateString(
        'fr-CA',
        displayMonth ? { day: 'numeric', month: 'long' } : { day: 'numeric' }
      )
    }}
  </span>
</template>
