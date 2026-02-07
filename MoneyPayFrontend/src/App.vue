<template>
  <div class="app-container">
    <router-view />
  </div>
</template>
<script setup>
import { onMounted } from 'vue';

onMounted(() => {
  // 禁止雙指縮放 (Pinch to Zoom)
  document.addEventListener('gesturestart', function (e) {
    e.preventDefault();
  });

  // 禁止雙擊縮放 (Double Tap to Zoom)
  let lastTouchEnd = 0;
  document.addEventListener('touchend', function (event) {
    const now = (new Date()).getTime();
    if (now - lastTouchEnd <= 300) {
      event.preventDefault();
    }
    lastTouchEnd = now;
  }, false);
});
</script>