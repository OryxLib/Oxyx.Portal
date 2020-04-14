import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServiceConfig } from '../services/serviceconfig';

@Injectable()
export class ContentService {
    constructor(public http: HttpClient,
        public serviceConf: ServiceConfig) {

    }

    getBanner(callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentGetBannerUrl(), callback);
    }

    getNewest(callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentGetNewestUrl(), callback);
    }

    getHotpot(callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentGetHotpotUrl(), callback);
    }

    getRecommend(callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentGetRecommendUrl(), callback);
    }

    getBannerList(callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentGetBannerUrl(), callback);
    }

    getTopList(callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentGetTopListUrl(), callback);
    }
    getContentList(cateId: string, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentGetListByCatIdUrl(cateId), callback);
    }
    getCategoryDetail(cateId: string, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentGetCategoryByCateIdUrl(cateId), callback);
    }

    getCategoryList(tagName: string, pageIndex: Number, pageSize: Number, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentGetCategoryByTagNameUrl(tagName, pageIndex, pageSize), callback)
    }

    getContentDetail(contentId: string, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentGetDetailByIdUrl(contentId), callback);
    }
    contentRecentCategoryByUser(callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentRecentCategoryByUser(), callback);
    }
    likeContent(contentId: string, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialPostEntryLikeUrl(contentId), callback);
    }

    postContentComments(comments: any, callback: Function) {
        this.serviceConf.httpPost(this.serviceConf.contentPostCommentUrl(), comments, callback);
    }
    postContentCommentsSubComment(comments: any, callback: Function) {
        this.serviceConf.httpPost(this.serviceConf.cartegoryPostCommentSubComment(), comments, callback)
    }
    getContentComments(contentId: string, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.contentListCommentUrl(contentId), callback);
    }

    getCategoryComment(cateId: string, skipCount: number, pageSize: number = 10, callback) {
        this.serviceConf.httpGet(this.serviceConf.categoryListCommentUrl(cateId, skipCount, pageSize), callback);
    }
    postCategoryComments(commentModel: any, callback: Function) {
        this.serviceConf.httpPost(this.serviceConf.categoryPostCommentUrl(), commentModel, callback);
    }

    getNews(skipCount: Number, pageSize: Number, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getNewsUrl(skipCount, pageSize), callback);
    }
    getNewsDetail(id: string, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getNewDetailUrl(id), callback);
    }
}