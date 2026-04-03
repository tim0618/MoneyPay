import { createApp } from "vue";
import { Quasar } from "quasar";
import "quasar/dist/quasar.css";
import "@quasar/extras/material-icons/material-icons.css";
import "@quasar/extras/mdi-v6/mdi-v6.css";
import App from "./App.vue";
import "./style.css";
import router from "./router";

const myApp = createApp(App);

myApp
  .use(router)
  .use(Quasar, {
    plugins: {},
  })
  .mount("#app");
