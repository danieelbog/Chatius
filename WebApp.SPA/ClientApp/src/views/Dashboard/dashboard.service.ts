import { api } from "@/app/initializer/vue/vue-axios";
import { UserDto } from "@/components/Chat/chat.service.dto";

export async function getUsers() {
	return api.get("/getUsers");
}
