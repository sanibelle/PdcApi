<script setup lang="ts"></script>

<template>
  <div class="top-header">
    <span class="left">
      <slot name="left" />
    </span>
    <div class="right">
      <slot name="right" />
    </div>
  </div>

  <div class="obj-std-row">
    <slot name="content" />
  </div>
</template>

<style scoped>
  .top-header {
    display: flex;
    justify-content: space-between;
    align-items: baseline;
    background: #d6e6f5;
    border: 1px solid #a0c0de;
    padding: 4px 14px;
    font-weight: bold;

    .left {
      color: #3a7abf;
    }

    .right {
      font-size: 0.9em;
      font-weight: bold;
      color: #333;
    }
  }

  .obj-std-row {
    display: flex;
    justify-content: space-between;
    padding: 4px 14px;
    font-weight: bold;
    color: #3a7abf;
    font-style: italic;
  }
</style>
