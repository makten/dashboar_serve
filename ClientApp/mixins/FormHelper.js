
import NProgress from 'nprogress';


export default {


	mounted () {

		this.$nextTick(function(){


		})
	},


	methods: {


		/**
		 * Form validator		
		 */

		 validateForm (form, rules, msg){

		 	return new Validator(form, rules, msg);
		 },
		 

		 hasErrors () {

		 	return this.validations.length > 0 ? true : false;				
		 },


		/**
		 * Save form section
		 */

		 persistForm (method, uri, form, databag) {
		 	
		 	form.errors = [];

		 	NProgress.start();

		 	axios(
		 		{method: method, 
		 		url: uri,
		 		data: form
		 	})		 				
		 	.then(response => {			 					

		 		NProgress.done();	

		 		databag.push(response.data)		 		

		 		// let databag = { section: form.section_name, form: form };

		 		// eventBroadcaster.$emit(`${form.section_name}-completed`, databag );		 		

		 		// form.redirect = '';

		 		// form.errors = [];

		 	})
		 	.catch( response => {

		 		NProgress.done();

		 		console.log(JSON.parse(response.data))

		 		eventBroadcaster.$emit(`${form.section_name}-completed`, { section: form.section_name, form: form });
		 		

		 		if (typeof response.data === 'object')
		 		{

		 			form.errors = _.flatten(_.toArray(response.data));
		 		}
		 		else {

		 			form.errors = ['Iets is mis gegaan. Probeer het opnieuw '];
		 		}

		 	});

		 },


		 /**
		  * Get section data and pass for edit
		  */

		  editSection (url){


		  	this.$http.get(url)
		  	.then((response) => {					

		  		this.setData(response.data);
		  	});	 	

		  },

		}


	}

