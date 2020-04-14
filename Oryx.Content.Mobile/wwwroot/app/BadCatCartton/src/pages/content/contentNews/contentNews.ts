import { Component } from '@angular/core';
import { NavController, NavParams, IonicPage } from 'ionic-angular';
import { ContentService } from '../../../services/contentService'
import { ContentDetailPage } from '../detail/detail'
import { GlobalData } from '../../../services/globalData'

@IonicPage({
    name: 'content-news',
    segment: 'contentNews/:id',
    defaultHistory: ['home']
})
@Component({
    selector: "content-news-page",
    templateUrl: "contentNews.html"
})
export class ContentNews {
    newsId: string;
    newsDetailInfo: any;
    localUrl: string;
    constructor(public navParams: NavParams,
        public navCtrl: NavController,
        public globalData: GlobalData,
        public contentService: ContentService) {
        this.newsId = this.navParams.data.id;

        this.localUrl = window.location.href;// window.location.protocol + "//" + window.location.host + '/#/' + "contentNews/" + this.newsId
        this.initData();
    }
    ionViewWillLeave() {
        console.log('leave page1')
        if (!window['wx'])
            return;
        if (!window['wx'].onMenuShareTimeline) {
            window['wx'].unbindMenuShareTimeline();
        }
        if (!window['wx'].onMenuShareAppMessage) {
            window['wx'].unbindShareAppMessage()
        }
    }
    ionViewWillUnload() {
        console.log('leave page2')
    }
    ionViewDidLeave() {
        console.log('leave page3')
        // if (!window['wx'])
        //     return;
        // if (!window['wx'].onMenuShareTimeline) {
        //     window['wx'].unbindMenuShareTimeline();
        // }
        // if (!window['wx'].onMenuShareAppMessage) {
        //     window['wx'].unbindShareAppMessage()
        // }
    }
    initData() {
        this.contentService.getNewsDetail(this.newsId, res => {
            this.newsDetailInfo = res.data
            this.initWxJSSDK()
        });

    }

    initWxJSSDK() {
        var xhr = new XMLHttpRequest();
        xhr.onload = () => {
            var jssdkInfo = JSON.parse(xhr.responseText);
            this.wxInit(jssdkInfo);
        }
        xhr.open("get", "https://cartoon.oryxl.com/Wx/JsApiPackage?url=" + encodeURIComponent(window.location.href))
        //xhr.open("get", "http://localhost:39499/Wx/JsApiPackage?url=" + window.location.href)
        xhr.send();
    }


    wxInit(sdkInfo) {
        if (!window['wx'])
            return;
        window['wx'].config({
            debug: false, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。
            appId: sdkInfo.appId, // 必填，企业号的唯一标识，此处填写企业号corpid
            timestamp: sdkInfo.timestamp, // 必填，生成签名的时间戳
            nonceStr: sdkInfo.nonceStr, // 必填，生成签名的随机串
            signature: sdkInfo.signature, // 必填，签名，见附录1
            jsApiList: ['updateTimelineShareData', 'updateAppMessageShareData', 'onMenuShareTimeline', 'onMenuShareAppMessage', 'onMenuShareQQ', 'onMenuShareWeibo',
                'onMenuShareQZone'
            ] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2
        });
        window['wx'].ready(() => {
            this.setNewsData()
        })
    }
    setNewsData() {
        if (!window['wx'].onMenuShareTimeline) {
            window['wx'].updateAppMessageShareData({
                title: "[次元吖]" + this.newsDetailInfo.title, // 分享标题
                desc: '',// 分享描述
                link: this.localUrl, // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl: "http://mobile.17look.net/assets/imgs/officialAvatar80.png", // 分享图标
                success: function () {
                    // 设置成功
                }
            })
        } else {
            window['wx'].onMenuShareTimeline({
                title: "[次元吖]" + this.newsDetailInfo.title, // 分享标题
                link: this.localUrl,
                imgUrl: "http://mobile.17look.net/assets/imgs/officialAvatar80.png", // 分享图标
                success: function (res) {
                    //alert(JSON.stringify(res))
                    console.log(res)
                },
                err: function (err) {
                    //alert(JSON.stringify(err))
                    console.log(err)
                }
            });
        }

        if (!window['wx'].onMenuShareAppMessage) {
            window['wx'].updateTimelineShareData({
                title: "[次元吖]" + this.newsDetailInfo.title, // 分享标题
                link: this.localUrl,// 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                imgUrl: "http://mobile.17look.net/assets/imgs/officialAvatar80.png",// 分享图标
                success: function () {
                    // 设置成功
                }
            });
        }
        else {
            // 获取“分享给朋友”按钮点击状态及自定义分享内容接口
            window['wx'].onMenuShareAppMessage({
                title: "[次元吖]" + this.newsDetailInfo.title, // 分享标题
                link: this.localUrl,
                imgUrl: "http://mobile.17look.net/assets/imgs/officialAvatar80.png", // 分享图标
                desc: '你要的姿势我都会！找朋友看漫画！就上次元吖！', // 分享描述
                type: 'link', // 分享类型,music、video或link，不填默认为link
                success: function (res) {
                    //alert(JSON.stringify(res))
                    console.log(res)
                },
                err: function (err) {
                    //alert(JSON.stringify(err))
                    console.log(err)
                }
            });
        }
    }
}