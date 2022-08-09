<template>
	<div class="p-2 m-2 bg-danger chatbox">
		{{ chatBox.text }}
	</div>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";

export default defineComponent({
	props: {
		chatBox: {
			type: null as () => { id: string; text: string },
		},
	},
	setup() {},
});
</script>
