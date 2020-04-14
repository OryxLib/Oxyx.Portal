import { NgModule } from '@angular/core';
import { ContentDetailPage} from './detail';
import { IonicPageModule } from 'ionic-angular';

@NgModule({
  declarations: [
    ContentDetailPage
  ],
  imports: [
    IonicPageModule.forChild(ContentDetailPage),
  ],
  exports: [
    ContentDetailPage
  ]
})
export class ContentDetailPageModule { }