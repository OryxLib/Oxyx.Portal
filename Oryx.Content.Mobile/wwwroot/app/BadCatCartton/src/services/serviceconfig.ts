import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Storage } from '@ionic/storage';
import { infoSetCheckUpdate } from '../pages/info/infoSet/infoSetIncludes/checkUpdate/infoSetCheckUpdate';

@Injectable()
export class ServiceConfig {

    domainUrl: string = window.location.hostname.indexOf('localhost') > -1 ? "http://localhost:39499" : "https://cartoon.oryxl.com" || "https://cartoon.oryxl.com";
    wsDomainUrl: string = "ws://localhost:39499"|| "ws://localhost:39499" || "wss://cartoon.oryxl.com";
    Wx = {
        CiYuanYa: "wx568e40eaad7d6736",
        OpenPlatform: "wxce16efce5d71abb8"
    }
    WB = {
        CiYuanYa: "3303196495"
    }
    MD5 = "8D:32:19:54:48:0C:99:8F:34:86:22:41:75:1D:DC:A7"
    constructor(public httpClient: HttpClient,
        public storage: Storage) {

    }

    httpGet(url: string, callback: Function) {
        console.log('get ')
        this.storage.get("AccessToken").then(accessToken => {
            this.httpClient.get(url, {
                headers: {
                    "AccessToken": accessToken || ''
                },
                withCredentials: true
            })
                .subscribe((res: any) => {
                    console.log(res)
                    callback && callback(res)
                }, err => {
                    callback && callback({ Success: false, Message: err.message })
                })
        })
    }

    httpPost(url: string, data: any, callback: Function) {
        this.storage.get("AccessToken").then(accessToken => {
            this.httpClient.post(url, data, {
                headers: {
                    "AccessToken": accessToken || ''
                },
                withCredentials: true
            }).subscribe((res: any) => {
                callback && callback(res)
            }, err => {
                callback && callback({ Success: false, Message: err.message })
            })
        })
    }


    /**Content */
    contentGetBannerUrl() {
        return this.domainUrl + "/Content/GetBanners";
    }

    contentGetNewestUrl() {
        return this.domainUrl + "/Content/GetNewest";
    }
    contentGetHotpotUrl() {
        return this.domainUrl + "/Content/GetHot";
    }
    contentGetRecommendUrl() {
        return this.domainUrl + "/Content/Recommand";
    }
    contentGetTopListUrl() {
        return this.domainUrl + "/Content/GetTopList"
    }
    contentGetAllCategoryUrl() {
        return this.domainUrl + "/Content/GetAllCategory";
    }
    contentGetCategoryByCateIdUrl(id: string) {
        return this.domainUrl + "/Content/GetCategoryByCateId?cid=" + id;
    }
    contentGetCategoryByTagNameUrl(tagName: string, pageIndex: Number, pageSize: Number) {
        return this.domainUrl + "/Content/GetCategoryByTagName?tagName=" + tagName + "&pageIndex=" + pageIndex + "&pageSize=" + pageSize
    }

    contentGetListByCatIdUrl(id: string) {
        return this.domainUrl + "/Content/GetContentList?Id=" + id;
    }
    contentGetDetailByIdUrl(id: string) {
        return this.domainUrl + "/Content/GetContentDetail?Id=" + id;
    }
    categoryPostCommentUrl() {
        return this.domainUrl + "/Content/PostCategoryComment"
    }
    cartegoryPostCommentSubComment() {
        return this.domainUrl + "/Content/PostCategoryCommentSubComment"
    }
    categoryListCommentUrl(cateId: string, skipCount: number, pageSize: number) {
        return this.domainUrl + "/Content/GetCategoryComment?cateId=" + cateId + "&skipCount=" + skipCount + "&pageSize=" + pageSize
    }
    contentPostCommentUrl() {
        return this.domainUrl + "/Contnet/PostComments";
    }
    contentListCommentUrl(id: string) {
        return this.domainUrl + "/Content/GetCommentsList?Id=" + id;
    }

    contentRecentCategoryByUser() {
        return this.domainUrl + "/Content/RecentCategory"
    }
    /**End Content */

