// pages/yanzhi/yanzhi.js
const app = getApp();
Page({

  /**
   * 页面的初始数据
   */
  data: {
    src: '' //|| 'https://mioto.milbit.com/1530782192869.jpg'
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

  },

  /**
   * 生命周期函数--监听页面初次渲染完成
   */
  onReady: function () {

  },

  /**
   * 生命周期函数--监听页面显示
   */
  onShow: function () {

  },

  /**
   * 生命周期函数--监听页面隐藏
   */
  onHide: function () {

  },

  /**
   * 生命周期函数--监听页面卸载
   */
  onUnload: function () {

  },

  /**
   * 页面相关事件处理函数--监听用户下拉动作
   */
  onPullDownRefresh: function () {

  },

  /**
   * 页面上拉触底事件的处理函数
   */
  onReachBottom: function () {

  },

  /**
   * 用户点击右上角分享
   */
  onShareAppMessage: function () {

  },
  takePhoto() {
    const ctx = wx.createCameraContext()
    ctx.takePhoto({
      quality: 'high',
      success: (res) => {
        this.setData({
          src: res.tempImagePath
        })
      }
    })
  },
  clearSrc() {
    this.setData({
      src: ''
    })
  },
  uploadYanzhiPic(remoteUrl) {
    console.log("img url : " + remoteUrl)
    wx.request({
      url: app.globalData.baseUrl + '/Pic/Yanzhi',
      data: {
        'imgurl': remoteUrl,
        'token': app.globalData.token
      }, success: function (res) {
        console.log(res)
        wx.navigateTo({
          url: '/pages/yanzhiResult/yanzhiResult'
        })
      }
    })
  },
  sendImg: function () {
    var _this = this;
    wx.request({
      url: app.globalData.baseUrl + '/pic/token',
      success: function (res) {
        _this.uploadImg(_this.data.src, res.data.data)
      },
      error: function () { }
    })
  },
  uploadImg: function (tmpPath, token) {
    var _this = this;
    console.log('upload img')
    wx.uploadFile({
      url: 'https://upload-z1.qiniup.com' || 'https://up-z1.qiniu.com', //仅为示例，非真实的接口地址
      filePath: tmpPath,
      name: 'file',
      formData: {
        'token': token,
        //        'key': (new Date() - 0).toString() + app.globalData.userInfo.nickName + '.jpg'
        'key': (new Date() - 0).toString() + '.jpg'
      },
      success: function (res) {
        var data = res.data
        console.log(res)
        var resObj = JSON.parse(res.data);
        var key = resObj.key;
        var imgUrl = 'https://mioto.milbit.com/' + key;

        // var msgData = {
        //   nickName: app.globalData.userInfo.nickName,
        //   avarta: app.globalData.userInfo.avatarUrl,
        //   msg: imgUrl,
        //   msgType: 'img',
        //   date: new Date()
        // };
        _this.uploadYanzhiPic(imgUrl)

        // _this.setData({
        //   src: ''
        // })
      }, error: function (err) {
        console.log(err)
      },
      complete: function (res) {
        console.log(res)

      }
    })
  }
})