import { createRouter, createWebHistory } from "vue-router";
import Login from "../pages/Login.vue";
import MainLayout from "../layouts/MainLayout.vue";
import Dashboard from "../pages/Dashboard.vue";

const routes = [
  {
    path: "/",
    redirect: "/login", // 預設開啟 /login
  },
  { path: "/login", component: Login },
  {
    path: "/dashboard",
    component: MainLayout,
    children: [{ path: "", component: Dashboard }],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
