using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cee.Tools.Drawing
{
    /// <summary>
    /// 图形处理尺寸类
    /// </summary>
    public class DrawingSize
    {
        /// <summary>
        /// 根据需要的图片尺寸，按比例剪裁原始图片
        /// </summary>
        /// <param name="nWidth">缩略图宽度</param>
        /// <param name="nHeight">缩略图高度</param>
        /// <param name="img">原始图片</param>
        /// <returns>剪裁区域尺寸</returns>
        public static Size CutRegion(int width, int height, Image img)
        {
            double w = 0.0;
            double h = 0.0;

            double nw = (double)width;
            double nh = (double)height;

            double pw = (double)img.Width;
            double ph = (double)img.Height;

            if (nw / nh > pw / ph)
            {
                w = pw;
                h = pw * nh / nw;
            }
            else if (nw / nh < pw / ph)
            {
                w = ph * nw / nh;
                h = ph;
            }
            else
            {
                w = pw;
                h = ph;
            }

            return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
        }

        /// <summary>
        /// 等比缩略尺寸
        /// </summary>
        /// <param name="nWidth">宽</param>
        /// <param name="nHeight">高</param>
        /// <param name="img">图形对象</param>
        /// <returns></returns>
        public static Size GeometricSize(int width, int height, Image img)
        {
            double w = 0.0;
            double h = 0.0;
            double sw = Convert.ToDouble(img.Width);
            double sh = Convert.ToDouble(img.Height);
            double mw = Convert.ToDouble(width);
            double mh = Convert.ToDouble(height);

            if (sw < mw && sh < mh)
            {
                w = sw;
                h = sh;
            }
            else if ((sw / sh) > (mw / mh))
            {
                w = width;
                h = (w * sh) / sw;
            }
            else
            {
                h = height;
                w = (h * sw) / sh;
            }

            return new Size(Convert.ToInt32(w), Convert.ToInt32(h));
        }
    }
}
