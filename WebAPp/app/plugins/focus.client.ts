import { defineNuxtPlugin } from '#app';

export default defineNuxtPlugin((nuxtApp) => {
  nuxtApp.vueApp.directive('focus', {
    async mounted(el: HTMLElement, binding: { value: boolean }) {
      if (binding.value) {
        console.log(el)
        el.focus();
      }
    },
  });
});
