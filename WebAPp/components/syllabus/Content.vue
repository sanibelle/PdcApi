<script setup lang="ts">
import type NestableItem from '~/types/syllabus/NestableItem';

const emit = defineEmits(['onRemove', 'onHighlight']);
const { item, depth } = defineProps({
  item: {
    type: Object as () => NestableItem,
    required: true,
  },
  depth: {
    type: Number,
    default: 0,
  },
});

const onRemove = (id: number) => {
  emit('onRemove', id);
};

const onHighlight = (id?: number) => {
  emit('onHighlight', id ? [id] : []);
};
</script>

<template>
  <div
    :class="{ item: depth == 0 }"
    :style="{ paddingLeft: `${depth * 1}rem` }"
    @mouseover.stop="onHighlight(item.id)"
    @mouseleave="onHighlight()"
  >
    {{ item.name }}
    <button @click="onRemove(item.id)">poubelle</button>
    <SyllabusContent
      v-for="child in item.children"
      :key="child.id"
      :item="child"
      :depth="depth + 1"
      @on-highlight="(ids) => emit('onHighlight', [...ids, item.id])"
      @on-remove="onRemove"
    />
  </div>
</template>

<style scoped>
button {
  background-color: red;
  color: white;
  border: none;
  padding: 0.5rem 1rem;
  cursor: pointer;
  border-radius: 0.25rem;
}

button:hover {
  background-color: darkred;
}
</style>
