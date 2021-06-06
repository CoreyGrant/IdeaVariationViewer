<template>
	<div class="policies-page">
		<div class="filters policies-filters">
			<div class="monarch-power-input">
				<img :src="baseUrl + 'Icons/Administrative_power.png'" :class="{active: search.monarch === 'ADM'}" @click="toggleMonarch('ADM')" />
				<img :src="baseUrl + 'Icons/Diplomatic_power.png'" :class="{active: search.monarch === 'DIP'}" @click="toggleMonarch('DIP')" />
				<img :src="baseUrl + 'Icons/Military_power.png'" :class="{active: search.monarch === 'MIL'}" @click="toggleMonarch('MIL')" />
			</div>
			<div class="bonus-input form-row">
				<label for="idea-bonus-search col-2 col-form-label">Bonus</label>
				<div class="col-10">
					<select v-model="search.bonus" class="form-control" id="policy-bonus-search">
						<option :value='null'>None</option>
						<option v-for="bonus in bonuses" :value="bonus">{{bonus | bonusName}}</option>
					</select>
				</div>
			</div>
			<div class="bonus-input form-row">
				<label for="idea-bonus-search col-2 col-form-label">Idea</label>
				<div class="col-10">
					<select v-model="search.ideaGroup" class="form-control" id="policy-bonus-search">
						<option :value='null'>None</option>
						<option v-for="ideaGroup in ideaGroups" :value="ideaGroup.name">{{ideaGroupName(ideaGroup)}}</option>
					</select>
				</div>
			</div>
			<p class="ml-4">{{filteredPolicies.length}} policies</p>
		</div>
		<div class="policies">
			<expanding-scroll-list @load-more="listLength += 40">
				<policy v-for="policy in pagedPolicies" :policy="policy" :active-bonus="search.bonus" :key="policy.name" :policy-name-display-name-map="nameDisplayNameMap" :idea-groups="ideaGroups"></policy>
			</expanding-scroll-list>
		</div>
	</div>
</template>

<script lang="ts">
	import Vue from 'vue';
	import Policy from '../../Components/policy.vue';
	import helpers from '../../Helpers/helpers';
	import ExpandingScrollList from '../../Components/expandingScrollList.vue';
	import { getFile } from '../../appData';
	import { getBonusName } from '../../Helpers/getBonusUrl';

	export default Vue.extend({
		components: {
			Policy,
			ExpandingScrollList
		},
		data(): any{
			return {
				listLength: 40,
				baseUrl: window.location.origin + '/',
				search: {
					bonus: null,
					monarch: null,
					ideaGroup: null
				},
				bonuses: [],
				policies: [],
				ideaGroups: [],
			};
		},
		props:{
			id: Number,
		},
		created(): void {
			getFile(this.id + "/ideaGroups.json").then(x => this.ideaGroups = x.ideaGroups);
			getFile(this.id + "/policies.json").then(x => {
				this.policies = x.policies;
			});
			getFile(this.id + "/bonuses.json").then(x => this.bonuses = x.bonuses.sort((x, y) => getBonusName(x) > getBonusName(y)));

		},
		methods: {
			ideaGroupName(ideaGroup): string {
				return ideaGroup.localizedName || (this.$options as any).filters["bonusName"](ideaGroup.name);
			},
			createName(policy) {
				var potential = policy.potential;
				var conditions = potential.conditions;
				var nameParts: string[] = [];
				for (var condition of conditions) {
					if (condition.name == "has_idea_group") {
						nameParts.push(this.ideaGroups.find(x => x.name == condition.value).localizedName || this.tidyName(condition.value));
					}
				}
				return nameParts.join(" + ");
			},
			toggleMonarch(val): void {
				this.search.monarch = this.search.monarch === val
					? null
					: val;
			},
			tidyName(name: string): string {
				var deideaedName = name.split("_ideas")[0];
				var tidiedName = (this.$options as any).filters["bonusName"](deideaedName);
				return tidiedName;
			},
		},
		computed: {
			nameDisplayNameMap(): any {
				return this.policies.reduce((prev, cur) => {
					return Object.assign({}, prev, {[cur.name]: this.createName(cur)});
				}, {});
			},
			filteredPolicies: function (): any[] {
				var activeBonusFilter: string | null = this.search.bonus;
				var activeMonarchFilters: string | null = this.search
					.monarch;
				var activeIdeaGroupFilter: string | null = this.search.ideaGroup;
				var policies: any[] = this.policies;
				if (activeBonusFilter !== null) {
					policies =
						policies.filter(x => Object.keys(x.bonuses).some(x => x == activeBonusFilter));
				}
				if (activeMonarchFilters !== null) {
					policies = policies.filter(x => x.monarchPower == activeMonarchFilters);
				}
				if (activeIdeaGroupFilter !== null) {
					policies = policies.filter(helpers.ideaGroupHasPolicy.bind(null, activeIdeaGroupFilter));
				}
				return policies || [];
			},
			pagedPolicies: function(): any[]{
				return this.filteredPolicies.slice(0, this.listLength);
			}
		},
		watch: {
			search: {
				handler() {
					this.listLength = 40;
				},
				deep: true
			},
		}
	});
</script>