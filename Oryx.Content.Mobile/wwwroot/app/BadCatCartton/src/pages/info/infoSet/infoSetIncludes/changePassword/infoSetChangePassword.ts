import { Component } from '@angular/core';
import { NavController, ModalController, AlertController, ViewController } from 'ionic-angular';
import { Authentication } from '../../../../../services/authentication'
import { ContentDetailPage } from '../../../../../pages/content/detail/detail'
import { SocialService } from '../../../../../services/socialService';
import { UserInfoService } from '../../../../../services/userInfoService';
import moment from "../../../../../lib/moment.js"
import { ModalTool } from '../../../../../lib/openModal'
import { InfoEditPage } from '../../../infoEdit/infoEdit'
import { Pay } from '../../../../../services/pay'

/**infoset includes */


@Component({
    selector: "info-set-changePassword-page",
    templateUrl: "infoSetChangePassword.html"
})
export class infoSetChangePassword {
    constructor(public navCtl: NavController,
        public viewCtrl: ViewController,
        public alertController: AlertController,
        public socialService: SocialService,
        public userInfoService: UserInfoService,
        public modelCtrl: ModalController,
        public modalTool: ModalTool,
        public auth: Authentication) {
    }

    oldPwd: string;
    newPwd: string;
    repeatNewPwd: string;
    alertHandler: any;
    submit() {
        this.auth.checkOldPassword(this.oldPwd, res => {
            if (!res.success) {
                this.presentAlert("原密码错误")
                return;
            }
            if (this.newPwd != this.repeatNewPwd) {
                this.presentAlert("新密码两次输入不一致")
            }

            this.presentConfirm(() => {
                this.auth.changePassword(this.repeatNewPwd, res => {
                    if (res.success) {
                        this.presentAlert("修改成功!")
                    }
                    else {
                        this.presentAlert("失败!\n" + res.message)
                    }
                })
            })
        })
    }

    dismiss() {
        console.log('dismiss')
        this.viewCtrl.dismiss();
    }

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

    presentConfirm(nextAction: Function) {
        this.alertHandler = this.alertController.create({
            title: '提示',
            subTitle: '确认修改密码?',
            buttons: [{
                text: '确定',
                handler: () => {
                    nextAction && nextAction();
                }
            }, {
                text: '取消',
                handler: () => {
                    this.alertHandler.hide();
                }
            }]
        });
        this.alertHandler.present();
    }
}