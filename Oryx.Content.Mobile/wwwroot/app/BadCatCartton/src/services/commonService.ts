import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServiceConfig } from '../services/serviceconfig';

@Injectable()
export class CommonService {
    constructor(public http: HttpClient,
        public serviceConf: ServiceConfig) {
    }
    getPicToken(callback: Function) {
        this.serviceConf.httpGet(this.serviceConf.getPicToken(), callback);
    }

    uploadQiniu(imgData, key, callback: Function, errCallback: Function) {
        this.getPicToken(res => {
            var formData = new FormData();
            formData.append('file', imgData)
            formData.append('token', res.data)
            formData.append('key', key + new Date().getTime() + '.jpg')
            this.http.post('https://upload-z1.qiniup.com', formData).subscribe(res => {
                callback(res)
            }, err => {
                errCallback(err)
                console.log('uploaderr' + err.message)
            })
        })
    }

    converToBase64(file, cb) {
        var fileReader = new FileReader();
        fileReader.onload = function (e) {
            cb && cb(e.target['result'])
        }
        fileReader.readAsDataURL(file);
    }

    imgSrcFilter(imgSrc) {
        if (!imgSrc) {
            return '';
        }
        //return imgSrc//.replace(/http\:\/\/i-1\.gumua\.com\//g, 'http://img.cartoon.oryxl.com/imgproxy/');

        switch (true) {
            case /i-1\.gumua\./g.test(imgSrc):
                return imgSrc.replace(/http\:\/\/i-1\.gumua\.com\//g, 'http://img.cartoon.oryxl.com/imgproxy/');
            //case /i-1\.gumua\.\d+\.com\//g.test(imgSrc):
            //    return imgSrc.replace(/http\:\/\/i-1\.gumua\.com\//g, 'http://img.cartoon.oryxl.com/imgproxy2/');
            case /i\.hamreus\.com/g.test(imgSrc):
                return imgSrc.replace(/https:\/\/i\.hamreus\.com/g, 'http://img.cartoon.oryxl.com/hamreus/');
            //return imgSrc.replace('?', '.webp?');
            case /http:\/\/img\.fox800\.xyz\//g.test(imgSrc):
                return imgSrc.replace(/http:\/\/img\.fox800\.xyz\//g, 'http://img.cartoon.oryxl.com/fox800/');
            case /http:\/\/tu\.ewmdc\.com\//g.test(imgSrc):
                return imgSrc.replace(/http:\/\/tu\.ewmdc\.com\//g, 'http://img.cartoon.oryxl.com/ewmdc/');
            default:
                return imgSrc;
        }
    }
}