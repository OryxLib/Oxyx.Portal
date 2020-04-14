import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServiceConfig } from '../services/serviceconfig';
import { Storage } from '@ionic/storage';

@Injectable()
export class Authentication {
    constructor(public http: HttpClient,
        public serviceConfig: ServiceConfig,
        public storage: Storage) {

    }
    isLogin(callback: Function) {
        this.storage.get("AccessToken").then(accessToken => {
            if (accessToken) {
                this.serviceConfig.httpGet(this.serviceConfig.getHasLoginUrl(), res => {
                    res.data = res.data[0]
                    callback(res)
                });
            } else {
                callback({ success: false, data: null })
            }
            // if (accessToken) {
            //     callback({ success: true, Data: accessToken })
            // } else {
            //     this.serviceConfig.httpGet(this.serviceConfig.getHasLoginUrl(), callback);
            // }

        })
    }
    login(UserName: string, Password: string, callback: Function) {
        this.http.post(this.serviceConfig.getLoginUserUrl(), {
            UserName,
            Password
        }).subscribe(res => {
            if (res['success']) {
                this.storage.set("AccessToken", res['data'])
            }
            callback(res)
        }, err => {
            callback({ success: false, message: err.message })
        })
    }
    register(UserName: string, Password: string, callback: Function) {
        console.log("UserName")
        console.log(UserName)
        this.http.post(this.serviceConfig.getRegisterUserUrl(), {
            UserName: UserName,
            Password: Password
        }).subscribe(res => {
            callback(res)
        }, err => {
            callback({ success: false, message: err.message })
        })
    }
    logout(callback: Function) {
        this.http.post(this.serviceConfig.getLogoutUserUrl(), {}).subscribe(res => {
            if (res['success']) {
                this.storage.remove("AccessToken")
                this.storage.remove('userInfo')
            }
            callback(res)
        }, err => {
            callback({ success: false, message: err.message })
        })
    }
    checkOldPassword(oldPwd: string, callback: Function) {
        this.http.post(this.serviceConfig.getAuthOldPwd(), {
            pwd: oldPwd
        }).subscribe(res => {
            callback && callback(res)
        }, err => {
            callback && callback({ success: false, message: err.message })
        })
    }
    changePassword(data, callback: Function) {
        this.http.post(this.serviceConfig.getChangePasswordUrl(), data).subscribe(res => {
            callback && callback(res);
        }, err => {
            callback({ success: false, message: err.message })
        });
    }
    wxLogin(code: string) {

    }
    getUserInfo() {

    }

}