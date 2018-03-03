export default class Errors {

    errors: any  = {};

    constructor() {
        this.errors = {};
    }

    //Check if error exists -- v-if="error.has('fieldname')" <span>
    has(field: any) {

        return this.errors.hasOwnProperty(field);

    }

    //Check if there are any errors
    any() {
        console.log(this.errors)
        return Object.keys(this.errors).length > 0;
    }

    //Get error by field name
    get(field: any) {

        if (this.errors[field]) {

            return this.errors[field][0];
        }
    }

    //register all errors
    record(errors: any) {

        this.errors = errors;

    }

    //Clear error onkeydown by field -- apply @keydown="errors.clear($event.target.name)" to <form>
    clear(field: any) {

        if (field) {

            delete this.errors[field];

            return;
        }

        this.errors = {};

    }

}