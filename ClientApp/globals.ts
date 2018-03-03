'use strict';
import Vue from 'vue';
import AuthService from './services/AuthService';

export const eventBroadcaster = new Vue();
export const home = '/';
// export const auth = new AuthService();



const  authObj = new AuthService();


export const  { login, logout, authenticated, authNotifier } = authObj;

export const auth = authObj;

export const scopes = [
    'user-read-private', 'user-read-email',
    'user-read-currently-playing', 'user-read-playback-state',
    'user-modify-playback-state'
],
    redirectUri = 'http://localhost:9000/dashboard/home',
    clientId = '1b577309cb494275ada4fe2f3ebce5dc',
    state = '';