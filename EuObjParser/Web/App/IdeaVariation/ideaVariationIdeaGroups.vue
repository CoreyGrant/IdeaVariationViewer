<template>
	<div class="idea-groups-page">
		<div class="filters idea-group-filters">
			<div class="monarch-power-input">
				<img :src="baseUrl + 'icons/Administrative_power.png'" :class="{active: search.monarch === 0}" @click="toggleMonarch(0)" />
				<img :src="baseUrl + 'icons/Diplomatic_power.png'" :class="{active: search.monarch === 1}" @click="toggleMonarch(1)" />
				<img :src="baseUrl + 'icons/Military_power.png'" :class="{active: search.monarch === 2}" @click="toggleMonarch(2)" />
			</div>
			<div class="bonus-input">
				<label for="idea-bonus-search">Bonus</label>
				<select v-model="search.bonus" class="form-control" id="idea-bonus-search">
					<option :value='null'>None</option>
					<option v-for="bonus in bonuses" :value="bonus.type">{{bonus.typeName}}</option>
				</select>
			</div>
			<div class="category-input">
				<label for="car-bonus-search">Category</label>
				<select v-model="search.exclusiveCategory" class="form-control" id="cat-bonus-search">
					<option :value='null'>None</option>
					<option v-for="(val, key) in exclusiveCategories" :value="val">{{key}}</option>
				</select>
			</div>
		</div>
		<div class="idea-groups">
			<idea-group v-for="idea in filteredIdeaGroups" :key="idea.name" :idea-group="idea" :active-bonus="search.bonus">
			</idea-group>
		</div>
	</div>
</template>

<script lang="ts">
	import Vue from 'vue';
	import IdeaGroup from '../Components/ideaGroup.vue';

	export default Vue.extend({
		components: {
			IdeaGroup
		},
		props:{
			bonuses: Array,
			ideaGroups: Array,
			exclusiveCategories: Object
		},
		data(): any{
			return {
				baseUrl: window.location.origin + '/',
				search: {
					bonus: null,
					monarch: null,
					exclusiveCategory: null
				}
			}
		}, 
		methods: {
			toggleMonarch(val): void{
                this.search.monarch = this.search.monarch === val
                    ? null
                    : val;
            },
		},
		computed: {
			filteredIdeaGroups(): any[]{
				var activeBonusFilter = this.search.bonus;
                var activeMonarchFilters = this.search
                    .monarch;
                var activeIdeaExclusiveCategory = this.search.exclusiveCategory;
				var ideas = this.ideaGroups;
                if(activeBonusFilter){
					ideas =
						ideas.filter(idea => {
							var bonusTypes = idea.ideas.map(x => x.bonuses.map(y => y.type)).flat();
							return bonusTypes.indexOf(activeBonusFilter) > -1;
						});
                }
                if(activeIdeaExclusiveCategory !== null){
                    ideas = 
                        ideas.filter(x => x.exclusiveCategory === activeIdeaExclusiveCategory);
                        
                }
                if(activeMonarchFilters !== null){
                    ideas = ideas.filter(x => x.category == activeMonarchFilters);
				}
                return ideas.sort((a, b) => a.name > b.name );
			}
		}
	});
</script>