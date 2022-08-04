<template>
	<div class="observable-infiinite-scroll-wrapper">
		<slot name="default"></slot>
		<observable-wrapper @intersect="emitAtPageBottom()"></observable-wrapper>
	</div>
</template>

<script lang="ts">
import { defineComponent, onBeforeUnmount, onMounted, onUnmounted, ref } from "@vue/composition-api";
import ObservableWrapper from "./observable-wrapper.vue";

export default defineComponent({
	components: { ObservableWrapper },
	props: {
		scrollingElement: {
			type: HTMLElement,
			required: false,
		},
	},
	setup(props, context) {
		const loading = ref(false);
		function emitAtPageBottom() {
			context.emit("pageAtBottom");
		}

		return {
			loading: loading,
			emitAtPageBottom: emitAtPageBottom,
		};
	},
});
</script>
