<template>
	<div class="app-container">
		<h1>Idea variation</h1>
		<div class="tab-row">
			<div class="tab">
				<router-link to="/IdeaVariation/IdeaGroups">Idea groups</router-link>
			</div>
			<div class="tab">
				<router-link to="/IdeaVariation/Policies">Policies</router-link>
			</div>
			<div class="tab">
				<router-link to="/IdeaVariation/Planner">Planner</router-link>
			</div>
		</div>
		<router-view></router-view>
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