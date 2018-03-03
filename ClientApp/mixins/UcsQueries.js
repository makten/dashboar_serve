// import NProgress from 'nprogress';

export default {
    props: [],

    data() {
        return {

        }
    },


    mounted() {
        this.$nextTick(function() {

        })
    },

    methods: {

        runQuery(method, uri) {

            NProgress.start();

            axios({
                    method: method,
                    url: uri
                })
                .then(response => {

                    databag = JSON.parse(response.data)

                    NProgress.done();

                })
                .catch(response => {

                    NProgress.done();
                });

        },

    }
}