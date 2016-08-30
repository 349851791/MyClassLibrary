/// <summary>
/// 使用前提： 
/// 类 说 明： 二维码帮助类
/// 编 码 人： 张贺
/// 创建日期： 20160401
/// 更新日期： 
/// 更新内容:  
/// </summary>
/// 

using System;
using System.Drawing;
using System.IO;
using ThoughtWorks.QRCode.Codec;

namespace DotNet.Utils
{
    /// <summary>
    /// 二维码帮助类
    /// </summary>
    public static class TwoDimensionCodHelper
    {
        //调用示例:
        //string path = SaveImg("c://", Create_ImgCode("abcdefg", 70));

        #region 二维码生成  
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="strPath">保存路径</param>
        /// <param name="img">图片</param>
        public static string SaveImg(string strPath, Bitmap img)
        {
            string wjm = string.Empty;
            //保存图片到目录
            if (Directory.Exists(strPath))
            {
                //文件名称
                wjm = Guid.NewGuid().ToString().Replace("-", "") + ".png";
                img.Save(strPath + "/" + wjm, System.Drawing.Imaging.ImageFormat.Png);
            } 
            else
            {
                //当前目录不存在，则创建
                Directory.CreateDirectory(strPath);
            }
            return strPath + wjm;
        }
        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="codeNumber">要生成二维码的字符串</param>     
        /// <param name="size">大小尺寸</param>
        /// <returns>二维码图片</returns>
        public static Bitmap Create_ImgCode(string codeNumber, int size)
        {
            //创建二维码生成类
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            //设置编码模式
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //设置编码测量度
            qrCodeEncoder.QRCodeScale = size;
            //设置编码版本
            qrCodeEncoder.QRCodeVersion = 0;
            //设置编码错误纠正
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //生成二维码图片
            System.Drawing.Bitmap image = qrCodeEncoder.Encode(codeNumber);
            return image;
        }
        #endregion
    }
}

#region 不用方法 

///// <summary>
///// 删除目录下所有文件
///// </summary>
///// <param name="aimPath">路径</param>
//public void DeleteDir(string aimPath)
//{
//    try
//    {
//        //目录是否存在
//        if (Directory.Exists(aimPath))
//        {
//            // 检查目标目录是否以目录分割字符结束如果不是则添加之
//            if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
//                aimPath += Path.DirectorySeparatorChar;
//            // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
//            // 如果你指向Delete目标文件下面的文件而不包含目录请使用下面的方法
//            string[] fileList = Directory.GetFiles(aimPath);
//            //string[] fileList = Directory.GetFileSystemEntries(aimPath);
//            // 遍历所有的文件和目录
//            foreach (string file in fileList)
//            {
//                // 先当作目录处理如果存在这个目录就递归Delete该目录下面的文件
//                if (Directory.Exists(file))
//                {
//                    DeleteDir(aimPath + Path.GetFileName(file));
//                }
//                // 否则直接Delete文件
//                else
//                {
//                    File.Delete(aimPath + Path.GetFileName(file));
//                }
//            }
//        }
//    }
//    catch (Exception e)
//    {
//        throw e;
//    }
//}
#endregion