<template>
  <div class="development-container">
    
    <div class="bg-deco">SYSTEM_DEV // SYSTEM_DEV // SYSTEM_DEV</div>

    <div class="cyber-card center-content">
      <div class="corner top-left"></div>
      <div class="corner top-right"></div>
      <div class="corner bottom-left"></div>
      <div class="corner bottom-right"></div>

      <div class="icon-wrapper">
        <q-icon name="engineering" size="80px" class="alert-icon" />
        <div class="icon-ring"></div>
      </div>
      
      <h3 class="dev-title glitch" data-text="SYSTEM_MAINTENANCE">
        SYSTEM_MAINTENANCE
      </h3>
      
      <div class="text-content">
        <p class="dev-subtitle">>> MODULE_UNDER_CONSTRUCTION...</p>
        <p class="dev-subtitle"> COMING_SOON</p>
      </div>

      <button class="cyber-btn-logout" @click="handleLogout">
        [LOGOUT]
      </button>
    </div>

  </div>
</template>

<script setup>
import { useRouter } from "vue-router";
import { useUserStore } from "../stores/userStore";

const router = useRouter();
const userStore = useUserStore();

const handleLogout = () => {
  userStore.logout();
  router.push("/login"); // 通常登出是回登入頁，不是回首頁(/)，看你路由設定
};
</script>

<style scoped>
/* 局部變數 */
.development-container {
  --neon-color: #00ffcc;
  --alert-color: #ff0055; /* 警示紅 */
  --bg-color: #050505;
  
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  min-height: 80vh; /* 拉高一點 */
  position: relative;
  overflow: hidden;
}

/* 背景裝飾大字 */
.bg-deco {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%) rotate(-15deg);
  font-size: 4rem;
  font-weight: 900;
  color: rgba(255, 255, 255, 0.03);
  white-space: nowrap;
  pointer-events: none;
  font-family: 'Share Tech Mono', monospace;
}

/* 主卡片 */
.cyber-card {
  background: rgba(10, 15, 20, 0.9);
  border: 1px solid var(--alert-color); /* 改用警示紅邊框 */
  padding: 50px 30px;
  width: 90%;
  max-width: 400px;
  text-align: center;
  position: relative;
  box-shadow: 0 0 30px rgba(255, 0, 85, 0.2);
  
  /* 背景斜線 */
  background-image: repeating-linear-gradient(
    45deg,
    rgba(255, 0, 85, 0.05),
    rgba(255, 0, 85, 0.05) 10px,
    transparent 10px,
    transparent 20px
  );
}

.center-content {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 25px;
}

/* 圖示動畫區 */
.icon-wrapper {
  position: relative;
  display: flex;
  justify-content: center;
  align-items: center;
  width: 100px;
  height: 100px;
}

.alert-icon {
  color: var(--alert-color);
  animation: pulse 2s infinite;
  z-index: 2;
}

.icon-ring {
  position: absolute;
  width: 100%;
  height: 100%;
  border: 2px dashed var(--alert-color);
  border-radius: 50%;
  animation: spin 10s linear infinite;
  opacity: 0.5;
}

/* 文字樣式 */
.dev-title {
  font-family: 'Share Tech Mono', monospace;
  font-size: 1.5rem;
  font-weight: bold;
  color: var(--alert-color);
  margin: 0;
  letter-spacing: 2px;
  position: relative;
}

.dev-subtitle {
  color: #888;
  margin: 5px 0;
  font-size: 0.9rem;
  font-family: 'Share Tech Mono', monospace;
  letter-spacing: 1px;
}

/* 按鈕樣式 */
.cyber-btn-logout {
  margin-top: 20px;
  background: transparent;
  border: 1px solid var(--neon-color);
  color: var(--neon-color);
  padding: 12px 25px;
  font-family: 'Share Tech Mono', monospace;
  font-size: 1rem;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s;
  text-transform: uppercase;
  letter-spacing: 1px;
}

.cyber-btn-logout:hover {
  background: var(--neon-color);
  color: #000;
  box-shadow: 0 0 20px var(--neon-color);
}

/* --- 角落裝飾 --- */
.corner {
  position: absolute;
  width: 10px;
  height: 10px;
  border-color: var(--alert-color);
  border-style: solid;
}
.top-left { top: -1px; left: -1px; border-width: 2px 0 0 2px; }
.top-right { top: -1px; right: -1px; border-width: 2px 2px 0 0; }
.bottom-left { bottom: -1px; left: -1px; border-width: 0 0 2px 2px; }
.bottom-right { bottom: -1px; right: -1px; border-width: 0 2px 2px 0; }

/* --- 動畫 --- */
@keyframes pulse {
  0% { opacity: 1; text-shadow: 0 0 10px var(--alert-color); }
  50% { opacity: 0.5; text-shadow: none; }
  100% { opacity: 1; text-shadow: 0 0 10px var(--alert-color); }
}

@keyframes spin {
  from { transform: rotate(0deg); }
  to { transform: rotate(360deg); }
}

/* 簡單的故障文字效果 (Glitch) */
.glitch::before,
.glitch::after {
  content: attr(data-text);
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(10, 15, 20, 0.9);
}

.glitch::before {
  left: 2px;
  text-shadow: -1px 0 #00ffcc;
  clip: rect(24px, 550px, 90px, 0);
  animation: glitch-anim-2 3s infinite linear alternate-reverse;
}

.glitch::after {
  left: -2px;
  text-shadow: -1px 0 #ff0055;
  clip: rect(85px, 550px, 140px, 0);
  animation: glitch-anim 2.5s infinite linear alternate-reverse;
}

@keyframes glitch-anim {
  0% { clip: rect(10px, 9999px, 30px, 0); }
  20% { clip: rect(80px, 9999px, 100px, 0); }
  40% { clip: rect(20px, 9999px, 60px, 0); }
  60% { clip: rect(40px, 9999px, 80px, 0); }
  80% { clip: rect(90px, 9999px, 20px, 0); }
  100% { clip: rect(10px, 9999px, 50px, 0); }
}
</style>