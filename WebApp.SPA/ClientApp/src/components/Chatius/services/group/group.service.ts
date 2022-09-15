import { api } from "@/app/initializer/vue/vue-axios";
import { IGroupDto } from "../interfaces/IGroup";
import { GroupDto } from "./group.dto";

export async function getChatGroups(): Promise<Array<GroupDto>> {
	var groups = Array<GroupDto>();
	var result = (await api.get("/getGroups")).data;
	result.forEach((group: IGroupDto) => {
		groups.push(new GroupDto(group.id, group.name, group.members));
	});
	return groups;
}
