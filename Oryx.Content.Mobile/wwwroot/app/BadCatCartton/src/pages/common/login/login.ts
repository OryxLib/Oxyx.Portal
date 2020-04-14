import { Component } from '@angular/core';
import {
    NavController,
    ViewController,
    NavParams,
    LoadingController,
    AlertController,
    Events
} from 'ionic-angular';
import { RegisterPage } from '../registerPage/registerPage'
import { Authentication } from '../../../services/authentication'
import { Storage } from '@ionic/storage';

@Component({
    selector: "login-page",
    templateUrl: "login.html"
})
export class LoginPage {
    isShowModal: boolean = false;
    userName: any;
    password: any;
    alertHander: any;
    loadingHanderl: any;
    constructor(public navCtl: NavController,
        public auth: Authentication,
        public params: NavParams,
        public viewCtrl: ViewController,
        public loadingCtrl: LoadingController,
        public alertCtrl: AlertController,
        public storage: Storage,
        public events: Events) {
        this.isShowModal = params.data.isShowModal
    }
    gotoReg(event) {
        this.navCtl.push(RegisterPage)
    }

    login() {
        this.presentLoading();
        console.log('login')
        this.auth.login(this.userName, this.password, res => {
            this.loadingHanderl.dismiss();
            if (res.success) { 
                this.userName = '';
                this.password = '';
                this.events.publish("userLogined")
                if(this.isShowModal){
                    this.viewCtrl.dismiss();
                } 
                else{
                    this.navCtl.parent.select(2)
                }
            }
            else {
                this.presentAlert();
            }
        })
    }

    wxLogin(){
        
    }

    dismiss() {
        console.log('dismiss')
        this.viewCtrl.dismiss();
    }

    presentAlert() {
        this.alertHander = this.alertCtrl.create({
            title: '提示',
            subTitle: '用户名密码错误',
            buttons: [{
                text: '确定',
                handler: () => {
                    // this.userName = '';
                    // this.password = '';
                    //this.viewCtrl.dismiss();
                }
            }]
        });
        this.alertHander.present();
    }
    presentLoading() {
        this.loadingHanderl = this.loadingCtrl.create({
            content: '登录中...'
        });
        this.loadingHanderl.present();
    }
}