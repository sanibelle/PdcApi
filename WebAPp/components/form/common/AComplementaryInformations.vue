<script setup lang="ts">
import '~/assets/css/form.css'

const emit = defineEmits(['deleteInformation']);
defineProps({
    name: {
        type: String,
        default: [],
    }
})

const model = defineModel<ComplementaryInformation[]>({
    required: true,
})

const addComplementaryInformation = () => {
    model.value.push({ text: '' });
};

const deleteComplementaryInformation = (index: number) => {
    model.value.splice(index, 1);
};
</script>

<template>
    <div class="flex">
        <slot></slot>
        <div v-if="model && model.length > 0" class="flex flex-col gap-2 ml-4">
            <div v-for="(information, index) in model" :key="index" class="flex items">
                <FormATextInput :name="`${name}.complementaryInformations[${index}].value`" :min="3" :max="5000"
                    :required="true" v-model="information.text" />
                <CommonAtomsAButton :data-testid="`delete-complementary-information-${name}-${index}`"
                    @click.prevent="() => deleteComplementaryInformation(index)" :preventDefault="true">
                    -
                </CommonAtomsAButton>
            </div>
        </div>
        <CommonAtomsAButton @click.prevent="addComplementaryInformation" :preventDefault="true"
            :data-testid="`add-complementary-information-${name}`">+</CommonAtomsAButton>
    </div>
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
