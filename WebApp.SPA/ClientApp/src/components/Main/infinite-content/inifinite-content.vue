<template>
	<div class="infinite-list-container">
		<div class="infinite-list" ref="infiniteList">
			<slot name="content"></slot>
		</div>
	</div>
</template>

<script lang="ts">
import { defineComponent, ref, onMounted } from "@vue/composition-api";
import "./infinite-content.scss";

export default defineComponent({
	setup(props, context) {
		const loading = ref(false);
		const nextItem = ref(1);
		const infiniteList = ref(null as unknown as HTMLElement);

		onMounted(() => {
			if (infiniteList) {
				infiniteList.value?.addEventListener("scroll", () => {
					if (atListBottom()) context.emit("list-at-bottom");
				});
			}
		});

		function atListBottom(): boolean {
			return infiniteList.value.scrollTop + infiniteList.value.clientHeight >= infiniteList.value.scrollHeight;
		}

		return {
			loading: loading,
			nextItem: nextItem,
			infiniteList: infiniteList,
		};
	},
});
</script>
