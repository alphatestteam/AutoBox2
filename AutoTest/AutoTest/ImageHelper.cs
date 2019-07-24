using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace AutoTest
{
    class ImageHelper
    {

        public static String path = Directory.GetCurrentDirectory();

        public static Image thumb(Image source, int width,
                int height, bool b)
        {
            // targetW，targetH分别表示目标长和宽
            //        int type = 0;//ource.getType();
            //Image target = null;
            double sx = (double)width / source.Width;
            double sy = (double)height / source.Height;

            if (b)
            {
                if (sx > sy)
                {
                    sx = sy;
                    width = (int)(sx * source.Width);
                }
                else
                {
                    sy = sx;
                    height = (int)(sy * source.Height);
                }
            }
            Bitmap ni = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(source);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(source, new Rectangle(0, 0, width, height), new Rectangle(0, 0, source.Width, source.Height), GraphicsUnit.Pixel);
            g.Dispose();


            return ni;
        }

        #region

        public static Image readImage(String filename)
        {
            try
            {
                Image sourceImage = Image.FromFile(filename);
                return sourceImage;
            }
            catch
            {

            }

            return null;
        }

        #endregion
        /**
	 * 灰度值计算
	 * @param pixels 像素
	 * @return int 灰度值
	 */
        public static int rgbToGray(Color posClr)
        {
            return (posClr.R + posClr.G + posClr.B + posClr.A) / 4;
            //return (posClr.R * 19595 + posClr.G * 38469 + posClr.B * 7472) >> 16;
        }

        /**
         * 计算数组的平均值
         */
        public static int average(int[] pixels)
        {
            float m = 0;
            for (int i = 0; i < pixels.Length; ++i)
            {
                m += pixels[i];
            }
            m = m / pixels.Length;
            return (int)m;
        }

        /**
	 * 计算（Hamming distance）。
	 * @param sourceHashCode 源hashCode
	 * @param hashCode 与之比较的hashCode
	 */
        public static int hammingDistance(String sourceHashCode, String hashCode)
        {
            int difference = 0;
            int len = sourceHashCode.Length;

            for (int i = 0; i < len; i++)
            {
                if (sourceHashCode.ToCharArray()[i] != hashCode.ToCharArray()[i])
                {
                    difference++;
                }
            }

            return difference;
        }

        /**
	 * 生成图片指纹
	 * @param filename 文件名
	 * @return 图片指纹
	 */
        public static String produceFingerPrint(String filename)
        {
            System.Drawing.Image source = ImageHelper.readImage(filename);// 读取文件
            //pictureBox1.Image = source;
            int width = 64;
            int height = 64;

            // 第一步，缩小尺寸。
            // 将图片缩小到64x64的尺寸，总共64个像素。这一步的作用是去除图片的细节，只保留结构、明暗等基本信息，摒弃不同尺寸、比例带来的图片差异。
            //System.Drawing.Image thumb = ImageHelper.thumb((System.Drawing.Image)source.Clone(), width, height, false);
            Bitmap b = new Bitmap(source, 64, 64);
            // pictureBox1.Image = b;
            System.Drawing.Image thumb = b;
            // 第二步，简化色彩。
            // 将缩小后的图片，转为64级灰度。也就是说，所有像素点总共只有64种颜色。
            int[] pixels = new int[width * height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pixels[i * height + j] = ImageHelper.rgbToGray(((Bitmap)thumb).GetPixel(i, j));
                }
            }

            // 第三步，计算平均值。
            // 计算所有64个像素的灰度平均值。
            int avgPixel = ImageHelper.average(pixels);

            // 第四步，比较像素的灰度。
            // 将每个像素的灰度，与平均值进行比较。大于或等于平均值，记为1；小于平均值，记为0。
            int[] comps = new int[width * height];
            for (int i = 0; i < comps.Length; i++)
            {
                if (pixels[i] >= avgPixel)
                {
                    comps[i] = 1;
                }
                else
                {
                    comps[i] = 0;
                }
            }

            // 第五步，计算哈希值。
            // 将上一步的比较结果，组合在一起，就构成了一个64位的整数，这就是这张图片的指纹。组合的次序并不重要，只要保证所有图片都采用同样次序就行了。
            String hashCode = "";
            for (int i = 0; i < comps.Length; i += 4)
            {
                int result = comps[i] * (int)Math.Pow(2, 3) + comps[i + 1] * (int)Math.Pow(2, 2) + comps[i + 2] * (int)Math.Pow(2, 1) + comps[i + 2];
                hashCode += (binaryToHex(result));
            }

            // 得到指纹以后，就可以对比不同的图片，看看64位中有多少位是不一样的。
            return hashCode;
        }

        public static String produceFingerPrint(System.Drawing.Image file)
        {
            System.Drawing.Image source = (System.Drawing.Image)file.Clone();// 读取文件
            //pictureBox1.Image = source;
            int width = 8;
            int height = 8;

            // 第一步，缩小尺寸。
            // 将图片缩小到8x8的尺寸，总共64个像素。这一步的作用是去除图片的细节，只保留结构、明暗等基本信息，摒弃不同尺寸、比例带来的图片差异。
            //System.Drawing.Image thumb = ImageHelper.thumb((System.Drawing.Image)source.Clone(), width, height, false);
            Bitmap b = new Bitmap(source, 8, 8);
            // pictureBox1.Image = b;
            System.Drawing.Image thumb = b;
            // 第二步，简化色彩。
            // 将缩小后的图片，转为64级灰度。也就是说，所有像素点总共只有64种颜色。
            int[] pixels = new int[width * height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    pixels[i * height + j] = ImageHelper.rgbToGray(((Bitmap)thumb).GetPixel(i, j));
                }
            }

            // 第三步，计算平均值。
            // 计算所有64个像素的灰度平均值。
            int avgPixel = ImageHelper.average(pixels);

            // 第四步，比较像素的灰度。
            // 将每个像素的灰度，与平均值进行比较。大于或等于平均值，记为1；小于平均值，记为0。
            int[] comps = new int[width * height];
            for (int i = 0; i < comps.Length; i++)
            {
                if (pixels[i] >= avgPixel)
                {
                    comps[i] = 1;
                }
                else
                {
                    comps[i] = 0;
                }
            }

            // 第五步，计算哈希值。
            // 将上一步的比较结果，组合在一起，就构成了一个64位的整数，这就是这张图片的指纹。组合的次序并不重要，只要保证所有图片都采用同样次序就行了。
            String hashCode = "";
            for (int i = 0; i < comps.Length; i += 4)
            {
                int result = comps[i] * (int)Math.Pow(2, 3) + comps[i + 1] * (int)Math.Pow(2, 2) + comps[i + 2] * (int)Math.Pow(2, 1) + comps[i + 2];
                hashCode += (binaryToHex(result));
            }

            // 得到指纹以后，就可以对比不同的图片，看看64位中有多少位是不一样的。
            return hashCode;
        }

        /**
	 * 二进制转为十六进制
	 * @param int binary
	 * @return char hex
	 */
        private static char binaryToHex(int binary)
        {
            char ch = ' ';
            switch (binary)
            {
                case 0:
                    ch = '0';
                    break;
                case 1:
                    ch = '1';
                    break;
                case 2:
                    ch = '2';
                    break;
                case 3:
                    ch = '3';
                    break;
                case 4:
                    ch = '4';
                    break;
                case 5:
                    ch = '5';
                    break;
                case 6:
                    ch = '6';
                    break;
                case 7:
                    ch = '7';
                    break;
                case 8:
                    ch = '8';
                    break;
                case 9:
                    ch = '9';
                    break;
                case 10:
                    ch = 'a';
                    break;
                case 11:
                    ch = 'b';
                    break;
                case 12:
                    ch = 'c';
                    break;
                case 13:
                    ch = 'd';
                    break;
                case 14:
                    ch = 'e';
                    break;
                case 15:
                    ch = 'f';
                    break;
                default:
                    ch = ' ';
                    break;
            }
            return ch;
        }
    }
}