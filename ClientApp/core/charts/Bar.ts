import { Bar, mixins } from 'vue-chartjs';
const { reactiveProp } = mixins

export default Bar.extend({
	mixins: [reactiveProp],
    props: ['options'],
    mounted() {
        // Overwriting base render method with actual data.
        

        // setInterval(() => {
				this.renderChart(this.chartData, this.options)
			// }, 9000);
    }
})

// , Bubble, Doughnut, HorizontalBar, Line, Pie, PolarArea, Radar