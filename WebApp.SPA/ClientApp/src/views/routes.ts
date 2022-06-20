import { RouteConfig } from "vue-router";

const routes: Array<RouteConfig> = [
    {
        path: "/",
        name: "Dashboard",
        meta: {
            pageTitle: "Chatius | Dashboard",
        },
        component: () => import(/* webpackChunkName: "dahsboard" */ "./Dashboard/Dashboard.vue"),
    },
];

export { routes };
