<template>
	<div class="country" v-bind:style="getCountryStyle(country)">
		<div class="country-bonuses">
			<div class="d-flex flex-row justify-content-between">
				<h4 style="margin-left: 4px; margin-top: 4px; font-size: 1.6rem">{{country.name}}</h4>
				<app-icon v-if="country.tag" class="flag-image" :url="'/Data/' + modId + '/flags/' + country.tag + '.png'"></app-icon>
			</div>
			<div class="country-idea-group-bonus d-flex flex-row" v-for="(idea, index) in orderedIdeas">
				<div style="width: 16px;" class="d-flex align-items-center justify-content-center">
					<p v-if="idea.name == 'start'" style="font-size: 12px; writing-mode: vertical-lr; text-orientation: mixed;">trad</p>
					<p v-if="idea.name == 'bonus'" style="font-size: 12px; writing-mode: vertical-lr; text-orientation: mixed;">+</p>
					<p v-if="idea.name != 'bonus' && idea.name != 'start'">{{index}}</p>
				</div>
				<div class="d-flex flex-column ml-1" :title="$options.filters.bonusName(idea.name)">
					<bonus v-for="(value, name) in idea.bonuses" :name="name" :value="value" :activeBonus="activeBonus == name" :key="name"/>
				</div>
			</div>
		</div>
	</div>
</template>

<script lang="ts">
    import Vue from 'vue';
	import AppIcon from './appIcon.vue';
	import Bonus from './bonus.vue';

	export default Vue.extend({
		components: {
			AppIcon,
			Bonus
		},
		props:{
			country: Object,
			activeBonus: String,
			modId: Number
		},
		methods:{
			getCountryStyle(country) {
				if (!country.colors || !country.colors.length) {
					return null;
				}
				var style = {
					"border": "3px solid " + country.colors[0].rgb,
				};
				return style;
			},
		},
		data() {
			return {
			};
		},
		computed:{
			orderedIdeas: function():any[]{
				var ideas = [...this.country.ideas];
				var final = ideas.find(x => x.name.toLowerCase() == "bonus");
				return [
					... ideas.filter(x => x.name.toLowerCase() !== "bonus"),
					final
				];
			}
		}
    });
</script>