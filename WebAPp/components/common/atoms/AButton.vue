<script setup lang="ts">
defineProps({
    isDisabled: {
        type: Boolean,
        default: false,
    }
});

defineEmits(['click']);

</script>
<template>
    <button :disabled="isDisabled" @click="$emit('click')">
        <slot></slot>
    </button>
</template>


<style scoped>
/* Example utility classes, adjust as needed */
button {
    background-color: #4caf50;
    /* Green */
}

button {
    margin: 4px 2px;
    font-size: 16px;
    padding: 8px 16px;
    border: none;
    border-radius: 4px;
    text-align: center;
    text-decoration: none;
    background-color: #3b82f6;
    color: white;
    font-weight: 500;
    cursor: pointer;
    transition: background-color 0.2s ease;

    &:hover {
        background-color: #2563eb;
    }

    &:disabled {
        background-color: #93c5fd;
        cursor: not-allowed;
    }
}
</style>