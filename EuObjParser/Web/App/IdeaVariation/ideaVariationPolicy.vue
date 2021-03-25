<template>
	<div class="policy-page">
		<div>
			<p class="link" @click="navigateBack()">&lt; Back</p>
		</div>
		<div class="full-policy">
			<div class="d-flex flex-row justify-content-between">
				<h4>{{selectedPolicy.displayName}}</h4>
				<img :src="baseUrl + selectedPolicy.monarchPowerUrl" />
			</div>
			<div class="row">
				<div class="col">
					<div class="d-flex flex-column">
						<div class="d-flex align-items-center flex-row" v-for="bonus in selectedPolicy.bonuses"><img :src="baseUrl + bonus.url" class="bonus-image" />{{bonus.typeName}}: {{bonus.displayValue}}</div>
					</div>
				</div>
			</div>
			<div class="row" v-if="selectedPolicy.allow.policyAllowLimit">
				<div class="col">
					<h4>Restrictions</h4>
					<p>This policy can only be active when less than {{selectedPolicy.allow.policyAllowLimit.amount}} of the following policies are active:</p>
					<p v-for="policyName in selectedPolicy.allow.policyAllowLimit.policies" @click="navigateToPolicy(policyByName(policyName))" v-if="policyName != selectedPolicy.name" class="link">{{policyByName(policyName).displayName}}</p>
				</div>
			</div>
		</div>
	</div>
</template>

<script lang="ts">
	import Vue from 'vue';

	export default Vue.extend({
		data() {
			return {
				baseUrl: window.location.origin + '/',
			};
		}
	});
</script>