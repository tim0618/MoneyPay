<template>
  <div
    v-if="modelValue"
    class="overlay"
    @click.self="$emit('update:modelValue', false)"
  >
    <div class="dialog" :style="{ '--theme-color': type?.color || '#00ffcc' }">
      <div class="dialog-header-deco"></div>

      <button class="closeBtn" @click="$emit('update:modelValue', false)">
        [ X ]
      </button>

      <h3 class="dialog-title">
        <span class="blinking-cursor">></span> ADD_TRANSACTION :: {{ type?.typeName }}
      </h3>

      <div class="content-section">
        <div class="detail-row">
           <span class="detail-label">[ TOTAL ]</span>
          <span class="detail-value text-red">{{ type?.totalPay || 0 }}</span>
        </div>

        <div class="input-group q-mt-md">
          <label for="priceInput" class="input-label">INPUT_AMOUNT //</label>
          <div class="input-wrapper">
             <span class="currency-symbol">$</span>
             <input
              id="priceInput"
              v-model="price"
              @input="price = price.replace(/[^0-9]/g, '')"
              class="cyber-input"
              placeholder="0"
              inputmode="numeric" 
              autocomplete="off"
            />
          </div>
        </div>
      </div>

      <div class="content-section remarks-section">
        <p class="section-title">SELECT_TAG //</p>

        <div class="remarks-grid">
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
            <div class="cyber-tile">
              {{ remark.remark }}
            </div>
          </label>
          
          <div v-if="remarks.length === 0" class="no-data-text">
            // NULL_DATA
          </div>
        </div>
      </div>

      <div class="action-buttons">
        <button class="cyber-btn-save" @click="handleAdd">
          INITIATE_TRANSFER
        </button>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* 基礎疊層 */
.overlay {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.85); /* 背景更黑 */
  display: flex;
  justify-content: center;
  align-items: flex-end;
  backdrop-filter: blur(5px); /* 模糊背景 */
  z-index: 9999;
}

/* 對話框本體 */
.dialog {
  width: 100%;
  max-width: 500px;
  height: auto;
  max-height: 90vh;
  
  /* Cyberpunk 風格面板 */
  background: #0a0a0a;
  border-top: 2px solid var(--theme-color);
  border-left: 1px solid #333;
  border-right: 1px solid #333;
  
  /* 直角 */
  border-radius: 0; 
  padding: 30px 20px 20px 20px;
  animation: slideUp 0.3s cubic-bezier(0.2, 1, 0.3, 1);
  position: relative;
  box-shadow: 0 -5px 20px rgba(0, 255, 204, 0.1);
  display: flex;
  flex-direction: column;
}

/* 頂部裝飾條 (斜紋) */
.dialog-header-deco {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 4px;
  background: repeating-linear-gradient(
    45deg,
    var(--theme-color),
    var(--theme-color) 10px,
    transparent 10px,
    transparent 20px
  );
  opacity: 0.5;
}

.closeBtn {
  position: absolute;
  top: 15px;
  right: 15px;
  border: none;
  background: transparent;
  color: #666;
  font-family: 'Share Tech Mono', monospace;
  font-size: 1.2rem;
  cursor: pointer;
}

.dialog-title {
  font-size: 1.2rem;
  color: var(--theme-color);
  margin-bottom: 20px;
  text-transform: uppercase;
  border-bottom: 1px solid #333;
  padding-bottom: 10px;
}

.blinking-cursor {
  animation: blink 1s step-end infinite;
}
@keyframes blink { 50% { opacity: 0; } }

/* 內容區塊 */
.content-section {
  background: rgba(255, 255, 255, 0.03); /* 極淡的白，提亮背景 */
  padding: 15px;
  border: 1px solid #222;
  margin-bottom: 20px;
}

.section-title, .input-label, .detail-label {
  color: #888;
  font-size: 0.9rem;
  margin-bottom: 10px;
  display: block;
}

