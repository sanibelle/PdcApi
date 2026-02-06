<script setup lang="ts">

const props = defineProps({
    numerator: {
        type: [String, Number],
        required: true
    },
    denominator: {
        type: [String, Number],
        required: true
    },
    size: {
        type: String,
        default: 'md'
    },
    color: {
        type: String,
        default: 'black'
    }
})
</script>

<template>
    <div class="fraction">
        <CommonAtomsAText :size="size" :color="color">{{props.numerator}}</CommonAtomsAText>
        <div class="line"></div>
        <CommonAtomsAText :size="size" :color="color">{{props.denominator}} </CommonAtomsAText>
    </div>
</template>

<style scoped>
.fraction {
    display: inline-flex;
    flex-direction: column;
    align-items: center;
    vertical-align: middle;
}

.fraction-line {
    width: 100%;
    height: 1px;
    background-color: currentColor;
    margin: 2px 0;
}
</style>