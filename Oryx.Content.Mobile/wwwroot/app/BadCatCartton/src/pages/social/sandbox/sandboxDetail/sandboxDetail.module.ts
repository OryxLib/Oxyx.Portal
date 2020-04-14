import { NgModule } from '@angular/core';
import { SandboxDetailPage } from './sandboxDetail';
import { IonicPageModule } from 'ionic-angular';

@NgModule({
    declarations: [
        SandboxDetailPage
    ],
    imports: [
        IonicPageModule.forChild(SandboxDetailPage),
    ],
    exports: [
        SandboxDetailPage
    ]
})
export class SandboxDetailPageModule { }