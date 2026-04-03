<template>
  <div class="homePage pixel-page">
    <MonthSwitcher
      :year="selectedYear"
      :month="selectedMonth"
      @previous="goPreviousMonth"
      @next="goNextMonth"
    />

    <SummaryHeader
      :expense-total="summary.expenseTotal"
      :income-total="summary.incomeTotal"
      :loading="loading"
    />

    <div v-if="errorMessage" class="pixel-card state-card">
      <p>{{ errorMessage }}</p>
      <button class="pixel-button secondary retryButton" @click="fetchSummary">
        {{ t("common.retry") }}
      </button>
    </div>

    <template v-else>
      <CategoryCardGrid
        :title="t('home.expenseCategories')"
        :categories="summary.expenseCategories"
        :loading="loading"
        :empty-label="t('home.emptyExpense')"
        @select="openTypeDialog"
      />

      <CategoryCardGrid
        :title="t('home.incomeCategories')"
        :categories="summary.incomeCategories"
        :loading="loading"
        :empty-label="t('home.emptyIncome')"
        @select="openTypeDialog"
      />
    </template>

    <MoneyTypeDialog
      v-model="showDialog"
      :type="selectedType"
      @refresh-data="fetchSummary"
    />
  </div>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { homeApi } from "../apiComposables/homeApiComposables";
import { useAppPreferences } from "../composables/useAppPreferences";
import MoneyTypeDialog from "../components/dialogs/MoneyTypeDialog.vue";
import CategoryCardGrid from "../components/home/CategoryCardGrid.vue";
import MonthSwitcher from "../components/home/MonthSwitcher.vue";
import SummaryHeader from "../components/home/SummaryHeader.vue";

const router = useRouter();
const { getHomeSummaryApi } = homeApi();
const { t } = useAppPreferences();

const currentDate = ref(new Date());
const loading = ref(true);
const errorMessage = ref("");
const showDialog = ref(false);
const selectedType = ref(null);
const summary = ref({
  year: currentDate.value.getFullYear(),
  month: currentDate.value.getMonth() + 1,
  expenseTotal: 0,
  incomeTotal: 0,
  expenseCategories: [],
  incomeCategories: [],
});

const selectedYear = computed(() => currentDate.value.getFullYear());
const selectedMonth = computed(() => currentDate.value.getMonth() + 1);

const openTypeDialog = (type) => {
  if (type?.isDeleted) {
    return;
  }

  selectedType.value = type;
  showDialog.value = true;
};

const fetchSummary = async () => {
  try {
    loading.value = true;
    errorMessage.value = "";

    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found");
    }

    summary.value = await getHomeSummaryApi(
      token,
      selectedYear.value,
      selectedMonth.value
    );
  } catch (error) {
    console.error(error);

    if (error.message === "No token found") {
      window.alert(t("common.loginRequired"));
      router.push("/login");
      return;
    }

    errorMessage.value = t("home.loadError");
  } finally {
    loading.value = false;
  }
};

const goPreviousMonth = async () => {
  currentDate.value = new Date(selectedYear.value, selectedMonth.value - 2, 1);
  await fetchSummary();
};

const goNextMonth = async () => {
  currentDate.value = new Date(selectedYear.value, selectedMonth.value, 1);
  await fetchSummary();
};

onMounted(() => {
  fetchSummary();
});
</script>

<style scoped>
.homePage {
  display: flex;
  flex-direction: column;
  gap: 18px;
}

.retryButton {
  margin-top: 14px;
}
</style>
