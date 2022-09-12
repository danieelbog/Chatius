<template>
	<toggable-container
		:showHideButton="true"
		:height="height"
		:width="width"
		:toggableContainerMargin="chatBoxMargin"
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
			<div class="ps-2">{{ getChatGroupName() }}</div>
		</template>
		<template v-slot:mainContent>
			<observable-infinite-scroll-wrapper
				@intersect="loadChatMessages()"
				:isColumnReverse="true"
				:showObservable="false"
			>
				<template>
					<div class="pt-2 pb-2">
						<div
							class="d-flex"
							:class="{
								'justify-content-end': isSentByMe(chatMessage),
								'justify-content-start': !isSentByMe(chatMessage),
							}"
							v-for="chatMessage in chatMessages"
							:key="chatMessage.id"
						>
							<chat-message
								:message="chatMessage"
								:isSentByMe="isSentByMe(chatMessage)"
							></chat-message>
						</div>
					</div>
				</template>
			</observable-infinite-scroll-wrapper>
		</template>
		<template v-slot:afterMainContent>
			<div class="p-2 mt-auto border-top">
				<div class="d-flex justify-content-around">
					<input type="text" v-model="chatInput" />
					<button class="btn p-2" @click="sendMessage()">Send</button>
				</div>
			</div>
		</template>
	</toggable-container>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, nextTick, watch } from "@vue/composition-api";
import { GroupDto, MessageDto, UserDto } from "./chat.service.dto";
import { getMessages } from "./chat.service";
import toggableContainer from "./toggable-container.vue";
import ObservableInfiniteScrollWrapper from "../Layouts/wrappers/observable-infinite-scroll-wrapper.vue";
import { useAuthStore } from "../../store/auth-store";
import { useSignalRStore } from "../../store/signalR-store";
import { useEventStore } from "../../store/event-store";
import ChatMessage from "./chat-message.vue";

export default defineComponent({
	components: { toggableContainer, ObservableInfiniteScrollWrapper, ChatMessage },
	props: {
		chatGroup: {
			type: Object as () => GroupDto,
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
		chatBoxMargin: {
			type: String,
			required: false,
			default: "0px",
		},
	},
	setup(props, context) {
		const chatMessages = ref([] as Array<MessageDto>);
		const scrollToElement = ref(null as unknown as HTMLElement);
		async function loadChatMessages() {
			chatMessages.value = await getMessages(props.chatGroup.id);
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

		const authStore = useAuthStore();
		function getChatGroupName(): string {
			var groupName = "";
			props.chatGroup.members.forEach((member: UserDto) => {
				if (member.userName != authStore.applicationUser.userName)
					groupName += `${member.userName}`;
			});
			return groupName;
		}

		const chatInput = ref(null as unknown as string);
		const signalRStore = useSignalRStore();
		function sendMessage() {
			if (!signalRStore.isConnected) return;
			var messageDto = {
				groupId: props.chatGroup.id,
				text: chatInput.value,
			} as MessageDto;
			signalRStore.connection.invoke("SendMessageTo", messageDto);
			chatInput.value = "";
		}

		const eventStore = useEventStore();
		watch(
			() => eventStore,
			(newEvent) => {
				if (newEvent.eventName != "recievedMessage") return;
				if (eventStore.args.groupId != props.chatGroup.id) return;
				chatMessages.value.push(eventStore.args);
			},
			{ deep: true },
		);

		function isSentByMe(message: MessageDto): boolean {
			return message.author.userName == authStore.applicationUser.userName;
		}

		return {
			chatMessages,
			scrollToElement,
			toggableContainer,
			loadChatMessages,
			hideChatBox,
			expand,
			getChatGroupName,
			chatInput,
			sendMessage,
			isSentByMe,
		};
	},
});
</script>
