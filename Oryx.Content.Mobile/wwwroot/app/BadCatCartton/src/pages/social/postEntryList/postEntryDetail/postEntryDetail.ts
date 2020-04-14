import { Component } from '@angular/core';
import { NavController, NavParams, IonicPage, LoadingController } from 'ionic-angular';
import { ContentService } from '../../../../services/contentService'
import { GlobalData } from '../../../../services/globalData'
import { SocialService } from '../../../../services/socialService'
import moment from "../../../../lib/moment.js"
import { SocialSharing } from '@ionic-native/social-sharing';

@IonicPage({
    name: 'postentry-detail',
    segment: 'postentryDetail/:Id',
    defaultHistory: ['home']
})
@Component({
    selector: "postentry-detail-page",
    templateUrl: "postEntryDetail.html"
})
export class PostEntryDetail {
    constructor(public navParams: NavParams,
        public loadingCtrl: LoadingController,
        public navCtrl: NavController,
        public globalData: GlobalData,
        public socialSrv: SocialService,
        public socialSharing: SocialSharing,
        public contentService: ContentService) {
        this.postentryDetailId = this.navParams.data.id;
    }
    loadingHanderl: any;
    postentryDetailId: string;
    postentryDetailItem: any;
    postEntryCommentList: any = [];
    /**old */
    // cateId: any;
    // contentList: any;
    // contentTitle: any;
    // tags: any;
    // converImg: any;
    // bdImg: any;
    // components: any;
    // cateoption = "comment";
    // commentList: any;
    txtComment: any;
    categoryCommentIndex: number = 0;
    categoryCommentPageSize: number = 10;
    replyTo: any;
    replyToItem: any;
    replyToSubCommentItem: any;
    ionViewWillEnter() {
        this.socialSrv.getPostEntryDetail(this.postentryDetailId, res => {
            this.postentryDetailItem = res.data;
            this.postEntryCommentList = this.postentryDetailItem.postEntryCommentList
        })
        // this.contentService.getCategoryDetail(this.cateId, res => {
        //     var categoryInfo = res.Category;
        //     this.contentTitle = categoryInfo.Name;
        //     this.tags = categoryInfo.Tags
        //     this.converImg = categoryInfo.MediaResource && categoryInfo.MediaResource[0] && categoryInfo.MediaResource[0].ActualPath
        //     //this.bdImg = "{'background-image':url('https://mioto.milbit.com/cartoon/xing想事成（感性变态）/coverimg.jpg')}";
        //     this.bdImg = "{'background-color': 'lime','font-size': '20px','font-weight': 'bold'}"
        //     this.contentList = categoryInfo.ContentList.sort((item1, item2) => {
        //         //return item1.Order < item2.Order
        //         if (item1.Order < item2.Order) {           // 按某种排序标准进行比较, a 小于 b
        //             return -1;
        //         }
        //         if (item1.Order > item2.Order) {
        //             return 1;
        //         }
        //     });
        //     this.globalData.currentCategory = categoryInfo;
        // })
        // this.getComment();
    }

    // itemSelected(item) {
    //     this.navCtrl.push(ContentDetailPage, {
    //         Id: item.id,
    //         title: this.contentTitle
    //     });
    // }
    getComment() {
        console.log('----111')
        this.socialSrv.getCommentsPostEntry(this.postentryDetailId, this.categoryCommentIndex * this.categoryCommentPageSize, this.categoryCommentPageSize, res => {
            console.log('----')
            console.log(res.data)
            this.postEntryCommentList = res.data
        })
    }
    writeComment() {
        this.loadingHanderl = this.loadingCtrl.create({
            content: '发送中...'
        });
        this.loadingHanderl.present();

        if (this.replyToSubCommentItem) {
            this.socialSrv.postPostEntryCommentsSubComment({
                PostId: this.postentryDetailId,
                Content: this.txtComment,
                ParentCommentId: this.replyToSubCommentItem.id
            }, res => {
                this.getComment();
                this.txtComment = ''
                this.loadingHanderl.dismiss();
            })
        }
        else if (this.replyToItem) {
            this.socialSrv.postPostEntryCommentsSubComment({
                PostId: this.replyToItem.id,
                Content: this.txtComment,
            }, res => {
                this.getComment();
                this.txtComment = ''
                this.loadingHanderl.dismiss();
            })
        } else {
            this.socialSrv.postPostEntryComments({
                PostId: this.postentryDetailId,
                Content: this.txtComment,
            }, res => {
                console.log(res);
                this.getComment();
                this.txtComment = ''
                this.loadingHanderl.dismiss();
            })
        }
    }
    goBack() {
        this.navCtrl.pop();
    }
    sharePage() {
        this.socialSharing.share("帖子分享", "帖子主题", null, "https://mobile.17look.net")
    }
    gotoReplySubComment(subCommentItem: any) {
        this.replyToSubCommentItem = subCommentItem
        this.replyTo = subCommentItem.userAccount.nickName;
    }
    gotoReplyPostEntry(item: any) {
        this.replyToItem = item;
        this.replyTo = item.userAccountEntry.nickName;
    }

    closeReplyTo() {
        this.replyTo = ''
        this.txtComment = '';
        this.replyToItem = null;
    }

    filterContent(content) {
        return content.replace(/\<a/g, '<div')
            .replace(/\<\/a\>/g, '</div>')
    }
    updateDatetime(datetime) {
        return moment(datetime).utc().startOf('hour').fromNow()
    }
}