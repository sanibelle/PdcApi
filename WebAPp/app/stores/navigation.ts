export const useNavigationStore = defineStore(
  'navigation',
  () => {
    const programCode = ref<string | null>(null);
    const courseFrameworkCode = ref<string | null>(null);

    const setProgramCode = (code: string | null) => {
      programCode.value = code;
    };

    const setCourseFrameworkCode = (code: string | null) => {
      courseFrameworkCode.value = code;
    };

    return {
      programCode,
      courseFrameworkCode,
      setProgramCode,
      setCourseFrameworkCode,
    };
  },
  {
    persist: {
      storage: piniaPluginPersistedstate.sessionStorage(),
    },
  },
);
