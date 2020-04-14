import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ServiceConfig } from '../services/serviceconfig';

@Injectable()
export class PostEntryService {
    constructor(public httpClient: HttpClient,
        public serviceConfig: ServiceConfig) { }
    sendPostEntry(data: any, callback: Function) {
        this.serviceConfig.httpPost(this.serviceConfig.getSocialPostEntryPostUrl(), data, callback);
    }
}