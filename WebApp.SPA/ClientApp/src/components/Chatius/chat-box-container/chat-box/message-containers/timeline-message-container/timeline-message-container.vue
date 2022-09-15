<template>
	<observable-infinite-scroll-wrapper
		@intersect="loadMessages()"
		:isColumnReverse="true"
		:showObservable="false"
	>
		<template>
			<div v-if="messages" class="pt-2 pb-2">
				<div v-for="(message, index) in messages" :key="message.id">
					<timeline-container
						:previousMessage="messages[index - 1]"
						:currentMessage="message"
						:key="message.id"
					></timeline-container>
					<timeline-message :message="message"></timeline-message>
				</div>
			</div>
		</template>
	</observable-infinite-scroll-wrapper>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, watch } from "@vue/composition-api";
import { useEventStore } from "../../../../../../store/event-store";
import { MessageDto } from "../../../../services/message/message.dto";
import { getMessages } from "../../../../services/message/message.service";
import ObservableInfiniteScrollWrapper from "../../../../../Layouts/wrappers/observable-infinite-scroll-wrapper.vue";
import TimelineMessage from "./timeline-message.vue";
import TimelineContainer from "./timeline-container.vue";

export default defineComponent({
	components: {
		ObservableInfiniteScrollWrapper,
		TimelineContainer,
		TimelineMessage,
	},
	props: {
		chatGroupId: {
			type: String,
			required: true,
		},
	},
	setup(props, context) {
		const messages = ref([] as Array<MessageDto>);
		const eventStore = useEventStore();
		watch(
			() => eventStore,
			(newEvent) => {
				if (newEvent.eventName != "recievedMessage") return;
				if (eventStore.args.groupId != props.chatGroupId) return;
				messages.value.push(eventStore.args);
			},
			{ deep: true },
		);

		onMounted(() => {
			loadMessages();
		});

		async function loadMessages() {
			messages.value = await getMessages(props.chatGroupId);
		}

		return {
			messages,
			loadMessages,
		};
	},
});
</script>
