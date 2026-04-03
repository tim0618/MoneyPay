<template>
  <section class="section">
    <div class="sectionHeader">
      <h2 class="sectionTitle">{{ title }}</h2>
      <div class="legend">
        <span class="legendItem">
          <span class="legendDot income"></span>
          {{ t("graphics.legendIncome") }}
        </span>
        <span class="legendItem">
          <span class="legendDot expense"></span>
          {{ t("graphics.legendExpense") }}
        </span>
      </div>
    </div>

    <div v-if="loading" class="pixel-card state-card">{{ t("common.loading") }}</div>
    <div v-else-if="items.length === 0" class="pixel-card state-card">
      {{ t("graphics.noTrendData") }}
    </div>
    <div v-else class="chartCard pixel-card">
      <div class="chartCanvas">
        <div v-for="item in items" :key="item.label" class="group">
          <div class="barPair">
            <div class="bar income" :style="{ height: `${getBarHeight(item.incomeTotal)}px` }"></div>
            <div class="bar expense" :style="{ height: `${getBarHeight(item.expenseTotal)}px` }"></div>
          </div>
          <span class="groupLabel">{{ item.label }}</span>
          <span class="groupValue">{{ item.netLabel }}</span>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
import { computed } from "vue";
import { useAppPreferences } from "../../composables/useAppPreferences";

const props = defineProps({
  title: {
    type: String,
    default: "",
  },
  items: {
    type: Array,
    default: () => [],
  },
  loading: {
    type: Boolean,
    default: false,
  },
});

const { t } = useAppPreferences();

const maxValue = computed(() => {
  const values = props.items.flatMap((item) => [item.incomeTotal, item.expenseTotal]);
  return Math.max(...values, 1);
});

const getBarHeight = (value) => {
  const baseHeight = 16;
  const maxHeight = 176;
  return baseHeight + (value / maxValue.value) * (maxHeight - baseHeight);
};
</script>

<style scoped>
.section {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.sectionHeader {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 12px;
}

.sectionTitle {
  font-size: 1rem;
  font-weight: 700;
}

.legend {
  display: flex;
  gap: 10px;
  flex-wrap: wrap;
}

.legendItem {
  display: inline-flex;
  align-items: center;
  gap: 6px;
  color: var(--muted);
  font-size: 0.8rem;
}

.legendDot {
  width: 10px;
  height: 10px;
  border: 2px solid var(--border);
}

.legendDot.income,
.bar.income {
  background: #6e92e6;
}

.legendDot.expense,
.bar.expense {
  background: #d97a64;
}

.chartCard {
  padding: 18px 14px;
}

.chartCanvas {
  display: grid;
  grid-template-columns: repeat(6, minmax(0, 1fr));
  gap: 8px;
  align-items: end;
  min-height: 240px;
}

.group {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 8px;
}

.barPair {
  width: 100%;
  min-height: 190px;
  display: flex;
  align-items: end;
  justify-content: center;
  gap: 6px;
}

.bar {
  width: 18px;
  border: 2px solid var(--border);
  border-bottom-width: 3px;
}

.groupLabel {
  font-size: 0.8rem;
  font-weight: 700;
}

.groupValue {
  color: var(--muted);
  font-size: 0.72rem;
}

@media (max-width: 420px) {
  .chartCanvas {
    gap: 4px;
  }

  .bar {
    width: 14px;
  }

  .groupValue {
    font-size: 0.66rem;
  }
}
</style>
