import { SaveVehicle, Vehicle, Contact, KeyValuePer } from './../../models/vehicle';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as _ from 'lodash';
import axios from 'axios';


//Core components
import Form from '../../core/Form';


interface Feature {
    name: string;
}

interface Make {
    id: number;
    name: string;
}

interface Model {
    id: number;
    name: string;
}


Vue.component('pagination', require('../paginator/paginator.vue.html'));


@Component
export default class VehicleFormComponent extends Vue {
   loading: boolean = false

    mounted() {

        // this.loading = !this.loading;
        
        // axios.get('/api/members').then(response => {
        //     console.log(response);
        // }).catch(err => {})

    }


    // validateBeforeSubmit() {

    //     this.$validator.validateAll().then(result => {
    //         if (result) {
    //             this.sending = true;
    //             let url = this.vehicleForm.formMode.method === 'put' ? `/api/vehicles/${this.vehicleForm.id}` : `/api/vehicles`

    //             this.vehicleForm[this.vehicleForm.formMode.method](url)
    //                 .then(data => {
                        
    //                     this.queryResult.push(data)
                        
    //                     this.sending = false;
    //                     this.$root.$router.push('/vehicle/new');
    //                     this.createEdit = false;
    //                 })
    //                 .catch(errors => {
    //                     this.sending = false;
    //                     this.errorBag = []
    //                     this.errorBag.push(errors)
    //                 });
    //         }

    //     });
    // }


    // setEdit(v) {

    //     this.createEdit = true;

    //     this.vehicleForm.id = v.id;
    //     this.vehicle.makeId = v.make.id;
    //     this.vehicle.modelId = v.model.id;
    //     this.vehicle.features = _.map(v.features, 'id')
    //     this.vehicle.isRegistered = v.isRegistered;
    //     this.vehicle.contact = v.contact;
    //     this.vehicle = this.vehicleForm;
    //     this.vehicleForm.formMode.method = 'put';
    //     this.vehicleForm.formMode.button = 'Update';
    //     this.changeVehicle();
    // }

    // deleteVehicle(vehicleId) {

    //     const token = localStorage.getItem("id_token");
       

        

    //     if (token != null)
    //         if (confirm("Are you sure you want to delete this item?"))

    //             axios.delete(`/api/vehicles/${vehicleId}`, {
    //                 headers: { 'Authorization': "Bearer " + token, 'Content-Type': 'application/json' }
    //             })
    //             .then(s => {
    //                 alert(`vehicle with ID ${vehicleId} has been deleted`);
                    
    //                 this.queryResult.items = _.reject(this.queryResult.items, v => { return v['id'] === vehicleId  })

    //             })
    //             .catch(e => {
    //                 console.log(e)
    //             });

    // }


}
