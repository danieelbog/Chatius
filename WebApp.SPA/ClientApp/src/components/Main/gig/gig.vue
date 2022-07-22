<template>
	<div>{{ text }} Daniel Is here</div>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";

export default defineComponent({
	props: {
		text: {
			type: String,
			required: true,
		},
	},
	setup(props, context) {},
});
</script>
