<template>
	<div
		id="chatius"
		class="toggable-container position-fixed bottom-0"
		:class="{
			'end-0 flex-row-reverse': directionRight,
			'start-0 flex-row': !directionRight,
		}"
	>
		<conversation-container
			:directionRight="directionRight"
			@setNewActiveChatBox="addSelected"
		></conversation-container>
		<chat-box-container
			:selectedChatGroup="selectedChatGroup"
			@setNewActiveChatBox="addSelected"
		></chat-box-container>
	</div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, watch } from "@vue/composition-api";
import { GroupDto } from "./chat.service.dto";

import ChatBoxContainer from "./chat-box-container.vue";
import ConversationContainer from "./conversation-container.vue";
import { useEventStore } from "../../store/event-store";
import "./chatius.scss";

export default defineComponent({
	components: { ConversationContainer, ChatBoxContainer },
	props: {
		directionRight: {
			type: Boolean,
			required: false,
			default: true,
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
		const selectedChatGroup = ref(null as unknown as GroupDto);
		function addSelected(chatGroup: GroupDto) {
			selectedChatGroup.value = chatGroup;
		}

		return {
			selectedChatGroup: selectedChatGroup,
			addSelected: addSelected,
		};
	},
});
</script>
