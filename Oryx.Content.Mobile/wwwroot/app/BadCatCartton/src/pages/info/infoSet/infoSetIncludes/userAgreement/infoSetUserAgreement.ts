import { Component } from '@angular/core';
import { NavController, ModalController, ViewController } from 'ionic-angular';
import { Authentication } from '../../../../../services/authentication'
import { SocialService } from '../../../../../services/socialService';
import { UserInfoService } from '../../../../../services/userInfoService';
import { ModalTool } from '../../../../../lib/openModal'
import { Pay } from '../../../../../services/pay'

/**infoset includes */


@Component({
    selector: "info-set-useragreement-page",
    templateUrl: "infoSetUserAgreement.html"
})
export class infoSetUserAgreement {
    constructor(public navCtl: NavController,
        public viewCtrl: ViewController,
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
}