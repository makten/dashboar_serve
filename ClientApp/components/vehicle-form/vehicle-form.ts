import { SaveVehicle, Vehicle, Contact, KeyValuePer } from './../../models/vehicle';
import Vue from 'vue';
import { Component } from 'vue-property-decorator';
// import vSelect from 'vue-select';
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
    private readonly PAGE_SIZE = 4;
    loading: boolean = false;
    queryResult: any = {};
    allVehicles: any[] = [];
    features: Feature[] = [];
    makes: any[] = [];
    models: any[] = [];
    modelsBag: any[] = [];
    name: string = '';
    vehicle: any = {};

    selectedMake: any[] = [];
    selectedModel: any[] = [];
    errorBag: any[] = [];

    vehicleForm: any = new Form({
        makeId: null,
        modelId: null,
        isRegistered: null,
        features: [],
        contact: { name: '', email: '', phone: '' },
        formMode: { button: 'Create', method: 'post' }
    });
    sending: boolean = false;

    query: any = {
        pageSize: this.PAGE_SIZE
    };
    filteredModels: any[] = [];
    columns = [
        { title: 'Id' },
        { title: 'Make', key: 'make', isSortable: true },
        { title: 'Model', key: 'model', isSortable: true },
        { title: 'Contact Name', key: 'contactName', isSortable: true },

    ];

    createEdit: boolean = false;


    mounted() {

        this.loading = !this.loading;
        this.getVehicles(this.query);
        this.getMakes();
        this.getModels();
        this.getFeatures();

    }

    testHub() {
        axios.get('/api/vehicles/test').then(() => {}).catch(() => {});
    }

    //Pagination event handler
    pageOneChanged(pageNum: any) {
        this.query.page = pageNum;
        this.getVehicles(this.query)
    }

    getVehicles(filter: string) {

        fetch('/api/vehicles' + '?' + this.toQueryString(filter))
            .then(response => response.json() as Promise<Vehicle[]>)
            .then(data => {
                this.queryResult = this.allVehicles = data;
                this.loading = false;
            });
    }

    toQueryString(obj: any) {

        let parts = []
        for (let prop in obj) {
            let value: any = obj[prop]

            if (value != null && value != undefined)
                parts.push(encodeURI(prop) + "=" + encodeURI(value))

        }
        return parts.join('&');
    }


    getFeatures() {

        fetch('/api/features')
            .then(response => response.json() as Promise<Feature[]>)
            .then(data => { this.features = data; });
    }

    getMakes() {

        fetch('/api/makes')
            .then(response => response.json() as Promise<Make[]>)
            .then(data => {
                this.makes = data;
            });
    }

    getModels() {

        fetch('/api/models')
            .then(response => response.json() as Promise<Model[]>)
            .then(data => { this.modelsBag = this.filteredModels = data; });
    }

    changeVehicle() {

        let selectedMake = _.find(this.makes, (m) => { return m.id == this.vehicleForm.makeId });
        if (!this.vehicleForm.id)
            this.vehicleForm.modelId = "";
        this.models = selectedMake ? selectedMake.models : [];

    }


    private cascadeDropdown() {
        let selectedMake = _.find(this.makes, (m) => { return m.id == this.query.makeId });
        this.filteredModels = selectedMake ? selectedMake.models : this.modelsBag;
    }


    filterVehicles() {
        //---Client Side Only Filter ---//
        // let vehicles = this.allVehicles;        
        // if(this.filter.makeId)
        //     vehicles = _.filter(vehicles, v =>{ return v.make.id == this.filter.makeId}) 
        // if(this.filter.modelId)
        //     vehicles = _.filter(vehicles, v =>{ return v.model.id == this.filter.modelId})         
        // this.vehicles = vehicles
        this.query.page = 1;
        this.cascadeDropdown()
        this.getVehicles(this.query)


    }

    resetFilter() {

        this.query = {
            page: 1,
            pageSize: this.PAGE_SIZE
        }

        this.changeVehicle();
        this.filterVehicles();
    }

    sortBy(columnName) {

        if (this.query.sortBy === columnName) {
            this.query.isSortAscending = !this.query.isSortAscending;
        }
        else {
            this.query.sortBy = columnName;
            this.query.isSortAscending = true;
        }

        this.getVehicles(this.query)
    }


    validateBeforeSubmit() {

        // this.$validator.validateAll().then(result => {
        //     if (result) {
        //         this.sending = true;
        //         let url = this.vehicleForm.formMode.method === 'put' ? `/api/vehicles/${this.vehicleForm.id}` : `/api/vehicles`

        //         this.vehicleForm[this.vehicleForm.formMode.method](url)
        //             .then(data => {
                        
        //                 this.queryResult.push(data)
                        
        //                 this.sending = false;
        //                 this.$root.$router.push('/vehicle/new');
        //                 this.createEdit = false;
        //             })
        //             .catch(errors => {
        //                 this.sending = false;
        //                 this.errorBag = []
        //                 this.errorBag.push(errors)
        //             });
        //     }

        // });
    }


    setEdit(v) {

        this.createEdit = true;

        this.vehicleForm.id = v.id;
        this.vehicle.makeId = v.make.id;
        this.vehicle.modelId = v.model.id;
        this.vehicle.features = _.map(v.features, 'id')
        this.vehicle.isRegistered = v.isRegistered;
        this.vehicle.contact = v.contact;
        this.vehicle = this.vehicleForm;
        this.vehicleForm.formMode.method = 'put';
        this.vehicleForm.formMode.button = 'Update';
        this.changeVehicle();
    }

    deleteVehicle(vehicleId) {

        const token = localStorage.getItem("id_token");
       

        

        if (token != null)
            if (confirm("Are you sure you want to delete this item?"))

                axios.delete(`/api/vehicles/${vehicleId}`, {
                    headers: { 'Authorization': "Bearer " + token, 'Content-Type': 'application/json' }
                })
                .then(s => {
                    alert(`vehicle with ID ${vehicleId} has been deleted`);
                    
                    this.queryResult.items = _.reject(this.queryResult.items, v => { return v['id'] === vehicleId  })

                })
                .catch(e => {
                    console.log(e)
                });

    }


}
