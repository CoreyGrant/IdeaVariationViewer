<template>
	<img :src="fullUrl" :class="className" @click="$emit('click')"/>
</template>

<script lang="ts">
    import Vue from 'vue';
	import {getBonusUrl} from '../Helpers/getBonusUrl';

    export default Vue.extend({
		props:{
			url: String,
			name: String,
			bonus: String,
			className: Object
		},
		data() {
			return {
				baseUrl: window.location.origin + '/',
			};
		},
		methods:{
			mapName(): string{
				switch(this.name.toLowerCase()){
					case "adm": return "Icons/Administrative_power.png";
					case "dip": return "Icons/Diplomatic_power.png";
					case "mil": return "Icons/Military_power.png";
					case "yes": return "Icons/Yes.png";
					case "no": return "Icons/No.png";
					default: throw new Error(this.name);
				}
			},
			mapBonus(): string{
				return "Icons/Bonuses/" + this.bonus.toLowerCase() + ".png";
			}
		},
		computed:{
			fullUrl(): string{
				return this.baseUrl + (this.name 
					? this.mapName() 
					: (this.bonus ? this.mapBonus() : this.url));
			}
		}
    });
</script>