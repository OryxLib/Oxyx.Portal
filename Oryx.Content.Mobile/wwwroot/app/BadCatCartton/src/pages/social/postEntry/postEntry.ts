import { Component } from '@angular/core';
import { NavController, ViewController, LoadingController, AlertController, Events } from 'ionic-angular';
import { PostEntryService } from '../../../services/postEntryService';
import { CommonService } from '../../../services/commonService';

@Component({
    selector: "postEntry-create-page",
    templateUrl: "postEntry.html"
})
export class PostEntryCreatePage {
    selectedImgList: any = [];
    TextContent: string;
    loadingHanderl: any;
    alertHander: any;
    tags: any = [];
    topic: any = '';
    constructor(public navCtl: NavController,
        public events: Events,
        public loadingCtrl: LoadingController,
        public alertCtrl: AlertController,
        public viewCtrl: ViewController,
        public commonService: CommonService,
        public postEntryService: PostEntryService) {

    }
    dismiss() {
        this.viewCtrl.dismiss();
    }

    sendPostEntry() {
        this.presentLoading()
        var uploadImgList = [];
        var imgLength = this.selectedImgList.length;
        //upload img to qiniu
        if (this.selectedImgList && this.selectedImgList.length > 0) {
            this.selectedImgList.forEach(imgItem => {
                var key = 'userPostentry/' + Math.random() * Math.pow(10, 17) + '/'
                this.commonService.uploadQiniu(imgItem.fileBlob, key, res => {
                    console.log(res)
                    uploadImgList.push({
                        Type: 0,
                        ActualPath: 'https://mioto.milbit.com/' + res.key,
                        Name: res.key
                    })
                    if (--imgLength == 0) {
                        this.sendPostEntryModel(uploadImgList)
                    }
                }, err => { })
            });
        } else {
            this.sendPostEntryModel()
        }
    }

    sendPostEntryModel(postEntryFileList?) {
        this.postEntryService.sendPostEntry({
            TextContent: this.TextContent,
            PostEntryFileList: postEntryFileList,
            PostEntryTopic: this.topic
        }, res => {
            this.loadingHanderl.dismiss();
            this.presentAlert()
        })
    }
    onTagInputChange(event) {
        console.log(this.tags)
    }
    presentAlert() {
        this.alertHander = this.alertCtrl.create({
            title: '提示',
            subTitle: '发帖成功!',
            buttons: [{
                text: '确定',
                handler: () => {
                    this.TextContent = '';
                    this.selectedImgList = [];
                    this.viewCtrl.dismiss();
                    this.events.publish("postenryPublished")
                }
            }]
        });
        this.alertHander.present();
    }
    presentLoading() {
        this.loadingHanderl = this.loadingCtrl.create({
            content: '加载中...'
        });
        this.loadingHanderl.present();
    }

    removeImg(index) {
        this.selectedImgList.splice(index, 1);
    }

    fileSelected(event) {
        if (!event.target.value) {
            return;
        }
        for (const fileItemKey in event.target.files) {
            if (event.target.files.hasOwnProperty(fileItemKey)) {
                const fileItem = event.target.files[fileItemKey];
                this.commonService.converToBase64(fileItem, dataUrl => {
                    this.selectedImgList.push({
                        fileBlob: fileItem,
                        base64: dataUrl,
                        fileType: fileItem.type
                    })
                })
            }
        }
        event.target.value = ''
    }
}