<template>
  <div class="q-pa-md">
    <div v-if="loading">載入中...</div>
    <div v-else>
      <div v-if="types.length == 0">目前沒有付費種類</div>
      <div v-else>
        <span style="display: flex; justify-content: center">本月</span>
        <div class="expenseTitle">
          <span style="display: flex; justify-content: center; padding: 20px"
            >支出 ${{ expenseTypesTotal }}</span
          >
        </div>
        <div class="btnGrid">
          <MoneyTypeButton
            v-for="type in expenseTypes"
            :key="type.id"
            :typeDetail="type"
            @typeSelect="openTypeDialog"
          />
        </div>
        <span style="display: flex; justify-content: center; padding: 20px"
          >收入 ${{ incomeTypesTotal }}</span
        >
        <div class="btnGrid">
          <!-- <MoneyTypeButton v-for="type in types" :key="type.id" v-bind="type" /> -->
          <MoneyTypeButton
            v-for="type in incomeTypes"
            :key="type.id"
            :typeDetail="type"
            @typeSelect="openTypeDialog"
          />
        </div>
      </div>
    </div>
    <MoneyTypeDialog
      v-model="showDialog"
      :type="selectedType"
      @refreshData="fetchMoneyTypes"
    />
  </div>
</template>
<script setup>
import { onMounted, ref, computed } from "vue";
import { typeApi } from "../apiComposables/typeApiComposables";
import { useRouter } from "vue-router";
import MoneyTypeButton from "../components/buttons/MoneyTypeButton.vue";
import MoneyTypeDialog from "../components/dialogs/MoneyTypeDialog.vue";

const { getMoneyTypesSumApi } = typeApi();

const router = useRouter();
const types = ref([]);
const loading = ref(true);

const showDialog = ref(false);
const selectedType = ref(null);

function openTypeDialog(type) {
  showDialog.value = true;
  selectedType.value = type;
}

const fetchMoneyTypes = async () => {
  try {
    loading.value = true; // 在開始載入時設定為 true
    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found");
    }
    const result = await getMoneyTypesSumApi(token);
    types.value = result;
  } catch (error) {
    console.error(error);
    alert("請先登入");
    router.push("/login");
  } finally {
    loading.value = false;
  }
};

onMounted(() => {
  fetchMoneyTypes();
});

const expenseTypes = computed(() =>
  types.value.filter((t) => t.categoryType === "Expense")
);

const incomeTypes = computed(() => {
  return types.value.filter((t) => {
    return t.categoryType === "Income";
  });
});

const expenseTypesTotal = computed(() =>
  expenseTypes.value.reduce((sum, item) => sum + item.totalPay, 0)
);

const incomeTypesTotal = computed(() =>
  incomeTypes.value.reduce((sum, item) => sum + item.totalPay, 0)
);
</script>
<style scoped>
.btnGrid {
  display: grid;
  grid-template-columns: repeat(3, 1fr); /* 每行三個 */
  gap: 12px; /* 按鈕間距 */
}
</style>
