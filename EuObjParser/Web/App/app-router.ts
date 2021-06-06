import Vue from 'vue';
import Router from 'vue-router';
import { getFile } from './appData';
import GenericModComponent from './genericModComponent.vue';
import ConfigureMod from './configureMod.vue';
import Countries from './Components/Section/countries.vue';
import IdeaGroups from './Components/Section/ideaGroups.vue';
import SectionIndex from './Components/Section/index.vue';
import Policies from './Components/Section/policies.vue';
import GreatProjects from './Components/Section/greatProjects.vue';
import ReligionGroups from './Components/Section/religionGroups.vue';
import ModsData from '../Data/mods.json';
import Index from './index.vue';
import About from './about.vue';

Vue.use(Router);
var modRoutes = ModsData.mods.map(mod => {
    return {
        path: "/Mod/" + mod.id.toString(),
        name: mod.name,
        component: GenericModComponent,
        props:{ name: mod.name, id: mod.id, sections: mod.sections },
        children: [
            {
                path: "Index",
                component: SectionIndex,
                props:{modId: mod.id, sections: mod.sections},
            },
            ...mod.sections.map(section => {
                var component: any = null;
                if(section.name == 'countries'){
                    component = Countries;
                } else
                if(section.name == 'ideaGroups'){
                    component = IdeaGroups;
                } else
                if(section.name == 'policies'){
                    component = Policies;
                } else
                if(section.name == 'religionGroups'){
                    component = ReligionGroups;
                            }
                if (section.name == 'greatProjects') {
                    component = GreatProjects;
                }
                return {
                    path: section.name,
                    component,
                    props:{id: mod.id}
                };
            })
        ],
    }
});
var routes = [
    {
        path: "/",
        redirect: "/Index"
    },
    {
        path: "/Index",
        component: Index,
        props:{mods: ModsData.mods}
    },
    {
        path: "/About",
        component: About,
    },
    {
        path: "/ConfigureMod",
        component: ConfigureMod,
    },
    ...modRoutes
];

var router = new Router({
    mode: 'hash',
    routes: routes,
});

export default {
    router,
    routes
};