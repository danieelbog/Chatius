<template>
	<div
		class="toggable-container bottom-0"
		:class="{
			'end-0': directionRight,
			'start-0': !directionRight,
			'position-sticky': position == 'sticky',
			'position-fixed': position == 'fixed',
		}"
	>
		<slot name="beforeMainContainer"></slot>
		<div
			class="toggable-container-overlay shadow rounded-top"
			:class="{ 'toggable-container-is-expanded': expanded, 'toggable-container-is-minimized': !expanded }"
			ref="toggableContainer"
		>
			<header class="bg-light rounded-top border" @click="toggleContainer()">
				<div class="d-flex justify-content-between p-2">
					<div class="d-flex align-items-center">
						<slot name="headerContent"></slot>
					</div>
					<div class="d-flex justify-content-between">
						<button type="button" class="navbar-toggler border-0 shadow-none not-collapsed">
							<span v-if="expanded" class="material-icons shadow-none">keyboard_arrow_down</span>
							<span v-else class="material-icons shadow-none">keyboard_arrow_up</span>
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
			<section class="bg-light toggable-container-list border border-top-0">
				<div class="toggable-container-list-overlay">
					<slot name="mainContent"></slot>
				</div>
			</section>
		</div>
		<slot name="afterMainContainer"></slot>
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
		directionRight: {
			type: Boolean,
			required: false,
			default: true,
		},
		position: {
			type: String,
			required: false,
			default: "fixed",
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
		toggableContainerLeftMaring: {
			type: String,
			required: false,
			default: "0px",
		},
		toggableContainerRightMaring: {
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

		watch(
			() => expanded.value,
			(newVal: boolean) => {
				expanded.value = newVal;
			},
		);

		const toggableContainer = ref(null as unknown as HTMLElement);
		function setToggableContainerStyle() {
			toggableContainer.value.style.height = props.height;
			toggableContainer.value.style.width = props.width;
			toggableContainer.value.style.flex = `$0 0 ${props.width}`;
			toggableContainer.value.style.marginRight = props.toggableContainerRightMaring;
			toggableContainer.value.style.marginLeft = props.toggableContainerLeftMaring;
		}

		onMounted(() => {
			setToggableContainerStyle();
		});

		function hideContainer() {
			context.emit("hideContainer");
		}

		return {
			toggableContainer: toggableContainer,
			expanded: expanded,
			toggleContainer: toggleContainer,
			hideContainer: hideContainer,
		};
	},
});
</script>
