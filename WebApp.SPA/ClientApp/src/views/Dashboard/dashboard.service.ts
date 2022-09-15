import { api } from "@/app/initializer/vue/vue-axios";

export async function getUsers() {
	return api.get("/getUsers");
}
