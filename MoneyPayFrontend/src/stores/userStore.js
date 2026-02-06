// src/stores/userStore.js
import { defineStore } from 'pinia';
import { MOCK_USER_STATS } from '../api/mock/userData';
import { useRouter } from 'vue-router'; // 為了在 store 裡做轉址

export const useUserStore = defineStore('user', {
  state: () => ({
    name: '未登入',
    token: null,
    stats: {
      recordContinuous: 0,
      recordDaysCount: 0,
      recordCount: 0
    },
    goals: {
      savingGoal: 0,
      expenseLimit: 0
    }
  }),

  actions: {
    // 初始化使用者資料
    initUser() {
      // 1. 模擬從 localStorage 讀取基本資料
      const storedName = localStorage.getItem('name');
      const storedToken = localStorage.getItem('token');
      
      if (storedName) this.name = storedName;
      if (storedToken) this.token = storedToken;

      // 2. 載入 Mock 統計數據 (模擬 API 呼叫)
      // 如果你是要完全模擬「第一次載入」，可以加個 if 判斷 stats 是否為 0
      this.stats = {
        recordContinuous: MOCK_USER_STATS.recordContinuous,
        recordDaysCount: MOCK_USER_STATS.recordDaysCount,
        recordCount: MOCK_USER_STATS.recordCount
      };
      
      // 載入目標設定 (Mock)
      this.goals = MOCK_USER_STATS.settings;
    },

    // 登出功能
    logout() {
      // 清除 Pinia 狀態
      this.name = '未登入';
      this.token = null;
      this.stats = { recordContinuous: 0, recordDaysCount: 0, recordCount: 0 };
      
      // 清除 LocalStorage
      localStorage.removeItem("token");
      localStorage.removeItem("name");
      localStorage.removeItem("userEmail");
      
      // 如果需要在這裡跳轉，可以讓 Component 處理，或者這裡引入 router
      // 為了簡單起見，我們通常讓 Component 呼叫 logout 後自己 router.push
    }
  },
  
  persist: true // 開啟持久化，重新整理後名字才不會不見
});