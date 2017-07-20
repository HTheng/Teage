using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web;
namespace Teage.ApiCommon
{
    public class Security
    {
      /// <summary>
        /// 创建验证码字符
        /// </summary>
        /// <param name="length">字符长度</param>
        /// <returns>验证码字符</returns>
        public static string CreateVerificationText(int length)
        {
            char[] verification = new char[length];
            char[] dictionary = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j',
                'k', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2',
                '3', '4', '5', '6', '7', '8', '9' };
            Random random = new Random();
            for (int i = 0; i < length; i++) { verification[i] = dictionary[random.Next(dictionary.Length - 1)]; }
            return new string(verification);
        }
        /// <summary>
        /// 创建验证码图片
        /// </summary>
        /// <param name="verificationText">验证码字符串</param>
        /// <param name="width">图片宽度</param>
        /// <param name="height">图片长度</param>
        /// <returns>图片</returns>
        public static Bitmap CreateVerificationImage(string verificationText, int width, int height)
        {
            Bitmap image = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器 
                System.Random random = new System.Random();

                //清空图片背景色 
                g.Clear(Color.White);

                //画图片的背景噪音线 
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
               
                Font font = new Font("Arial", 16, (FontStyle.Bold | FontStyle.Italic));
                SizeF totalSizeF = g.MeasureString(verificationText, font);
                PointF startPointF = new PointF((width - totalSizeF.Width) / 2, (height - totalSizeF.Height) / 2);
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(verificationText, font, brush, startPointF);

                //画图片的前景噪音点 
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线 
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                return image;
            }
            finally
            {
                g.Dispose();
                //image.Dispose();
            }
        }
    }
}
