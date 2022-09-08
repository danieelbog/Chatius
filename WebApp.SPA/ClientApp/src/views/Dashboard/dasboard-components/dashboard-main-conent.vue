<template>
	<div class="col ms-4 me-4">
		<div class="">
			<add-content></add-content>
		</div>
		<hr />
		<div class="p-4 mt-4"></div>
	</div>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";
import Gig from "../../../components/Main/gig/gig.vue";
import AddContent from "../../../components/Main/add-content/add-content.vue";

export default defineComponent({
	components: {
		AddContent,
	},
	props: {},
	setup(props, context) {},
});
</script>
