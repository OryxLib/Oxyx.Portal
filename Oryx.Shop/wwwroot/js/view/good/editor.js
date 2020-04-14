var _vGoodEditor = new Vue({
    el: "#createds-panel",
    data: {
        Good: {
            Name: "test food 1",
            Price: 0.1,
            Category: {Id:3},
            CoverPics: "",
            Detail: ""
        },
        CoverDom: "#uplodaCovs",
        CoverPics: ["http://p8pkycql7.bkt.clouddn.com/1528357245151.jpg","http://p8pkycql7.bkt.clouddn.com/1528357254780.jpg"],
        Detail: "",
        Categories: [],
        NewCatetory: {
            Name: ""
        }
    },
    created: function () {
        this.init();
    },
    methods: {
        init: function () {
            var self = this;
            //get all categories??没有数据？
             self.initCagetory();
            //modify init(if)
        },
        initCagetory: function () {
            var self = this;
            $.get("/Goods/GetListGoodCategory", function (msg) {
                self.$set(self, 'Categories', msg.Data);
            })
        },
        submit: function () {
            var self = this;
            self.Good.CoverPics = JSON.stringify(self.CoverPics);
            self.Good.Detail = self.Detail;
            self.Good.Category = self.Categories.find(x => x.Id === self.Good.Category.Id);
            console.log(self.Good);
            $.post("/Goods/AddGoods", { goodModel: self.Good }, function (msg) {
                InitResult(msg);
            })
        },
        uploadCovs: function (event) {
            var self = this;
            var baseUrl = "http://p8pkycql7.bkt.clouddn.com";
            var blob = event.target.files[0];
            UploadFile({
                blob: blob,
                name: new Date() - 0 + ".jpg",
                next: function (res) {
                    //item.percent = res.total.percent;
                    //Vue.set(arr, index, item)
                },
                error: function (res) {
                    console.log(res)
                },
                complete: function (res) {
                    self.CoverPics.push(baseUrl + "/" + res.key)
                    //if (--uploadLength == 0) {
                    //    _this.createVote();
                    //}
                }
            })
            //var self.CoverPics
        },
        setDetail: function (txt) {
            var self = this;
            self.Detail = txt;
            self.submit();
        },
        createCagetory: function () {
            var self = this;
            ShowMask();
            if (self.NewCatetory.Name === '') {
                ErrorMessage('类别名称不能为空');
                return;
            }
            $.post('/Goods/AddGoodCategory', { category: self.NewCatetory },
                function (msg) {
                    InitResult(msg, function (data) {
                        HideMask();
                        $('#createCategory').modal('hide');
                        self.initCagetory();
                    })
                })
        },
        //changeCate: function (id, name) {
        //    $.post('/Goods/UpdateCategory', { id: id, name: name },
        //        function (msg) {
        //            console.log(msg);
        //        })
        //}
    }

})

function converToBase64(file, cb) {
    var fileReader = new FileReader();
    fileReader.onload = function (e) {
        cb && cb(e.target.result)
    }
    fileReader.readAsDataURL(file);
}

function UploadFile(file) {
    var token = '';
    $.ajax({
        url: "/Pic/Token",
        dataType: "json",
        type: "get",
        async: false,
        success: function (res) {
            token = res.Data;
        }
    })
    var observer = qiniu.upload(file.blob, file.name, token, null, {
        useCdnDomain: true,
        region: qiniu.region.z0
    });
    observer.subscribeAction({
        next: file.next,
        error: file.error,
        complete: file.complete
    })
}