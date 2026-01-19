<script setup lang="ts">
const { t } = useI18n();
const route = useRoute();

defineI18nRoute({
  paths: {
    fr: `/administration/programme/[programCode]`,
  },
});

const { fetchUsers, fetchRoles, updateUserRoles } = useUserClient();
const users = ref<User[]>();
const selectedRoles = ref<{ name: string, selected: boolean }[]>([]);
const roles = ref<string[]>();
const selectedUser = ref<User | null>(null);

onMounted(async () => {
  try {
    users.value = await fetchUsers();
    roles.value = await fetchRoles();

  } catch (e) {
    console.error(e);
    alert("ERREUR")
  }
});

const manageUser = (user: User) => {
  selectedUser.value = user;
  setSelectedRolesOfUser(user);
};

const setSelectedRolesOfUser = (user: User) => {
  selectedRoles.value = roles.value?.map(x => ({ name: x, selected: user.roles.includes(x) })) as { name: string, selected: boolean }[];
};

const isRoleAdded = (role: string, isRoleSelected: boolean): boolean => {
  return !!!selectedUser.value?.roles.includes(role) && isRoleSelected;
};

const isRoleRemoved = (role: string, isRoleSelected: boolean): boolean => {
  return !!selectedUser.value?.roles.includes(role) && !isRoleSelected;
};

const updatedRoles = async () => {
  if (!selectedUser.value) return;
  try {
    const updatedUser = await updateUserRoles(selectedUser.value.id, selectedRoles.value.filter(x => x.selected).map(x => x.name));
    users.value = users.value!.map(x => x.id === updatedUser.id ? updatedUser : x);
    selectedUser.value = null;
    selectedRoles.value = [];
  } catch (e) {
    // TODO manage that
    console.error(e);
    alert("Erreur lors de la mise à jour des rôles");
  }
};

</script>

<template>
  <div class="admin-container">
    <h1 class="admin-title">
      {{ t('title') }}
    </h1>

    <section class="admin-board">
      <div class="users-panel">
        <div class="panel-header">
          <h2 class="panel-title">Utilisateurs</h2>
        </div>
        <div class="panel-content">
          <div v-if="!selectedUser" v-for="user in users" :key="user.id" class="user-item" @click="manageUser(user)"
            @mouseenter="setSelectedRolesOfUser(user)" @mouseleave="selectedRoles = []">
            {{ user.userName }}
          </div>
          <div v-else class="user-item flex justify-between items-center" @click="selectedUser = null">
            <span>
              {{ selectedUser.userName }}
            </span>
            <CommonAtomsAButton @click="selectedUser = null">X</CommonAtomsAButton>
            <CommonAtomsAButton @click="updatedRoles">OK</CommonAtomsAButton>
          </div>
        </div>
      </div>

      <div class="roles-panel">
        <div class="panel-header">
          <h2 class="panel-title">Rôles</h2>
        </div>
        <div class="panel-content">
          <div v-for="(role, index) in roles" :key="role" class="role-item" :class="{
            'role-item-editable':
              selectedUser
          }" @click="selectedRoles[index] && (selectedRoles[index].selected = !selectedRoles[index].selected)">
            <span
              :class="{ 'role-active': selectedRoles.some(x => x.name === role && x.selected), 'role-added': isRoleAdded(role, !!selectedRoles[index]?.selected), 'role-removed': isRoleRemoved(role, !!selectedRoles[index]?.selected) }">
              {{ role }}
            </span>
            <FormACheckboxInput v-if="selectedUser && selectedRoles[index]" name="role"
              v-model="selectedRoles[index].selected" />
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<i18n lang="json">{
  "fr": {
    "title": "Gestion des utilisateurs",
    "loading": "Chargement...",
    "userNotFound": "Utilisateur non trouvé",
    "action": "Actions"
  }
}</i18n>

<style scoped>
.role-item-editable {
  @apply cursor-pointer;
}

.admin-container {
  @apply max-w-7xl mx-auto px-4 py-8;
}

.admin-title {
  @apply text-3xl font-bold text-gray-900 mb-8;
}

.admin-board {
  @apply grid grid-cols-2 gap-6;
}

.users-panel {
  @apply bg-white rounded-lg shadow-md overflow-hidden;
}

.roles-panel {
  @apply bg-white rounded-lg shadow-md overflow-hidden;
}

.panel-header {
  @apply bg-gradient-to-r from-blue-600 to-blue-700 px-6 py-4;
}

.panel-title {
  @apply text-xl font-semibold text-white;
}

.panel-content {
  @apply p-6 space-y-3;
}

.user-item {
  @apply px-4 py-3 bg-gray-50 rounded-md border border-gray-200 hover:bg-gray-100 transition-colors cursor-pointer;
}

.role-item {
  @apply px-4 py-3 bg-gray-50 rounded-md border border-gray-200 hover:bg-gray-100 transition-colors flex justify-between items-center;
}

.role-active {
  @apply font-bold text-blue-600;
}

.role-added {
  @apply font-bold text-green-600;
}

.role-removed {
  @apply font-bold text-red-600;
}
</style>