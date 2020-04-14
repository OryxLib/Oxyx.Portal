﻿using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using System;

namespace AliyunTest
{
    class Program
    {
        static String product = "Dysmsapi";//短信API产品名称
        static String domain = "dysmsapi.aliyuncs.com";//短信API产品域名
        static String accessId = "LTAIvJuUYWkTBZTG";
        static String accessSecret = "uNSjAEUYF8bGSTZYhn3xWOXRN9bnZ4";
        static String regionIdForPop = "cn-hangzhou";
        static void Main(string[] args)
        {


            DefaultProfile profile = DefaultProfile.GetProfile(regionIdForPop, accessId, accessSecret);
            profile.AddEndpoint(regionIdForPop, regionIdForPop, product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                //request.SignName = "上云预发测试";//"管理控制台中配置的短信签名（状态必须是验证通过）"
                //request.TemplateCode = "SMS_71130001";//管理控制台中配置的审核通过的短信模板的模板CODE（状态必须是验证通过）"
                //request.RecNum = "13567939485";//"接收号码，多个号码可以逗号分隔"
                //request.ParamString = "{\"name\":\"123\"}";//短信模板中的变量；数字需要转换为字符串；个人用户每个变量长度必须小于15个字符。"
                //SingleSendSmsResponse httpResponse = client.GetAcsResponse(request);
                request.PhoneNumbers = "18843406621";
                request.SignName = "李华明";
                request.TemplateCode = "SMS_140275003";
                request.TemplateParam = "{\"code\":\"dazhutizi\"}";
                request.OutId = "xxxxxxxx";
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                System.Console.WriteLine(sendSmsResponse.Message);

            }
            catch (ServerException e)
            {
                System.Console.WriteLine("ServerException");
            }
            catch (ClientException e)
            {
                System.Console.WriteLine("ClientException");
            }
        }
    }
}
