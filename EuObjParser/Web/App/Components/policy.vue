<template>
	<div class="policy">
		<div class="d-flex flex-row justify-content-between">
			<h4 @click="navigateToPolicy(policy)" class="link">{{policy.displayName}}</h4>
			<img :src="baseUrl + policy.monarchPowerUrl" />
		</div>
		<div class="row">
			<div class="col">
				<div class="d-flex flex-column">
					<div class="d-flex align-items-center flex-row" v-for="bonus in policy.bonuses" :class="{active: activeBonus === bonus.type}"><img :src="baseUrl + bonus.url" class="bonus-image" />{{bonus.typeName}}: {{bonus.displayValue}}</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script lang="ts">
    import Vue from 'vue';

    export default Vue.extend({
		props:{
			policy: Object,
			activeBonus: Number
		},
		data() {
			return {
				baseUrl: window.location.origin + '/',
			};
		},
		methods:{
			
		}
    });
</script>