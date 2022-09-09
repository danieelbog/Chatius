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
			<div class="ms-2">{{ getChatGroupName(chatGroup) }}</div>
		</template>
		<template v-slot:mainContent>
			<observable-infinite-scroll-wrapper
				@intersect="loadChatMessages()"
				:isColumnReverse="true"
				:showObservable="false"
			>
				<template>
					<div class="">
						<div v-for="chatMessage in chatMessages" :key="chatMessage.id">
							<div
								v-if="chatMessage.author.userName == currentUserUsername"
								class="bg-warning"
							>
								{{ chatMessage.text }}
							</div>
							<div v-else class="bg-danger">
								{{ chatMessage.text }}
							</div>
						</div>
					</div>
				</template>
			</observable-infinite-scroll-wrapper>
			<div class="p-2 fixed-bottom">
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

export default defineComponent({
	components: { toggableContainer, ObservableInfiniteScrollWrapper },
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
		function getChatGroupName(group: GroupDto): string {
			var friendName = "";
			group.members.forEach((member: UserDto) => {
				if (member.userName != authStore.applicationUser.userName)
					friendName += `${member.userName}`;
			});
			return friendName;
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
		}

		const eventStore = useEventStore();
		watch(
			() => eventStore,
			(newEvent) => {
				if (newEvent.eventName != "recievedMessage") return;
				chatMessages.value.push(eventStore.args);
			},
			{ deep: true },
		);

		const currentUserUsername = ref(authStore.applicationUser.userName);
		return {
			chatMessages: chatMessages,
			scrollToElement: scrollToElement,
			toggableContainer: toggableContainer,
			loadChatMessages: loadChatMessages,
			hideChatBox: hideChatBox,
			expand: expand,
			getChatGroupName: getChatGroupName,
			chatInput,
			sendMessage,
			currentUserUsername: currentUserUsername,
		};
	},
});
</script>
