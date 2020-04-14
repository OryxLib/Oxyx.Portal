import { Component } from '@angular/core';
import { NavController, NavParams, IonicPage, ModalController } from 'ionic-angular';
import { ContentService } from '../../../services/contentService'
import { ContentDetailPage } from '../detail/detail'
import { GlobalData } from '../../../services/globalData'
import { CommonService } from '../../../services/commonService';
import { Authentication } from '../../../services/authentication'
import { ModalTool } from '../../../lib/openModal'
/**Common */
import { LoginPage } from '../../../pages/common/login/login'

@IonicPage({
    name: 'category-detail',
    segment: 'categoryDetail/:id',
    defaultHistory: ['home']
})
@Component({
    selector: "category-detail-page",
    templateUrl: "categoryDetail.html"
})
export class CategoryDetail {
    constructor(public navParams: NavParams,
        public navCtrl: NavController,
        public globalData: GlobalData,
        public commonSrv: CommonService,
        public auth: Authentication,
        public modelCtrl: ModalController,
        public modalTool: ModalTool,
        public contentService: ContentService) {
        console.log('category-detail') 
        console.log(this.navParams.data)
        this.cateId = this.navParams.data.id;
    }
    cateId: any;
    contentList: any;
    userContentLog: any = [];
    contentTitle: any;
    tags: any;
    converImg: any;
    bdImg: any;
    components: any;
    cateoption = "category";
    commentList: any;
    txtComment: any;
    categoryCommentIndex: number = 0;
    categoryCommentPageSize: number = 10;
    replyTo: any;
    replyToItem: any;
    replyToSubCommentItem: any;
    isLogin = false;

    ionViewWillEnter() {
        this.contentService.getCategoryDetail(this.cateId, res => {
            var categoryInfo = res.data.category;
            var _userLog = res.data.userReadLog;
            this.contentTitle = categoryInfo.name;
            this.tags = categoryInfo.tags
            this.converImg = categoryInfo.mediaResource && categoryInfo.mediaResource[0] && categoryInfo.mediaResource[0].actualPath
            //this.bdImg = "{'background-image':url('https://mioto.milbit.com/cartoon/xing想事成（感性变态）/coverimg.jpg')}";
            this.bdImg = "{'background-color': 'lime','font-size': '20px','font-weight': 'bold'}"
            this.contentList = categoryInfo.contentList.sort((item1, item2) => {
                //return item1.order < item2.order
                if (item1.order < item2.order) {           // 按某种排序标准进行比较, a 小于 b
                    return -1;
                }
                if (item1.order > item2.order) {
                    return 1;
                }
            });
            this.contentList.forEach(item => {

                console.log(item)
                this.userContentLog[item.id] = _userLog&&_userLog.findIndex(x => x.contentEntryId == item.id) > -1
            })
            this.globalData.currentCategory = categoryInfo;
        })
        this.getComment();
        this.auth.isLogin(res => {
            if (res.success) {
                this.isLogin = true
            }
        })
    }

    login() {
        this.modalTool.openModal(this.modelCtrl, LoginPage, {
            isShowModal: true
        })
    }
    itemSelected(item) {
        this.navCtrl.push(ContentDetailPage, {
            id: item.id,
            title: this.contentTitle
        });
    }
    getComment() {
        this.contentService.getCategoryComment(this.cateId, this.categoryCommentIndex * this.categoryCommentPageSize, this.categoryCommentPageSize, res => {
            this.commentList = res.data
            console.log(res.data)
        })
    }
    writeComment() {
        if (this.replyToSubCommentItem) {
            this.contentService.postContentCommentsSubComment({
                PostId: this.replyToSubCommentItem.postEntryId,
                Content: this.txtComment,
                ParentCommentId: this.replyToSubCommentItem.id
            }, res => {
                console.log(res);
                this.getComment();
            })
        }
        else if (this.replyToItem) {
            this.contentService.postContentCommentsSubComment({
                PostId: this.replyToItem.id,
                Content: this.txtComment
            }, res => {
                console.log(res);
                this.getComment();
            })
        } else {
            this.contentService.postCategoryComments({
                Content: this.txtComment,
                CategoryId: this.cateId
            }, res => {
                console.log(res);
                this.getComment();
            })
            this.txtComment = ''
        }
    }
    goBack() {
        this.navCtrl.pop();
    }
    sharePage() {

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

    filterImgPath(imgSrc) {
        return this.commonSrv.imgSrcFilter(imgSrc);
    }
}