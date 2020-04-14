import { Authentication } from './authentication'
import { socialMessage } from './social-message'
import { socketMessage } from './socketMessager'
import { chatMessageManager } from './socialChatMessageManager'
import { ServiceConfig } from '../services/serviceconfig';
import { Injectable } from '@angular/core';
import { flattenStyles } from '@angular/platform-browser/src/dom/dom_renderer';

@Injectable()
export class SocialService {
    authTool: Authentication;
    socketMgr: socketMessage;
    chatMsg: chatMessageManager;
    constructor(auth: Authentication,
        socketMgr: socketMessage,
        public serviceConf: ServiceConfig) {

        this.authTool = auth;
        this.socketMgr = socketMgr;
    }
    getNewPostEntry(startTimeStamp: number, endTiemStamp: number, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialPostEntryListUrl(startTimeStamp, endTiemStamp), callback);
    }

    getNewPostEntryWithPageSize(startTimeStamp: number, pageSize: number, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialPostEntryListWithPageSizeUrl(startTimeStamp, pageSize), callback);
    }

    getPostEntryDetail(postEntryId: string, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialPostEntryDetailUrl(postEntryId), callback);
    }

    getNewPostEntryByUser(skipCount: number = 0, pageSize: number = 10, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialPostEntryListByUserUrl(skipCount, pageSize), callback)
    }

    postNewEntry(data: any, callback: Function) {
        this.serviceConf.httpPost(this.serviceConf.getSocialPostEntryPostUrl(), data, callback);
    }
    likePostEntry(postEntryId: string, callback: Function) {
        this.authTool.isLogin(res => {
            if (res.success) {
                this.serviceConf.httpGet(this.serviceConf.getSocialPostEntryLikeUrl(postEntryId), res => {
                    if (res.success) {
                        callback({ success: true, msg: '关注成功' })
                    } else {
                        callback({ success: false, msg: '服务器异常，请重试', needLogin: false })
                    }
                });
            }
            else {
                callback({ success: false, msg: '请先登录', needLogin: true })
            }
        })

    }
    // postCommentPostEntry(comments: any, callback: Function) {
    //     this.serviceConf.httpPost(this.serviceConf.getSocialPostEntryCommentUrl(), comments, callback);
    // }

    postPostEntryComments(comments: any, callback: Function) {
        this.serviceConf.httpPost(this.serviceConf.postEntryPostCommentUrl(), comments, callback);
    }
    postPostEntryCommentsSubComment(comments: any, callback: Function) {
        this.serviceConf.httpPost(this.serviceConf.postEntryCommentSubComment(), comments, callback)
    }

    getCommentsPostEntry(postEntryId: string, skipCount: number, pageSize: number, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialPostEntryCommentsListUrl(postEntryId, skipCount, pageSize), callback);
    }

    getPostEntryCommentsByUsefSelf(skipCount: number, pageSize: number, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getPostEntryCommentsByUserSelf(skipCount, pageSize), callback);
    }

    getPostTopicInfo(topicId: string, skipCount: Number = 0, pageSize: Number = 10, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getPostEntryTopicInfoUrl(topicId, skipCount, pageSize), callback)
    }

    createChatComponent(callback: Function) {
        this.authTool.getUserInfo();
    }

    startMsg(userId) {
        this.socketMgr.registerSocket(userId)
        this.socketMgr.heartBeat();
    }

    sendSocketMessage(msg: socialMessage) {
        this.socketMgr.sendMessage(JSON.stringify(msg));
    }

    sendTxtMessage(msg: socialMessage, callback: Function) {
        this.serviceConf.httpPost(this.serviceConf.getSocialChatSendTxtUrl(), msg, callback)
    }

    recieveMessage(callback) {
        this.socketMgr.onReciveMessage(msgContent => {
            callback && callback(msgContent)
        })
    }
    getChatList(callback: Function) {
        this.authTool.isLogin(res => {
            if (res.success) {
                this.serviceConf.httpGet(this.serviceConf.getSocialChatListUrl(res.data), callback);
            }
        })
    }

    getUnReadMsgNum(callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getUnReadMsgNumUrl(), callback)
    }

    getHistoryMessage(userId: string, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialChatHistoryUrl(userId), callback)
    }

    getUserListByQueryKey(queryKey: string, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialInfoListUrl(queryKey), callback);
    }

    /**social user info  */
    getSocialUserInfo(userId: string, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialInfoUrl(userId), callback)
    }

    getNewPostEntryByUserOfSocial(userId: string, skipCount: number, pageSize: number, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialPostEntryListByUserOfSocialUrl(userId, skipCount, pageSize), callback)
    }

    getContentUserReadLogOfSocial(userId: string, skipCount: number, pageSize: number, callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getContentUserReadLogOfSocialUrl(userId, skipCount, pageSize), callback);
    }

    getUserRelationShipInfo(userId?: string, callback?: Function) {
        this.serviceConf.httpGet(this.serviceConf.getUserRelationShipInfo(userId), callback);
    }

    setSubscribe(userId: string, isSubscribe: boolean, callback: Function) {
        this.authTool.isLogin(res => {
            if (res.success) {
                this.serviceConf.httpGet(this.serviceConf.setSocialSubscrib(userId, isSubscribe), res => {
                    if (res.success) {
                        callback({ success: true, msg: '关注成功' })
                    } else {
                        callback({ success: false, msg: '服务器异常，请重试', needLogin: false })
                    }
                })
            } else {
                callback({ success: false, msg: '请先登录', needLogin: true })
            }
        })
    }

    getFansList(userId: string, skipCount: number = 0, pageSize: number = 10, callback?: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialFans(userId, skipCount, pageSize), callback)
    }

    getSubscribeList(userId: string, skipCount: number = 0, pageSize: number = 10, callback?: Function) {
        this.serviceConf.httpGet(this.serviceConf.getSocialSubscribe(userId, skipCount, pageSize), callback)
    }
}