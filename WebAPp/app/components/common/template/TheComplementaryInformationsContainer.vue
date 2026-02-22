<script setup lang="ts">

const { t } = useI18n();
const commentPanel = ref<HTMLDivElement | null>(null);

const observer = ref<MutationObserver | null>(null);
const childCount = ref(0);

onMounted(() => {
  if (commentPanel.value) {
    childCount.value = commentPanel.value.childElementCount;
    observer.value = new MutationObserver(() => {
      childCount.value = commentPanel.value?.childElementCount ?? 0;
    });
    observer.value.observe(commentPanel.value, { childList: true });
  }
});

onUnmounted(() => {
  observer.value?.disconnect();
});

const isVisible = computed(() => childCount.value > 1);
</script>

<template>
  <div id="comments-panel" v-show="isVisible" ref="commentPanel">
    <div class="comments-panel-header">{{t('complementaryInformation')}}</div>
  </div>
</template>

<i18n lang="json">{
  "fr": {
    "complementaryInformation": "Informations complémentaires"
  }
}</i18n>

<style lang="scss" scoped>
  #comments-panel {
    max-width: 280px;
    background: #fafafa;
    position: sticky;
    top: 24px;
    border-left: 1px solid #ddd;
    max-height: calc(100vh - 48px);
    overflow-y: auto;
    padding: 12px;
    display: flex;
    flex-direction: column;
    gap: 12px;
    margin: 2rem 0;
  }

  .comments-panel-header {
    font-size: 12px;
    font-weight: 700;
    color: #888;
    text-transform: uppercase;
    letter-spacing: 0.5px;
    border-bottom: 1px solid #ddd;
    margin-bottom: 4px;
  }
</style>