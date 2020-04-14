import { socialMessage } from './social-message'
import { Events } from 'ionic-angular';
import { Injectable } from '@angular/core';
import { SocialService } from '../services/socialService'

@Injectable()
export class chatMessageManager {
    chatMsgTableStorage: any = {}
    currentToUserId: any;
    unReadMsgNum: number;
    constructor(public events: Events,
        public socialService: SocialService) {

    }


    initialSocket(userId) {
        this.socialService.startMsg(userId)
        this.socialService.recieveMessage(res => {
            var socialMsg = new socialMessage();
            socialMsg.fromId = res.fromUserId
            socialMsg.toId = res.toUserId
            socialMsg.content = res.messageContent
            socialMsg.type = res.msgType
            this.pushMessage(res.data);
            this.events.publish("newMsg", socialMsg)
            if (res.data.toId != this.currentToUserId) {
                this.unReadMsgNum++;
                this.events.publish('updateUnReadNum', this.unReadMsgNum)
            }
        })
        this.socialService.getHistoryMessage(userId, res => {
            res.data && res.data.forEach(msgItem => {
                this.pushMessage(msgItem)
            });;
        });
    }
    pushMessage(msg: socialMessage) {
        if (!this.chatMsgTableStorage[msg.toId]) {
            this.chatMsgTableStorage[msg.toId] = {
                fromId: msg.fromId,
                toId: msg.toId,
                content: [],
                isRead: true
            }
        }
        this.chatMsgTableStorage[msg.toId].content.push({
            fromId: msg.fromId,
            toId: msg.toId,
            content: msg.content,
            timestamp: msg.timestamp
        })
    }
    getMsgSnapchat() {
        var snapChatMsgList = [];
        if (this.chatMsgTableStorage && this.chatMsgTableStorage.length > 0) {
            for (const key in this.chatMsgTableStorage) {
                if (this.chatMsgTableStorage.hasOwnProperty(key)) {
                    const msgItem = this.chatMsgTableStorage[key];
                    snapChatMsgList.push(msgItem)
                }
            }
        }
        return snapChatMsgList;
    }
    getMessage(toId: string) {
        if (this.chatMsgTableStorage[toId]) {
            return this.chatMsgTableStorage[toId];
        }
        return null;
    }
    updateMessage(msg: socialMessage, updateStatusCB: Function) {

    }
}