<script setup lang="ts">
const props = defineProps({
  hint: {
    type: String,
    default: '',
  },
});
</script>
<template>
  <p v-if="hint" class="hint-text">{{ hint }}</p>
</template>

<style scoped>
.hint-text {
  margin-top: 4px;
  font-size: 12px;
  color: #64748b;
}
</style>
