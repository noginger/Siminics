using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Siminics.WebMain.Handler;

namespace Cee.Resources.Handler
{
    /// <summary>
    /// EditorUp 的摘要说明
    /// </summary>
    public class EditorUp : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.ContentType = "text/plain";

            //上传配置
            string pathbase = "/Upload/";                                                                    //保存路径
            int size = 10;           //文件大小限制,单位MB                                                                                                              //文件大小限制，单位MB
            string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };         //文件允许格式


            //上传图片
            Hashtable info = new Hashtable();
            Uploader up = new Uploader();
            info = up.upFile(context, pathbase, filetype, size, -1);                               //获取上传状态

            string title = up.getOtherInfo(context, "pictitle");                              //获取图片描述

            string oriName = up.getOtherInfo(context, "fileName");                //获取原始文件名

            string newurl = info["url"].ToString();

            HttpContext.Current.Response.Write("{'url':'" + newurl + "','title':'" + title + "','original':'" + oriName + "','state':'" + info["state"] + "'}");  //向浏览器返回数据json数据
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}