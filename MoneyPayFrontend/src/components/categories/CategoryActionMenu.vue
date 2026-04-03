<template>
  <q-btn flat round dense icon="more_horiz" class="actionButton" :aria-label="t('common.confirm')">
    <q-menu anchor="bottom right" self="top right" content-class="pixelMenu">
      <q-list class="pixelMenuList">
        <q-item
          v-if="menuType === 'main'"
          clickable
          v-close-popup
          @click="$emit('create-subcategory', category)"
        >
          <q-item-section>{{ t("categories.actionCreateSub") }}</q-item-section>
        </q-item>

        <q-item
          v-if="menuType === 'child'"
          clickable
          v-close-popup
          @click="$emit('edit-subcategory', { category, parentCategory })"
        >
          <q-item-section>{{ t("categories.actionEditSub") }}</q-item-section>
        </q-item>

        <q-item clickable v-close-popup @click="handleDelete">
          <q-item-section class="deleteText">{{ t("categories.actionDelete") }}</q-item-section>
        </q-item>
      </q-list>
    </q-menu>
  </q-btn>
</template>

<script setup>
import { useAppPreferences } from "../../composables/useAppPreferences";

const props = defineProps({
  category: {
    type: Object,
    required: true,
  },
  parentCategory: {
    type: Object,
    default: null,
  },
  menuType: {
    type: String,
    default: "main",
  },
});

const emit = defineEmits([
  "create-subcategory",
  "edit-subcategory",
  "delete-category",
  "delete-subcategory",
]);

const { t } = useAppPreferences();

const handleDelete = () => {
  if (props.menuType === "child") {
    emit("delete-subcategory", {
      category: props.category,
      parentCategory: props.parentCategory,
    });
    return;
  }

  emit("delete-category", props.category);
};
</script>

<style scoped>
.actionButton {
  color: var(--muted);
}

.deleteText {
  color: var(--danger);
}

:global(.pixelMenu) {
  border: var(--pixel-border) solid var(--border);
  border-radius: 10px;
  background: var(--surface);
  color: var(--text);
  box-shadow: 4px 4px 0 var(--shadow);
  overflow: hidden;
}

:global(.pixelMenuList .q-item) {
  min-height: 42px;
}

:global(.pixelMenuList .q-item + .q-item) {
  border-top: 2px solid var(--border);
}
</style>
