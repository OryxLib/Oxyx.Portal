import { NgModule } from '@angular/core';
import { CategoryDetail} from './categoryDetail';
import { IonicPageModule } from 'ionic-angular';

@NgModule({
  declarations: [
    CategoryDetail
  ],
  imports: [
    IonicPageModule.forChild(CategoryDetail),
  ],
  exports: [
    CategoryDetail
  ]
})
export class CategoryDetailPageModule { }