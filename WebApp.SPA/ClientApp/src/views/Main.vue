<template>
    <!-- <chat-box></chat-box> -->
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import ChatBox from "../components/ChatBox/chat-box.vue";

export default defineComponent({
    components: {
        "chat-box": ChatBox
    }
});
</script>