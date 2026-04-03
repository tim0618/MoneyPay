<template>
  <section class="card pixel-card">
    <div class="cardHeader">
      <div class="titleBlock">
        <span class="cardTitle">{{ title }}</span>
      </div>

      <button
        class="pixel-button secondary createButton"
        type="button"
        @click="$emit('create-main', categoryType)"
      >
        {{ t("categories.addMain") }}
      </button>
    </div>

    <div v-if="categories.length === 0" class="emptyState">
      {{ t("categories.empty") }}
    </div>
    <CategoryTreeList
      v-else
      :categories="categories"
      @create-subcategory="$emit('create-subcategory', $event)"
      @edit-subcategory="$emit('edit-subcategory', $event)"
      @delete-category="$emit('delete-category', $event)"
      @delete-subcategory="$emit('delete-subcategory', $event)"
    />
  </section>
</template>

<script setup>
import { useAppPreferences } from "../../composables/useAppPreferences";
import CategoryTreeList from "./CategoryTreeList.vue";

defineProps({
  title: {
    type: String,
    required: true,
  },
  categoryType: {
    type: String,
    required: true,
  },
  categories: {
    type: Array,
    default: () => [],
  },
});

defineEmits([
  "create-main",
  "create-subcategory",
  "edit-subcategory",
  "delete-category",
  "delete-subcategory",
]);

const { t } = useAppPreferences();
</script>

<style scoped>
.card {
  padding: 16px 0;
}

.cardHeader {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 12px;
  padding: 0 16px 12px;
}

.cardTitle {
  font-size: 1rem;
  font-weight: 700;
}

.createButton {
  padding: 10px 12px;
}

.emptyState {
  padding: 0 16px;
  color: var(--muted);
}

@media (max-width: 420px) {
  .cardHeader {
    flex-direction: column;
    align-items: stretch;
  }

  .createButton {
    width: 100%;
  }
}
</style>
