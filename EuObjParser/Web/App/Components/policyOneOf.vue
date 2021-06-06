<template>
	<span :title="title">(One of)</span>

</template>
<script lang="ts">
	import Vue from 'vue';
	import AppIcon from './appIcon.vue';

	export default Vue.extend({
		components: {
			AppIcon
		},
		props: {
			potential: Object,
			ideaGroups: Array,
		},
		data(): any {
			return {
			};
		},
		mounted() {
		},
		methods: {
			getLocalizedName(ideaGroupName: string): string {
				var ideaGroup = this.ideaGroups.find(x => x.name == ideaGroupName);
				return ideaGroup
					? ideaGroup.localizedName
					: (this.$options as any).filters["bonusName"](ideaGroupName);
			}
		},
		computed: {
			title(): string {
				var lines = ["The policy also requires one of\nthe following idea groups:"];
				var hasIdeaGroups = this.potential.conditionSets
					.find(x => x.composeOr).conditions
					.filter(x => x.name == "has_idea_group");
				hasIdeaGroups
					.map(x => this.getLocalizedName(x.value))
					.forEach(x => lines.push(x));
				return lines.join("\n");
			}
		},
		watch: {
		}
	});
</script>