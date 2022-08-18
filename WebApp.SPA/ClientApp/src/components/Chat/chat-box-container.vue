<template>
	<div class="chat-box-container">
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
import { defineComponent, onMounted, watch, ref, onBeforeUnmount, nextTick } from "@vue/composition-api";
import { ChatGroup } from "./chat.service.dto";
import chatBox from "./chat-box.vue";
import "./chat-box-container.scss";

export default defineComponent({
	components: { chatBox },
	props: {
		selectedChatGroup: {
			type: Object as () => ChatGroup,
			required: false,
			default: null,
		},
		conversationContainerWidth: {
			type: Number,
			required: false,
			default: 288,
		},
		chatBoxLeftMaring: {
			type: Number,
			required: false,
			default: 4,
		},
		chatBoxRightMaring: {
			type: Number,
			required: false,
			default: 4,
		},
		chatBoxWidth: {
			type: Number,
			required: false,
			default: 310,
		},
	},
	setup(props, context) {
		const floatToint = (value: number) => value | 0;
		const numberOfAllowedChatBoxes = () =>
			floatToint(
				window.innerWidth /
					(props.conversationContainerWidth + props.chatBoxLeftMaring + props.chatBoxRightMaring + props.chatBoxWidth),
			);
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
				showOrExpandIncomingChatGroup(newSelectedChatGroup);
			},
		);

		function showOrExpandIncomingChatGroup(newSelectedChatGroup: ChatGroup) {
			var incomingChatGroupIndex = getIndexOf(newSelectedChatGroup, activeChatGroups.value);
			if (incomingChatGroupIndex < 0) {
				if (allowedNumberOfChatGroups.value < activeChatGroups.value.length) removeChatBoxes();
				activeChatGroups.value.push(newSelectedChatGroup);
			} else {
				removeChatBox(activeChatGroups.value[incomingChatGroupIndex]);
				nextTick(() => {
					activeChatGroups.value.splice(incomingChatGroupIndex, 0, newSelectedChatGroup);
				});
			}
			resetActiveChatBox();
		}

		function getIndexOf(newSelectedChatGroup: ChatGroup, activeChatGroups: Array<ChatGroup>): number {
			for (let i = 0; i < activeChatGroups.length; i++) {
				const activeChatGroup = activeChatGroups[i];
				if (activeChatGroup.memebers.every((memberId) => newSelectedChatGroup.memebers.includes(memberId))) return i;
			}
			return -1;
		}

		function removeChatBoxes() {
			const numberOfChatBoxesToRemove = floatToint(activeChatGroups.value.length - allowedNumberOfChatGroups.value);
			activeChatGroups.value.splice(0, numberOfChatBoxesToRemove);
		}

		function removeChatBox(chatBox: ChatGroup) {
			var index = activeChatGroups.value.indexOf(chatBox);
			if (index != -1) {
				activeChatGroups.value.splice(index, 1);
				resetActiveChatBox();
			}
		}

		function resetActiveChatBox() {
			context.emit("setNewActiveChatBox", null);
		}

		return {
			activeChatGroups: activeChatGroups,
			removeChatBox: removeChatBox,
		};
	},
});
</script>
