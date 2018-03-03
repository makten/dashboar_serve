

export default {
	components: {  
		Gauge
	},

	data() {
		return {		
			gauges: [],
		}
	},

	mounted() {
		this.$nextTick(function(){
			// this.initialize();

			// setInterval(() => {
			// 	this.updateGauges()
			// }, 5000);
		});
	},

	methods: {

		createGauge(name, label, min, max) {

			var config = {
				size: 150,
				label: label,
				min: undefined != min ? min : 0,
				max: undefined != max ? max : 100,
				minorTicks: 5
			}

			var range = config.max - config.min;
			config.yellowZones = [{
				from: config.min + range * 0.75,
				to: config.min + range * 0.9
			}];

			config.redZones = [
			{
				from: config.min + range * 0.9,
				to: config.max
			}
			];

			this.gauges[name] = new Gauge(name + "GaugeContainer", config);
			this.gauges[name].render();
		},

		createGauges(gaugesContainer) {
			_.each(gaugesContainer, (c) => {this.createGauge(c.id, c.label, c.minVal, c.maxVal)})						
		},

		updateGauges(id, val, total) {

			for (var key in this.gauges) {

				if(key == id) {
					// this.gauges[key].redraw( val * 1 * 2);
					this.gauges[key].redraw( 100 - (val / total * 100));
						// this.gauges[key].redraw( 100 - (41000 /  this.loadedStats.total_memory * 100));
			    		// this.gauges[key].redraw( 100 - (this.loadedStats.available_memory /  this.loadedStats.total_memory * 100));
			    	}
			    }
			},

			// initialize() {
			// 	this.createGauges();	
			// }

		}
	}