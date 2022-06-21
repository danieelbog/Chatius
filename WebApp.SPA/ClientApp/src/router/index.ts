import Vue from "vue";
import VueRouter from "vue-router";
import { routes } from "../views/routes";
Vue.use(VueRouter);

const router = new VueRouter({
    mode: "history",
    routes: routes,
});

router.beforeEach(async (to, from, next) => {
    document.title = to.meta?.pageTitle;
    next();
});

export { router }