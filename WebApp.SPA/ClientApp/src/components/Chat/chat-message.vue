<template>
	<div
		class="d-flex align-items-center w-75 m-1"
		:class="{
			'justify-content-end': isSentByMe,
		}"
	>
		<img
			v-if="!isSentByMe"
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
				'bg-primary text-white': isSentByMe,
				'bg-secondary text-white': !isSentByMe,
			}"
		>
			<div class="p-2">
				{{ message.text }}
			</div>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";
import { MessageDto } from "./chat.service.dto";

export default defineComponent({
	props: {
		message: {
			type: Object as () => MessageDto,
		},
		isSentByMe: {
			type: Boolean,
			required: true,
		},
	},
	setup() {},
});
</script>
