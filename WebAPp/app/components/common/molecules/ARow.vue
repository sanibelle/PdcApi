<script setup lang="ts">

</script>

<template>
    <div class="panel-row">
        <div class="col col-left">
            <slot name="col-left"></slot>
        </div>
        <div class="col col-right">
            <slot name="col-right"></slot>
        </div>
    </div>
</template>

<style scoped>
.panel-row {
    display: flex;
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