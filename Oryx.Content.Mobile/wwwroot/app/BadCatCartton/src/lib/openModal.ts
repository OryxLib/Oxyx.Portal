import { NavController, ModalController, PageTransition, Animation, Events, DateTime } from 'ionic-angular';
import { Injectable } from '@angular/core';

@Injectable()
export class ModalTool {
    constructor() {
    }

    openModal(
        modelCtrl: ModalController,
        commponent: any, 
        data?: any,
        callback?: Function) {
        modelCtrl.config.setTransition('modal-right-in', ModalFromRightEnter);
        modelCtrl.config.setTransition('modal-right-leave', ModalFromRightLeave);

        var modal = modelCtrl.create(commponent, data, {
            enterAnimation: 'modal-right-in',
            leaveAnimation: 'modal-right-leave'
        });
        modal.onDidDismiss(_data => {
            callback && callback(_data)
        })
        return modal.present();
    }
}

export class ModalFromRightEnter extends PageTransition {
    public init() {
        super.init();
        const ele = this.enteringView.pageRef().nativeElement;
        const backdrop = new Animation(this.plt, ele.querySelector('ion-backdrop'));
        backdrop.beforeStyles({ 'z-index': 0, 'opacity': 0.3, 'visibility': 'visible' });
        const wrapper = new Animation(this.plt, ele.querySelector('.modal-wrapper'));
        wrapper.fromTo('transform', 'translateX(100%)', 'translateX(0%)');
        const contentWrapper = new Animation(this.plt, ele.querySelector('ion-content.content'));
        contentWrapper.beforeStyles({ 'width': '100%' });
        this
            .element(this.enteringView.pageRef())
            .duration(300)
            .easing('ease')
            .add(backdrop)
            .add(wrapper)
            .add(contentWrapper);
    }
}

export class ModalFromRightLeave extends PageTransition {
    public init() {
        super.init();
        const ele = this.leavingView.pageRef().nativeElement;
        const backdrop = new Animation(this.plt, ele.querySelector('ion-backdrop'));
        backdrop.beforeStyles({ 'visibility': 'hidden' });
        const wrapper = new Animation(this.plt, ele.querySelector('.modal-wrapper'));
        wrapper.fromTo('transform', 'translateX(0%)', 'translateX(100%)');
        this
            .element(this.leavingView.pageRef())
            .duration(300)
            .easing('ease')
            .add(backdrop)
            .add(wrapper);
    }
}
