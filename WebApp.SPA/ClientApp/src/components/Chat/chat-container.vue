<template>
	<div id="chat-container" class="position-fixed bottom-0" :class="chatContainerDirection()">
		<div class="chat-container-overlay shadow rounded-top" ref="chatContainer">
			<header class="bg-light chat-box-container rounded-top border" @click="toggleChatBox()">
				<div class="d-flex justify-content-between p-2">
					<div class="d-flex align-items-center">
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
					</div>
					<div class="d-flex justify-content-between">
						<button type="button" class="navbar-toggler border-0 shadow-none not-collapsed">
							<span class="material-icons shadow-none">{{ expandedIcon }}</span>
						</button>
					</div>
				</div>
			</header>
			<div class="bg-light container-fluid border border-top-0 p-2">Search here</div>
			<section class="bg-light conversation-list border border-top-0">
				<div class="conversation-list-overlay">
					<observable-infinite-scroll-wrapper @pageAtBottom="loadChatUsers()">
						<template>
							<div class="conversation-user p-2" v-for="chatUser in chatUsers" :key="chatUser">{{ chatUser }}</div>
						</template>
					</observable-infinite-scroll-wrapper>
				</div>
			</section>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent, ref, watch, onMounted } from "@vue/composition-api";
import ObservableInfiniteScrollWrapper from "../Layouts/wrappers/observable-infinite-scroll-wrapper.vue";
import "./chat-container.scss";
// import * as signalR from "@microsoft/signalr";

export default defineComponent({
	components: { ObservableInfiniteScrollWrapper },
	props: {
		directionRight: {
			type: Boolean,
			required: false,
			default: true,
		},
	},
	setup(props, context) {
		function chatContainerDirection(): string {
			if (props.directionRight) return "end-0";
			return "start-0";
		}

		onMounted(() => {
			setExpandedIcon();
			loadChatUsers();
		});

		const expanded = ref(true);
		function toggleChatBox() {
			expanded.value = !expanded.value;
		}

		watch(
			() => expanded.value,
			(newVal: boolean, oldValue: boolean) => {
				expanded.value = newVal;
				setExpandedIcon();
				setExpandedClass();
			},
		);

		const expandedIcon = ref("keyboard_arrow_down");
		function setExpandedIcon() {
			if (expanded.value) expandedIcon.value = "keyboard_arrow_down";
			else expandedIcon.value = "keyboard_arrow_up";
		}

		const chatContainer = ref(null as unknown as HTMLElement);
		function setExpandedClass() {
			if (!chatContainer.value) return;

			if (expanded.value) {
				chatContainer.value.classList.remove("chat-container-is-minimized");
				chatContainer.value.classList.add("chat-container-is-expanded");
			} else {
				chatContainer.value.classList.remove("chat-container-is-expanded");
				chatContainer.value.classList.add("chat-container-is-minimized");
			}
		}

		const chatUsers = ref([] as Array<string>);
		const chatUserId = ref(0);
		function loadChatUsers() {
			for (var i = 0; i < 25; i++) {
				chatUserId.value++;
				chatUsers.value.push(`User` + chatUserId.value);
			}
		}

		return {
			chatContainer: chatContainer,
			chatContainerDirection: chatContainerDirection,
			toggleChatBox: toggleChatBox,
			expandedIcon: expandedIcon,
			loadChatUsers: loadChatUsers,
			chatUsers: chatUsers,
		};

		// const message = ref("");
		// const username = new Date().getTime()
		// const messages = ref([] as Array<{
		//     username: String,
		//     message: String
		// }>);

		// const connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7010/hub").build()
		// connection.start().catch(err => document.write(err));
		// connection.on("messageReceived", (username: string, message: string) => {
		//     messages.value.push({
		//         username: username,
		//         message: message
		//     })
		// });

		// function send() {
		//     connection.send("newMessage", username, message.value)
		//         .then(() => message.value = "");
		// }

		// return {
		//     send: send,
		//     message: message,
		//     messages: messages
		// }
	},
});
</script>
