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
    {
        path: "/login",
        name: "Login",
        meta: {
            allowAnonymous: true,
            noLayout: true,
            pageTitle: "Chatius | Login",
        },
        component: () => import(/* webpackChunkName: "dahsboard" */ "./Authentication/Login.vue"),
    },
    {
        path: "/register",
        name: "Register",
        meta: {
            allowAnonymous: true,
            noLayout: true,
            pageTitle: "Chatius | Register",
        },
        component: () => import(/* webpackChunkName: "dahsboard" */ "./Authentication/Register.vue"),
    },
];

export { routes };
