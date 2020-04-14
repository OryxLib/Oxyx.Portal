import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';
import { SplashScreen } from '@ionic-native/splash-screen';
import { StatusBar } from '@ionic-native/status-bar';
import { HttpClientModule } from '@angular/common/http';
import { IonicStorageModule } from '@ionic/storage';
import { IonTagsInputModule } from "ionic-tags-input";
import { AppCenterCrashes } from '@ionic-native/app-center-crashes';
import moment from '../lib/moment.js'
import moment_zh_cn from '../lib/moment-zh-cn.js'

import { SocialSharing } from '@ionic-native/social-sharing'; 
import { JPush } from '@jiguang-ionic/jpush';

// import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
// import { TranslateHttpLoader } from '@ngx-translate/http-loader';
/**lib */
import { ModalTool } from '../lib/openModal'

/**chat service */
import { socialMessage } from '../services/social-message'
import { socketMessage } from '../services/socketMessager'
import { chatMessageManager } from '../services/socialChatMessageManager'
/**end chat service */

/**Services */
import { SocialService } from '../services/socialService'
import { CommonService } from '../services/commonService'
import { ServiceConfig } from '../services/serviceconfig'
import { Authentication } from '../services/authentication'
import { ContentService } from '../services/contentService'
import { GlobalData } from '../services/globalData'
import { PostEntryService } from '../services/postEntryService'
import { UserInfoService } from '../services/userInfoService'
/**End Services */

/**Common */
import { LoginPage } from '../pages/common/login/login'
import { RegisterPage } from '../pages/common/registerPage/registerPage'
/**End Common */

import { Pay } from '../services/pay'

import { MyApp } from './app.component';
import { HomePage } from '../pages/home/home';
/**Content */
import { ContentDetailPage } from '../pages/content/detail/detail'
import { ContentDetailPageModule } from '../pages/content/detail/detail.module'
import { CategoryListPage } from '../pages/content/list/list'
import { CategoryDetail } from '../pages/content/categoryDetail/categoryDetail'
import { CategoryDetailPageModule } from '../pages/content/categoryDetail/categoryDetail.module';
import { ContentNewsPageModule } from '../pages/content/contentNews/contentNews.module';
/**End Content */

/**Social */
import { ChatSearchPage } from '../pages/social/chat/search/search'
import { ChatListPage } from '../pages/social/chat/list/list'
import { ChatDetailPage } from '../pages/social/chat/detail/detail'
import { SocialInfoHomePage } from '../pages/social/info/infoHome'
import { PostEntryCreatePage } from '../pages/social/postEntry/postEntry'
import { PostEntryListPage } from '../pages/social/postEntryList/postEntryList'
import { PostEntryTopicPageModule } from '../pages/social/postEntryTopic/postEntryTopic.module'
import { PostEntryDetailPageModule } from '../../src/pages/social/postEntryList/postEntryDetail/postEntryDetail.module'
import { SandboxListPage } from '../pages/social/sandbox/sandboxList'
import { SandboxDetailPageModule } from '../pages/social/sandbox/sandboxDetail/sandboxDetail.module'
/**End Social */

/**Info */
import { InfoHomePage } from '../pages/info/infoHome/infoHome'
import { InfoEditPage } from '../pages/info/infoEdit/infoEdit'
import { HomePageModule } from '../pages/home/home.module';
import { infoSetMain } from '../pages/info/infoSet/infoSetMain';
import { infoSetChangePassword } from '../pages/info/infoSet/infoSetIncludes/changePassword/infoSetChangePassword';
import { infoSetCheckUpdate } from '../pages/info/infoSet/infoSetIncludes/checkUpdate/infoSetCheckUpdate';
import { infoSetFeedback } from '../pages/info/infoSet/infoSetIncludes/feedback/infoSetFeedback';
import { infoSetUserAgreement } from '../pages/info/infoSet/infoSetIncludes/userAgreement/infoSetUserAgreement';
/**End Info */

import { TestJiGuangHomePage } from "../pages/testjiguang/home/home"
import { Device } from "@ionic-native/device";
import { WechatService } from '../services/wechatService'

@NgModule({
  declarations: [
    MyApp,
    CategoryListPage,
    SocialInfoHomePage,
    ChatListPage,
    ChatSearchPage,
    ChatDetailPage,
    PostEntryCreatePage,
    PostEntryListPage,
    InfoHomePage,
    InfoEditPage,
    LoginPage,
    RegisterPage,
    TestJiGuangHomePage,
    infoSetMain,
    infoSetChangePassword,
    infoSetCheckUpdate,
    infoSetFeedback,
    infoSetUserAgreement,
    SandboxListPage
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot(MyApp, {
      tabsHideOnSubPages: true,
      mode: 'ios'
    }),
    IonicStorageModule.forRoot(),
    HttpClientModule,
    IonTagsInputModule,
    HomePageModule,
    CategoryDetailPageModule,
    ContentDetailPageModule,
    PostEntryTopicPageModule,
    PostEntryDetailPageModule,
    SandboxDetailPageModule,
    ContentNewsPageModule
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    CategoryListPage,
    SocialInfoHomePage,
    ChatSearchPage,
    ChatListPage,
    ChatDetailPage,
    PostEntryCreatePage,
    PostEntryListPage,
    InfoHomePage,
    InfoEditPage,
    LoginPage,
    RegisterPage,
    TestJiGuangHomePage,
    infoSetMain,
    infoSetChangePassword,
    infoSetCheckUpdate,
    infoSetFeedback,
    infoSetUserAgreement,
    SandboxListPage
  ],
  providers: [
    StatusBar,
    SplashScreen,
    { provide: ErrorHandler, useClass: IonicErrorHandler },
    CommonService,
    Authentication,
    ServiceConfig,
    ContentService,
    PostEntryService,
    GlobalData,
    SocialService,
    socketMessage,
    socialMessage,
    chatMessageManager,
    UserInfoService,
    moment,
    // moment_zh_cn,
    ModalTool,
    Pay,
    JPush,
    Device,
    WechatService,
    AppCenterCrashes,
    SocialSharing
  ]
})
export class AppModule { }
