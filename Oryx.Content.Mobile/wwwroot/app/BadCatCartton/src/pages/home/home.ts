import { Component } from '@angular/core';
import { NavController, IonicPage, NavParams, LoadingController } from 'ionic-angular';
import { CategoryDetail } from '../../pages/content/categoryDetail/categoryDetail'
import { ContentService } from '../../services/contentService'
import { CategoryListPage } from '../content/list/list';
import { WechatService } from '../../services/wechatService'
import { ContentNews } from '../../pages/content/contentNews/contentNews';
import { CommonService } from '../../services/commonService';

@IonicPage({
  name: 'home',
  segment: 'home'
})
@Component({
  selector: 'page-home',
  templateUrl: 'home.html'
})

export class HomePage {
  contentList = []
  hotContentList = []
  recentContentList = []
  limitContentList = []
  kongbuContentList = []
  japanContentList = []
  queryKey: string
  contentSplit: string = "category";
  newslist: any;
  newsIndex: number = 0;
  newsPagesize: number = 10;
  checkedContentList = []
  loadingHanderl: any;
  bannerList = [];
  constructor(public navCtrl: NavController,
    public loadingCtrl: LoadingController,
    private navParams: NavParams,
    public wechatService: WechatService,
    public commonSrv: CommonService,
    public contentService: ContentService) {
    console.log("home info")
    console.log(this.navParams.data)
  }
  onInput(event) {
    console.log(event)
  }

  ionViewWillEnter() {
    var that = this;
    // this.contentService.getTopList(function (res) {
    //   that.contentList = res.Data;
    // });
    this.contentService.getBannerList(res => {
      console.log(res)
    });
    this.contentService.getCategoryList("恋爱", 0, 6, res => {
      this.contentList = res.data.item1;
    });
    this.contentService.getCategoryList("恐怖", 0, 6, res => {
      this.kongbuContentList = res.data.item1;
    });
    this.contentService.getCategoryList("日漫", 0, 6, res => {
      this.japanContentList = res.data.item1;
    })
    this.contentService.contentRecentCategoryByUser(res => {
      console.log(res.data)
      if (res.data) {
        this.recentContentList = res.data.slice(0, 3)

      }
    })
    this.contentService.getNews(this.newsIndex * this.newsPagesize, this.newsPagesize, res => {
      this.newslist = res.data
    })
    this.setWxSDK();
  }

  setWxSDK() {
    if (!window['wx'])
      return;
    window['wx'].ready(function () {
      // 获取“分享到朋友圈”按钮点击状态及自定义分享内容接口
      window['wx'].onMenuShareTimeline({
        title: "次元吖漫画社区", // 分享标题
        link: window.location.href,
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

      // 获取“分享给朋友”按钮点击状态及自定义分享内容接口
      window['wx'].onMenuShareTimeline({
        title: "次元吖漫画社区", // 分享标题
        link: window.location.href,
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
    });

  }
  wxLogin() {
    window.Wechat.isInstalled(function (installed) {
      alert("Wechat installed: " + (installed ? "Yes" : "No"));
    }, function (reason) {
      alert("Failed: " + reason);
    });
    // this.wechatService.login();
    var scope = "snsapi_userinfo",
      state = "_" + (+new Date());
    window.Wechat.auth(scope, state, function (response) {
      // you may use response.code to get the access token.
      alert(JSON.stringify(response));
    }, function (reason) {
      alert("Failed: " + reason);
    });


  }

  wxShare() {
    // this.wechatService.share("", "send-link-thumb-local");
    window.Wechat.share({
      text: "This is just a plain string",
      scene: window.Wechat.Scene.TIMELINE   // share to Timeline
    }, function () {
      alert("Success");
    }, function (reason) {
      alert("Failed: " + reason);
    });
  }
  wbLogin() {
    try {
      window.WeiboSDK.ssoLogin(function (args) {
        window.Sentry.captureMessage("微博login:" + JSON.stringify(args));
      }, function (failReason) {
        window.Sentry.captureMessage("微博login fail:" + failReason);
      });
    } catch (error) {
      window.Sentry.captureMessage("微博登录trycatch:" + error);
    }
  }

  wbShare() {
    var args;
    args.url = 'https://cordova.apache.org/';
    args.title = 'Apache Cordova';
    args.description = 'This is a Cordova Plugin';
    args.image = 'https://cordova.apache.org/static/img/pluggy.png';
    window.WeiboSDK.shareToWeibo(function () {
      alert('share success');
    }, function (failReason) {
      alert(failReason);
    }, args);
  }
  itemSelected(item) {
    this.navCtrl.push(CategoryDetail, {
      id: item.id
    });
  }
  goto(tagName) {
    this.navCtrl.push(CategoryListPage, { tagName })
  }

  gotoDetail(id) {
    // this.navCtrl.push(ContentNews, { id })
    this.navCtrl.push('content-news', { id })
  }

  onSearchByQuery() {
    var tagName = this.queryKey;
    this.navCtrl.push(CategoryListPage, { tagName })
  }


  activeDict = {
    status: {
      End: true,
      Continue: false
    },
    type: {
      "全部": true,
      "爱情": false,
      "游戏": false,
      "科幻": false,
      "恐怖": false,
      "耽美": false,
      "热血": false,
    }
  }

  checkList = {}
  checkTxt = '';
  checkCat(txt, group) {
    this.checkList = {}
    for (const key in this.activeDict[group]) {
      if (this.activeDict[group].hasOwnProperty(key)) {
        if (key == txt) {
          this.activeDict[group][key] = true;
          if (txt != '全部') {
            this.checkList[txt] = []
            this.checkTxt = txt;
            this.getCheckedContentList(txt);
          } else {
            this.checkList = {}
            this.checkTxt = '';
            this.checkedContentList = []
          }
        } else
          this.activeDict[group][key] = false;
      }
    }
  }

  getCheckedContentList(checkedTxt) {
    if (!checkedTxt) return;
    this.loadingHanderl = this.loadingCtrl.create({
      content: '加载中...'
    });
    this.loadingHanderl.present();
    this.contentService.getCategoryList(checkedTxt, 0, 21, res => {
      this.checkedContentList = res.data.item1;
      this.loadingHanderl.dismiss();
    })
  }


  filterImgPath(imgSrc) {
    return this.commonSrv.imgSrcFilter(imgSrc);
  }
}
