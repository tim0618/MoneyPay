<template>
  <!-- https://uiverse.io/Cobp/horrible-quail-18 -->
  <p>分類管理</p>
  <div class="card">
    <div class="cardContent">
      <span>支出</span>
      <span>+</span>
    </div>
    <div
      class="cardContent"
      v-for="expenseType in expenseTypes"
      :key="expenseType.typeId"
    >
      <span>
        <q-icon
          v-if="expenseType.icon"
          style="padding: 5px 0px"
          :name="expenseType.icon"
          size="24px"
          color="black"
        />
        {{ expenseType.typeName }}</span
      >
      <span @click="openTypeDialog(expenseType)">...</span>
    </div>
  </div>

  <div class="card">
    <div class="cardContent">
      <span>收入</span>
      <span @click="handleEdit">+</span>
    </div>
    <div class="cardContent" v-for="incomeType in incomeTypes">
      <span
        ><q-icon
          v-if="incomeType.icon"
          style="padding: 5px 0px"
          :name="incomeType.icon"
          size="24px"
          color="black"
        />
        {{ incomeType.typeName }}</span
      >
      <span @click="openTypeDialog(incomeType)">...</span>
    </div>
  </div>
  <SettingMoneyTypeDialog v-model="showDialog" :type="selectedType" />
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { typeApi } from "../apiComposables/typeApiComposables";
import SettingMoneyTypeDialog from "../components/dialogs/SettingMoneyTypeDialog.vue";

const router = useRouter();
const { getMoneyTypesSumApi } = typeApi();
const moneyType = ref([]);
const showDialog = ref(false);
const selectedType = ref(null);

const getMoneyType = async () => {
  try {
    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found");
    }
    const result = await getMoneyTypesSumApi(token);
    moneyType.value = result;
    console.log(moneyType.value);
  } catch (e) {
    router.push("/");
    alert("請先登入");
    console.error("Get Money Type", e);
  }
};

onMounted(() => {
  getMoneyType();
});

const expenseTypes = computed(() =>
  moneyType.value.filter((t) => t.categoryType === "Expense")
);

const incomeTypes = computed(() =>
  moneyType.value.filter((t) => t.categoryType === "Income")
);

function openTypeDialog(type) {
  showDialog.value = true;
  selectedType.value = type;
}
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
