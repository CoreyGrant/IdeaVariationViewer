<template>
	<div class="app-container">
		<nav class="mod-navigation">
			<div class="nav-start">
				<router-link to="/Index">Home</router-link>
			</div>
			<ul>
				<li v-for="section in sections"><router-link :to="'/Mod/' + id + '/' + section.name">{{section.displayName}}</router-link></li>
			</ul>
			<div class="nav-end">
				<a v-if="id" :href="'https://steamcommunity.com/sharedfiles/filedetails/?id=' + id" target="_blank">Steam Workshop</a>
			</div>
		</nav>
		<div class="page">
			<h2>{{name}}</h2>
			<router-view></router-view>
		</div>
	</div>
</template>
<script lang="ts">
	import Vue from 'vue';

	export default Vue.extend({
		data(): any {
			return {};
		},
		props: {
			name: String,
			id: Number,
			sections: Array
		}
	});
</script>