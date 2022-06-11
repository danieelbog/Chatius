<template>
  <main-layout></main-layout>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import MainLayout from "../components/Layouts/main-layout.vue"
export default defineComponent({
  components: {
    "main-layout": MainLayout
  }
});
</script>