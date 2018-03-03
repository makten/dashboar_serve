import { Component, Prop } from 'vue-property-decorator';
import Vue from 'vue';
import axios from 'axios';
import * as globals from '../../globals';



@Component({
    components: {
        HeaderComponent: require('../header/header.vue.html'),
        SidebarComponent: require('../sidebar/sidebar.vue.html'),
        FooterComponent: require('../footer/footer.vue.html'),
        DashboardComponent: require('../dashboard/dashboard.vue.html'),
        // HomeComponent: require('../home/home.vue.html'),

    }
})

export default class MainDashboardComponent extends Vue {
    authenticated: boolean = globals.authenticated;
    auth: any = globals.auth;
    login: any = globals.login;
    logout: any = globals.logout;
    fullScreen: boolean = false;



    mounted() {        
        
        globals.eventBroadcaster.$on('changeRoute', (routeLink: any) => {
            this.$root.$router.push(routeLink)
        })

        globals.eventBroadcaster.$on('authChange', (authState: any) => {
            this.authenticated = authState.authenticated
        })
    }
}