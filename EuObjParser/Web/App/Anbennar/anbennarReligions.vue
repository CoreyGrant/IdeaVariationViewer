<template>
	<div class="countries-page">
		<div class="filters idea-group-filters">
			<!--<div class="bonus-input">
				<label for="idea-bonus-search">Bonus</label>
				<select v-model="search.bonus" class="form-control" id="idea-bonus-search">
					<option :value='null'>None</option>
					<option v-for="bonus in bonuses" :value="bonus.type">{{bonus.typeName}}</option>
				</select>
			</div>
			<div class="category-input">
				<label for="country-bonus-search">Name</label>
				<input id="country-bonus-search" type="text" class="form-control" v-model="search.name" />
			</div>-->
		</div>
		<div class="religions">
			<div class="religion-group" v-for="religionGroup in religions">
				<div class="row religion-group-top">
					<div class="col-4  d-flex justify-content-start">
						<h3>{{religionGroup.displayName}}</h3>
					</div>
				</div>
				<div class="row">
					<div class="col-6 d-flex justify-content-start align-items-center">
						<div><p>Defender of faith</p></div>
						<img v-if="religionGroup.defenderOfFaith" :src="baseUrl + 'Icons/Yes.png'" />
						<img v-if="!religionGroup.defenderOfFaith" :src="baseUrl + 'Icons/No.png'" />
					</div>
					<div class="col-6 d-flex justify-content-end align-items-center">
						<div><p>Can form personal unions</p></div>
						<img v-if="religionGroup.canFormPersonalUnions" :src="baseUrl + 'Icons/Yes.png'" />
						<img v-if="!religionGroup.canFormPersonalUnions" :src="baseUrl + 'Icons/No.png'" />
					</div>
				</div>
				<div class="religions">
					<div style="width: 100%;">
						<div class="religion" v-for="religion in religionGroup.religions">
							<div class="d-flex flex-row justify-content-between">
								<h4>{{religion.displayName}}</h4>
								<div style="width: 20px; height: 20px; border-radius: 10px; border: 1px solid black;" v-bind:style="getReligionStyle(religion)"></div>
							</div>
							<div class="row">
								<div class="col-4" v-if="religion.country && religion.country.length">
									<p>Global modifiers</p>
									<div style="height: 100%;" class="d-flex flex-row">
										<div style="width: 100%;">
											<div class="d-flex flex-column bonus-detail">
												<div class="d-flex align-items-center flex-row" v-for="bonus in religion.country"><img :src="baseUrl + bonus.url" class="bonus-image" />{{bonus.typeName}}: {{bonus.displayValue}}</div>
											</div>
										</div>
									</div>
								</div>
								<div class="col-4" v-if="religion.province && religion.province.length">
									<p>Local modifiers</p>
									<div style="height: 100%;" class="d-flex flex-row">
										<div style="width: 100%;">
											<div class="d-flex flex-column bonus-detail">
												<div class="d-flex align-items-center flex-row" v-for="bonus in religion.province"><img :src="baseUrl + bonus.url" class="bonus-image" />{{bonus.typeName}}: {{bonus.displayValue}}</div>
											</div>
										</div>
									</div>
								</div>
								<div class="col-4" v-if="religion.secondaryCountry && religion.secondaryCountry.length">
									<p>Syncretic modifiers</p>
									<div style="height: 100%;" class="d-flex flex-row">
										<div style="width: 100%;">
											<div class="d-flex flex-column bonus-detail">
												<div class="d-flex align-items-center flex-row" v-for="bonus in religion.secondaryCountry"><img :src="baseUrl + bonus.url" class="bonus-image" />{{bonus.typeName}}: {{bonus.displayValue}}</div>
											</div>
										</div>
									</div>
								</div>
							</div>
							<div class="row" v-if="religion.blessings && religion.blessings.length">
								<div class="col d-flex flex-column">
									<h5>Blessings</h5>
									<div v-for="blessing in religion.blessings">
										<div class="d-flex align-items-center flex-row" v-for="bonus in blessing.bonuses" :class="{active: search.bonus === bonus.type}"><img :src="baseUrl + bonus.url" class="bonus-image" />{{bonus.typeName}}: {{bonus.displayValue}}</div>
									</div>
								</div>
							</div>
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
			religions: Array,
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
			getReligionStyle(religion) {
				if (!religion.color || !religion.color.rgb) {
					return {};
				}
				return {
					"background-color": religion.color.rgb
				};
			},
		},
		computed: {
			
		}
	});
</script>