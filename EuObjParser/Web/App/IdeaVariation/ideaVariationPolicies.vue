<template>
	<div class="policies-page">
			<div class="filters policies-filters">
				<div class="monarch-power-input">
					<img :src="baseUrl + 'icons/Administrative_power.png'" :class="{active: search.monarch === 0}" @click="toggleMonarch(0)" />
					<img :src="baseUrl + 'icons/Diplomatic_power.png'" :class="{active: search.monarch === 1}" @click="toggleMonarch(1)" />
					<img :src="baseUrl + 'icons/Military_power.png'" :class="{active: search.monarch === 2}" @click="toggleMonarch(2)" />
				</div>
				<div class="bonus-input form-row">
					<label for="idea-bonus-search col-2 col-form-label">Bonus</label>
					<div class="col-10">
						<select v-model="search.bonus" class="form-control" id="policy-bonus-search">
							<option value=''>None</option>
							<option v-for="bonus in bonuses" :value="bonus.type">{{bonus.typeName}}</option>
						</select>
					</div>
				</div>
			</div>
			<div class="policies">
				<policy v-for="policy in filteredPolicies" :policy="policy" :active-bonus="search.bonus" :key="policy.name"></policy>
			</div>
		</div>
</template>

<script lang="ts">
	import Vue from 'vue';
	import Policy from '../Components/policy.vue';


	export default Vue.extend({
		components: {
			Policy
		},
		data(): any{
			return {
				baseUrl: window.location.origin + '/',
				search: {
					bonus: null,
					monarch: null
				}
			};
		},
		props:{
			bonuses: Array,
			policies: Array
		},
		methods:{
			toggleMonarch(val): void {
				this.search.monarch = this.search.monarch === val
					? null
					: val;
			},
		},
		computed: {
			filteredPolicies: function (): any[] {
				var activeBonusFilter: number | null = this.search.bonus;
				var activeMonarchFilters: number | null = this.search
					.monarch;
				var policies: any[] = this.policies;
				if (activeBonusFilter !== null) {
					policies =
						policies.filter(x => x.bonuses.some(x => x.type == activeBonusFilter));

				}
				if (activeMonarchFilters !== null) {
					policies = policies.filter(x => x.monarchPower == activeMonarchFilters);
				}
				return policies;
			},
		}
	});
</script>