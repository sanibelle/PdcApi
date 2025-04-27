<script setup lang="ts">
import type { Step } from 'types/common/Step';

const { steps } = defineProps<{
  steps: Step[];
}>();

const emit = defineEmits<{
  (e: 'on-select-step', newSteps: Step[], selectedIndex: number): void;
}>();

const handleSelectStep = (currentStepIndex: number) => {
  const res: Step[] = steps.map((step, index) => ({
    ...step,
    isSelected: index === currentStepIndex,
    isCompleted: index < currentStepIndex,
    isDisabled: index > currentStepIndex + 1,
  }));
  emit('on-select-step', res, currentStepIndex);
};
</script>

<template>
  <div class="min-h-screen bg-gray-100 flex items-center justify-center p-4">
    <div class="w-full max-w-4xl bg-white rounded-lg shadow-md">
      <template v-for="(step, index) in steps" :key="index">
        <div class="flex border-b">
          <button
            class="flex-1 py-4 px-6 text-sm font-medium focus:outline-none transition-colors duration-200"
            :class="{
              disabled: step.isDisabled,
              active: step.isSelected,
              test: step.isCompleted,
            }"
            @click="handleSelectStep(index)"
            :aria-selected="step.isSelected"
            :disabled="step.isDisabled"
            role="tab"
          >
            {{ step.title }}
          </button>
        </div>
        <div v-if="step.isSelected" class="p-6">
          <component :is="step.content" />
        </div>
      </template>
    </div>
  </div>
</template>

<style lang="postcss">
.disabled {
  @apply text-gray-400 bg-gray-100 cursor-not-allowed;
}

.active {
  @apply text-blue-600 border-b-2 border-blue-600;
}

.test {
  @apply text-gray-500 hover:text-gray-700 hover:bg-gray-50;
}
</style>
