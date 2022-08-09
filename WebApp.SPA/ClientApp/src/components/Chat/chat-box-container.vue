<template>
	<div class="bottom-0 position-sticky me-1 ms-1 d-flex justify-content-between">
		<chat-box :chatBox="chatBox" v-for="chatBox in activeChatboxes" :key="chatBox.id"></chat-box>
	</div>
</template>

<script lang="ts">
import { defineComponent, onMounted, watch, ref, onBeforeUnmount } from "@vue/composition-api";
import chatBox from "./chat-box.vue";

export default defineComponent({
	components: { chatBox },
	props: {
		selectedChatBox: {
			type: null as () => { id: string; text: string },
		},
	},
	setup(props, context) {
		const conversationContainerWidth = 288;
		const marginBetweenChatBoxesWidth = 4;
		const chatBoxWidth = 188;

		const floatToint = (value) => value | 0;
		const numberOfAllowedChatBoxes = () =>
			floatToint(window.innerWidth / (conversationContainerWidth + marginBetweenChatBoxesWidth + chatBoxWidth));
		const setAllowedChatBoxes = () => (allowedNumberOfChatBoxes.value = numberOfAllowedChatBoxes());
		const allowedNumberOfChatBoxes = ref(numberOfAllowedChatBoxes());

		onMounted(() => {
			window.addEventListener("resize", setAllowedChatBoxes);
		});
		onBeforeUnmount(() => {
			window.removeEventListener("resize", setAllowedChatBoxes);
		});

		const activeChatboxes = ref([] as Array<{ id: string; text: string }>);
		watch(
			() => props.selectedChatBox,
			(newChatBox: { id: string; text: string }) => {
				const chatBoxExist = activeChatboxes.value.find((c) => c.id == newChatBox.id);
				if (!chatBoxExist) {
					if (allowedNumberOfChatBoxes.value < activeChatboxes.value.length) removeChatBoxes();
					activeChatboxes.value.push(newChatBox);
				} else {
					//do something
				}
			},
		);

		function removeChatBoxes() {
			const numberOfChatBoxesToRemove = floatToint(activeChatboxes.value.length - allowedNumberOfChatBoxes.value);
			activeChatboxes.value.splice(0, numberOfChatBoxesToRemove);
		}

		function removeChatBox(chatBox: { id: string; text: string }) {
			var index = activeChatboxes.value.indexOf(chatBox);
			if (index != -1) {
				activeChatboxes.value.splice(index, 1);
				context.emit("setActiveChatBox", null);
			}
		}

		return {
			activeChatboxes: activeChatboxes,
		};
	},
});
</script>
