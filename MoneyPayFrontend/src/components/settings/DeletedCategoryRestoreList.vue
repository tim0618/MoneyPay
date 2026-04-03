<template>
  <section class="sectionCard pixel-card">
    <div class="sectionHeader">
      <div>
        <h3 class="sectionTitle">{{ t("settings.restoreTitle") }}</h3>
        <p class="sectionHint">{{ t("settings.restoreHint") }}</p>
      </div>

      <div class="filterGroup">
        <button
          v-for="option in filterOptions"
          :key="option.value"
          class="filterButton"
          :class="{ active: selectedType === option.value }"
          type="button"
          @click="$emit('change-type', option.value)"
        >
          {{ option.label }}
        </button>
      </div>
    </div>

    <div v-if="loading" class="stateBox">{{ t("common.loading") }}</div>
    <div v-else-if="categories.length === 0" class="stateBox">
      {{ t("settings.noDeletedCategories") }}
    </div>
    <div v-else class="restoreList">
      <article v-for="category in categories" :key="category.id" class="restoreGroup pixel-card">
        <div class="restoreHeader">
          <div class="restoreIdentity">
            <q-icon
              class="restoreIcon"
              :style="{ backgroundColor: category.color || '#888888' }"
              :name="category.icon || 'mdi-shape-outline'"
              size="18px"
              color="white"
            />
            <div class="restoreText">
              <span class="restoreName">{{ category.name }}</span>
              <span class="restoreMeta">
                {{
                  category.isDeleted
                    ? t("settings.deletedCategoryOnly")
                    : t("settings.deletedChildrenOnly")
                }}
              </span>
            </div>
          </div>

          <button
            v-if="category.isDeleted"
            class="pixel-button secondary"
            type="button"
            @click="$emit('restore-category', category)"
          >
            {{ t("settings.restoreCategory") }}
          </button>
        </div>

        <div v-if="category.children?.length" class="childRestoreList">
          <div v-for="child in category.children" :key="child.id" class="childRestoreRow">
            <div class="childTextGroup">
              <span class="childName">{{ child.name }}</span>
              <span class="childMeta">{{ t("categories.childItem") }}</span>
            </div>

            <button
              v-if="!category.isDeleted"
              class="pixel-button secondary"
              type="button"
              @click="$emit('restore-subcategory', child)"
            >
              {{ t("settings.restoreSubcategory") }}
            </button>
            <span v-else class="parentHint">{{ t("settings.restoreParentFirst") }}</span>
          </div>
        </div>
      </article>
    </div>
  </section>
</template>

<script setup>
import { computed } from "vue";
import { useAppPreferences } from "../../composables/useAppPreferences";

const props = defineProps({
  categories: {
    type: Array,
    default: () => [],
  },
  loading: {
    type: Boolean,
    default: false,
  },
  selectedType: {
    type: String,
    default: "expense",
  },
});

defineEmits(["change-type", "restore-category", "restore-subcategory"]);

const { t } = useAppPreferences();

const filterOptions = computed(() => [
  { label: t("settings.deletedExpense"), value: "expense" },
  { label: t("settings.deletedIncome"), value: "income" },
]);
</script>

<style scoped>
.sectionCard {
  padding: 18px;
}

.sectionHeader {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  gap: 16px;
  margin-bottom: 16px;
}

.sectionTitle {
  font-size: 1rem;
  font-weight: 700;
}

.sectionHint,
.restoreMeta,
.childMeta,
.parentHint {
  color: var(--muted);
  font-size: 0.82rem;
  line-height: 1.5;
}

.filterGroup {
  display: inline-flex;
  gap: 8px;
}

.filterButton {
  border: var(--pixel-border) solid var(--border);
  border-radius: 8px;
  padding: 9px 12px;
  background: var(--surface-soft);
  color: var(--text);
  cursor: pointer;
  font-weight: 700;
  box-shadow: 2px 2px 0 var(--shadow);
}

.filterButton.active {
  background: var(--accent);
  color: var(--accent-contrast);
}

.stateBox {
  color: var(--muted);
}

.restoreList {
  display: flex;
  flex-direction: column;
  gap: 14px;
}

.restoreGroup {
  padding: 16px;
  background: var(--surface-soft);
}

.restoreHeader,
.childRestoreRow {
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: 12px;
}

.restoreIdentity {
  display: flex;
  align-items: center;
  gap: 12px;
}

.restoreIcon {
  padding: 8px;
  border: 2px solid var(--border);
  border-radius: 8px;
}

.restoreText,
.childTextGroup {
  display: flex;
  flex-direction: column;
  gap: 3px;
}

.restoreName,
.childName {
  font-weight: 700;
}

.childRestoreList {
  margin-top: 14px;
  padding-top: 14px;
  border-top: 2px solid var(--border);
  display: flex;
  flex-direction: column;
  gap: 10px;
}

@media (max-width: 420px) {
  .sectionHeader,
  .restoreHeader,
  .childRestoreRow {
    flex-direction: column;
    align-items: stretch;
  }

  .filterGroup {
    width: 100%;
  }

  .filterButton,
  .pixel-button {
    width: 100%;
  }
}
</style>
