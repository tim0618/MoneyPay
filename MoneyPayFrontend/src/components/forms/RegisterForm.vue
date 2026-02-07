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
    if (true) {
      alert("demo，此功能暫不開放");
      router.push("/login");
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
