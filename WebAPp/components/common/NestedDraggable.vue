<script setup lang="ts">
import { VueDraggable, type SortableEvent } from 'vue-draggable-plus';

const emit = defineEmits(['end', 'update:modelValue']);
const { modelValue, group, idsToHighlight } = defineProps({
  modelValue: {
    type: Array<NestableItem>,
    required: true,
  },
  group: {
    type: Number,
    required: false,
    default: 1,
  },
  idsToHighlight: {
    type: Array<number>,
    required: true,
  },
});

const items = computed({
  get: () => modelValue,
  set: (value) => {
    emit('update:modelValue', value);
  },
});

const onMoveEnd = (evt: SortableEvent) => {
  emit('end', evt.item.dataset.id);
};

const isCompleted = (item: NestableItem): boolean => {
  return item.children
    ? item.children.every((x: NestableItem) => isCompleted(x))
    : !!item.isCompleted;
};
</script>

<template>
  <VueDraggable v-model="items" :animation="150" ghostClass="ghost" :group="{ name: `${group}`, pull: 'clone' }"
    @end="onMoveEnd">
    <div v-for="item in items" :key="item.id" class="item" :data-id="item.id">
      <div class="flex flex-row">
        <p :class="{
          completed: isCompleted(item),
          highlight: idsToHighlight.some((x) => x == item.id),
        }">
          {{ item.name }}
        </p>
        <CommonNestedDraggable v-if="item.children && item.children.length" v-model="item.children" :group="group + 1"
          :ids-to-highlight="idsToHighlight" @end="(id) => emit('end', id)" />
      </div>
    </div>
  </VueDraggable>
</template>

<style scoped>
.item {
  background-color: red;
  line-height: 30px;
  text-align: center;
  cursor: move;
  border: solid 1px black;
  margin-bottom: 5px;
}

.completed {
  text-decoration: line-through;
}
</style>
