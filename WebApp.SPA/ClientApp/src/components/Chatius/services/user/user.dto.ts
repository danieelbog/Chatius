import { IUserDto } from "../interfaces/IUser";

export class UserDto implements IUserDto {
	userName: string;
	email: string;
	connected: boolean;

	constructor(userName: string, email: string, connected: boolean) {
		this.userName = userName;
		this.email = email;
		this.connected = connected;
	}
}
