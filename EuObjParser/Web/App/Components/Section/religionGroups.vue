<template>
	<div class="religions-page">
		<expanding-scroll-list>
			<div class="religion-groups">
				<religion-group v-for="religionGroup in religionGroups" :religion-group="religionGroup" :key="religionGroup.displayName"></religion-group>
			</div>
		</expanding-scroll-list>
	</div>
</template>

<script lang="ts">
	import Vue from 'vue';
	import ReligionGroup from '../../Components/religionGroup.vue';
	import ExpandingScrollList from '../../Components/expandingScrollList.vue';
	import { getFile } from '../../appData';

	export default Vue.extend({
		components: {
			ReligionGroup,
			ExpandingScrollList
		},
		props: {
			id: Number,
		},
		data() {
			return {
				religionGroups: [],
				bonuses: []
			};
		},
		created(): void {
			getFile(this.id + "/religionGroups.json").then(x => this.religionGroups = x.religionGroups);
			getFile(this.id + "/bonuses.json").then(x => this.bonuses = x.bonuses);
		},
		methods: {
		},
		computed: {
			
		}
	});
</script>