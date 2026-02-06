<script setup lang="ts">

interface Props {
  week: Week;
}

const props = defineProps<Props>();

const displayMonth = (date: Date, index: number): boolean => {
  if (props.week.dates.length === 1) return true;
  if (index + 1 === props.week.dates.length) return true;
  if (props.week.dates[index + 1]?.getMonth() !== date.getMonth()) return true;
  return false;
};

const displayComa = (index: number): boolean => {
  if (displayMonth(props.week.dates[index] as any, index)) return false;
  return props.week.dates.length > 1 && index < props.week.dates.length - 1;
};

const displayAnd = (index: number): boolean => {
  return displayMonth(props.week.dates[index]as any, index) && index < props.week.dates.length - 1;
};
</script>

<template>
  <div>
    <p>
      Semaine {{ week.day }}
      <template v-for="(date, index) in week.dates" :key="index">
        <ADate :date="date" :display-month="displayMonth(date, index)" />
        <template v-if="displayComa(index)">-</template>
        <template v-else-if="displayAnd(index)">et</template>
      </template>
    </p>
  </div>
</template>
