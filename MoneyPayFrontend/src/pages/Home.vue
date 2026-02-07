<template>
  <div class="q-pa-md">
    <div v-if="loading" class="text-center text-cyber">
      <q-spinner-matrix size="50px" color="primary" />
      <div class="q-mt-sm">SYSTEM LOADING...</div>
    </div>
    
    <div v-else>
      <div v-if="moneyStore.list.length == 0" class="text-center text-cyber q-mt-xl">
        NO_DATA_FOUND // 請新增類別
      </div>
      
      <div v-else>
        <div class="hud-header">:: CURRENT_CYCLE ::</div>

        <div class="expense-panel">
          <div class="panel-label">TOTAL_EXPENSE</div>
          <div class="panel-value text-red-13">
            -${{ moneyStore.totalExpense }}
          </div>
        </div>

        <div class="btnGrid">
          <MoneyTypeButton
            v-for="type in moneyStore.expenseList"
            :key="type.typeId"
            :typeDetail="type"
            @typeSelect="openTypeDialog"
          />
        </div>

        <q-separator color="grey-9" class="q-my-lg" />

        <div class="expense-panel">
          <div class="panel-label">TOTAL_INCOME</div>
          <div class="panel-value text-green-13">
            +${{ moneyStore.totalIncome }}
          </div>
        </div>

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

<style scoped>
.btnGrid {
  display: grid;
  grid-template-columns: repeat(3, 1fr); 
  gap: 10px; /*稍微縮小間距，讓排列更緊湊像儀表板*/
}

.text-cyber {
  color: var(--cyber-neon);
  letter-spacing: 2px;
}

.hud-header {
  text-align: center;
  color: #555;
  letter-spacing: 4px;
  margin-bottom: 20px;
  font-size: 0.8rem;
  border-bottom: 1px dashed #333;
  padding-bottom: 5px;
}

.expense-panel {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-left: 2px solid #333;
  padding-left: 10px;
  margin-bottom: 15px;
  background: linear-gradient(90deg, rgba(255,255,255,0.03) 0%, transparent 100%);
}

.panel-label {
  font-size: 0.9rem;
  color: #888;
}

.panel-value {
  font-size: 1.5rem;
  font-weight: bold;
  text-shadow: 0 0 10px rgba(0,0,0,0.5);
}
</style>
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
