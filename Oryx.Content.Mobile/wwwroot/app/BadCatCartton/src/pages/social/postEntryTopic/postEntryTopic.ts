import { Component } from '@angular/core';
import { NavController, NavParams, LoadingController, IonicPage } from 'ionic-angular';
import { ContentService } from '../../../services/contentService';
import { SocialService } from '../../../services/socialService'
import { GlobalData } from '../../../services/globalData'


@IonicPage({
    name: 'postentrytopic',
    segment: 'postentrytopic/:Id',
    defaultHistory: ['home']
})
@Component({
    selector: "postentry-topic-page",
    templateUrl: "postEntryTopic.html"
})
export class PostEntryTopicPage {
    topicTxt: string;
    topicInfoSkipcount = 0;
    topicInfoPagesize = 10;
    topicInfoItem: any;
    postEntryList: any;
    constructor(public navParams: NavParams,
        public navCtrl: NavController,
        public globalData: GlobalData,
        public socialService: SocialService,
        public contentService: ContentService) {
        console.log('category-detail')
        console.log(this.navCtrl)
        console.log(navParams.get)
        this.topicTxt = this.navParams.data.topicTxt;
    }

    ionViewWillEnter() {
        this.socialService.getPostTopicInfo(this.topicTxt, this.topicInfoSkipcount, this.topicInfoPagesize, res => {
            console.log(res)
            this.topicInfoItem = res.data;
            this.postEntryList = res.data.postEntryList
            // LinkedCartoonId: "f07a5471-17a5-4254-9807-68f108f7e19d"
            // LinkedCartoonName: "神奇见面礼"
            // NewsCount: 0
            // NewsList: []
            // PostEntryCount: 3
            // PostEntryList: []
            // TopicPosterAvatar: ""
            // TopicPosterId: "00000000-0000-0000-0000-000000000000"
            // TopicPosterName: "未知"
        })
    }

    goBack() {
        this.navCtrl.pop();
    }
}