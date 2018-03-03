
import Errors from './Errors';
import axios from 'axios';
import * as _ from 'lodash';

// Handle form and errors
export default class Form {
    originalData: any;
    errors: any = {};

    constructor(data: any) {


        this.originalData = data;

        for (let field in data) {

            this[field] = data[field];
        }

        //Create new instance of Errors
        this.errors = new Errors();

    }


    data() {

        let data: any = {};

        for (let property in this.originalData) {

            data[property] = this[property];
        }

        return data;
    }

    //Reset form fields
    reset() {

        for (let field in this.originalData) {
            this[field] = '';
        }

    }


    post(url: string) {
        return this.submit('post', url)
    }


    put(url: string) {   
        alert('you hit put') ;    
        return this.submit('put', url);
    }


    patch(url: string) {
        return this.submit('patch', url);
    }


    delete(url) {
        return this.submit('delete', url);
    }


    submit(requestType: string, url: string) {

        return new Promise((resolve: any, reject: any) => {

            axios[requestType](url, this.data())
                .then((response: any) => {

                    this.onSuccess(response.data);

                    //Part of promise
                    resolve(response.data);

                })
                .catch((error: any) => {

                    this.onFail(error.response)

                    //Part of promise
                    reject(error.response.data)
                });

        });

    }


    onSuccess(response: any) {

        if (!_.isEmpty(response.error)) {

            this.errors.record(response.error)

        } else {

            this.reset()
            this.errors.clear()

            return response
        }

    }

    onFail(response: any) {
        this.errors.record(response.error)
    }

}

declare var Promise: any;