import { OidcClientSettings, UserManagerSettings } from 'oidc-client';
import Oidc from "oidc-client";

export const authConfig: OidcClientSettings = {

    // Url of the Identity Provider
    authority: 'http://localhost:8080/auth/realms/master',

    // URL of the SPA to redirect the user to after login
    redirect_uri: "http://localhost:9000",

    // The SPA's id. The SPA is registerd with this id at the auth-server
    client_id: 'team-dashboard',

    // set the scope for the permissions the client should request
    // The first three are defined by OIDC. The 4th is a usecase-specific one
    scope: 'openid profile email',
    response_type: 'id_token token',

}

export const userManagerSettings = {
    userStore: new Oidc.WebStorageStateStore({store: window.localStorage}),
    authority: 'http://localhost:8080/auth/realms/master',
    client_id: 'team-dashboard',
    redirect_uri: 'http://localhost:9000',
    response_type: 'id_token token',
    scope: 'openid profile all_claims',
    post_logout_redirect_uri: 'http://localhost:9000',
    loadUserInfo: false
}