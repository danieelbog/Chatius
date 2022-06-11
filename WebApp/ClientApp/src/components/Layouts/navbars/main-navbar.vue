<template>
    <nav class="navbar shadow justify-cotnent-between sticky-top bg-white p-2 mb-4">
        <router-link class="navbar-brand" to="/">
            <img src="https://placekitten.com/g/30/30" alt="Kitten">
        </router-link>
        <span class="material-icons md-36 text-muted">
            account_circle
        </span>
    </nav>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
});
</script>