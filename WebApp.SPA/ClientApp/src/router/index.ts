import Vue from "vue";
import VueRouter from "vue-router";
import { routes } from "../views/routes";
Vue.use(VueRouter);

var router = null as VueRouter | null;
export { router };

export function initRouter(baseUrl: string, pageTitle: string): VueRouter {
    if (router) return router;

    var instance = new VueRouter({
        mode: "history",
        base: baseUrl,
        routes: routes,        
    });

    instance.beforeEach(async (to, from, next) => {
        document.title = pageTitle;
    });

    router = instance;
    return instance;
}
