<template>
	<toggable-container
		:showHideButton="true"
		:height="height"
		:width="width"
		:toggableContainerMargin="chatBoxMargin"
		@hideContainer="hideChatBox()"
		ref="toggableContainer"
	>
		<template v-slot:headerContent>
			<div class="position-relative">
				<img
					src="https://placekitten.com/g/30/30"
					alt=""
					width="32"
					height="32"
					class="d-inline-block align-text-top rounded-circle"
				/>
				<div class="status rounded-circle bg-success"></div>
			</div>
			<div class="ps-2">
				{{ chatGroup.getPresentableChatGroupName(myUserName) }}
			</div>
		</template>
		<template v-slot:mainContent>
			<component
				:is="messageContainerTemplate"
				:chatGroupId="chatGroup.id"
			></component>
		</template>
		<template v-slot:afterMainContent>
			<message-sender :chatGroupId="chatGroup.id"></message-sender>
		</template>
	</toggable-container>
</template>

<script lang="ts">
import { defineComponent, ref } from "@vue/composition-api";
import { useAuthStore } from "../../../../store/auth-store";
import { GroupDto } from "../../services/group/group.dto";
import ToggableContainer from "../../base-container/toggable-container.vue";
import MessageSender from "./message-containers/message-sender/message-sender.vue";
import FancyMessageContainer from "./message-containers/fancy-message-container/fancy-message-container.vue";
import TimelineMessageContainer from "./message-containers/timeline-message-container/timeline-message-container.vue";

export default defineComponent({
	components: {
		ToggableContainer,
		MessageSender,
	},
	props: {
		chatGroup: {
			type: Object as () => GroupDto,
			required: true,
		},

		height: {
			type: String,
			required: false,
			default: "400px",
		},
		width: {
			type: String,
			required: false,
			default: "310px",
		},
		chatBoxMargin: {
			type: String,
			required: false,
			default: "0px",
		},
		messageTemplate: {
			type: String,
			required: false,
			default: "timeline",
			validator: (value: string) => ["fancy", "timeline"].indexOf(value) != -1,
		},
	},
	setup(props, context) {
		function hideChatBox() {
			context.emit("hideChatBox", props.chatGroup);
		}

		const toggableContainer = ref(null as unknown as any);
		function expand() {
			toggableContainer.value.expand();
		}

		const messageContainerTemplate = ref(
			props.messageTemplate == "fancy"
				? FancyMessageContainer
				: TimelineMessageContainer,
		);

		const authStore = useAuthStore();
		const myUserName = ref(authStore.applicationUser.userName);

		return {
			myUserName,
			toggableContainer,
			hideChatBox,
			expand,
			messageContainerTemplate,
		};
	},
});
</script>
