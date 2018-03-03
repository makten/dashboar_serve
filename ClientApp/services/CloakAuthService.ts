import { authConfig } from './auth.config';
import { AUTH0_CONFIG } from './auth.variable';
import { Prop, Component } from 'vue-property-decorator';
import auth0 from 'auth0-js';
import Vue from 'vue';
import * as globals from '.././globals';
var jwtDecode = require('jwt-decode');
import Oidc  from 'oidc-client';




export default class CloakAuthService extends Vue {

    private roles: string[] = [];
    authenticated: any = this.isAuthenticated()
    authNotifier: any = globals.eventBroadcaster;
    userProfile: any = {};
    expiration: string = '';
    signinResponse: any;    

    auth0: any = new auth0.WebAuth({
        domain: AUTH0_CONFIG.domain,
        clientID: AUTH0_CONFIG.clientID,
        redirectUri: AUTH0_CONFIG.redirectUri,
        responseType: AUTH0_CONFIG.responseType,
        scope: AUTH0_CONFIG.scope
    });

    client: any = new Oidc.OidcClient(authConfig);


    constructor() {
        super();

        this.signin = this.signin.bind(this);
        this.setSession = this.setSession.bind(this);
        this.readRolesFromStorage();
        this.logout = this.logout.bind(this);
        this.isAuthenticated = this.isAuthenticated.bind(this);
    }


 signin() {
        var vm = this;
        this.client.createSigninRequest({ state: { bar: 15 } }).then(function(req) {
            console.log("signin request", req, "<a href='" + req.url + "'>go signin</a>");
            // if (vm.followLinks()) {
                window.location.href = req.url;
            // }
        }).catch(function(err) {
            console.log(err);
        });
    }

    signout() {
        var vm = this;
        this.client.createSignoutRequest({ id_token_hint: this.signinResponse && this.signinResponse.id_token, state: { foo: 5 } }).then(function(req) {
            console.log("signout request", req, "<a href='" + req.url + "'>go signout</a>");
            // if (vm.followLinks()) {
                window.location.href = req.url;
            // }
        });
    }

    processSigninResponse() {
        this.client.processSigninResponse().then(function(response) {
            this.signinResponse = response;
            console.log("signin response", this.signinResponse);
        }).catch(function(err) {
            console.log(err);
        });
    }

    processSignoutResponse() {
        this.client.processSignoutResponse().then(function(response) {
            this.signinResponse = null;
            console.log("signout response", response);
        }).catch(function(err) {
            console.log(err);
        });
    }    

    followLinks() {
        return localStorage.getItem("follow") === "true";
    }




































   
    login() {
        this.auth0.authorize();
    }

    handleAuthentication() {

        let vm = this;
        this.auth0.parseHash((err: any, authResult: any) => {

            if (authResult && authResult.accessToken && authResult.idToken) {
                this.auth0.client.userInfo(authResult.accessToken, function (err: any, user: any) {
                    localStorage.setItem('profile', JSON.stringify(user))
                });

                this.setSession(authResult)
                this.authNotifier.$emit('changeRoute', '/vehicle/new')

            } else if (err) {
                console.log(err)
            }
        })
    }

    setSession(authResult: any) {

        // Set the time that the access token will expire at
        let expiresAt = JSON.stringify(authResult.expiresIn * 1000 + new Date().getTime())
        localStorage.setItem('access_token', authResult.accessToken)
        localStorage.setItem('id_token', authResult.idToken)
        localStorage.setItem('expires_at', expiresAt)

        this.authNotifier.$emit('authChange', { authenticated: true })
        window.location.reload();

    }

    logout() {
        // Clear access token and ID token from local storage
        localStorage.removeItem('access_token')
        localStorage.removeItem('id_token')
        localStorage.removeItem('expires_at')
        localStorage.removeItem('profile');
        this.userProfile = null
        this.roles = [];

        // navigate to the home route        
        this.authNotifier.$emit('changeRoute', '/home')
        this.authNotifier.$emit('authChange', false)

        //Temporal solution for page freeze
        window.location.reload();

    }

    private readRolesFromStorage() {

        let token = localStorage.getItem('id_token');
        if (token) {
            let decodedToken = jwtDecode(token);
            console.log(decodedToken)
            if (decodedToken['https://dashapp.com/roles'])
                this.roles = decodedToken['https://dashapp.com/roles'];
        }
    }

    public hasRole(roleName) {
        return this.roles.indexOf(roleName) > -1;
    }

    isAuthenticated() {
        // Check whether the current time is past the
        // access token's expiry time        
        let expiresAt = JSON.parse(localStorage.getItem('expires_at'))
        return new Date().getTime() < expiresAt
    }
}