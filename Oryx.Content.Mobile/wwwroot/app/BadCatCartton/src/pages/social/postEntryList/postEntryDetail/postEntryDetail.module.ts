import { NgModule } from '@angular/core';
import { PostEntryDetail } from './postEntryDetail';
import { IonicPageModule } from 'ionic-angular';

@NgModule({
    declarations: [
        PostEntryDetail
    ],
    imports: [
        IonicPageModule.forChild(PostEntryDetail),
    ],
    exports: [
        PostEntryDetail
    ]
})
export class PostEntryDetailPageModule { }