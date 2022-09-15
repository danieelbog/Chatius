import { IUserDto } from "./IUser";

export interface IGroupDto {
	id: string;
	name: string;
	members: Array<IUserDto>;
}
