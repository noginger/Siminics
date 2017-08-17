using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Cee.Tools.IO
{
    /// <summary>
    /// 操作目录类
    /// </summary>
    public class DirectoryUtils
    {
        /// <summary>
        /// 目录是否存在
        /// </summary>
        /// <param name="dirPath">目录的绝对路径</param>
        /// <returns></returns>
        public static bool IsExistDirectory(string dirPath)
        {
            return Directory.Exists(dirPath);
        }

        /// <summary>
        /// 获取目录文件列表
        /// </summary>
        /// <param name="dirPath">目录绝对路径</param>
        /// <returns></returns>
        public static string[] GetFileNames(string dirPath)
        {
            return !IsExistDirectory(dirPath) ? new string[0] : Directory.GetFiles(dirPath);
        }

        /// <summary>
        /// 检索目录文件列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //如果目录不存在，则抛出异常
            if (!IsExistDirectory(directoryPath))
            {
                return new string[0];
            }

            return Directory.GetFiles(
                directoryPath,
                searchPattern,
                isSearchChild ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }


        /// <summary>
        /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.
        /// </summary>
        /// <param name="dirPath">指定目录的绝对路径</param>        
        public static string[] GetDirectories(string dirPath)
        {
            return Directory.GetDirectories(dirPath);
        }

        /// <summary>
        /// 根据文件获取目录路径
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DirectoryInfo GetDirNameFromFile(string filePath)
        {
            FileInfo file = new FileInfo(filePath);
            DirectoryInfo dir = file.Directory;
            return dir;
        }


        /// <summary>
        /// 获取指定目录及子目录中所有子目录列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            return Directory.GetDirectories(
                directoryPath,
                searchPattern,
                isSearchChild
                    ? SearchOption.AllDirectories
                    : SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// 检索文件夹文件数
        /// </summary>
        /// <param name="dirPath">目录根路径</param>
        /// <param name="searchPattern">检索条件,"*"代表0或N个字符，"?"代表一个字符</param>
        /// <remarks>e.g:"log*.xml表示搜索所有以log开头的xml文件"</remarks>
        /// <returns></returns>
        public static int SearchFileCount(string dirPath, string searchPattern)
        {
            //获取搜索目录文件列表
            string[] fileNames = GetFileNames(dirPath, searchPattern, false);

            return fileNames.Length;
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="dirPath">目录路径</param>
        public static void CreateDirectory(string dirPath)
        {
            if (!IsExistDirectory(dirPath))
                Directory.CreateDirectory(dirPath);
        }

        /// <summary>
        /// 文件移动目录[剪切]
        /// </summary>
        /// <param name="filePath">移动文件</param>
        /// <param name="dirPath">移动到目录</param>
        public static void MoveToDir(string filePath,string dirPath)
        {
            //文件是否存在
            if(!FileUtils.Exists(filePath))
                return;

            //获取源文件的名称
            string sourceFileName = FileUtils.GetFileName(filePath);

            //如果目标目录不存在则创建
            CreateDirectory(dirPath);

            //如果目标中存在同名文件,则删除
            if (FileUtils.Exists(dirPath + "\\" + sourceFileName))
            {
                FileUtils.Delete(dirPath + "\\" + sourceFileName);
            }

            //目标文件路径
            string descFilePath;
            if (!dirPath.EndsWith(@"\"))
            {
                descFilePath = dirPath + "\\" + sourceFileName;
            }
            else
            {
                descFilePath = dirPath + sourceFileName;
            }

            //将文件移动到指定目录
            File.Move(filePath, descFilePath); 
        }

        /// <summary>
        /// 清空指定目录下所有文件及子目录,但该目录依然保存.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static void ClearDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                //删除目录中所有的文件
                string[] fileNames = GetFileNames(directoryPath);
                foreach (string t in fileNames)
                {
                    FileUtils.Delete(t);
                }

                //删除目录中所有的子目录
                string[] directoryNames = GetDirectories(directoryPath);
                foreach (string t in directoryNames)
                {
                    Delete(t);
                }
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dirPath">指定目录的绝对路径</param>
        public static void Delete(string dirPath)
        {
            if (IsExistDirectory(dirPath))
            {
                Directory.Delete(dirPath, true);
            }
        }
    }
}
