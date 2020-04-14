# Oryx.Shop

#### Description
dotnet core development 代码仓库
包含:
1. 小程序商城
2. 微信开放平台全功能代码实现 dotnet core 版
3. 基于phantomjs 的爬虫功能 dotnet core 版
4. 漫画社区源码
    包含: ionic angular4 跨平台端源码
          漫画服务端源码(dotnet core)
          漫画爬虫,可爬去spa应用
    功能: 漫画查看
          咨询查看
          内容评论
          漫画社区
          多端同步登陆
          即时聊天
5. 基于Redis 的dotnet core session 功能实现
6. 基于RebbitMQ 的websocket 功能实现
7. 微信支付功能实现
8. 旷视 dotnet core功能实现(含官方sdk)
9. 阿里云sdk 功能包装
10. 腾讯云sdk 功能包装



#### Software Architecture
软件架构

Oryx.Content.Admin 后台管理界面
Oryx.Content.Portal 前台界面
Oryx.Shop.BusinessApi 商城功能前端接口
Oryx.Shop 小程序商城功能封装

Oryx.Content.xxx 内容管理系统相关
Oryx.Cartoon.xxx 漫画功能相关
Oryx.Social.xxx 漫画社区,即时通讯相关项目
Social.WebSocket  分布式WebSocket  实现(Oryx.Websocket, RabbitMQ)

Oryx.DynamicPage.xxx 动态mvc 路由功能

Oryx.Spider.Standard[vxxx] 爬虫功能封装
Oryx.SpiderCartoon.Core.xxx 爬虫业务逻辑项目

Oryx.Websocket websocket 功能封装, 灵活可扩展

Oryx.Wx.xxx 微信开放平台功能封装

Oryx.UserAccount 用户功能项目

Oryx.Common  公共功能封装(文件上传,文件列表,对接七牛云)


#### Installation

1. Visual Studio >= 2017 
2. dotnet core >= 2.2
3. radis  若不需要可在starup中注释
4. rabbit MQ 若不需要可在 startup中注释
5. MySql >= 5.6

#### Instructions

1. 作为代码库使用,可以找到相关的代码片段.
2. 作为思路使用,可以找到相关的代码编写思路
3. 某些项目 如websocket 模块,可以重用,作为备用的代码仓库.

#### Contribution

1. No 
