// src/stores/moneyStore.js
import { defineStore } from 'pinia';
import { MOCK_MONEY_TYPE_SUM } from '../api/mock/moneyTypeData';

const INITIAL_REMARKS = [
  // Food (typeId: 1)
  { remarkId: 1, typeId: 1, remark: 'Breakfast' },
  { remarkId: 2, typeId: 1, remark: 'Lunch' },
  { remarkId: 3, typeId: 1, remark: 'Dinner' },
  
  // Transport (typeId: 2)
  { remarkId: 4, typeId: 2, remark: 'Bus' },
  { remarkId: 5, typeId: 2, remark: 'MRT' },
  { remarkId: 6, typeId: 2, remark: 'Taxi' },
  
  // Entertainment (typeId: 3)
  { remarkId: 7, typeId: 3, remark: 'Movie' },
  { remarkId: 8, typeId: 3, remark: 'Game' },
  { remarkId: 9, typeId: 3, remark: 'Concert' },
  
  // Shopping (typeId: 4)
  { remarkId: 10, typeId: 4, remark: 'Clothes' },
  { remarkId: 11, typeId: 4, remark: 'Electronics' },
  { remarkId: 12, typeId: 4, remark: 'Groceries' },
  
  // Healthcare (typeId: 5)
  { remarkId: 13, typeId: 5, remark: 'Medicine' },
  { remarkId: 14, typeId: 5, remark: 'Checkup' },
  { remarkId: 15, typeId: 5, remark: 'Insurance' },
  
  // Education (typeId: 6)
  { remarkId: 16, typeId: 6, remark: 'Books' },
  { remarkId: 17, typeId: 6, remark: 'Tuition' },
  { remarkId: 18, typeId: 6, remark: 'OnlineCourse' },
  
  // Family (typeId: 7)
  { remarkId: 19, typeId: 7, remark: 'Gift' },
  { remarkId: 20, typeId: 7, remark: 'House' },
  { remarkId: 21, typeId: 7, remark: 'Children' },
  
  // Travel (typeId: 8)
  { remarkId: 22, typeId: 8, remark: 'Flight' },
  { remarkId: 23, typeId: 8, remark: 'Hotel' },
  { remarkId: 24, typeId: 8, remark: 'Taxi' }, // 雖然跟上面名稱一樣，但這是旅行用的
  
  // Pet (typeId: 9)
  { remarkId: 25, typeId: 9, remark: 'Food' },
  { remarkId: 26, typeId: 9, remark: 'Vet' },
  { remarkId: 27, typeId: 9, remark: 'Toy' },
  
  // Salary (Income - typeId: 10)
  { remarkId: 28, typeId: 10, remark: 'Monthly' },
  { remarkId: 29, typeId: 10, remark: 'Bonus' },
  { remarkId: 30, typeId: 10, remark: 'Overtime' },
  
  // Bonus (Income - typeId: 11)
  { remarkId: 31, typeId: 11, remark: 'Performance' },
  { remarkId: 32, typeId: 11, remark: 'Referral' },
  { remarkId: 33, typeId: 11, remark: 'Holiday' },
  
  // Other (Income - typeId: 12)
  { remarkId: 34, typeId: 12, remark: 'Misc1' },
  { remarkId: 35, typeId: 12, remark: 'Misc2' },
  { remarkId: 36, typeId: 12, remark: 'Misc3' }
];

export const useMoneyStore = defineStore('money', {
  state: () => ({
    list: [],
    remarks: []
  }),

  actions: {
    initData() {
      if (this.list.length === 0) this.list = MOCK_MONEY_TYPE_SUM;
      if (this.remarks.length === 0) this.remarks = INITIAL_REMARKS;
    },

    updateType(typeId, newName) {
      const target = this.list.find(t => t.typeId === typeId);
      if (target) target.typeName = newName;
    },

    // ★★★ 關鍵修改：確保這個函數名稱是 getRemarksByTypeId ★★★
    // 這樣首頁和設定頁面都能通用
    getRemarksByTypeId(typeId) {
      return this.remarks.filter(r => r.typeId === typeId);
    },
    // 為了相容性，如果你設定頁面是用 getRemarksByType，我們做個轉接
    getRemarksByType(typeId) {
      return this.getRemarksByTypeId(typeId);
    },

    addRemark(typeId, remarkText) {
      const newId = Date.now();
      this.remarks.push({
        remarkId: newId,
        typeId: typeId,
        remark: remarkText
      });
    },

    deleteRemark(remarkId) {
      const index = this.remarks.findIndex(r => r.remarkId === remarkId);
      if (index !== -1) this.remarks.splice(index, 1);
    },

    // 首頁 Dialog 用的新增交易功能
    addTransaction(payload) {
      const { typeId, price } = payload;
      const target = this.list.find(item => item.typeId === typeId);
      if (target) {
        target.totalPay += Number(price);
        return true;
      }
      return false;
    }
  },

  getters: {
    expenseList: (state) => state.list.filter(t => t.categoryType === "Expense"),
    incomeList: (state) => state.list.filter(t => t.categoryType === "Income"),
    // 首頁用的總金額計算
    totalExpense: (state) => state.list
        .filter(t => t.categoryType === "Expense")
        .reduce((sum, item) => sum + item.totalPay, 0),
    totalIncome: (state) => state.list
        .filter(t => t.categoryType === "Income")
        .reduce((sum, item) => sum + item.totalPay, 0),
  },
  
  persist: true
});