<template>
	<div class="countries-page">
			<div class="filters idea-group-filters">
				<div class="bonus-input">
					<label for="idea-bonus-search">Bonus</label>
					<select v-model="search.bonus" class="form-control" id="idea-bonus-search">
						<option :value='null'>None</option>
						<option v-for="bonus in bonuses" :value="bonus.type">{{bonus.typeName}}</option>
					</select>
				</div>
				<div class="category-input">
					<label for="country-bonus-search">Name</label>
					<input id="country-bonus-search" type="text" class="form-control" v-model="search.name" />
				</div>
			</div>
			<div class="idea-groups">
				<div class="idea-group" v-for="country in filteredCountries" v-bind:style="getCountryStyle(country)">
					<div class="row idea-group-top">
						<div class="col-4  d-flex justify-content-start">
							<h4>{{country.name}}</h4>
						</div>
					</div>
					<div class="idea-group-bonuses">
						<div class="idea-group-bonus" v-for="idea in country.ideas" v-bind:style="">
							<div style="height: 100%;" class="d-flex flex-row">
								<div style="width: 100%;">
									<div class="d-flex flex-column bonus-detail">
										<p style="align-self:center;">{{idea.name}}</p>
										<div class="d-flex align-items-center flex-row" v-for="bonus in idea.bonuses" :class="{active: search.bonus === bonus.type}"><img :src="baseUrl + bonus.url" class="bonus-image" />{{bonus.typeName}}: {{bonus.displayValue}}</div>
									</div>
								</div>
								<!-- <div class="d-flex justify-content-center align-items-center idea-num" style="width: 10%;">
									<p class="sideways">{{idea.name}}</h6>
								</div> -->
							</div>


						</div>
					</div>

				</div>
			</div>
		</div>
</template>

<script lang="ts">
	import Vue from 'vue';

	export default Vue.extend({
		props: {
			countries: Array,
			bonuses: Array
		},
		data() {
			return {
				baseUrl: window.location.origin + '/',
				search: {
					bonus: null,
					name: ''
				}
			};
		},
		methods: {
			getCountryStyle(country) {
				if (!country.colors || !country.colors.length) {
					return null;
				}
				var style = {
					"background-color": country.colors[0].rgb,
				};
				return style;
			},
		},
		computed: {
			filteredCountries: function () : any[]{
				var searchName = this.search.name;
				var searchBonus = this.search.bonus;
				var countries = <any[]>this.countries;
				if (searchBonus !== null) {
					countries = countries.filter(x => x.ideas.some(y => y.bonuses.some(z => z.type === searchBonus)));
				}
				if (searchName.length) {
					countries = countries.filter(x => x.name && x.name.toLowerCase().includes(searchName.toLowerCase())
					);
				}
				return countries;
			},
		}
	});
</script>