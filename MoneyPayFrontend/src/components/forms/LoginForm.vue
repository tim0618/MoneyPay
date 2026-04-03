<template>
  <form class="authForm" @submit.prevent="handleLogin">
    <label class="field-stack">
      <span class="field-label">{{ t("auth.email") }}</span>
      <input
        v-model.trim="email"
        class="pixel-input"
        type="email"
        autocomplete="email"
        placeholder="name@example.com"
      />
    </label>

    <label class="field-stack">
      <span class="field-label">{{ t("auth.password") }}</span>
      <input
        v-model="password"
        class="pixel-input"
        type="password"
        autocomplete="current-password"
        placeholder="******"
      />
    </label>

    <button class="pixel-button submitButton" type="submit">
      {{ t("auth.loginButton") }}
    </button>
  </form>
</template>

<script setup>
import { ref } from "vue";
import { jwtDecode } from "jwt-decode";
import { useRouter } from "vue-router";
import { auth } from "../../apiComposables/userAuthApiComposables";
import { useAppPreferences } from "../../composables/useAppPreferences";

const email = ref("");
const password = ref("");
const router = useRouter();
const { loginApi } = auth();
const { t } = useAppPreferences();

const handleLogin = async () => {
  try {
    if (!email.value || !password.value) {
      window.alert(t("auth.missingFields"));
      return;
    }

    const result = await loginApi({
      email: email.value,
      password: password.value,
    });

    if (result.token) {
      localStorage.setItem("token", result.token);

      const decoded = jwtDecode(result.token);
      localStorage.setItem("name", decoded.name);
      localStorage.setItem("userEmail", decoded.userEmail);
      router.push("/home");
      return;
    }

    if (result.message === "Not Registered Yet") {
      email.value = "";
      password.value = "";
      window.alert(t("auth.notRegistered"));
      return;
    }

    if (result.message === "Wrong Password") {
      password.value = "";
      window.alert(t("auth.wrongPassword"));
      return;
    }
  } catch (error) {
    console.error("Login error", error);
    email.value = "";
    password.value = "";
    window.alert(t("auth.loginError"));
  }
};
</script>

<style scoped>
.authForm {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.formTitle {
  font-size: 1.12rem;
  font-weight: 700;
}

.formHint {
  color: var(--muted);
  font-size: 0.84rem;
  line-height: 1.6;
}

.submitButton {
  margin-top: 6px;
}
</style>
