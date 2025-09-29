<template>
  <div
    v-if="modelValue"
    class="overlay"
    @click.self="$emit('update:modelValue', false)"
  >
    <div class="dialog">
      <button class="closeBtn" @click="$emit('update:modelValue', false)">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          width="24"
          height="24"
          viewBox="0 0 24 24"
          fill="none"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
        >
          <line x1="18" y1="6" x2="6" y2="18"></line>
          <line x1="6" y1="6" x2="18" y2="18"></line>
        </svg>
      </button>

      <h3 class="dialog-title">新增交易 - {{ type?.type }}</h3>

      <div class="content-section">
        <p class="section-title">交易金額</p>

        <div class="detail-row">
          <span class="detail-label">本分類總支出：</span>
          <span class="detail-value text-red">{{ type?.totalPay || 0 }}</span>
        </div>

        <div class="detail-row editable input-amount-row">
          <label for="priceInput" class="detail-label input-label-large"
            >輸入金額 (NTD)：</label
          >
          <textarea
            id="priceInput"
            v-model="price"
            @input="price = price.replace(/[^0-9]/g, '')"
            class="price-input input-editable"
            placeholder="0"
            rows="1"
          ></textarea>
        </div>
      </div>

      <div class="content-section remarks-section">
        <p class="section-title">選擇備註 (子分類)：</p>

        <div class="remarks-grid radio-inputs-container">
          <label
            v-for="remark in remarks"
            class="remark-input-label"
            :key="remark.remarkId"
          >
            <input
              class="radio-input"
              type="radio"
              name="remark"
              :value="remark.remarkId"
              v-model="remarkId"
            />
            <span class="radio-tile radio-tile-modern">
              <span class="radio-label">{{ remark.remark }}</span>
            </span>
          </label>
        </div>
      </div>

      <div class="action-buttons justify-center">
        <button class="btn btn-save" @click="addTransaction">
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="18"
            height="18"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
          >
            <path d="M12 5v14M5 12h14"></path>
          </svg>
          確認提交
        </button>
      </div>
    </div>
  </div>

  <!-- <p>UpdateTypePay</p>
  <p>DeleteTypePay</p> -->
</template>
<script setup>
import { ref, watch } from "vue";
import { typeApi } from "../../apiComposables/typeApiComposables";

const props = defineProps({
  modelValue: Boolean, // 用 v-model 控制開關
  type: Object, // 父層傳進來的資料
});
const emit = defineEmits(["update:modelValue", "refreshData"]);

const { getTypeRemarkApi, addTypePay } = typeApi();
const remarks = ref([]);
const remarkId = ref("");
const price = ref("");

const addTransaction = async () => {
  if (!price.value || price.value <= 0) {
    (price.value = ""), (remarkId.value = "");
    alert("填寫金額錯誤");
    return;
  }
  if (!remarkId.value) {
    (price.value = ""), (remarkId.value = "");
    alert("尚未點選備註");
    return;
  }
  try {
    const token = localStorage.getItem("token");
    if (!token) throw new Error("No token found");
    const result = await addTypePay(token, {
      price: price.value,
      remarkId: remarkId.value,
    });
    emit("update:modelValue", false);
    emit("refreshData");
    (price.value = ""), (remarkId.value = "");
  } catch (e) {
    console.error("addTransaction Error", e);
  }
};

// 使用 watch 監聽 modelValue（Dialog 開啟時抓資料）
watch(
  () => props.modelValue,
  async (open) => {
    if (open && props.type?.typeId) {
      try {
        const token = localStorage.getItem("token");
        if (!token) throw new Error("No token found");

        const result = await getTypeRemarkApi(token, props.type.typeId);
        remarks.value = result;
      } catch (e) {
        console.error("GetTypeRemark Error", e);
      }
    }
  }
);
</script>

<style scoped>
/* 基礎疊層和滑動動畫 */
.overlay {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: flex-end;
  backdrop-filter: blur(8px);
  z-index: 9999;
}
.dialog {
  width: 100%;
  max-width: 500px;
  height: 90%;
  background: #ffffff;
  border-radius: 16px 16px 0 0;
  padding: 30px 20px 20px 20px;
  animation: slideUp 0.3s cubic-bezier(0.34, 1.56, 0.64, 1);
  position: relative;
  box-shadow: 0 -10px 30px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  overflow-y: auto;
}
.closeBtn {
  position: absolute;
  top: 15px;
  right: 15px;
  border: none;
  background: transparent;
  cursor: pointer;
  color: #333;
  padding: 5px;
  border-radius: 50%;
  transition: background 0.2s;
}
.closeBtn:hover {
  background: #eee;
}
.closeBtn svg {
  display: block;
  width: 24px;
  height: 24px;
}

