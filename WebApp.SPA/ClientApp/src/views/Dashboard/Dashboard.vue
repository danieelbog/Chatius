<template>
	<div>
		<main-navbar></main-navbar>
		<observable-infinite-scroll-wrapper @intersect="loadUsers()">
			<template>
				<div class="container mt-5">
					<div class="row dashboard-row">
						<div v-for="user in users" :key="user.userName">
							<div
								class="p-2 bg-danger"
								@click="startChatWith(user.userName)"
							>
								{{ user.userName }}
							</div>
						</div>
					</div>
				</div>
			</template>
		</observable-infinite-scroll-wrapper>
		<chatius></chatius>
	</div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, watch } from "@vue/composition-api";
import { getUsers } from "./dashboard.service";
import { UserDto } from "../../components/Chatius/services/user/user.dto";
import { useSignalRStore } from "../../store/signalR-store";
import DashboardLeftSidebar from "./dasboard-components/dashboard-left-sidebar.vue";
import DashboardMainConent from "./dasboard-components/dashboard-main-conent.vue";
import DashboardRightSidebar from "./dasboard-components/dashboard-right-sidebar.vue";
import MainNavbar from "../../components/Layouts/navbars/main-navbar.vue";
import ObservableInfiniteScrollWrapper from "../../components/Layouts/wrappers/observable-infinite-scroll-wrapper.vue";
import Chatius from "../../components/Chatius/chatius.vue";
import "./dashboard.scss";

export default defineComponent({
	components: {
		DashboardLeftSidebar,
		DashboardMainConent,
		DashboardRightSidebar,
		MainNavbar,
		ObservableInfiniteScrollWrapper,
		Chatius,
	},

	setup(props, context) {
		onMounted(() => {
			loadUsers();
		});

		const users = ref([] as Array<UserDto>);
		async function loadUsers() {
			const result = await getUsers();
			if (result) users.value = result.data;
		}

		const signalRStore = useSignalRStore();
		async function startChatWith(userName: string) {
			if (!signalRStore.isConnected) return;
			await signalRStore.connection.invoke("AddToGroup", userName);
		}

		return {
			users: users,
			loadUsers: loadUsers,
			startChatWith: startChatWith,
		};
	},
});
</script>
