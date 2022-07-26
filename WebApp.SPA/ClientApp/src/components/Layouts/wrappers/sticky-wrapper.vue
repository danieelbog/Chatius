<template>
	<div class="sticky-wrapper sticky-top" ref="stickyWrapper">
		<slot name="content"></slot>
	</div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from "@vue/composition-api";

export default defineComponent({
	props: {
		distanceFromTop: {
			type: Number,
			required: false,
			default: 0,
		},
	},
	setup(props) {
		const stickyWrapper = ref(null as unknown as HTMLElement);
		onMounted(() => {
			setStickyWrapperStyle();
		});

		function setStickyWrapperStyle() {
			if (!stickyWrapper) return;
			stickyWrapper.value.style.top = `${props.distanceFromTop}px`;
			stickyWrapper.value.style.zIndex = "100";
		}
		return {
			stickyWrapper: stickyWrapper,
		};
	},
});
</script>
