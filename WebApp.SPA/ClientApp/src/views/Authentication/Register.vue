<template>
	<div class="container p-3 pt-lg-5">
		<h2 class="text-center my-3">Register</h2>
		<div class="row">
			<div class="col-12 col-lg-4"></div>
			<div class="col-12 col-lg-4 text-center">
				<div class="mt-5 mt-lg-0">
					<div v-if="showError" class="alert alert-danger d-flex flex-row align-items-center error-messages">
						<i class="material-icons material-icon-24">report_problem</i>
						<span>{{ errorMessage }}</span>
					</div>
					<input
						type="email"
						v-model="email"
						class="form-control border-0 mt-3"
						id="register-email"
						name="register-email"
						aria-label="email"
						placeholder="Email"
						required
					/>
					<input
						type="text"
						v-model="username"
						class="form-control border-0 mt-3"
						id="register-username"
						aria-label="username"
						placeholder="Username"
						required
					/>
					<input
						type="password"
						v-model="password"
						class="form-control border-0 mt-3"
						id="register-password"
						aria-label="password"
						placeholder="password"
						autocomplete="current-password"
						required
					/>
					<div class="mt-3">
						<button @click="register" class="btn btn-primary w-100">Register</button>
						<router-link :to="loginPath">
							<template><p class="p-2">Or Login</p></template>
						</router-link>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent, ref } from "@vue/composition-api";
import { useAuthStore } from "../../store/auth-store";
import { router } from "../../router/index";
import { RegisterDto } from "./auth.service.dto";

export default defineComponent({
	setup() {
		const authStore = useAuthStore();
		const email = ref("");
		const password = ref("");
		const username = ref("");
		const errorMessage = ref("");
		const showError = ref(false);
		const loginPath = ref({
			path: "/login",
			query: {
				redirect: router?.currentRoute.query.redirect,
			},
		});

		async function register() {
			var registerDto = {
				email: email.value,
				username: username.value,
				password: password.value,
			} as RegisterDto;

			try {
				await authStore.registerUser(registerDto);
				if (authStore.isAuthenticated()) registerRedirect();
			} catch (error: any) {
				showError.value = true;
				errorMessage.value = error;
			}
		}

		function registerRedirect() {
			router?.push((router.currentRoute.query.redirect as string) || ("/" as string));
		}

		return {
			email,
			password,
			username,
			showError,
			errorMessage,
			loginPath,
			register,
		};
	},
});
</script>
