import _Vue from "vue";
import { BootstrapVue } from 'bootstrap-vue'

// Import Bootstrap and BootstrapVue CSS files (order is important)
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

export function initBootstrapVue(Vue: typeof _Vue) {
    Vue.use(BootstrapVue)
}