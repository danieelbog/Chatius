<template>
	<div class="p-2 mt-auto border-top">
		<div class="d-flex justify-content-around">
			<input type="text" v-model="chatInput" />
			<button class="btn p-2" @click="sendMessage()">Send</button>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent, ref } from "@vue/composition-api";
import { useSignalRStore } from "../../../../../../store/signalR-store";
import { MessageDto } from "../../../../services/message/message.dto";

export default defineComponent({
	props: {
		chatGroupId: {
			type: String,
			required: true,
		},
	},
	setup(props, context) {
		const chatInput = ref(null as unknown as string);
		const signalRStore = useSignalRStore();
		function sendMessage() {
			if (!signalRStore.isConnected) return;
			var messageDto = {
				groupId: props.chatGroupId,
				text: chatInput.value,
			} as MessageDto;
			signalRStore.connection.invoke("SendMessageTo", messageDto);
			chatInput.value = "";
		}

		return {
			chatInput,
			sendMessage,
		};
	},
});
</script>
