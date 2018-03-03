import { Component, Prop } from 'vue-property-decorator';
import Vue from 'vue';
import * as globals from '../../globals';
import * as moment from 'moment';


@Component
export default class DatetimePicker extends Vue {

	@Prop({ required: false, default: true })
	model: any;

	@Prop({ required: false, default: false })
	name: any;

	@Prop({ required: false, default: moment() })
	placeholder: any;



	mounted() {

		this.$nextTick(function () {
			(<any>$(this.$refs.datetimeinput)).datepicker();
		});

	}

}
