<script setup lang="ts">
  const props = defineProps({
    date: {
      type: [Date, String, undefined, null],
      default: null,
    },
    displayMonth: { type: Boolean, default: false },
  });
  const displayedDate = computed(() => {
    if (!props.date) {
      return '';
    }
    const dateObj = props.date instanceof Date ? props.date : new Date(props.date);
    return dateObj.toLocaleDateString('fr-CA', props.displayMonth ? { day: 'numeric', month: 'long' } : { day: 'numeric', month: 'numeric', year: 'numeric', hour: 'numeric', minute: 'numeric' });
  });
</script>

<template>
  <span>
    {{ displayedDate }}
  </span>
</template>
