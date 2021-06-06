<template>
    <div class="idea-group">
		<div class="idea-group-top">
			<div class="d-flex flex-row justify-content-between">
				<h4 style="margin-left: 4px;  margin-top: 4px; font-size: 1.6rem">{{name}}</h4>
				<app-icon class="bonus-image" :name="ideaGroup.category" />
			</div>
		</div>
		<div class="idea-group-bonuses">
			<div class="idea-group-bonus d-flex flex-row" v-for="(idea, index) in orderedIdeas">
				<div style="width: 16px;" class="d-flex align-items-center justify-content-center">
					<p v-if="idea.name == 'bonus'" style="font-size: 12px; writing-mode: vertical-lr; text-orientation: mixed;">+</p>
					<p v-if="idea.name != 'bonus' && idea.name != 'start'">{{index + 1}}</p>
				</div>
				<div class="d-flex flex-column bonus-detail">
					<bonus v-for="(value, name) in idea.bonuses" :name="name" :value="value" :activeBonus="activeBonus == name" :key="name" />
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
		components:{
			AppIcon,
			Bonus
		},
		props:{
			ideaGroup: Object,
			activeBonus: String
		},
		methods:{
			
		},
		data() {
			return {
				baseUrl: window.location.origin + '/'
			};
		},
		computed: {
			name(): string {
				return this.ideaGroup.localizedName || (this.$options as any).filters["bonusName"](this.ideaGroup.name);
			},
			orderedIdeas: function():any[]{
				var ideas = [...this.ideaGroup.ideas];
				var final = ideas.find(x => x.name.toLowerCase() == "bonus");
				return [
					... ideas.filter(x => x.name.toLowerCase() !== "bonus"),
					final
				];
			}
		}
    });
</script>