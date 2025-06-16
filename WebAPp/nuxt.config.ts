import { resolve } from 'node:path';

const isHTTPS = process.env.NUXT_PUBLIC_API_USE_HTTPS == 'true'

export default defineNuxtConfig({
  // Utilisé pour le https en local pour que les cookie secure soient acceptés
  devServer: {
    https: isHTTPS ? {
      key: 'certificates/localhost-key.pem',
      cert: 'certificates/localhost.pem',
    } : undefined,
  },
  build: {
    transpile: ['@vuepic/vue-datepicker'],
  },
  compatibilityDate: '2024-11-01',
  devtools: { enabled: true },
  css: ['~/assets/css/main.css'],
  postcss: {
    plugins: {
      tailwindcss: {},
      autoprefixer: {},
    },
  },
  alias: {
    types: resolve(__dirname, './types'), // Use absolute path
    components: resolve(__dirname, './components'), // Use absolute path
  },
  typescript: {
    strict: true,
    typeCheck: true,
  },
  plugins: ['~/plugins/opentelemetry'],
  i18n: {
    bundle: {
      optimizeTranslationDirective: false,
    },
    strategy: 'prefix_except_default',
    customRoutes: 'page',
    defaultLocale: 'fr',
    locales: [{ code: 'fr', iso: 'fr-CA', name: 'Français', file: 'fr.json' }],
    detectBrowserLanguage: {
      useCookie: true,
    },
  },
  modules: [
    '@nuxtjs/i18n',
    '@pinia/nuxt',
    'pinia-plugin-persistedstate/nuxt',
    'nuxt-security',
    '@vee-validate/nuxt',
  ],
  runtimeConfig: {
    public: {
      apiBaseUrl: '',
    },
  },
  imports: {
    dirs: ['composables/**', 'shared/types/**'],
  },
  future: {
    compatibilityVersion: 4,
  },
});
