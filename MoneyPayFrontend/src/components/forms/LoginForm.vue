<template>
  <q-card flat bordered class="my-card" style="width: 90%; max-width: 400px">
    <q-card-section>
      <div class="text-h6">登入</div>
    </q-card-section>

    <q-card-section class="q-pt-none">
      <q-input v-model="email" label="Email" outlined />
    </q-card-section>
    <q-card-section>
      <q-input v-model="password" type="password" label="Password" outlined />
    </q-card-section>
    <q-card-section class="flex flex-center">
      <q-btn label="Login" outlined @click="handleLogin" />
    </q-card-section>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
import { auth } from "../../apiComposables/userAuthApiComposables";
import { useRouter } from "vue-router";
import { jwtDecode } from "jwt-decode";

const email = ref("");
const password = ref("");
const router = useRouter();

const { loginApi } = auth();

const handleLogin = async () => {
  try {
    if (email.value === "" || password.value === "") {
      alert("請輸入完整資料");
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
    } else if (result.message == "Not Registered Yet") {
      email.value = "";
      password.value = "";
      alert("尚未註冊");
    } else if (result.message == "Wrong Password") {
      password.value = "";
      alert("密碼錯誤");
    }
  } catch (e) {
    console.error("Login error", e);
    email.value = "";
    password.value = "";
    alert("登入失敗");
  }
};
</script>
