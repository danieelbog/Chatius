<template>
	<div class="card" style="width: 18rem">
		<img class="card-img-top" src="" alt="Card image cap" />
		<div class="card-body">
			<h5 class="card-title">{{ gig.title }}</h5>
			<div>{{ gig.text }} Daniel Is here</div>
			<a href="#" class="btn btn-primary">Go somewhere</a>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";
import { GigDto } from "../../../interfaces/gig.dto";

export default defineComponent({
	props: {
		gig: {
			type: Object as () => GigDto,
			required: true,
		},
	},
	setup(props, context) {},
});
</script>
