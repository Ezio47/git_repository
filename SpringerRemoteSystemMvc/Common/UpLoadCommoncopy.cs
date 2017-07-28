using Springer.EntityModel.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace Springer.Common
{
    public class UpLoadCommoncopy
    {
        //public string UploadFile(FileUploadMessageModel request, string uploadFolder)
        //{
        //    string dateString = DateTime.Now.ToShortDateString() + @"\";
        //    string fileName = Guid.NewGuid().ToString() + request.FileName;
        //    Stream sourceStream = request.FileData;
        //    FileStream targetStream = null;

        //    if (!sourceStream.CanRead)
        //    {
        //        throw new Exception("数据流不可读!");
        //    }
        //    uploadFolder = uploadFolder + dateString;//相对路径
        //    string diruploadFolder = Path.GetFullPath(uploadFolder);//绝对路径
        //    if (!Directory.Exists(diruploadFolder))
        //    {
        //        Directory.CreateDirectory(diruploadFolder);
        //    }

        //    string filePath = Path.Combine(uploadFolder, fileName);//相对路径+文件名
        //    string dirfilePath = Path.Combine(diruploadFolder, fileName);//绝对路径+文件名
        //    try
        //    {
        //        using (targetStream = new FileStream(dirfilePath, FileMode.Create, FileAccess.Write, FileShare.None))
        //        {
        //            //read from the input stream in 4K chunks
        //            //and save to output stream
        //            const int bufferLen = 4096;
        //            byte[] buffer = new byte[bufferLen];
        //            int count = 0;
        //            while ((count = sourceStream.Read(buffer, 0, bufferLen)) > 0)
        //            {
        //                targetStream.Write(buffer, 0, count);
        //            }
        //            targetStream.Close();
        //            sourceStream.Close();
        //        }
        //        return filePath;
        //    }
        //    catch (Exception ex)
        //    {

        //        return "";
        //    }

        //}
    }
}
