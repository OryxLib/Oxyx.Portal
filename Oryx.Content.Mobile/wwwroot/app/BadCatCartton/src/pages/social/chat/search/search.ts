import { socialMessage } from '../../../../services/social-message';
import { Component } from '@angular/core';
import { ViewController, NavController, NavParams, Events } from 'ionic-angular';
import { SocialService } from '../../../../services/socialService'

@Component({
    selector: "chat-search-page",
    templateUrl: "search.html"
})
export class ChatSearchPage {
    chatUserLsit: any;
    searchUserName: any;
    constructor(public navCtl: NavController,
        public socialService: SocialService,
        public viewCtrl: ViewController,
        public events: Events,
        navParams: NavParams) {

    }

    dismiss() {
        this.viewCtrl.dismiss();
    }

    onInput(event) {
        this.socialService.getUserListByQueryKey(this.searchUserName, res => {
            this.chatUserLsit = res.data;
        });
    }

    chatWith(item) {
        this.events.publish("chatWithUser", item)
    }
}