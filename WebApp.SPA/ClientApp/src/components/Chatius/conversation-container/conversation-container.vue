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
			<div class="bg-light container-fluid p-2">Search here</div>
		</template>
		<template v-slot:mainContent>
			<observable-infinite-scroll-wrapper @intersect="LoadChatGroups()">
				<template>
					<div
						v-for="group in groups"
						:key="group.id"
						@click="addSelected(group)"
						class="conversation-user p-2"
					>
						<div>
							{{ group.getPresentableChatGroupName(myUserName) }}
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
import { useEventStore } from "../../../store/event-store";
import { useAuthStore } from "../../../store/auth-store";
import { GroupDto } from "../services/group/group.dto";
import { getChatGroups } from "../services/group/group.service";
import ObservableInfiniteScrollWrapper from "../../Layouts/wrappers/observable-infinite-scroll-wrapper.vue";
import ToggableContainer from "../base-container/toggable-container.vue";
import "./conversation-container.scss";

export default defineComponent({
	components: { ObservableInfiniteScrollWrapper, ToggableContainer },
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
		const myUserName = ref(authStore.applicationUser.userName);

		return {
			groups,
			selectedChatGroup,
			myUserName,
			LoadChatGroups,
			addSelected,
		};
	},
});
</script>
