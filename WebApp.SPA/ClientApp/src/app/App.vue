<template>
  <div id="app">
    <default-layout></default-layout>
  </div>
</template>


<script lang="ts">
    import { defineComponent } from '@vue/composition-api';
    import ChatBox from "../components/ChatBox/chat-box.vue";
    import DefaultLayout from "../components/Layouts/main-layout.vue";

    export default defineComponent({
        components: {
            "default-layout": DefaultLayout
        }
    });
</script>