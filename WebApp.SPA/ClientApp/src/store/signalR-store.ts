import { GroupDto } from "./../components/Chatius/services/group/group.dto";
import { IGroupDto } from "./../components/Chatius/services/interfaces/IGroup";
import * as signalR from "@microsoft/signalr";
import { defineStore } from "pinia";
import { api } from "../app/initializer/vue/vue-axios";
import { useEventStore } from "./event-store";
import { MessageDto } from "@/components/Chatius/services/message/message.dto";
import { IMessageDto } from "@/components/Chatius/services/interfaces/IMessage";

export const useSignalRStore = defineStore("signalR", {
	state: () => ({
		connection: null as unknown as signalR.HubConnection,
		isConnected: false,
		manuallyClosed: false,
	}),
	actions: {
		async startSignalR() {
			this.connection = new signalR.HubConnectionBuilder()
				.withUrl(`${api.defaults.baseURL}/hub`)
				.build();

			const eventStore = useEventStore();
			this.connection.on("userConnected", (userName: string) => {
				eventStore.clearState();
				eventStore.emitNewEvent("user-connected", userName);
			});

			this.connection.on("userDisconnected", (userName: string) => {
				eventStore.clearState();
				eventStore.emitNewEvent("user-disconnected", userName);
			});

			this.connection.on("userRegistered", (userName: string) => {
				eventStore.clearState();
				eventStore.emitNewEvent("userRegistered", userName);
			});

			this.connection.on("addedToGroup", (group: IGroupDto) => {
				eventStore.clearState();
				var groupDto = new GroupDto(group.id, group.name, group.members);
				eventStore.emitNewEvent("addedToGroup", groupDto);
			});

			this.connection.on("recievedMessage", (message: IMessageDto) => {
				eventStore.clearState();
				var messageDto = new MessageDto(
					message.id,
					message.author,
					message.groupId,
					message.text,
					message.creationDate,
				);
				eventStore.emitNewEvent("recievedMessage", messageDto);
			});

			this.connection.onclose(() => {
				if (!this.manuallyClosed) this.start();
			});
			this.manuallyClosed = false;
			await this.start();
		},
		async start() {
			try {
				await this.connection.start();
				this.isConnected = true;
			} catch (error) {
				console.error("Failed to connect with hub", error);
				this.isConnected = false;
				setTimeout(() => this.start(), 5000);
			}
		},
		async stopSignalR() {
			if (!this.isConnected) return;
			this.manuallyClosed = true;
			await this.connection.stop();
			this.isConnected = false;
		},
	},
});
