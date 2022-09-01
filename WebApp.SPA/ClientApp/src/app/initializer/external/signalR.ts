import _Vue from "vue";
import * as signalR from "@microsoft/signalr";
import { useEventStore } from "@/store/event-store";
import { useAuthStore } from "@/store/auth-store";
export default {
	install(Vue: typeof _Vue) {
		var connection = null as unknown as signalR.HubConnection;
		var startedPromise = null as any;
		var manuallyClosed = false;

		Vue.prototype.startSignalR = async (jwtToken: string) => {
			connection = new signalR.HubConnectionBuilder()
				.withUrl(`${Vue.prototype.$http.defaults.baseURL}/hub`)
				.build();

			const eventStore = useEventStore();
			connection.on("userConnected", (userName: string) => {
				eventStore.emitNewEvent("user-connected", userName);
			});

			connection.on("userDisconnected", (userName: string) => {
				eventStore.emitNewEvent("user-disconnected", userName);
			});

			connection.on("chatMessageRecieved", (userName, text) => {
				eventStore.emitNewEvent("chat-message-recieved", {
					userName,
					text,
				});
			});

			async function start() {
				startedPromise = connection.start().catch((err) => {
					console.error("Failed to connect with hub", err);
					return new Promise((resolve, reject) =>
						setTimeout(() => start().then(resolve).catch(reject), 5000),
					);
				});

				return startedPromise;
			}

			connection.onclose(() => {
				if (!manuallyClosed) start();
			});

			manuallyClosed = false;
			await start();
			await connectUser();
		};

		Vue.prototype.stopSignalR = async () => {
			if (!startedPromise) return;
			await disconnectUser();
			manuallyClosed = true;
			await connection.stop();
			startedPromise = null;
		};

		async function connectUser() {
			if (!startedPromise) return;
			const authStore = useAuthStore();
			try {
				await connection.invoke(
					"ConnectUser",
					authStore.applicationUser.userName,
				);
			} catch (error) {
				console.error(error);
			}
		}

		async function disconnectUser() {
			if (!startedPromise) return;
			const authStore = useAuthStore();
			try {
				await connection.invoke(
					"DisconnectUser",
					authStore.applicationUser.userName,
				);
			} catch (error) {
				console.error(error);
			}
		}

		Vue.prototype.$SendChatMessage = async (message: string) => {
			if (!startedPromise) return;
			try {
				await connection.invoke("SendChatMessage", message);
			} catch (error) {
				console.error(error);
			}
		};
	},
};
