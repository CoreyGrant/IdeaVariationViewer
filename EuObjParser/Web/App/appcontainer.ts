import Vue from 'vue';
import IdeaVariation from './ideaVariation.vue';
import Anbennar from './anbennar.vue';
import router from './app-router';

const app = new Vue({
    el: "#app",
    router,
    components: {
        IdeaVariation,
        Anbennar
    },
    data() {
        return {};
    }
});