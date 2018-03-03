import Vue from 'vue';
import vSelect from 'vue-select';
import * as _ from 'lodash';
import axios from 'axios';
import * as globals from './../globals';

class DashboardService {

    code: string = "";

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


    async performQuery(query) {
        const endpoint = `/api/player/auth/${this.code}/${query}`;
        return await axios.get(endpoint);
    }

    async initialize() {       

    }
}


export default new DashboardService();