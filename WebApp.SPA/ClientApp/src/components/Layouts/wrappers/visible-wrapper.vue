<template>
	<div ref="visibleWrapper">
		<slot name="content"></slot>
	</div>
</template>

<script lang="ts">
import { defineComponent, onMounted, onBeforeUnmount, ref, watch } from "@vue/composition-api";

export default defineComponent({
	setup(props, context) {
		const visibleWrapper = ref(null as unknown as HTMLElement);
		const contnetIsPartiallyVisible = ref(null as unknown as boolean);
		const contnetIsFullyVisible = ref(null as unknown as boolean);

		onMounted(() => {
			setVisibility();
			window.addEventListener("scroll", setVisibility);
			window.addEventListener("resize", setVisibility);
		});
		onBeforeUnmount(() => {
			window.removeEventListener("scroll", setVisibility);
			window.addEventListener("resize", setVisibility);
		});

		watch(
			() => contnetIsPartiallyVisible.value,
			(newVal: boolean, oldValue: boolean) => {
				if (newVal != oldValue) context.emit("isPartiallyVisible", contnetIsPartiallyVisible.value);
			},
		);

		watch(
			() => contnetIsFullyVisible.value,
			(newVal: boolean, oldValue: boolean) => {
				if (newVal != oldValue) context.emit("isFullyVisible", contnetIsFullyVisible.value);
			},
		);

		function setVisibility() {
			let rect = visibleWrapper.value.getBoundingClientRect();
			contnetIsPartiallyVisible.value = elementIsPartialyVisible(rect);
			contnetIsFullyVisible.value = elementIsFullyVisible(rect) && contnetIsPartiallyVisible.value;
		}

		function elementIsPartialyVisible(rect: DOMRect): boolean {
			let isTopInViewPort = rect.top >= visibleWrapper.value.offsetTop * -1;
			let isLeftInViewPort = rect.left >= 0.1;

			return isTopInViewPort && isLeftInViewPort;
		}

		function elementIsFullyVisible(rect: DOMRect): boolean {
			let viewPortRight = window.innerWidth || document.documentElement.clientWidth;
			let viewPortBottom = window.innerHeight || document.documentElement.clientHeight;

			let isTopInViewPort = rect.top >= 0.1;
			let isLeftInViewPort = rect.left >= 0.1;
			let isBottomInViewPort = rect.bottom <= viewPortBottom;
			let letisRightInViewPort = rect.right <= viewPortRight;

			return isBottomInViewPort && letisRightInViewPort && isTopInViewPort && isLeftInViewPort;
		}

		return {
			visibleWrapper: visibleWrapper,
		};
	},
});
</script>
