import Vue from 'vue';
import Router from 'vue-router';
import Anbennar from './anbennar.vue';
import IdeaVariation from './ideaVariation.vue';
import AnbennarNations from './Anbennar/anbennarNations.vue';
import AnbennarReligions from './Anbennar/anbennarReligions.vue';
import IdeaVariationIdeaGroups from './IdeaVariation/ideaVariationIdeaGroups.vue';
import IdeaVariationPolicies from './IdeaVariation/ideaVariationPolicies.vue';
import IdeaVariationPolicy from './IdeaVariation/ideaVariationPolicy.vue';
import IdeaVariationPlanner from './IdeaVariation/ideaVariationPlanner.vue';
import AnbennarData from '../Data/Anbennar.json';
import IdeaVariationData from '../Data/IdeaVariation.json';

Vue.use(Router);

var router = new Router({
    mode: "history",
    routes: [
        {
            path: "/",
            redirect: "/IdeaVariation/IdeaGroups"
        },
        {
            path: "/Anbennar",
            name: "Anbennar",
            component: Anbennar,
            children: [
                {
                    path: "Countries",
                    component: AnbennarNations,
                    props: {
                        countries: AnbennarData.countries,
                        bonuses: AnbennarData.bonuses
                    }
                },
                {
                    path: "Religions",
                    component: AnbennarReligions,
                    props: {
                        religions: AnbennarData.religionGroups,
                        bonuses: AnbennarData.bonuses
                    }
                }
            ]
        },
        {
            path: "/IdeaVariation",
            name: "IdeaVariation",
            component: IdeaVariation,
            children: [
                {
                    path: "IdeaGroups",
                    component: IdeaVariationIdeaGroups,
                    props: {
                        bonuses: IdeaVariationData.bonuses,
                        ideaGroups: IdeaVariationData.ideaGroups,
                        exclusiveCategories: IdeaVariationData.exclusiveCategories
                    }
                },
                {
                    path: "Planner",
                    component: IdeaVariationPlanner
                },
                {
                    path: "Policies",
                    component: IdeaVariationPolicies,
                    props: {
                        bonuses: IdeaVariationData.bonuses,
                        policies: IdeaVariationData.policies
                    }
                },
                {
                    path: "Policy/:id",
                    component: IdeaVariationPolicy
                }
            ]
        },
    ]
});

export default router;