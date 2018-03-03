import Errors from './Errors';
import axios from 'axios';
import * as _ from 'lodash';
// Handle form and errors
export default class Form {
    constructor(data) {
        this.errors = {};
        this.originalData = data;
        for (let field in data) {
            this[field] = data[field];
        }
        //Create new instance of Errors
        this.errors = new Errors();
    }
    data() {
        let data = {};
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
    post(url) {
        return this.submit('post', url);
    }
    put(url) {
        alert('you hit put');
        return this.submit('put', url);
    }
    patch(url) {
        return this.submit('patch', url);
    }
    delete(url) {
        return this.submit('delete', url);
    }
    submit(requestType, url) {
        return new Promise((resolve, reject) => {
            axios[requestType](url, this.data())
                .then((response) => {
                this.onSuccess(response.data);
                //Part of promise
                resolve(response.data);
            })
                .catch((error) => {
                this.onFail(error.response);
                //Part of promise
                reject(error.response.data);
            });
        });
    }
    onSuccess(response) {
        if (!_.isEmpty(response.error)) {
            this.errors.record(response.error);
        }
        else {
            this.reset();
            this.errors.clear();
            return response;
        }
    }
    onFail(response) {
        this.errors.record(response.error);
    }
}
//# sourceMappingURL=Form.js.map