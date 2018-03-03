import { AUTH0_CONFIG } from './auth.variable';
import { Prop, Component } from 'vue-property-decorator';
import auth0 from 'auth0-js';
import Vue from 'vue';
import * as globals from '.././globals';
var jwtDecode = require('jwt-decode');
import Spotify from 'spotify-web-api-node';
import VueSpotify from 'vue-spotify';
import axios from 'axios';
Vue.use(VueSpotify, new Spotify())

class SpotifyService {

    code: string = "";
    authrizeUrl: string = "";

    constructor(){
        this.initialize()
    }

    async player(query) {

        const response = await this.performQuery(query);
        return response.data;
    }

    async currentlyPlaying(query) {
        const response = await this.performQuery(query);
        return response.data;
    }

    async spotifyUser(query) {
        const response = await this.performQuery(query)
        return response.data;
    }

    async performQuery(query) {
        const endpoint = `/api/player/auth/${this.code}/${query}`;
        return await axios.get(endpoint);
    }

    async initialize() {

        const spotify = new Spotify({
            redirectUri: globals.redirectUri,
            clientId: globals.clientId
        });

        this.authrizeUrl = spotify.createAuthorizeURL(globals.scopes, globals.state);

        const url = location.href;
        if (url != "" && url != "http://localhost:9000/dashboard/home") {
            this.code = url.split('=')[1].split('&')[0];
            spotify.setAccessToken(this.code)
        }

    }

}

export default new SpotifyService();