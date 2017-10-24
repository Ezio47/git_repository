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
    /// 有害生物_危害等级表
    /// </summary>
    public class PEST_HARMCLASSCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message Manager(PEST_HARMCLASS_Model m)
        {
            Message msg = BaseDT.PEST_HARMCLASS.Save(m);//保存更新二维
            if (msg.Success)
            {
                Message msgAce = BaseDT.PEST_HARMCLASS.UpdateAceHarmClass(m);//更新三维库
            }
            return new Message(msg.Success, msg.Msg, msg.Url);
        }

        /// <summary>
        /// 删除火险等级
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message DeleteHarmClassData(PEST_HARMCLASS_Model m)
        {
            return BaseDT.PEST_HARMCLASS.DeleteByDCDATE(m);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static Message ImportData(PEST_HARMCLASS_Model m)
        {
            return BaseDT.PEST_HARMCLASS.AddImport(m);
        }

        public static Message UpdateAceHarmClass(PEST_HARMCLASS_Model m)
        {
            return BaseDT.PEST_HARMCLASS.UpdateAceHarmClass_XZ(m);
        }
        #endregion

        #region 获取数据模型
        /// <summary>
        /// 获取数据模型
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static PEST_HARMCLASS_Model getModel(PEST_HARMCLASS_SW sw)
        {
            PEST_HARMCLASS_Model m = new PEST_HARMCLASS_Model();
            DataTable dt = BaseDT.PEST_HARMCLASS.getDT(sw);
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.PEST_HARMCLASSID = dt.Rows[i]["PEST_HARMCLASSID"].ToString();
                m.DCDATE = ClsSwitch.SwitDate(dt.Rows[i]["DCDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TOWNNAME = dt.Rows[i]["TOWNNAME"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.HARMCLASS = dt.Rows[i]["HARMCLASS"].ToString();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 获取数据列表
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<PEST_HARMCLASS_Model> getListModel(PEST_HARMCLASS_SW sw)
        {
            var result = new List<PEST_HARMCLASS_Model>();
            DataTable dt = BaseDT.PEST_HARMCLASS.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_HARMCLASS_Model m = new PEST_HARMCLASS_Model();
                m.PEST_HARMCLASSID = dt.Rows[i]["PEST_HARMCLASSID"].ToString();
                m.DCDATE = ClsSwitch.SwitDate(dt.Rows[i]["DCDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TOWNNAME = dt.Rows[i]["TOWNNAME"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.HARMCLASS = dt.Rows[i]["HARMCLASS"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        /// <summary>
        /// 获取最新数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<PEST_HARMCLASS_Model> getTopListModel(PEST_HARMCLASS_SW sw)
        {
            var result = new List<PEST_HARMCLASS_Model>();
            DataTable dt = BaseDT.PEST_HARMCLASS.getTopDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PEST_HARMCLASS_Model m = new PEST_HARMCLASS_Model();
                m.PEST_HARMCLASSID = dt.Rows[i]["PEST_HARMCLASSID"].ToString();
                m.DCDATE = ClsSwitch.SwitDate(dt.Rows[i]["DCDATE"].ToString());
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.TOWNNAME = dt.Rows[i]["TOWNNAME"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.HARMCLASS = dt.Rows[i]["HARMCLASS"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取OBJECTID列表
        /// <summary>
        /// 获取OBJECTID列表,以英文逗号分割
        /// </summary>
        /// <param name="orgNo">机构编码</param>
        /// <param name="dcDate">日期</param>
        /// <returns></returns>
        public static string GetOBJECTIDStr(string orgNo, string dcDate)
        {
            string str = "";
            string[] arrOrgNo = orgNo.Split(',');
            string[] arrDcDate = dcDate.Split(',');
            DataTable dt = BaseDT.PEST_HARMCLASS.getDT(new PEST_HARMCLASS_SW());
            for (int i = 0; i < arrOrgNo.Length - 1; i++)
            {
                str = str + BaseDT.PEST_HARMCLASS.getID(dt, arrOrgNo[i], arrDcDate[i]) + ",";
            }
            return str;
        }
        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在 </returns>
        public static bool isExists(PEST_HARMCLASS_SW sw)
        {
            return BaseDT.PEST_HARMCLASS.isExists(sw);
        }
        #endregion
    }
}
