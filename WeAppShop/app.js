//app.js
App({
  onLaunch: function () {
    // var that = this;
    // //  获取商城名称
    // wx.request({
    //   url:  that.globalData.baseUrl + '/config/get-value',
    //   data: {
    //     key: 'mallName'
    //   },
    //   success: function (res) {
    //     if (res.data.code == 0) {
    //       wx.setStorageSync('mallName', res.data.data.value);
    //     }
    //   }
    // })
    // wx.request({
    //   url:  that.globalData.baseUrl + '/score/send/rule',
    //   data: {
    //     code: 'goodReputation'
    //   },
    //   success: function (res) {
    //     if (res.data.code == 0) {
    //       that.globalData.order_reputation_score = res.data.data[0].score;
    //     }
    //   }
    // })
    // wx.request({
    //   url:  that.globalData.baseUrl + '/config/get-value',
    //   data: {
    //     key: 'recharge_amount_min'
    //   },
    //   success: function (res) {
    //     if (res.data.code == 0) {
    //       that.globalData.recharge_amount_min = res.data.data.value;
    //     }
    //   }
    // })
    // // 获取砍价设置
    // wx.request({
    //   url:  that.globalData.baseUrl + '/shop/goods/kanjia/list',
    //   data: {},
    //   success: function (res) {
    //     if (res.data.code == 0) {
    //       that.globalData.kanjiaList = res.data.data.result;
    //     }
    //   }
    // })
    this.registerUser();
  },
  login: function () {
    var that = this;
    var token = that.globalData.token;
    if (token) {
      wx.request({
        url: that.globalData.baseUrl + '/user/check-token',
        data: {
          token: token
        },
        success: function (res) {
          if (res.data.code != 0) {
            that.globalData.token = null;
            that.login();
          }
        }
      })
      return;
    }
    wx.login({
      success: function (res) {
        wx.request({
          url: that.globalData.baseUrl + '/wxapp/login',
          data: {
            code: res.code
          },
          success: function (res) {
            // if (res.data.code == 10000) {
            //   // 去注册
            //   that.registerUser();
            //   return;
            // }
            // if (res.data.code != 0) {
            //   // 登录错误
            //   wx.hideLoading();
            //   wx.showModal({
            //     title: '提示',
            //     content: '无法登录，请重试',
            //     showCancel:false
            //   })
            //   return;
            // }
            //console.log(res.data.data)
            that.globalData.token = res.data;
            // that.globalData.uid = res.data.data.uid;
          }
        })
      }
    })
  },
  registerUser: function () {
    var that = this;
    wx.login({
      success: function (res) {
        var code = res.code; // 微信登录接口返回的 code 参数，下面注册接口需要用到
        wx.request({
          url: that.globalData.baseUrl + '/wxapp/login',
          data: {
            code: res.code
          },
          success: function (res) {
            that.globalData.token = res.data;


            wx.getUserInfo({
              success: function (_res) {
                console.log(_res)
                // 下面开始调用注册接口
                wx.request({
                  url: that.globalData.baseUrl + '/Customer/Register',
                  data: {
                    OpenId: that.globalData.token,
                    NickName: _res.userInfo.nickName,
                    Avatar: _res.userInfo.avatarUrl
                  }, // 设置请求的 参数
                  success: (__res) => {
                    wx.hideLoading();
                    //that.login();
                    that.globalData.UserCode = __res.data.data.userCode
                  }
                })
              }
            })
          }
        })

      }
    })
  },
  sendTempleMsg: function (orderId, trigger, template_id, form_id, page, postJsonString) {
    var that = this;
    wx.request({
      url: that.globalData.baseUrl + '/template-msg/put',
      method: 'POST',
      header: {
        'content-type': 'application/x-www-form-urlencoded'
      },
      data: {
        token: that.globalData.token,
        type: 0,
        module: 'order',
        business_id: orderId,
        trigger: trigger,
        template_id: template_id,
        form_id: form_id,
        url: page,
        postJsonString: postJsonString
      },
      success: (res) => {
        //console.log('*********************');
        //console.log(res.data);
        //console.log('*********************');
      }
    })
  },
  sendTempleMsgImmediately: function (template_id, form_id, page, postJsonString) {
    var that = this;
    wx.request({
      url: that.globalData.baseUrl + '/template-msg/put',
      method: 'POST',
      header: {
        'content-type': 'application/x-www-form-urlencoded'
      },
      data: {
        token: that.globalData.token,
        type: 0,
        module: 'immediately',
        template_id: template_id,
        form_id: form_id,
        url: page,
        postJsonString: postJsonString
      },
      success: (res) => {
        console.log(res.data);
      }
    })
  },
  getUserInfo: function (cb) {
    var that = this
    if (this.globalData.userInfo) {
      typeof cb == "function" && cb(this.globalData.userInfo)
    } else {
      //调用登陆接口
      wx.getUserInfo({
        success: function (res) {
          that.globalData.userInfo = res.userInfo
          typeof cb == "function" && cb(that.globalData.userInfo)
        }
      })
    }

  },
  globalData: {
    userInfo: null,
    baseUrl: 'http://haomianer.oryxl.com' ||'http://localhost:21762' ||  "",
    subDomain: "tz", // 如果你的域名是： https://api.it120.cc/abcd 那么这里只要填写 abcd
    version: "2.0",
    shareProfile: '靠颜值也可以养活自己啦' // 首页转发的时候话术
  }
  /*
  根据自己需要修改下单时候的模板消息内容设置，可增加关闭订单、收货时候模板消息提醒；
  1、/pages/to-pay-order/index.js 中已添加关闭订单、商家发货后提醒消费者；
  2、/pages/order-details/index.js 中已添加用户确认收货后提供用户参与评价；评价后提醒消费者好评奖励积分已到账；
  3、请自行修改上面几处的模板消息ID，参数为您自己的变量设置即可。  
   */
})
