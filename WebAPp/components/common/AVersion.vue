<script setup lang="ts">
const { t } = useI18n();

const props = defineProps({
    versionNumber: {
        type: Number,
        required: true,
    },
    isDraft: {
        type: Boolean,
        default: false,
    },
});
</script>

<template>
    <div class="version-badge" :class="{ 'draft': isDraft }">
        <span class="version-text">v{{ versionNumber }}</span>
        <span v-if="isDraft" class="draft-label">{{ t('draft') }}
            <br>
            <template v-if="versionNumber !== 1">{{ t('notv1') }}</template>
            -    <div class="version-badge" :class="{ 'draft': isDraft }">
        </span>
        <span v-else class="draft-label">{{ t('notdraft') }}
            <br>
            {{ t('newVersion') }}
        </span>

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