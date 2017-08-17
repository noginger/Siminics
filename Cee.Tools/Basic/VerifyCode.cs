using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Cee.Tools.Basic
{
    /// <summary>
    /// 验证码公共类
    /// </summary>
    public class VerifyCode
    {
        #region 私有字段
        private string text;
        private Bitmap image;
        private int letterCount = 4;   //验证码位数
        private int letterWidth = 16;  //单个字体的宽度范围
        private int letterHeight = 20; //单个字体的高度范围
        private static byte[] randb = new byte[4];
        private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        private Font[] fonts =
            {
                new Font(new FontFamily("Times New Roman"), 10 + Next(1), System.Drawing.FontStyle.Regular),
                new Font(new FontFamily("Georgia"), 10 + Next(1), System.Drawing.FontStyle.Regular),
                new Font(new FontFamily("Arial"), 10 + Next(1), System.Drawing.FontStyle.Regular),
                new Font(new FontFamily("Comic Sans MS"), 10 + Next(1), System.Drawing.FontStyle.Regular)
            };
        #endregion

        #region 公有属性
        /// <summary>
        /// 验证码
        /// </summary>
        public string Text
        {
            get { return this.text; }
        }

        /// <summary>
        /// 验证码图片
        /// </summary>
        public Bitmap Image
        {
            get { return this.image; }
        }
        #endregion

        #region 构造函数
        public VerifyCode()
        {
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            HttpContext.Current.Response.CacheControl = "no-cache";
            this.text = RandomNumber(4);
            CreateImage();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="max">最大值</param>
        private static int Next(int max)
        {
            rand.GetBytes(randb);
            int value = BitConverter.ToInt32(randb, 0);
            value = value % (max + 1);
            if (value < 0) value = -value;
            return value;
        }

        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        private static int Next(int min, int max)
        {
            int value = Next(max - min) + min;
            return value;
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 绘制验证码
        /// </summary>
        public void CreateImage()
        {
            int intImageWidth = this.text.Length * letterWidth;
            Bitmap twistImage = new Bitmap(intImageWidth, letterHeight);
            Graphics g = Graphics.FromImage(twistImage);
            g.Clear(Color.White);
            for (int i = 0; i < 2; i++)
            {
                int x1 = Next(twistImage.Width - 1);
                int x2 = Next(twistImage.Width - 1);
                int y1 = Next(twistImage.Height - 1);
                int y2 = Next(twistImage.Height - 1);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            int _x, _y = 0;
            _x = -12;
            for (int intIndex = 0; intIndex < this.text.Length; intIndex++)
            {
                _x += Next(12, 16);
                _y = Next(-2, 2);
                string strChar = this.text.Substring(intIndex, 1);
                strChar = Next(1) == 1 ? strChar.ToLower() : strChar.ToUpper();
                Brush newBrush = new SolidBrush(GetRandomColor());
                Point thePos = new Point(_x, _y);
                g.DrawString(strChar, fonts[Next(fonts.Length - 1)], newBrush, thePos);
            }
            for (int i = 0; i < 10; i++)
            {
                int x = Next(twistImage.Width - 1);
                int y = Next(twistImage.Height - 1);
                twistImage.SetPixel(x, y, Color.FromArgb(Next(0, 255), Next(0, 255), Next(0, 255)));
            }
            twistImage = TwistImage(twistImage, true, Next(1, 3), Next(4, 6));
            g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, intImageWidth - 1, (letterHeight - 1));
            this.image = twistImage;
        }

        /// <summary>
        /// 字体随机颜色
        /// </summary>
        public Color GetRandomColor()
        {
            Random randomNumFirst = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(randomNumFirst.Next(50));
            Random randomNumSencond = new Random((int)DateTime.Now.Ticks);
            int intRed = randomNumFirst.Next(180);
            int intGreen = randomNumSencond.Next(180);
            int intBlue = (intRed + intGreen > 300) ? 0 : 400 - intRed - intGreen;
            intBlue = (intBlue > 255) ? 255 : intBlue;
            return Color.FromArgb(intRed, intGreen, intBlue);
        }

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高,一般为3</param>
        /// <param name="dPhase">波形的起始相位,取值区间[0-2*PI)</param>
        public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            double PI = 6.283185307179586476925286766559;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI * (double)j) / dBaseAxisLen : (PI * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            srcBmp.Dispose();
            return destBmp;
        }
        #endregion

        #region 生成随机数字
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="length">生成长度</param>
        public static string RandomNumber(int Length)
        {
            return RandomNumber(Length, false);
        }

        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string RandomNumber(int Length, bool Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
        #endregion

        #region 生成随机字母与数字
        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="IntStr">生成长度</param>
        public static string Str(int Length)
        {
            return RandomStr(Length, false);
        }

        /// <summary>
        /// 生成随机字母与数字
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string RandomStr(int Length, bool Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        #endregion

        #region 生成随机纯字母随机数
        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="IntStr">生成长度</param>
        public static string RandomStrChar(int Length)
        {
            return RandomStrChar(Length, false);
        }

        /// <summary>
        /// 生成随机纯字母随机数
        /// </summary>
        /// <param name="Length">生成长度</param>
        /// <param name="Sleep">是否要在生成前将当前线程阻止以避免重复</param>
        public static string RandomStrChar(int Length, bool Sleep)
        {
            if (Sleep) System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }
        #endregion
    }

    /// <summary>
    /// 自定义验证码格式
    /// </summary>
    public class VerifyCodeAuto : System.Web.UI.Page
    {
        #region 似有变量

        //验证码长度
        private int _codeLen = 4;
        //图片清晰度
        private int _fineness = 90;
        //图片宽度
        private int _imgWidth = 92;
        //图片高度
        private int _imgHeight = 36;
        //字体家族名称
        private string _fontFamily = "Arial Black";
        //字体大小
        private int _fontSize = 14;
        //字体样式
        private int _fontStyle = 0;
        //绘制起始坐标X
        private int _posX = 0;
        //绘制起始坐标Y 
        private int _posY = 0;

        #endregion

        #region 属性
        /// <summary>
        /// 验证码长度
        /// </summary>
        public int CodeLen
        {
            get { return _codeLen; }
            set { this._codeLen = value; }
        }

        /// <summary>
        /// 图片宽度
        /// </summary>
        public int ImgWidth
        {
            get { return _imgWidth; }
            set { this._imgWidth = value; }
        }

        /// <summary>
        /// 图片高度
        /// </summary>
        public int ImgHeight
        {
            get { return _imgHeight; }
            set { this._imgHeight = value; }
        }

        #endregion

        #region 创建图片验证码
        public void CreateImage(HttpContext context, string nCode)
        {
            byte[] image = CreateImage(nCode);
            context.Response.ClearContent(); //需要输出图象信息 要修改HTTP头
            context.Response.ContentType = "image/Png";
            context.Response.BinaryWrite(image);
            context.Response.End();
        }

        public  byte[] CreateImage(string nCode)
        {
            int intImageWidth = ImgWidth;
            int width = intImageWidth;
            int height = ImgHeight;
            Random newRandom = new Random();
            //  图高20px
            Bitmap theBitmap = new Bitmap(width, height);
            Graphics theGraphics = Graphics.FromImage(theBitmap);
            try
            {
                theGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                theGraphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                theGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                theGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //  白色背景
                //theGraphics.Clear(Color.White);
                theGraphics.DrawImage(theBitmap, new Rectangle(0, 0, width, height), new Rectangle(0, 0, theBitmap.Width, theBitmap.Height), GraphicsUnit.Pixel);
                //  灰色边框
                theGraphics.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, intImageWidth - 1, height - 1);
                //13pt的字体
                float fontSize = ImgHeight * 1.0f / 1.68f;
                float fontSpace = fontSize / 20f;
                Font theFont = new Font("Arial", fontSize);
                System.Drawing.Drawing2D.GraphicsPath gp = null;
                System.Drawing.Drawing2D.Matrix matrix;
                for (int int_index = 0; int_index < nCode.Length; int_index++)
                {
                    string str_char = nCode.Substring(int_index, 1);
                    Brush newBrush = new SolidBrush(GetRandomColor());
                    Point thePos = new Point((int)(int_index * (fontSize + fontSpace) + newRandom.Next(3)), 1 + newRandom.Next(3));
                    gp = new System.Drawing.Drawing2D.GraphicsPath();
                    gp.AddString(str_char, theFont.FontFamily, 0, fontSize, thePos, new StringFormat());
                    matrix = new System.Drawing.Drawing2D.Matrix();
                    int angle = GetRandomAngle();
                    PointF centerPoint = new PointF(thePos.X + fontSize / 2, thePos.Y + fontSize / 2);
                    matrix.RotateAt(angle, centerPoint);
                    theGraphics.Transform = matrix;
                    theGraphics.DrawPath(new Pen(Color.White, 2f), gp);
                    //theGraphics.FillPath(new SolidBrush(Color.Black), gp);
                    theGraphics.FillPath(new SolidBrush(GetRandomColor()), gp);
                    theGraphics.ResetTransform();
                }
                if (gp != null) gp.Dispose();
                //  将生成的图片发回客户端
                MemoryStream ms = new MemoryStream();
                theBitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception)
            {
                return null;

            }
            finally
            {
                theGraphics.Dispose();
                theBitmap.Dispose();
            }
        }

        #endregion

        public string GetRandomFile(string path)
        {
            FileInfo[] fi = this.GetAllFilesInPath(path);
            Random rand = new Random(new Guid().GetHashCode() + (int)DateTime.Now.Ticks);
            int k = rand.Next(0, fi.Length);

            return fi[k].FullName;
        }

        public FileInfo[] GetAllFilesInPath(string path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(path);

            return di.GetFiles("*.jpg", SearchOption.TopDirectoryOnly);
        }

        public int GetRandomAngle()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(rand.Next(50));
            return rand.Next(-45, 45);
        }


        #region 返回纯数字
        //------------------------------------------------------------
        // 随机生成验证码，并保存到SESSION中
        //------------------------------------------------------------
        public string GetIntCode(HttpContext context)
        {
            string code = "";

            // 随机数对象
            Random random = new Random();

            for (int i = 0; i < CodeLen; i++)
            {
                // 26: a - z
                int n = random.Next(26);

                // 将数字转换成大写字母
                code += (char)(n + 65);
            }

            return code;
        }

        #endregion

        #region 返回数字、字母

        public string GetStringCode(HttpContext context)
        {
            String Vchar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
            String[] VcArray = Vchar.Split(',');
            String VNum = "";
            Random random = new Random();
            for (int i = 1; i <= CodeLen; i++)
            {
                int iNum = 0;
                while ((iNum = Convert.ToInt32(VcArray.Length * random.NextDouble())) == VcArray.Length)
                {
                    iNum = Convert.ToInt32(VcArray.Length * random.NextDouble());
                }
                VNum += VcArray[iNum];
            }
            return VNum;

        }

        #endregion

        #region 返回汉字

        public string GetText(HttpContext context)
        {
            Encoding gb = Encoding.GetEncoding("gb2312");

            //调用函数产生4个随机中文汉字编码 
            //object[] bytes = CreateRegionCode(3);

            //定义一个字符串数组储存汉字编码的组成元素 
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            Random rnd = new Random();

            //定义一个object数组用来 
            object[] bytes = new object[CodeLen - 1];

            /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中 
             每个汉字有四个区位码组成 
             区位码第1位和区位码第2位作为字节数组第一个元素 
             区位码第3位和区位码第4位作为字节数组第二个元素 
            */
            for (int i = 0; i < CodeLen - 1; i++)
            {
                //区位码第1位 
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();

                //区位码第2位 
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);//更换随机数发生器的种子避免产生重复值 
                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();

                //区位码第3位 
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();

                //区位码第4位 
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();

                //定义两个字节变量存储产生的随机汉字区位码 
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                //将两个字节变量存储在字节数组中 
                byte[] str_r = new byte[] { byte1, byte2 };

                //将产生的一个汉字的字节数组放入object数组中 
                bytes.SetValue(str_r, i);

            }


            //根据汉字编码的字节数组解码出中文汉字 
            string str1 = gb.GetString((byte[])Convert.ChangeType(bytes[0], typeof(byte[])));
            string str2 = gb.GetString((byte[])Convert.ChangeType(bytes[1], typeof(byte[])));
            string str3 = gb.GetString((byte[])Convert.ChangeType(bytes[2], typeof(byte[])));

            string txt = str1 + str2 + str3;
            return txt;

        }

        #endregion

        #region 生成随机颜色
        //生成随机颜色
        public Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            System.Threading.Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            int int_Red = RandomNum_First.Next(256);
            int int_Green = RandomNum_Sencond.Next(256);
            int int_Blue = (int_Red + int_Green > 400) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;

            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }



        #endregion
    }
}
