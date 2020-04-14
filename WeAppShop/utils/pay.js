function wxpay(app, money, orderId, redirectUrl) {
  let remark = "在线充值";
  let nextAction = {};
  if (orderId != 0) {
    remark = "支付订单 ：" + orderId;
    nextAction = { type: 0, id: orderId };
  }
  wx.request({
    url: app.globalData.baseUrl + '/WxApp/UiniferOrder',
    data: {
      orderId: orderId
      // token:app.globalData.token,
      // money:money,
      // remark: remark,
      // payName:"在线支付",
      // nextAction: nextAction
    },
    //method:'POST',
    success: function (res) {
      if (res.data) {
        // 发起支付
        wx.requestPayment({
          timeStamp: res.data.timeStamp,
          nonceStr: res.data.nonceStr,
          package: res.data.package,
          signType: res.data.signType,
          paySign: res.data.paySign,
          fail: function (err) {
            wx.showToast({ title: '支付失败:' + err })
          },
          success: function () {
            wx.showToast({ title: '支付成功' })
            wx.redirectTo({
              url: redirectUrl
            });
          }
        })
      } else {
        wx.showToast({ title: '服务器忙' + res.data.code + res.data.msg })
      }
    }
  })
}

module.exports = {
  wxpay: wxpay
}
