<template>
	<div
		class="toggable-container-overlay shadow rounded-top bg-light border"
		:class="{
			'toggable-container-is-expanded': expanded,
			'toggable-container-is-minimized': !expanded,
		}"
		ref="toggableContainer"
	>
		<header class="bg-light rounded-top border-bottom" @click="toggleContainer()">
			<div class="d-flex justify-content-between p-2">
				<div class="d-flex align-items-center overflow-hidden">
					<slot name="headerContent"></slot>
				</div>
				<div class="d-flex justify-content-between">
					<button
						type="button"
						class="navbar-toggler border-0 shadow-none not-collapsed"
					>
						<span v-if="expanded" class="material-icons shadow-none"
							>keyboard_arrow_down</span
						>
						<span v-else class="material-icons shadow-none"
							>keyboard_arrow_up</span
						>
					</button>
					<button
						v-if="showHideButton"
						type="button"
						class="navbar-toggler border-0 shadow-none not-collapsed"
						@click="hideContainer()"
					>
						<span class="material-icons shadow-none md-18">close</span>
					</button>
				</div>
			</div>
		</header>
		<slot name="beforeMainContent"></slot>
		<slot name="mainContent"></slot>
		<slot name="afterMainContent"></slot>
	</div>
</template>

<script lang="ts">
import { defineComponent, ref, watch, onMounted } from "@vue/composition-api";
import "./toggable-container.scss";

export default defineComponent({
	props: {
		showExpanded: {
			type: Boolean,
			required: false,
			default: true,
		},
		showHideButton: {
			type: Boolean,
			required: false,
			default: false,
		},
		height: {
			type: String,
			required: false,
			default: "calc(100vh - 100px)",
		},
		width: {
			type: String,
			required: false,
			default: "288px",
		},
		toggableContainerMargin: {
			type: String,
			required: false,
			default: "0px",
		},
	},
	setup(props, context) {
		const expanded = ref(props.showExpanded);
		function toggleContainer() {
			expanded.value = !expanded.value;
		}

		function expand() {
			expanded.value = true;
		}

		function minimize() {
			expanded.value = false;
		}

		watch(
			() => props.showExpanded,
			(newVal: boolean) => {
				expanded.value = newVal;
			},
		);

		const toggableContainer = ref(null as unknown as HTMLElement);
		function setToggableContainerStyle() {
			toggableContainer.value.style.height = props.height;
			toggableContainer.value.style.width = props.width;
			toggableContainer.value.style.flex = `$0 0 ${props.width}`;
			toggableContainer.value.style.marginRight = props.toggableContainerMargin;
		}

		onMounted(() => {
			setToggableContainerStyle();
		});

		function hideContainer() {
			context.emit("hideContainer");
		}

		return {
			toggableContainer,
			expanded,
			toggleContainer,
			hideContainer,
			expand,
			minimize,
		};
	},
});
</script>
