<template>
	<div v-if="showDesktopPage" class="shadow col-12 col-md-2 position-relative" ref="sidebar">
		<div v-if="showExpandButtons" class="mt-2 ms-0 mb-2 bottom-0 position-absolute text-left">
			<div v-if="sidebarIsExpanded">
				<b-button @click="minifySidebar" class="material-icons outline-none shadow-none" variant="outline-primary">
					keyboard_double_arrow_right</b-button
				>
			</div>
			<div v-else>
				<b-button @click="expandSidebar" class="material-icons outline-none shadow-none" variant="outline-primary">
					keyboard_double_arrow_left</b-button
				>
			</div>
		</div>
	</div>
	<b-navbar v-else toggleable type="light" variant="light" class="p-2 shadow">
		<b-navbar-brand href="#">
			<img src="https://placekitten.com/g/30/30" alt="Kitten" />
		</b-navbar-brand>

		<b-navbar-toggle target="navbar-toggle-collapse" class="border-0 shadow-none">
			<span class="material-icons md-36"> account_circle </span>
		</b-navbar-toggle>

		<b-collapse id="navbar-toggle-collapse" is-nav>
			<b-navbar-nav class="ml-auto align-items-end p-1">
				<b-nav-item href="#">Profile Settings</b-nav-item>
				<b-nav-item href="#">Account</b-nav-item>
			</b-navbar-nav>
		</b-collapse>
	</b-navbar>
</template>

<script lang="ts">
import { defineComponent, ref, watch } from "@vue/composition-api";
import { useEventStore } from "../../../store/event-store";

export default defineComponent({
	setup(props, context) {
		const eventStore = useEventStore();
		const showDesktopPage = ref(true);
		watch(
			() => eventStore,
			(newEvent) => {
				if (newEvent.eventName != "show-desktop-layout") return;
				showDesktopPage.value = newEvent.args;
			},
			{ deep: true },
		);

		const sidebar = ref(null as unknown as HTMLElement);
		var sidebarIsExpanded = ref(true);
		function minifySidebar() {
			if (!sidebar.value) return;
			sidebar.value.classList.remove("col-12", "col-md-2");
			sidebar.value.classList.add("col-1");
			sidebarIsExpanded.value = false;
		}

		function expandSidebar() {
			if (!sidebar.value) return;
			sidebar.value.classList.remove("col-1");
			sidebar.value.classList.add("col-12", "col-md-2");
			sidebarIsExpanded.value = true;
		}

		return {
			sidebarIsExpanded: sidebarIsExpanded,
			minifySidebar: minifySidebar,
			expandSidebar: expandSidebar,
			showDesktopPage: showDesktopPage,
			sidebar: sidebar,
		};
	},
});
</script>

<style></style>
