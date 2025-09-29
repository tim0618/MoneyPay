<template>
  <div v-if="modelValue" class="overlay">
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

      <h3 class="dialog-title">編輯分類</h3>

      <div class="content-section">
        <p class="section-title">分類資訊</p>

        <div class="detail-row editable">
          <label for="typeName" class="detail-label">名稱：</label>
          <input
            id="typeName"
            type="text"
            :value="type.type"
            @input="$emit('update:type-name', $event.target.value)"
            class="input-editable"
            placeholder="請輸入分類名稱"
          />
        </div>

        <div class="detail-row">
          <span class="detail-label">圖標：</span>
          <q-icon
            class="detail-value icon-display"
            :style="{ background: type.color }"
            style="padding: 5px"
            :name="type.icon"
            size="24px"
            color="white"
          />
        </div>
        <div class="detail-row">
          <span class="detail-label">顏色：</span>
          <span
            class="detail-value color-preview"
            :style="{ backgroundColor: type.color }"
          ></span>
        </div>
      </div>

      <div class="content-section remarks-section">
        <p class="section-title">子分類：</p>
        <div class="remarks-grid">
          <label v-for="remark in remarks" class="remark-tag">
            {{ remark.remark }}
          </label>
        </div>
      </div>

      <div class="action-buttons">
        <button class="btn btn-save" @click="saveTypeDetails">
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
            <path
              d="M19 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h11l5 5v11a2 2 0 0 1-2 2z"
            ></path>
            <polyline points="17 21 17 13 7 13 7 21"></polyline>
            <polyline points="7 3 7 8 15 8"></polyline>
          </svg>
          儲存變更
        </button>
        <!-- <button class="btn btn-delete" @click="deleteType">
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
            <polyline points="3 6 5 6 21 6"></polyline>
            <path
              d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2"
            ></path>
            <line x1="10" y1="11" x2="10" y2="17"></line>
            <line x1="14" y1="11" x2="14" y2="17"></line>
          </svg>
          刪除分類
        </button> -->
      </div>
    </div>
  </div>
</template>
<style scoped>
/* 基礎疊層和滑動動畫 (與您原有的相同，但增加了 z-index) */
.overlay {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.7); /* 加深背景，突出對話框 */
  display: flex;
  justify-content: center;
  align-items: flex-end;
  backdrop-filter: blur(8px); /* 增加模糊程度 */
  z-index: 9999;
}
.dialog {
  width: 100%;
  max-width: 500px; /* 限制最大寬度，讓桌面版好看 */
  height: 90%;
  background: #ffffff;
  border-radius: 16px 16px 0 0;
  padding: 30px 20px 20px 20px; /* 調整 padding，為標題留出空間 */
  animation: slideUp 0.3s cubic-bezier(0.34, 1.56, 0.64, 1); /* 稍微彈跳的動畫 */
  position: relative;
  box-shadow: 0 -10px 30px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
  overflow-y: auto; /* 內容過多時可捲動 */
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

/* --- 新增/優化樣式 --- */

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

.detail-value {
  color: #1a1a1a;
  font-weight: 600;
}

/* 可編輯名稱的樣式 */
.input-editable {
  flex-grow: 1;
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 8px;
  transition: border-color 0.3s, box-shadow 0.3s;
  font-size: 1rem;
  font-weight: 600;
  color: #1a1a1a;
  text-align: right;
}

.input-editable:focus {
  border-color: #4a90e2; /* 藍色焦點邊框 */
  box-shadow: 0 0 0 3px rgba(74, 144, 226, 0.2);
  outline: none;
  background-color: #fff;
}

/* 圖標與顏色顯示 */
.icon-display {
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: white;
  font-size: 1.2rem;
}
.color-preview {
  width: 50px;
  height: 15px;
  border-radius: 4px;
  border: 1px solid #ccc;
}

/* 子分類標籤樣式 */
.remarks-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  padding-top: 5px;
}

.remark-tag {
  background: #e6e6fa; /* 淺紫色背景 */
  color: #4b0082; /* 深紫色文字 */
  padding: 6px 12px;
  border-radius: 20px;
  font-size: 0.85rem;
  font-weight: 500;
  cursor: default;
  transition: background 0.2s;
}
.remark-tag:hover {
  background: #d4c2f9;
}

/* 底部操作按鈕樣式 */
.action-buttons {
  display: flex;
  justify-content: space-between;
  gap: 15px;
  margin-top: auto; /* 推到底部 */
  padding-top: 20px;
  border-top: 1px solid #f0f0f0;
}

.btn {
  flex: 1;
  padding: 12px 20px;
  border: none;
  border-radius: 10px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: background-color 0.3s, transform 0.1s, box-shadow 0.3s;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 8px;
}

.btn-save {
  background-color: #4a90e2; /* 藍色 */
  color: white;
}

.btn-save:hover {
  background-color: #3b7cd1;
  box-shadow: 0 4px 15px rgba(74, 144, 226, 0.4);
}

.btn-delete {
  background-color: #ff6b6b; /* 紅色 */
  color: white;
}

.btn-delete:hover {
  background-color: #e65a5a;
  box-shadow: 0 4px 15px rgba(255, 107, 107, 0.4);
}

.btn:active {
  transform: scale(0.98);
}
</style>

<script setup>
import { ref, watch } from "vue";
import { typeApi } from "../../apiComposables/typeApiComposables";

const { getTypeRemarkApi } = typeApi();
const remarks = ref("");
const props = defineProps({
  modelValue: Boolean,
  type: Object,
});
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
