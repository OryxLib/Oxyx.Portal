import { Authentication } from './authentication'
import { ServiceConfig } from '../services/serviceconfig'
import { Injectable } from '@angular/core';

@Injectable()
export class Pay {
    authTool: Authentication;
    constructor(
        public auth: Authentication,
        public serviceConfig: ServiceConfig) { 
    }
    getOrderList() {
        this.auth.getUserInfo();
    }
    createOrder() {
        this.auth.getUserInfo();
    }
    checkOrder(orderId: string) {
        this.auth.getUserInfo();
    }

    getQRCode() {
      return  this.serviceConfig.getQRCodePay();
    }
}