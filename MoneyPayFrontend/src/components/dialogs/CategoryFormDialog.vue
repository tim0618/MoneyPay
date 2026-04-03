<template>
  <div
    v-if="modelValue"
    class="overlay"
    @click.self="$emit('update:modelValue', false)"
  >
    <div class="dialog pixel-card">
      <button
        class="closeBtn"
        type="button"
        :aria-label="t('common.close')"
        @click="$emit('update:modelValue', false)"
      >
        <q-icon name="close" size="20px" />
      </button>

      <div class="dialogHeader">
        <h3 class="dialogTitle">{{ dialogTitle }}</h3>
        <p class="dialogMeta">{{ categoryLabel }}</p>
      </div>

      <div class="contentSection pixel-card">
        <div class="detailRow">
          <span class="detailLabel">{{ t("categories.typeLabel") }}</span>
          <span class="detailValue">{{ categoryLabel }}</span>
        </div>

        <div v-if="isSubcategory" class="detailRow">
          <span class="detailLabel">{{ t("categories.parentLabel") }}</span>
          <span class="detailValue">{{ parentCategory?.name }}</span>
        </div>

        <label class="field-stack inputGroup">
          <span class="field-label">{{ t("categories.nameLabel") }}</span>
          <input
            v-model="name"
            class="pixel-input"
            type="text"
            :placeholder="t('categories.nameLabel')"
          />
        </label>

        <template v-if="!isSubcategory">
          <div class="inputGroup">
            <div class="rowHeader">
              <span class="field-label">{{ t("categories.iconLabel") }}</span>
              <button
                class="toggleMoreButton"
                type="button"
                @click="showAllIcons = !showAllIcons"
              >
                {{ showAllIcons ? t("categories.lessIcons") : t("categories.moreIcons") }}
              </button>
            </div>

            <div class="iconGrid">
              <button
                v-for="iconOption in visibleIconOptions"
                :key="iconOption.name"
                type="button"
                class="iconOption"
                :class="{ selected: icon === iconOption.name }"
                :aria-label="t(iconOption.labelKey)"
                @click="icon = iconOption.name"
              >
                <q-icon :name="iconOption.name" size="24px" />
                <span class="iconLabel">{{ t(iconOption.labelKey) }}</span>
              </button>
            </div>
          </div>

          <div class="inputGroup">
            <span class="field-label">{{ t("categories.colorLabel") }}</span>

            <div class="colorPalette">
              <button
                v-for="colorOption in colorOptions"
                :key="colorOption.value"
                type="button"
                class="colorOption"
                :class="{ selected: !isCustomColor && color === colorOption.value }"
                :style="{ backgroundColor: colorOption.value }"
                :aria-label="t(colorOption.labelKey)"
                @click="selectPresetColor(colorOption.value)"
              >
                <q-icon
                  v-if="!isCustomColor && color === colorOption.value"
                  name="check"
                  size="18px"
                  color="white"
                />
              </button>

              <button
                type="button"
                class="colorOption customColorOption"
                :class="{ selected: isCustomColor }"
                :style="{ background: customColorBackground }"
                :aria-label="t('categories.customColor')"
                @click="openCustomColorPicker"
              >
                <q-icon
                  :name="isCustomColor ? 'palette' : 'add'"
                  size="18px"
                  color="white"
                />
              </button>
            </div>

            <input
              ref="customColorInput"
              class="sr-only"
              type="color"
              :value="customColor"
              @input="handleCustomColorChange"
            />
          </div>
        </template>

        <div v-else class="inheritBox">
          {{ t("categories.inheritHint") }}
        </div>

        <div class="previewBox">
          <q-icon
            class="previewIcon"
            :style="{ backgroundColor: previewColor }"
            :name="previewIcon"
            size="28px"
            color="white"
          />
          <div class="previewTextGroup">
            <span class="previewText">{{ previewName }}</span>
            <span class="previewHint">{{ t("categories.previewLabel") }}</span>
          </div>
        </div>
      </div>

      <div class="actionButtons">
        <button class="pixel-button" type="button" @click="submitForm">
          {{ submitLabel }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { computed, ref, watch } from "vue";
import { useAppPreferences } from "../../composables/useAppPreferences";

const EXPENSE_ICONS = [
  { name: "mdi-silverware-fork-knife", labelKey: "categories.iconFood" },
  { name: "mdi-coffee", labelKey: "categories.iconCoffee" },
  { name: "mdi-cart", labelKey: "categories.iconShopping" },
  { name: "mdi-storefront-outline", labelKey: "categories.iconStore" },
  { name: "mdi-car", labelKey: "categories.iconTransport" },
  { name: "mdi-train", labelKey: "categories.iconTrain" },
  { name: "mdi-home", labelKey: "categories.iconHome" },
  { name: "mdi-lightbulb-outline", labelKey: "categories.iconLight" },
  { name: "mdi-heart-pulse", labelKey: "categories.iconHealth" },
  { name: "mdi-school", labelKey: "categories.iconEducation" },
  { name: "mdi-airplane", labelKey: "categories.iconTravel" },
  { name: "mdi-cellphone", labelKey: "categories.iconPhone" },
  { name: "mdi-wifi", labelKey: "categories.iconInternet" },
  { name: "mdi-gamepad-variant", labelKey: "categories.iconGame" },
  { name: "mdi-movie-open-outline", labelKey: "categories.iconMovie" },
  { name: "mdi-dumbbell", labelKey: "categories.iconFitness" },
  { name: "mdi-paw", labelKey: "categories.iconPet" },
  { name: "mdi-tshirt-crew-outline", labelKey: "categories.iconClothes" },
  { name: "mdi-dots-horizontal", labelKey: "categories.iconOther" },
];

const INCOME_ICONS = [
  { name: "mdi-cash", labelKey: "categories.iconSalary" },
  { name: "mdi-cash-plus", labelKey: "categories.iconBonus" },
  { name: "mdi-briefcase", labelKey: "categories.iconWork" },
  { name: "mdi-account-cash", labelKey: "categories.iconFreelance" },
  { name: "mdi-bank", labelKey: "categories.iconBank" },
  { name: "mdi-wallet", labelKey: "categories.iconWallet" },
  { name: "mdi-credit-card-outline", labelKey: "categories.iconCard" },
  { name: "mdi-gift", labelKey: "categories.iconGift" },
  { name: "mdi-cash-refund", labelKey: "categories.iconRefund" },
  { name: "mdi-chart-line", labelKey: "categories.iconInvest" },
  { name: "mdi-finance", labelKey: "categories.iconDividend" },
  { name: "mdi-home-import-outline", labelKey: "categories.iconRent" },
  { name: "mdi-piggy-bank", labelKey: "categories.iconSaving" },
  { name: "mdi-safe-square-outline", labelKey: "categories.iconSafe" },
  { name: "mdi-account-heart-outline", labelKey: "categories.iconFamily" },
  { name: "mdi-tag-outline", labelKey: "categories.iconSale" },
  { name: "mdi-dots-horizontal", labelKey: "categories.iconOther" },
];

const COLOR_OPTIONS = [
  { value: "#FF7043", labelKey: "categories.colorSunset" },
  { value: "#EF5350", labelKey: "categories.colorCoral" },
  { value: "#EC407A", labelKey: "categories.colorRose" },
  { value: "#AB47BC", labelKey: "categories.colorPlum" },
  { value: "#7E57C2", labelKey: "categories.colorIndigo" },
  { value: "#5C6BC0", labelKey: "categories.colorNavy" },
  { value: "#42A5F5", labelKey: "categories.colorSky" },
  { value: "#26A69A", labelKey: "categories.colorMint" },
  { value: "#66BB6A", labelKey: "categories.colorLeaf" },
];

const INITIAL_ICON_COUNT = 10;

const props = defineProps({
  modelValue: Boolean,
  mode: {
    type: String,
    default: "main",
  },
  categoryType: {
    type: String,
    default: "Expense",
  },
  parentCategory: {
    type: Object,
    default: null,
  },
  subcategory: {
    type: Object,
    default: null,
  },
});

const emit = defineEmits(["update:modelValue", "submit"]);

const { t } = useAppPreferences();
const name = ref("");
const icon = ref("");
const color = ref(COLOR_OPTIONS[0].value);
const customColor = ref("#111111");
const isCustomColor = ref(false);
const showAllIcons = ref(false);
const customColorInput = ref(null);

const isSubcategory = computed(
  () => props.mode === "sub" || props.mode === "edit-sub"
);
const isEditSubcategory = computed(() => props.mode === "edit-sub");

const categoryLabel = computed(() =>
  props.categoryType === "Income"
    ? t("categories.categoryTypeIncome")
    : t("categories.categoryTypeExpense")
);

const dialogTitle = computed(() =>
  isEditSubcategory.value
    ? t("categories.editSub")
    : isSubcategory.value
      ? t("categories.addSub")
      : t("categories.addMain")
);

const iconOptions = computed(() =>
  props.categoryType === "Income" ? INCOME_ICONS : EXPENSE_ICONS
);

const visibleIconOptions = computed(() =>
  showAllIcons.value
    ? iconOptions.value
    : iconOptions.value.slice(0, INITIAL_ICON_COUNT)
);

const colorOptions = computed(() => COLOR_OPTIONS);

const previewIcon = computed(() =>
  isSubcategory.value
    ? props.parentCategory?.icon || "mdi-shape-outline"
    : icon.value || "mdi-shape-outline"
);

const previewColor = computed(() =>
  isSubcategory.value ? props.parentCategory?.color || "#888888" : color.value
);

const previewName = computed(() => name.value || t("categories.nameLabel"));
const submitLabel = computed(() =>
  isEditSubcategory.value ? t("common.update") : t("common.create")
);

const customColorBackground = computed(() =>
  isCustomColor.value
    ? `linear-gradient(180deg, rgba(255,255,255,0.16), rgba(0,0,0,0.12)), ${customColor.value}`
    : "linear-gradient(135deg, #1c1c1c 0%, #1c1c1c 50%, #f6f6f6 50%, #f6f6f6 100%)"
);

const selectPresetColor = (value) => {
  color.value = value;
  customColor.value = value;
  isCustomColor.value = false;
};

const openCustomColorPicker = () => {
  customColorInput.value?.click();
};

const handleCustomColorChange = (event) => {
  customColor.value = event.target.value;
  color.value = event.target.value;
  isCustomColor.value = true;
};

watch(
  () => props.modelValue,
  (open) => {
    if (!open) {
      return;
    }

    name.value = isEditSubcategory.value ? props.subcategory?.name || "" : "";
    icon.value = iconOptions.value[0]?.name || "";
    color.value = colorOptions.value[0]?.value || "#FF7043";
    customColor.value = color.value;
    isCustomColor.value = false;
    showAllIcons.value = false;
  },
  { immediate: true }
);

const submitForm = () => {
  if (!name.value.trim()) {
    window.alert(t("categories.nameRequired"));
    return;
  }

  emit("submit", {
    name: name.value.trim(),
    icon: icon.value,
    color: color.value,
  });
};
</script>

<style scoped>
.overlay {
  position: fixed;
  inset: 0;
  background: var(--overlay);
  display: flex;
  justify-content: center;
  align-items: flex-end;
  z-index: 9999;
  padding-top: 32px;
}

.dialog {
  position: relative;
  width: 100%;
  max-width: 520px;
  max-height: 92dvh;
  padding: 22px 18px 18px;
  overflow-y: auto;
}

.closeBtn {
  position: absolute;
  top: 16px;
  right: 16px;
  border: var(--pixel-border) solid var(--border);
  border-radius: 8px;
  background: var(--surface-soft);
  width: 38px;
  height: 38px;
  cursor: pointer;
}

.dialogHeader {
  display: flex;
  flex-direction: column;
  gap: 4px;
  margin-bottom: 16px;
}

.dialogTitle {
  font-size: 1.15rem;
  font-weight: 700;
}

.dialogMeta {
  color: var(--muted);
  font-size: 0.82rem;
}

.contentSection {
  padding: 16px;
  display: flex;
  flex-direction: column;
  gap: 16px;
  background: var(--surface-soft);
}

.detailRow,
.inputGroup {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.detailLabel {
  color: var(--muted);
  font-size: 0.82rem;
  font-weight: 700;
}

.detailValue {
  font-weight: 700;
}

.rowHeader {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
}

.toggleMoreButton {
  border: none;
  background: transparent;
  color: var(--muted);
  cursor: pointer;
  font-size: 0.78rem;
  font-weight: 700;
}

.iconGrid {
  display: grid;
  grid-template-columns: repeat(5, minmax(0, 1fr));
  gap: 10px;
}

.iconOption {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 6px;
  border: var(--pixel-border) solid var(--border);
  border-radius: 8px;
  background: var(--surface);
  min-height: 78px;
  cursor: pointer;
  padding: 10px 6px;
  box-shadow: 2px 2px 0 var(--shadow);
}

.iconOption.selected {
  background: var(--accent);
  color: var(--accent-contrast);
}

.iconLabel {
  font-size: 0.7rem;
}

.colorPalette {
  display: grid;
  grid-template-columns: repeat(5, minmax(0, 1fr));
  gap: 10px;
}

.colorOption {
  width: 100%;
  aspect-ratio: 1;
  border: var(--pixel-border) solid var(--border);
  border-radius: 8px;
  cursor: pointer;
  box-shadow: 2px 2px 0 var(--shadow);
}

.colorOption.selected {
  transform: translate(1px, 1px);
  box-shadow: 1px 1px 0 var(--shadow);
}

.customColorOption {
  display: inline-flex;
  align-items: center;
  justify-content: center;
}

.inheritBox,
.previewBox {
  border: 2px solid var(--border);
  border-radius: 8px;
  padding: 12px 14px;
  background: var(--surface);
}

.inheritBox {
  color: var(--muted);
  font-size: 0.82rem;
  line-height: 1.6;
}

.previewBox {
  display: flex;
  align-items: center;
  gap: 12px;
}

.previewIcon {
  border: 2px solid var(--border);
  border-radius: 8px;
  padding: 8px;
}

.previewTextGroup {
  display: flex;
  flex-direction: column;
  gap: 2px;
}

.previewText {
  font-weight: 700;
}

.previewHint {
  font-size: 0.82rem;
  color: var(--muted);
}

.actionButtons {
  display: flex;
  justify-content: flex-end;
  margin-top: 16px;
}

@media (max-width: 480px) {
  .iconGrid {
    grid-template-columns: repeat(4, minmax(0, 1fr));
  }

  .colorPalette {
    grid-template-columns: repeat(5, minmax(0, 1fr));
  }
}
</style>
