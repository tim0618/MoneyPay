<template>
  <div v-if="modelValue" class="overlay">
    <div class="dialog">
      <button class="closeBtn" @click="closeDialog">
        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
          <line x1="18" y1="6" x2="6" y2="18"></line>
          <line x1="6" y1="6" x2="18" y2="18"></line>
        </svg>
      </button>

      <h3 class="dialog-title">編輯分類</h3>

      <div class="content-section">
        <p class="section-title">分類資訊</p>

        <div class="detail-row editable">
          <label for="typeName" class="detail-label">名稱：</label>
          <input
            id="typeName"
            type="text"
            v-model="editingName"
            class="input-editable"
            placeholder="請輸入分類名稱"
          />
        </div>

        <div class="detail-row">
          <span class="detail-label">圖標：</span>
          <q-icon
            v-if="type?.icon"
            class="detail-value icon-display"
            :style="{ background: type.color }"
            style="padding: 5px"
            :name="type.icon"
            size="24px"
            color="white"
          />
        </div>
      </div>

      <div class="content-section remarks-section">
        <p class="section-title">子分類管理：</p>
        
        <div class="add-remark-row">
          <input 
            v-model="newRemarkText" 
            class="remark-input" 
            placeholder="輸入新子分類..." 
            @keyup.enter="handleAddRemark"
          />
          <button class="btn-small" @click="handleAddRemark">新增</button>
        </div>

        <div class="remarks-grid">
          <div v-for="remark in currentRemarks" :key="remark.remarkId" class="remark-tag-wrapper">
            <span class="remark-tag">{{ remark.remark }}</span>
            <span class="delete-x" @click="handleDeleteRemark(remark.remarkId)">×</span>
          </div>
          
          <div v-if="currentRemarks.length === 0" style="color:#999; font-size:12px; width:100%;">
            暫無子分類
          </div>
        </div>
      </div>

      <div class="action-buttons">
        <button class="btn btn-save" @click="handleSave">
          <svg xmlns="http://www.w3.org/2000/svg" width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"></path><polyline points="17 21 17 13 7 13 7 21"></polyline><polyline points="7 3 7 8 15 8"></polyline></svg>
          儲存變更
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch, computed } from "vue";
import { useMoneyStore } from "../../stores/moneyStore";

const props = defineProps({
  modelValue: Boolean,
  type: Object,
});

const emit = defineEmits(['update:modelValue']);
const moneyStore = useMoneyStore();

// 用來暫存編輯中的名稱
const editingName = ref("");
// 用來暫存新備註的輸入
const newRemarkText = ref("");

// 透過 computed 自動去 Store 抓最新的備註列表
// 這樣當我們 addRemark 時，畫面會自動更新
const currentRemarks = computed(() => {
  if (!props.type?.typeId) return [];
  return moneyStore.getRemarksByType(props.type.typeId);
});

// 當 Dialog 打開時，初始化資料
watch(
  () => props.modelValue,
  (isOpen) => {
    if (isOpen && props.type) {
      editingName.value = props.type.typeName; // 把目前的名稱帶入輸入框
      newRemarkText.value = ""; // 清空備註輸入框
    }
  }
);

// 新增備註
const handleAddRemark = () => {
  if (!newRemarkText.value.trim()) return;
  moneyStore.addRemark(props.type.typeId, newRemarkText.value);
  newRemarkText.value = ""; // 清空
};

// 刪除備註
const handleDeleteRemark = (remarkId) => {
  if (confirm("確定要刪除這個子分類嗎？")) {
    moneyStore.deleteRemark(remarkId);
  }
};

// 儲存所有變更 (主要是名稱)
const handleSave = () => {
  if (props.type?.typeId) {
    moneyStore.updateType(props.type.typeId, editingName.value);
  }
  closeDialog();
};

const closeDialog = () => {
  emit('update:modelValue', false);
};
</script>

<style scoped>
/* 讓背景變黑色的遮罩 */
.overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5); /* 半透明黑 */
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 9999; /* 確保在最上層 */
}

/* 彈窗本體 */
.dialog {
  background: white;
  padding: 20px;
  border-radius: 12px;
  width: 90%;
  max-width: 400px;
  position: relative;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
  max-height: 80vh; /* 避免太高超出螢幕 */
  overflow-y: auto; /* 內容太多可捲動 */
}

/* 關閉按鈕 */
.closeBtn {
  position: absolute;
  top: 10px;
  right: 10px;
  background: none;
  border: none;
  cursor: pointer;
  color: #666;
}

/* 其他輸入框樣式 */
.input-editable, .remark-input {
  width: 100%;
  padding: 8px;
  margin-top: 5px;
  border: 1px solid #ddd;
  border-radius: 4px;
}

.remarks-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
  margin-top: 10px;
}

.remark-tag-wrapper {
  background: #f0f0f0;
  padding: 4px 10px;
  border-radius: 16px;
  font-size: 14px;
  display: flex;
  align-items: center;
  gap: 5px;
}

.delete-x {
  cursor: pointer;
  color: #999;
  font-weight: bold;
}
.delete-x:hover {
  color: red;
}

.btn-save {
  margin-top: 20px;
  width: 100%;
  padding: 10px;
  background-color: #4CAF50;
  color: white;
  border: none;
  border-radius: 6px;
  cursor: pointer;
}
</style>
