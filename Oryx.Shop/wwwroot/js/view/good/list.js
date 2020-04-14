var _vgoodList = new Vue({
    el: "#list-panel",
    data: {
        list: [],
        query: {
            GoodsName: "",
            StartPrice: null,
            EndPrice: null,
            StartTime: null,
            EndTime: null,
            Index: 1,
            Size: 10,
            Total: 1
        }
    },
    created: function () {
        this.init();
    },
    methods: {
        init: function (page) {
            var self = this;
            if (page) { self.page = page; }
            $.post("/Goods/GetListGoods", { page: self.page },
                function (msg) {
                    var res = [];
                    msg.Data.map(good => {
                        var item = good;
                        item.CoverPics = JSON.parse(good.CoverPics);
                        res.push(item);
                    })
                    self.$set(self, "list", res)
                }
            )
        },
        delGoods: function (id) {
            if (window.confirm("该操作不可逆，确认删除吗？")) {
                $.post("/Goods/DeleteGood?Id=" + id,
                    function (msg) {
                        InitResult(msg)
                    }
                )
            }
        }
    }
})