import { Component } from '@angular/core';
import { NavController, NavParams, LoadingController, IonicPage } from 'ionic-angular';
import { ContentService } from '../../../services/contentService'
import { GlobalData } from '../../../services/globalData'
import { CommonService } from '../../../services/commonService';

@IonicPage({
    name: 'content-detail',
    segment: 'contentdetail/:id',
    defaultHistory: ['home', 'category-detail']
})
@Component({
    selector: "content-detail-page",
    templateUrl: "detail.html"
})
export class ContentDetailPage {
    categoryList = [];
    contentList: any;
    tmpcontentList: any;
    currentOrder: number;
    loadingHanderl: any;
    loadedNumber: number;
    loadIndex: number = 1;
    isLoading: boolean;
    contentTitle: any;
    loaded: boolean = false;
    detailTitle: string;
    constructor(private navParams: NavParams,
        public loadingCtrl: LoadingController,
        public navCtrl: NavController,
        public globalData: GlobalData,
        public commonSrv: CommonService,
        public contentService: ContentService) {
        console.log('content detail')
        console.log(this.navCtrl)

        //this.currentOrder = this.navParams.data.item.order;
        //this.contentList = navParams.data.item.MediaResource; 
        //this.setContentListNumber(this.contentList.length)
        console.log(this.navParams.data)
        this.getDetail(this.navParams.data.id);
        console.log(this.navCtrl.getByIndex(this.navCtrl.length() - 1))

    }

    getDetail(contentId, callback?) {
        this.contentService.getContentDetail(contentId, res => {
            this.navCtrl['_views'][this.navCtrl['_views'].length - 2].data['Id'] = res.category.id
            this.currentOrder = res.order;
            this.detailTitle = res.title;
            var _contentList = res.mediaResource.sort((item1, item2) => {
                if (item1.order && item2.order) {
                    var order1 = item1.order//parseInt(item1.ActualPath.split('/').reverse()[0].match(/\d+/g)[0])
                    var order2 = item2.order//parseInt(item2.ActualPath.split('/').reverse()[0].match(/\d+/g)[0])
                    //return item1.order < item2.order
                    if (order1 < order2) {           // 按某种排序标准进行比较, a 小于 b
                        return -1;
                    }
                    if (order1 > order2) {
                        return 1;
                    }
                } else {
                    var pathOrder1 = parseInt(item1.actualPath.split('/').reverse()[0].match(/\d+/g)[0]);
                    var pathOrder2 = parseInt(item2.actualPath.split('/').reverse()[0].match(/\d+/g)[0]);
                    if (pathOrder1 < pathOrder2) {           // 按某种排序标准进行比较, a 小于 b
                        return -1;
                    }
                    if (pathOrder1 > pathOrder2) {
                        return 1;
                    }
                }

            });
            this.contentList = _contentList;
            this.tmpcontentList = _contentList.slice(0, 2);
            this.setContentListNumber(this.contentList.length)
            this.categoryList.push({
                contentId,
                title: res.title,
                order: res.order,
                contentList: _contentList
            })
            callback && callback()
        })
    }

    ionViewWillEnter() {
        // var that = this;
        // this.contentService.getContentDetail(this.navParams.data.item.id, res => {
        //     that.contentList = res.data;
        // })
    }

    setContentListNumber(length: number) {
        this.loadedNumber = length;
        if (this.loadedNumber > 0) {
            this.loaded = true;
            this.loadingHanderl = this.loadingCtrl.create({
                content: '加载中...'
            });
            //this.loadingHanderl.present();
        } else {
            this.loaded = false
        }
    }

    imgLoaded(event) {
        console.log('img load')
        // console.log(this.loadedNumber)
        // console.log(event)
        if (--this.loadedNumber == 0) {
            this.loadingHanderl.dismiss();
            this.isLoading = false;
            this.loaded = true;
        }
        console.log('load index' + this.loadIndex)
        if (this.loadIndex < this.contentList.length - 1) {
            this.loadIndex++;
            var imgItem = this.contentList.slice(this.loadIndex, this.loadIndex + 1)
            imgItem && this.tmpcontentList.push(imgItem[0])
        }
    }

    openModal() {

    }

    doInfinite(): Promise<any> {
        var that = this;
        //console.log('Begin async operation'); 
        //console.log(this.navParams.data.item.order)
        //console.log(this.globalData)
        //this.globalData.currentCategory.ContentList[]

        return new Promise((resolve) => {
            if (this.isLoading) {
                resolve();
                return;
            }
            this.currentOrder++;
            this.isLoading = true;
            var nextItem = this.globalData.currentCategory.contentList.find(x => {
                return x.order == this.currentOrder
            })
            this.contentTitle = nextItem.title
            var _contentList = nextItem.mediaResource;
            this.currentOrder = nextItem.order;

            this.contentList = _contentList;

            // this.categoryList.push({
            //     contentId: nextItem.id,
            //     title: nextItem.Title,
            //     order: nextItem.order,
            //     contentList: _contentList
            // })
            this.getDetail(nextItem.id, () => {
                resolve();
            })
        })
    }


    filterImgPath(imgSrc) {
        return this.commonSrv.imgSrcFilter(imgSrc);
    }
}