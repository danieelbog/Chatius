import { createPinia, Pinia } from 'pinia'

const pinia = installPinia();
export { pinia }

function installPinia(): Pinia {
    return createPinia();
}