<template>
	<div class="app-container">
		<div class="index-page">
			<h1>EU4 Mod Viewer</h1>
			<div class="row">
				<div class="col-md-12">
					<p>The app is still in development, so features may be in flux and there will be bugs. If you want to report a bug, request a feature or mod, or just ask a question, the Discord is <a href="https://discord.gg/HMKN7tY7zV" target="_blank">here</a></p>
				</div>
			</div>
			<div class="mod-list">
				<div class="row">
					<div class="col-12">
						<div class="filters mod-filters">
							<div class="name-input d-flex">
								<label for="name-search" class=" mr-3">Name</label>
								<input id="name-search" type="text" class="form-control" v-model="search.name" />
							</div>
							<p class="ml-4">{{filteredMods.length}} mods</p>
						</div>
					</div>
				</div>
				<div class="row">
					<div class="col-12">
						<table class="table">
							<thead>
								<tr>
									<th style="color: #d4d4d4">Mod (steam link)</th>
									<th style="color: #d4d4d4"><p class="ml-4">Sections</p></th>
								</tr>
							</thead>
							<tbody>
								<tr v-for="mod in filteredMods">
									<td><a :href="steamLink(mod)" target="_blank">{{mod.name}}</a></td>
									<td><router-link class="ml-4" v-for="section in mod.sections" :to="'/Mod/' + mod.id + '/' + section.name">{{section.displayName}}</router-link></td>
								</tr>
							</tbody>
						</table>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>

<script lang="ts">
	import Vue from 'vue';

	export default Vue.extend({
		components: {
		},
		props:{
			mods: Array
		},

		data() : any{
			return {
				baseUrl: window.location.origin + '/',
				search: {
					name: ''
				}
			};
		},
		created(): void{
		},
		computed: {
			filteredMods(): any[] {
				var searchName = this.search.name;
				var mods = this.mods;
				if (searchName) {
					mods = mods.filter(x => x.name.toLowerCase().includes(searchName.toLowerCase()));
				}
				return mods.sort((x,y) => x.name.toLowerCase() > y.name.toLowerCase());
			}
		},
		methods: {
			steamLink(mod): string {
				return mod.id
					? "https://steamcommunity.com/sharedfiles/filedetails/?id=" + mod.id
					: "https://store.steampowered.com/app/236850/Europa_Universalis_IV/"
			}
		}
	});
</script>
