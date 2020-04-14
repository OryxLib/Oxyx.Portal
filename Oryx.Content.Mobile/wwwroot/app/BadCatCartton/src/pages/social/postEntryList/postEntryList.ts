import { Component, ViewChild } from '@angular/core';
import { NavController, ModalController, PageTransition, Animation, Events, AlertController, Slides } from 'ionic-angular';
import { PostEntryCreatePage } from '../postEntry/postEntry'
import { PostEntryDetail } from './postEntryDetail/postEntryDetail'
import { PostEntryTopicPage } from '../postEntryTopic/postEntryTopic'
import { Authentication } from '../../../services/authentication'
import { LoginPage } from '../../common/login/login'
import { SocialService } from '../../../services/socialService'
import { ChatListPage } from '../../social/chat/list/list'
import { SocialInfoHomePage } from '../info/infoHome'
import { ModalTool } from '../../../lib/openModal'
import { UserInfoService } from '../../../services/userInfoService'
import { SandboxListPage } from '../sandbox/sandboxList'
import moment from "../../../lib/moment.js"

@Component({
    selector: "postEntry-list-page",
    templateUrl: "postEntryList.html"
})
export class PostEntryListPage {
    contentList: any;
    queryItems: string[];
    selfUserInfo: any;
    @ViewChild(Slides) slides: Slides;
    public selectedCategory: any;
    public categories: any;
    public showLeftButton: boolean;
    public showRightButton: boolean;
    slideTab = "推荐"
    showSearchContent: boolean = false;

    alertHander: any;
    constructor(public navCtl: NavController,
        public userInfoSrv: UserInfoService,
        public alertCtrl: AlertController,
        public events: Events,
        public auth: Authentication,
        public modalTool: ModalTool,
        public modelCtrl: ModalController,
        public socialService: SocialService) {
        modelCtrl.config.setTransition('modal-right-in', ModalFromRightEnter);
        modelCtrl.config.setTransition('modal-right-leave', ModalFromRightLeave);
        events.subscribe('postenryPublished', () => {
            this.loadPostEntryList();
        })
        this.userInfoSrv.getUserInfo(res => {
            this.selfUserInfo = res;
            this.initializeItems()
            this.initializeCategories()
        })
    }


    showSearch() {
        this.showSearchContent = !this.showSearchContent;
        if (this.showSearchContent) {
            this.selectedCategory = this.categories[0];
            this.slideTab = "搜索"
        } else {
            this.selectedCategory = this.categories[1];
            this.slideTab = "推荐"
        }
        var slidesInstance = this.slides._slides[0];
    }

    /**
     * scroll tabs
     */
    private initializeCategories(): void {
        this.categories = [
            {
                id: '220',
                name: '搜索'
            },
            {
                id: '222',
                name: '推荐'
            },
            {
                id: '223',
                name: 'Coser'
            },
            {
                id: '224',
                name: '声优'
            },
            {
                id: '225',
                name: '摄影'
            }
        ]
        // Select it by defaut
        this.selectedCategory = this.categories[1];

        // Check which arrows should be shown
        this.showLeftButton = false;
        this.showRightButton = this.categories.length > 3;
    }
    filterContent(content) {
        return content.replace(/\<a/g, '<div')
            .replace(/\<\/a\>/g, '</div>')
    }
    public filterData(category: number, index: number, swtichTxt: string): void {
        // Handle what to do when a category is selected 
        this.selectedCategory = category;
        this.slideTab = swtichTxt
    }

    // Method executed when the slides are changed
    public slideChanged(): void {
        let currentIndex = this.slides.getActiveIndex();
        this.showLeftButton = currentIndex !== 0;
        this.showRightButton = currentIndex !== Math.ceil(this.slides.length() / 3);
    }

    // Method that shows the next slide
    public slideNext(): void {
        this.slides.slideNext();
    }

    // Method that shows the previous slide
    public slidePrev(): void {
        this.slides.slidePrev();
    }
    /**
     *end scroll tabs
     */

