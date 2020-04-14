import { Component } from '@angular/core';
import { NavController, ModalController, ViewController, AlertController } from 'ionic-angular';
import { Authentication } from '../../../../../services/authentication'
import { UserInfoService } from '../../../../../services/userInfoService';

/**infoset includes */


@Component({
    selector: "info-set-feedback-page",
    templateUrl: "infoSetFeedback.html"
})
export class infoSetFeedback {
    constructor(public navCtl: NavController,
        public viewCtrl: ViewController,
        public alertCtrl: AlertController,
        public userInfoService: UserInfoService,
        public modelCtrl: ModalController,
        public auth: Authentication) {
    }
    contact: string;
    feedbackTxt: string;
    alertHandler: any;
    dismiss() {
        console.log('dismiss')
        this.viewCtrl.dismiss();
    }

    submit() {
        this.userInfoService.pushFeedback({
            Contact: this.contact,
            FedbackTxt: this.feedbackTxt
        }, res => {
            this.presentAlert("感谢您提出宝贵的意见或建议，谢谢~")
        });
    }

    presentAlert(alertMsg: string) {
        this.alertCtrl.create({
            title: '提示',
            subTitle: alertMsg,
            buttons: [{
                text: '确定'
            }]
        });
        this.alertHandler.present();
    }
}