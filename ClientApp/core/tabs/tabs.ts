import { Component } from 'vue-property-decorator';
import Vue from 'vue';

@Component
export default class TabsComponent extends Vue {




    tabs: any[] = [];
    

    created() {
        this.tabs = this.$children;
        // Listen to currently selected tab from Dashboard.vue
        this.$on('setTab', this.selectTab)
    }

    selectTab(selectTab) {
        // console.log(selectTab.tab, selectTab.ucs)

        this.tabs.forEach(tab => {
            tab.isActive = (tab.href == selectTab);

        });

        // window.location.hash = selectTab;

    }

}