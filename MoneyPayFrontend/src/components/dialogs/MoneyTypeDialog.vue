<template>
  <div v-if="modelValue" class="overlay">
    <div class="dialog">
      <button class="closeBtn" @click="$emit('update:modelValue', false)">
        ✖
      </button>
      <h3>{{ type?.type }}</h3>
      <p>總支出：{{ type?.totalPay }}</p>

      <textarea v-model="price"></textarea>

      <p>選擇備註：</p>
      <div class="radio-inputs">
        <label v-for="remark in remarks">
          <input
            class="radio-input"
            type="radio"
            name="remark"
            :value="remark.remarkId"
            v-model="remarkId"
          />
          <span class="radio-tile">
            <span class="radio-label">{{ remark.remark }}</span>
          </span>
        </label>
      </div>

      <button @click="addTransaction">Submit</button>
    </div>
  </div>
</template>

<script setup>
import { ref, watch } from "vue";
import { typeApi } from "../../apiComposables/typeApiComposables";

const { getTypeRemarkApi, addTypePay } = typeApi();
const remarks = ref([]);

const remarkId = ref("");
const price = ref("");

const addTransaction = async () => {
  try {
    const token = localStorage.getItem("token");
    if (!token) throw new Error("No token found");
    const result = await addTypePay(token, {
      price: price.value,
      remarkId: remarkId.value,
    });
  } catch (e) {
    console.error("addTransaction Error", e);
  }
};

const props = defineProps({
  modelValue: Boolean, // 用 v-model 控制開關
  type: Object, // 父層傳進來的資料
});

// 使用 watch 監聽 modelValue（Dialog 開啟時抓資料）
watch(
  () => props.modelValue,
  async (open) => {
    if (open && props.type?.id) {
      try {
        const token = localStorage.getItem("token");
        if (!token) throw new Error("No token found");

        const result = await getTypeRemarkApi(token, props.type.id);
        remarks.value = result;
      } catch (e) {
        console.error("GetTypeRemark Error", e);
      }
    }
  }
);
</script>

<style scoped>
.overlay {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.5); /* 背景半透明 */
  display: flex;
  justify-content: center;
  align-items: flex-end; /* 從底部滑出 */
  backdrop-filter: blur(6px); /* 模糊背景 */
}

.dialog {
  width: 100%;
  height: 90%;
  background: white;
  border-radius: 16px 16px 0 0;
  padding: 20px;
  animation: slideUp 0.3s ease;
  position: relative;
}

.closeBtn {
  position: absolute;
  top: 10px; /* 父元素上方 10px，右方 10px。 */
  right: 10px;
  border: none;
  background: transparent;
  font-size: 20px;
  cursor: pointer;
}

@keyframes slideUp {
  from {
    transform: translateY(100%);
  }
  to {
    transform: translateY(0);
  }
}

/* From Uiverse.io by Yaya12085 */
.radio-inputs {
  display: flex;
  justify-content: center;
  align-items: center;
  max-width: 350px;
  -webkit-user-select: none;
  -moz-user-select: none;
  -ms-user-select: none;
  user-select: none;
}

.radio-inputs > * {
  margin: 6px;
}

.radio-tile {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 80px;
  min-height: 80px;
  border-radius: 0.5rem;
  border: 2px solid #b5bfd9;
  background-color: #fff;
  box-shadow: 0 5px 10px rgba(0, 0, 0, 0.1);
  transition: 0.15s ease;
  cursor: pointer;
  position: relative;
}

.radio-tile:hover {
  border-color: #2260ff;
}

.radio-label {
  color: #707070;
  transition: 0.375s ease;
  text-align: center;
  font-size: 13px;
}

.radio-input {
  clip: rect(0 0 0 0);
  -webkit-clip-path: inset(100%);
  clip-path: inset(100%);
  height: 1px;
  position: absolute;
  white-space: nowrap;
  width: 1px;
}
</style>
