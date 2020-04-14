import { socialMessage, MessageType } from '../../../../services/social-message';
import { ViewChild, Component } from '@angular/core';
import { NavController, NavParams, Events, IonicPage } from 'ionic-angular';
import { SocialService } from '../../../../services/socialService'
import { UserInfoService } from '../../../../services/userInfoService'
import { Authentication } from '../../../../services/authentication'
import { chatMessageManager } from '../../../../services/socialChatMessageManager'
import moment from "../../../../lib/moment.js"
import moment_zh_cn from '../../../../lib/moment-zh-cn.js'


@Component({
    selector: "chat-detail-page",
    templateUrl: "detail.html",
})
export class ChatDetailPage {
    chatContentList: any = [];
    //currentUserInfo: any = {};
    //selfUserInfo: any = {};
    fromUserInfo: any;
    toUserInfo: any;
    txtMsg: any;
    chatMessageList: any = [];
    //userInfoCollection: any = {}
    @ViewChild('msgList') msgList: any;
    constructor(public navCtl: NavController,
        public socialService: SocialService,
        public userInfoService: UserInfoService,
        public events: Events,
        public chatMessageManager: chatMessageManager,
        public navParams: NavParams,
        public auth: Authentication) {
        //console.log(navParams)
        this.toUserInfo = navParams.data.userInfo
        this.userInfoService.getUserInfo(res => {
            this.fromUserInfo = res
        })
        this.socialService.getHistoryMessage(this.toUserInfo.id, res => {
            console.log(res)
            this.chatMessageList = res.data
        })
        // navParams.data.userCollection && navParams.data.userCollection.forEach(item => {
        //     this.userInfoCollection[item.id] = item
        // });
        //this.chatContentList = socialService.getChat(navParams.data.userId)
        //this.initialSocket();
        // this.auth.isLogin(res => {
        //     this.selfUserInfo.userId = res.data
        // })
    }
    ionViewWillEnter() {
        //this.chatMessageManager.getMsgSnapchat()
        //  this.chatContentList = this.chatMessageManager.getMessage(this.toUserInfo.userId)
    }

    // initialSocket() {
    //     this.socialService.startMsg(this.navParams.data.userId)
    //     this.socialService.recieveMessage(res => {
    //         this.chatMessageList.push({
    //             MessageContent: res.data
    //         })
    //         this.scorllToBottom();
    //     })
    // }
    scorllToBottom() {
        setTimeout(() => {
            let dimensions = this.msgList.getContentDimensions();
            this.msgList.scrollTo(0, dimensions.scrollHeight, 0);
        }, 300);
    }
    sendMsg() {
        var txtMsgPack = new socialMessage();
        txtMsgPack.content = this.txtMsg;
        txtMsgPack.fromId = this.fromUserInfo.userId;
        txtMsgPack.toId = this.toUserInfo.id;
        txtMsgPack.type = 1;
        this.chatMessageList.push(txtMsgPack)
        this.scorllToBottom();
        this.socialService.sendTxtMessage(txtMsgPack, res => {
            console.log(res)
            this.txtMsg = '';
        });
    }

    datetimeAnchor: any;
    showDateAnchor(datetime) {
        if (!this.datetimeAnchor) {
            this.datetimeAnchor = datetime
        }
        if (this.datetimeAnchor == datetime) {
            return true
        } else if (Math.abs(this.datetimeAnchor - datetime) > 5 * 1000 * 60) {
            this.datetimeAnchor = datetime
            return true
        }
        else {
            return false
        }
    }

    showDateTime(datetime) {
        moment_zh_cn
        moment.locale('zh-cn');
        return this.updateDatetime(datetime); 

        // switch (datetime) {
        //     case this.datetimeAnchor - datetime < 1 * 1000 * 60:
        //         return moment(datetime).utc().startOf('second').fromNow()
        //     case this.datetimeAnchor - datetime < 1 * 1000 * 60 * 60:
        //         return moment(datetime).utc().startOf('minute').fromNow()
        //     case this.datetimeAnchor - datetime < 1 * 1000 * 60 * 60 * 24:
        //         return moment(datetime).utc().startOf('hour').fromNow()
        //     default:
        //         return moment(datetime).utc().startOf('day').fromNow()
        // }
        //return moment(datetime).format('hh:mm')
    }

    updateDatetime(datetime) {
        return moment(datetime).utc().fromNow()
    }
    doInfinite() {

    }
}