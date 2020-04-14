import { NgModule } from '@angular/core';
import { PostEntryTopicPage} from './postEntryTopic';
import { IonicPageModule } from 'ionic-angular';

@NgModule({
  declarations: [
    PostEntryTopicPage
  ],
  imports: [
    IonicPageModule.forChild(PostEntryTopicPage),
  ],
  exports: [
    PostEntryTopicPage
  ]
})
export class PostEntryTopicPageModule { }