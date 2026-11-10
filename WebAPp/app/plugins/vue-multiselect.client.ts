import VueMultiselect from 'vue-multiselect';
import 'vue-multiselect/dist/vue-multiselect.css';

export default defineNuxtPlugin((nuxtApp) => {
  // Registers it globally under <VueMultiselect />
  nuxtApp.vueApp.component('VueMultiselect', VueMultiselect);
});
