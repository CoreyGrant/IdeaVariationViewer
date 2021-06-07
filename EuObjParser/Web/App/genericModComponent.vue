<template>
	<div class="app-container">
		<nav class="mod-navigation">
			<div class="nav-start">
				<router-link to="/Index">Home</router-link>
			</div>
			<ul>
				<li v-for="section in sections"><router-link :to="'/Mod/' + id + '/' + section.name">{{section.displayName}}<img :src="sectionUrl(section)" style="width: 36px; height: 36px;"/></router-link></li>
			</ul>
			<div class="nav-end">
				<a v-if="id" :href="'https://steamcommunity.com/sharedfiles/filedetails/?id=' + id" target="_blank">Steam</a>
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
		},
		methods: {
			sectionUrl(section): string {
				switch (section.name) {
					case 'countries':
						return '/Icons/countries.png';
					case 'ideaGroups':
						return '/Icons/ideas.png';
					case 'policies':
						return '/Icons/policies.png';
					case 'religionGroups':
						return '/Icons/religions.png';
				}
				return "";
			}
		}
	});
</script>