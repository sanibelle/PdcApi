<script setup lang="ts">
import '~/assets/css/form.css'

const { t } = useI18n();
defineProps({
  title: {
    type: String,
    default: 'Modal',
  },
  closeOnOverlayClick: {
    type: Boolean,
    default: false,
  },
  hideFooter: {
    type: Boolean,
    default: false,
  },
});

const model = defineModel<boolean>({
  required: true,
});

const emit = defineEmits(['confirm']);

const confirm = () => {
  emit('confirm');
  model.value = false;
};
</script>

<template>
  <Teleport to="body">
    <Transition name="modal">
      <div v-if="model" class="modal-overlay" @click="model = !closeOnOverlayClick">
        <div class="modal" @click.stop>
          <div class="modal-header">
            <slot name="header">
              <h3>{{ title }}</h3>
            </slot>
            <button class="close-button" @click="model = false">×</button>
          </div>
          <div class="modal-body">
            <slot />
          </div>
          <div class="modal-footer" v-if="!hideFooter">
            <slot name="footer">
              <button class="modal-button cancel" @click="model = false">
                {{ t('cancel') }}
              </button>
              <button class="modal-button confirm" @click="confirm"> {{ t('confirm') }}
              </button>
            </slot>
          </div>
        </div>
      </div>
    </Transition>
  </Teleport>
</template>

<i18n>{
  "fr": {
    "cancel": "Annuler",
    "confirm": "Confirmer"
  }
}</i18n>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0,
      0,
      0,
      0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 999;
}

.modal {
  background-color: white;
  border-radius: 8px;
  width: 90%;
  max-width: 500px;
  max-height: 90vh;
  overflow-y: auto;
  box-shadow: 0 4px 6px rgba(0,
      0,
      0,
      0.1);
}

.modal-header {
  padding: 15px 20px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1px solid #eee;
}

.modal-body {
  padding: 20px;
}

.close-button {
  transition:
    transform 0.2s,
    color 0.2s,
    box-shadow 0.2s;
  border-radius: 50%;
  width: 26px;
  height: 26px;
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: 0 2px 8px rgba(0,
      0,
      0,
      0.08);
  background: linear-gradient(135deg, #f8fafc 60%, #e0e7ef 100%);
}

.close-button:hover {
  color: #e53935;
  background: linear-gradient(135deg, #ffeaea 60%, #ffd6d6 100%);
  transform: scale(1.15) rotate(12deg);
  box-shadow: 0 4px 16px rgba(229,
      57,
      53,
      0.15);
}



button:hover {
  transition: background-color 0.2s;
  filter: brightness(0.9);
}

.cancel {
  background-color: #f2f2f2;
  color: #333;
}

.confirm {
  background-color: #4caf50;
  color: white;
}

/* Animation transitions */
.modal-enter-active,
.modal-leave-active {
  transition: opacity 0.3s ease;
}

.modal-enter-from,
.modal-leave-to {
  opacity: 0;
}
</style>
