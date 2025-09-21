<template>
  <button class="button" :style="{ backgroundColor: type.color }">
    <div class="buttonContent">
      <span class="label">{{ type.type }}</span>
      <q-icon :name="type.icon" size="32px" color="white" />
      <span>{{ type.totalPay }}</span>
    </div>
  </button>
</template>

<script setup>
defineProps({
  type: {
    type: Object,
    required: true, //必須傳直
    // default: () => ({ type: "未知", icon: "", color: "#ccc" }), 預設
  },
});
</script>

<style scoped>

.button {
  width: 100%;
  height: 150px;
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden; /* 避免文字太長撐破 */

  color: #ffffff;
  font-size: 17px;
  border: 1px solid;
  border-radius: 25px;
  cursor: pointer;
  padding: 10px;
  transition: all 0.1s;
}

.buttonContent {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  max-width: 100%;
}

.label {
  width: 100%;
  white-space: nowrap; /* 不換行 */
  overflow: hidden; /* 超出隱藏 */
  text-overflow: ellipsis; /* 變省略號 */
}

.button:active {
  position: relative;
  top: 2px;
}
</style>
