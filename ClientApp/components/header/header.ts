import { eventBroadcaster } from './../../globals';
import { auth0 } from 'auth0-js';
import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import * as _ from 'lodash';
import * as $ from 'jquery';
import * as globals from '../../globals';
import Helpers from '../../core/Helpers';




@Component
export default class HeaderComponent extends Vue {

  @Prop({})
  auth: any;

  @Prop({})
  authenticated: boolean;

  userProfile: any = {};

  themes: any[] = [
    { color: '#35475e', name: 'Default', text: '#E3F2FD'},
    { color: '#5E35B1', name: 'Purple', text: '#E1F5FE'},
    { color: '#00695C', name: 'Green', text: '#E1F5FE'},
    { color: '#E53935', name: 'Red', text: '#E1F5FE'},
    { color: '#1E88E5', name: 'Blue', text: '#E1F5FE'},
    { color: '#BDBDBD', name: 'Light Grey', text: '#424242'}
  ]
  
  themeColor: any = {color:'#35475e', name:'Default',  text: "#E3F2FD"};

  mounted() {

    //Testing textSummarizer...
    let v = "Hello world of people and programmers who are busy learning and really really really";
    console.log(Helpers.textSummerizer(v, 60));
    console.log(Helpers.passwordGenerator());
    
    let profile = localStorage.getItem('profile');
    if (typeof profile !== 'undefined' && profile !== null)
      this.userProfile = JSON.parse(profile);

  }

  setTheme(theme)
  {
    this.themeColor = theme;    
    globals.eventBroadcaster.$emit('themeChanged', theme)
  }



}



