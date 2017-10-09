/// <summary>
/// 类 说 明： 压缩包帮助类
/// 编 码 人： 张贺
/// 创建日期： 2016-3-31
/// 更新日期： 
/// 更新内容:
/// </summary> 
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace DotNet.Utils
{
    /// <summary>
    /// 压缩包帮助类
    /// </summary>
    /// <summary>   
    /// 适用与ZIP压缩   
    /// </summary>   
    public class ZipHelperNew
    {
        /// <summary>   
        /// 压缩文件或文件夹   
        /// </summary>   
        /// <param name="fileToZip">要压缩的路径</param>   
        /// <param name="zipedFile">压缩后的文件名</param>   
        /// <param name="password">密码</param>   
        /// <returns>压缩结果</returns>   


        #region 压缩  
        /// <summary>
        /// 压缩文件或文件夹
        /// </summary>
        /// <param name="fileToZip">要压缩的路径</param>   
        /// <param name="zipedFile">压缩后的文件名</param>   
        /// <param name="err">错误信息</param>
        /// <param name="password">密码</param>
        /// <param name="isMD5">是否启用MD5加密</param>
        /// <returns>压缩结果</returns>
        public static bool Zip(string fileToZip, string zipedFile, out string err, string password = null, bool isMD5 = true)
        {
            bool result = false;
            err = "";
            if (Directory.Exists(fileToZip))
                result = ZipDirectory(fileToZip, zipedFile, out err, password, isMD5);
            else if (File.Exists(fileToZip))
                result = ZipFile(fileToZip, zipedFile, out err, password, isMD5);

            return result;
        }

        /// <summary>
        /// 压缩文件或文件夹
        /// </summary>
        /// <param name="folderToZip">要压缩的路径</param>   
        /// <param name="zipedFile">压缩后的文件名</param>   
        /// <param name="err">错误信息</param>
        /// <param name="password">密码</param>
        /// <param name="isMD5">是否启用MD5加密</param>
        /// <returns>压缩结果</returns>
        private static bool ZipDirectory(string folderToZip, string zipedFile, out string err, string password = null, bool isMD5 = true)
        {
            bool result = true;
            err = "";
            if (folderToZip == string.Empty)
            {
                err = "要压缩的文件夹不能为空！ ";
                return false;
            }
            if (!Directory.Exists(folderToZip))
            {
                err = "要压缩的文件夹不存在！ ";
                return false;
            }
            //压缩文件名为空时使用文件夹名＋ zip
            if (zipedFile == string.Empty)
            {
                if (folderToZip.EndsWith("\\"))
                {
                    folderToZip = folderToZip.Substring(0, folderToZip.Length - 1);
                }
                zipedFile = folderToZip + ".zip";
            }

            using (ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipedFile)))
            {
                zipStream.SetLevel(6);
                if (password != null)
                {
                    zipStream.Password = isMD5 ? Encrypt.Encrypt.MD5Encrypt(password) : password;
                }
                result = ZipDirectory(folderToZip, zipStream, "");

                zipStream.Finish();
                zipStream.Close();
            }
            return result;
        }



        /// <summary>   
        /// 递归压缩文件夹的内部方法   
        /// </summary>   
        /// <param name="folderToZip">要压缩的文件夹路径</param>   
        /// <param name="zipStream">压缩输出流</param>   
        /// <param name="parentFolderName">此文件夹的上级文件夹</param>   
        /// <returns></returns>   
        private static bool ZipDirectory(string folderToZip, ZipOutputStream zipStream, string parentFolderName)
        {
            bool result = true;
            string[] folders, files;
            ZipEntry ent = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();

            try
            {
                ent = new ZipEntry(Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/"));
                zipStream.PutNextEntry(ent);
                zipStream.Flush();

                files = Directory.GetFiles(folderToZip);
                foreach (string file in files)
                {
                    fs = File.OpenRead(file);

                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ent = new ZipEntry(Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/" + Path.GetFileName(file)));
                    ent.DateTime = DateTime.Now;
                    ent.Size = fs.Length;

                    fs.Close();

                    crc.Reset();
                    crc.Update(buffer);

                    ent.Crc = crc.Value;
                    zipStream.PutNextEntry(ent);
                    zipStream.Write(buffer, 0, buffer.Length);
                }

            }
            catch
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }

            folders = Directory.GetDirectories(folderToZip);
            foreach (string folder in folders)
                if (!ZipDirectory(folder, zipStream, folderToZip))
                    return false;

            return result;
        }

        /// <summary>
        /// 压缩文件或文件夹
        /// </summary>
        /// <param name="fileToZip">要压缩的路径</param>   
        /// <param name="zipedFile">压缩后的文件路径</param>   
        /// <param name="err">错误信息</param>
        /// <param name="password">密码</param>
        /// <param name="isMD5">是否启用MD5加密</param>
        /// <returns>压缩结果</returns> 
        private static bool ZipFile(string fileToZip, string zipedFile, out string err, string password = null, bool isMD5 = true)
        {
            bool result = true;
            ZipOutputStream zipStream = null;
            FileStream fs = null;
            ZipEntry ent = null;

            err = "";

            if (!File.Exists(fileToZip))
            {
                err = "要压缩的文件不存在！ ";
                return false;
            }
            try
            {
                fs = File.OpenRead(fileToZip);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();

                //压缩文件名为空时使用文件名
                if (zipedFile == string.Empty)
                {
                    //fileToZip = fileToZip.Substring(0, fileToZip.LastIndexOf(".")) + ".zip";

                    zipedFile = fileToZip.Substring(0, fileToZip.LastIndexOf(".")) + ".zip";
                }

                fs = File.Create(zipedFile);
                zipStream = new ZipOutputStream(fs);
                if (password != null)
                {
                    zipStream.Password = isMD5 ? Encrypt.Encrypt.MD5Encrypt(password) : password;
                }
                ent = new ZipEntry(Path.GetFileName(fileToZip));
                zipStream.PutNextEntry(ent);
                zipStream.SetLevel(6);

                zipStream.Write(buffer, 0, buffer.Length);

            }
            catch(Exception ex)
            {
                result = false;
            }
            finally
            {
                if (zipStream != null)
                {
                    zipStream.Finish();
                    zipStream.Close();
                }
                if (ent != null)
                {
                    ent = null;
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
            GC.Collect();
            GC.Collect(1);

            return result;
        }

        #endregion

        #region 解压  

        /// <summary>   
        /// 解压功能(解压压缩文件到指定目录)   
        /// </summary>   
        /// <param name="fileToUnZip">待解压的文件</param>   
        /// <param name="zipedFolder">指定解压目标目录</param>   
        /// <param name="password">密码</param>   
        /// <returns>解压结果</returns>   
        public static bool UnZip(string fileToUnZip, string zipedFolder, out string err, string password = null, bool isMD5 = true)
        {
            bool result = true;
            FileStream fs = null;
            ZipInputStream zipStream = null;
            ZipEntry ent = null;
            string fileName;

            err = "";
            if (fileToUnZip == string.Empty)
            {
                err = "要解压缩的文件夹不能为空！ ";
                return false;
            }
            if (!Directory.Exists(fileToUnZip))
            {
                err = "要解压缩的文件夹不存在！ ";
                return false;
            }
            //解压文件名为空时使用文件夹名＋ zip
            if (zipedFolder == string.Empty)
            {
                if (fileToUnZip.EndsWith("\\"))
                {
                    fileToUnZip = fileToUnZip.Substring(0, fileToUnZip.Length - 1);
                }
                zipedFolder = fileToUnZip;
            }

            if (!Directory.Exists(zipedFolder))
                Directory.CreateDirectory(zipedFolder);

            try
            {
                zipStream = new ZipInputStream(File.OpenRead(fileToUnZip));
                if (password != null)
                {
                    zipStream.Password = isMD5 ? Encrypt.Encrypt.MD5Encrypt(password) : password;
                }
                while ((ent = zipStream.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrEmpty(ent.Name))
                    {
                        fileName = Path.Combine(zipedFolder, ent.Name);
                        fileName = fileName.Replace('/', '\\');//change by Mr.HopeGi   

                        if (fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }

                        fs = File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[size];
                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);
                            if (size > 0)
                                fs.Write(data, 0, data.Length);
                            else
                                break;
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (zipStream != null)
                {
                    zipStream.Close();
                    zipStream.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return result;
        }
        #endregion
    }
}