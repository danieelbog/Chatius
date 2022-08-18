<template>
	<toggable-container
		:showHideButton="true"
		:height="height"
		:width="width"
		:toggableContainerLeftMaring="chatBoxLeftMaring"
		:toggableContainerRightMaring="chatBoxRightMaring"
		@hideContainer="hideChatBox()"
		ref="toggableContainer"
	>
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
			<div class="ms-2">User Name</div>
		</template>
		<template v-slot:mainContent>
			<div v-for="chatMessage in chatMessages" :key="chatMessage.id">
				<div v-if="chatMessage.authorId != 'other'" class="bg-warning">
					{{ chatMessage.content }}
				</div>
				<div v-else class="bg-danger">
					{{ chatMessage.content }}
				</div>
			</div>
			<div ref="scrollToElement"></div>
		</template>
	</toggable-container>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, nextTick } from "@vue/composition-api";
import { ChatGroup, ChatMessage } from "./chat.service.dto";
import { getMessages } from "./chat.service";
import toggableContainer from "./toggable-container.vue";
import ObservableInfiniteScrollWrapper from "../Layouts/wrappers/observable-infinite-scroll-wrapper.vue";

export default defineComponent({
	components: { toggableContainer, ObservableInfiniteScrollWrapper },
	props: {
		chatGroup: {
			type: Object as () => ChatGroup,
			required: true,
		},
		height: {
			type: String,
			required: false,
			default: "400px",
		},
		width: {
			type: String,
			required: false,
			default: "310px",
		},
		chatBoxLeftMaring: {
			type: String,
			required: false,
			default: "0px",
		},
		chatBoxRightMaring: {
			type: String,
			required: false,
			default: "0px",
		},
	},
	setup(props, context) {
		const chatMessages = ref([] as Array<ChatMessage>);
		const scrollToElement = ref(null as unknown as HTMLElement);
		async function loadChatMessages() {
			chatMessages.value = await getMessages(props.chatGroup.id);
			nextTick(() => {
				scrollToElement.value.scrollIntoView({ behavior: "smooth" });
			});
		}

		onMounted(() => {
			loadChatMessages();
		});

		function hideChatBox() {
			context.emit("hideChatBox", props.chatGroup);
		}

		const toggableContainer = ref(null as unknown as any);
		function expand() {
			toggableContainer.value.expand();
		}

		return {
			chatMessages: chatMessages,
			scrollToElement: scrollToElement,
			toggableContainer: toggableContainer,
			loadChatMessages: loadChatMessages,
			hideChatBox: hideChatBox,
			expand: expand,
		};
	},
});
</script>
