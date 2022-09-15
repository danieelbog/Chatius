<template>
	<div
		class="d-flex align-items-center w-75 m-1"
		:class="{
			'justify-content-end': message.isSentBy(myUserName),
		}"
	>
		<img
			v-if="!message.isSentBy(myUserName)"
			src="https://placekitten.com/g/30/30"
			alt=""
			width="22"
			height="22"
			class="d-inline-block align-text-top rounded-circle"
		/>
		<div class="status rounded-circle bg-success"></div>
		<div
			class="badge text-wrap ms-1 rounded-pill"
			:class="{
				'bg-primary text-white': message.isSentBy(myUserName),
				'bg-secondary text-white': !message.isSentBy(myUserName),
			}"
		>
			<div class="p-2">
				{{ message.text }}
			</div>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent, ref } from "@vue/composition-api";
import { useAuthStore } from "../../../../../../store/auth-store";
import { MessageDto } from "../../../../services/message/message.dto";

export default defineComponent({
	props: {
		message: {
			type: Object as () => MessageDto,
		},
	},
	setup() {
		const authStore = useAuthStore();
		const myUserName = ref(authStore.applicationUser.userName);
		return {
			myUserName,
		};
	},
});
</script>
