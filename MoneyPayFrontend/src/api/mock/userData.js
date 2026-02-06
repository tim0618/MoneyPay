// src/api/mock/userData.js

export const MOCK_USER_STATS = {
  recordContinuous: 15,    // 連續記帳天數
  recordDaysCount: 180,    // 總記帳天數
  recordCount: 542,        // 總筆數
  
  // 順便預留設定頁面會用到的目標資料
  settings: {
    savingGoal: 50000,     // 目標存款
    currentSaving: 25000,  // 目前存款 (用來算進度條)
    expenseLimit: 30000,   // 花費上限
    currentExpense: 12000  // 目前花費
  }
};