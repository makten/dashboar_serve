import { authConfig, userManagerSettings } from './auth.config';
import { AUTH0_CONFIG } from './auth.variable';
import { Prop, Component } from 'vue-property-decorator';
import auth0 from 'auth0-js';
import Vue from 'vue';
import * as globals from '.././globals';
var jwtDecode = require('jwt-decode');
import Oidc from 'oidc-client';

export default class CloakAuthService {
    svc: any = {
        manager: null,
        user: null
    }

    constructor() {
        this.activate();
    }

    private activate() {

        Oidc.Log.logger = window.console;
        Oidc.Log.level = Oidc.Log.INFO;

        this.svc.manager = new Oidc.UserManager(userManagerSettings);

        this.svc.manager.events.addUserLoaded(function (userLoaded) {
            alert("sometihng happening")
            this.log('The user has signed in');
            this.svc.user = userLoaded;
        });

        this.svc.manager.events.addUserSignedOut(function () {
            this.log('The user has signed out.');
            this.svc.user = null;
        });

        this.svc.manager.events.addAccessTokenExpiring(function () {
            this.log("Access token expiring...");
        });
        this.svc.manager.events.addSilentRenewError(function (err) {
            this.log("Silent renew error: " + err.message);
        });

    }



    log(data) {
        document.getElementById('response').innerText = '';

        Array.prototype.forEach.call(arguments, function (msg) {
            if (msg instanceof Error) {
                msg = "Error: " + msg.message;
            }
            else if (typeof msg !== 'string') {
                msg = JSON.stringify(msg, null, 2);
            }
            document.getElementById('response').innerHTML += msg + '\r\n';
        });
    }
}