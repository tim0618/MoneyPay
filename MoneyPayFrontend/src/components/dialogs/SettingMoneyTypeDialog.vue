<template>
  <div v-if="modelValue" class="overlay">
    <div class="dialog" :style="{ '--theme-color': type?.color || '#00ffcc' }">
      
      <div class="dialog-header">
        <span class="header-deco">SYSTEM_CONFIG //</span>
        <span class="header-text">EDIT_MODE</span>
        <button class="closeBtn" @click="closeDialog">[ ESC ]</button>
      </div>

      <div class="scroll-area">
        
        <div class="section-group">
          <div class="section-label">:: BASIC_INFO</div>
          
          <div class="input-row">
            <label class="cyber-label">TYPE_NAME</label>
            <input
              type="text"
              v-model="editingName"
              class="cyber-input"
              placeholder="ENTER_NAME..."
            />
          </div>

          <div class="input-row q-mt-md">
            <label class="cyber-label">ICON_PREVIEW</label>
            <div class="icon-preview-box">
              <q-icon
                v-if="type?.icon"
                :name="type.icon"
                size="32px"
                class="preview-icon"
              />
              <span class="hex-code">{{ type?.color }}</span>
            </div>
          </div>
        </div>

        <div class="section-group">
          <div class="section-label">:: SUB_CATEGORIES</div>
          
          <div class="add-remark-row">
            <input 
              v-model="newRemarkText" 
              class="cyber-input flex-grow" 
              placeholder="NEW_TAG_NAME..." 
              @keyup.enter="handleAddRemark"
            />
            <button class="btn-add-tag" @click="handleAddRemark">
              <q-icon name="add" /> ADD
            </button>
          </div>

          <div class="remarks-grid">
            <div v-for="remark in currentRemarks" :key="remark.remarkId" class="cyber-tag">
              <span class="tag-text">{{ remark.remark }}</span>
              <button class="tag-delete" @click="handleDeleteRemark(remark.remarkId)">×</button>
            </div>
            
            <div v-if="currentRemarks.length === 0" class="empty-state">
              // NULL_DATA: NO_SUB_CATEGORIES
            </div>
          </div>
        </div>
      
      </div> <div class="action-footer">
        <button class="btn-save" @click="handleSave">
          <span class="save-text">SAVE_CHANGES</span>
          <div class="loading-bar"></div>
        </button>
      </div>

    </div>
  </div>
</template>

<style scoped>
/* 遮罩 */
.overlay {
  position: fixed;
  inset: 0;
  background-color: rgba(0, 0, 0, 0.85);
  backdrop-filter: blur(5px);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 9999;
}

/* 彈窗本體 - 終端機風格 */
.dialog {
  background: #0a0a0a;
  border: 1px solid var(--theme-color);
  width: 90%;
  max-width: 450px;
  max-height: 85vh;
  display: flex;
  flex-direction: column;
  position: relative;
  box-shadow: 0 0 30px rgba(0,0,0,0.8), inset 0 0 20px rgba(0,0,0,0.5);
}

/* 頂部標題 */
.dialog-header {
  padding: 15px 20px;
  border-bottom: 1px solid #333;
  display: flex;
  align-items: center;
  background: rgba(255,255,255,0.02);
}

.header-deco { color: #555; margin-right: 10px; }
.header-text { color: var(--theme-color); font-weight: bold; letter-spacing: 2px; flex-grow: 1;}

.closeBtn {
  background: none;
  border: none;
  color: #666;
  cursor: pointer;
  font-family: 'Share Tech Mono', monospace;
  transition: color 0.3s;
}
.closeBtn:hover { color: #fff; }

/* 捲動區域 */
.scroll-area {
  padding: 20px;
  overflow-y: auto;
  flex-grow: 1;
}

/* 分區樣式 */
.section-group {
  margin-bottom: 30px;
  border-left: 2px solid #222;
  padding-left: 15px;
}

.section-label {
  color: #888;
  font-size: 0.9rem;
  margin-bottom: 15px;
  letter-spacing: 1px;
}

/* Cyber Input 樣式 */
.cyber-label {
  display: block;
  color: var(--theme-color);
  font-size: 0.8rem;
  margin-bottom: 5px;
  opacity: 0.8;
}

.cyber-input {
  width: 100%;
  background: transparent;
  border: none;
  border-bottom: 1px solid #444;
  color: white;
  padding: 8px 0;
  font-family: 'Share Tech Mono', monospace;
  font-size: 1.1rem;
  transition: all 0.3s;
}

.cyber-input:focus {
  border-bottom-color: var(--theme-color);
  box-shadow: 0 5px 5px -5px var(--theme-color);
}

/* Icon Preview */
.icon-preview-box {
  display: flex;
  align-items: center;
  gap: 15px;
  padding: 10px;
  border: 1px solid #222;
  background: rgba(0,0,0,0.3);
}
.preview-icon { color: var(--theme-color); filter: drop-shadow(0 0 5px var(--theme-color)); }
.hex-code { color: #555; font-size: 0.9rem; }

/* 子分類管理 */
.add-remark-row {
  display: flex;
  gap: 10px;
  align-items: flex-end;
  margin-bottom: 20px;
}

.flex-grow { flex-grow: 1; }

.btn-add-tag {
  background: #222;
  border: 1px solid #444;
  color: white;
  padding: 8px 15px;
  cursor: pointer;
  font-family: 'Share Tech Mono', monospace;
  transition: all 0.2s;
}
.btn-add-tag:hover {
  border-color: var(--theme-color);
  color: var(--theme-color);
}

/* 標籤矩陣 */
.remarks-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
}

.cyber-tag {
  display: flex;
  align-items: center;
  border: 1px solid var(--theme-color);
  background: rgba(0,0,0,0.5);
  padding: 5px 10px;
  color: #eee;
  font-size: 0.9rem;
  box-shadow: 0 0 5px rgba(0,0,0,0.2);
}

.tag-text { margin-right: 8px; }

.tag-delete {
  background: transparent;
  border: none;
  color: #666;
  cursor: pointer;
  font-weight: bold;
  font-size: 1.1rem;
  line-height: 1;
}
.tag-delete:hover { color: #ff4d4d; }

.empty-state { color: #444; font-size: 0.8rem; font-style: italic; }

/* 底部按鈕 */
.action-footer {
  padding: 20px;
  border-top: 1px solid #333;
}

.btn-save {
  width: 100%;
  padding: 15px;
  background: var(--theme-color);
  border: none;
  position: relative;
  cursor: pointer;
  overflow: hidden;
}

.save-text {
  color: #000;
  font-weight: bold;
  font-size: 1.1rem;
  letter-spacing: 2px;
  position: relative;
  z-index: 2;
}

.loading-bar {
  position: absolute;
  bottom: 0;
  left: 0;
  width: 100%;
  height: 4px;
  background: rgba(255,255,255,0.5);
  transform: translateX(-100%);
  transition: transform 0.3s;
}

.btn-save:hover .loading-bar {
  transform: translateX(0);
}

.btn-save:hover {
  box-shadow: 0 0 20px var(--theme-color);
}
</style>
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