    ionViewWillEnter() {
        this.loadPostEntryList();
    }
    initializeItems() {
        this.queryItems = [
            'Amsterdam',
            'Bogota'
        ];
    }

    getItems(ev: any) {
        this.initializeItems();
        // set val to the value of the searchbar
        const val = ev.target.value;

        // if the value is an empty string don't filter the items
        if (val && val.trim() != '') {
            this.queryItems = this.queryItems.filter((item) => {
                return (item.toLowerCase().indexOf(val.toLowerCase()) > -1);
            })
        }
    }

    loadPostEntryList() {
        var startTimeStamp = new Date().getTime();
        var endTimeStamp = 0;
        this.socialService.getNewPostEntry(startTimeStamp, endTimeStamp, res => {
            this.contentList = res.data;
            console.log(this.contentList)
        });
    }

    subscribe(userId) {
        this.socialService.setSubscribe(userId, false, res => {
            this.presentAlert(res.msg)
            if (res.needLogin) {
                this.openLoginModal();
            }
        });
    }

    likePost(postentryItem: any) {
        postentryItem.likeNum++;
        this.socialService.likePostEntry(postentryItem.id, res => {
            if (!res.success) {
                this.presentAlert(res.msg)
                postentryItem.likeNum--;
                if (res.needLogin) {
                    this.openLoginModal();
                }
            }
        });
    }

    goSocialInfo(userInfoItem) {
        this.modalTool.openModal(this.modelCtrl, SocialInfoHomePage, {
            id: userInfoItem.userInfo.id
        })
    }

    gotoDetail(postentryId: string) {
        this.navCtl.push(PostEntryDetail, { id: postentryId })
    }

    gotoTopic(topicTxt: string) {
        this.navCtl.push(PostEntryTopicPage, { topicTxt: topicTxt })
    }

    presentAlert(txt: string) {
        this.alertHander = this.alertCtrl.create({
            title: '提示',
            subTitle: txt,
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


    openModal() {
        this.auth.isLogin(res => {
            console.log(res)
            if (res.success) {
                this.openCreatePostModal();
            }
            else {
                this.openLoginModal();
            }
        })
    }

    openChat() {
        this.auth.isLogin(res => {
            console.log(res)
            if (res.success) {
                this.openChatModal();
            }
            else {
                this.openLoginModal();
            }
        })
    }

    openLoginModal() {
        var loginModal = this.modelCtrl.create(LoginPage, { isShowModal: true }, {
            enterAnimation: 'modal-right-in',
            leaveAnimation: 'modal-right-leave'
        });
        loginModal.present();
    }

    openChatModal() {
        var chatModal = this.modelCtrl.create(ChatListPage, {}, {
            enterAnimation: 'modal-right-in',
            leaveAnimation: 'modal-right-leave'
        });
        chatModal.present();
    }

    openCreatePostModal() {
        var createPageModal = this.modelCtrl.create(PostEntryCreatePage, {}, {
            enterAnimation: 'modal-right-in',
            leaveAnimation: 'modal-right-leave'
        });
        createPageModal.present();
    }

    updateDatetime(datetime) {
        return moment(datetime).utc().fromNow()
    }
    doRefresh(refresher) {
        console.log('Begin async operation', refresher);

        setTimeout(() => {
            console.log('Async operation has ended');
            refresher.complete();
        }, 2000);
    }
    postEntryIndex = 1;
    postEntryPagesize = 10;
    doInfinite(): Promise<any> {
        var lastItem;
        if (this.contentList) {
            lastItem = this.contentList[this.contentList.length - 1]
        }
        return new Promise((resolve) => {
            this.socialService.getNewPostEntryWithPageSize(lastItem.timeStamp, this.postEntryPagesize, res => {
                this.contentList = this.contentList.concat(res.data)
                this.postEntryIndex++;
                resolve();
            })
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
