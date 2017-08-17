using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cee.Tools.Drawing;

namespace Cee.Tools
{
    /// <summary>
    /// 图片操作类
    /// </summary>
    public class PhotoHelper
    {

        #region 生成缩略图

        #region 生成缩略图，不加水印
        /// <summary>
        /// 生成缩略图，不加水印
        /// </summary>
        /// <param name="filename">源文件</param>
        /// <param name="nWidth">缩略图宽度</param>
        /// <param name="nHeight">缩略图高度</param>
        /// <param name="destfile">缩略图保存位置</param>
        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile)
        {
            CreateSmallPhoto(filename, nWidth, nHeight, destfile, true);
        }
        /// <summary>
        /// 生成缩略图，不加水印
        /// </summary>
        /// <param name="filename">源文件</param>
        /// <param name="nWidth">缩略图宽度</param>
        /// <param name="nHeight">缩略图高度</param>
        /// <param name="destfile">缩略图保存位置</param>
        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile, bool cut)
        {
            Image img = Image.FromFile(filename);
            ImageFormat thisFormat = img.RawFormat;

            Size CutSize =DrawingSize.CutRegion(nWidth, nHeight, img);
            if (!cut)
                CutSize = DrawingSize.GeometricSize(nWidth, nHeight, img);
            //Bitmap outBmp = new Bitmap(nWidth, nHeight);
            Bitmap outBmp = new Bitmap(CutSize.Width, CutSize.Height);
            Graphics g = Graphics.FromImage(outBmp);

            ImageAttributes attr = new ImageAttributes();
            attr.SetColorKey(Color.White, Color.White);

            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.Clear(Color.White);

            int nStartX = (img.Width - CutSize.Width) / 2;
            int nStartY = (img.Height - CutSize.Height) / 2;
            g.DrawImage(img, new Rectangle(0, 0, CutSize.Width, CutSize.Height),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel);

            //int nStartX = (nWidth - CutSize.Width) / 2;
            //int nStartY = (nHeight - CutSize.Height) / 2;
            //g.DrawImage(img, new Rectangle(nStartX, nStartY, CutSize.Width, CutSize.Height),
            //    0, 0, img.Width, img.Height, GraphicsUnit.Pixel);

            g.Dispose();

            // 以下代码为保存图片时，设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            for (int x = 0; x < arrayICI.Length; x++)
            {
                if (arrayICI[x].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[x];//设置JPEG编码
                    break;
                }
            }

            if (jpegICI != null)
            {
                //outBmp.Save(Response.OutputStream, jpegICI, encoderParams);
                outBmp.Save(destfile, jpegICI, encoderParams);
            }
            else
            {
                //outBmp.Save(Response.OutputStream, thisFormat);
                outBmp.Save(destfile, thisFormat);
            }

            img.Dispose();
            outBmp.Dispose();
        }
        #endregion

        #region 生成缩略图，加水印
        /// <summary>
        /// 以剪裁方式生成带水印的缩略图
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="destfile"></param>
        /// <param name="sy"></param>
        /// <param name="nType"></param>
        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile, string sy, int nType)
        {
            if (nType == 0)
                CreateSmallPhoto(filename, nWidth, nHeight, destfile, sy, "", true);
            else
                CreateSmallPhoto(filename, nWidth, nHeight, destfile, "", sy, true);
        }
        /// <summary>
        /// 按自定义方式生成缩略图
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="nWidth"></param>
        /// <param name="nHeight"></param>
        /// <param name="destfile"></param>
        /// <param name="sy"></param>
        /// <param name="nType"></param>
        /// <param name="cut">true：剪裁；false：缩放</param>
        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile, string sy, int nType, bool cut)
        {
            if (nType == 0)
                CreateSmallPhoto(filename, nWidth, nHeight, destfile, sy, "", cut);
            else
                CreateSmallPhoto(filename, nWidth, nHeight, destfile, "", sy, cut);
        }
        #endregion

        #region 生成缩略图
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="filename">源文件</param>
        /// <param name="nWidth">缩略图宽度</param>
        /// <param name="nHeight">缩略图高度</param>
        /// <param name="destfile">缩略图保存位置</param>
        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile, string png, string text)
        {
            CreateSmallPhoto(filename, nWidth, nHeight, destfile, png, text, true);
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="filename">源文件</param>
        /// <param name="nWidth">缩略图宽度</param>
        /// <param name="nHeight">缩略图高度</param>
        /// <param name="destfile">缩略图保存位置</param>
        public static void CreateSmallPhoto(string filename, int nWidth, int nHeight, string destfile, string png, string text, bool cut)
        {
            Image img = Image.FromFile(filename);
            ImageFormat thisFormat = img.RawFormat;

            Size CutSize = DrawingSize.CutRegion(nWidth, nHeight, img);
            if (!cut)
                CutSize =DrawingSize.GeometricSize(nWidth, nHeight, img);
            Bitmap outBmp = new Bitmap(nWidth, nHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.White);

            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            int nStartX = (img.Width - CutSize.Width) / 2;
            int nStartY = (img.Height - CutSize.Height) / 2;

            g.DrawImage(img, new Rectangle(0, 0, nWidth, nHeight),
                nStartX, nStartY, CutSize.Width, CutSize.Height, GraphicsUnit.Pixel);
            g.Dispose();

            // 以下代码为保存图片时，设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            for (int x = 0; x < arrayICI.Length; x++)
            {
                if (arrayICI[x].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[x];//设置JPEG编码
                    break;
                }
            }

            if (jpegICI != null)
            {
                outBmp.Save(destfile, jpegICI, encoderParams);
            }
            else
            {
                outBmp.Save(destfile, thisFormat);
            }

            img.Dispose();
            outBmp.Dispose();

            if (!string.IsNullOrEmpty(png))
                AttachPng(png, destfile);

            if (!string.IsNullOrEmpty(text))
                AttachText(text, destfile);
        }
        #endregion

        #region 限制对大宽度
        public static void CreateSmallPhoto(string filename, int nMaxWidth)
        {
            Image img = Image.FromFile(filename);
            if (img.Width <= nMaxWidth)
            {
                img.Dispose();
                return;
            }
            int nMaxHeight = (int)Math.Ceiling((double)(img.Height * nMaxWidth) / img.Width);

            img.Dispose();

            System.IO.FileInfo oFile = new System.IO.FileInfo(filename);
            string dir = oFile.Directory.FullName;
            string TempFile = System.IO.Path.Combine(dir, Guid.NewGuid().ToString() + oFile.Extension);

            CreateSmallPhoto(filename, nMaxWidth, nMaxHeight, TempFile, false);
            System.IO.File.Copy(TempFile, filename, true);
            System.IO.File.Delete(TempFile);
        }
        #endregion

        #endregion

        #region 添加文字水印
        public static void AttachText(string text, string file)
        {
            if (string.IsNullOrEmpty(text))
                return;

            if (!System.IO.File.Exists(file))
                return;

            System.IO.FileInfo oFile = new System.IO.FileInfo(file);
            string strTempFile = System.IO.Path.Combine(oFile.DirectoryName, Guid.NewGuid().ToString() + oFile.Extension);
            oFile.CopyTo(strTempFile);

            Image img = Image.FromFile(strTempFile);
            ImageFormat thisFormat = img.RawFormat;

            int nHeight = img.Height;
            int nWidth = img.Width;

            Bitmap outBmp = new Bitmap(nWidth, nHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.White);

            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(img, new Rectangle(0, 0, nWidth, nHeight),
                0, 0, nWidth, nHeight, GraphicsUnit.Pixel);

            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };

            Font crFont = null;
            SizeF crSize = new SizeF();

            //通过循环这个数组，来选用不同的字体大小
            //如果它的大小小于图像的宽度，就选用这个大小的字体
            for (int i = 0; i < 7; i++)
            {
                //设置字体，这里是用arial，黑体
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                //Measure the Copyright string in this Font
                crSize = g.MeasureString(text, crFont);

                if ((ushort)crSize.Width < (ushort)nWidth)
                    break;
            }

            //因为图片的高度可能不尽相同, 所以定义了
            //从图片底部算起预留了5%的空间
            int yPixlesFromBottom = (int)(nHeight * .08);

            //现在使用版权信息字符串的高度来确定要绘制的图像的字符串的y坐标

            float yPosFromBottom = ((nHeight - yPixlesFromBottom) - (crSize.Height / 2));

            //计算x坐标
            float xCenterOfImg = (nWidth / 2);

            //把文本布局设置为居中
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //通过Brush来设置黑色半透明
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            //绘制版权字符串
            g.DrawString(text,                 //版权字符串文本
                crFont,                                   //字体
                semiTransBrush2,                           //Brush
                new PointF(xCenterOfImg + 1, yPosFromBottom + 1),  //位置
                StrFormat);

            //设置成白色半透明
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            //第二次绘制版权字符串来创建阴影效果
            //记住移动文本的位置1像素
            g.DrawString(text,                 //版权文本
                crFont,                                   //字体
                semiTransBrush,                           //Brush
                new PointF(xCenterOfImg, yPosFromBottom),  //位置
                StrFormat);

            g.Dispose();

            // 以下代码为保存图片时，设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            for (int x = 0; x < arrayICI.Length; x++)
            {
                if (arrayICI[x].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[x];//设置JPEG编码
                    break;
                }
            }

            if (jpegICI != null)
            {
                outBmp.Save(file, jpegICI, encoderParams);
            }
            else
            {
                outBmp.Save(file, thisFormat);
            }

            img.Dispose();
            outBmp.Dispose();

            System.IO.File.Delete(strTempFile);
        }
        #endregion

        #region 添加图片水印
        public static void AttachPng(string png, string file)
        {
            if (string.IsNullOrEmpty(png))
                return;

            if (!System.IO.File.Exists(png))
                return;

            if (!System.IO.File.Exists(file))
                return;

            System.IO.FileInfo oFile = new System.IO.FileInfo(file);
            string strTempFile = System.IO.Path.Combine(oFile.DirectoryName, Guid.NewGuid().ToString() + oFile.Extension);
            oFile.CopyTo(strTempFile);

            Image img = Image.FromFile(strTempFile);
            ImageFormat thisFormat = img.RawFormat;
            int nHeight = img.Height;
            int nWidth = img.Width;

            Bitmap outBmp = new Bitmap(nWidth, nHeight);
            Graphics g = Graphics.FromImage(outBmp);

            // 设置画布的描绘质量
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(img, new Rectangle(0, 0, nWidth, nHeight),
                0, 0, nWidth, nHeight, GraphicsUnit.Pixel);

            img.Dispose();

            img = Image.FromFile(png);

            //Bitmap bmpPng = new Bitmap(img);

            //ImageAttributes imageAttr = new ImageAttributes();
            //Color bg = Color.Green;
            //imageAttr.SetColorKey(bg, bg);

            Size pngSize = DrawingSize.GeometricSize(nWidth, nHeight, img);
            g.DrawImage(img, new Rectangle((nWidth - pngSize.Width) / 2, (nHeight - pngSize.Height) / 2, pngSize.Width, pngSize.Height),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel);

            g.Dispose();

            // 以下代码为保存图片时，设置压缩质量
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;

            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;

            //获得包含有关内置图像编码解码器的信息的ImageCodecInfo 对象。
            ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo jpegICI = null;
            for (int x = 0; x < arrayICI.Length; x++)
            {
                if (arrayICI[x].FormatDescription.Equals("JPEG"))
                {
                    jpegICI = arrayICI[x];//设置JPEG编码
                    break;
                }
            }

            if (jpegICI != null)
            {
                outBmp.Save(file, jpegICI, encoderParams);
            }
            else
            {
                outBmp.Save(file, thisFormat);
            }

            img.Dispose();
            outBmp.Dispose();

            System.IO.File.Delete(strTempFile);
        }
        #endregion

        #region 得到指定mimeType的ImageCodecInfo
        /// <summary> 
        /// 保存JPG时用 
        /// </summary> 
        /// <param name="mimeType"> </param> 
        /// <returns>得到指定mimeType的ImageCodecInfo </returns> 
        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }
        #endregion

        #region 保存为JPEG格式，支持压缩质量选项
        /// <summary>
        /// 保存为JPEG格式，支持压缩质量选项
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="FileName"></param>
        /// <param name="Qty"></param>
        /// <returns></returns>
        public static bool KiSaveAsJPEG(string SourceFile, string FileName, int Qty)
        {
            Bitmap bmp = new Bitmap(SourceFile);

            try
            {
                EncoderParameter p;
                EncoderParameters ps;

                ps = new EncoderParameters(1);

                p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Qty);
                ps.Param[0] = p;

                bmp.Save(FileName, GetCodecInfo("image/jpeg"), ps);

                bmp.Dispose();

                return true;
            }
            catch
            {
                bmp.Dispose();
                return false;
            }

        }
        #endregion

        #region 将图片压缩到指定大小
        /// <summary>
        /// 将图片压缩到指定大小
        /// </summary>
        /// <param name="FileName">待压缩图片</param>
        /// <param name="size">期望压缩后的尺寸</param>
        public static void CompressPhoto(string FileName, int size)
        {
            if (!System.IO.File.Exists(FileName))
                return;

            int nCount = 0;
            System.IO.FileInfo oFile = new System.IO.FileInfo(FileName);
            long nLen = oFile.Length;
            while (nLen > size * 1024 && nCount < 10)
            {
                string dir = oFile.Directory.FullName;
                string TempFile = System.IO.Path.Combine(dir, Guid.NewGuid().ToString() + "." + oFile.Extension);
                oFile.CopyTo(TempFile, true);

                KiSaveAsJPEG(TempFile, FileName, 70);

                try
                {
                    System.IO.File.Delete(TempFile);
                }
                catch { }

                nCount++;

                oFile = new System.IO.FileInfo(FileName);
                nLen = oFile.Length;
            }
        }
        #endregion
    }
}
