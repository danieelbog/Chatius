<template>
	<div class="infinite-scroller">
		<slot name="content"></slot>
	</div>
</template>

<script lang="ts">
import { defineComponent, onBeforeUnmount, onMounted, onUnmounted, ref } from "@vue/composition-api";
import "./infinite-scroller.scss";

export default defineComponent({
	setup(props, context) {
		const loading = ref(false);

		onMounted(() => {
			window.addEventListener("scroll", emitAtPageBottom);
		});

		onBeforeUnmount(() => {
			window.removeEventListener("scroll", emitAtPageBottom);
		});

		function emitAtPageBottom() {
			if (!atPageBottom()) return;
			context.emit("pageAtBottom");
		}

		function atPageBottom(): boolean {
			const totalPageHeight = document.body.scrollHeight;
			const scrollPoint = window.scrollY + window.innerHeight;
			return scrollPoint >= totalPageHeight;
		}

		return {
			loading: loading,
		};
	},
});
</script>
