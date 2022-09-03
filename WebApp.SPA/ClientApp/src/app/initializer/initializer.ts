import Vue from "vue";
import App from "../App.vue";
import AppError from "../App-Error.vue";

import { pinia } from "./external/pinia";
import { router } from "../../router/index";
import { initBootstrapVue } from "./vue/vue-bootstrap";
import SignalRPlugin from "./external/signalR";

import "../../assets/styles/material-icons/material-icons.scss";

export async function initApplication() {
	try {
		initBootstrapVue(Vue);
		// Vue.use(SignalRPlugin);
		Vue.config.productionTip = false;
		new Vue({
			pinia,
			router,
			render: (h) => h(App),
		}).$mount("#app");
	} catch (error) {
		new Vue({
			render: (h) => h(AppError, { props: { error } }),
		}).$mount("#app");
	}
}
