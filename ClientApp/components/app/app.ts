import { authConfig, userManagerSettings } from './../../services/auth.config';
import { Vue, Component } from 'vue-property-decorator';
import Oidc from 'oidc-client';
import CloakAuthService from './../../services/CloakAuthService'

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
    authService: any = new CloakAuthService();

    mounted() {

        // console.log(this.authService);
        // this.signin();
        // this.signout();
        // this.test();


        // console.log(x);
        
        // this.signOuts();
        // window.setInterval(() => {
        //     this.processSigninResponse();
        // }, 200)

        // window.setInterval(() => {
        // this.signout();
        // this.getUser();
        // }, 5000)

    }

    

    
    

    signin() {
        var vm = this;

        // this.authService.svc.manager.createSigninRequest().then((user) => {console.log(user)}).catch(error => {console.log("There was an error", error)})

        this.authService.svc.manager.signinRedirect().then(function (req) {
            // window.location.href = req.url;
            console.log(req);

        }).catch(function (err) {
            console.log(err);
        });
    }

    getUser() {
        let self = this
        this.authService.getUser().then(function (user) {

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

    signout() {
        var vm = this;
        this.client.createSignoutRequest({ id_token_hint: this.signinResponse && this.signinResponse.id_token, state: { foo: 5 } }).then(function (req) {
            window.location.href = req.url;
        });
    }

   
}
