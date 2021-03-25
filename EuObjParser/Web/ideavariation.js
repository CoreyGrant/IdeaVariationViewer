(async function(){
    var data = await fetch("Data/IdeaVariation.json").then(x => x.json());
    var anbennarData = await fetch("Data/Anbennar.json").then(x => x.json());

    var searchIdeas = data.ideaGroups.map(x => x.type).reduce((prev, cur) =>
        Object.assign({[cur]: false}, prev),
        {});
    window.data = data;
    var app = new Vue({
        el: '#app',
        data: {
            app: 'anbennar',
            tab: 'countries-page',
            ideas: data.ideaGroups,
            policies: data.policies,
            bonuses: data.bonuses.sort((a,b) => a.typeName > b.typeName),
            exclusiveCategories: data.exclusiveCategories,
            selectedPolicy: {},
            search: {
                ideaBonus: null,
                ideaExclusiveCategory: null,
                policyBonus: null,
                ideas: searchIdeas,
                ideaMonarch: null,
                policyMonarch: null,
            },
            navigationBuffer: [],
            planner: {
                selectedIdeaGroup: '',
                selectedPolicy: '',
                ideaGroups: [],
                policies: []
            },
            anbennar:{
                countries: anbennarData.countries.sort((a,b) => a.name > b.name),
                bonuses: anbennarData.bonuses.sort((a,b) => a.typeName > b.typeName),
                search: {
                    countryName: '',
                    countryBonus: null
                }
            }
        },
        methods:{
            getCountryStyle(country){
                if(!country.colors){
                    return null;
                }
                var style = {
                    "background-color": country.colors[0].rgb,
                };
                return style;
            },
            getAge(allow){
                return allow.current_age ? `[${allow.current_age}]` : '';
            },
            getImageLink(item){
                return window.imageMap(item);
            },
            getTitleForPlannerPolicy(policyAllow){
                var policy = this.policies.find(x => x.displayAllow == policyAllow);
                return policy.bonuses.map(x => x.name + ": " + this.displayBonusVal(x.name, x.bonus)).join('\n');
            },
            displayBonusVal(bonusName, bonusVal){
                bonusVal = bonusVal.toString();
                if(!bonusName || !bonusVal){return '';}
                if(bonusVal.indexOf("/") > -1){
                    return bonusVal.split("/").map(window.formatDisplay.bind(window, bonusName))
                        .join("/");
                }
                return window.formatDisplay(bonusName, bonusVal);
            },
            toggleIdeaMonarch(val){
                this.search.ideaMonarch = this.search.ideaMonarch === val
                    ? null
                    : val;
            },
            togglePolicyMonarch(val){
                this.search.policyMonarch = this.search.policyMonarch === val
                    ? null
                    : val;
            },
            navigateToTab(tab){
                this.tab = tab;
                var app = this.app;
                this.navigationBuffer.push({tab, app});
            },
            navigateToApp(app, tab){
                this.tab = tab;
                this.app = app;
                this.navigationBuffer.push({tab, app});
            },
            navigateToPolicy(policy){
                this.selectedPolicy = policy;
                this.tab = "policy";
                var app = this.app;
                this.navigationBuffer.push({tab: this.tab, selectedPolicy: policy, app});
            },
            navigateBack(){
                var current = this.navigationBuffer.pop();
                var last = this.navigationBuffer[this.navigationBuffer.length - 1];
                if(this.tab == 'policy'){
                    this.selectedPolicy = last.selectedPolicy;
                }
                this.tab = last.tab;
                this.app = last.app;
            },
            policyByName(policyName){
                return this.policies.find(x => x.name === policyName);
            },
            plannerAddSelectedIdea(){
                if(!this.planner.selectedIdeaGroup){
                    return;
                }
                this.planner.ideaGroups.push(this.planner.selectedIdeaGroup.type);
                this.planner.selectedIdeaGroup = null;
            },
            plannerAddSelectedPolicy(){
                if(!this.planner.selectedPolicy){
                    return;
                }
                this.planner.policies.push(this.planner.selectedPolicy.displayName);
                this.planner.selectedPolicy = null;
            },
            filteredPlannerIdeas(monarch){
                var ideaGroups = this.ideas.filter(x => this.planner.ideaGroups.indexOf(x.type) > -1);
                return ideaGroups.filter(x => x.category === monarch).map(x => x.name);
            },
            removePlannerIdeaGroup(idea){
                this.planner.ideaGroups = this.planner.ideaGroups.filter(x => x !== idea);
                
                var plannerIdeaGroups = this.planner.ideaGroups;
                function getAllows(allow){
                    var key = 'full_idea_group';
                    return Object.getOwnPropertyNames(allow).filter(x => x.startsWith(key)).map(x => allow[x]);
                }
                var mappedIdeaGroups = plannerIdeaGroups
                    .map(x => window.getIdeaGroupExclusiveCategory(x) == "General" 
                        ? x 
                        : '[' + window.getIdeaGroupExclusiveCategory(x) + ']');
                
                this.planner.policies = this.policies.filter(x => {
                    if(!this.planner.policies.includes(x.displayAllow)){
                        return false;
                    }
                    var allows = getAllows(x.allow);
                    var hasAllIdeasNeeded = allows.every(y => mappedIdeaGroups.indexOf(y) > -1);
                    return hasAllIdeasNeeded;
                }).map(x => x.displayAllow);
            },
            filteredPlannerPolicies(monarch){
                var ideaGroups = this.policies.filter(x => this.planner.policies.indexOf(x.displayName) > -1);
                return ideaGroups.filter(x => x.monarchPower === monarch).map(x => x.displayName);
            },
            removePlannerPolicy(policy){
                this.planner.policies = this.planner.policies.filter(x => x !== policy);
            }
        },
        computed:{
            filteredCountries: function(){
                var searchName = this.anbennar.search.countryName;
                var searchBonus = this.anbennar.search.countryBonus;
                var countries = this.anbennar.countries;
                if(searchBonus !== null){
                    countries = countries.filter(x => x.ideas.some(y => y.bonuses.some(z => z.type === searchBonus)));
                }
                if(searchName.length){
                    countries = countries.filter(x =>  x.name && x.name.toLowerCase().includes(searchName.toLowerCase())
                    );
                }
                return countries;
            },
            filteredIdeas: function(){
                var activeBonusFilter = this.search.ideaBonus;
                var activeMonarchFilters = this.search
                    .ideaMonarch;
                var activeIdeaExclusiveCategory = this.search.ideaExclusiveCategory;
                var ideas = this.ideas;
                if(activeBonusFilter){
                    ideas = 
                        ideas.filter(idea =>{
                            var bonusTypes = idea.ideas.map(x => x.bonuses.map(y => y.type)).flat();
                            return bonusTypes.indexOf(activeBonusFilter) > -1;
                        })
                }
                if(activeIdeaExclusiveCategory !== null){
                    ideas = 
                        ideas.filter(x => x.exclusiveCategory === activeIdeaExclusiveCategory);
                        
                }
                if(activeMonarchFilters !== null){
                    ideas = ideas.filter(x => x.category == activeMonarchFilters);
                }
                return ideas.sort((a, b) => a.name > b.name );
            },
            filteredPolicies: function(){
                var activeBonusFilter = this.search.policyBonus;
                var activeMonarchFilters = this.search
                    .policyMonarch;
                var activeIdeaGroups = this.ideas.map(x => x.name)
                    .filter(x => this.search.ideas[x]);
                var policies = this.policies;
                if(activeBonusFilter !== null){
                    policies = 
                        policies.filter(x => x.bonuses.some(x => x.type == activeBonusFilter));
                      
                }
                if(activeMonarchFilters !== null){
                    policies = policies.filter(x => x.monarchPower == activeMonarchFilters);
                }
                return policies;
            },
            plannerAvailableIdeas: function(){
                var ideaCategories = this.planner.ideaGroups
                    .map(window.getIdeaGroupExclusiveCategory)
                    .filter(x => x !== "General");
                var output = this.ideas.filter(x => x.exclusiveCategory == "General"
                    ? this.planner.ideaGroups.indexOf(x.name) === -1
                    : ideaCategories.indexOf(x.exclusiveCategory) === -1);

                return output;
            },
            plannerAvailablePolicies: function(){
                var plannerIdeaGroups = this.planner.ideaGroups;
                function getAllows(allow){
                    var key = 'full_idea_group';
                    return Object.getOwnPropertyNames(allow).filter(x => x.startsWith(key)).map(x => allow[x]);
                }
                var mappedIdeaGroups = plannerIdeaGroups
                    .map(x => window.getIdeaGroupExclusiveCategory(x) == "General" 
                        ? x 
                        : '[' + window.getIdeaGroupExclusiveCategory(x) + ']');
                
                return this.policies.filter(x => {
                    var allows = getAllows(x.allow);
                    var isAlreadySelected = this.planner.policies.indexOf(x.displayAllow) > -1;
                    if(isAlreadySelected){
                        return false;
                    }
                    var hasAllIdeasNeeded = allows.every(y => mappedIdeaGroups.indexOf(y) > -1);
                    return hasAllIdeasNeeded;
                });
            },
            plannerBonuses: function(){
                var ideaGroups = this.ideas.filter(x => this.planner.ideaGroups.indexOf(x.name) > -1);
                var policies = this.policies.filter(x => this.planner.policies.indexOf(x.displayAllow) > -1);
                var ideaBonuses = ideaGroups.map(
                    x => x.bonuses.map(
                        y => Object.getOwnPropertyNames(y.bonus).filter(z => z !== '__ob__' && z !== '_order').map(z => 
                            {
                                return {
                                    name: z,
                                    bonus: y.bonus[z]
                                };
                            })).flat()).flat();
                var policyBonuses = policies.map(
                    x => x.bonuses
                ).flat();
                var allBonuses = ideaBonuses.concat(policyBonuses);
                var collapsedBonuses = allBonuses.reduce(
                    (prev, cur) => {
                        var prevItem = prev.find(x => x.name === cur.name);
                        if(prevItem){
                            prevItem.bonus = +prevItem.bonus + +cur.bonus;
                        } else {
                            return [...prev, cur];
                        }
                        return prev;
                    },
                    []
                )
                return collapsedBonuses.sort((x, y) => x.name > y.name);
            }
        }
    })
}());