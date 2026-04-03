<template>
  <div class="authPage pixel-page">
    <section class="authShell pixel-card">
      <div class="tabRow">
        <button
          class="tabButton"
          :class="{ active: activeTab === 'login' }"
          type="button"
          @click="activeTab = 'login'"
        >
          {{ t("auth.login") }}
        </button>
        <button
          class="tabButton"
          :class="{ active: activeTab === 'register' }"
          type="button"
          @click="activeTab = 'register'"
        >
          {{ t("auth.register") }}
        </button>
      </div>

      <div class="formWrap">
        <LoginForm v-if="activeTab === 'login'" />
        <RegisterForm v-else />
      </div>
    </section>
  </div>
</template>

<script setup>
import { ref } from "vue";
import LoginForm from "../components/forms/LoginForm.vue";
import RegisterForm from "../components/forms/RegisterForm.vue";
import { useAppPreferences } from "../composables/useAppPreferences";

const activeTab = ref("login");
const { t } = useAppPreferences();
</script>

<style scoped>
.authPage {
  width: 100%;
  min-height: 100vh;
  min-height: 100dvh;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.authShell {
  padding: 18px;
  display: flex;
  flex-direction: column;
  gap: 18px;
}

.authKicker {
  color: var(--muted);
  font-size: 0.78rem;
  letter-spacing: 0.12em;
  text-transform: uppercase;
}

.authTitle {
  font-size: 1.45rem;
  font-weight: 700;
}

.authSubtitle {
  color: var(--muted);
  font-size: 0.84rem;
  line-height: 1.6;
}

.tabRow {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 10px;
}

.tabButton {
  border: var(--pixel-border) solid var(--border);
  border-radius: 8px;
  background: var(--surface-soft);
  color: var(--text);
  padding: 12px 14px;
  cursor: pointer;
  font-weight: 700;
  box-shadow: 2px 2px 0 var(--shadow);
}

.tabButton.active {
  background: var(--accent);
  color: var(--accent-contrast);
}

.formWrap {
  min-height: 320px;
}
</style>
