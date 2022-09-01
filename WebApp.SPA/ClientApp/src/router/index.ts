import Vue from "vue";
import VueRouter from "vue-router";
import { routes } from "../views/routes";
import { useAuthStore } from "../store/auth-store";

Vue.use(VueRouter);
const router = new VueRouter({
	mode: "history",
	routes: routes,
});

router.beforeEach(async (to, from, next) => {
	document.title = to.meta?.pageTitle;

	const authStore = useAuthStore();
	const allowAnonymous = to.matched.some((record) => record.meta.allowAnonymous);
	const loginQuery = { path: "/login", query: { redirect: to.fullPath } };

	if (!allowAnonymous && !authStore.isAuthenticated()) {
		await authStore.getAuthUser();
		if (!authStore.isAuthenticated()) next(loginQuery);
		else next();
	} else next();
});

export { router };
