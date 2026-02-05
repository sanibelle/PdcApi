<script setup lang="ts">
const { t } = useI18n();


const props = defineProps({
    complementaryInformations: {
        type: Array as PropType<ComplementaryInformation[]>,
        default: () => [],
    }
});
</script>

<template>
    <div v-if="complementaryInformations.length > 0" class="mt-4 space-y-2">
        <div v-for="(complementaryInformation, index) in complementaryInformations" :key="index" class="p-4 border border-gray-300 rounded-md bg-gray-50">
            <div class="mt-2 text-gray-800" v-html="complementaryInformation.text"></div>
            TODO : À Finir en vue
        </div>
    </div>
</template>


<i18n>
{
    "fr": {
        "draft": "Cette version est en cours de rédaction.",
        "notv1": "Tous les changements effectués à cette version seront suivis et auront un impact dans tous les documents qui y sont liés.",
        "notdraft": "Cette version est approuvée. Seules les modifications mineures sont possibles (ex : corriger les fautes de frappe).",
        "newVersion": "Pour effectuer des modifications majeures, veuillez créer une nouvelle version.",
    }
}
</i18n>

<style scoped>
.version-badge {
    display: inline-flex;
    align-items: center;
    gap: 0.25rem;
    padding: 0.25rem 0.5rem;
    border-radius: 4px;
    background-color: #e5e7eb;
    color: #374151;
    font-size: 0.875rem;
    font-weight: 500;
}

.version-badge.draft {
    background-color: #fed7aa;
    color: #ea580c;
}

.version-text {
    font-weight: 600;
}

.draft-label {
    font-size: 0.6rem;
    text-transform: uppercase;
    font-style: italic;
    letter-spacing: 0.05em;
}
</style>