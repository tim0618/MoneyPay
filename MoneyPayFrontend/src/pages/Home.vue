<template>
  <div class="q-pa-md">
    <div v-if="loading">載入中...</div>
    <div v-else>
      <div v-if="moneyStore.list.length == 0">目前沒有付費種類</div>
      <div v-else>
        
        <span style="display: flex; justify-content: center">本月</span>
        <div class="expenseTitle">
          <span style="display: flex; justify-content: center; padding: 20px">
            支出 ${{ moneyStore.totalExpense }}
          </span>
        </div>
        <div class="btnGrid">
          <MoneyTypeButton
            v-for="type in moneyStore.expenseList"
            :key="type.typeId"
            :typeDetail="type"
            @typeSelect="openTypeDialog"
          />
        </div>

        <span style="display: flex; justify-content: center; padding: 20px">
           收入 ${{ moneyStore.totalIncome }}
        </span>
        <div class="btnGrid">
          <MoneyTypeButton
            v-for="type in moneyStore.incomeList"
            :key="type.typeId"
            :typeDetail="type"
            @typeSelect="openTypeDialog"
          />
        </div>
      </div>
    </div>
    
    <MoneyTypeDialog
      v-model="showDialog"
      :type="selectedType"
    />
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
// 1. 移除 apiComposables，改引入 Store
import { useMoneyStore } from "../stores/moneyStore"; 
import { useRouter } from "vue-router";
import MoneyTypeButton from "../components/buttons/MoneyTypeButton.vue";
import MoneyTypeDialog from "../components/dialogs/MoneyTypeDialog.vue";

const router = useRouter();

// 2. 初始化 Store
const moneyStore = useMoneyStore();

// 這些原本是計算用的 computed，現在因為 Store 的 getters 幫你算好了，
// 我們可以直接在 template 用 moneyStore.xxx，所以這裡可以刪除大量的 computed 程式碼！

const loading = ref(true);
const showDialog = ref(false);
const selectedType = ref(null);

function openTypeDialog(type) {
  showDialog.value = true;
  selectedType.value = type;
}

// 3. 簡化版的 init 流程
const initPage = () => {
  loading.value = true;
  
  // 呼叫 Store 的初始化動作 (載入 Mock Data)
  moneyStore.initData();

  // 模擬一點點延遲讓 loading 轉一下 (非必要，看你心情)
  setTimeout(() => {
    loading.value = false;
  }, 300);
};

onMounted(() => {
  initPage();
});

// 原本的 expenseTypes, incomeTypes 等 computed 都不需要了
// 原本的 fetchMoneyTypes 不需要了
</script>
<style scoped>
.btnGrid {
  display: grid;
  grid-template-columns: repeat(3, 1fr); /* 每行三個 */
  gap: 12px; /* 按鈕間距 */
}
</style>
