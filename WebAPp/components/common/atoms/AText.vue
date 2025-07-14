<script setup lang="ts">
import { computed } from 'vue'

const props = defineProps({
    tag: {
        type: String,
        default: 'span'
    },
    size: {
        type: String,
        default: 'base' // e.g., 'sm', 'base', 'lg'
    },
    color: {
        type: String,
        default: 'text-black'
    },
    weight: {
        type: String,
        default: 'normal' // e.g., 'bold', 'light'
    }
})

const computedClass = computed(() => [
    props.color,
    `text-${props.size}`,
    `font-${props.weight}`
])
</script>
<template>
    <span :class="computedClass">
        <slot />
    </span>
</template>


<style scoped>
/* Example utility classes, adjust as needed */
.text-sm {
    font-size: 0.875rem;
}

.text-base {
    font-size: 1rem;
}

.text-lg {
    font-size: 1.125rem;
}

.font-normal {
    font-weight: 400;
}

.font-bold {
    font-weight: 700;
}

.text-black {
    color: #000;
}
</style>