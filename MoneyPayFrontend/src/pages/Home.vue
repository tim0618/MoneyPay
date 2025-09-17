<template>
  <div class="q-pa-md">
    <div v-if="loading">載入中...</div>
    <div v-else>
      <div v-if="types.length == 0">目前沒有付費種類</div>
      <div v-else>
        本月
        <ul>
          <li v-for="type in types">
            {{ type.type }} - {{ type.icon }} - {{ type.color }}
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>
<script setup>
import { onMounted, ref } from "vue";
import { typeApi } from "../apiComposables/typeApiComposables";
import { useRouter } from "vue-router";

const { getMoneyTypesByEmailApi } = typeApi();

const router = useRouter();
const types = ref([]);
const loading = ref(true);

onMounted(async () => {
  try {
    const token = localStorage.getItem("token");
    if (!token) {
      throw new Error("No token found");
    }
    const result = await getMoneyTypesByEmailApi(token);
    console.log(result);
    types.value = result;
  } catch (error) {
    console.error(error);
    alert("請先登入");
    router.push("/login");
  } finally {
    loading.value = false;
  }
});
</script>
<style scoped></style>
