<template>
  <div class="summaryGrid">
    <div class="summaryCard pixel-card">
      <span class="summaryLabel">{{ t("home.totalExpense") }}</span>
      <span class="summaryValue">{{ displayExpenseTotal }}</span>
    </div>

    <div class="summaryCard pixel-card">
      <span class="summaryLabel">{{ t("home.totalIncome") }}</span>
      <span class="summaryValue">{{ displayIncomeTotal }}</span>
    </div>
  </div>
</template>

<script setup>
import { computed } from "vue";
import { useAppPreferences } from "../../composables/useAppPreferences";

const props = defineProps({
  expenseTotal: {
    type: Number,
    default: 0,
  },
  incomeTotal: {
    type: Number,
    default: 0,
  },
  loading: {
    type: Boolean,
    default: false,
  },
});

const { t, formatCurrency } = useAppPreferences();

const displayExpenseTotal = computed(() =>
  props.loading ? "--" : formatCurrency(props.expenseTotal)
);

const displayIncomeTotal = computed(() =>
  props.loading ? "--" : formatCurrency(props.incomeTotal)
);
</script>

<style scoped>
.summaryGrid {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 12px;
}

.summaryCard {
  padding: 16px;
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.summaryLabel {
  color: var(--muted);
  font-size: 0.84rem;
}

.summaryValue {
  font-size: 1.4rem;
  font-weight: 700;
}
</style>
