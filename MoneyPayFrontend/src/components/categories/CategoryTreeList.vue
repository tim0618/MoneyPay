<template>
  <div class="categoryList">
    <div v-for="category in categories" :key="category.id" class="categoryGroup">
      <div class="categoryRow">
        <button
          class="toggleButton"
          :class="{ expanded: isExpanded(category.id), disabled: !category.children?.length }"
          :aria-label="`${category.name} ${t('categories.childItem')}`"
          type="button"
          @click="toggleCategory(category.id)"
        >
          <q-icon
            :name="
              category.children?.length
                ? isExpanded(category.id)
                  ? 'keyboard_arrow_down'
                  : 'chevron_right'
                : 'keyboard_arrow_down'
            "
            :class="{ placeholderIcon: !category.children?.length }"
            size="18px"
          />
        </button>

        <q-icon
          class="categoryIcon"
          :style="{ backgroundColor: category.color || '#888888' }"
          :name="category.icon || 'mdi-shape-outline'"
          size="20px"
          color="white"
        />

        <div class="categoryText">
          <span class="categoryName">{{ category.name }}</span>
          <span class="categoryMeta">
            {{ t("categories.childCount", { count: category.children?.length || 0 }) }}
          </span>
        </div>

        <CategoryActionMenu
          :category="category"
          menu-type="main"
          @create-subcategory="$emit('create-subcategory', $event)"
          @delete-category="$emit('delete-category', $event)"
        />
      </div>

      <div v-if="isExpanded(category.id) && category.children?.length" class="childList">
        <div v-for="child in category.children" :key="child.id" class="childRow">
          <span class="togglePlaceholder"></span>

          <q-icon
            class="categoryIcon"
            :style="{ backgroundColor: category.color || '#888888' }"
            :name="category.icon || 'mdi-shape-outline'"
            size="18px"
            color="white"
          />

          <div class="categoryText">
            <span class="categoryName childName">{{ child.name }}</span>
            <span class="categoryMeta">{{ t("categories.childItem") }}</span>
          </div>

          <CategoryActionMenu
            :category="child"
            :parent-category="category"
            menu-type="child"
            @edit-subcategory="$emit('edit-subcategory', $event)"
            @delete-subcategory="$emit('delete-subcategory', $event)"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch } from "vue";
import { useAppPreferences } from "../../composables/useAppPreferences";
import CategoryActionMenu from "./CategoryActionMenu.vue";

const props = defineProps({
  categories: {
    type: Array,
    default: () => [],
  },
});

defineEmits([
  "create-subcategory",
  "edit-subcategory",
  "delete-category",
  "delete-subcategory",
]);

const { t } = useAppPreferences();
const expandedIds = ref([]);

const isExpanded = (categoryId) => expandedIds.value.includes(categoryId);

const hasChildren = (categoryId) =>
  props.categories.some(
    (category) =>
      category.id === categoryId &&
      Array.isArray(category.children) &&
      category.children.length > 0
  );

const toggleCategory = (categoryId) => {
  if (!hasChildren(categoryId)) {
    return;
  }

  expandedIds.value = isExpanded(categoryId)
    ? expandedIds.value.filter((id) => id !== categoryId)
    : [...expandedIds.value, categoryId];
};

watch(
  () => props.categories,
  (nextCategories) => {
    expandedIds.value = expandedIds.value.filter((id) =>
      nextCategories.some((category) => category.id === id)
    );
  },
  { deep: true }
);
</script>

<style scoped>
.categoryGroup + .categoryGroup {
  border-top: 2px solid var(--border);
}

.categoryRow,
.childRow {
  display: grid;
  grid-template-columns: 30px 42px minmax(0, 1fr) 40px;
  align-items: center;
  gap: 10px;
  padding: 14px 16px;
}

.toggleButton {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  width: 30px;
  height: 30px;
  border: var(--pixel-border) solid var(--border);
  border-radius: 8px;
  background: var(--surface-soft);
  color: var(--text);
  cursor: pointer;
}

.toggleButton.expanded {
  background: var(--accent);
  color: var(--accent-contrast);
}

.toggleButton.disabled {
  opacity: 0.45;
  cursor: default;
}

.placeholderIcon {
  visibility: hidden;
}

.categoryIcon {
  padding: 8px;
  border: 2px solid var(--border);
  border-radius: 8px;
}

.categoryText {
  min-width: 0;
  display: flex;
  flex-direction: column;
  gap: 3px;
}

.categoryName {
  font-weight: 700;
  word-break: break-word;
}

.childName {
  font-weight: 600;
}

.categoryMeta {
  color: var(--muted);
  font-size: 0.78rem;
}

.childList {
  background: rgba(0, 0, 0, 0.04);
}

.togglePlaceholder {
  display: block;
  width: 30px;
  height: 1px;
}
</style>
