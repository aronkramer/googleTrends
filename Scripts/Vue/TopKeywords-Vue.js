new Vue({
    el: '#kwApp',
    mounted: function () {
        this.GetSkus();
    },
    data: {
        topkws: [],
        itemQuestion: {
            Id: '',
            SKU: '',
            Asin: '',
            Keywords: ''
        },
        editMode: false
    },
    methods: {
        deleteSku: async function (Id) {
            await $.post("/SKU/DeleteById", { Id });
            window.location.reload();
        },
        GetSkus: function () {
            $.get("/SKU/GetTopKWForVue", result => {
                this.topkws = result;
            });
        },
        GetSkusById: function (id) {
            this.itemQuestion = this.topkws.find(data => data.Id === id);
        },
        makeEditable: function (id) {
            this.GetSkusById(id);
            var row = this.itemQuestion;
            console.log(row.Id);
        },
        clear: function () {
            document.getElementById('clearForm1').reset();
            document.getElementById('clearForm2').reset();
        }
    }
})