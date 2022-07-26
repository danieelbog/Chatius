<template>
	<div class="col-0 col-md-3">
		<visible-wrapper>
			<template v-slot:content>
				<div class="p-3">
					<card-profile></card-profile>
				</div>
			</template>
		</visible-wrapper>
		<sticky-wrapper :distanceFromTop="75">
			<template v-slot:content>
				<div class="p-3 sticky-top">
					<card-info></card-info>
				</div>
			</template>
		</sticky-wrapper>
	</div>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";
import CardProfile from "../../../components/Main/cards/card-profile.vue";
import CardInfo from "../../../components/Main/cards/card-info.vue";
import StickyWrapper from "../../../components/Layouts/wrappers/sticky-wrapper.vue";
import VisibleWrapper from "../../../components/Layouts/wrappers/visible-wrapper.vue";
export default defineComponent({
	components: {
		CardProfile,
		CardInfo,
		StickyWrapper,
		VisibleWrapper,
	},
	setup() {},
});
</script>
