<template>
  <q-layout view="hHh lpR fFf">
    <q-page-container>
      <router-view />
    </q-page-container>

    <q-footer class="bg-transparent" style="height: 12vh">
      <div id="navbody">
        <form action="#">
          <ul class="ul cyber-nav">
            
            <input
              checked=""
              name="rad"
              class="radio"
              id="choose1"
              type="radio"
            />
            <label for="choose1" @click="goHome">
              <li class="li">
                <div class="active-light"></div> <svg
                  viewBox="0 0 24 24"
                  fill="none"
                  height="24"
                  width="24"
                  xmlns="http://www.w3.org/2000/svg"
                  class="svg"
                >
                  <path
                    d="m4 12 8-8 8 8M6 10.5V19a1 1 0 0 0 1 1h3v-3a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1v3h3a1 1 0 0 0 1-1v-8.5"
                    stroke-width="2"
                    stroke-linejoin="round"
                    stroke-linecap="round"
                    stroke="currentColor"
                  ></path>
                </svg>
              </li>
            </label>

            <input class="radio" name="rad" id="choose2" type="radio" />
            <label for="choose2" @click="goSettingType">
              <li class="li">
                <div class="active-light"></div>
                <svg
                  viewBox="0 0 24 24"
                  fill="none"
                  height="24"
                  width="24"
                  xmlns="http://www.w3.org/2000/svg"
                  class="svg"
                >
                  <path
                    d="m17 21-5-4-5 4V3.889a.92.92 0 0 1 .244-.629.808.808 0 0 1 .59-.26h8.333a.81.81 0 0 1 .589.26.92.92 0 0 1 .244.63V21Z"
                    stroke-width="2"
                    stroke-linejoin="round"
                    stroke-linecap="round"
                    stroke="currentColor"
                  ></path>
                </svg>
              </li>
            </label>

            <input class="radio" name="rad" id="choose3" type="radio" />
            <label for="choose3" @click="goGraphics">
              <li class="li">
                <div class="active-light"></div>
                <svg
                  viewBox="0 0 24 24"
                  fill="none"
                  height="24"
                  width="24"
                  xmlns="http://www.w3.org/2000/svg"
                  class="svg"
                >
                  <path
                    d="m21 21-3.5-3.5M17 10a7 7 0 1 1-14 0 7 7 0 0 1 14 0Z"
                    stroke-width="2"
                    stroke-linejoin="round"
                    stroke-linecap="round"
                    stroke="currentColor"
                  ></path>
                </svg>
              </li>
            </label>

            <input class="radio" name="rad" id="choose4" type="radio" />
            <label for="choose4" @click="goUserSetting">
              <li class="li">
                <div class="active-light"></div>
                <svg
                  viewBox="0 0 24 24"
                  fill="none"
                  height="24"
                  width="24"
                  xmlns="http://www.w3.org/2000/svg"
                  class="svg"
                >
                  <path
                    d="M10 19H5a1 1 0 0 1-1-1v-1a3 3 0 0 1 3-3h2m10 1a3 3 0 0 1-3 3m3-3a3 3 0 0 0-3-3m3 3h1m-4 3a3 3 0 0 1-3-3m3 3v1m-3-4a3 3 0 0 1 3-3m-3 3h-1m4-3v-1m-2.121 1.879-.707-.707m5.656 5.656-.707-.707m-4.242 0-.707.707m5.656-5.656-.707.707M12 8a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                    stroke-width="2"
                    stroke-linejoin="round"
                    stroke-linecap="square"
                    stroke="currentColor"
                  ></path>
                </svg>
              </li>
            </label>

          </ul>
        </form>
      </div>
    </q-footer>
  </q-layout>
</template>

<style scoped>
/* 定義霓虹變數，方便統一調整 */
.cyber-nav {
  --nav-bg: rgba(5, 5, 5, 0.95);
  --nav-border: #333;
  --nav-active: #00ffcc; /* 霓虹青 */
  --nav-glow: rgba(0, 255, 204, 0.4);
  --nav-text: #555;
}

#navbody {
  height: 100%;
  background-color: transparent;
  align-items: flex-end;
  justify-content: center;
  display: flex;
  padding-bottom: 20px;
}

/* 導覽列容器 */
.ul {
  background-color: var(--nav-bg);
  width: auto;
  min-width: 340px; /* 稍微加寬 */
  
  /* Cyberpunk 風格修改重點：去圓角，加邊框 */
  border-radius: 4px; /* 極小圓角或 0 */
  border: 1px solid var(--nav-border);
  box-shadow: 0 0 20px rgba(0, 0, 0, 0.8);
  
  /* 兩側裝飾線條（模擬科技面板） */
  border-left: 2px solid var(--nav-text);
  border-right: 2px solid var(--nav-text);

  list-style: none;
  display: flex;
  justify-content: space-between;
  padding: 0 10px;
  margin: 0;
  position: relative;
  
  /* 背景網格線 */
  background-image: linear-gradient(rgba(255, 255, 255, 0.03) 1px, transparent 1px);
  background-size: 10px 10px;
}

/* 標籤容器 */
.ul .li {
  display: flex;
  flex-direction: column;
  align-items: center;
  position: relative;
  width: 70px; /* 固定寬度確保點擊範圍 */
}

.radio {
  display: none;
}

/* SVG 圖標樣式 */
.svg {
  color: var(--nav-text); /* 未選中時是暗灰色 */
  width: 40px; /* 調整大小適中 */
  height: 40px;
  opacity: 0.6;
  cursor: pointer;
  padding: 8px;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
}

/* Hover 效果 */
.ul .li .svg:hover {
  color: white;
  opacity: 1;
  filter: drop-shadow(0 0 5px white);
}

/* --- 選中狀態 (Active) --- */

/* 圖標變霓虹色 */
.radio:checked + label .li .svg {
  color: var(--nav-active);
  opacity: 1;
  transform: translateY(-5px); /* 微微上浮 */
  filter: drop-shadow(0 0 8px var(--nav-glow)); /* 強烈發光 */
}

/* 頂部亮條指示器 */
.active-light {
  width: 0%;
  height: 2px;
  background-color: var(--nav-active);
  box-shadow: 0 0 10px var(--nav-active);
  transition: width 0.3s ease;
  position: absolute;
  top: 0;
}

/* 當 radio 被選中時，亮條伸展 */
.radio:checked + label .li .active-light {
  width: 60%; /* 亮條寬度 */
}
</style>

<script setup>
import { useRouter } from "vue-router";

const router = useRouter();
const goHome = () => {
  router.push("/home");
};
const goSettingType = () => {
  router.push("/settingType");
};
const goGraphics = () => {
  router.push("/graphics");
};
const goUserSetting = () => {
  router.push("/userSetting");
};
</script>

 
