import { Component } from '@angular/core';
import { NavController, ViewController, NavParams } from 'ionic-angular';
import { UserInfoService } from '../../../services/userInfoService';
import { CommonService } from '../../../services/commonService'

@Component({
    selector: "info-edit-page",
    templateUrl: "infoEdit.html"
})
export class InfoEditPage {
    constructor(public navCtl: NavController,
        public commonService: CommonService,
        public viewCtrl: ViewController,
        public userInfoService: UserInfoService,
        public navParams: NavParams) {
        this.currentUserInfo = navParams.data;
    }
    public tmpImg = "";
    public tmpBlob = null;
    public currentUserInfo: any = {
        nickName: '昵称',
        avatar: '/assets/imgs/avatar/avatar_1.jpg',
        birthDay: '',
        location: '银河'
    };

    ionViewWillEnter() {
        this.userInfoService.getUserInfo(res => {
            this.currentUserInfo = res;
        })
    }
 
    saveChange() {
        var key = 'userInfo/' + Math.random() * Math.pow(10, 17) + '/'
        this.commonService.uploadQiniu(this.tmpBlob, key, res => {
            this.currentUserInfo.avatar ='https://mioto.milbit.com/' + res.key
            this.userInfoService.updateUserInfo(this.currentUserInfo, res => {
                this.dismiss();
            })
        }, err => {

        })
    }

    selectImg(event) {
        this.tmpBlob = event.target.files[0]
        this.commonService.converToBase64(this.tmpBlob, res => {
            this.tmpImg = res
        })
        event.target.value = ''
    }

    dismiss() {
        this.viewCtrl.dismiss();
    }

}