<script setup lang="ts">
const { t } = useI18n();

defineI18nRoute({
  paths: {
    fr: `/gestion/utilisateurs`,
  },
});

const { fetchUsers, fetchRoles, updateUserRoles } = useUserClient();
const users = ref<User[]>();
const roles = ref<{ name: string, selected: boolean }[]>([]);
const rolesFilter = ref<string>('');
const usersFilter = ref<string>('');
const selectedUser = ref<User | null>(null);

onMounted(async () => {
  try {
    users.value = await fetchUsers();
    const fetchedRoles = await fetchRoles();
    roles.value = fetchedRoles.map(role => ({ name: role, selected: false }));
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
  roles.value = roles.value?.map(x => ({ name: x.name, selected: user.roles.includes(x.name) }));
};

const isRoleAdded = (role: string, isRoleSelected: boolean): boolean => {
  if (!selectedUser.value) return false;
  return !selectedUser.value.roles.includes(role) && isRoleSelected;
};

const isRoleRemoved = (role: string, isRoleSelected: boolean): boolean => {
  if (!selectedUser.value) return false;
  return selectedUser.value?.roles.includes(role) && !isRoleSelected;
};

const updatedRoles = async () => {
  if (!selectedUser.value || !users.value) return;
  try {
    const updatedUser = await updateUserRoles(selectedUser.value.id, roles.value?.filter(x => x.selected).map(x => x.name) || []);
    users.value = users.value.map(x => x.id === updatedUser.id ? updatedUser : x);
    selectedUser.value = null;
    roles.value = roles.value?.map(x => ({ name: x.name, selected: false }));
  } catch (e) {
    // TODO manage that
    console.error(e);
    alert("Erreur lors de la mise à jour des rôles");
  }
};

const handleRoleRowClick = (role: string) => {
  const roleEntry = roles.value?.find(x => x.name === role);
  if (roleEntry) {
    roleEntry.selected = !roleEntry.selected;
  }
};

const userClasses = (user: User) => {
  let classes = '';
  if (!user.userName.toLowerCase().includes(usersFilter.value.toLowerCase())) {
    return 'hidden';
  }
  return classes;
};

const roleClasses = (role: { name: string, selected: boolean }) => {
  let classes = '';
  if (selectedUser.value) {
    classes = 'item-editable ';
  }
  if (isRoleAdded(role.name, role.selected)) {
    classes += 'role-added ';
  }
  else if (isRoleRemoved(role.name, role.selected)) {
    classes += 'role-removed';
  }
  else if (role.selected) {
    classes += 'role-active';
  }
  else if (!role.name.toLowerCase().includes(rolesFilter.value.toLowerCase())) {
    return 'hidden';
  }
  return classes;
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
          <h2 class="panel-title">{{ t('users') }}</h2>
          <FormATextInput name="usersFilter" placeholder="🔍" v-model.lazy="usersFilter" :disabled="!!selectedUser" />
        </div>
        <div class="panel-content">
          <template v-if="!selectedUser">
            <div v-for="(user, index) in users" :data-testid="`user-${index}`" :key="user.id"
              :class="`${userClasses(user)} item`" @click="manageUser(user)" @mouseenter="setSelectedRolesOfUser(user)"
              @mouseleave="roles = roles.map(x => ({ name: x.name, selected: false }))">
              {{ user.userName }}
            </div>
          </template>
          <div v-else class="item flex justify-between items-center">
            <span>
              {{ selectedUser.userName }}
            </span>
            <CommonAtomsAButton class="ok-button" @click="updatedRoles">OK</CommonAtomsAButton>
            <CommonAtomsAButton class="cancel-button" @click="selectedUser = null">X</CommonAtomsAButton>
          </div>
        </div>
      </div>

      <div class="roles-panel">
        <div class="panel-header">
          <h2 class="panel-title">{{ t('roles') }}</h2>
          <FormATextInput name="rolesFilter" placeholder="🔍" v-model.lazy="rolesFilter" />
        </div>
        <div class="panel-content" data-testid="roles-container">
          <div v-for="(role, index) in roles" :data-testid="`role-${index}`" :key="role.name"
            :class="`${roleClasses(role)} item`" @click="handleRoleRowClick(role.name)">
            <span>
              {{ role.name }}
            </span>
            <FormACheckboxInput v-if="selectedUser" :data-testid="`role-checkbox-${index}`" name="role"
              v-model="role.selected" />
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
    "action": "Actions",
    "users": "Utilisateurs",
    "roles": "Rôles"
  }
}</i18n>

<style scoped>
:deep(.ok-button) {
  @apply bg-green-600 hover:bg-green-700 text-white font-semibold px-4 py-2 rounded-md;
}

:deep(.cancel-button) {
  @apply bg-red-600 hover:bg-red-700 text-white font-semibold px-4 py-2 rounded-md;
}

.item-editable {
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
  @apply bg-white rounded-lg shadow-md;
}

.roles-panel {
  @apply bg-white rounded-lg shadow-md;
}

.panel-header {
  @apply bg-gradient-to-r from-blue-600 to-blue-700 px-6 py-4 flex items-center justify-between;

  h2 {
    @apply text-white text-lg font-semibold h-10;
  }
}

.panel-title {
  @apply text-xl font-semibold text-white;
}

.panel-content {
  @apply p-6 space-y-3;
}

.item {
  @apply px-4 py-3 bg-gray-50 rounded-md border border-gray-200 hover:bg-gray-100 flex justify-between items-center;
  transition: height 0.7s ease, padding 0.7s ease, margin 0.7s ease, border-width 0.7s ease, opacity 0.7s ease;
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

.hidden {
  @apply h-0 p-0 m-0 border-0 overflow-hidden opacity-0;
  margin-top: 0 !important;
}
</style>