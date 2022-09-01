export interface ApplicationUser {
	id: string;
	userName: string;
	email: string;
	password: string;
}
export interface ChatGroup {
	id: string;
	memebers: Array<ApplicationUser>;
	creatorId: string;
	creator: ApplicationUser;
}

export interface ChatMessage {
	id: string;
	chatGroupId: string;
	chatGroup: ChatGroup;
	authorId: string;
	author: ApplicationUser;
	content: string;
}
