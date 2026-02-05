<script setup lang="ts">
import TheStep1 from '~/components/calendar/creationSteps/TheStep1.vue';
import AStepNavigator from '~/components/common/AStepNavigator.vue';

const activeTab = ref(0);
const steps = ref<Step[]>([
  {
    id: 0,
    title: 'Step 1',
    content: markRaw(TheStep1), // You'll need to import step1 component
    isDisabled: false,
    isCompleted: false,
    isSelected: true,
  },
  {
    id: 1,
    title: 'Step 2',
    content: markRaw(TheStep1), // You'll need to import step2 component
    isDisabled: false,
    isCompleted: false,
    isSelected: false,
  },
]);

const handleStep1 = (test: any) => {
  console.log(test);
};

provide<Step1>('step1', {
  handleStep1,
});

const handleSelectStep = (newSteps: Step[], selectedIndex: number) => {
  steps.value = newSteps;
};
</script>

<template>
  allo
  <div>
    <AStepNavigator :steps="steps" @on-select-step="handleSelectStep" />
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
