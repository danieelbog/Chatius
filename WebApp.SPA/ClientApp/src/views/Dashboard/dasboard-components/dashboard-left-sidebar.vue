<template>
	<div class="d-none d-lg-block col-md-3">
		<div class="p-3">
			<card-profile></card-profile>
		</div>

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
import ObservableWrapper from "../../../components/Layouts/wrappers/observable-wrapper.vue";
export default defineComponent({
	components: {
		CardProfile,
		CardInfo,
		StickyWrapper,
		ObservableWrapper,
	},
	setup() {},
});
</script>
