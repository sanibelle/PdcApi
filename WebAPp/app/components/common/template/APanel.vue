<template>
  <div class="panel">
    <div class="panel-header">
      <div class="col col-left"><slot name="header-col-left" /></div>
      <div class="col col-right"><slot name="header-col-right" /></div>
    </div>
    <div class="panel-row">
      <slot name="content" />
    </div>
  </div>
</template>

<style lang="scss" scoped>
  .panel {
    border: 1px solid #bbb;
    overflow: visible;
  }

  .panel-header {
    display: flex;
    background: linear-gradient(180deg, #6aaed6 0%, #3a7abf 100%);
    color: #fff;
    font-weight: bold;

    .col {
      padding: 0.5em 0.5em;
    }

    .col-left {
      width: 45%;
    }

    .col-right {
      width: 55%;
    }
  }

  .panel-row {
    display: flex;
    flex-direction: column;
    border-top: 1px solid #bbb;

    .col {
      padding: 8px 12px;
    }

    .col-left {
      width: 45%;
      font-weight: bold;
      font-size: 13px;
    }

    .col-right {
      width: 55%;
      font-size: 13px;
      display: flex;
      flex-direction: column;
      gap: 6px;
    }
  }
</style>
