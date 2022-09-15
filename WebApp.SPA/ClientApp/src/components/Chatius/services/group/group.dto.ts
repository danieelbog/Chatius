import { IGroupDto } from "../interfaces/IGroup";
import { IUserDto } from "../interfaces/IUser";

export class GroupDto implements IGroupDto {
	id: string;
	name: string;
	members: Array<IUserDto>;

	constructor(id: string, name: string, members: Array<IUserDto>) {
		this.id = id;
		this.name = name;
		this.members = members;
	}

	getPresentableChatGroupName(myUsername: string): string {
		var friendName = "";
		this.members.forEach((member: IUserDto) => {
			if (member.userName != myUsername) friendName += `${member.userName}`;
		});
		return friendName;
	}
}
