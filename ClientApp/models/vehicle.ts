import { Contact } from './vehicle';
import * as _ from 'lodash';


export interface KeyValuePer {
    id: number;
    name: string;
}

export interface Contact {
    name: string;
    email: string;
    phone: string;
}

export interface Vehicle {
    id: number;
    model: KeyValuePer;
    make: KeyValuePer;
    isRegistered: boolean;
    features: KeyValuePer[];
    contact: Contact;
    lastUpdate: string;
}


export interface SaveVehicle {
    id: number;
    modelId: number;
    makeId: number;
    isRegistered: boolean;
    features: number[];
    contact: Contact;
}