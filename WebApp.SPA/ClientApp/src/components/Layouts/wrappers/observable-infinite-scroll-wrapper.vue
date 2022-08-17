<template>
	<div class="observable-infiinite-scroll-wrapper">
		<div v-if="scrollToBottom">
			<slot name="default"></slot>
			<observable-wrapper @intersect="emitAtPageBottom()"></observable-wrapper>
		</div>
		<div v-else>
			<observable-wrapper @intersect="emitAtPageTop()"></observable-wrapper>
			<slot name="default"></slot>
			<slot name="scrollToElement"></slot>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent, ref } from "@vue/composition-api";
import ObservableWrapper from "./observable-wrapper.vue";

export default defineComponent({
	components: { ObservableWrapper },
	props: {
		scrollingElement: {
			type: HTMLElement,
			required: false,
		},
		scrollToBottom: {
			type: Boolean,
			required: false,
			default: true,
		},
	},
	setup(props, context) {
		const loading = ref(false);
		function emitAtPageTop() {
			context.emit("pageAtTop");
		}

		function emitAtPageBottom() {
			context.emit("pageAtBottom");
		}

		return {
			loading: loading,
			emitAtPageTop: emitAtPageTop,
			emitAtPageBottom: emitAtPageBottom,
		};
	},
});
</script>
