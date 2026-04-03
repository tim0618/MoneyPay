<template>
  <section class="section">
    <div class="sectionHeader">
      <h2 class="sectionTitle">{{ title }}</h2>
      <span class="sectionHint">{{ hint }}</span>
    </div>

    <div v-if="loading" class="pixel-card state-card">{{ t("common.loading") }}</div>
    <div v-else class="statGrid">
      <article v-for="item in items" :key="item.label" class="statCard pixel-card">
        <span class="statLabel">{{ item.label }}</span>
        <strong class="statValue">{{ item.value }}</strong>
        <span class="statHint">{{ item.hint }}</span>
      </article>
    </div>
  </section>
</template>

<script setup>
import { useAppPreferences } from "../../composables/useAppPreferences";

defineProps({
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
  hint: {
    type: String,
    default: "",
  },
});

const { t } = useAppPreferences();
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
  font-size: 0.82rem;
}

.statGrid {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 12px;
}

.statCard {
  padding: 16px;
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.statLabel {
  color: var(--muted);
  font-size: 0.84rem;
}

.statValue {
  font-size: 1.28rem;
}

.statHint {
  color: var(--muted);
  font-size: 0.78rem;
  line-height: 1.5;
}

@media (max-width: 420px) {
  .statGrid {
    grid-template-columns: 1fr;
  }
}
</style>
