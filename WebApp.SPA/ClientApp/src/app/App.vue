<template>
	<main id="app">
		<router-view></router-view>
	</main>
</template>

<script lang="ts">
import { defineComponent, watch } from "@vue/composition-api";
import { useEventStore } from "../store/event-store";
import { useSignalRStore } from "../store/signalR-store";
export default defineComponent({
	setup() {
		const eventStore = useEventStore();
		watch(
			() => eventStore,
			(newEvent) => {
				if (newEvent.eventName != "user-connected") return;
				console.log(`${eventStore.args} just Connected`);
			},
			{ deep: true },
		);

		watch(
			() => eventStore,
			(newEvent) => {
				if (newEvent.eventName != "user-disconnected") return;
				console.log(`${eventStore.args} just disconnected`);
			},
			{ deep: true },
		);
	},
});
</script>
