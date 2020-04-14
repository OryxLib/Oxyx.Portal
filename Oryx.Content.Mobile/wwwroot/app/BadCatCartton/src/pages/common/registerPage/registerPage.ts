import { Component } from '@angular/core';
import {
    NavController,
    LoadingController,
    AlertController,
    ViewController,
    Events
} from 'ionic-angular';
import { Authentication } from '../../../services/authentication'
import { Storage } from '@ionic/storage';

@Component({
    selector: "register-page",
    templateUrl: "register.html"
})
export class RegisterPage {
    userName: string;
    password: string;
    repeatPassword: string;
    alertHander: any;
    loadingHanderl: any;
    constructor(public navCtl: NavController,
        public loadingCtrl: LoadingController,
        public alertCtrl: AlertController,
        public viewCtrl: ViewController,
        public auth: Authentication,
        public storage: Storage,
        public events: Events) {

    }

    register() {
        this.presentLoading()
        this.auth.register(this.userName, this.repeatPassword, res => {
            this.loadingHanderl.dismiss();
            if (res.success) {
                this.storage.set("AccessToken", res.data)
                this.presentAlert();
            }
        })
    }

    presentAlert() {
        this.alertHander = this.alertCtrl.create({
            title: '提示',
            subTitle: '结界密令已下发',
            buttons: [{
                text: '确定',
                handler: () => {
                    this.userName = '';
                    this.password = '';
                    this.repeatPassword = '';
                    //this.viewCtrl.dismiss();
                    this.events.publish("userLogined")
                }
            }]
        });
        this.alertHander.present();
    }
    presentLoading() {
        this.loadingHanderl = this.loadingCtrl.create({
            content: '正在写信给太上老君...'
        });
        this.loadingHanderl.present();
    }

}