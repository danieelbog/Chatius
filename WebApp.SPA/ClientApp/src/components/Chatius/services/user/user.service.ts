import { api } from "@/app/initializer/vue/vue-axios";
import { IUserDto } from "../interfaces/IUser";
import { UserDto } from "./user.dto";

export async function getUsers(): Promise<Array<UserDto>> {
	var users = Array<UserDto>();
	var result = (await api.get("/getUsers")).data;
	result.forEach((user: IUserDto) => {
		users.push(new UserDto(user.userName, user.email, user.connected));
	});
	return users;
}
