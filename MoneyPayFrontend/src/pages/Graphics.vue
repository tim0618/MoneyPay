<template>
  <div class="graphicsPage pixel-page">
    <MonthSwitcher
      :year="selectedYear"
      :month="selectedMonth"
      @previous="goPreviousMonth"
      @next="goNextMonth"
    />

    <div v-if="errorMessage" class="pixel-card state-card">
      <p>{{ errorMessage }}</p>
      <button class="pixel-button secondary retryButton" type="button" @click="fetchDashboard">
        {{ t("common.retry") }}
      </button>
    </div>

    <template v-else>
      <StatsOverview
        :title="t('graphics.overviewTitle')"
        :items="overviewItems"
        :loading="loading"
        :hint="currentMonthLabel"
      />

      <TrendBarChart
        :title="t('graphics.trendTitle')"
        :items="trendItems"
        :loading="loading"
      />

      <CategoryRankingChart
        :title="t('graphics.rankingTitle')"
        :hint="topExpenseHint"
        :categories="topExpenseCategories"
        :loading="loading"
      />
    </template>
  </div>
</template>

<script setup>
import { computed, onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { homeApi } from "../apiComposables/homeApiComposables";
import { userRecordApi } from "../apiComposables/userRecordApiComposables";
import { useAppPreferences } from "../composables/useAppPreferences";
import MonthSwitcher from "../components/home/MonthSwitcher.vue";
import CategoryRankingChart from "../components/graphics/CategoryRankingChart.vue";
import StatsOverview from "../components/graphics/StatsOverview.vue";
import TrendBarChart from "../components/graphics/TrendBarChart.vue";

const router = useRouter();
const { getHomeSummaryApi } = homeApi();
const { GetBookRecordsApi } = userRecordApi();
const { t, formatCurrency, formatCompactNumber, formatMonthSlash, formatMonthShort } =
  useAppPreferences();

const currentDate = ref(new Date());
const loading = ref(true);
const errorMessage = ref("");
const trendSummaries = ref([]);
const selectedSummary = ref({
  incomeTotal: 0,
  expenseTotal: 0,
  expenseCategories: [],
  incomeCategories: [],
});
const recordStats = ref({
  recordContinuous: 0,
  recordDaysCount: 0,
  recordCount: 0,
});

const selectedYear = computed(() => currentDate.value.getFullYear());
const selectedMonth = computed(() => currentDate.value.getMonth() + 1);

const currentMonthLabel = computed(() =>
  formatMonthSlash(selectedYear.value, selectedMonth.value)
);

const monthBalance = computed(
  () => (selectedSummary.value.incomeTotal || 0) - (selectedSummary.value.expenseTotal || 0)
);

const topExpenseCategories = computed(() =>
  [...(selectedSummary.value.expenseCategories || [])]
    .filter((category) => category.amount > 0)
    .sort((a, b) => b.amount - a.amount)
    .slice(0, 5)
);

const topExpenseHint = computed(() => {
  if (!topExpenseCategories.value.length) {
    return currentMonthLabel.value;
  }

  return t("graphics.highestExpense", {
    name: topExpenseCategories.value[0].name,
  });
});

const overviewItems = computed(() => {
  const topExpense = topExpenseCategories.value[0];

  return [
    {
      label: t("graphics.monthBalance"),
      value: formatCurrency(monthBalance.value),
      hint:
        monthBalance.value >= 0
          ? t("graphics.positiveBalanceHint")
          : t("graphics.negativeBalanceHint"),
    },
    {
      label: t("graphics.streak"),
      value: `${recordStats.value.recordContinuous || 0}`,
      hint: t("graphics.streakHint"),
    },
    {
      label: t("graphics.totalRecords"),
      value: `${recordStats.value.recordCount || 0}`,
      hint: `${recordStats.value.recordDaysCount || 0} ${t("graphics.trackDays")}`,
    },
    {
      label: t("graphics.topExpense"),
      value: topExpense ? topExpense.name : t("graphics.noTopExpense"),
      hint: topExpense ? formatCurrency(topExpense.amount) : t("graphics.noRecordsYet"),
    },
  ];
});

const trendItems = computed(() =>
  trendSummaries.value.map((item) => ({
    ...item,
    label: formatMonthShort(item.year, item.month),
    netLabel: formatCompactNumber((item.incomeTotal || 0) - (item.expenseTotal || 0)),
  }))
);

const buildMonthRange = (year, month, count) =>
  Array.from({ length: count }, (_, index) => {
    const date = new Date(year, month - count + index, 1);
    return {
      year: date.getFullYear(),
      month: date.getMonth() + 1,
    };
  });

const fetchDashboard = async () => {
  try {
    loading.value = true;
    errorMessage.value = "";

    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found");
    }

    const months = buildMonthRange(selectedYear.value, selectedMonth.value, 6);

    const [bookRecords, ...summaries] = await Promise.all([
      GetBookRecordsApi(token),
      ...months.map((item) => getHomeSummaryApi(token, item.year, item.month)),
    ]);

    recordStats.value = bookRecords;
    trendSummaries.value = summaries;
    selectedSummary.value =
      summaries[summaries.length - 1] || {
        incomeTotal: 0,
        expenseTotal: 0,
        expenseCategories: [],
        incomeCategories: [],
      };
  } catch (error) {
    console.error(error);

    if (error.message === "No token found") {
      window.alert(t("common.loginRequired"));
      router.push("/login");
      return;
    }

    errorMessage.value = t("graphics.loadError");
  } finally {
    loading.value = false;
  }
};

const goPreviousMonth = async () => {
  currentDate.value = new Date(selectedYear.value, selectedMonth.value - 2, 1);
  await fetchDashboard();
};

const goNextMonth = async () => {
  currentDate.value = new Date(selectedYear.value, selectedMonth.value, 1);
  await fetchDashboard();
};

onMounted(() => {
  fetchDashboard();
});
</script>

<style scoped>
.graphicsPage {
  display: flex;
  flex-direction: column;
  gap: 18px;
}

.retryButton {
  margin-top: 14px;
}
</style>
