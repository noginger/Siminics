using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cee.Tools.IO
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileUtils
    {
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="absolutePath">文件的绝对路径</param>        
        public static bool Exists(string absolutePath)
        {
            return File.Exists(absolutePath);
        }

        /// <summary>
        /// //判断原始文件并创建目标目录
        /// </summary>
        /// <param name="sourceFileName">源文件路径</param>
        /// <param name="destFileName">目标路径</param>
        public static void ExistsAndCreateDir(string sourceFileName, string destFileName)
        {
            //文件是否存在
            if (!Exists(sourceFileName))
                return;

            //获取文件所在目录名
            string descDirPath = DirectoryUtils.GetDirNameFromFile(destFileName).FullName;

            DirectoryUtils.CreateDirectory(descDirPath);
        }

        /// <summary>
        /// 从文件的绝对路径中获取文件名( 包含扩展名 )
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>        
        public static string GetFileName(string filePath)
        {
            //获取文件的名称
            FileInfo finfo = new FileInfo(filePath);
            return finfo.Name;
        }

        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="absolutePath">文件的绝对路径</param>
        public static void Delete(string absolutePath)
        {
            File.Delete(absolutePath);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="sourceFileName"></param>
        /// <param name="destFileName"></param>
        public static void Move(string sourceFileName, string destFileName)
        {
            //判断原始文件并创建目标目录
            ExistsAndCreateDir(sourceFileName, destFileName);
            //移动文件到目录
            File.Move(sourceFileName, destFileName);
        }

        /// <summary>
        /// 拷贝文件(如果目标存在，不覆盖)
        /// </summary>
        /// <param name="sourceFileName">原来的路径</param>
        /// <param name="destFileName">需要挪到的新路径</param>
        public static void Copy(String sourceFileName, String destFileName)
        {
            //判断原始文件并创建目标目录
            ExistsAndCreateDir(sourceFileName, destFileName);
            //拷贝
            File.Copy(sourceFileName,destFileName);
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="sourceFileName">原来的路径</param>
        /// <param name="destFileName">需要挪到的新路径</param>
        /// <param name="overwrite">如果目标存在，是否覆盖</param>
        public static void Copy(String sourceFileName, String destFileName, Boolean overwrite)
        {
            //判断原始文件并创建目标目录
            ExistsAndCreateDir(sourceFileName, destFileName);
            //拷贝
            File.Copy(sourceFileName, destFileName, overwrite);
        }

        /// <summary>
        /// 读取文件的内容(采用UTF8编码)
        /// </summary>
        /// <param name="absolutePath">文件的绝对路径</param>
        /// <returns>文件的内容</returns>
        public static String Read(String absolutePath)
        {
            return Read(absolutePath, Encoding.UTF8);
        }

        /// <summary>
        /// 以某种编码方式，读取文件的内容
        /// </summary>
        /// <param name="absolutePath">文件的绝对路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>文件的内容</returns>
        public static String Read(String absolutePath, Encoding encoding)
        {
            using (StreamReader reader = new StreamReader(absolutePath, encoding))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 读取文件各行内容(采用UTF8编码)，以数组形式返回
        /// </summary>
        /// <param name="absolutePath">文件的绝对路径</param>
        /// <returns>文件各行内容</returns>
        public static String[] ReadAllLines(String absolutePath)
        {
            return ReadAllLines(absolutePath, Encoding.UTF8);
        }

        /// <summary>
        /// 以某种编码方式，读取文件各行内容(采用UTF8编码)，以数组形式返回
        /// </summary>
        /// <param name="absolutePath">文件的绝对路径</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>文件各行内容</returns>
        public static String[] ReadAllLines(String absolutePath, Encoding encoding)
        {
            ArrayList list = new ArrayList();
            using (StreamReader reader = new StreamReader(absolutePath, encoding))
            {
                String str;
                while ((str = reader.ReadLine()) != null)
                {
                    list.Add(str);
                }
            }
            return (String[])list.ToArray(typeof(String));
        }

        /// <summary>
        /// 将字符串写入某个文件中(采用UTF8编码)
        /// </summary>
        /// <param name="absolutePath">文件的绝对路径</param>
        /// <param name="fileContent">需要写入文件的字符串</param>
        public static void Write(String absolutePath, String fileContent)
        {
            Write(absolutePath, fileContent, Encoding.UTF8);
        }

        /// <summary>
        /// 将字符串写入某个文件中(需要指定文件编码方式)
        /// </summary>
        /// <param name="absolutePath">文件的绝对路径</param>
        /// <param name="fileContent">需要写入文件的字符串</param>
        /// <param name="encoding">编码方式</param>
        public static void Write(String absolutePath, String fileContent, Encoding encoding)
        {
            using (StreamWriter writer = new StreamWriter(absolutePath, false, encoding))
            {
                writer.Write(fileContent);
            }
        }

        /// <summary>
        /// 将内容追加到文件中(采用UTF8编码)
        /// </summary>
        /// <param name="absolutePath">文件的绝对路径</param>
        /// <param name="fileContent">需要追加的内容</param>
        public static void Append(String absolutePath, String fileContent)
        {
            Append(absolutePath, fileContent, Encoding.UTF8);
        }

        /// <summary>
        /// 将内容追加到文件中
        /// </summary>
        /// <param name="absolutePath">文件的绝对路径</param>
        /// <param name="fileContent">需要追加的内容</param>
        /// <param name="encoding">编码方式</param>
        public static void Append(String absolutePath, String fileContent, Encoding encoding)
        {
            using (StreamWriter writer = new StreamWriter(absolutePath, true, encoding))
            {
                writer.Write(fileContent);
            }
        }


        #region 格式化文件大小
        /// <summary>
        /// 格式化文件大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string FromatFileSize(long size)
        {
            string str;
            if (size < 1024)
                str = size.ToString() + " 字节";
            else if (size / 1024 < 1024)
            {
                double d = (double)size / 1024;
                str = Math.Round(d, 2).ToString() + " KB";
            }
            else if (size / (1024 * 1024) < 1024)
            {
                double d = (double)size / (1024 * 1024);
                str = Math.Round(d, 2).ToString() + " MB";
            }
            else
            {
                double d = (double)size / (1024 * 1024 * 1024);
                str = Math.Round(d, 2).ToString() + " GB";
            }

            return str;
        }
        #endregion 

        #region 获取文件后缀名

        /// <summary>
        /// 获取文件后缀名
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string GetFileExt(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf(".") + 1);  
        }

        #endregion
    }
}
