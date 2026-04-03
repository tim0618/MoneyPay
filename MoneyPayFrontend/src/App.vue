<template>
  <div class="app-container">
    <router-view />
  </div>
</template>

<script setup>
import { watchEffect } from "vue";
import { useAppPreferences } from "./composables/useAppPreferences";

const { theme, locale } = useAppPreferences();

watchEffect(() => {
  document.documentElement.dataset.theme = theme.value;
  document.documentElement.lang = locale.value;
  document.documentElement.style.colorScheme =
    theme.value === "dark" ? "dark" : "light";
});
</script>
