import { Component, ViewChild } from '@angular/core';
import { Platform, Events, Tabs } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { ModalController } from 'ionic-angular';
import { AppCenterCrashes } from '@ionic-native/app-center-crashes';

import { JPush } from '@jiguang-ionic/jpush';

import { ChatListPage } from '../pages/social/chat/list/list'

import { Authentication } from '../services/authentication'

import { HomePage } from '../pages/home/home';
import { chatMessageManager } from '../services/socialChatMessageManager'
import { UserInfoService } from '../services/userInfoService'

/**Common */
import { LoginPage } from '../pages/common/login/login'
/**End Common */
/**Social */
import { SocialInfoHomePage } from '../pages/social/info/infoHome'
import { PostEntryListPage } from '../pages/social/postEntryList/postEntryList'

/**End Social */

/**Info */
import { InfoHomePage } from '../pages/info/infoHome/infoHome'
/**End Info */

import { TestJiGuangHomePage } from '../pages/testjiguang/home/home'

@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild('mainTab') mainTab: Tabs
  rootPage: any = HomePage;
  tab1: any = HomePage;
  tab2: any = PostEntryListPage;
  tab3: any;
  chatmsg: any;
  testJihugang: any = TestJiGuangHomePage
  msgNum: number = 0;

  /**jiguang msg */
  public registrationId: string;

  devicePlatform: string = "Android";
  sequence: number = 0;

  /**jiguang msg end*/
  constructor(platform: Platform,
    public chatMessageManager: chatMessageManager,
    public userInfoService: UserInfoService,
    public events: Events,
    public jpush: JPush,
    statusBar: StatusBar,
    splashScreen: SplashScreen,
    public auth: Authentication,
    public appCenterCrashes: AppCenterCrashes
  ) {
    try {
      this.appCenterCrashes.setEnabled(true).then(() => {
        this.appCenterCrashes.lastSessionCrashReport().then(report => {
          console.log('Crash report', report);
          window.Sentry.captureMessage(report);
        });
      }).catch(err => {
        console.log(err)
      });
    } catch (error) {

    }


    this.init();
    //延迟执行，等极光完全初始化
    setTimeout(() => {
      this.setAlias("Alias");

    }, 300)

    this.userInfo()

    events.subscribe("userLogined", () => {
      this.startChatSocket();
      this.mainTab._tabs[2].root = ChatListPage;
      this.mainTab._tabs[3].root = InfoHomePage;
      // this.mainTab.initPane()
      // this.mainTab.initTabs()
      this.mainTab.select(3)
      console.log(this.mainTab.selectedIndex)
      //this.mainTab._tabs[2]=this.tab3 


    })

    events.subscribe("userLogout", () => {
      this.mainTab._tabs[2].root = LoginPage;
      this.mainTab._tabs[3].root = LoginPage;
    })

    platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      statusBar.styleDefault();
      splashScreen.hide();
    });
  }

  init() {
    //初始化极光
    this.jpush.init();

    //收到通知时会触发该事件。
    document.addEventListener("jpush.receiveNotification", function (event) {
      // alert(JSON.stringify(event));
    }, false);

  }

  //绑定别名
  setAlias(Alias: string) {
    this.jpush.setAlias({ sequence: 0, alias: Alias }).then((res) => {
      //alert(JSON.stringify(res));
    }).catch((err) => {
      //alert(JSON.stringify(err));
    });
  }

  userInfo() {
    this.auth.isLogin(res => {
      console.log(res)
      if (res && res.success) {
        this.startChatSocket();
        this.tab3 = InfoHomePage;
        this.chatmsg = ChatListPage;
      } else {
        this.tab3 = LoginPage;
        this.chatmsg = LoginPage;
      }
    });
  }

  startChatSocket() {
    this.userInfoService.getUserInfo(res => {
      this.chatMessageManager.initialSocket(res.userId)
    })
  }
}

