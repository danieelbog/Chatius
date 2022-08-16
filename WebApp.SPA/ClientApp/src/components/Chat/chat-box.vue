<template>
	<div class="p-2 m-2 bg-danger chatbox">
		<div v-for="chatMessage in chatMessages" :key="chatMessage.id">
			<div v-if="chatMessage.authorId != 'other'" class="bg-warning">
				{{ chatMessage.content }}
			</div>
			<div v-else class="bg-danger">
				{{ chatMessage.content }}
			</div>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "@vue/composition-api";
import { ChatGroup, ChatMessage } from "./chat.service.dto";
import { getMessages } from "./chat.service";

export default defineComponent({
	props: {
		chatGroup: {
			type: Object as () => ChatGroup,
			required: true,
		},
	},
	setup(props, context) {
		const chatMessages = ref([] as Array<ChatMessage>);
		async function loadChatMessages() {
			chatMessages.value = await getMessages(props.chatGroup.id);
		}

		onMounted(() => {
			loadChatMessages();
		});

		return {
			chatMessages: chatMessages,
		};
	},
});
</script>
