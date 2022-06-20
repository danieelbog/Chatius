import { RouteConfig } from "vue-router";

const routes: Array<RouteConfig> = [
    {
        path: "/",
        name: "Chatius",
        meta: {
            pageTitle: "Chatius",
        },
        component: () => import(/* webpackChunkName: "main" */ "../views/Main/Main.vue"),
    },
];

export { routes };
