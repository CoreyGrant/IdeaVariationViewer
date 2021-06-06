<template>
	<div class="app-header">
		<div class="app-header-icon d-flex justify-content-center align-items-center ml-4 mr-4">
			<app-icon url="app-icon.png"/>
		</div>
		<div class="vertical-divider"></div>
		<div v-if="$route.path !== '/Index'" class="app-header-title d-flex justify-content-center align-items-center ml-4 mr-4">
			<h2 class="m0 p0" @click="$router.push('/Index')">Eu4 mod viewer</h2>
		</div>
		<div class="vertical-divider"></div>
		<nav class="app-navigation ml-2">
			<a href="https://discord.gg/HMKN7tY7zV" target="_blank">Discord</a>
		</nav>
	</div>
</template>

<script lang="ts">
	import Vue from 'vue';
	import AppIcon from './appIcon.vue';

	export default Vue.extend({
		components: {
			AppIcon,
		},
		data() {
			return {
			};
		},
		
	});
</script>
