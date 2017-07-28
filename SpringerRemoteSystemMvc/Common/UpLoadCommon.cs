using log4net;
using Springer.Common.Utils;
using Springer.EntityModel.Entity;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace Springer.Common
{
    public class UpLoadCommon
    {
        private readonly ILog logs = LogHelper.GetInstance();

        public string UploadFile(FileUploadMessageModel request, string uploadFolder)
        {
            string dateString = DateTime.Now.ToShortDateString() + @"\";
            string fileName = Guid.NewGuid().ToString() + request.FileName;
            //Stream sourceStream = request.FileData;
            //FileStream targetStream = null;

            //if (!sourceStream.CanRead)
            //{
            //    throw new Exception("数据流不可读!");
            //}
            uploadFolder = uploadFolder + dateString;//相对路径
            string diruploadFolder = Path.GetFullPath(uploadFolder);//绝对路径
            if (!Directory.Exists(diruploadFolder))
            {
                Directory.CreateDirectory(diruploadFolder);
            }

            string filePath = Path.Combine(uploadFolder, fileName);//相对路径+文件名
            string dirfilePath = Path.Combine(diruploadFolder, fileName);//绝对路径+文件名
            try
            {
                //using (targetStream = new FileStream(dirfilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                //{
                //    //read from the input stream in 4K chunks
                //    //and save to output stream
                //    const int bufferLen = 4096;
                //    byte[] buffer = new byte[bufferLen];
                //    int count = 0;
                //    while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
                //    {
                //        targetStream.Write(buffer, 0, count);
                //    }
                //    targetStream.Close();
                //    sourceStream.Close();
                //}

                using (FileStream p_Stream = new FileStream(dirfilePath, FileMode.Create))
                {
                    p_Stream.Write(request.FileData, 0, request.FileData.Length);
                }

                return filePath;
            }
            catch (Exception ex)
            {

                return "";
            }

        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="base64Data">加密文件</param>
        /// <param name="uploadFolder">相对目录</param>
        /// <param name="dirFolder">根目录</param>
        /// <returns></returns>
        public string UploadFile(byte[] base64Data, string uploadFolder, string dirFolder)
        {
            string dateString = DateTime.Now.ToString("yyyyMMdd") + @"\";
            string fileName = Guid.NewGuid().ToString() + ".jpg";

            uploadFolder = uploadFolder + dateString;//相对路径
            string diruploadFolder = Path.GetFullPath(dirFolder + uploadFolder);//绝对路径
            if (!Directory.Exists(diruploadFolder))
            {
                Directory.CreateDirectory(diruploadFolder);
            }
            string filePath = Path.Combine(uploadFolder, fileName);//相对路径+文件名
            //string filePath = uploadFolder + fileName;//相对路径+文件名
            string dirfilePath = Path.Combine(diruploadFolder, fileName);//绝对路径+文件名
            try
            {

                using (FileStream p_Stream = new FileStream(dirfilePath, FileMode.Create))
                {
                    p_Stream.Write(base64Data, 0, base64Data.Length);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                logs.Error("照片===" + ex.Message);
                return "";
            }

        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="base64Data">加密文件</param>
        /// <param name="type">上传文件类型</param>
        /// <param name="uploadFolder">相对目录</param>
        /// <param name="dirFolder">根目录</param>
        /// <param name="fileVirtualPath">文件转换相对路径</param>
        /// <returns></returns>
        public string UploadFileByType(byte[] base64Data, string type, string uploadFolder, string dirFolder, string fileVirtualPath = "")
        {
            string dateString = DateTime.Now.ToString("yyyyMMdd") + @"\";
            string fileName = string.Empty;
            string convetfileName = string.Empty;
            if (type == "1")
            {
                fileName = Guid.NewGuid().ToString() + ".jpg";
            }
            else if (type == "2")
            {
                var guidname = Guid.NewGuid().ToString();
                fileName = guidname + ".3g2";//3g2 
                convetfileName = guidname + ".mp4";
            }
            else
            {
                var guidname = Guid.NewGuid().ToString();
                fileName = guidname + ".amr";//amr
                convetfileName = guidname + ".mp3";
            }
            uploadFolder = uploadFolder + dateString;//上传相对路径
            string diruploadFolder = Path.GetFullPath(dirFolder + uploadFolder);//绝对路径
            if (!Directory.Exists(diruploadFolder))
            {
                Directory.CreateDirectory(diruploadFolder);
            }

            string dirConvertFolder = "";//上传转化文件绝对路径
            if (!string.IsNullOrEmpty(fileVirtualPath))//转化文件相对路径
            {
                fileVirtualPath = fileVirtualPath + dateString;//上传转化文件相对路径
                dirConvertFolder = Path.GetFullPath(dirFolder + fileVirtualPath);
                if (!Directory.Exists(dirConvertFolder))
                {
                    Directory.CreateDirectory(dirConvertFolder);
                }
            }
            string filePath = Path.Combine(uploadFolder, fileName);//相对路径+文件名
            string dirfilePath = Path.Combine(diruploadFolder, fileName);//上传文件绝对路径+文件名
            string dirfconvertilePath = Path.Combine(dirConvertFolder, convetfileName);//转换文件绝对路径+文件名
            try
            {
                //上传文件
                using (FileStream p_Stream = new FileStream(dirfilePath, FileMode.Create))
                {
                    p_Stream.Write(base64Data, 0, base64Data.Length);
                }
                var switchValue = ConfigurationManager.AppSettings["SwitchValue"];//是否转换视频与音频格式
                if (switchValue == "1")//0 为否 1 为是
                {
                    if (type == "2" || type == "3")//2 视频 3 音频
                    {
                        var ffmpegVirtualPath = ConfigurationManager.AppSettings["ffmpegpath"];//ffmpeg
                        var bo = MediaFileFormatConverter.ConvertAudioAndVideo(ffmpegVirtualPath, dirfilePath, dirfconvertilePath, type);
                        if (bo == false)
                        {
                            return "";
                        }
                        Thread.Sleep(100);
                        string convertfilePath = Path.Combine(fileVirtualPath, convetfileName);//文件转换相对路径+文件名
                        filePath = convertfilePath;
                    }
                }
                return filePath;
            }
            catch (Exception ex)
            {
                logs.Error("上传文件错误===" + ex.Message + "filePath===" + filePath);
                return "";
            }

        }


        #region 弃用

        //public string ByteToBase64Str(string filepath)
        //{
        //    Bitmap bmp = new Bitmap(filepath);
        //    MemoryStream ms = new MemoryStream();
        //    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
        //    byte[] arr = new byte[ms.Length];
        //    ms.Position = 0;
        //    ms.Read(arr, 0, (int)ms.Length);
        //    ms.Close();
        //    string pic = Convert.ToBase64String(arr);
        //    return pic;
        //}


        //base64string到byte[]再到图片的转换

        //             byte[] imageBytes = Convert.FromBase64String(pic);
        //             //读入MemoryStream对象
        //             MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
        //             memoryStream.Write(imageBytes, 0, imageBytes.Length);
        //             //转成图片
        //             Image image = Image.FromStream(memoryStream);


        #endregion
    }
}
