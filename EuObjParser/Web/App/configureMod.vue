<template>
    <div class="app-container">
		<div class="configure-mod">
			<div class="row">
				<div class="col-12">
					<label>Id <input v-model="id" class="form-control"/></label>
				</div>
			</div>
			<div class="row">
				<div class="col-12">
					<label>Name <input v-model="name" class="form-control"/></label>
				</div>
			</div>
			<div class="row">
				<div class="col-3">
					<label><input type="checkbox" v-model="sections.policies" /> Policies</label>
					<label><input type="checkbox" v-model="sections.ideas" /> Ideas</label>
					<label><input type="checkbox" v-model="sections.countries" /> Countries</label>
					<label><input type="checkbox" v-model="sections.religions" /> Religions</label>
				</div>
			</div>
			<div class="row" v-for="folder in folders" v-if="show(folder)">
				<div class="col">
					<label>{{folder}}</label>
				</div>
				<div class="col">
					<mod-folder-input v-on:change="inputChange(folder, $event)" :value="fileMappings[folder]"></mod-folder-input>
				</div>
			</div>
			<div class="row" v-for="folder in historyFolders" v-if="showHistory(folder)">
				<div class="col">
					<label>{{'History/' + folder}}</label>
				</div>
				<div class="col">
					<mod-folder-input v-on:change="inputHistoryChange(folder, $event)" :value="fileMappings.history[folder]"></mod-folder-input>
				</div>
			</div>
			<div class="row" v-if="valid">
				<p>Now take the completed json and post it in the discord, and the mod will get added asap.</p>
				<code>
					{{jsonOutput}}
				</code>
			</div>
		</div>
	</div>
</template>

<script lang="ts">
    import Vue from 'vue';
	import ModFolderInput from './Components/modFolderInput.vue'

	export default Vue.extend({
		components: {
			ModFolderInput
		},
		data(): any {
			return {
				valid: false,
				id: '',
				name: '',
				fileMappings:{
					history: { countries: null },
					countries: null,
					countryTags: null,
					ideas: null,
					cultures: null,
					policies: null,
					religions: null,
					churchAspects: null,
				},
				sections: {
					policies: false,
					ideas: false,
					religions: false,
					countries: false
				},
				folders: ["countries", "countryTags", "ideas", "cultures", "policies", "religions", "churchAspects"],
				historyFolders: ["countries"]
			};
		},
		computed:{
			jsonOutput(): string{
				var mappings = this.fileMappings;
				mappings.sections = this.sections;
				if (!mappings.countries) { delete mappings.countries; }
				if (!mappings.countryTags) { delete mappings.countryTags; }
				if (mappings.history && !mappings.history.countries) { delete mappings.history.countries; }
				if (!mappings.cultures) { delete mappings.cultures; }
				if (!mappings.ideas) { delete mappings.ideas; }
				if (!mappings.policies) { delete mappings.policies; }
				if (!mappings.religions) { delete mappings.religions; }
				if (!mappings.churchAspects) { delete mappings.churchAspects; }
				var output = {id: +this.id, name: this.name, fileMappings: mappings};
				return JSON.stringify(output);
			}
		},
		methods:{
			isValid(): boolean {
				if (!+this.id) { return false }
				if (!this.name || !this.name.length) { return false }
				if (this.sections.policies) {
					if (!this.fileMappings.policies || !this.fileMappings.ideas) {
						return false;
					}
				}
				if (this.sections.ideas) {
					if (!this.fileMappings.ideas) {
						return false;
					}
				}
				if (this.sections.religions) {
					if (!this.fileMappings.religions || !this.fileMappings.churchAspects) {
						return false;
					}
				}
				if (this.sections.countries) {
					if (!this.fileMappings.countries || !this.fileMappings.countryTags || !this.fileMappings.history.countries || !this.fileMappings.cultures || !this.fileMappings.ideas) {
						return false;
					}
				}
				if (!this.sections.countries && !this.sections.religions && !this.sections.ideas && !this.sections.policies) { return false; }
				return true;
			},
			inputHistoryChange(folder, e): void{
				this.fileMappings.history[folder] = e;
				this.valid = this.isValid();
			},
			inputChange(folder, e): void{
				this.fileMappings[folder] = e;
				this.valid = this.isValid();
			},
			show(folder): boolean{
				switch(folder){
					case "countries":
					case "countryTags":
						return this.sections.countries;
					case "ideas":
						return this.sections.countries || this.sections.ideas || this.sections.policies;
					case "cultures":
						return this.sections.countries;
					case "policies":
						return this.sections.policies;
					case "religions":
					case "churchAspects":
						return this.sections.religions;
				}
				return false;
			},
			showHistory(folder): boolean{
				return this.sections.countries;
			},
		},
		watch: {
			sections: {
				handler(): void{
					for(var i = 0; i < this.folders; i++){
						var folder = this.folders[i];
						if(!this.show(folder)){
							this.fileMappings[folder] = null;
						}
					}
					for(var i = 0; i < this.historyFolders; i++){
						var folder = this.historyFolders[i];
						if(!this.showHistory(folder)){
							this.fileMappings.history[folder] = null;
						}
					}
					this.valid = this.isValid();
				},
				deep: true
			},
			id() { this.valid = this.isValid(); },
			name() { this.valid = this.isValid(); },
		}

	});
</script>