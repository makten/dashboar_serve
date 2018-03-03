<script>

  import ChartJs from 'chart.js';

  export default {  

    props: {
      chartid: {required: true, default: 'mychart', type: String},
      dataset: { required: true},
      width: {required: false, default: 550, type: Number},
      height: {required: false, default: 200, type: Number},
      options: {required: false}
    },

    data() {
      return {

        count: 10,        

        chartOptions: {
            responsive: true,
            animation: {
              duration: 0,              
              scaleOverride : true,             
              scaleSteps : 10,             
              scaleStepWidth : 10,              
              scaleStartValue : 0
            },

            elements: {
              line: {
                borderWidth: 1,
              },
              point: {
                radius: 0,
              },
            },

            scales: {
              xAxes: [{
                type: "time",
                time: {
                  // format: "HH:mm:ss",
                  // tooltipFormat: "HH:mm:ss",
                },
                scaleLabel: {
                  display: true,
                  labelString: "Date",
                },
              }],
              yAxes: [{
                stacked: true,
                scaleLabel: {
                  display: true,
                },
              }],
            },

          },

        optionsAnimation: {
          
          animation: {
            duration: 0,              
            scaleOverride : true,             
            scaleSteps : 10,             
            scaleStepWidth : 10,              
            scaleStartValue : 0
          },

          scaleOverride : true,          
          scaleSteps : 10,          
          scaleStepWidth : 10,          
          scaleStartValue : 0
        },

        optionsNoAnimation: {          
          animation : false,
          //Boolean - If we want to override with a hard coded scale
          scaleOverride : true,
          //** Required if scaleOverride is true **
          //Number - The number of steps in a hard coded scale
          scaleSteps : 20,
          //Number - The value jump in the hard coded scale
          scaleStepWidth : 10,
          //Number - The scale starting value
          scaleStartValue : 0
        }

      };
    },


    mounted() {

      this.$nextTick(function(){
        
         //Get the context of the canvas element we want to select
         var ctx = document.getElementById(`${this.chartid}`).getContext("2d");
         this.createChart(ctx)

         // let vm = this;
        //  setInterval(function(){
        //   // vm.updateData(vm.chartdata);            
        //   // vm.createChart(ctx, this.dataset, this.options)

        // }, 9000);        

       });      
    },

    methods: {

      createChart(ctx) {
       
        let vm = this
        var myChart = new ChartJs(ctx, {
          type: 'line',
          data: vm.dataset,          
          options: vm.options
        });

      }      

    },

    computed: {}
  }


</script>

<template>
  <div>

    <canvas :id="chartid" width="width" height="height" style="width: 400px !important; height: 150px !important;"></canvas>    

  </div>
</template>

<style lang="sass">

</style>