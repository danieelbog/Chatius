import { api } from "@/app/initializer/vue/vue-axios";
import { IMessageDto, IMessagesGroupedByDateDto } from "../interfaces/IMessage";
import { MessageDto, MessagesGroupedByDateDto } from "./message.dto";

export async function getMessages(groupId: string): Promise<Array<MessageDto>> {
	var messages = Array<MessageDto>();
	var result = (await api.get("/getMessages?groupId=" + groupId)).data;
	result.forEach((message: IMessageDto) => {
		messages.push(
			new MessageDto(
				message.id,
				message.author,
				message.groupId,
				message.text,
				message.creationDate,
			),
		);
	});
	return messages;
}

export async function getMessagesGroupedByDate(
	groupId: string,
): Promise<Array<MessagesGroupedByDateDto>> {
	var messagesGroupedByDateDto = Array<MessagesGroupedByDateDto>();
	var result = (await api.get("/getMessagesByDate?groupId=" + groupId)).data;
	result.forEach((date: IMessagesGroupedByDateDto) => {
		var messageGroupedByDateDto = new MessagesGroupedByDateDto(date.creationDate, []);
		date.messages.forEach((message: IMessageDto) => {
			messageGroupedByDateDto.messages.push(
				new MessageDto(
					message.id,
					message.author,
					message.groupId,
					message.text,
					message.creationDate,
				),
			);
		});
		messagesGroupedByDateDto.push(messageGroupedByDateDto);
	});

	return messagesGroupedByDateDto;
}
