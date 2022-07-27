<template>
	<div class="col ms-4 me-4">
		<div class="">
			<add-content></add-content>
		</div>
		<hr />
		<div class="p-4 mt-4">
			<div class="p-2 d-flex justify-content-center" v-for="gig in gigs" v-bind:key="gig.id">
				<gig :gig="gig"></gig>
			</div>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";
import Gig from "../../../components/Main/gig/gig.vue";
import AddContent from "../../../components/Main/add-content/add-content.vue";
import { GigDto } from "../../../interfaces/gig.dto";

export default defineComponent({
	components: {
		Gig,
		AddContent,
	},
	props: {
		gigs: {
			type: Array as () => Array<GigDto>,
			required: false,
		},
	},
	setup(props, context) {},
});
</script>
