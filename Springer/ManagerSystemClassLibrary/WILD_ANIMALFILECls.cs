using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 野生动物附件表
    /// </summary>
   public class WILD_ANIMALFILECls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型Message</returns>
       public static Message Manager(WILD_ANIMALFILE_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.WILD_ANIMALFILE.Add(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.WILD_ANIMALFILE.Mdy(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "MdyTP")
            {
                Message msg = BaseDT.WILD_ANIMALFILE.MdyTP(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.WILD_ANIMALFILE.Del(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            return new Message(false, "无效操作!", m.returnUrl);
        }
        #endregion

       #region 获取单条记录
       /// <summary>
       /// 获取单条记录
       /// </summary>
       /// <param name="sw"></param>
       /// <returns></returns>
       public static WILD_ANIMALFILE_Model getModel(WILD_ANIMALFILE_SW sw)
       {
           WILD_ANIMALFILE_Model m = new WILD_ANIMALFILE_Model();
           DataTable dt = BaseDT.WILD_ANIMALFILE.getDT(sw);//列表
           if (dt.Rows.Count > 0)
           {
               int i = 0;
               m.PESTFILEID = dt.Rows[i]["PESTFILEID"].ToString();
               m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
               m.PESTFILETITLE = dt.Rows[i]["PESTFILETITLE"].ToString();
               m.PESTFILETYPE = dt.Rows[i]["PESTFILETYPE"].ToString();
               m.PESTFILENAME = dt.Rows[i]["PESTFILENAME"].ToString();
               m.UPLOADTIME = ClsSwitch.SwitTM(dt.Rows[i]["UPLOADTIME"].ToString());
               m.UID = dt.Rows[i]["UID"].ToString();
           }
           dt.Clear();
           dt.Dispose();
           return m;
       }
       #endregion

       #region 获取数量列表
       /// <summary>
       /// 获取数量列表
       /// </summary>
       /// <param name="sw"></param>
       /// <returns></returns>
       public static IEnumerable<WILD_ANIMALFILE_Model> getModelList(WILD_ANIMALFILE_SW sw)
       {
           var result = new List<WILD_ANIMALFILE_Model>();
           DataTable dt = BaseDT.WILD_ANIMALFILE.getDT(sw);//列表
           DataTable dtBiolo = BaseDT.T_SYS_BIOLOGICALTYPE.getDT(new T_SYS_BIOLOGICALTYPE_SW());
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               WILD_ANIMALFILE_Model m = new WILD_ANIMALFILE_Model();
               m.PESTFILEID = dt.Rows[i]["PESTFILEID"].ToString();
               m.BIOLOGICALTYPECODE = dt.Rows[i]["BIOLOGICALTYPECODE"].ToString();
               m.BIOLOGICALTYPENAME = BaseDT.T_SYS_BIOLOGICALTYPE.getName(dtBiolo, m.BIOLOGICALTYPECODE);
               m.PESTFILETITLE = dt.Rows[i]["PESTFILETITLE"].ToString();
               m.PESTFILETYPE = dt.Rows[i]["PESTFILETYPE"].ToString();
               m.PESTFILENAME = dt.Rows[i]["PESTFILENAME"].ToString();
               m.UPLOADTIME = ClsSwitch.SwitTM(dt.Rows[i]["UPLOADTIME"].ToString());
               m.UID = dt.Rows[i]["UID"].ToString();
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           dtBiolo.Clear();
           dtBiolo.Dispose();
           return result;
       }
       #endregion
    }
}
