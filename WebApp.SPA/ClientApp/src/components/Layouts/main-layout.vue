<template>
	<div class="container-fluid">
		<div class="row flex-xl-nowrap2">
			<main-navbar></main-navbar>
			<div class="container-fluid mt-3 mb-3">
				<div class="row">
					<left-sidebar></left-sidebar>
					<main-content></main-content>
					<right-sidebar></right-sidebar>
				</div>
			</div>
			<main-footer></main-footer>
		</div>
	</div>
</template>

<script lang="ts">
import MainNavbar from "./navbars/main-navbar.vue";
import MainContent from "./content/main-content.vue";
import MainFooter from "./footers/main-footer.vue";
import LeftSidebar from "./sidebars/left-sidebar.vue";
import RightSidebar from "./sidebars/right-sidebar.vue";

import { defineComponent, ref, onMounted, onBeforeUnmount } from "@vue/composition-api";
import { useEventStore } from "../../store/event-store";

export default defineComponent({
	components: {
		"main-navbar": MainNavbar,
		"main-content": MainContent,
		"main-footer": MainFooter,
		"left-sidebar": LeftSidebar,
		"right-sidebar": RightSidebar,
	},
	setup(props, context) {
		const eventStore = useEventStore();
		onMounted(() => {
			eventStore.emitNewEvent("show-desktop-layout", ShowDesktopLayout());
			window.addEventListener("resize", onResize);
		});
		onBeforeUnmount(() => window.removeEventListener("resize", onResize));

		function onResize(event: Event) {
			eventStore.emitNewEvent("show-desktop-layout", ShowDesktopLayout());
		}

		function ShowDesktopLayout(): boolean {
			const desktopPageWidth = 768;
			return window.innerWidth >= desktopPageWidth;
		}
	},
});
</script>