@keyframes slideUp {
  from {
    transform: translateY(100%);
  }
  to {
    transform: translateY(0);
  }
}

/* --- 結構與排版樣式 --- */

.dialog-title {
  font-size: 1.5rem;
  font-weight: 700;
  color: #1a1a1a;
  margin-bottom: 20px;
  border-bottom: 2px solid #f0f0f0;
  padding-bottom: 10px;
}

.content-section {
  background: #f8f8f8;
  padding: 15px;
  border-radius: 12px;
  margin-bottom: 20px;
}

.section-title {
  font-size: 1.1rem;
  font-weight: 600;
  color: #444;
  margin-bottom: 15px;
  padding-bottom: 5px;
  border-bottom: 1px dashed #e0e0e0;
}

.detail-row {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 8px 0;
  font-size: 0.95rem;
}

.detail-label {
  font-weight: 500;
  color: #666;
  min-width: 60px;
}
.input-label-large {
  font-size: 1.1rem;
  font-weight: 600;
  color: #333;
}

.detail-value {
  color: #1a1a1a;
  font-weight: 600;
}
.text-red {
  color: #ff4d4d;
  font-size: 1.2rem;
}

/* 可編輯金額輸入框的樣式 */
.price-input {
  flex-grow: 1;
  height: auto; /* 讓單行文字框更合適 */
  min-height: 40px;
  resize: none; /* 禁用拖拉改變大小 */
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 8px;
  transition: border-color 0.3s, box-shadow 0.3s;
  font-size: 1.8rem; /* 放大金額數字 */
  font-weight: 700;
  color: #1a1a1a;
  text-align: right;
  line-height: 1.2;
}

.price-input:focus {
  border-color: #2260ff; /* 藍色焦點邊框 */
  box-shadow: 0 0 0 3px rgba(34, 96, 255, 0.2);
  outline: none;
  background-color: #fff;
}

/* 子分類標籤 (單選按鈕) 樣式 */
.remarks-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  padding-top: 5px;
}

.remark-input-label {
  position: relative;
  cursor: pointer;
}

/* 沿用原有的 radio-input 隱藏邏輯 */
.radio-input {
  clip: rect(0 0 0 0);
  clip-path: inset(100%);
  height: 1px;
  position: absolute;
  white-space: nowrap;
  width: 1px;
}

.radio-tile-modern {
  background: #e9e9e9; /* 淺灰色背景 */
  color: #444;
  padding: 8px 15px;
  border-radius: 20px;
  font-size: 0.9rem;
  font-weight: 500;
  transition: background 0.2s, border 0.2s;
  border: 2px solid transparent;
  display: block; /* 確保它能響應點擊 */
}

/* 選中狀態 */
.radio-input:checked + .radio-tile-modern {
  background: #4a90e2; /* 藍色背景 */
  color: white;
  border: 2px solid #2260ff;
  box-shadow: 0 4px 10px rgba(74, 144, 226, 0.3);
}

.radio-tile-modern:hover {
  background: #d4d4d4;
}

/* 底部操作按鈕樣式 */
.action-buttons {
  display: flex;
  justify-content: center; /* 置中 */
  gap: 15px;
  margin-top: auto;
  padding-top: 20px;
  border-top: 1px solid #f0f0f0;
}

.btn {
  flex: 0 0 180px; /* 限制寬度 */
  padding: 12px 20px;
  border: none;
  border-radius: 10px;
  font-size: 1.1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background-color 0.3s, transform 0.1s, box-shadow 0.3s;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
}

.btn-save {
  background-color: #4caf50; /* 綠色，代表新增/確認 */
  color: white;
  box-shadow: 0 4px 15px rgba(76, 175, 80, 0.4);
}

.btn-save:hover {
  background-color: #45a049;
}

.btn:active {
  transform: scale(0.98);
}
</style>
