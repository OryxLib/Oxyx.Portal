import { Component } from '@angular/core';
import { NavController, ViewController, LoadingController, AlertController, Events } from 'ionic-angular';
import { PostEntryService } from '../../../../services/postEntryService';
import { CommonService } from '../../../../services/commonService';

@Component({
    selector: "sandbox-detail-page",
    templateUrl: "sandboxDetail.html"
})
export class SandboxDetailPage {
    selectedImgList: any = [];
    TextContent: string;
    loadingHanderl: any;
    alertHander: any;
    tags: any = [];
    topic: any = []
    constructor(public navCtl: NavController,
        public events: Events,
        public loadingCtrl: LoadingController,
        public alertCtrl: AlertController,
        public viewCtrl: ViewController,
        public commonService: CommonService,
        public postEntryService: PostEntryService) {

    }
}