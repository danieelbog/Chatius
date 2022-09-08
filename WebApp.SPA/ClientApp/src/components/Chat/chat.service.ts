import { api } from "@/app/initializer/vue/vue-axios";
import { MessageDto, UserDto, GroupDto } from "./chat.service.dto";
var uuid = require("uuid");

export async function getUsers(): Promise<Array<UserDto>> {
	return (await api.get("/getUsers")).data;
}

export async function getMessages(groupId: string): Promise<Array<MessageDto>> {
	return (await api.get("/getMessages?groupId=" + groupId)).data;
}

export async function getChatGroups(): Promise<Array<GroupDto>> {
	return (await api.get("/getGroups")).data;
}

export async function getChatGroup(groupId: string): Promise<GroupDto> {
	return (await api.get("/getGroup/?groupId=" + groupId)).data;
}
