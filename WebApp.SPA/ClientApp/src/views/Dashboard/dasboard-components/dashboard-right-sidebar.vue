<template>
	<div class="d-none d-lg-block col-md-3">
		<div class="pt-3 pb-3 pl-0 pr-0">
			<card-recommendation></card-recommendation>
		</div>
		<sticky-wrapper :distanceFromTop="75">
			<template v-slot:content>
				<div class="pt-3 pb-3 pl-0 pr-0 sticky-top">
					<card-footer></card-footer>
				</div>
			</template>
		</sticky-wrapper>
	</div>
</template>

<script lang="ts">
import { defineComponent } from "@vue/composition-api";
import CardRecommendation from "../../../components/Main/cards/card-recommendation.vue";
import CardFooter from "../../../components/Main/cards/card-footer.vue";
import StickyWrapper from "../../../components/Layouts/wrappers/sticky-wrapper.vue";

export default defineComponent({
	components: {
		CardRecommendation,
		CardFooter,
		StickyWrapper,
	},
	setup() {},
});
</script>
