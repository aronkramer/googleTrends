new Vue({
    el: '#trendApp',    
    methods: {
        GetTrendsForConsole: function () {
            console.log(true);
            $.get('/Trends/dataforconsole', result => {
                console.log(result);
            })
        },
        cleanData: function () {
            $.post("/AmazonReports/CleanData");
            alert('Data is ready for the next Google trends update!');
        }

    }
})