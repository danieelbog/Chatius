import Vue from "vue";
import { LoginDto, RegisterDto } from "./../views/Authentication/auth.service.dto";
import { ApplicationUser } from "./../components/Chat/chat.service.dto";
import { defineStore } from "pinia";
import { api } from "../app/initializer/vue/vue-axios";

export const useAuthStore = defineStore("auth", {
	state: () => ({
		applicationUser: null as unknown as ApplicationUser,
	}),
	actions: {
		setApplicationUser(applicationUser: ApplicationUser) {
			this.applicationUser = applicationUser;
		},
		isAuthenticated(): boolean {
			if (!this.applicationUser) return false;
			return true;
		},
		async getAuthUser() {
			// await api.get("/sanctum/csrf-cookie");
			const result = await api.get("/getAuthUser");
			if (result.data) this.setApplicationUser(result.data);
			if (this.isAuthenticated()) Vue.prototype.startSignalR();
		},
		async login(loginDto: LoginDto) {
			// await api.get("/sanctum/csrf-cookie");
			const result = await api.post("/login", loginDto);
			if (result.data) this.setApplicationUser(result.data);
			if (this.isAuthenticated()) Vue.prototype.startSignalR();
		},
		async registerUser(registerDto: RegisterDto) {
			// await api.get("/sanctum/csrf-cookie");
			const result = await api.post("/register", registerDto);
			if (result.data) this.setApplicationUser(result.data);
			if (this.isAuthenticated()) Vue.prototype.startSignalR();
		},
		async logout() {
			// await api.get("/sanctum/csrf-cookie");
			Vue.prototype.stopSignalR();
			await api.post("/logout");
			this.$reset();
		},

		//async resetPassword(payload: any) {
		//    console.log("resetPassword tirggered")
		//    // await api.get("/sanctum/csrf-cookie");
		//    // return api.post("/reset-password", payload);
		//},
		//updatePassword(payload: any) {
		//    console.log("updatePassword tirggered")
		//    // return api.put("/user/password", payload);
		//},
		//sendVerification(payload: any) {
		//    console.log("sendVerification tirggered")
		//    // return api.post("/email/verification-notification", payload);
		//},
		//updateUser(payload: any) {
		//    console.log("updateUser tirggered")
		//    // return api.put("/user/profile-information", payload);
		//},
		//async forgotPassword(payload: any) {
		//    console.log("forgotPassword tirggered")
		//    // await api.get("/sanctum/csrf-cookie");
		//    // return api.post("/forgot-password", payload);
		//},
	},
});