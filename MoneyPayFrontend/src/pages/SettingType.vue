<template>
  <div class="cyber-panel">
    <div class="panel-header">
      <span class="header-title">>> EXPENSE_MODULE</span>
      <button class="add-btn" @click="handleAddType">[ + ADD ]</button>
    </div>
    
    <div class="panel-body">
      <div
        class="data-slot"
        v-for="expenseType in moneyStore.expenseList"
        :key="expenseType.typeId"
        :style="{ '--slot-color': expenseType.color }"
      >
        <div class="slot-left">
          <div class="icon-box">
             <q-icon :name="expenseType.icon" size="20px" />
          </div>
          <span class="slot-name">{{ expenseType.typeName }}</span>
        </div>
        
        <button class="config-btn" @click="openTypeDialog(expenseType)">
          [ EDIT ]
        </button>
      </div>
    </div>
  </div>

  <div class="cyber-panel">
    <div class="panel-header">
      <span class="header-title">>> INCOME_MODULE</span>
      <button class="add-btn" @click="handleAddType">[ + ADD ]</button>
    </div>
    
    <div class="panel-body">
      <div
        class="data-slot"
        v-for="incomeType in moneyStore.incomeList"
        :key="incomeType.typeId"
        :style="{ '--slot-color': incomeType.color }"
      >
        <div class="slot-left">
          <div class="icon-box">
             <q-icon :name="incomeType.icon" size="20px" />
          </div>
          <span class="slot-name">{{ incomeType.typeName }}</span>
        </div>
        
        <button class="config-btn" @click="openTypeDialog(incomeType)">
          [ EDIT ]
        </button>
      </div>
    </div>
  </div>

  <SettingMoneyTypeDialog 
    v-model="showDialog" 
    :type="selectedType" 
  />
</template>

<style scoped>
/* 1. 外層面板 (取代原本的 card) */
.cyber-panel {
  background-color: rgba(10, 15, 20, 0.9);
  border: 1px solid #333;
  margin: 20px 5%; /* 保持原本間距 */
  position: relative;
  /* 直角風格 */
  border-radius: 0; 
  box-shadow: 0 0 15px rgba(0, 0, 0, 0.5);
}

/* 頂部裝飾條 */
.cyber-panel::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 2px;
  background: linear-gradient(90deg, #00ffcc, transparent);
}

/* 2. 標題區塊 */
.panel-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 15px 20px;
  border-bottom: 1px dashed #333;
  background: rgba(255, 255, 255, 0.02);
}

.header-title {
  color: #00ffcc;
  font-size: 1.1rem;
  letter-spacing: 2px;
  font-weight: bold;
}

.add-btn {
  background: transparent;
  border: 1px solid #00ffcc;
  color: #00ffcc;
  padding: 5px 10px;
  cursor: pointer;
  font-family: 'Share Tech Mono', monospace;
  font-size: 0.9rem;
  transition: all 0.3s;
}

.add-btn:hover {
  background: #00ffcc;
  color: black;
  box-shadow: 0 0 10px #00ffcc;
}

/* 3. 列表內容區 */
.panel-body {
  padding: 10px;
}

/* 每一列數據槽 */
.data-slot {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 12px 10px;
  margin-bottom: 5px;
  border-left: 2px solid var(--slot-color); /* 左側顏色條 */
  background: linear-gradient(90deg, rgba(255,255,255,0.03) 0%, transparent 100%);
  transition: background 0.2s;
}

.data-slot:hover {
  background: rgba(255, 255, 255, 0.08);
}

.slot-left {
  display: flex;
  align-items: center;
  gap: 15px;
}

.icon-box {
  color: var(--slot-color);
  filter: drop-shadow(0 0 5px var(--slot-color));
}

.slot-name {
  color: #eee;
  font-size: 1.1rem;
  letter-spacing: 1px;
}

.config-btn {
  background: transparent;
  border: none;
  color: #666;
  cursor: pointer;
  font-family: 'Share Tech Mono', monospace;
  font-size: 0.9rem;
}
.config-btn:hover {
  color: var(--slot-color);
  text-shadow: 0 0 5px var(--slot-color);
}
</style>

<script setup>
import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
// 1. 引入 Store
import { useMoneyStore } from "../stores/moneyStore";
import SettingMoneyTypeDialog from "../components/dialogs/SettingMoneyTypeDialog.vue";

const router = useRouter();
const moneyStore = useMoneyStore();

const showDialog = ref(false);
const selectedType = ref(null);

onMounted(() => {
  // 記得初始化資料
  moneyStore.initData();
});

// computed 不用了，直接在 template 用 store.expenseList 即可

function openTypeDialog(type) {
  selectedType.value = type;
  showDialog.value = true;
}

const handleAddType = () => {
  alert('新增分類功能待實作'); 
};
</script>
