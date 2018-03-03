import { Component, Prop } from 'vue-property-decorator';
import Vue from 'vue';
import axios from 'axios';
import * as globals from '../../globals';
import SpotifyApi from 'spotify-web-api-node';
import VueSpotify from 'vue-spotify';
Vue.use(VueSpotify, new SpotifyApi())
import Spotify from './../../services/spotify-service';

interface SpotifyUser { }
interface Player { }
interface CurrentlyPlaying { }

@Component({
})

export default class SpotifyComponent extends Vue {
    spotifyUser: any = {};
    player: any = {};
    currentlyPlaying: any = {};
    authURL: string = "";

    mounted() {


        this.authURL = Spotify.authrizeUrl
        // if(this.authURL != null){
        this.fetchSpotifyUser();
        this.fetchPlayer();
        this.fetchCurrentlyPlaying();
        // }

    }

    async fetchSpotifyUser() {
        const query = `spotifyuser`;
        this.spotifyUser = await Spotify.spotifyUser(query);
    }

    async fetchPlayer() {
        const query = `player`;
        this.player = await Spotify.player(query);
    }

    async fetchCurrentlyPlaying() {
        const query = `currentlyplaying`;
        this.currentlyPlaying = await Spotify.currentlyPlaying(query);
    }

}