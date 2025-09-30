<template>
  <div
    class="q-pa-md flex flex-center app-container"
    style="height: 100vh; width: 100vw; flex-direction: column"
  >
    <div class="radio-inputs">
      <label class="radio">
        <input type="radio" name="radio" value="login" v-model="activeTab" />
        <span class="name">Login</span>
      </label>

      <label class="radio">
        <input type="radio" name="radio" value="regidter" v-model="activeTab" />
        <span class="name">Register</span>
      </label>
    </div>

    <!-- Tab -->
    <div
      style="
        width: 100%;
        margin-top: 20px;
        display: flex;
        justify-content: center;
      "
    >
      <LoginForm v-if="activeTab === 'login'" />
      <RegisterForm v-else />
    </div>
  </div>
</template>
<script setup>
import LoginForm from "../components/forms/LoginForm.vue";
import RegisterForm from "../components/forms/RegisterForm.vue";
import { ref } from "vue";

const activeTab = ref("login"); // 預設顯示 Login
</script>
<style scoped>
.radio-inputs {
  position: relative;
  display: flex;
  flex-wrap: wrap;
  border-radius: 0.5rem;
  background-color: #eee;
  box-sizing: border-box;
  box-shadow: 0 0 0px 1px rgba(0, 0, 0, 0.06);
  padding: 0.25rem;
  max-width: 400px;
  width: 90%;

  font-size: 14px;
}

.radio-inputs .radio {
  flex: 1 1 auto;
  text-align: center;
}

.radio-inputs .radio input {
  display: none;
}

.radio-inputs .radio .name {
  display: flex;
  cursor: pointer;
  align-items: center;
  justify-content: center;
  border-radius: 0.5rem;
  border: none;
  padding: 0.5rem 0;
  color: rgba(51, 65, 85, 1);
  transition: all 0.15s ease-in-out;
}

.radio-inputs .radio input:checked + .name {
  background-color: #fff;
  font-weight: 600;
}
</style>
