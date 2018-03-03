import { Component, Prop } from 'vue-property-decorator';
import Vue from 'vue';


@Component
export default class CallbackComponent extends Vue {

    @Prop({})
    auth: any;


    mounted() {
        
        this.auth.handleAuthentication()
    }


}