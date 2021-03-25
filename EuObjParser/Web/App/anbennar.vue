<template>
	<div class="app-container">
		<h1>Anbennar</h1>
		<div class="tab-row">
			<div class="tab">
				<router-link to="/Anbennar/Countries">Countries</router-link>
			</div>
			<div class="tab">
				<router-link to="/Anbennar/Religions">Religions</router-link>
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