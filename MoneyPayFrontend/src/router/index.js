import { createRouter, createWebHistory } from "vue-router";
import Login from "../pages/Login.vue";
import MainLayout from "../layouts/MainLayout.vue";
import Home from "../pages/Home.vue";
import UserSetting from "../pages/UserSetting.vue";
import SettingType from "../pages/SettingType.vue";
import Graphics from "../pages/Graphics.vue";

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
  {
    path: "/settingType",
    component: MainLayout,
    children: [{ path: "", component: SettingType }],
  },
  {
    path: "/graphics",
    component: MainLayout,
    children: [{ path: "", component: Graphics }],
  },
  {
    path: "/userSetting",
    component: MainLayout,
    children: [{ path: "", component: UserSetting }],
  },
];
const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
