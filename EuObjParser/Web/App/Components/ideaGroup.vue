<template>
    <div class="idea-group">
		<div class="row idea-group-top">
			<div class="col-4 d-flex justify-content-start">
				<h4>{{ideaGroup.typeName}}</h4>
			</div>
			<div class="col-4 d-flex justify-content-center">
				<p v-if="ideaGroup.exclusiveCategory">Category: {{ideaGroup.exclusiveCategoryName}}</p>
			</div>
			<div class="col-4 d-flex justify-content-end">
				<img :src="baseUrl + ideaGroup.categoryUrl" />
			</div>
		</div>
		<div class="idea-group-bonuses">
			<div class="idea-group-bonus" v-for="idea in ideaGroup.ideas">
				<div style="height: 100%;" class="d-flex flex-row">
					<div style="width: 100%;">
						<div class="d-flex flex-column bonus-detail">
							<div class="d-flex align-items-center flex-row" v-for="bonus in idea.bonuses" :class="{active: activeBonus === bonus.type}"><img :src="baseUrl + bonus.url" class="bonus-image" />{{bonus.typeName}}: {{bonus.displayValue}}</div>
						</div>
					</div>
					<!-- <div class="d-flex justify-content-center align-items-center idea-num" style="width: 10%;">
						<p class="sideways">{{idea.name}}</h6>
					</div> -->
				</div>


			</div>
		</div>

	</div>
</template>

<script lang="ts">
    import Vue from 'vue';

    export default Vue.extend({
		props:{
			ideaGroup: Object,
			activeBonus: Number
		},
		methods:{
			
		},
		data() {
			return {
				baseUrl: window.location.origin + '/'
			};
		}
    });
</script>