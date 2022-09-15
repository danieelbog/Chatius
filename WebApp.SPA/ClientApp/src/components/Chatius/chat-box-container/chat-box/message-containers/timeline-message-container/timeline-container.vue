<template>
	<span v-if="showTimeLine && currentMessage" class="container-timeline m-1">
		<h2 class="container-timeline-header text-center border-bottom position-relative">
			<span
				class="p-1 text-secondary container-timeline-text bg-light position-relative"
			>
				{{ currentMessage.getMessageSentDate() }}
			</span>
		</h2>
	</span>
</template>

<script lang="ts">
import { defineComponent, onBeforeMount, onMounted, ref } from "@vue/composition-api";
import { MessageDto } from "../../../../services/message/message.dto";
import "./timeline-container.scss";
export default defineComponent({
	props: {
		previousMessage: {
			required: false,
			type: MessageDto,
			default: null,
		},
		currentMessage: {
			required: true,
			type: MessageDto,
		},
	},
	setup(props) {
		const showTimeLine = ref(false);
		onBeforeMount(() => {
			setShowTimeLine();
		});

		function setShowTimeLine() {
			if (!props.previousMessage) showTimeLine.value = true;
			var curStringDate = new Date(
				props.currentMessage?.creationDate,
			).toDateString();
			var prevStringDate = new Date(
				props.previousMessage?.creationDate,
			).toDateString();
			showTimeLine.value = curStringDate != prevStringDate;
		}
		return {
			showTimeLine,
		};
	},
});
</script>
