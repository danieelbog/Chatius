<template>
	<div
		v-if="showObservable"
		class="observable-infiinite-scroll-wrapper d-flex overflow-auto"
		:class="{
			'flex-column-reverse': isColumnReverse,
			'flex-column': !isColumnReverse,
		}"
	>
		<slot name="default"></slot>
		<observable-wrapper @intersect="intersect()"></observable-wrapper>
	</div>
	<div v-else>
		<slot name="default"></slot>
	</div>
</template>

<script lang="ts">
import { defineComponent, ref } from "@vue/composition-api";
import ObservableWrapper from "./observable-wrapper.vue";

export default defineComponent({
	components: { ObservableWrapper },
	props: {
		isColumnReverse: {
			type: Boolean,
			required: false,
			default: false,
		},
		showObservable: {
			type: Boolean,
			required: false,
			default: true,
		},
	},
	setup(props, context) {
		const loading = ref(false);
		function intersect() {
			context.emit("intersect");
		}

		return {
			loading: loading,
			intersect: intersect,
		};
	},
});
</script>
