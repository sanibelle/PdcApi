<script setup lang="ts">

const { t } = useI18n();

defineI18nRoute({
  paths: {
    fr: '/gestion',
  },
});
const localePath = useLocalePath();

</script>

<template>
  <div>
    <h1>{{ t('title') }}</h1>
    <div>
      <NuxtLink :to="localePath('/administration/programOfStudy')">{{ t('studyProgram') }}</NuxtLink>
    </div>
    <div>
      <NuxtLink :to="localePath('/administration/user')">{{ t('user') }}</NuxtLink>
    </div>
  </div>
</template>

<i18n lang="json">{
  "fr": {
    "title": "Gestion",
    "studyProgram": "Programmes d'études",
    "user": "Utilisateurs"
  }
}</i18n>