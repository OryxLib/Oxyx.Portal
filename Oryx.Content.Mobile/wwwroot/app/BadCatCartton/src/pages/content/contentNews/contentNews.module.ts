import { NgModule } from '@angular/core';
import { ContentNews} from './contentNews';
import { IonicPageModule } from 'ionic-angular';

@NgModule({
  declarations: [
    ContentNews
  ],
  imports: [
    IonicPageModule.forChild(ContentNews),
  ],
  exports: [
    ContentNews
  ]
})
export class ContentNewsPageModule { }