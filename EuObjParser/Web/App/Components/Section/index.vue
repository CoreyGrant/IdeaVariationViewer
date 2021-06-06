<template>
	<div class="section-index">
		<ul>
			<li v-for="section in sections"><router-link :to="'/Mod/' + modId + '/' + section.name">{{section.displayName}}</router-link></li>
		</ul>
	</div>
</template>

<script lang="ts">
	import Vue from 'vue';

	export default Vue.extend({
		components: {
		},
		props:{
			sections: Array,
			modId: Number
		},
		data() {
			return {
			};
		},

	});
</script>
