import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import * as _ from 'lodash';
import axios from 'axios';

//Core components
import Form from '../../core/Form';

interface Vehicle {
    id: number;
    make: any;
    feature: any;
    isRegistered: boolean;
    contact: any;
    lastUpdate: string;

}

Vue.component('tabs', require('../../core/tabs/tabs.vue.html'));
Vue.component('tab', require('../../core/tabs/tab.vue.html'));
Vue.component('modal', require('../../core/modals/modal.vue.html'));

@Component
export default class VehicleComponent extends Vue {
    vehicle: any = {};
    vehicleEdit: any = {};
    vehicleId: number;
    showDetails: boolean = false;
    photos: any[] = [];
    uploadProgress: number = null;
    openEditModal: boolean = false;

    //Vehicle form
    vehicleForm: any = new Form({
        makeId: null,
        modelId: null,
        isRegistered: null,
        features: [],
        contact: { name: '', email: '', phone: '' },
        formMode: { button: 'Create', method: 'post' }
    });


    mounted() {

        this.showDetails = false;
        this.getVehicle(this.$route.params.vehicleId);
        if (this.vehicleId != null || this.vehicleId != undefined)
            this.getPhotos()
    }



    getVehicle(vehicleId) {
        this.vehicleId = vehicleId;
        fetch(`/api/vehicles/${vehicleId}`)
            .then(response => response.json() as Promise<Vehicle[]>)
            .then(data => {
                this.vehicle = data;
                this.showDetails = !this.showDetails;
            });
    }


    getPhotos() {

        axios.get(`/api/vehicles/${this.vehicleId}/photos`)
            .then(response => {
                this.photos = response.data;
            })
            .catch(error => { })
    }


    processFile(e) {
        let files = e.target.files || e.dataTransfer.files;
        if (!files.length)
            return;

        let formData = new FormData();
        formData.append("file", files[0]);

        let vm = this;
        var config = {
            onUploadProgress: function (progressEvent) {
                var percentCompleted = Math.round((progressEvent.loaded * 100) / progressEvent.total);
                vm.uploadProgress = percentCompleted;
            }
        };

        axios.post(`/api/vehicles/${this.vehicleId}/photos`, formData, config)
            .then(photo => {
                this.photos.push(photo.data)
                this.uploadProgress = null;
                e.target.value = '';
            })
            .catch(error => {
                alert("File too big or invalid file type!");
            })

    }

    editVehicle(v) {

        this.vehicleForm.id = v.id;
        this.vehicleEdit.makeId = v.make.id;
        this.vehicleEdit.modelId = v.model.id;
        this.vehicleEdit.features = _.map(v.features, 'id')
        this.vehicleEdit.isRegistered = v.isRegistered;
        this.vehicleEdit.contact = v.contact;
        this.vehicleEdit = this.vehicleForm;
        this.vehicleForm.formMode.method = 'put';
        this.vehicleForm.formMode.button = 'Update';
        // this.changeVehicle();
        this.openEditModal = true;

    }

    deleteVehicle(vehicleId) {
        alert(vehicleId);
        if (confirm("Are you sure you want to delete this item?"))
            this.vehicleForm.delete(`/api/vehicles/${vehicleId}`).then(s => {
                alert(`vehicle with ID ${vehicleId} has been deleted`);

                this.$root.$router.push('/vehicles/new');

                // this.queryResult.items = _.reject(this.queryResult.items, (v) => { return v.id == vehicleId })
            })
                .catch(e => {
                    console.log(e)
                });

    }

    deletePhoto(id) {

        const token = localStorage.getItem("id_token");

        if( token != null)
            axios.delete(`/api/vehicles/${this.vehicleId}/photos/${id}`, {headers: {Authorization: "Bearer " + token }})
                .then(response => { 
                    this.photos = _.reject( this.photos, (photo) => { return photo.id == id })
                })
                .catch(error => { })
    }





}

