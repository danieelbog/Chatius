<template>
	<div class="toggable-container bottom-0" :class="setToggableContainerPosition()">
		<slot name="beforeMainContainer"></slot>
		<div class="toggable-container-overlay shadow rounded-top" ref="toggableContainer">
			<header class="bg-light rounded-top border" @click="toggleContainer()">
				<div class="d-flex justify-content-between p-2">
					<div class="d-flex align-items-center">
						<slot name="headerContent"></slot>
					</div>
					<div class="d-flex justify-content-between">
						<button type="button" class="navbar-toggler border-0 shadow-none not-collapsed">
							<span class="material-icons shadow-none">{{ expandedIcon }}</span>
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
	setup(props) {
		const expanded = ref(true);
		function toggleContainer() {
			expanded.value = !expanded.value;
		}

		watch(
			() => expanded.value,
			(newVal: boolean) => {
				expanded.value = newVal;
				setExpandedIcon();
				setExpandedClass();
			},
		);

		const expandedIcon = ref("keyboard_arrow_down");
		function setExpandedIcon() {
			if (expanded.value) expandedIcon.value = "keyboard_arrow_down";
			else expandedIcon.value = "keyboard_arrow_up";
		}

		const toggableContainer = ref(null as unknown as HTMLElement);
		function setExpandedClass() {
			if (!toggableContainer.value) return;

			if (expanded.value) {
				toggableContainer.value.classList.remove("toggable-container-is-minimized");
				toggableContainer.value.classList.add("toggable-container-is-expanded");
			} else {
				toggableContainer.value.classList.remove("toggable-container-is-expanded");
				toggableContainer.value.classList.add("toggable-container-is-minimized");
			}
		}

		function setToggableContainerPosition() {
			if (props.directionRight) return `end-0 position-` + props.position;
			return `start-0 position-` + props.position;
		}

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

		return {
			toggableContainer: toggableContainer,
			expandedIcon: expandedIcon,
			setToggableContainerPosition: setToggableContainerPosition,
			toggleContainer: toggleContainer,
		};
	},
});
</script>
