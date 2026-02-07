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
  /* 定義局部變數，確保獨立運作 */
  --cyber-neon: #00ffcc;
  --cyber-dark: #0a0a0a;
  --cyber-dim: #555;

  position: relative;
  display: flex;
  flex-wrap: wrap;
  /* 去圓角 */
  border-radius: 0; 
  /* 改黑底 */
  background-color: var(--cyber-dark); 
  box-sizing: border-box;
  /* 加上深色邊框 */
  border: 1px solid #333;
  /* 科技感陰影 */
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.8);
  padding: 4px;
  max-width: 400px;
  width: 90%;
  font-size: 14px;
  
  /* 增加一點背景紋路 */
  background-image: linear-gradient(rgba(255, 255, 255, 0.02) 1px, transparent 1px);
  background-size: 100% 4px;
}

.radio-inputs .radio {
  flex: 1 1 auto;
  text-align: center;
  margin: 0 2px; /* 按鈕間留一點縫隙 */
}

.radio-inputs .radio input {
  display: none;
}

.radio-inputs .radio .name {
  display: flex;
  cursor: pointer;
  align-items: center;
  justify-content: center;
  /* 直角 */
  border-radius: 0; 
  border: 1px solid transparent; /* 預留邊框位置 */
  padding: 0.8rem 0;
  
  /* 未選中時的顏色 */
  color: var(--cyber-dim);
  font-family: 'Share Tech Mono', monospace; /* 確保字體 */
  font-weight: bold;
  letter-spacing: 2px;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
}

/* 滑鼠懸停效果 */
.radio-inputs .radio .name:hover {
  color: #aaa;
  background-color: rgba(255, 255, 255, 0.03);
}

/* 選中狀態：發光、霓虹邊框 */
.radio-inputs .radio input:checked + .name {
  background-color: rgba(0, 255, 204, 0.1); /* 微弱背景光 */
  color: var(--cyber-neon); /* 霓虹字 */
  border-color: var(--cyber-neon); /* 亮邊框 */
  box-shadow: 0 0 10px rgba(0, 255, 204, 0.3), inset 0 0 5px rgba(0, 255, 204, 0.1);
  text-shadow: 0 0 8px var(--cyber-neon);
}
</style>
