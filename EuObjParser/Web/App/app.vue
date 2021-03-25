<template>
    <div>
        <div class="tab-row">
            <div class="tab">
                <router-link to="/Anbennar">Anbennar</router-link>
            </div>
            <div class="tab">
                <router-link to="/IdeaVariation">Idea variation</router-link>
            </div>
        </div>
        <router-view></router-view>
    </div>
</template>

<script lang="ts">
    import Vue from 'vue';
    import Anbennar from './anbennar.vue';
    import IdeaVariation from './ideaVariation.vue';

    export default Vue.extend({
        components: {
            Anbennar,
            IdeaVariation
		},
		data() {
			return {};
		}
    });
</script>