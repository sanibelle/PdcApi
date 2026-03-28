<script setup lang="ts">
import { ref, onMounted } from 'vue';
import { Calendar, type EventDropArg, type EventInput } from '@fullcalendar/core';
import multiMonthPlugin from '@fullcalendar/multimonth';
import interactionPlugin from '@fullcalendar/interaction';
import frLocale from '@fullcalendar/core/locales/fr';
import { SemesterEnum } from '#shared/types/enum/SemesterEnum';


const { duration, semester, year } = defineProps<{
  duration: number;
  semester: SemesterEnum;
  year: number;
}>();
const calendarEl = ref<HTMLElement | null>(null);
const month = semester === SemesterEnum.Winter ? 0 : 7;

const events = ref<EventInput[]>([
  {
    id: 'a',
    title: '1 - jour 1',
    start: new Date('2025-01-01'),
    allDay: true,
    extendedProps: {
      type: '',
    },
  },
  {
    id: 'a',
    title: '2 - jour 1',
    allDay: true,
    start: new Date('2025-01-08'),
    extendedProps: {
      type: '',
    },
  },
  {
    id: 'b',
    allDay: true,
    title: 'Semaine detude',
    start: new Date('2025-01-20:00:00:00'),
    end: new Date('2025-01-25:00:00:00'),
    editabe: false,
    extendedProps: {
      type: 'Semaine detude',
    },
  },
]);

new Date(year, month, 1);

onMounted(() => {
  if (calendarEl.value) {
    const calendar = new Calendar(calendarEl.value as HTMLElement, {
      plugins: [multiMonthPlugin, interactionPlugin],
      editable: true,
      initialView: 'multiMonthFourMonth',
      multiMonthMaxColumns: 1,
      multiMonthMinWidth: 350,
      navLinks: true,
      headerToolbar: false,
      locale: frLocale,
      weekends: false,
      eventDrop: function (info: EventDropArg) {
        events.value = events.value.map((x) =>
          x.id === info.event.id ? (info.event as EventInput) : x
        );
        console.log(events.value.map((x) => x.start));

        const relache = events.value.find((x) => x.extendedProps?.type === 'Semaine detude');
        if (relache && info.event.start && relache.start && relache.end) {
          if (info.event.start > relache.start && info.event.start < relache.end) {
            alert('pas pendant la relache');
            info.revert();
          }
        }
      },
      views: {
        multiMonthFourMonth: {
          type: 'multiMonth',
          duration: { months: duration },
        },
      },
      events: events.value,
    });
    calendar.render();
  }
});
</script>

<template>
  <div>
    <div ref="calendarEl" class="w-100" style="width: 500px"></div>
  </div>
</template>
