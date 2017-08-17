using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace Cee.Tools.IO
{
    public class FileDownLoad
    { 
        /// <summary>
        /// 文件下载处理
        /// </summary>
        /// <para>功能说明：文件下载类--不管是什么格式的文件,都能够弹出打开/保存窗口,</para>> 
        /// <param name="downFilePath">下载文件路径</param>
        /// <param name="resetFileName">重命名下载文件名，可为空</param>
        /// <returns></returns>
        public static bool ResponseFile(string downFilePath, string resetFileName)
        {
            //创建http请求、响应对象
            HttpResponse response = HttpContext.Current.Response;
            HttpRequest request = HttpContext.Current.Request;

            try
            {
                string tagetPath = HttpContext.Current.Server.MapPath(downFilePath);
                string fileName = Path.GetFileName(downFilePath);

                if (!string.IsNullOrEmpty(resetFileName))
                {
                    if (Path.GetExtension(resetFileName).Length == 0)
                        resetFileName += Path.GetExtension(downFilePath);
                }

                //FileUtils.Copy(fullPath, tagetPath);
                FileStream myFile = new FileStream(tagetPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    response.AddHeader("Accept-Ranges", "bytes");
                    response.Buffer = false;
                    long fileLength = myFile.Length;
                    long startBytes = 0L;
                    int pack = 0x2800;
                    if (request.Headers["Range"] != null)
                    {
                        response.StatusCode = 0xce;
                        startBytes = Convert.ToInt64(request.Headers["Range"].Split(new char[] { '=', '-' })[1]);
                    }
                    response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0L)
                    {
                        response.AddHeader("Content-Range",
                            string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1L, fileLength));
                    }
                    response.AddHeader("Connection", "Keep-Alive");
                    response.ContentType = "application/octet-stream";
                    response.AddHeader(
                        "Content-Disposition",
                        "attachment;filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = ((int)Math.Floor((decimal)((fileLength - startBytes) / pack))) + 1;
                    for (int i = 0; i < maxCount; i++)
                    {
                        if (response.IsClientConnected)
                        {
                            response.BinaryWrite(br.ReadBytes(pack));
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch (IOException oE)
                {
                    string str = oE.Message;
                    return false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