    /**Social */
    getSocialChatSendTxtUrl() {
        return this.domainUrl + "/social/sendTxtMsg";
    }

    getSocialChatSocketUrl() {
        return this.wsDomainUrl + "/social";
    }
    getSocialChatListUrl(accessToken: string) {
        return this.domainUrl + "/social/chatList?accessToken=" + accessToken;
    }

    getSocialChatHistoryUrl(userId: string) {
        return this.domainUrl + "/social/chatHistory?toUserId=" + userId;
    }

    getUnReadMsgNumUrl() {
        return this.domainUrl + "/social/GetUnReadMsg"
    }

    getSocialInfoUrl(userId: string) {
        return this.domainUrl + "/social/GetUserSocialInfo?connectorUserId=" + userId;
    }

    getUserRelationShipInfo(userId?: string) {
        var param = '';
        if (userId) {
            param += 'userId=' + userId
        }
        return this.domainUrl + "/social/GetUserRelationShipInfo?" + param;
    }

    setSocialSubscrib(userId: string, isSubscibe: boolean) {
        return this.domainUrl + "/social/subscribUser?connectorUserId=" + userId + "&isSubscibe=" + isSubscibe
    }

    //查询粉丝
    getSocialFans(userId?: string, skipCount: number = 0, pageSize: number = 10) {
        var param = [];
        if (userId) {
            param.push('userId=' + userId)
        }
        if (skipCount) {
            param.push('skipCount=' + skipCount)
        }
        if (pageSize) {
            param.push('pageSize=' + pageSize)
        }
        var paramStr = param.join('&')
        return this.domainUrl + "/social/GetFansByUser?" + paramStr
    }

    //查询关注者
    getSocialSubscribe(userId?: string, skipCount: number = 0, pageSize: number = 10) {
        var param = [];
        if (userId) {
            param.push('userId=' + userId)
        }
        if (skipCount) {
            param.push('skipCount=' + skipCount)
        }
        if (pageSize) {
            param.push('pageSize=' + pageSize)
        }

        var paramStr = param.join('&')
        return this.domainUrl + "/social/GetSubscribByUser?" + paramStr
    }

    getSocialPostEntryPostUrl() {
        return this.domainUrl + "/PostEntry/CreatEntry";
    }

    getSocialPostEntryListUrl(startTimeStamp: number, endTiemStamp: number) {
        return this.domainUrl + "/postEntry/postEntryList?startTimeStamp=" + startTimeStamp + "&endTiemStamp=" + endTiemStamp;
    }

    getSocialPostEntryListWithPageSizeUrl(startTimeStamp: number, pageSize: number) {
        return this.domainUrl + "/postEntry/postEntryList?startTimeStamp=" + startTimeStamp + "&pageSize=" + pageSize;
    }

    getSocialPostEntryListByUserUrl(skipCount: number = 0, pageSize: number = 10) {
        return this.domainUrl + "/PostEntry/PostEntryListByUserId?skipCount=" + skipCount + "&pageSize=" + pageSize;
    }

    getSocialPostEntryListByUserOfSocialUrl(userId: string, skipCount: number = 0, pageSize: number = 10) {
        return this.domainUrl + "/PostEntry/PostEntryListByUserId?userId=" + userId + "&&skipCount=" + skipCount + "&pageSize=" + pageSize;
    }

    getSocialPostEntryDetailUrl(id: string) {
        return this.domainUrl + "/postEntry/PostEntryDetail?Id=" + id;
    }

    /**点赞 */
    getSocialPostEntryLikeUrl(postEntryId) {
        return this.domainUrl + "/postEntry/PostEntryLiked?postEntryId=" + postEntryId;
    }

    /**添加发帖的评论 */
    getSocialPostEntryAddCommentUrl() {
        return this.domainUrl + "/postEntry/AddPostEntryComment"
    }

    /**发帖评论 */
    getSocialPostEntryCommentUrl() {
        return this.domainUrl + "/postEntry/postEntryComment";
    }

    /**发帖评论 列表 */
    getSocialPostEntryCommentsListUrl(postEntryId: string, skipCount: number, pageSize: number) {
        return this.domainUrl + "/postEntry/GetPostEntryComments?postEntryId=" + postEntryId + "&skipCount=" + skipCount + "&pageSize=" + pageSize;
    }

