import { createApp } from "vue";
import { Quasar } from "quasar";

// Quasar CSS
import "quasar/dist/quasar.css";
import "@quasar/extras/material-icons/material-icons.css";
import App from "./App.vue";
import "./style.css"

import router from "./router";

// 引入 Quasar 核心 CSS
import 'quasar/dist/quasar.css'
// 引入 MDI 圖示 CSS
import '@quasar/extras/mdi-v6/mdi-v6.css'

const myApp = createApp(App);

myApp
  .use(router)
  .use(Quasar, {
    plugins: {}, // 需要 Quasar plugin 再加
  })
  .mount("#app");
