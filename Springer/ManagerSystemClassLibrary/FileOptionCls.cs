using ManagerSystemModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileOptionCls
    {

        /// <summary>
        /// 遍历文件
        /// </summary>
        /// <param name="path">绝对路径</param>
        /// <param name="extname"></param>
        /// <returns></returns>
        public List<PathNameModel> GetFileList(string path, string extname)
        {
            var result = new List<PathNameModel>();
            string ipath = System.Configuration.ConfigurationManager.AppSettings["FireFlaPath"].ToString();//相对路径
            //string PhysicalPath = Server.MapPath(ipath + "\\");//绝路径
            DirectoryInfo TheFolder = new DirectoryInfo(path);
            // file.FileName;
            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                // FileInfo.Name，FileInfo.Extensioin：获取文件的名称和扩展名；
                var extnm = NextFile.Extension.ToLower();
                if (extnm.Equals(extname.ToLower()))
                {
                    var model = new PathNameModel();
                    model.Name = NextFile.Name;
                    model.Path = ipath + "/";
                    result.Add(model);
                }
            }
            return result;
        }
 
    }
}