.detail-value {
  float: right;
  font-size: 1.2rem;
  font-weight: bold;
}
.text-red { color: #ff4d4d; text-shadow: 0 0 5px #ff4d4d; }

/* 輸入框設計 */
.input-wrapper {
  display: flex;
  align-items: center;
  border-bottom: 2px solid var(--theme-color);
  background: rgba(0,0,0,0.3);
}

.currency-symbol {
  font-size: 1.5rem;
  color: var(--theme-color);
  padding-left: 10px;
}

.cyber-input {
  width: 100%;
  background: transparent;
  border: none;
  color: white;
  font-size: 2.5rem; /* 超大數字 */
  text-align: right;
  padding: 10px;
  font-family: 'Share Tech Mono', monospace;
}
.cyber-input::placeholder { color: #333; }

/* 標籤 (Radio Tiles) */
.remarks-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(80px, 1fr));
  gap: 10px;
}

.radio-input { display: none; }

.cyber-tile {
  background: #111;
  color: #888;
  border: 1px solid #333;
  padding: 10px 5px;
  text-align: center;
  font-size: 0.9rem;
  cursor: pointer;
  transition: all 0.2s;
  /* 直角 */
  border-radius: 0; 
}

/* 選中狀態 */
.radio-input:checked + .cyber-tile {
  background: rgba(0, 255, 204, 0.1);
  border-color: var(--theme-color);
  color: var(--theme-color);
  box-shadow: 0 0 8px var(--theme-color);
  text-shadow: 0 0 5px var(--theme-color);
}

.no-data-text { color: #444; font-size: 0.8rem; }

/* 底部按鈕 */
.action-buttons {
  margin-top: auto;
  padding-top: 20px;
}

.cyber-btn-save {
  width: 100%;
  padding: 15px;
  background: transparent;
  border: 1px solid var(--theme-color);
  color: var(--theme-color);
  font-family: 'Share Tech Mono', monospace;
  font-size: 1.2rem;
  font-weight: bold;
  cursor: pointer;
  text-transform: uppercase;
  letter-spacing: 2px;
  transition: all 0.3s;
}

.cyber-btn-save:hover, .cyber-btn-save:active {
  background: var(--theme-color);
  color: #000;
  box-shadow: 0 0 20px var(--theme-color);
}

@keyframes slideUp {
  from { transform: translateY(100%); }
  to { transform: translateY(0); }
}
</style>
<script setup>
import { ref, watch } from "vue";
// 1. 引入 Store
import { useMoneyStore } from "../../stores/moneyStore";

const props = defineProps({
  modelValue: Boolean,
  type: Object,
});

// refreshData 不需要了，因為 Pinia 會自動更新畫面
const emit = defineEmits(["update:modelValue"]);

// 2. 初始化 Store
const moneyStore = useMoneyStore();

const remarks = ref([]);
const remarkId = ref("");
const price = ref("");

const handleAdd = () => {
  // 驗證
  if (!price.value || Number(price.value) <= 0) {
    alert("填寫金額錯誤");
    return;
  }
  if (!remarkId.value) {
    alert("尚未點選備註");
    return;
  }

  // 3. 呼叫 Store Action
  moneyStore.addTransaction({
    typeId: props.type.typeId, // 告訴 Store 是哪個分類
    price: price.value,
    remarkId: remarkId.value
  });

  // 4. 重置與關閉
  price.value = "";
  remarkId.value = "";
  emit("update:modelValue", false);
  
  // 這裡不需要 emit('refreshData')，因為父層已經綁定 Pinia Store 了
};

// 監聽 Dialog 開啟
watch(
  () => props.modelValue,
  (open) => {
    if (open && props.type?.typeId) {
      // 5. 改從 Store 拿備註資料
      remarks.value = moneyStore.getRemarksByTypeId(props.type.typeId);
      
      // 重置輸入框
      price.value = "";
      remarkId.value = "";
    }
  }
);
</script>
