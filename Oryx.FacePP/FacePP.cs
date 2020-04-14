using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;

namespace Oryx.FacePP
{
    public class FacePP
    {
        public static void CheckYanzhi(Stream imgStream)
        {
            Dictionary<string, object> verifyPostParameters = new Dictionary<string, object>();
            verifyPostParameters.Add("api_key", "7Dx83WTIIEmIcabdcO293Lo97cYg4zcG");
            verifyPostParameters.Add("api_secret", "NgH2sfXe3Pb_95AjXJXv4kmAZFgdv8u4");
            verifyPostParameters.Add("return_landmark", "1");
            verifyPostParameters.Add("return_attributes", "gender,age,smiling,headpose,facequality,blur,eyestatus,emotion,ethnicity,beauty,mouthstatus,eyegaze,skinstatus");
            //Bitmap bmp = new Bitmap("/tmp/1.jpg"); // 图片地址
            byte[] fileImage = new byte[imgStream.Length];
            //using (Stream stream1 = new MemoryStream())
            //{
            //    bmp.Save(stream1, ImageFormat.Jpeg);
            //    byte[] arr = new byte[stream1.Length];
            //    stream1.Position = 0;
            //    stream1.Read(arr, 0, (int)stream1.Length);
            //    stream1.Close();
            //    fileImage = arr;
            //}
            imgStream.ReadAsync(fileImage, 0, (int)imgStream.Length);
            //添加图片参数
            verifyPostParameters.Add("image_file", new HttpHelper4MultipartForm.FileParameter(fileImage, "1.jpg", "application/octet-stream"));
            HttpWebResponse verifyResponse = HttpHelper4MultipartForm.MultipartFormDataPost("https://api-cn.faceplusplus.com/facepp/v3/detect", "", verifyPostParameters);
        }

        public static string CheckYanzhi(Uri uri)
        {
            Dictionary<string, object> verifyPostParameters = new Dictionary<string, object>();
            verifyPostParameters.Add("api_key", "7Dx83WTIIEmIcabdcO293Lo97cYg4zcG");
            verifyPostParameters.Add("api_secret", "NgH2sfXe3Pb_95AjXJXv4kmAZFgdv8u4");
            verifyPostParameters.Add("return_landmark", "1");
            verifyPostParameters.Add("return_attributes", "gender,age,smiling,headpose,facequality,blur,eyestatus,emotion,ethnicity,beauty,mouthstatus,eyegaze,skinstatus");

            //添加图片参数
            verifyPostParameters.Add("image_url", uri.ToString());
            HttpWebResponse verifyResponse = HttpHelper4MultipartForm.MultipartFormDataPost("https://api-cn.faceplusplus.com/facepp/v3/detect", "", verifyPostParameters);
            var resStream = verifyResponse.GetResponseStream();
            var streamReader = new StreamReader(resStream);
            return streamReader.ReadToEnd();
        }
    }
}
