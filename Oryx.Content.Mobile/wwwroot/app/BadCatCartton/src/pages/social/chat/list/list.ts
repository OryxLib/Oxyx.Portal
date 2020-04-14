import { Component } from '@angular/core';
import { ViewController, NavController, ModalController, PageTransition, Events, Animation } from 'ionic-angular';
import { SocialService } from '../../../../services/socialService'
import { ChatDetailPage } from '../detail/detail'
import { ChatSearchPage } from '../search/search'
import { chatMessageManager } from '../../../../services/socialChatMessageManager'
import { UserInfoService } from '../..//../../services/userInfoService'
import { SandboxListPage } from '../../sandbox/sandboxList'
import moment from "../../../../lib/moment.js"

@Component({
    selector: "chat-list-page",
    templateUrl: "list.html"
})
export class ChatListPage {
    chatList: any;
    queryUserList: any;
    searchUserName: any;
    filterUserList: any;
    selfUserId: any;
    sandBoxMsg: string;
    sandBoxMsgTimestamp: string;
    constructor(public navCtl: NavController,
        public modelCtrl: ModalController,
        public viewCtrl: ViewController,
        public events: Events,
        public chatMessageManager: chatMessageManager,
        public userInfoService: UserInfoService,
        public socialService: SocialService) {
        // modelCtrl.config.setTransition('modal-right-in', ModalFromRightEnter);
        // modelCtrl.config.setTransition('modal-right-leave', ModalFromRightLeave);
        events.subscribe('chatWithUser', res => {
            console.log(res)
            //this.chatWith(res.Id)
        })
    }


    dismiss() {
        this.viewCtrl.dismiss();
    }

    ionViewWillEnter() {
        this.userInfoService.getUserInfo(userInfo => {
            this.selfUserId = userInfo.userId
            this.socialService.getChatList(res => {
                this.chatList = res.data;
            });
        })
    }


    gotoSandbox() {
        this.navCtl.push(SandboxListPage)
    }

    onInput(event) {
        this.userInfoService.getUserInfo(userInfo => {
            var userId = userInfo.userId
            if (this.searchUserName) {
                this.socialService.getUserListByQueryKey(this.searchUserName, res => {
                    this.queryUserList = this.filterSelf(res.data, userId);
                });

                this.filterUserList = this.chatList && this.chatList.filter(x => {
                    var contactUserInfo = this.getAnoterUserInfo(x.userAccountCollectoin, userId)
                    return contactUserInfo.nickName.indexOf(this.searchUserName) > -1
                        && contactUserInfo.userId != userId
                })
            }
            else {
                this.queryUserList = []
                this.filterUserList = []
            }
        })
    }

    getAnoterUserInfo(list, userId) {

        return list.find(x => { return x.id != userId })
    }

    filterSelf(list, userId) {
        if (!list) return [];
        else if (list.length == 1) return list
        return list.filter(x => {
            return x.userAccountCollectoin[1].userId != userId;
        })
    }

    chatWithToUserInfo(userInfo) {
        this.navCtl.push(ChatDetailPage, { userInfo: userInfo.toUserInfo });
    }

    chatWith(userInfo) {
        this.navCtl.push(ChatDetailPage, { userInfo: userInfo });
    }

    updateDatetime(datetime) {
        if (!datetime) return '';
        return moment(datetime).utc().fromNow()
    }

    doInfinite(): Promise<any> {
        var that = this;
        return new Promise((resolve) => {

            resolve();
        })
    }
}

export class ModalFromRightEnter extends PageTransition {
    public init() {
        super.init();
        const ele = this.enteringView.pageRef().nativeElement;
        const backdrop = new Animation(this.plt, ele.querySelector('ion-backdrop'));
        backdrop.beforeStyles({ 'z-index': 0, 'opacity': 0.3, 'visibility': 'visible' });
        const wrapper = new Animation(this.plt, ele.querySelector('.modal-wrapper'));
        wrapper.fromTo('transform', 'translateX(100%)', 'translateX(0%)');
        const contentWrapper = new Animation(this.plt, ele.querySelector('ion-content.content'));
        contentWrapper.beforeStyles({ 'width': '100%' });
        this
            .element(this.enteringView.pageRef())
            .duration(300)
            .easing('ease')
            .add(backdrop)
            .add(wrapper)
            .add(contentWrapper);
    }
}

export class ModalFromRightLeave extends PageTransition {
    public init() {
        super.init();
        const ele = this.leavingView.pageRef().nativeElement;
        const backdrop = new Animation(this.plt, ele.querySelector('ion-backdrop'));
        backdrop.beforeStyles({ 'visibility': 'hidden' });
        const wrapper = new Animation(this.plt, ele.querySelector('.modal-wrapper'));
        wrapper.fromTo('transform', 'translateX(0%)', 'translateX(100%)');
        this
            .element(this.leavingView.pageRef())
            .duration(300)
            .easing('ease')
            .add(backdrop)
            .add(wrapper);
    }
}