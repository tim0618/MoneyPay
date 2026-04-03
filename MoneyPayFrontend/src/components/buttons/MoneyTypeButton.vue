<template>
  <button
    class="button pixel-card"
    :class="{ deleted: typeDetail.isDeleted }"
    :style="{ '--card-color': typeDetail.color || '#8d8d8d' }"
    :disabled="typeDetail.isDeleted"
    @click="$emit('type-select', typeDetail)"
  >
    <div class="buttonContent">
      <span class="label">{{ typeDetail.name }}</span>
      <q-icon
        class="typeIcon"
        :name="typeDetail.icon || 'mdi-shape-outline'"
        size="28px"
        color="white"
      />
      <span v-if="typeDetail.isDeleted" class="status">
        {{ t("common.deleted") }}
      </span>
      <span class="amount">{{ formatCurrency(typeDetail.amount) }}</span>
    </div>
  </button>
</template>

<script setup>
import { useAppPreferences } from "../../composables/useAppPreferences";

defineProps({
  typeDetail: {
    type: Object,
    required: true,
  },
});

defineEmits(["type-select"]);

const { t, formatCurrency } = useAppPreferences();
</script>

<style scoped>
.button {
  width: 100%;
  min-height: 150px;
  padding: 12px;
  cursor: pointer;
  background:
    linear-gradient(180deg, rgba(255, 255, 255, 0.18), rgba(0, 0, 0, 0.14)),
    var(--card-color);
  color: #ffffff;
}

.button.deleted {
  cursor: not-allowed;
  filter: grayscale(0.25);
}

.buttonContent {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 10px;
  height: 100%;
  text-align: center;
}

.label {
  max-width: 100%;
  white-space: nowrap;
  overflow: hidden;
  text-overflow: ellipsis;
  font-weight: 700;
}

.typeIcon {
  padding: 8px;
  border-radius: 8px;
  background: rgba(0, 0, 0, 0.18);
}

.status {
  font-size: 0.72rem;
  background: rgba(0, 0, 0, 0.26);
  padding: 2px 8px;
  border-radius: 999px;
}

.amount {
  font-size: 0.9rem;
  font-weight: 700;
}

.button:active {
  transform: translate(2px, 2px);
}
</style>
