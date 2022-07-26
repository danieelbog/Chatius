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
		const contnetIsVisible = ref(null as boolean);

		onMounted(() => {
			isVisible();
			window.addEventListener("scroll", isVisible);
		});
		onBeforeUnmount(() => {
			window.removeEventListener("scroll", isVisible);
		});

		watch(
			() => contnetIsVisible.value,
			(newVal: boolean, oldValue: boolean) => {
				console.log(newVal);
				if (newVal != oldValue) context.emit("isVisible", contnetIsVisible.value);
			},
		);

		function isVisible() {
			contnetIsVisible.value = visibleWrapper.value && visibleWrapper.value.scrollHeight >= window.scrollY;
		}

		return {
			visibleWrapper: visibleWrapper,
		};
	},
});
</script>
