<template>
  <div class="development-container">
    <div class="card center-content">
      <q-icon name="mdi-hammer-wrench" size="80px" color="grey-5" />
      
      <h3 class="dev-title">功能開發中</h3>
      <p class="dev-subtitle">個人設定頁面正在建置中...</p>
      <p class="dev-subtitle">敬請期待！</p>
    </div>

    <!-- <button class="comic-button" @click="handleLogout">Logout</button> -->
  </div>
</template>

<script setup>
import { useRouter } from "vue-router";
import { useUserStore } from "../stores/userStore";

const router = useRouter();
const userStore = useUserStore();

// 這裡不需要 onMounted 去抓資料了，因為我們不顯示數據

const handleLogout = () => {
  userStore.logout();
  router.push("/");
};
</script>

<style scoped>
/* 讓整個區塊垂直置中，看起來比較空曠整潔 */
.development-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  min-height: 70vh; /* 佔據螢幕大部分高度 */
  gap: 30px;
  padding: 20px;
}

.card {
  background: white;
  border-radius: 16px;
  box-shadow: 0 4px 15px rgba(0,0,0,0.08); /* 稍微加深一點陰影 */
  padding: 50px 30px;
  width: 100%;
  max-width: 350px;
  text-align: center;
}

.center-content {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 20px;
}

.dev-title {
  font-size: 22px;
  font-weight: bold;
  color: #333;
  margin: 0;
}

.dev-subtitle {
  color: #999;
  margin: 0;
  font-size: 15px;
  line-height: 1.5;
}

/* 沿用你原本的按鈕樣式，確保風格一致 */
.comic-button {
  display: inline-block;
  padding: 12px 30px;
  font-size: 16px;
  font-weight: bold;
  text-align: center;
  text-decoration: none;
  color: #fff;
  background-color: #ff5252;
  border: 2px solid #000;
  border-radius: 10px;
  box-shadow: 4px 4px 0px #000;
  transition: all 0.2s ease;
  cursor: pointer;
  min-width: 120px;
}

.comic-button:hover {
  background-color: #ff1744;
  transform: translate(-1px, -1px);
  box-shadow: 5px 5px 0px #000;
}

.comic-button:active {
  transform: translate(2px, 2px);
  box-shadow: 2px 2px 0px #000;
}
</style>