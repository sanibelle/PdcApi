<script setup lang="ts">

const { t } = useI18n();
const props = defineProps({
  complementaryInformations: {
    type: Array as PropType<ComplementaryInformation[]>,
    default: () => [],
  }
});

const hasComments = computed(() => props.complementaryInformations.length > 0);
const isHovered = ref(false);
const commentRef = ref<HTMLDivElement | null>(null);

const scrollToComment = () => {
  commentRef.value?.scrollIntoView({ behavior: 'smooth', block: 'nearest' });
}
</script>

<template>
  <div>
    <div :class="{ 'has-comment': hasComments, 'highlight': isHovered && hasComments }" @mouseenter="isHovered = true; scrollToComment()"
      @mouseleave="isHovered = false">
      <slot></slot>
    </div>
    <Teleport to="#comments-panel">
      <div class="comment" :class="{ 'highlight': isHovered }" v-if="hasComments" @mouseenter="isHovered = true"
        @mouseleave="isHovered = false" ref="commentRef">
        <div class="comment-entry" v-for="complementaryInformation of props.complementaryInformations"
          :key="complementaryInformation.id">
          <div class="comment-author">{{ complementaryInformation.createdBy?.userName || t('unknownUser') }}</div>
          <div class="comment-date">{{ complementaryInformation.createdOn}}</div>
          <div class="comment-text">{{ complementaryInformation.text}}</div>
        </div>
      </div>
    </Teleport>

  </div>
</template>

<i18n lang="json">{
  "fr": {
    "unknownUser": "Utilisateur inconnu"
  }
}</i18n>

<style lang="scss">
.has-comment {
  background: #fff9c4;
  border-left: 2px solid #f5c242;
  transition: all 0.3s ease;

  &.highlight {
    background: #f7b10059;
    box-shadow: 0 0 0 2px #f5c242;
    margin-left: -2px;
    margin-right: 2px;
    width: calc(100% + 2px);
  }
}

.comment {
  top: -1px;
  background: #fff;
  border: 1px solid #d0d0d0;
  border-left: 3px solid #f5c242;
  box-shadow: 0 1px 6px rgba(0, 0, 0, 0.1);
  transition: margin 0.3s ease;

  &.highlight {
    background: #f7b10059;
    box-shadow: 0 0 0 2px #f5c242, 0 2px 8px rgba(0, 0, 0, 0.15);
    margin-left: -2px;
  }

  .comment-entry {
    padding: 5px 6px;
  }

  .comment-author {
    font-weight: 600;
    color: #333;
    font-size: .75rem;
    margin-bottom: 1px;
  }

  .comment-date {
    font-size: .6rem;
    color: #999;
    margin-bottom: 5px;
  }

  .comment-text {
    font-size: .7rem;
    color: #555;
  }
}
</style>