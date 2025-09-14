<template>
  <div
    class="q-pa-md row items-start q-gutter-md flex flex-center"
    style="height: 100vh; width: 100vw"
  >
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
      <q-card-section class="q-pt-none">
        <q-input v-model="name" label="Name" outlined />
      </q-card-section>
      <q-card-section class="flex flex-center">
        <q-btn label="Register" outlined @click="handleRegister" />
      </q-card-section>
    </q-card>
  </div>
</template>
<script setup>
import { ref } from "vue";

const email = ref("");
const password = ref("");
const name = ref("");

if (email.value != "" && password.value != "" && name.value != "") {
  alert("註冊成功");
} else {
  alert("請輸入完整資料");
}
</script>
