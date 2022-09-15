<template>
	<observable-infinite-scroll-wrapper
		@intersect="loadMessages()"
		:isColumnReverse="true"
		:showObservable="false"
	>
		<template>
			<div class="pt-2 pb-2">
				<div
					v-for="message in messages"
					:key="message.id"
					:class="{
						'd-flex justify-content-end': message.isSentBy(myUserName),
						'd-flex justify-content-start': !message.isSentBy(myUserName),
					}"
				>
					<fancy-message :message="message"></fancy-message>
				</div>
			</div>
		</template>
	</observable-infinite-scroll-wrapper>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, watch } from "@vue/composition-api";
import { useAuthStore } from "../../../../../../store/auth-store";
import { useEventStore } from "../../../../../../store/event-store";
import { MessageDto } from "../../../../services/message/message.dto";
import { getMessages } from "../../../../services/message/message.service";
import ObservableInfiniteScrollWrapper from "../../../../../Layouts/wrappers/observable-infinite-scroll-wrapper.vue";
import FancyMessage from "./fancy-message.vue";

export default defineComponent({
	components: {
		ObservableInfiniteScrollWrapper,
		FancyMessage,
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

		const authStore = useAuthStore();
		const myUserName = ref(authStore.applicationUser.userName);

		return {
			messages,
			myUserName,
			loadMessages,
		};
	},
});
</script>
