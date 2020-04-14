import { Component } from '@angular/core';
import { NavController, ViewController, LoadingController, AlertController, Events } from 'ionic-angular';
import { PostEntryService } from '../../../services/postEntryService';
import { CommonService } from '../../../services/commonService';

@Component({
    selector: "sandbox-list-page",
    templateUrl: "sandboxList.html"
})
export class SandboxListPage {
    selectedImgList: any = [];
    TextContent: string;
    loadingHanderl: any;
    alertHander: any;
    tags: any = [];
    topic: any = []
    sandboxType: string = 'official'
    constructor(public navCtl: NavController,
        public events: Events,
        public loadingCtrl: LoadingController,
        public alertCtrl: AlertController,
        public viewCtrl: ViewController,
        public commonService: CommonService,
        public postEntryService: PostEntryService) {

    }
}