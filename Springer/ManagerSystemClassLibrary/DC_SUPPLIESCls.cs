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
    /// 数据中心_物资表
    /// </summary>
     public class DC_SUPPLIESCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_SUPPLIES_Model m)
        {
            if (m.opMethod == "Add")
            {

                Message msgUser = BaseDT.DC_SUPPLIES.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {

                Message msgUser = BaseDT.DC_SUPPLIES.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {

                Message msgUser = BaseDT.DC_SUPPLIES.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");
        }
         #endregion

        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条信息记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DC_SUPPLIES_Model getModel(DC_SUPPLIES_SW sw)
        {
            var result = new List<DC_SUPPLIES_Model>();

            DataTable dt = BaseDT.DC_SUPPLIES.getDT(sw);//列表

            DC_SUPPLIES_Model m = new DC_SUPPLIES_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DCSUPPLIESID = dt.Rows[i]["DCSUPPLIESID"].ToString();
                m.REPID = dt.Rows[i]["REPID"].ToString();
                m.SUPID = dt.Rows[i]["SUPID"].ToString();
                m.DCSUPCOUNT = dt.Rows[i]["DCSUPCOUNT"].ToString();
                m.EQUIPTYPEName = DC_EQUIP_NEWCls.getEQUIPTYPEName(m.DCSUPPLIESID);
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取物资表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_SUPPLIES_Model> getModelList(DC_SUPPLIES_SW sw)
        {
            var result = new List<DC_SUPPLIES_Model>();
            DataTable dt = BaseDT.DC_SUPPLIES.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_SUPPLIES_Model m = new DC_SUPPLIES_Model();
                m.DCSUPPLIESID = dt.Rows[i]["DCSUPPLIESID"].ToString();
                m.REPID = dt.Rows[i]["REPID"].ToString();
                m.SUPID = dt.Rows[i]["SUPID"].ToString();
                m.SUPNAME = DC_SUPPLIESPROPCls.getsupname(m.SUPID);
                m.DCSUPCOUNT = dt.Rows[i]["DCSUPCOUNT"].ToString();
                m.EQUIPTYPEName = DC_EQUIP_NEWCls.getEQUIPTYPEName(m.DCSUPPLIESID);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 出库时物资名称+型号json
        /// <summary>
        /// 获取物资名称+型号json
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string getSupCKJsonStr(DC_SUPPLIES_SW sw,string type)
        {
            DataTable dt = BaseDT.DC_SUPPLIES.getcombox(sw, type);
            char[] specialChars = new char[] { ',' };
            string JSONstring = "[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Id = dt.Rows[i]["SUPID"].ToString();
                string Name = dt.Rows[i]["DCSUPPROPNAME"].ToString();
                string Model = dt.Rows[i]["DCSUPPROPMODEL"].ToString();
                string PRICE = dt.Rows[i]["PRICE"].ToString();
                JSONstring += "{";
                JSONstring += "\"id\":\"" + Id + "\",";
                if (string.IsNullOrEmpty(Model)==false)
                {
                    JSONstring += "\"text\":\"" + Name + "【" + Model + "】" + "￥" + PRICE + "\"";
                }
                else
                {
                    JSONstring += "\"text\":\"" + Name + "【--】" + "￥" + PRICE + "\"";
                }
                JSONstring += "},";
            }
            JSONstring = JSONstring.TrimEnd(specialChars);
            JSONstring += "]";
            return JSONstring.ToString();
        }
        #endregion      

        #region 获取物资数量
         /// <summary>
         /// 获取物资数量
         /// </summary>
         /// <param name="sw"></param>
         /// <returns></returns>
        public static string getcount(DC_SUPPLIES_SW sw) 
        {
            DC_SUPPLIES_Model m = getModel(sw);
            return m.DCSUPCOUNT;
        }
        #endregion

        #region  物资名称下拉框
        /// <summary>
        /// 物资名称下拉框
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getnameSelectOption(DC_SUPPLIES_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.DC_SUPPLIES.getDT(sw);
            if (sw.isShowAll == "1")
                sb.AppendFormat("<option value=\"\">{0}</option>", "--所有--");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string SUPID = dt.Rows[i]["SUPID"].ToString();
                string DCSUPPROPNAME = DC_SUPPLIESPROPCls.getsupname(SUPID);
                string DCmodel = DC_SUPPLIESPROPCls.getsupmodel(SUPID);
                if (string.IsNullOrEmpty(DCmodel) == false)
                {
                    sb.AppendFormat("<option value=\"{0}\">{1}【{2}】</option>", SUPID, DCSUPPROPNAME, DCmodel);
                }
                else
                {
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", SUPID, DCSUPPROPNAME);
                }
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();
        }
        #endregion
    }
}
