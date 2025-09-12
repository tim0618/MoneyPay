<template>
  <div
    class="q-pa-md row items-start q-gutter-md flex flex-center"
    style="height: 100vh"
  >
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
  </div>
</template>
<script setup>
import { ref } from "vue";
import { auth } from "../apiComposables/userAuthApiComposables";
import { useRouter } from "vue-router";

const email = ref("");
const password = ref("");
const router = useRouter();
const { loginApi } = auth();

const handleLogin = async () => {
  try {
    console.log("登入資料", email.value, password.value);
    const result = await loginApi(email.value, password.value);
    if (result.token) {
      localStorage.setItem("token", result.token);
      router.push("/dashboard");
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
