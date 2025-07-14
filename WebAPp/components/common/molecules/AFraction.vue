<script>

defineProps({
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
        <CommonAtomsAText :text="numerator" :size="size" :color="color" class="numerator" />
        <div class="fraction-line"></div>
        <CommonAtomsAText :text="denominator" :size="size" :color="color" class="denominator" />
    </div>
</template>

<style scoped>
.fraction {
    display: inline-flex;
    flex-direction: column;
    align-items: center;
    vertical-align: middle;
}

.numerator,
.denominator {
    text-align: center;
}

.fraction-line {
    width: 100%;
    height: 1px;
    background-color: currentColor;
    margin: 2px 0;
}
</style>