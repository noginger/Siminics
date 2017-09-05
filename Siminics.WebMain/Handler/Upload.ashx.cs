using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using BaseLibrary.Common;
using Cee.Tools.Web;
using Siminics.WebMain.Handler;

namespace Cee.Resources.Handler
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int uploadType = WebUtils.GetQueryInt("uptype", -1);

            string json = "([{{\"url\":\"{0}\",\"title\":\"{1}\",\"original\":\"{2}\",\"state\":\"{3}\"}}])";
            //上传配置
            string pathbase = "/Upload/";//保存路径
            int size = 50;           //文件大小限制,单位MB   
            string[] filetype = GetUploadAllowExtensions((EnumConst.UploadType)uploadType);  //文件允许格式
            if (filetype != null)
            {
                Hashtable info = new Hashtable();
                Uploader up = new Uploader();
                info = up.upFile(context, pathbase, filetype, size, uploadType); //获取上传状态
                //string title = up.getOtherInfo(context, "pictitle"); //获取图片描述
                //string oriName = up.getOtherInfo(context, "fileName"); //获取原始文件名
                context.Response.Write(string.Format(json, info["url"], "", "", info["state"]));
            }
            else
            {
                context.Response.Write(string.Format(json, string.Empty, string.Empty, string.Empty, "没有对应的配置", 0));
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public static string[] GetUploadAllowExtensions(EnumConst.UploadType uploadType)
        {
            string value = ConfigurationManager.AppSettings[uploadType.ToString()];
            if (!string.IsNullOrEmpty(value))
                return value.Split(',');
            else
                return null;
        }
    }
}