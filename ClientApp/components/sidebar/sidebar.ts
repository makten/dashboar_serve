import Vue from 'vue';
import { Component, Watch } from 'vue-property-decorator';
import vSelect from 'vue-select';
import * as _ from 'lodash';
import * as globals from '../../globals';


@Component
export default class DashboardComponent extends Vue {

  auth: any = globals.auth;
  userProfile: any = {};
  linkList: any[] = [
    { icon: '<i class="material-icons md-24" aria-hidden="true">dashboard</i>', title: 'Dashboard', link: '/', active: true },
    { icon: '<i class="material-icons md-24" aria-hidden="true">note_add</i>', title: 'Create Page', link: '/vehicle/new', active: false },
  ];

  forAdmin: any[] = [
    { icon: '<i class="material-icons md-24" aria-hidden="true">business</i>', title: 'Departments', link: '/#', active: false },
    { icon: '<i class="material-icons md-24" aria-hidden="true">people</i>', title: 'Employees', link: '/buildModel', active: false },
    { icon: '<i class="material-icons md-24" aria-hidden="true">security</i>', title: 'Roles ', link: '/#', active: false }
  ];

  themeColor: any = {color:'#35475e', name:'Default',  text: "#E3F2FD"};

  logo: string = 'Eile Mit Weile';

  windowHeight: number = 0;


  mounted() {
    this.$nextTick(function () {      
      
      // window.addEventListener('resize', this.resizeSidebar);
      let profile = localStorage.getItem('profile');
      if (typeof profile !== 'undefined' && profile !== null)
        this.userProfile = JSON.parse(profile);

      this.changeActive(window.location.pathname);

      globals.eventBroadcaster.$on('themeChanged', (theme) => { this.themeColor = theme; } )


    })
  }

  
  changeActive(title) {

    this.linkList = _.map(this.linkList, link => {

      if (link.link == title) {

        link.active = true;
      }
      else {
        link.active = false;
      }     

      return link;

    });
  }


  get sidebarSize() {
    return this.$root.$el.offsetHeight;
  }

  @Watch('sideSize')
  onSidebarSize(sidebarZise, oldval) { }

}

