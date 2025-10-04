<script setup lang="ts">
import '~/assets/css/form.css'

const emit = defineEmits(['deleteRow']);
defineProps({
    index: {
        type: Number,
        required: true,
    }
})

const model = defineModel<CompetencyElement>({
    required: true,
})
</script>

<template>
    <FormATextInput :name="`competency.competencyElements[${index}].value`" :min="3" :max="100" :required="true"
        v-model="model.value" />
    <CommonAtomsAButton @click.prevent="() => emit('deleteRow', index)" :preventDefault="true">-
    </CommonAtomsAButton>
</template>


<style scoped>
.form {
    padding: 0.5rem;
}

.top-row {
    display: flex;
    gap: 1rem;
    background-color: #10b981;
    /* emerald-500 */
    padding: 1rem;
    border-radius: 8px;
    margin-bottom: 1.5rem;
}

.top-row>* {
    flex: 1;
}

.row {
    display: flex;
    gap: 1rem;
}

.buttons {
    display: flex;
    gap: 1rem;
    justify-content: flex-end;
    margin-top: 1.5rem;
}
</style>
