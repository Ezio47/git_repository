using DataBaseClassLibrary;
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
    /// 数据中心_照片
    /// </summary>
   public class DC_PHOTOCls
   {
       #region 增、删、改
       /// <summary>
       /// 增、删、改
       /// </summary>
       /// <param name="m">参见模型</param>
       /// <returns>参见模型</returns>
       public static Message Manager(DC_PHOTO_Model m)
       {
           if (m.opMethod == "Add")
           {
               //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.DC_PHOTO.Add(m);
               return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
           }
           if (m.opMethod == "Mdy")
           {
               //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.DC_PHOTO.Mdy(m);
               return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

           }
           if (m.opMethod == "Del")
           {
               //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.DC_PHOTO.Del(m);
               return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
           }
           if (m.opMethod == "MdyTP")
           {
               //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
               Message msgUser = BaseDT.DC_PHOTO.MdyTP(m);
               return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
           }
           return new Message(false, "无效操作", "");


       }

       #endregion

       #region 获取单条
       /// <summary>
       /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <returns>参见模型</returns>
       public static DC_PHOTO_Model getModel(DC_PHOTO_SW sw)
       {
           var result = new List<DC_PHOTO_Model>();

           DataTable dt = BaseDT.DC_PHOTO.getDT(sw);//列表
           DC_PHOTO_Model m = new DC_PHOTO_Model();

           if (dt.Rows.Count > 0)
           {
               int i = 0;
               m.PHOTO_ID = dt.Rows[i]["PHOTO_ID"].ToString();
               m.PHOTOTITLE = dt.Rows[i]["PHOTOTITLE"].ToString();
               m.PHOTOFILENAME = dt.Rows[i]["PHOTOFILENAME"].ToString();
               m.PHOTOEXPLAIN = dt.Rows[i]["PHOTOEXPLAIN"].ToString();
               m.PHOTOTIME =PublicClassLibrary.ClsSwitch.SwitTM( dt.Rows[i]["PHOTOTIME"].ToString());
               m.PHOTOTYPE = dt.Rows[i]["PHOTOTYPE"].ToString();
               m.PRID = dt.Rows[i]["PRID"].ToString();
           }
           dt.Clear();
           dt.Dispose();
           return m;
       }

       #endregion

       #region 获取列表
       /// <summary>
       /// 获取列表
       /// </summary>
       /// <param name="sw"></param>
       /// <returns></returns>
       public static IEnumerable<DC_PHOTO_Model> getModelList(DC_PHOTO_SW sw)
       {
           var result = new List<DC_PHOTO_Model>();
           DataTable dt = BaseDT.DC_PHOTO.getDT(sw);//列表
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               DC_PHOTO_Model m = new DC_PHOTO_Model();
               m.PHOTO_ID = dt.Rows[i]["PHOTO_ID"].ToString();
               m.PHOTOTITLE = dt.Rows[i]["PHOTOTITLE"].ToString();
               m.PHOTOFILENAME = dt.Rows[i]["PHOTOFILENAME"].ToString();
               m.PHOTOEXPLAIN = dt.Rows[i]["PHOTOEXPLAIN"].ToString();
               m.PHOTOTIME = PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["PHOTOTIME"].ToString());
               m.PHOTOTYPE = dt.Rows[i]["PHOTOTYPE"].ToString();
               m.PRID = dt.Rows[i]["PRID"].ToString();
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           return result;
       }

       #endregion


       #region 获取分页
       /// <summary>
       /// 获取分页
       /// </summary>
       /// <param name="sw">参见模型</param>
       /// <param name="total">记录总数</param>
       /// <returns>参见模型</returns>
       public static IEnumerable<DC_PHOTO_Model> getModelList(DC_PHOTO_SW sw, out int total)
       {
           var result = new List<DC_PHOTO_Model>();

           DataTable dt = BaseDT.DC_PHOTO.getDT(sw, out total);//列表
           for (int i = 0; i < dt.Rows.Count; i++)
           {
               DC_PHOTO_Model m = new DC_PHOTO_Model();
               m.PHOTO_ID = dt.Rows[i]["PHOTO_ID"].ToString();
               m.PHOTOTITLE = dt.Rows[i]["PHOTOTITLE"].ToString();
               m.PHOTOFILENAME = dt.Rows[i]["PHOTOFILENAME"].ToString();
               m.PHOTOEXPLAIN = dt.Rows[i]["PHOTOEXPLAIN"].ToString();
               m.PHOTOTIME =PublicClassLibrary.ClsSwitch.SwitTM( dt.Rows[i]["PHOTOTIME"].ToString());
               m.PHOTOTYPE = dt.Rows[i]["PHOTOTYPE"].ToString();
               m.PRID = dt.Rows[i]["PRID"].ToString();
               result.Add(m);
           }
           dt.Clear();
           dt.Dispose();
           return result;
       }
       #endregion
    }
}
