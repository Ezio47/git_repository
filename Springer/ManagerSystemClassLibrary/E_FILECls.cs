using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 附件
    /// </summary>
    public class E_FILECls
    {
        #region 获取附件列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<E_FILE_Model> getListModel(E_File_SW sw)
        {
            DataTable dt = BaseDT.E_FILE.getDT(sw);//列表
            var result = new List<E_FILE_Model>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                E_FILE_Model m = new E_FILE_Model();
                m.BYEMAILID = dt.Rows[i]["BYEMAILID"].ToString();
                m.EFID = dt.Rows[i]["EFID"].ToString();
                m.EMAILFILENAME = dt.Rows[i]["EMAILFILENAME"].ToString();
                m.EMAILFILESIZE = dt.Rows[i]["EMAILFILESIZE"].ToString();
                m.EMAILFILETITLE = dt.Rows[i]["EMAILFILETITLE"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
        #region 增加附件-返回主键
        /// <summary>
        /// 增加附件-返回主键
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string AddReturn(E_FILE_Model m)
        {
            return BaseDT.E_FILE.AddReturn(m);
        }
        #endregion
        #region 附件删除
        /// <summary>
        /// 附件删除
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message DEL(E_FILE_Model m) 
        {
            if (m.opMethod == "DEL") 
            {
              Message msg= BaseDT.E_FILE.Del(m);
                return new Message(true, "删除成功", "");
            }
            return new Message(false, "删除失败", "");
        }
        #endregion
    }
}
