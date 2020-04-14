import { Component } from '@angular/core';
import { NavController, ModalController, Events } from 'ionic-angular';
import { Authentication } from '../../../services/authentication'
import { ContentDetailPage } from '../../../pages/content/detail/detail'
import { SocialService } from '../../../services/socialService';
import { UserInfoService } from '../../../services/userInfoService';
import moment from "../../../lib/moment.js"
import { ModalTool } from '../../../lib/openModal'
import { InfoEditPage } from '../infoEdit/infoEdit'
import { Pay } from '../../../services/pay'

/**infoset includes */
//import * from '../../info/infoSet/infoSetIncludes/changePassword/infoSetChangePassword'
import { infoSetChangePassword } from '../../info/infoSet/infoSetIncludes/changePassword/infoSetChangePassword'
import { infoSetCheckUpdate } from '../infoSet/infoSetIncludes/checkUpdate/infoSetCheckUpdate'
import { infoSetFeedback } from '../infoSet/infoSetIncludes/feedback/infoSetFeedback'
import { infoSetUserAgreement } from '../infoSet/infoSetIncludes/userAgreement/infoSetUserAgreement'

@Component({
    selector: "info-set-main-page",
    templateUrl: "infoSetMain.html"
})
export class infoSetMain {
    constructor(public navCtl: NavController,
        public pay: Pay,
        public socialService: SocialService,
        public userInfoService: UserInfoService,
        public modelCtrl: ModalController,
        public modalTool: ModalTool,
        public auth: Authentication,
        public events: Events) {
    }

    ionViewWillEnter() {

    }

    openUserAgreement() {
        this.modalTool.openModal(this.modelCtrl, infoSetUserAgreement)
    }

    openCheckUpdate() {
        this.modalTool.openModal(this.modelCtrl, infoSetCheckUpdate)
    }

    openFeedback() {
        this.modalTool.openModal(this.modelCtrl, infoSetFeedback)
    }

    openChangePassword() {
        this.modalTool.openModal(this.modelCtrl, infoSetChangePassword)
    }

    logout() {
        this.auth.logout(res => {
            this.events.publish("userLogout")
        })
    }
}