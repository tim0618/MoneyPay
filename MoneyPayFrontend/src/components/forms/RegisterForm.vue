<template>
  <q-card flat bordered class="my-card" style="width: 90%; max-width: 400px">
    <q-card-section>
      <div class="text-h6">註冊</div>
    </q-card-section>

    <q-card-section class="q-pt-none">
      <q-input v-model="email" label="Email" outlined />
    </q-card-section>
    <q-card-section>
      <q-input v-model="password" type="password" label="Password" outlined />
    </q-card-section>
    <q-card-section>
      <q-input v-model="name" label="Name" outlined />
    </q-card-section>
    <q-card-section class="flex flex-center">
      <q-btn label="Register" outlined @click="handleRegister" />
    </q-card-section>
  </q-card>
</template>

<script setup>
import { ref } from "vue";
import { auth } from "../../apiComposables/userAuthApiComposables";
import { useRouter } from "vue-router";

const email = ref("");
const password = ref("");
const name = ref("");
const router = useRouter();

const { registerApi } = auth();

const handleRegister = async () => {
  try {
    console.log("註冊資料", email.value, password.value, name.value);
    if (email.value === "" || password.value === "" || name.value === "") {
      alert("請輸入完整資料");
      return;
    }
    const result = await registerApi({
      email: email.value,
      password: password.value,
      name: name.value,
    });
    if (result == "Registration Successful") {
      alert("註冊成功");
      router.push("/login");
    } else if (result == "Email Already Registered") {
      email.value = "";
      password.value = "";
      name.value = "";
      alert("Email 已被註冊");
    } else {
      email.value = "";
      password.value = "";
      name.value = "";
      alert("註冊失敗");
    }
  } catch (e) {
    console.error("Login error", e);
    email.value = "";
    password.value = "";
    name.value = "";
    alert("註冊失敗");
  }
};
</script>
