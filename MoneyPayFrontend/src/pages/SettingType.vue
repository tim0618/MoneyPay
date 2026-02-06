<template>
  <div class="card">
    <div class="cardContent">
      <span>支出</span>
      <span @click="handleAddType">+</span>
    </div>
    <div
      class="cardContent"
      v-for="expenseType in moneyStore.expenseList"
      :key="expenseType.typeId"
    >
      <span>
        <q-icon
          v-if="expenseType.icon"
          style="padding: 5px 0px"
          :name="expenseType.icon"
          size="24px"
          :style="{ color: expenseType.color }"
        />
        {{ expenseType.typeName }}
      </span>
      <span @click="openTypeDialog(expenseType)">...</span>
    </div>
  </div>

  <div class="card">
    <div class="cardContent">
      <span>收入</span>
      <span @click="handleAddType">+</span>
    </div>
    <div class="cardContent" v-for="incomeType in moneyStore.incomeList">
      <span>
        <q-icon
          v-if="incomeType.icon"
          style="padding: 5px 0px"
          :name="incomeType.icon"
          size="24px"
          :style="{ color: incomeType.color }"
        />
        {{ incomeType.typeName }}
      </span>
      <span @click="openTypeDialog(incomeType)">...</span>
    </div>
  </div>

  <SettingMoneyTypeDialog 
    v-model="showDialog" 
    :type="selectedType" 
  />
</template>

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

<style>
.card {
  background-color: rgb(255, 255, 255);
  border-radius: 8px;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  padding: 20px 0px;
  margin: 5%;
  gap: 13px;
  box-shadow: 2px 2px 20px rgba(0, 0, 0, 0.062);
}
.cardContent {
  width: 100%;
  display: flex;
  justify-content: space-between;
  padding: 0px 10%;
}
</style>
