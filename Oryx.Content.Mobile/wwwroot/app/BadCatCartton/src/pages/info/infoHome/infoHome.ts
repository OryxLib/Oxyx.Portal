import { Component } from '@angular/core';
import { NavController, ModalController } from 'ionic-angular';
import { Authentication } from '../../../services/authentication'
import { ContentDetailPage } from '../../../pages/content/detail/detail'
import {CategoryDetail} from '../../../pages/content/categoryDetail/categoryDetail'
import { SocialService } from '../../../services/socialService';
import { UserInfoService } from '../../../services/userInfoService';
import moment from "../../../lib/moment.js"
import { ModalTool } from '../../../lib/openModal'
import { InfoEditPage } from '../infoEdit/infoEdit'
import { Pay } from '../../../services/pay'
import { infoSetMain } from '../infoSet/infoSetMain';
import { CommonService } from '../../../services/commonService';

@Component({
    selector: "info-home-page",
    templateUrl: "infoHome.html"
})
export class InfoHomePage {
    pet = "readLog";
    contentList: any = []
    currentUserInfo: any = {
        avatar: '',
        nickName: '',
        location: '',
        birthDay: ''
    }
    payQRcode = ''
    fansCount = 0;
    subscribeCount = 0;
    itemSelected(item) {
        this.navCtl.push(CategoryDetail, {
            id: item.id,
            title: item.name
        });

    }
    constructor(public navCtl: NavController,
        public pay: Pay,
        public socialService: SocialService,
        public userInfoService: UserInfoService,
        public modelCtrl: ModalController,
        public modalTool: ModalTool,
        public commonSrv: CommonService,
        public auth: Authentication) {
        this.payQRcode = pay.getQRCode();
    }

    ionViewWillEnter() {
        this.getUserInfo();
        this.getSocialRelationShip();
        this.getPostEntryList();
        this.getContentReadLog();
        this.getCommentList();
    }

    getUserInfo() {
        this.userInfoService.getUserInfo(res => {
            this.currentUserInfo = res;
        })
    }

    getSocialRelationShip() {
        this.socialService.getUserRelationShipInfo(null, res => {
            if (res.Success) {
                this.fansCount = res.Data.FansCount;
                this.subscribeCount = res.Data.SubcribCount
            }
        })
    }

    myPostEntryList = [];
    postEntrySkipCount = 0;
    postEntryPageSize = 10;
    getPostEntryList() {
        this.socialService.getNewPostEntryByUser(this.postEntrySkipCount, this.postEntryPageSize, res => {
            this.myPostEntryList = res.data
        })
    }

    contentReadLogSkipCount = 0;
    coutnentReadLogPageSize = 9;
    getContentReadLog() {
        this.userInfoService.getContentUserReadLog(this.contentReadLogSkipCount, this.coutnentReadLogPageSize, res => {
            this.contentList = res
        })
    }

    commentList = [];
    commentSkipCount = 0;
    commentPageSize = 10;
    getCommentList() {
        this.socialService.getPostEntryCommentsByUsefSelf(this.commentSkipCount, this.commentPageSize, res => {
            this.commentList = res.data;
        })
    }

    updateDatetime(datetime) {
        return moment(datetime).utc().startOf('hour').fromNow()
    }

    openEdit() {
        this.modalTool.openModal(this.modelCtrl, InfoEditPage, this.currentUserInfo, data => {
            this.getUserInfo()
        })
    }

    gotoInfoSet() {
        this.navCtl.push(infoSetMain);
    }

    onBridgeReady() {
        //get pay info
        //调用统一下单接口 拿到数据
        window['WeixinJSBridge'].invoke(
            'getBrandWCPayRequest', {
                "appId": "wx2421b1c4370ec43b",     //公众号名称，由商户传入     
                "timeStamp": "1395712654",         //时间戳，自1970年以来的秒数     
                "nonceStr": "e61463f8efa94090b1f366cccfbbb444", //随机串     
                "package": "prepay_id=u802345jgfjsdfgsdg888",
                "signType": "MD5",         //微信签名方式：     
                "paySign": "70EA570631E4BB79628FBCA90534C63FF7FADD89" //微信签名 
            },
            function (res) {
                if (res.err_msg == "get_brand_wcpay_request:ok") {
                    // 使用以上方式判断前端返回,微信团队郑重提示：
                    //res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
                }
            });
    }

    filterImgPath(imgSrc) {
        return this.commonSrv.imgSrcFilter(imgSrc);
    }

}