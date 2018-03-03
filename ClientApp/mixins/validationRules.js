export default {


    methods: {



        ucsValidationRules() {


            return {

                rules: {

                    ipAddress: 'required|min:6',
                    subnet: 'required|min:6',
                    cred_name: 'required|min:4',
                    cred_username: 'required|min:4',
                    cred_password: 'required|min:5',
                    cred_port: 'required|min:2',
                    cred_protocol: 'required|min:3'
                        // contract_naam: 'required|min:4',
                        // meervest: 'required',
                        // vestigingen: 'required_if:meervest,ja|min:2',
                        // imtech: 'required',
                        // imtechconnr: 'required_if:imtech,ja',
                        // contract_type: 'required|not_in:0',
                        // algemeen_opmerking: 'max:250',

                },
                messages: {

                    required: 'Dit is een verplicht veld',
                    required_if: 'Dit is een verplicht veld',
                    not_in: 'Maak een keuze',
                    max: 'Te veel tekst',
                    min: 'Te weinig tekens of keuzes',

                },
            }

        },

        /**
         * Validation rules and messages for
         * Klantgegevens
         */

        klantgegevensRules() {


            let rules_set1 = {

                mOpdrachtgever: 'required',
                klant_type: 'required|not_in:0',
                klant_naam: 'required',
                klantKvK: 'required_if:klant_ype,2|min:4',

            };

            let rules_set2 = {

                mOpdrachtgever: 'required',
                versklant_type: 'required|not_in:0',
                naam_partij: 'required|min:4',
                versklant_opdrgever: 'required|not_in:0',
                opdrachtgever: 'required',
                kvk_opdrachtgever: 'required_if:versklant_opdrgever,2|min:4',
                versfactuur_partij: 'required|not_in:0',
                factuur_partij: 'required',

            };

            let ruleset = this.klantgegevensForm.mOpdrachtgever == 'nee' ? rules_set2 : rules_set1;


            return {

                rules: ruleset,

                messages: {
                    required: 'Dit is een verplicht veld',
                    required_if: 'Dit is een verplicht veld',
                    not_in: 'Maak een keuze',
                    max: 'Te veel tekst',
                    min: 'Kies min. 2',

                }
            }

        }

    }

}