import { Component } from '@angular/core';
import { NavController, NavParams, ViewController, AlertController } from 'ionic-angular';
import { Pay } from '../../../services/pay'
import { SocialService } from '../../../services/socialService'
import { UserInfoService } from '../../../services/userInfoService';
import moment from "../../../lib/moment.js"
import { ContentDetailPage } from '../../../pages/content/detail/detail'
@Component({
    selector: "social-info-page",
    templateUrl: "infoHome.html"
})
export class SocialInfoHomePage {
    pet = "postEntry";
    contentList = [];
    payQRcode = '';
    userId: string;
    currentUserInfo: any
    isSubscrib: boolean;
    fansCount = 0;
    subscribeCount = 0;
    alertHander: any;
    constructor(public navCtl: NavController,
        public viewCtrl: ViewController,
        public navParams: NavParams,
        public socialService: SocialService,
        public userInfoService: UserInfoService,
        public alertCtrl: AlertController,
        public pay: Pay) {
        // this.payQRcode = pay.getQRCode();
        this.userId = this.navParams.data.id;

        this.getUserInfo();
        this.getSocialRelationShip();
        this.getPostEntryList();
        this.getContentReadLog();
    }
    // ionViewWillEnter() {
    //     this.getUserInfo()
    // }

    itemSelected(item) {
        this.navCtl.push(ContentDetailPage, {
            id: item.id,
            title: item.name
        });

    }


    getUserInfo() {
        this.socialService.getSocialUserInfo(this.userId, res => {
            this.currentUserInfo = res.data.userInfo;
            this.isSubscrib = res.data.isSubcrib
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
    postEntryPageSize = 9;

    getPostEntryList() {
        this.socialService.getNewPostEntryByUserOfSocial(this.userId, this.postEntrySkipCount, this.postEntryPageSize, res => {
            this.myPostEntryList = res.data
        })
    }

    contentReadLogSkipCount = 0;
    coutnentReadLogPageSize = 9;
    getContentReadLog() {
        this.socialService.getContentUserReadLogOfSocial(this.userId, this.contentReadLogSkipCount, this.contentReadLogSkipCount, res => {
            this.contentList = res.data
        })
    }

    subscribe(userId: string) {
        this.socialService.setSubscribe(this.userId, this.isSubscrib, res => {
            this.alertHander = this.alertCtrl.create({
                title: '提示',
                subTitle: '关注成功!',
                buttons: [{
                    text: '确定',
                    handler: () => {
                        
                    }
                }]
            });
            this.alertHander.present();
        });
    }
    updateDatetime(datetime) {
        return moment(datetime).utc().startOf('hour').fromNow()
    }

    dismiss() {
        console.log('dismiss')
        this.viewCtrl.dismiss();
    } 
} 