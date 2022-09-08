export interface UserDto {
	userName: string;
	email: string;
	connected: boolean;
}

export interface GroupDto {
	id: string;
	name: string;
	members: Array<UserDto>;
}

export interface MessageDto {
	id: string;
	author: UserDto;
	groupId: string;
	text: string
}
