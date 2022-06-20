<template>
    <div class="position-sticky row g-0">
        <div class="col-9">
            <router-view />
        </div>
        <div class="col-3">
            <main-sidebar></main-sidebar>
        </div>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from '@vue/composition-api';
    import MainSidebar from '../sidebars/main-sidebar.vue';
    export default defineComponent({
        components: {
            "main-sidebar": MainSidebar
        }
    });
</script>