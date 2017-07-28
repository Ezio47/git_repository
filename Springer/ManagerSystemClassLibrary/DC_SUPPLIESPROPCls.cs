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
    /// 数据中心_物资属性表
    /// </summary>
    public class DC_SUPPLIESPROPCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_SUPPLIESPROP_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.DC_SUPPLIESPROP.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.DC_SUPPLIESPROP.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.DC_SUPPLIESPROP.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdyunit")
            {
                Message msgUser = BaseDT.DC_SUPPLIESPROP.Mdyunit(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
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
        public static DC_SUPPLIESPROP_Model getModel(DC_SUPPLIESPROP_SW sw)
        {
            var result = new List<DC_SUPPLIESPROP_Model>();

            DataTable dt = BaseDT.DC_SUPPLIESPROP.getDT(sw);//列表
            DC_SUPPLIESPROP_Model m = new DC_SUPPLIESPROP_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DC_SUPPLIESPROP_ID = dt.Rows[i]["DC_SUPPLIESPROP_ID"].ToString();
                m.DCSUPPROPNAME = dt.Rows[i]["DCSUPPROPNAME"].ToString();
                m.DCSUPPROPMODEL = dt.Rows[i]["DCSUPPROPMODEL"].ToString();
                m.DCSUPPROPUNIT = dt.Rows[i]["DCSUPPROPUNIT"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.DCSUPPROPFACTORY = dt.Rows[i]["DCSUPPROPFACTORY"].ToString();
                m.REPERTORYCOUNT = BaseDT.DC_SUPPLIES.getNum(new DC_SUPPLIES_SW { SUPID = m.DC_SUPPLIESPROP_ID });
                m.PRICE = BaseDT.DC_SUPPLIES.getPrice(new DC_SUPPLIES_SW { SUPID = m.DC_SUPPLIESPROP_ID });
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
        public static IEnumerable<DC_SUPPLIESPROP_Model> getModelList(DC_SUPPLIESPROP_SW sw)
        {
            var result = new List<DC_SUPPLIESPROP_Model>();

            DataTable dt = BaseDT.DC_SUPPLIESPROP.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_SUPPLIESPROP_Model m = new DC_SUPPLIESPROP_Model();
                m.DC_SUPPLIESPROP_ID = dt.Rows[i]["DC_SUPPLIESPROP_ID"].ToString();
                m.DCSUPPROPNAME = dt.Rows[i]["DCSUPPROPNAME"].ToString();
                m.DCSUPPROPMODEL = dt.Rows[i]["DCSUPPROPMODEL"].ToString();
                m.DCSUPPROPUNIT = dt.Rows[i]["DCSUPPROPUNIT"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.DCSUPPROPFACTORY = dt.Rows[i]["DCSUPPROPFACTORY"].ToString();
                m.REPERTORYCOUNT = BaseDT.DC_SUPPLIES.getNum(new DC_SUPPLIES_SW { SUPID = m.DC_SUPPLIESPROP_ID });
                m.PRICE = BaseDT.DC_SUPPLIES.getPrice(new DC_SUPPLIES_SW { SUPID = m.DC_SUPPLIESPROP_ID });
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 入库时物资名称+型号json
        /// <summary>
        /// 获取物资名称+型号json
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getSupMOJsonStr(DC_SUPPLIESPROP_SW sw)
        {
            DataTable dt = BaseDT.DC_SUPPLIESPROP.getDT(sw);
            char[] specialChars = new char[] { ',' };
            string JSONstring = "[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Id = dt.Rows[i]["DC_SUPPLIESPROP_ID"].ToString();
                string Name = dt.Rows[i]["DCSUPPROPNAME"].ToString();
                string Model = dt.Rows[i]["DCSUPPROPMODEL"].ToString();
                JSONstring += "{";
                JSONstring += "\"id\":\"" + Id + "\",";
                JSONstring += "\"text\":\"" + Name + "【" + Model + "】\"";
                JSONstring += "},";
            }
            JSONstring = JSONstring.TrimEnd(specialChars);
            JSONstring += "]";
            return JSONstring.ToString();
        }
        #endregion

        #region  物资单位下拉框
        /// <summary>
        /// 单位下拉框
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getSelectOption(DC_SUPPLIESPROP_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.DC_SUPPLIESPROP.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string DCSUPPROPUNIT = dt.Rows[i]["DCSUPPROPUNIT"].ToString();

                sb.AppendFormat("<option value=\"{0}\">{1}</option>", DCSUPPROPUNIT, DCSUPPROPUNIT);
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();

        }
        #endregion

        #region 通过物资id获取物资名称
        /// <summary>
        /// 物资id获取物资名称
        /// </summary>
        /// <param name="supid"></param>
        /// <returns></returns>
        public static string getsupname(string supid)
        {
            DC_SUPPLIESPROP_Model m = getModel(new DC_SUPPLIESPROP_SW { DC_SUPPLIESPROP_ID = supid });
            return m.DCSUPPROPNAME;
        }
        #endregion

        #region 通过物资id获取物资型号
        /// <summary>
        /// 物资id获取物资名称
        /// </summary>
        /// <param name="supid"></param>
        /// <returns></returns>
        public static string getsupmodel(string supid)
        {
            DC_SUPPLIESPROP_Model m = getModel(new DC_SUPPLIESPROP_SW { DC_SUPPLIESPROP_ID = supid });
            return m.DCSUPPROPMODEL;
        }
        #endregion

        #region  物资名称下拉框
        /// <summary>
        /// 物资名称下拉框
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getnameSelectOption(DC_SUPPLIESPROP_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.DC_SUPPLIESPROP.getDT(sw);
            if (sw.isShowAll == "1")
                sb.AppendFormat("<option value=\"\">{0}</option>", "--所有--");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string DCSUPPROPNAME = dt.Rows[i]["DCSUPPROPNAME"].ToString();
                string DC_SUPPLIESPROP_ID = dt.Rows[i]["DC_SUPPLIESPROP_ID"].ToString();
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", DC_SUPPLIESPROP_ID, DCSUPPROPNAME);
            }
            dt.Clear();
            dt.Dispose();
            return sb.ToString();

        }
        #endregion
    }
}
