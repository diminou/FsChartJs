module ChartJS.Renderer

open ChartJS.Chart

let renderChart (canvasName: string) (chartSpec: JsonData)=
    sprintf """<canvas id='%s' width='400' height='400'></canvas>
<script>
requirejs(['https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.1/Chart.min.js'],
    function(chartJs){
        var ctx = document.getElementById('%s').getContext('2d');
        var myChart = new Chart(ctx, %s);
    },
    function(error){
        console.log('error');
        console.log(error);
    }
);
</script>""" canvasName canvasName (JsonData.ToString chartSpec)
