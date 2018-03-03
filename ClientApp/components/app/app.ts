import { authConfig, userManagerSettings } from './../../services/auth.config';
import { Vue, Component } from 'vue-property-decorator';
import Oidc from 'oidc-client';
import { grad, linspace, plot } from "propel";


@Component({
    components: {

        HomeComponent: require('../home/home.vue.html'),
        DashboardComponent: require('../dashboard/dashboard.vue.html'),
    }
})
export default class AppComponent extends Vue {
    signinResponse: any;

    client: any = new Oidc.OidcClient(authConfig);
    mng: any = new Oidc.UserManager(userManagerSettings);
    user: any = null;

    mounted() {

        // this.signOuts();
        this.test();
        
        // this.signOuts();
        // window.setInterval(() => {
        //     this.processSigninResponse();
        // }, 200)

        // window.setInterval(() => {
        // this.signout();
        // this.getUser();
        // }, 5000)

    }

    

    test() {
        this.signin();
        // this.mng.signinRedirect().catch(err => { console.log(err) })
    }

    getUser() {
        let self = this
        this.mng.getUser().then(function (user) {

            if (user == null) {
                // self.test()
            } else {
                self.user = user
                console.log(self.user)
                // self.signedIn = true
            }
        }).catch(function (err) {
            console.log(err)
        });
    }

    signOuts() {
        var self = this;
        this.mng.signoutRedirect().then(function (resp) {
            //   self.signedIn = false
            console.log("signed out", resp);
        }).catch(function (err) {
            console.log(err)
        })
    }

    signin() {
        var vm = this;

        this.client.createSigninRequest({ state: { bar: 15 } }).then(function (req) {

            // window.location.href = req.url;
            vm.client.signinRedirectCallback(req.url).then(function (loggedUser) { 
                ///// the logged user contains the user info
                console.log(loggedUser);
             });
            

            // if (window.location.href.indexOf("#") >= 0) {
            //     vm.processSigninResponse();
            // }
            // else if (window.location.href.indexOf("?") >= 0) {
            //     vm.processSignoutResponse();
            // }

        }).catch(function (err) {
            console.log(err);
        });
    }

    signout() {
        var vm = this;
        this.client.createSignoutRequest({ id_token_hint: this.signinResponse && this.signinResponse.id_token, state: { foo: 5 } }).then(function (req) {
            window.location.href = req.url;
        });
    }

    processSigninResponse() {
        this.client.processSigninResponse().then(function (response) {
            this.signinResponse = response;
            console.log("signin response", this.signinResponse);
        }).catch(function (err) {
            console.log(err);
        });
    }

    processSignoutResponse() {
        var vm = this;
        this.client.processSignoutResponse().then(function (response) {
            vm.signinResponse = null;
        }).catch(function (err) {
            console.log(err);
        });
    }

}
