<template>
	<div class="d-grid">
		<div class="d-flex p-1">
			<img
				src="https://placekitten.com/g/30/30"
				alt=""
				width="22"
				height="22"
				class="d-inline-block align-text-top rounded-circle"
			/>
			<div class="ms-2 text-wrap">
				{{ message.author.userName }}
				<div class="text-secondary">
					{{ message.getMessageSentTime() }}
				</div>
			</div>
			<div class="status rounded-circle bg-success"></div>
		</div>

		<div class="text-wrap ms-1">
			<div class="p-2">
				{{ message.text }}
			</div>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";
import { MessageDto } from "../../../../services/message/message.dto";
export default defineComponent({
	props: {
		message: {
			type: Object as () => MessageDto,
		},
	},
	setup() {},
});
</script>
