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
    /// 数据中心_出入库明细表
    /// </summary>
    public class DC_DETAILSCls
    {
        #region 出入库
        /// <summary>
        /// 出入库
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_DETAILS_Model m)
        {
            if (m.opMethod == "INPORT")
            {
                if (string.IsNullOrEmpty(m.CountID) == false)
                {
                    string[] arr = m.CountID.Split('|');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (string.IsNullOrEmpty(arr[i]) == false)
                        {
                            string[] brr = arr[i].Split(',');
                            if (string.IsNullOrEmpty(brr[0].ToString()) == false)
                            {
                                m.SUPID = brr[0].ToString();//物资id
                                m.PRICE = brr[1].ToString();
                                m.DCREPSUPCOUNT = brr[2].ToString();
                                m.REPERTORYCOUNT = brr[2].ToString();
                                BaseDT.DC_DETAILS.Add(m);
                                DataTable dt = BaseDT.DC_SUPPLIES.getDT(new DC_SUPPLIES_SW { SUPID = m.SUPID, REPID = m.REPID });
                                if (dt.Rows.Count > 0)
                                {
                                    DC_SUPPLIES_Model m1 = new DC_SUPPLIES_Model();
                                    string id = BaseDT.DC_SUPPLIES.getid(new DC_SUPPLIES_SW { SUPID = m.SUPID, REPID = m.REPID });//物资表id
                                    string DCsupcount = BaseDT.DC_SUPPLIES.getNum(new DC_SUPPLIES_SW { SUPID = m.SUPID, REPID = m.REPID });
                                    m1.DCSUPCOUNT = (Convert.ToInt32(DCsupcount) + Convert.ToInt32(m.DCREPSUPCOUNT)).ToString();
                                    m1.SUPID = m.SUPID;
                                    m1.REPID = m.REPID;
                                    m1.DCSUPPLIESID = id;
                                    m1.PRICE = m.PRICE;
                                    BaseDT.DC_SUPPLIES.Mdy(m1);
                                    DC_EQUIP_NEW_Model m2 = new DC_EQUIP_NEW_Model();
                                    m2.DC_EQUIP_NEW_ID = m1.SUPID;
                                    m2.EQUIPAMOUNT = m1.DCSUPCOUNT;
                                    m2.WORTH = (Convert.ToInt32(m1.PRICE) * Convert.ToInt32(m1.DCSUPCOUNT)).ToString();
                                    m2.REPID = m1.REPID;
                                    BaseDT.DC_EQUIP_NEW.MdyCount(m2);
                                }
                                else
                                {
                                    DC_SUPPLIES_Model m1 = new DC_SUPPLIES_Model();
                                    m1.DCSUPCOUNT = m.DCREPSUPCOUNT;
                                    m1.SUPID = m.SUPID;
                                    m1.REPID = m.REPID;
                                    m1.PRICE = m.PRICE;
                                    BaseDT.DC_SUPPLIES.Add(m1);
                                    DC_EQUIP_NEW_Model m2 = new DC_EQUIP_NEW_Model();
                                    m2.DC_EQUIP_NEW_ID = m1.SUPID;
                                    m2.EQUIPAMOUNT = m1.DCSUPCOUNT;
                                    m2.WORTH = (Convert.ToInt32(m1.PRICE) * Convert.ToInt32(m1.DCSUPCOUNT)).ToString();
                                    m2.REPID = m1.REPID;
                                    BaseDT.DC_EQUIP_NEW.MdyCount(m2);
                                }
                            }
                            else 
                            {
                                return new Message(false, "请选择出库的物资", "");
                            }
                        }
                    }
                }
                return new Message(true, "入库成功", m.NUMBER);
            }
       
            if (m.opMethod == "EXPORT")//出库
            {
                string str = "";
                if (string.IsNullOrEmpty(m.CountID) == false)
                {
                    string[] arr = m.CountID.Split('|');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (string.IsNullOrEmpty(arr[i]) == false)
                        {
                            string[] brr = arr[i].Split(',');
                            if (string.IsNullOrEmpty(brr[0].ToString()) == false)
                            {
                                //m.SUPID = BaseDT.DC_DETAILS.getsupid(brr[0].ToString());
                                m.SUPID = brr[0].ToString();
                                m.DCREPSUPCOUNT = brr[1].ToString();
                                m.PRICE = BaseDT.DC_SUPPLIES.getPrice(new DC_SUPPLIES_SW { SUPID = m.SUPID });
                                //m.PRICE = BaseDT.DC_DETAILS.getprice(brr[0].ToString());

                                DataTable dt = BaseDT.DC_SUPPLIES.getDT(new DC_SUPPLIES_SW { SUPID = m.SUPID, REPID = m.REPID });
                                if (dt.Rows.Count > 0)
                                {
                                    DC_SUPPLIES_Model m1 = new DC_SUPPLIES_Model();
                                    string id = BaseDT.DC_SUPPLIES.getid(new DC_SUPPLIES_SW { SUPID = m.SUPID, REPID = m.REPID });
                                    string DCsupcount = BaseDT.DC_SUPPLIES.getNum(new DC_SUPPLIES_SW { SUPID = m.SUPID, REPID = m.REPID });
                                    //string RECOUNT = BaseDT.DC_DETAILS.getsum(brr[0].ToString());//剩余数量                                    
                                    if (Convert.ToInt32(DCsupcount) >= Convert.ToInt32(m.DCREPSUPCOUNT))//如果剩余数量大于出库数量
                                    {
                                        m.REPERTORYCOUNT = (Convert.ToInt32(DCsupcount) - Convert.ToInt32(m.DCREPSUPCOUNT)).ToString();
                                        m.DCDETAILSID = brr[0].ToString();
                                        BaseDT.DC_DETAILS.Add(m);
                                        BaseDT.DC_DETAILS.Mdy(m);  //更新剩余数量
                                        m1.DCSUPCOUNT = (Convert.ToInt32(DCsupcount) - Convert.ToInt32(m.DCREPSUPCOUNT)).ToString();
                                        m1.SUPID = m.SUPID;
                                        m1.REPID = m.REPID;
                                        m1.DCSUPPLIESID = id;
                                        m1.PRICE = m.PRICE;
                                        BaseDT.DC_SUPPLIES.Mdy(m1);
                                        str = "";
                                        DC_EQUIP_NEW_Model m2 = new DC_EQUIP_NEW_Model();
                                        m2.DC_EQUIP_NEW_ID = m1.SUPID;
                                        m2.EQUIPAMOUNT = m1.DCSUPCOUNT;
                                        m2.WORTH = (Convert.ToInt32(m1.PRICE) * Convert.ToInt32(m1.DCSUPCOUNT)).ToString();
                                        m2.REPID = m1.REPID;
                                        BaseDT.DC_EQUIP_NEW.MdyCount(m2);
                                    }
                                    else
                                    {
                                        return new Message(true, "超过的部分未出库其他的出库成功", m.NUMBER);
                                    }
                                }
                                else
                                {
                                    str = "库存中没有改物资";
                                }
                            }
                            else 
                            {
                                return new Message(false, "请选择出库的物资", "");
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(str) == true)
                {
                    return new Message(true, "出库成功", m.NUMBER);
                }
                if (string.IsNullOrEmpty(str) == false)
                {
                    return new Message(false, str, "");
                }

            }
            return new Message(false, "异常", "");
        }

        #endregion

        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条信息记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DC_DETAILS_Model getModel(DC_DETAILS_SW sw)
        {
            DataTable dt = BaseDT.DC_DETAILS.getDT(sw);//列表
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            DC_DETAILS_Model m = new DC_DETAILS_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DCDETAILSID = dt.Rows[i]["DCDETAILSID"].ToString();
                m.SUPID = dt.Rows[i]["SUPID"].ToString();
                m.SUPNAME = DC_SUPPLIESPROPCls.getsupname(m.SUPID);//获取物资的名称
                m.REPID = dt.Rows[i]["REPID"].ToString();
                m.DPNAME = DC_REPOSITORYCls.getdepotname(m.REPID);//获取仓库的名称
                m.DCSUPPROPMODEL = BaseDT.DC_SUPPLIESPROP.getmodel(new DC_SUPPLIESPROP_SW { DC_SUPPLIESPROP_ID = m.SUPID });//获取物资的型号
                m.DCSUPPROPUNIT = BaseDT.DC_SUPPLIESPROP.getunit(new DC_SUPPLIESPROP_SW { DC_SUPPLIESPROP_ID = m.SUPID });//获取物资的单位
                m.RESPONSIBLEMAN = DC_REPOSITORYCls.getdepotman(m.REPID);//获取仓库负责人
                m.DCREPTIME = ClsSwitch.SwitDate(dt.Rows[i]["DCREPTIME"].ToString()); ;
                m.DCREPFLAG = dt.Rows[i]["DCREPFLAG"].ToString();
                m.DCREPSUPCOUNT = dt.Rows[i]["DCREPSUPCOUNT"].ToString();
                m.DCENTYMANID = dt.Rows[i]["DCENTYMANID"].ToString();
                m.DCUSERID = dt.Rows[i]["DCUSERID"].ToString();
                m.DCCUSTODIANID = dt.Rows[i]["DCCUSTODIANID"].ToString();
                m.DCUSERORG = dt.Rows[i]["DCUSERORG"].ToString();
                m.PRICE = dt.Rows[i]["PRICE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.REPERTORYCOUNT = dt.Rows[i]["REPERTORYCOUNT"].ToString();
                m.DCFAFANGREN = dt.Rows[i]["DCFAFANGREN"].ToString();
                m.DCZHIBIAOREN = dt.Rows[i]["DCZHIBIAOREN"].ToString();
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                //m.DCENTYMANNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DCENTYMANID);
                //m.DCUSERNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DCUSERID);
                //m.DCCUSTODIANNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DCCUSTODIANID);
                m.SUM = (float.Parse(m.DCREPSUPCOUNT) * float.Parse(m.PRICE)).ToString("F2");//金额
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
        public static IEnumerable<DC_DETAILS_Model> getModelList(DC_DETAILS_SW sw)
        {
            var result = new List<DC_DETAILS_Model>();
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            DataTable dt = BaseDT.DC_DETAILS.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_DETAILS_Model m = new DC_DETAILS_Model();
                m.DCDETAILSID = dt.Rows[i]["DCDETAILSID"].ToString();
                m.SUPID = dt.Rows[i]["SUPID"].ToString();
                m.SUPNAME = DC_SUPPLIESPROPCls.getsupname(m.SUPID);
                m.REPID = dt.Rows[i]["REPID"].ToString();
                m.DPNAME = DC_REPOSITORYCls.getdepotname(m.REPID);
                m.DCSUPPROPMODEL = BaseDT.DC_SUPPLIESPROP.getmodel(new DC_SUPPLIESPROP_SW { DC_SUPPLIESPROP_ID = m.SUPID });
                m.DCSUPPROPUNIT = BaseDT.DC_SUPPLIESPROP.getunit(new DC_SUPPLIESPROP_SW { DC_SUPPLIESPROP_ID = m.SUPID });
                m.DCREPTIME = ClsSwitch.SwitDate(dt.Rows[i]["DCREPTIME"].ToString()); ;
                m.DCREPFLAG = dt.Rows[i]["DCREPFLAG"].ToString();
                m.DCREPSUPCOUNT = dt.Rows[i]["DCREPSUPCOUNT"].ToString();
                m.DCENTYMANID = dt.Rows[i]["DCENTYMANID"].ToString();
                m.DCUSERID = dt.Rows[i]["DCUSERID"].ToString();
                m.DCCUSTODIANID = dt.Rows[i]["DCCUSTODIANID"].ToString();
                m.DCUSERORG = dt.Rows[i]["DCUSERORG"].ToString();
                m.PRICE = dt.Rows[i]["PRICE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.REPERTORYCOUNT = dt.Rows[i]["REPERTORYCOUNT"].ToString();
                m.DCFAFANGREN = dt.Rows[i]["DCFAFANGREN"].ToString();
                m.DCZHIBIAOREN = dt.Rows[i]["DCZHIBIAOREN"].ToString();
                //m.DCENTYMANNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DCENTYMANID);
                //m.DCUSERNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DCUSERID);
                //m.DCCUSTODIANNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DCCUSTODIANID);
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.SUM = (float.Parse(m.DCREPSUPCOUNT) * float.Parse(m.PRICE)).ToString("F2");
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return result;
        }

        #endregion

        #region 获取列表分页
        /// <summary>
        /// 获取列表分页
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<DC_DETAILS_Model> getModelPager(DC_DETAILS_SW sw, out int total)
        {
            var result = new List<DC_DETAILS_Model>();
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            DataTable dt = BaseDT.DC_DETAILS.getDT(sw, out total);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_DETAILS_Model m = new DC_DETAILS_Model();
                m.DCDETAILSID = dt.Rows[i]["DCDETAILSID"].ToString();
                m.SUPID = dt.Rows[i]["SUPID"].ToString();
                m.SUPNAME = DC_SUPPLIESPROPCls.getsupname(m.SUPID);
                 m.DCSUPPROPMODEL = BaseDT.DC_SUPPLIESPROP.getmodel(new DC_SUPPLIESPROP_SW { DC_SUPPLIESPROP_ID = m.SUPID });
                 m.DCSUPPROPUNIT = BaseDT.DC_SUPPLIESPROP.getunit(new DC_SUPPLIESPROP_SW { DC_SUPPLIESPROP_ID = m.SUPID });
                m.REPID = dt.Rows[i]["REPID"].ToString();
                m.DPNAME = DC_REPOSITORYCls.getdepotname(m.REPID);
                m.DCREPTIME = ClsSwitch.SwitDate(dt.Rows[i]["DCREPTIME"].ToString()); ;
                m.DCREPFLAG = dt.Rows[i]["DCREPFLAG"].ToString();
                m.DCREPSUPCOUNT = dt.Rows[i]["DCREPSUPCOUNT"].ToString();
                m.DCENTYMANID = dt.Rows[i]["DCENTYMANID"].ToString();
                m.DCUSERID = dt.Rows[i]["DCUSERID"].ToString();
                m.DCCUSTODIANID = dt.Rows[i]["DCCUSTODIANID"].ToString();
                m.DCUSERORG = dt.Rows[i]["DCUSERORG"].ToString();
                m.PRICE = dt.Rows[i]["PRICE"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.REPERTORYCOUNT = dt.Rows[i]["REPERTORYCOUNT"].ToString();
                m.DCFAFANGREN = dt.Rows[i]["DCFAFANGREN"].ToString();
                m.DCZHIBIAOREN = dt.Rows[i]["DCZHIBIAOREN"].ToString();
                //m.DCENTYMANNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DCENTYMANID);
                //m.DCUSERNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DCUSERID);
                //m.DCCUSTODIANNAME = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.DCCUSTODIANID);
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.SUM = (float.Parse(m.DCREPSUPCOUNT) * float.Parse(m.PRICE)).ToString("F2");
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return result;
        }
        #endregion

        #region 出库时物资名称+型号json
        /// <summary>
        /// 获取物资名称+型号json
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getSupCKJsonStr( DC_DETAILS_SW sw)
        {
            DataTable dt = BaseDT.DC_DETAILS.getcombox(sw);
            char[] specialChars = new char[] { ',' };
            string JSONstring = "[";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string detailid = dt.Rows[i]["DCDETAILSID"].ToString();
                string Id = dt.Rows[i]["SUPID"].ToString();
                string Name = dt.Rows[i]["DCSUPPROPNAME"].ToString();
                string Model = dt.Rows[i]["DCSUPPROPMODEL"].ToString();
                string PRICE = dt.Rows[i]["PRICE"].ToString();
                JSONstring += "{";
                JSONstring += "\"id\":\"" + detailid + "\",";
                JSONstring += "\"text\":\"" + Name + "【" + Model + "】" + "￥" + PRICE + "\"";
                JSONstring += "},";
            }
            JSONstring = JSONstring.TrimEnd(specialChars);
            JSONstring += "]";
            return JSONstring.ToString();
        }
        #endregion      

        #region 通过id获取物资id
        /// <summary>
        /// 通过id获取物资id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string getsupid(string id) 
        {
            DC_DETAILS_Model m = getModel(new DC_DETAILS_SW { DCDETAILSID = id });
            return m.SUPID;
        }
        #endregion

        #region 日期转换成数字
        /// <summary>
        /// 日期转换成数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string swDate(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            if (string.IsNullOrEmpty(obj.ToString()) == true)
                return "";

            if (Convert.ToDateTime(obj).ToString("yyyyMMdd") == "19000101")
                return "";
            return Convert.ToDateTime(obj).ToString("yyyyMMdd");
        }
        #endregion

        #region 获取编号
        /// <summary>
        /// 网页自动获取编号
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string getnumber(string number) 
        {
            //string number = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now.ToString());
            string Number ="";
            string num = BaseDT.DC_DETAILS.getNUMBER(new DC_DETAILS_SW { NUMBER = number });
            if (int.Parse(num) >= 10 && int.Parse(num)<=100)
            {
                Number = (DC_DETAILSCls.swDate(DateTime.Now.ToString())).ToString() + "0" + num;
            }
            if (int.Parse(num) < 10) 
            {
                Number = (DC_DETAILSCls.swDate(DateTime.Now.ToString())).ToString() + "00" + num;
            }
            return Number;
        }
        #endregion
    }
}
