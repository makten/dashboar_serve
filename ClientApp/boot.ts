import { datetimepicker } from 'jquery-datetimepicker';
import 'bootstrap';
import Vue from 'vue';
import VueRouter from 'vue-router';
import * as globals from './globals';
import vSelect from "vue-select";
import axios from 'axios';

Vue.use(VueRouter);

import moment from 'moment';

moment.updateLocale('nl', {
    calendar: {
        lastDay: '[Yesterday]',
        sameDay: '[Today]',
        nextDay: '[Tomorrow]',
        lastWeek: '[last] dddd',
        nextWeek: 'dddd',
        sameElse: 'L',
    },
});

const routes = [
    { path: '/dashboard', component: require('./components/dashboard/dashboard.vue.html') },
    { path: '/', component: require('./components/dashboard/dashboard.vue.html') },
    { path: '/callback', name: 'auth', component: require('./services/authentication/callback.vue.html') },
    { path: '/counter', component: require('./components/counter/counter.vue.html') },
    { path: '/vehicle/new', component: require('./components/vehicle-form/vehicle-form.vue.html') },
    { path: '/vehicles', component: require('./components/vehicle/vehicle.vue.html') },
    { name: 'vehicle', path: '/vehicles/:vehicleId', component: require('./components/vehicle/vehicle.vue.html') },
    { path: '/user/settings', component: require('./components/user/settings.vue.html') },
];


new Vue({
    el: '#app-root',
    router: new VueRouter({ mode: 'history', routes: routes }),
    
    created(){
    },
    mounted() {
       
        window.setInterval(() => {
            this.getCurrentlyPlaying()
        }, 4000)
        // this.$router.beforeEach((to, from, next) => {

        //     if (to.path === '/vehicle/new') {
        //         if (globals.auth.hasRole('Admin')) {
        //             next();
        //         }
        //         else {
        //             next(from.path);
        //         }

        //     }
        //     else {
        //         next();
        //     }

        // })
    },
    methods: {
        getCurrentlyPlaying() {
            axios.get("api/FetchCurrentTrack").then(r => { }).catch()
        }
    },
    render: (h: any) => h(require('./components/app/app.vue.html'))
});

