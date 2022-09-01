<template>
	<b-navbar toggleable type="light" variant="light" class="p-2 shadow sticky-top">
		<b-navbar-brand href="#">
			<img src="https://placekitten.com/g/30/30" alt="Kitten" />
		</b-navbar-brand>

		<b-navbar-toggle target="navbar-toggle-collapse" class="border-0 shadow-none">
			<span class="material-icons md-36"> account_circle </span>
		</b-navbar-toggle>

		<b-collapse id="navbar-toggle-collapse" is-nav>
			<b-navbar-nav class="ml-auto align-items-end p-1">
				<b-nav-item href="#">Profile Settings</b-nav-item>
				<b-nav-item href="#">Account</b-nav-item>
				<b-nav-item href="#" @click="logout()">Logout</b-nav-item>
			</b-navbar-nav>
		</b-collapse>
	</b-navbar>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";
import { useAuthStore } from "../../../store/auth-store";
import { router } from "../../../router";

export default defineComponent({
	setup(props, context) {
		async function logout() {
			const authStore = useAuthStore();
			await authStore.logout();
			logoutRedirect();
		}

		function logoutRedirect() {
			router?.push("/login");
		}

		return {
			logout,
		};
	},
});
</script>
