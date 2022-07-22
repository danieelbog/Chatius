<template>
	<infinite-scroll @list-at-bottom="loadGigs()">
		<template v-slot:content>
			<div v-for="gig in gigs" v-bind:key="gig">
				<gig :text="gig"></gig>
			</div>
		</template>
	</infinite-scroll>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "@vue/composition-api";
import ChatBox from "../../components/ChatBox/chat-box.vue";
import InfiniteComponent from "../../components/Main/infinite-content/inifinite-content.vue";
import Gig from "../../components/Main/gig/gig.vue";

export default defineComponent({
	components: {
		"chat-box": ChatBox,
		"infinite-scroll": InfiniteComponent,
		gig: Gig,
	},

	setup(props, context) {
		const gigs = ref([] as Array<string>);
		const currentPage = ref(1);
		const nextItem = ref(1);
		onMounted(() => {
			loadGigs();
		});

		function loadGigs() {
			console.log("load gigs triggered");
			currentPage.value++;
			for (var i = 0; i < 210; i++) {
				gigs.value.push("Item " + nextItem.value++);
			}
		}

		return {
			gigs: gigs,
			loadGigs: loadGigs,
		};
	},
});
</script>
