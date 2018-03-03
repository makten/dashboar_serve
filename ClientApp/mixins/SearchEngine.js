export default {

    mounted() {

        this.$nextTick(function() {


        })
    },


    methods: {


        /**
         * Form validator		
         */

        makeSearch(uri, query) {

            this.$http.post(uri, query).then((response) => {

                this.setCompanySearchData(response.data);
            })

        },

        // This is where the debounce actually belongs.
        // expensiveOperation: _.debounce(function () {
        // 	this.isCalculating = true
        // 	setTimeout(function () {
        // 		this.isCalculating = false
        // 		this.searchQueryIsDirty = false
        // 	}.bind(this), 1000)
        // }, 500)

        persistCompanydetails(method, uri, form) {

            this.$http[method](uri, form).then(response => {

                this.companies.push(response.data);
            })
        }





    }


}