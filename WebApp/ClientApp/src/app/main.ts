import App from "./App.vue";
import router from "../router";
import VueAxios from "vue-axios";
import BootstrapVue3 from "bootstrap-vue-3";

import { createApp } from "vue";
import { api } from "../app/axios/index";
import { pinia } from "../app/pinia/index";
import { mockApiAdapter } from "./axios-adapter/index";

const app = createApp(App);

app.use(VueAxios, api);
app.use(mockApiAdapter, api);
app.use(pinia);
app.use(BootstrapVue3);
app.use(router);

app.mount("#app");
