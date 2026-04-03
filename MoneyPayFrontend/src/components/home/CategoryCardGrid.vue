<template>
  <section class="section">
    <div class="sectionHeader">
      <h2 class="sectionTitle">{{ title }}</h2>
      <span class="sectionCount">
        {{ t("home.categoryCount", { count: categories.length }) }}
      </span>
    </div>

    <div v-if="loading" class="pixel-card state-card">{{ t("common.loading") }}</div>
    <div v-else-if="categories.length === 0" class="pixel-card state-card">
      {{ emptyLabel }}
    </div>
    <div v-else class="btnGrid">
      <MoneyTypeButton
        v-for="category in categories"
        :key="category.id"
        :type-detail="category"
        @type-select="$emit('select', $event)"
      />
    </div>
  </section>
</template>

<script setup>
import { useAppPreferences } from "../../composables/useAppPreferences";
import MoneyTypeButton from "../buttons/MoneyTypeButton.vue";

defineProps({
  title: {
    type: String,
    required: true,
  },
  categories: {
    type: Array,
    default: () => [],
  },
  loading: {
    type: Boolean,
    default: false,
  },
  emptyLabel: {
    type: String,
    default: "",
  },
});

defineEmits(["select"]);

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
  align-items: center;
  justify-content: space-between;
  gap: 12px;
}

.sectionTitle {
  font-size: 1rem;
  font-weight: 700;
}

.sectionCount {
  color: var(--muted);
  font-size: 0.8rem;
}

.btnGrid {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 12px;
}

@media (max-width: 360px) {
  .btnGrid {
    grid-template-columns: repeat(2, minmax(0, 1fr));
  }
}
</style>
