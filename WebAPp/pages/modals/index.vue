<template>
  <div class="container">
    <h1>Nuxt 3 Modal Example</h1>

    <button @click="showBasicModal = true" class="btn">Open Basic Modal</button>
    <button @click="showCustomModal = true" class="btn">Open Custom Modal</button>
    <button @click="showFormModal = true" class="btn">Open Form Modal</button>

    <!-- Basic Modal -->
    <Modal v-model="showBasicModal" title="Basic Modal">
      <p>This is a basic modal with default header and footer.</p>
    </Modal>

    <!-- Custom Modal -->
    <Modal v-model="showCustomModal" @confirm="handleConfirm">
      <template #header>
        <h2 style="color: #4caf50">Custom Header</h2>
      </template>

      <div>
        <p>This modal has a custom header and footer.</p>
        <p>You can put any content here.</p>
      </div>

      <template #footer>
        <button class="btn-secondary" @click="showCustomModal = false">Close</button>
        <button class="btn-primary" @click="handleConfirm">Save Changes</button>
      </template>
    </Modal>

    <!-- Form Modal -->
    <Modal v-model="showFormModal" title="Form Example">
      <form @submit.prevent="submitForm">
        <div class="form-group">
          <label for="name">Name:</label>
          <input type="text" id="name" v-model="formData.name" required />
        </div>
        <div class="form-group">
          <label for="email">Email:</label>
          <input type="email" id="email" v-model="formData.email" required />
        </div>
      </form>

      <template #footer>
        <button class="btn-secondary" @click="showFormModal = false">Cancel</button>
        <button class="btn-primary" @click="submitForm">Submit</button>
      </template>
    </Modal>
  </div>
</template>

<script setup>
import { ref } from 'vue';

// Modal visibility states
const showBasicModal = ref(false);
const showCustomModal = ref(false);
const showFormModal = ref(false);

// Form data
const formData = ref({
  name: '',
  email: '',
});

// Custom modal confirm handler
const handleConfirm = () => {
  console.log('Custom modal confirmed!');
};

// Form submission handler
const submitForm = () => {
  console.log('Form submitted:', formData.value);
  showFormModal.value = false;
};
</script>

<style scoped>
.container {
  max-width: 800px;
  margin: 0 auto;
  padding: 20px;
}

.btn {
  background-color: #4caf50;
  color: white;
  border: none;
  padding: 10px 15px;
  margin: 5px;
  border-radius: 4px;
  cursor: pointer;
}

.btn-primary {
  background-color: #2196f3;
  color: white;
  border: none;
  padding: 8px 16px;
  border-radius: 4px;
  cursor: pointer;
}

.btn-secondary {
  background-color: #f2f2f2;
  color: #333;
  border: none;
  padding: 8px 16px;
  border-radius: 4px;
  cursor: pointer;
}

.form-group {
  margin-bottom: 15px;
}

.form-group label {
  display: block;
  margin-bottom: 5px;
}

.form-group input {
  width: 100%;
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
}
</style>
