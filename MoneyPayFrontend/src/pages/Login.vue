<template>
  <div class="q-pa-md row items-start q-gutter-md">
     
    <q-card flat bordered class="my-card">
      <q-card-section>
        <div class="text-h6">登入</div>
      </q-card-section>

      <q-card-section class="q-pt-none">
        帳號：
      </q-card-section>
      <q-card-section>
        密碼:
      </q-card-section>
    </q-card>
  </div>
</template>

 
<!-- <template>
  <div class="q-pa-md flex flex-center" style="height:100vh">
    <q-card class="q-pa-lg" style="width: 400px; max-width: 90%">
      <q-card-section>
        <div class="text-h6">登入</div>
      </q-card-section>

      <q-card-section>
        <q-input v-model="email" label="Email" outlined />
        <q-input v-model="password" label="密碼" type="password" outlined class="q-mt-md" />
      </q-card-section>

      <q-card-actions align="right">
        <q-btn label="登入" color="primary" @click="login" />
      </q-card-actions>
    </q-card>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const email = ref('')
const password = ref('')

const login = () => {
  console.log('登入資料', email.value, password.value)
  // 呼叫後端 API 驗證
  // 登入成功後 router.push('/') 到 MainLayout
}
</script> -->
