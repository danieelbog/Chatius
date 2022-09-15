import { IUserDto } from "./IUser";

export interface IMessageDto {
	id: string;
	author: IUserDto;
	groupId: string;
	text: string;
	creationDate: Date;
}

export interface IMessagesGroupedByDateDto {
	creationDate: Date;
	messages: Array<IMessageDto>;
}
