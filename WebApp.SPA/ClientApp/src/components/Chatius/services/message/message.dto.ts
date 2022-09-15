import { IMessageDto, IMessagesGroupedByDateDto } from "../interfaces/IMessage";
import { IUserDto } from "../interfaces/IUser";

export class MessageDto implements IMessageDto {
	id: string;
	author: IUserDto;
	groupId: string;
	text: string;
	creationDate: Date;

	constructor(
		id: string,
		author: IUserDto,
		groupId: string,
		text: string,
		creationDate: Date,
	) {
		this.id = id;
		this.author = author;
		this.groupId = groupId;
		this.text = text;
		this.creationDate = creationDate;
	}

	isSentBy(authorUsername: string): boolean {
		return this.author.userName == authorUsername;
	}

	getMessageSentDay(): string {
		const weekday = [
			"Sunday",
			"Monday",
			"Tuesday",
			"Wednesday",
			"Thursday",
			"Friday",
			"Saturday",
		];
		return weekday[new Date(this.creationDate).getDay()];
	}

	getMessageSentDate(): string {
		let date = new Date(this.creationDate);
		return date.toDateString();
	}

	getMessageSentTime(): string {
		let date = new Date(this.creationDate);
		return date.toLocaleTimeString("el-GR");
	}
}

export class MessagesGroupedByDateDto implements IMessagesGroupedByDateDto {
	creationDate: Date;
	messages: Array<IMessageDto>;

	constructor(creationDate: Date, messages: Array<IMessageDto>) {
		this.creationDate = creationDate;
		this.messages = messages;
	}
}
