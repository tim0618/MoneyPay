<template>
  <q-layout view="hHh lpR fFf" class="mainLayout">
    <q-page-container class="pageContainer">
      <router-view />
    </q-page-container>

    <div class="floatingNavWrap">
      <nav class="navShell pixel-card" :aria-label="t('nav.home')">
        <button
          v-for="item in navItems"
          :key="item.path"
          class="navItem"
          :class="{ active: isActive(item.path) }"
          type="button"
          @click="goTo(item.path)"
        >
          <q-icon :name="item.icon" size="22px" />
          <span class="navLabel">{{ item.label }}</span>
        </button>
      </nav>
    </div>
  </q-layout>
</template>

<script setup>
import { computed } from "vue";
import { useRoute, useRouter } from "vue-router";
import { useAppPreferences } from "../composables/useAppPreferences";

const router = useRouter();
const route = useRoute();
const { t } = useAppPreferences();

const navItems = computed(() => [
  { path: "/home", label: t("nav.home"), icon: "home" },
  { path: "/settingType", label: t("nav.categories"), icon: "bookmark" },
  { path: "/graphics", label: t("nav.insights"), icon: "bar_chart" },
  { path: "/userSetting", label: t("nav.settings"), icon: "settings" },
]);

const goTo = (path) => {
  if (route.path === path) {
    return;
  }

  router.push(path);
};

const isActive = (path) => route.path === path;
</script>

<style scoped>
.mainLayout {
  background: transparent;
  min-height: 100vh;
  min-height: 100dvh;
  --footer-offset: calc(env(safe-area-inset-bottom, 0px) + 12px);
}

.pageContainer {
  min-height: 100vh;
  min-height: 100dvh;
  padding-bottom: calc(var(--bottom-nav-height) + var(--footer-offset) + 22px) !important;
}

.floatingNavWrap {
  position: fixed;
  left: 50%;
  bottom: var(--footer-offset);
  transform: translateX(-50%);
  width: min(calc(100vw - 20px), calc(var(--app-max-width) - 20px));
  width: min(calc(100dvw - 20px), calc(var(--app-max-width) - 20px));
  z-index: 2000;
  pointer-events: none;
}

.navShell {
  width: 100%;
  display: grid;
  grid-template-columns: repeat(4, minmax(0, 1fr));
  gap: 8px;
  padding: 10px;
  pointer-events: auto;
  background: var(--surface);
}

.navItem {
  min-width: 0;
  min-height: 62px;
  border: var(--pixel-border) solid transparent;
  border-radius: 8px;
  background: transparent;
  color: var(--muted);
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 4px;
  cursor: pointer;
}

.navItem.active {
  background: var(--accent);
  color: var(--accent-contrast);
  border-color: var(--border);
  box-shadow: 2px 2px 0 var(--shadow);
}

.navLabel {
  font-size: 0.72rem;
  font-weight: 700;
}

@media (max-width: 420px) {
  .pageContainer {
    padding-bottom: calc(var(--bottom-nav-height) + var(--footer-offset) + 16px) !important;
  }

  .navShell {
    gap: 6px;
    padding: 8px;
  }

  .navItem {
    min-height: 58px;
  }

  .floatingNavWrap {
    width: calc(100dvw - 16px);
  }

  .navLabel {
    font-size: 0.66rem;
  }
}
</style>