    /**删除自己的帖子 */
    getSocialPostEntryDeleteUrl() {
        return this.domainUrl + "/postEntry/postEntryDelete";
    }

    getPostEntryTopicUrl(topicId: string) {
        return this.domainUrl + "/postEntry/TopicInfo?topicId=" + topicId;
    }

    postEntryPostCommentUrl() {
        return this.domainUrl + "/postEntry/PostEntryComment";
    }

    postEntryCommentSubComment() {
        return this.domainUrl + "/postEntry/PostEntryCommentSubComment";
    }


    getSocialPostEntryCommentDeleteUrl() {
        return this.domainUrl + "/postEntry/postEntryCommentDelete";
    }

    getPostEntryCommentsByUserSelf(skipCount: number, pageSize: number) {
        return this.domainUrl + "/postEntry/getpostEntryCommentsByUser?skiCount=" + skipCount + "&pageSize=" + pageSize
    }
    getPostEntryCommentsByUser(userId: string, skipCount: number, pageSize: number) {
        return this.domainUrl + "/postEntry/getPostEntryCommentsByUser?userId=" + userId + "&skiCount=" + skipCount + "&pageSize=" + pageSize
    }

    getSocialInfoListUrl(queryKey: string) {
        return this.domainUrl + "/social/userList?queryKey=" + queryKey
    }
    getPostEntryTopicInfoUrl(topicName, skipCount, pageSize) {
        return this.domainUrl + "/postEntry/TopicInfo?topicName=" + topicName + "&skipCount=" + skipCount + "&pageSize=" + pageSize
    }
    /**End Social */

    /**Info */
    getUserInfoUrl() {
        return this.domainUrl + "/info/detailInfo";
    }
    getOtherUserInfoUrl(userId) {
        return this.domainUrl + "/info/detailInfo?userId=" + userId;
    }
    getUserInfoUpdateUrl() {
        return this.domainUrl + "/info/updateInfo";
    }
    getContentUserReadLog(skipCount: number = 0, pageSize: number = 10) {
        return this.domainUrl + "/Content/GetContentUserReadLog?skipCount=" + skipCount + "&pageSize=" + pageSize
    }

    getContentUserReadLogOfSocialUrl(userId: string, skipCount: number = 0, pageSize: number = 10) {
        return this.domainUrl + "/Content/GetContentUserReadLog?userId=" + userId + "&skipCount=" + skipCount + "&pageSize=" + pageSize
    }

    getUserfeedback() {
        return this.domainUrl + "/info/PushFeedback"
    }
    /**End Info */

    /**Common */
    getPicToken() {
        return this.domainUrl + "/Pic/Token";
    }
    /**End Common */

    /**Pay  */
    getCreateOrderUrl() {
        return this.domainUrl + "/Pay/CreateOrder";
    }

    getWxPayUrl() {
        return this.domainUrl + "/Pay";
    }

    getQRCodePay() {
        return this.domainUrl + "/QRCodeNotify/GetImg"
    }

    getOrderListUrl() {
        return this.domainUrl + "/Pay/OrderList";
    }
    /**End Pay  */

    /**Auth */
    getHasLoginUrl() {
        return this.domainUrl + "/Auth/ApiHasLogin";
    }

    getLogoutUserUrl() {
        return this.domainUrl + "/Auth/ApiLogout";
    }

    getChangePasswordUrl() {
        return this.domainUrl + "/Auth/ApiChangePassword";
    }

    getRegisterUserUrl() {
        return this.domainUrl + "/Auth/ApiRegister";
    }

    getLoginUserUrl() {
        return this.domainUrl + "/Auth/ApiLogin";
    }

    getAuthOldPwd() {
        return this.domainUrl + "/Auth/ApiCheckOldPassword"
    }

    getNewsUrl(skipCount: Number, pageSize: Number) {
        return this.domainUrl + "/Content/GetNews?skipCount=" + skipCount + "&pageSize=" + pageSize
    }
    getNewDetailUrl(id: string) {
        return this.domainUrl + "/Content/GetNewsDetail?id=" + id;
    }
}
