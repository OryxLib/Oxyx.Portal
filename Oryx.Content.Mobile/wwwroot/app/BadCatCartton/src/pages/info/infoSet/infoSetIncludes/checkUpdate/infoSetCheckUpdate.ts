import { Component } from '@angular/core';
import { NavController, ModalController, ViewController, AlertController } from 'ionic-angular';
import { Authentication } from '../../../../../services/authentication'
import { SocialService } from '../../../../../services/socialService';
import { UserInfoService } from '../../../../../services/userInfoService';
import { ModalTool } from '../../../../../lib/openModal'
import { Pay } from '../../../../../services/pay'

/**infoset includes */


@Component({
    selector: "info-set-checkupdate-page",
    templateUrl: "infoSetCheckUpdate.html"
})
export class infoSetCheckUpdate {
    constructor(public navCtl: NavController,
        public viewCtrl: ViewController,
        public alertController: AlertController,
        public pay: Pay,
        public socialService: SocialService,
        public userInfoService: UserInfoService,
        public modelCtrl: ModalController,
        public modalTool: ModalTool,
        public auth: Authentication) {
    }

    dismiss() {
        console.log('dismiss')
        this.viewCtrl.dismiss();
    }
    checkUpdate() {
        this.presentAlert("恭喜当前已是最新版本!");
    }
    alertHandler: any;
    presentAlert(alertMsg: string) {
        this.alertController.create({
            title: '提示',
            subTitle: alertMsg,
            buttons: [{
                text: '确定'
            }]
        });
        this.alertHandler.present();
    }
}