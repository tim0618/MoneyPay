<template>
  <div class="monthSwitcher pixel-card">
    <button
      class="switchButton"
      type="button"
      :aria-label="t('home.monthPrevious')"
      @click="$emit('previous')"
    >
      <q-icon name="chevron_left" size="22px" />
    </button>

    <div class="monthValue">
      <span class="monthLabel">{{ t("graphics.selectedMonth") }}</span>
      <strong class="monthText">{{ formatMonthSlash(year, month) }}</strong>
    </div>

    <button
      class="switchButton"
      type="button"
      :aria-label="t('home.monthNext')"
      @click="$emit('next')"
    >
      <q-icon name="chevron_right" size="22px" />
    </button>
  </div>
</template>

<script setup>
import { useAppPreferences } from "../../composables/useAppPreferences";

defineProps({
  year: {
    type: Number,
    required: true,
  },
  month: {
    type: Number,
    required: true,
  },
});

defineEmits(["previous", "next"]);

const { t, formatMonthSlash } = useAppPreferences();
</script>

<style scoped>
.monthSwitcher {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
  padding: 12px;
}

.switchButton {
  width: 46px;
  height: 46px;
  border: var(--pixel-border) solid var(--border);
  border-radius: 8px;
  background: var(--surface-soft);
  color: var(--text);
  cursor: pointer;
  box-shadow: 2px 2px 0 var(--shadow);
}

.switchButton:active {
  transform: translate(1px, 1px);
  box-shadow: 1px 1px 0 var(--shadow);
}

.monthValue {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 4px;
  text-align: center;
}

.monthLabel {
  color: var(--muted);
  font-size: 0.78rem;
}

.monthText {
  font-size: 1.1rem;
}
</style>
