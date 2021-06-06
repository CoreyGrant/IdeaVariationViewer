import Vue from 'vue';
import AppHeader from './Components/appHeader.vue';
import appRouter from './app-router';
import { getBonusName, displayBonusValue } from './Helpers/getBonusUrl';
import '../Styles/eu4modviewer.scss';

Vue.filter('bonusName', function(value){
    return getBonusName(value);
});

Vue.filter('bonusValue', function(value, bonusName){
    return displayBonusValue(value, bonusName);
});

Vue.filter('religionName', function(value){
    return value.split('_').map(capitalise).join(' ')
});

function capitalise(s) {
    return s.substring(0,1).toUpperCase() + s.substring(1).toLowerCase();
}

const app = new Vue({
    el: "#app",
    router: appRouter.router,
    components: {
        AppHeader
    },
    data() {
        return {};
    }
});