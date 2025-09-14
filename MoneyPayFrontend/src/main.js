import { createApp } from "vue";
import { Quasar } from "quasar";

// Quasar CSS
import "quasar/dist/quasar.css";
import "@quasar/extras/material-icons/material-icons.css";
import App from "./App.vue";
import "./style.css"

import router from "./router";

const myApp = createApp(App);

myApp
  .use(router)
  .use(Quasar, {
    plugins: {}, // 需要 Quasar plugin 再加
  })
  .mount("#app");
