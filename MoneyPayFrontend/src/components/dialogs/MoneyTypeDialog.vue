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
        <span class="page-eyebrow">{{ t("home.eyebrow") }}</span>
        <h3 class="dialogTitle">
          {{ t("home.addRecordTitle", { name: type?.name || "--" }) }}
        </h3>
      </div>

      <div class="contentSection pixel-card">
        <div class="detailRow">
          <span class="detailLabel">{{ t("home.currentTotal") }}</span>
          <span class="detailValue">{{ formatCurrency(type?.amount) }}</span>
        </div>

        <label class="field-stack">
          <span class="field-label">{{ t("home.amountLabel") }}</span>
          <input
            v-model="price"
            class="pixel-input priceInput"
            type="text"
            inputmode="numeric"
            placeholder="0"
            @input="price = price.replace(/[^0-9]/g, '')"
          />
        </label>
      </div>

      <div class="contentSection pixel-card">
        <div class="remarkHeader">
          <span class="detailLabel">{{ t("home.remarkLabel") }}</span>
        </div>

        <div v-if="remarks.length === 0" class="emptyState">
          {{ t("home.noRemark") }}
        </div>
        <div v-else class="remarksGrid">
          <label
            v-for="remark in remarks"
            :key="remark.remarkId"
            class="remarkInputLabel"
          >
            <input
              v-model="remarkId"
              class="radioInput"
              type="radio"
              name="remark"
              :value="remark.remarkId"
            />
            <span class="radioTile">
              {{ remark.name || remark.remark }}
            </span>
          </label>
        </div>
      </div>

      <div class="actionButtons">
        <button class="pixel-button" type="button" @click="addTransaction">
          {{ t("home.addRecord") }}
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch } from "vue";
import { typeApi } from "../../apiComposables/typeApiComposables";
import { useAppPreferences } from "../../composables/useAppPreferences";

const props = defineProps({
  modelValue: Boolean,
  type: Object,
});

const emit = defineEmits(["update:modelValue", "refresh-data"]);

const { getTypeRemarkApi, addTypePay } = typeApi();
const { t, formatCurrency } = useAppPreferences();
const remarks = ref([]);
const remarkId = ref("");
const price = ref("");

const addTransaction = async () => {
  if (!price.value || Number(price.value) <= 0) {
    price.value = "";
    remarkId.value = "";
    window.alert(t("home.invalidAmount"));
    return;
  }

  if (!remarkId.value) {
    price.value = "";
    remarkId.value = "";
    window.alert(t("home.chooseRemark"));
    return;
  }

  try {
    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found");
    }

    await addTypePay(token, {
      price: price.value,
      remarkId: remarkId.value,
    });

    emit("update:modelValue", false);
    emit("refresh-data");
    price.value = "";
    remarkId.value = "";
  } catch (error) {
    console.error("Add Transaction Error", error);
    window.alert(t("home.loadError"));
  }
};

watch(
  () => props.modelValue,
  async (open) => {
    if (open && props.type?.id) {
      try {
        const token = localStorage.getItem("token");
        if (!token) {
          throw new Error("No token found");
        }

        remarks.value = await getTypeRemarkApi(token, props.type.id);
      } catch (error) {
        console.error("Get Type Remark Error", error);
      }
    } else if (!open) {
      remarks.value = [];
      remarkId.value = "";
      price.value = "";
    }
  }
);
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
  max-width: 500px;
  max-height: 92dvh;
  padding: 22px 18px 18px;
  overflow-y: auto;
  background: var(--surface);
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
  gap: 6px;
  margin-bottom: 16px;
}

.dialogTitle {
  font-size: 1.15rem;
  font-weight: 700;
}

.contentSection {
  padding: 16px;
  margin-bottom: 16px;
  background: var(--surface-soft);
}

.detailRow {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
  margin-bottom: 14px;
}

.detailLabel {
  color: var(--muted);
  font-size: 0.82rem;
  font-weight: 700;
}

.detailValue {
  font-weight: 700;
}

.priceInput {
  font-size: 1.35rem;
  font-weight: 700;
  text-align: right;
}

.emptyState {
  color: var(--muted);
  font-size: 0.84rem;
}

.remarksGrid {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
}

.remarkInputLabel {
  position: relative;
  cursor: pointer;
}

.radioInput {
  position: absolute;
  inset: 0;
  opacity: 0;
}

.radioTile {
  display: block;
  border: var(--pixel-border) solid var(--border);
  border-radius: 8px;
  background: var(--surface);
  padding: 9px 12px;
  box-shadow: 2px 2px 0 var(--shadow);
}

.radioInput:checked + .radioTile {
  background: var(--accent);
  color: var(--accent-contrast);
}

.actionButtons {
  display: flex;
  justify-content: flex-end;
}
</style>
