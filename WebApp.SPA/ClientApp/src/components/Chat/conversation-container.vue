<template>
	<toggable-container :directionRight="directionRight">
		<template v-slot:headerContent>
			<div class="position-relative">
				<img
					src="https://placekitten.com/g/30/30"
					alt=""
					width="32"
					height="32"
					class="d-inline-block align-text-top rounded-circle"
				/>
				<div class="status rounded-circle bg-success"></div>
			</div>
			<div class="ms-2">Messaging</div>
		</template>
		<template v-slot:beforeMainContent>
			<div class="bg-light container-fluid border border-top-0 p-2">Search here</div>
		</template>
		<template v-slot:mainContent>
			<observable-infinite-scroll-wrapper @pageAtBottom="LoadChatGroups()">
				<template>
					<div class="conversation-user p-2" @click="addSelected(chatGroup)" v-for="chatGroup in chatGroups" :key="chatGroup.id">
						{{ chatGroup.id }}
					</div>
				</template>
			</observable-infinite-scroll-wrapper>
		</template>
	</toggable-container>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "@vue/composition-api";
import { ChatGroup } from "./chat.service.dto";
import { getChatGroups } from "./chat.service";
import ObservableInfiniteScrollWrapper from "../Layouts/wrappers/observable-infinite-scroll-wrapper.vue";
import ChatBoxContainer from "./chat-box-container.vue";
import ToggableContainer from "./toggable-container.vue";
import "./conversation-container.scss";

var uuid = require("uuid");
const myId = uuid.v4();

export default defineComponent({
	components: { ObservableInfiniteScrollWrapper, ChatBoxContainer, ToggableContainer },
	props: {
		directionRight: {
			type: Boolean,
			required: false,
			default: true,
		},
	},
	setup(props, context) {
		onMounted(() => {
			LoadChatGroups();
		});

		const chatGroups = ref([] as Array<ChatGroup>);
		async function LoadChatGroups() {
			chatGroups.value = await getChatGroups(myId);
		}

		const selectedChatGroup = ref(null as unknown as ChatGroup);
		function addSelected(chatGroup: ChatGroup) {
			context.emit("setNewActiveChatBox", chatGroup);
		}

		return {
			chatGroups: chatGroups,
			selectedChatGroup: selectedChatGroup,
			LoadChatGroups: LoadChatGroups,
			addSelected: addSelected,
		};
	},
});
</script>
