import { createRouter, createWebHistory } from "vue-router";
import Login from "../pages/Login.vue";
import MainLayout from "../layouts/MainLayout.vue";
import Home from "../pages/Home.vue";

const routes = [
  {
    path: "/",
    redirect: "/login", // 預設開啟 /login
  },
  { path: "/login", component: Login },
  {
    path: "/home",
    component: MainLayout,
    children: [{ path: "", component: Home }],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
