new Vue({
    el: '#trendApp',    
    methods: {
        GetTrendsForConsole: function () {
            console.log(true);
            $.get('/Trends/dataforconsole', result => {
                console.log(result);
            })
        }

    }
})