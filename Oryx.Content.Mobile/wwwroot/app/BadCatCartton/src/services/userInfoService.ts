import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServiceConfig } from '../services/serviceconfig';
import { Storage } from '@ionic/storage';

@Injectable()
export class UserInfoService {
    constructor(public http: HttpClient,
        public storage: Storage,
        public serviceConf: ServiceConfig) {
    }
    getUserInfo(callback: Function) {
        this.storage.get("userInfo").then(result => {
            var _result = result;
            if (!_result) {
                this.serviceConf.httpGet(this.serviceConf.getUserInfoUrl(), res => {
                    if (res.success) {
                        this.storage.set("userInfo", res.data)
                        _result = res.data;
                    }
                    callback && callback(_result)
                });
            } else {
                callback && callback(_result)
            }
        })
    }

    getOtherUserInfo(userId, callback) {
        this.storage.get('userInfo-' + userId).then(result => {
            var _result = result;
            if (!_result) {
                this.serviceConf.httpGet(this.serviceConf.getOtherUserInfoUrl(userId), res => {
                    if (res.success) {
                        this.storage.set("userInfo", res.data)
                        _result = res.data;
                    }
                    callback && callback(_result)
                });
            } else {
                callback && callback(_result)
            }
        })
    }

    updateUserInfo(userInfo: any, callback: Function) {
        this.serviceConf.httpPost(this.serviceConf.getUserInfoUpdateUrl(), userInfo, res => {
            if (res.success) {
                this.storage.remove('userInfo')
            }
            callback(res);
        });
    }

    getContentUserReadLog(skipCount: number = 0, pageSize: number = 10, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getContentUserReadLog(skipCount, pageSize), callback);
    }

    pushFeedback(feedback: any, callback: Function) {
        this.serviceConf.httpPost(this.serviceConf.getUserfeedback(), feedback, callback);
    }
}