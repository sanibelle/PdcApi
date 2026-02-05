<script setup lang="ts">

const props = defineProps({
    isDisabled: {
        type: Boolean,
        default: false,
    },
    preventDefault: {
        type: Boolean,
        default: false,
    },
    type: {
        type: String as PropType<'button' | 'submit' | 'reset' | undefined>,
        default: 'button',
    },
});

const emit = defineEmits(['click']);

const handleClick = (event: MouseEvent) => {
    if (props.preventDefault) {
        event.preventDefault();
    }
    emit('click', event);
};
</script>

<template>
    <button :type="type" :disabled="isDisabled" @click="handleClick">
        <slot></slot>
    </button>
</template>


<style lang="scss" scoped>
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