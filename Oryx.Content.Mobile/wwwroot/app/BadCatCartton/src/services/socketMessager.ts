import { ServiceConfig } from '../services/serviceconfig';
import { Injectable } from '@angular/core';

@Injectable()
export class socketMessage { 
    socket: WebSocket;
    userId: string;
    constructor(public config: ServiceConfig) { 
    }
    registerSocket(userId: string) {
        this.socket = new WebSocket(this.config.getSocialChatSocketUrl() + "?userId=" + userId);
        this.userId = userId;
    }
    heartBeat() {
        var mainScoket = this.socket;
        var socketUrl = this.config.getSocialChatSocketUrl() + '?userId=' + this.userId;
        setTimeout(function () {
            setInterval(function () {
                var socketStatus = mainScoket.readyState;
                console.log(mainScoket)
                switch (socketStatus) {
                    case 4:
                        mainScoket.send('heartbeat')
                        break;
                    default:
                        mainScoket = new WebSocket(socketUrl) 
                        break;
                }
            }, 55000)
        }, 55000)
    }
    getSocket(key: string) {
        return this.socket;
    }
    sendMessage(key: string) {
        this.socket.send(key);
    }

    onReciveMessage(callback: Function) {
        this.socket.onmessage = function (msg) {
            callback && callback(msg);
        }
    }

    onOpen(callback: Function) {
        this.socket.onopen = function (env) {
            callback && callback(env)
        }
    }

    onClose(callback: Function) {
        this.socket.onclose = function (env) {
            callback && callback(env)
        }
    }

    onError(callback: Function) {
        this.socket.onerror = function (env) {
            callback && callback(env)
        }
    }
}