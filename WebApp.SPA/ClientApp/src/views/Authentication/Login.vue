<template>
	<div class="container p-3 pt-lg-5">
		<h2 class="login-user-header text-center my-3">Login</h2>
		<div class="row">
			<div class="col-12 col-lg-4"></div>
			<div class="col-12 col-lg-4 text-center">
				<div class="mt-5 mt-lg-0 login-user">
					<div v-if="showError" class="alert alert-danger d-flex flex-row align-items-center error-messages">
						<i class="material-icons material-icon-24">report_problem</i>
						<span>{{ errorMessage }}</span>
					</div>
					<input
						type="text"
						v-model="username"
						class="form-control border-0 mt-3"
						id="fld-login-username"
						name="fld-login-username"
						aria-label="email"
						placeholder="email"
						value=""
						autocomplete="username"
						required
					/>
					<input
						type="password"
						v-model="password"
						class="form-control border-0 mt-3"
						id="fld-login-password"
						name="fld-login-password"
						aria-label="password"
						placeholder="password"
						autocomplete="current-password"
						required
					/>
					<div class="mt-1 mb-1">
						<b-form-checkbox
							id="login-remember"
							v-model="rememberMe"
							name="login-remember"
							value="accepted"
							unchecked-value="not_accepted"
						>
							<p class="p-2">Remember me</p>
						</b-form-checkbox>
					</div>

					<div class="mt-3">
						<button @click="login" class="btn btn-primary w-100">Login</button>
						<router-link :to="registerPath">
							<template><p class="p-2">Or Register</p></template>
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
import { LoginDto } from "./auth.service.dto";
import { router } from "../../router/index";

export default defineComponent({
	setup() {
		const authStore = useAuthStore();
		const password = ref("");
		const username = ref("");
		const rememberMe = ref("not_accepted");
		const errorMessage = ref("");
		const showError = ref(false);
		const registerPath = ref({
			path: "/register",
			query: {
				redirect: router?.currentRoute.query.redirect,
			},
		});

		async function login() {
			try {
				var loginDto = {
					username: username.value,
					password: password.value,
				} as LoginDto;
				await authStore.login(loginDto);
				if (authStore.isAuthenticated()) loginRedirect();
			} catch (error: any) {
				showError.value = true;
				errorMessage.value = error;
			}
		}

		function loginRedirect() {
			router?.push((router.currentRoute.query.redirect as string) || ("/" as string));
		}

		return {
			password,
			username,
			rememberMe,
			showError,
			errorMessage,
			registerPath,
			login,
		};
	},
});
</script>
