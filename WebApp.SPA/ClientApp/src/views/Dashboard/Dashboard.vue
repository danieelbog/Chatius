<template>
	<div>
		<main-navbar></main-navbar>
		<infinite-scroller @pageAtBottom="loadGigs()">
			<template v-slot:content>
				<div class="container mt-5">
					<div class="row dashboard-row">
						<dashboard-left-sidebar></dashboard-left-sidebar>
						<dashboard-main-conent :gigs="gigs"></dashboard-main-conent>
						<dashboard-right-sidebar></dashboard-right-sidebar>
					</div>
				</div>
			</template>
		</infinite-scroller>
	</div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref, watch } from "@vue/composition-api";
import InfiniteScroller from "../../components/Layouts/infinite-scroller/infinite-scroller.vue";
import DashboardLeftSidebar from "./dasboard-components/dashboard-left-sidebar.vue";
import DashboardMainConent from "./dasboard-components/dashboard-main-conent.vue";
import DashboardRightSidebar from "./dasboard-components/dashboard-right-sidebar.vue";
import ChatBox from "../../components/ChatBox/chat-box.vue";
import MainNavbar from "../../components/Layouts/navbars/main-navbar.vue";
import { GigDto } from "../../interfaces/gig.dto";
import "./dashboard.scss";

export default defineComponent({
	components: {
		DashboardLeftSidebar,
		DashboardMainConent,
		DashboardRightSidebar,
		InfiniteScroller,
		ChatBox,
		MainNavbar,
	},

	setup(props, context) {
		const gigs = ref([] as Array<GigDto>);
		const currentPage = ref(1);
		const nextItem = ref(1);
		onMounted(() => {
			loadGigs();
		});

		function loadGigs() {
			currentPage.value++;
			for (var i = 0; i < 15; i++) {
				var gig = new GigDto(JSON.stringify(nextItem.value), "Item " + nextItem.value, "Title" + nextItem.value);
				nextItem.value++;
				gigs.value.push(gig);
			}
		}

		return {
			gigs: gigs,
			loadGigs: loadGigs,
		};
	},
});
</script>
