<script setup lang="ts">
  import type { PropType } from 'vue';

  const props = defineProps({
    slug: {
      type: String,
      required: true,
    },
    displayOnlyWhenAuthenticated: {
      type: Boolean,
      default: false,
    },
    items: {
      type: Array<MenuItem>,
      default: null,
    },
    selectedItem: {
      type: String as PropType<string | null>,
      default: null,
    },
  });

  const searchQuery = ref('');
  const filteredItems = computed(() => filterText(props.items ?? [], searchQuery.value, ['name']));

  const authStore = useAuthStore();
  onMounted(async () => {
    await authStore.authenticate();
  });

  const emit = defineEmits(['onSelectItemClick', 'onRemoveItemClick']);
</script>
<template>
  <div
    v-if="!props.displayOnlyWhenAuthenticated || authStore.isAuthenticated"
    class="wrapper"
  >
    <LayoutMoleculesSectionHeader :slug="props.slug">
      <LayoutAtomsSectionHeaderTitle
        v-if="selectedItem"
        class="selected-item"
        :slug="`${selectedItem} X`"
        @click="emit('onRemoveItemClick')"
      />
    </LayoutMoleculesSectionHeader>
    <slot>
      <FormTemplatesASearchInput
        v-if="items && !selectedItem"
        v-model.lazy="searchQuery"
        name="searchQuery"
      />
      <div
        v-if="items && !selectedItem"
        class="list-container"
      >
        <ul
          v-for="item in filteredItems"
          :key="item.id"
        >
          <li @click="emit('onSelectItemClick', item)">
            <button>{{ item.shortName }} - {{ item.name }}</button>
          </li>
        </ul>
      </div>
    </slot>
  </div>
</template>

<style scoped lang="scss">
  .wrapper {
    background-color: #111;
    border-radius: 10px 10px 0 0;
    display: flex;
    width: 100%;
  }

  .selected-item {
    font-size: 0.75rem;
    background-color: cyan;
    font-weight: bold;
    cursor: pointer;
  }

  .list-container {
    width: 100%;
  }
  .list-container ul {
    list-style: none;
    background-color: #fff;
    padding: 0;
    margin: 0;
    flex-direction: column;
    display: flex;
    align-items: center;
    justify-content: start;
    li {
      padding: 0.5rem;
      width: 100%;
      &:hover {
        background-color: #aaa;
        cursor: pointer;
      }
    }
  }
</style>
