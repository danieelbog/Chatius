<template>
	<toggable-container :directionRight="directionRight">
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
			<div class="ms-2">Messaging</div>
		</template>
		<template v-slot:beforeMainContent>
			<div class="bg-light container-fluid border border-top-0 p-2">
				Search here
			</div>
		</template>
		<template v-slot:mainContent>
			<observable-infinite-scroll-wrapper @intersect="LoadChatGroups()">
				<template>
					<div
						class="conversation-user p-2"
						v-for="group in groups"
						:key="group.id"
						@click="addSelected(group)"
					>
						<div>
							{{ getChatGroupName(group) }}
						</div>
						<div>STATUS</div>
					</div>
				</template>
			</observable-infinite-scroll-wrapper>
		</template>
	</toggable-container>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted, watch } from "@vue/composition-api";
import { GroupDto, UserDto } from "./chat.service.dto";
import { getChatGroup, getChatGroups } from "./chat.service";
import ObservableInfiniteScrollWrapper from "../Layouts/wrappers/observable-infinite-scroll-wrapper.vue";
import ChatBoxContainer from "./chat-box-container.vue";
import ToggableContainer from "./toggable-container.vue";
import "./conversation-container.scss";
import { useEventStore } from "../../store/event-store";
import { useAuthStore } from "../../store/auth-store";
export default defineComponent({
	components: { ObservableInfiniteScrollWrapper, ChatBoxContainer, ToggableContainer },
	props: {
		directionRight: {
			type: Boolean,
			required: false,
			default: true,
		},
	},
	setup(props, context) {
		onMounted(() => {
			LoadChatGroups();
		});

		const groups = ref([] as Array<GroupDto>);
		async function LoadChatGroups() {
			groups.value = await getChatGroups();
		}

		const eventStore = useEventStore();
		watch(
			() => eventStore,
			(newEvent) => {
				if (newEvent.eventName != "addedToGroup") return;
				addSelected(eventStore.args);
			},
			{ deep: true },
		);

		const selectedChatGroup = ref(null as unknown as GroupDto);
		function addSelected(chatGroup: GroupDto) {
			context.emit("setNewActiveChatBox", chatGroup);
		}

		const authStore = useAuthStore();
		function getChatGroupName(group: GroupDto): string {
			var friendName = "";
			group.members.forEach((member: UserDto) => {
				if (member.userName != authStore.applicationUser.userName)
					friendName += `${member.userName}`;
			});
			return friendName;
		}

		return {
			groups: groups,
			selectedChatGroup: selectedChatGroup,
			LoadChatGroups: LoadChatGroups,
			addSelected: addSelected,
			getChatGroupName: getChatGroupName,
		};
	},
});
</script>
