<template>
  <div class="bg-warning">HELLO WORLD</div>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";
import ChatBox from "../../components/ChatBox/chat-box.vue";

export default defineComponent({
  components: {
    "chat-box": ChatBox,
  },
});
</script>