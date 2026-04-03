<template>
  <section class="section">
    <div class="sectionHeader">
      <h2 class="sectionTitle">{{ title }}</h2>
      <span class="sectionHint">{{ hint }}</span>
    </div>

    <div v-if="loading" class="pixel-card state-card">{{ t("common.loading") }}</div>
    <div v-else-if="categories.length === 0" class="pixel-card state-card">
      {{ t("graphics.noRankingData") }}
    </div>
    <div v-else class="rankCard pixel-card">
      <div v-for="category in categories" :key="category.id" class="rankRow">
        <div class="rankHeader">
          <div class="rankIdentity">
            <q-icon
              class="rankIcon"
              :style="{ backgroundColor: category.color || '#888888' }"
              :name="category.icon || 'mdi-shape-outline'"
              size="18px"
              color="white"
            />
            <div class="rankText">
              <span class="rankName">{{ category.name }}</span>
              <span v-if="category.isDeleted" class="rankBadge">{{ t("common.deleted") }}</span>
            </div>
          </div>
          <strong class="rankAmount">{{ formatCurrency(category.amount) }}</strong>
        </div>

        <div class="track">
          <div
            class="fill"
            :style="{
              width: `${getPercentage(category.amount)}%`,
              backgroundColor: category.color || '#888888',
            }"
          ></div>
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
  hint: {
    type: String,
    default: "",
  },
  categories: {
    type: Array,
    default: () => [],
  },
  loading: {
    type: Boolean,
    default: false,
  },
});

const { t, formatCurrency } = useAppPreferences();

const maxAmount = computed(() =>
  Math.max(...props.categories.map((category) => category.amount), 1)
);

const getPercentage = (amount) => (amount / maxAmount.value) * 100;
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
  align-items: baseline;
  gap: 12px;
}

.sectionTitle {
  font-size: 1rem;
  font-weight: 700;
}

.sectionHint {
  color: var(--muted);
  font-size: 0.8rem;
}

.rankCard {
  display: flex;
  flex-direction: column;
  gap: 16px;
  padding: 16px;
}

.rankRow {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.rankHeader {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 12px;
}

.rankIdentity {
  display: flex;
  align-items: center;
  gap: 10px;
  min-width: 0;
}

.rankIcon {
  padding: 8px;
  border: 2px solid var(--border);
  border-radius: 8px;
}

.rankText {
  min-width: 0;
  display: flex;
  align-items: center;
  gap: 8px;
  flex-wrap: wrap;
}

.rankName {
  font-weight: 700;
}

.rankBadge {
  color: var(--danger-contrast);
  background: var(--danger);
  border-radius: 999px;
  padding: 2px 8px;
  font-size: 0.72rem;
}

.rankAmount {
  white-space: nowrap;
}

.track {
  width: 100%;
  height: 14px;
  border: 2px solid var(--border);
  background: var(--surface-soft);
}

.fill {
  height: 100%;
  border-right: 2px solid var(--border);
}
</style>
