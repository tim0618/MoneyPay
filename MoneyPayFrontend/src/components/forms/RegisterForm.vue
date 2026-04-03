<template>
  <form class="authForm" @submit.prevent="handleRegister">
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
        autocomplete="new-password"
        placeholder="******"
      />
    </label>

    <label class="field-stack">
      <span class="field-label">{{ t("auth.name") }}</span>
      <input
        v-model.trim="name"
        class="pixel-input"
        type="text"
        autocomplete="name"
        :placeholder="t('auth.name')"
      />
    </label>

    <button class="pixel-button submitButton" type="submit">
      {{ t("auth.registerButton") }}
    </button>
  </form>
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";
import { auth } from "../../apiComposables/userAuthApiComposables";
import { useAppPreferences } from "../../composables/useAppPreferences";

const email = ref("");
const password = ref("");
const name = ref("");
const router = useRouter();
const { registerApi } = auth();
const { t } = useAppPreferences();

const resetForm = () => {
  email.value = "";
  password.value = "";
  name.value = "";
};

const handleRegister = async () => {
  try {
    if (!email.value || !password.value || !name.value) {
      window.alert(t("auth.missingFields"));
      return;
    }

    const result = await registerApi({
      email: email.value,
      password: password.value,
      name: name.value,
    });

    if (result === "Registration Successful") {
      window.alert(t("auth.registerSuccess"));
      router.push("/login");
      return;
    }

    if (result === "Email Already Registered") {
      resetForm();
      window.alert(t("auth.emailRegistered"));
      return;
    }

    resetForm();
    window.alert(t("auth.registerError"));
  } catch (error) {
    console.error("Register error", error);
    resetForm();
    window.alert(t("auth.registerError"));
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
