<template>
  <div v-if="modelValue" class="overlay">
    <div class="dialog">
      <button class="closeBtn" @click="$emit('update:modelValue', false)">
        ✖
      </button>
      <p>分類</p>
      <div>
        <p>名稱：{{ type.type }}</p>
        <p>圖標：{{ type.icon }}</p>
        <p>顏色：{{ type.color }}</p>
      </div>
      <div>
        <p>子分類：</p>
        <label v-for="remark in remarks">{{ remark.remark }} </label>
      </div>

      <div>
        <button>Save</button>
        <button>Delete</button>
      </div>
    </div>
  </div>
</template>
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
<style>
.overlay {
  position: fixed;
  inset: 0;
  background: rgba(0, 0, 0, 0.5); /* 背景半透明 */
  display: flex;
  justify-content: center;
  align-items: flex-end; /* 從底部滑出 */
  backdrop-filter: blur(6px); /* 模糊背景 */
  z-index: 9999; /* 給予一個夠高的 z-index */
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
</style>
