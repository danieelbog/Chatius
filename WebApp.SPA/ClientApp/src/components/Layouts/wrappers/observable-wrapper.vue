<template>
	<div ref="observable">
		<slot name="default"></slot>
	</div>
</template>

<script lang="ts">
import { defineComponent, onBeforeUnmount, onMounted, ref } from "@vue/composition-api";

interface ObserverOptions {
	root: HTMLElement;
	rootMarging: String;
}

export default defineComponent({
	props: {
		options: {
			type: Object as () => ObserverOptions,
			required: false,
			default: () => new Object() as ObserverOptions,
		},
	},
	setup(props, context) {
		const observable = ref(null as unknown as HTMLElement);

		onMounted(() => {
			startObserving();
		});

		onBeforeUnmount(() => {
			observer.disconnect();
		});

		const observer = new IntersectionObserver(([entry]) => {
			if (entry && entry.isIntersecting) context.emit("intersect");
		}, props.options);

		function startObserving() {
			if (!observable) return;
			observable.value.style.height = "25px";
			observer.observe(observable.value);
		}

		return {
			observable: observable,
		};
	},
});
</script>
