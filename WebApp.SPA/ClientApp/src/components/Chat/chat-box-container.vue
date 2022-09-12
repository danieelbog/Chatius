<template>
	<div class="chat-box-container" ref="chatBoxContainer">
		<chat-box
			:chatGroup="chatGroup"
			v-for="chatGroup in activeChatGroups"
			:key="chatGroup.id"
			:width="chatBoxWidth + 'px'"
			:chatBoxMargin="chatBoxMargin + 'px'"
			@hideChatBox="removeChatBox"
			ref="chatBoxes"
		></chat-box>
	</div>
</template>

<script lang="ts">
import {
	defineComponent,
	onMounted,
	watch,
	ref,
	onBeforeUnmount,
	nextTick,
} from "@vue/composition-api";
import { GroupDto } from "./chat.service.dto";
import chatBox from "./chat-box.vue";
import "./chat-box-container.scss";

export default defineComponent({
	components: { chatBox },
	props: {
		selectedChatGroup: {
			type: Object as () => GroupDto,
			required: false,
			default: null,
		},
		conversationContainerWidth: {
			type: Number,
			required: false,
			default: 288,
		},
		conversationMargin: {
			type: Number,
			required: false,
			default: 8,
		},
		chatBoxMargin: {
			type: Number,
			required: false,
			default: 4,
		},
		chatBoxWidth: {
			type: Number,
			required: false,
			default: 310,
		},
	},
	setup(props, context) {
		const floatToint = (value: number) => value | 0;
		const numberOfAllowedChatBoxes = () =>
			floatToint(
				window.innerWidth /
					(props.conversationContainerWidth +
						props.conversationMargin +
						props.chatBoxMargin +
						props.chatBoxWidth),
			);
		const setAllowedChatBoxes = () =>
			(allowedNumberOfChatGroups.value = numberOfAllowedChatBoxes());
		const allowedNumberOfChatGroups = ref(numberOfAllowedChatBoxes());

		onMounted(() => {
			setChatBoxStyle();
			window.addEventListener("resize", setAllowedChatBoxes);
		});
		onBeforeUnmount(() => {
			window.removeEventListener("resize", setAllowedChatBoxes);
		});

		const chatBoxContainer = ref(null as unknown as HTMLElement);
		function setChatBoxStyle() {
			if (!chatBoxContainer) return;
			chatBoxContainer.value.style.marginRight = props.conversationMargin + "px";
		}

		const activeChatGroups = ref([] as Array<GroupDto>);
		watch(
			() => props.selectedChatGroup,
			(newSelectedChatGroup: GroupDto) => {
				if (!newSelectedChatGroup) return;
				showOrExpandIncomingChatGroup(newSelectedChatGroup);
			},
		);

		const chatBoxes = ref([] as Array<any>);
		function showOrExpandIncomingChatGroup(newSelectedChatGroup: GroupDto) {
			var incomingChatGroupIndex = -1;
			for (let index = 0; index < activeChatGroups.value.length; index++) {
				if (activeChatGroups.value[index].id == newSelectedChatGroup.id) {
					incomingChatGroupIndex = index;
					break;
				}
			}

			if (incomingChatGroupIndex < 0) {
				if (allowedNumberOfChatGroups.value < activeChatGroups.value.length)
					removeChatBoxes();
				activeChatGroups.value.push(newSelectedChatGroup);
			} else {
				chatBoxes.value
					.find((chatBox) => chatBox.chatGroup.id == newSelectedChatGroup.id)
					.expand();
			}
			resetActiveChatBox();
		}

		function removeChatBoxes() {
			const numberOfChatBoxesToRemove = floatToint(
				activeChatGroups.value.length - allowedNumberOfChatGroups.value,
			);
			activeChatGroups.value.splice(0, numberOfChatBoxesToRemove);
		}

		function removeChatBox(chatBox: GroupDto) {
			var index = activeChatGroups.value.indexOf(chatBox);
			if (index != -1) {
				activeChatGroups.value.splice(index, 1);
				resetActiveChatBox();
			}
		}

		function resetActiveChatBox() {
			context.emit("setNewActiveChatBox", null);
		}

		return {
			chatBoxContainer,
			removeChatBox: removeChatBox,
			activeChatGroups: activeChatGroups,
			chatBoxes: chatBoxes,
		};
	},
});
</script>
