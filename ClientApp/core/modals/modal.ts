import { Component, Prop } from 'vue-property-decorator';
import Vue from 'vue';
import * as $ from 'jquery';
import * as globals from '../../globals';

@Component
export default class ModalComponent extends Vue {

	@Prop({ required: true, default: 'default-model' })
	modalname: any;

	@Prop({ required: false, default: false })
	isDashboard: any;

	@Prop({ required: false })
	width: any;

	styleObject: any = { width: this.width + '%', background: "#fff" }



	mounted() {

		
		this.$nextTick(function () {			

			$(`#${this.modalname}`).modal('show');

			$(`#${this.modalname}`).modal({
				backdrop: 'static',
				keyboard: true
			})

			$(`#${this.modalname}`).on("hidden.bs.modal", function () {

				globals.eventBroadcaster.$emit('closeModal')
			});

		});
	}	

}
