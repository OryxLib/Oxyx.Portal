// pages/yanzhiResult/yanzhiResult.js
var action = require('../../utils/action.js')
var ctx;
Page({

  /**
   * 页面的初始数据
   */
  data: {
    score: 98,
    qrcodeUrl: 'https://mioto.milbit.com/shopping/qrcode.jpg',
    bgImgUrl: 'https://mioto.milbit.com/shopping/6.26%E8%8D%B7%E8%8A%B1.jpg'
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
  shareImg: function () {
    wx.showModal({
      title: '分享',
      content: '请在下载完成后, 分享到朋友圈',
      showCancel: false,
      mask: true,
      success: function (res) {
        if (res.confirm) {
          wx.showLoading({
            title: '处理中',
            mask: true,
            success: function (res) { },
            fail: function (res) { },
            complete: function (res) { },
          })
        }
      }
    })
    //ctx.setFillStyle('white');
    //ctx.fillRect(0, 0, 375, 592);
    var _this = this;
    var localBgPath = '', localQRCodePath = '';

    action.action(thread => {
      thread.run(seed => {
        _this.downloadImg(_this.data.bgImgUrl, res => {
          localBgPath = res.tempFilePath
          seed.end();
        })
      });
    })

    action.action(thread => {
      thread.run(seed => {
        _this.downloadImg(_this.data.qrcodeUrl, res => {
          localQRCodePath = res.tempFilePath
          seed.end();
        })
      });
    })

    action.action(thread => {
      thread.waitAll(seed => {
        _this.drawCanvas(ctx => {
          ctx.drawImage(localBgPath, -200, 0, 500, 395)
          ctx.drawImage(localQRCodePath, 205, 322, 70, 70)
          setTimeout(function () {
            wx.canvasToTempFilePath({
              canvasId: 'qrcode',
              success: res => {
                console.log(res)
                // wx.previewImage({
                //   urls: [res.tempFilePath],
                // })
                wx.saveImageToPhotosAlbum({
                  filePath: res.tempFilePath,
                })
                wx.hideLoading()
              }
            })
          }, 500)
        })
      })
    })

    // this.downloadImg(this.data.bgImgUrl, res => {
    //   localBgPath = res.tempFilePath
    //   _this.downloadImg(this.data.qrcodeUrl, res => {
    //     localQRCodePath = res.tempFilePath

    //   })
    // })


    // wx.downloadFile({
    //   url: _this.data.imgPath,
    //   success: function (res) {
    //     console.log(_this.data.txtIdea)
    //     ctx.beginPath();
    //     ctx.setFillStyle('black');
    //     //ctx.fillRect(0, 50, 375, 250);
    //     var offsetY = 0;
    //     var offsetX = 0;
    //     var scale = 0;
    //     var width = 0;
    //     var height = 0;
    //     wx.getImageInfo({
    //       src: res.tempFilePath,
    //       success: function (res) {
    //         console.log(res.width)
    //         console.log(res.height)
    //         width = res.width;
    //         height = res.height;
    //         scale = res.width / 600;
    //         if (res.width > res.height) {
    //           offsetX = res.width * scale - 600;
    //         } else if (res.height > res.width) {
    //           offsetY = res.height * scale - 600;
    //         }
    //       }
    //     })
    //     if (width < height) {
    //       ctx.drawImage(res.tempFilePath, 37.5, 70, 300, 300 / width * height);
    //     } else if (width > height) {
    //       ctx.drawImage(res.tempFilePath, 37.5, 70, 300 / height * width, 300);
    //     } else {
    //       ctx.drawImage(res.tempFilePath, 37.5, 70, 300, 300);
    //     }

    //     ctx.drawImage('/images/background.png', 0, 0, 375, 592)

    //     if (_this.data.hasCover == 'active') {
    //       ctx.drawImage(_this.data.rect1, 45, 90, 375 * 0.75, 214 * 0.75)
    //       ctx.drawImage(_this.data.rect2, 65, 100, 375 * 0.65, 214 * 0.65)
    //       ctx.beginPath();
    //       ctx.setFillStyle('#4c4c4c')
    //       ctx.setFontSize(36)
    //       ctx.fillText(_this.data.year, 100, 150);
    //       ctx.beginPath();
    //       ctx.setFillStyle('#4c4c4c')
    //       ctx.setFontSize(18)
    //       ctx.fillText(_this.data.fullyear, 140, 130);
    //       ctx.fillText(_this.data.weekday, 140, 150);
    //       ctx.fillText(_this.data.txtIdea, 100, 210);
    //       ctx.setFontSize(20)
    //     }

    //     ctx.fill();
    //     ctx.draw();
    //     wx.canvasToTempFilePath({
    //       canvasId: 'qrcode',
    //       success: res => {
    //         console.log(res)
    //         wx.previewImage({
    //           urls: [res.tempFilePath],
    //         })
    //         // wx.saveImageToPhotosAlbum({
    //         //   filePath: res.tempFilePath,
    //         // })
    //       }
    //     })
    //   }
    // }) 
  },
  drawCanvas: function (operateCB, cb) {
    if (!ctx)
      ctx = wx.createCanvasContext('qrcode', this);

    operateCB && operateCB(ctx)
    ctx.fill();
    ctx.draw();
    cb && cb()
  },
  downloadImg: function (imgUrl, successCB) {
    var _this = this;
    wx.downloadFile({
      url: imgUrl,
      header: {},
      success: function (res) {
        successCB && successCB(res)
      },
      fail: function (res) { },
      complete: function (res) { },
    })
  }
})