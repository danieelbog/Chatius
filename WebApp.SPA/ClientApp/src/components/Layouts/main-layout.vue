<template>
    <section>
        <main-navbar></main-navbar>
        <main-content></main-content>
        <main-footer></main-footer>
    </section>
</template>

<script lang="ts">
    import { defineComponent } from '@vue/composition-api';
    import MainNavbar from "./navbars/main-navbar.vue";
    import MainContent from "./content/main-content.vue";
    import MainFooter from "./footers/main-footer.vue";

    export default defineComponent({
        components: {
            "main-navbar": MainNavbar,
            "main-content": MainContent,
            "main-footer": MainFooter,
        }
    });
</script>