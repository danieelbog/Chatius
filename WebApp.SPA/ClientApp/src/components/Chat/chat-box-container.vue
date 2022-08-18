<template>
	<div id="chatbox-container" class="d-flex justify-content-between" v-if="activeChatGroups">
		<chat-box
			:chatGroup="chatGroup"
			v-for="chatGroup in activeChatGroups"
			:key="chatGroup.id"
			:width="chatBoxWidth + 'px'"
			:chatBoxLeftMaring="chatBoxLeftMaring + 'px'"
			:chatBoxRightMaring="chatBoxRightMaring + 'px'"
			@hideChatBox="removeChatBox"
		></chat-box>
	</div>
</template>

<script lang="ts">
import { defineComponent, onMounted, watch, ref, onBeforeUnmount } from "@vue/composition-api";
import chatBox from "./chat-box.vue";
import { ChatGroup } from "./chat.service.dto";

export default defineComponent({
	components: { chatBox },
	props: {
		selectedChatGroup: {
			type: Object as () => ChatGroup,
			required: false,
			default: null,
		},
	},
	setup(props, context) {
		const conversationContainerWidth = 288;
		const chatBoxLeftMaring = 4;
		const chatBoxRightMaring = 4;
		const chatBoxWidth = 310;

		const floatToint = (value: number) => value | 0;
		const numberOfAllowedChatBoxes = () =>
			floatToint(window.innerWidth / (conversationContainerWidth + chatBoxLeftMaring + chatBoxRightMaring + chatBoxWidth));
		const setAllowedChatBoxes = () => (allowedNumberOfChatGroups.value = numberOfAllowedChatBoxes());
		const allowedNumberOfChatGroups = ref(numberOfAllowedChatBoxes());

		onMounted(() => {
			window.addEventListener("resize", setAllowedChatBoxes);
		});
		onBeforeUnmount(() => {
			window.removeEventListener("resize", setAllowedChatBoxes);
		});

		const activeChatGroups = ref([] as Array<ChatGroup>);
		watch(
			() => props.selectedChatGroup,
			(newSelectedChatGroup: ChatGroup) => {
				if (!newSelectedChatGroup) return;
				var incomingChatGroup = null as unknown as ChatGroup;
				activeChatGroups.value.forEach((activeChatGroup) => {
					if (activeChatGroup.memebers.every((memberId) => newSelectedChatGroup.memebers.includes(memberId)))
						incomingChatGroup = activeChatGroup;
				});

				if (!incomingChatGroup) {
					if (allowedNumberOfChatGroups.value < activeChatGroups.value.length) removeChatBoxes();
					activeChatGroups.value.push(newSelectedChatGroup);
				} else {
					//do something
				}
			},
		);

		function removeChatBoxes() {
			const numberOfChatBoxesToRemove = floatToint(activeChatGroups.value.length - allowedNumberOfChatGroups.value);
			activeChatGroups.value.splice(0, numberOfChatBoxesToRemove);
		}

		function removeChatBox(chatBox: ChatGroup) {
			var index = activeChatGroups.value.indexOf(chatBox);
			if (index != -1) {
				activeChatGroups.value.splice(index, 1);
				context.emit("setNewActiveChatBox", null);
			}
		}

		return {
			activeChatGroups: activeChatGroups,
			chatBoxWidth: chatBoxWidth,
			chatBoxLeftMaring: chatBoxLeftMaring,
			chatBoxRightMaring: chatBoxRightMaring,
			removeChatBox: removeChatBox,
		};
	},
});
</script>
