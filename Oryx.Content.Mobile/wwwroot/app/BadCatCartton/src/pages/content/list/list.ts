import { Component } from '@angular/core';
import { NavController, NavParams } from 'ionic-angular';
import { ContentService } from '../../../services/contentService'
import { CategoryDetail } from '../categoryDetail/categoryDetail'
import { CommonService } from '../../../services/commonService';

@Component({
    selector: "content-list-page",
    templateUrl: "list.html"
})
export class CategoryListPage {
    tagName: any = '';
    contentList: any;
    pageIndex: number = 1;
    pageSize: number = 12;
    pageAllCount: number = 0;
    get pageSum() {
        var divisor = this.pageAllCount / this.pageSize;
        divisor = Math.round(divisor);
        return divisor;
    }
    constructor(public navCtl: NavController,
        private navParams: NavParams,
        public commonSrv:CommonService,
        public contentService: ContentService) {
        this.tagName = this.navParams.data.tagName
    }

    ionViewWillEnter() {
        var that = this;
        this.contentService.getCategoryList(this.tagName, this.pageIndex - 1, this.pageSize, res => {
            // that.contentList = res.data;
            console.log(res.data)
            this.contentList = res.data.item1;
            this.pageAllCount = res.data.item2;
        });
    }
    gotoDetail(item) {
        console.log('goto cate detail ' + JSON.stringify(item))
        this.navCtl.push(CategoryDetail, {
            id: item.id
        });
    }
    changePage() {
        this.contentService.getCategoryList(this.tagName, this.pageIndex - 1, this.pageSize, res => {
            // that.contentList = res.data;
            console.log(res.data)
            this.contentList = res.data.item1;
        });
    }
    goPrev() {
        if (this.pageIndex >= 1) {
            this.pageIndex--
            this.contentService.getCategoryList(this.tagName, this.pageIndex - 1, this.pageSize, res => {
                // that.contentList = res.data;
                console.log(res.data)
                this.contentList = res.data.item1;
            });
        }
    }
    goNext() {
        if (this.pageIndex <= this.pageSum) {
            this.pageIndex++
            this.contentService.getCategoryList(this.tagName, this.pageIndex - 1, this.pageSize, res => {
                // that.contentList = res.data;
                console.log(res.data)
                this.contentList = res.data.item1;
            });
        }
    }
    filterImgPath(imgSrc) {
        return this.commonSrv.imgSrcFilter(imgSrc);
    }
}