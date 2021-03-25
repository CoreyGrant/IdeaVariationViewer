<template>
	<div class="planner-page">
			<div class="planner">
				<div class="planner-idea-groups">
					<div class="planner-idea-group-add">
						<div class="d-flex">
							<label for="planner-idea-group-add">Idea group </label>
							<v-select class="planner-idea-group-add-select" :options="plannerAvailableIdeas"
									  v-model="planner.selectedIdeaGroup" label="name">
								<template slot="option" slot-scope="option">
									{{ option.name }}
									<img :src="option.categoryUrl" />
								</template>
							</v-select>
							<p class="link planner-idea-group-add-confirm" @click="plannerAddSelectedIdea()" :class="{disabled: !planner.selectedIdeaGroup}" style="text-decoration: none;">+</p>
						</div>
					</div>
					<div class="planner-idea-group-show">
						<img :src="getImageLink('ADM')" />
						<div>
							<div v-for="idea in filteredPlannerIdeas(0)" @click="removePlannerIdeaGroup(idea)" class="planner-choice">
								{{idea}}
							</div>
						</div>
						<img :src="getImageLink('DIP')" />
						<div>
							<div v-for="idea in filteredPlannerIdeas(1)" @click="removePlannerIdeaGroup(idea)" class="planner-choice">
								{{idea}}
							</div>
						</div>
						<img :src="getImageLink('MIL')" />
						<div>
							<div v-for="idea in filteredPlannerIdeas(2)" @click="removePlannerIdeaGroup(idea)" class="planner-choice">
								{{idea}}
							</div>
						</div>
					</div>
				</div>
				<div class="planner-policies">
					<div class="planner-policies-add">
						<div class="d-flex">
							<label for="planner-policies-add">Policy </label>
							<v-select class="planner-policies-add-select" :options="plannerAvailablePolicies"
									  v-model="planner.selectedPolicy" label="name">
								<template slot="option" slot-scope="option">
									{{ option.displayAllow }}
									<img :src="getImageLink(option.monarchPower)" v-if="option.monarchPower" />
								</template>
							</v-select>
							<p class="link planner-policies-add-confirm" @click="plannerAddSelectedPolicy()" :class="{disabled: !planner.selectedPolicy}" style="text-decoration: none;">+</p>
						</div>
					</div>
					<div class="planner-policies-show">
						<img :src="getImageLink('ADM')" />
						<div>
							<div v-for="policy in filteredPlannerPolicies(0)" @click="removePlannerPolicy(policy)" :title="getTitleForPlannerPolicy(policy)" class="planner-choice">
								{{policy}}
							</div>
						</div>
						<img :src="getImageLink('DIP')" />
						<div>
							<div v-for="policy in filteredPlannerPolicies(1)" @click="removePlannerPolicy(policy)" :title="getTitleForPlannerPolicy(policy)" class="planner-choice">
								{{policy}}
							</div>
						</div>
						<img :src="getImageLink('MIL')" />
						<div>
							<div v-for="policy in filteredPlannerPolicies(2)" @click="removePlannerPolicy(policy)" :title="getTitleForPlannerPolicy(policy)" class="planner-choice">
								{{policy}}
							</div>
						</div>
					</div>
				</div>
				<div class="planner-bonuses">
					<div v-for="bonus in plannerBonuses"><img :src="bonus.url" class="bonus-image" />{{bonus.name}}: {{bonus.displayValue}}</div>
				</div>
			</div>
		</div>
</template>

<script lang="ts">
	import Vue from 'vue';

	export default Vue.extend({
    data() {
        return {};
    }
	});
		
</script>