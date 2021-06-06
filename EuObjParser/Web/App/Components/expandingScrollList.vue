<template>
	<div class="scroll-container">
		<slot></slot>
	</div>
</template>

<script lang="ts">
	import Vue from 'vue';

	export default Vue.extend({
		components: {
		},
		props: {
		},
		data() {
			return {
			};
		},
		mounted() {
			this.$nextTick(() => {
				var scrollContainer = window.document.getElementsByClassName('scroll-container')[0];
				scrollContainer.addEventListener('scroll', (e) => {
					var scrollHeight = scrollContainer.scrollHeight;
					var scrollTop = scrollContainer.scrollTop;
					var offsetHeight = (<any>scrollContainer).offsetHeight;
					if (scrollHeight * 0.85 < (scrollTop + offsetHeight)) {
						this.$emit('load-more');
					}
				});
			})
		},
		methods: {
		},
		computed: {
		},
		watch: {
		}
	});
</script>
