<template>
  <button
    class="cyber-button"
    :style="{ '--type-color': typeDetail.color }"
    @click="$emit('typeSelect', typeDetail)"
  >
    <div class="corner-tl"></div>
    <div class="corner-br"></div>

    <div class="buttonContent">
      <span class="label">{{ typeDetail.typeName }}</span>
      <q-icon
        class="icon-glow"
        style="padding: 10px 0px"
        :name="typeDetail.icon"
        size="32px"
      />
      <span class="amount">${{ typeDetail.totalPay }}</span>
    </div>
  </button>
</template>

<style scoped>
.cyber-button {
  /* 使用 CSS 變數承接原本的顏色 */
  --btn-bg: rgba(10, 10, 10, 0.8);
  
  position: relative;
  width: 100%;
  height: 120px; /* 稍微降低高度，讓比例更像控制面板 */
  
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;

  /* 核心風格 */
  background-color: var(--btn-bg);
  border: 1px solid #333; /* 預設暗邊框 */
  border-bottom: 2px solid var(--type-color); /* 底部亮條 */
  color: var(--type-color); /* 文字跟隨顏色 */
  
  border-radius: 0; /* 直角 */
  cursor: pointer;
  transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1);
  
  /* 加上掃描線紋路 */
  background-image: linear-gradient(transparent 50%, rgba(0,0,0,0.5) 50%);
  background-size: 100% 4px;
}

/* 懸停或點擊效果 */
.cyber-button:active {
  transform: translateY(2px);
  background-color: rgba(255, 255, 255, 0.05);
  box-shadow: 0 0 15px var(--type-color);
  border: 1px solid var(--type-color);
}

.buttonContent {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  z-index: 1;
}

.label {
  font-size: 0.9rem;
  letter-spacing: 1px;
  opacity: 0.8;
}

.amount {
  font-size: 1.1rem;
  font-weight: bold;
}

.icon-glow {
  filter: drop-shadow(0 0 2px var(--type-color));
}

/* 裝飾性角落標記 (Sci-fi UI 常見的角落框) */
.corner-tl {
  position: absolute;
  top: 0;
  left: 0;
  width: 6px;
  height: 6px;
  border-top: 2px solid var(--type-color);
  border-left: 2px solid var(--type-color);
}

.corner-br {
  position: absolute;
  bottom: 0;
  right: 0;
  width: 6px;
  height: 6px;
  border-bottom: 2px solid var(--type-color);
  border-right: 2px solid var(--type-color);
}
</style>

<script setup>
defineProps({
  typeDetail: {
    type: Object,
    required: true, //必須傳直
    // default: () => ({ type: "未知", icon: "", color: "#ccc" }), 預設
  },
});
</script>
