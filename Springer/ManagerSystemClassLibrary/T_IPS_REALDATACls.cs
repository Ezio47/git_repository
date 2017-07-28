using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 坐标上传原始记录公共类
    /// </summary>
    public class T_IPS_REALDATACls
    {
        #region 获取电量

        /// <summary>
        /// 获取电量
        /// </summary>
        /// <example>
        /// sw.SearchTime   查询日期，查询某日的电量信息
        /// sw.USERID       护林员序号（多序号以逗号分隔）
        /// sw.DateBegin    开始日期
        /// sw.DateEnd      结束日期
        /// </example>
        /// <param name="sw">参见条件模型T_IPS_REALDATASW</param>
        /// <returns>参见模型T_IPS_REALDATAModel</returns>
        public static IEnumerable<T_IPS_REALDATAModel> getElectricModelList(T_IPS_REALDATASW sw)
        {
            var result = new List<T_IPS_REALDATAModel>();
            //获取当前登录用户有权限查看的组织机构
            DataTable dtOrg = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });
            //获取所有有权限查看的护林员
            DataTable dtFRUser = BaseDT.T_IPSFR_USER.getDT(new T_IPSFR_USER_SW { ISENABLE = "1", HID = sw.HID });
            DataTable dt = BaseDT.T_IPS_REALDATA.getDT(sw);
            DataRow[] dr = dt.Select("", "SBTIME desc");
            for (int i = 0; i < dr.Length; i++)
            {
                if (string.IsNullOrEmpty(dr[i]["ELECTRIC"].ToString()) == false)
                {
                    T_IPS_REALDATAModel m = new T_IPS_REALDATAModel();

                    m.REALDATAID = dr[i]["REALDATAID"].ToString();
                    m.PHONE = dr[i]["PHONE"].ToString();//电话号码
                    m.LONGITUDE = dr[i]["LONGITUDE"].ToString();
                    m.LATITUDE = dr[i]["LATITUDE"].ToString();
                    m.HEIGHT = dr[i]["HEIGHT"].ToString();
                    m.ELECTRIC = dr[i]["ELECTRIC"].ToString();
                    m.SPEED = dr[i]["SPEED"].ToString();
                    m.DIRECTION = dr[i]["DIRECTION"].ToString();
                    m.SBTIME = dr[i]["SBTIME"].ToString();
                    if (string.IsNullOrEmpty(m.SBTIME) == false)
                        m.SBTIME = PublicClassLibrary.ClsSwitch.SwitTM(m.SBTIME);
                    m.NOTE = dr[i]["NOTE"].ToString();

                    DataRow[] drFRUser = dtFRUser.Select("PHONE='" + m.PHONE + "'");
                    if (drFRUser.Length > 0)
                    {
                        m.HID = drFRUser[0]["HID"].ToString();//护林员编号
                        m.HNAME = drFRUser[0]["HNAME"].ToString();//护林员名称
                        m.ORGNO = drFRUser[0]["BYORGNO"].ToString();//组织机构编码
                        m.ORGNAME = BaseDT.T_SYS_ORG.getName(dtOrg, m.ORGNO);//组织机构名称
                    }
                    result.Add(m);
                }
            }
            dtOrg.Clear();
            dtOrg.Dispose();
            dtFRUser.Clear();
            dtFRUser.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion


        #region 获取实时轨迹
        /// <summary>
        /// 根据电话号码获取实时传输数据
        /// </summary>
        /// <example>
        /// sw.SearchTime   查询日期，查询某日的电量信息
        /// sw.USERID       护林员序号（多序号以逗号分隔）
        /// sw.DateBegin    开始日期
        /// sw.DateEnd      结束日期
        /// </example>
        /// <param name="sw">参见条件模型T_IPS_REALDATASW</param>
        /// <returns>参见模型T_IPS_REALDATAModel</returns>
        public static IEnumerable<T_IPS_REALDATAModel> getModelList(T_IPS_REALDATASW sw)
        {
            var result = new List<T_IPS_REALDATAModel>();
            DataTable dt = BaseDT.T_IPS_REALDATA.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_IPS_REALDATAModel m = new T_IPS_REALDATAModel();
                m.REALDATAID = dt.Rows[i]["REALDATAID"].ToString();
                m.PHONE = dt.Rows[i]["PHONE"].ToString();
                m.LONGITUDE = dt.Rows[i]["LONGITUDE"].ToString();//经度
                m.LATITUDE = dt.Rows[i]["LATITUDE"].ToString();//纬度
                if (sw.MapType != "Skyline")
                {
                    //******************计算坐标偏移量
                    string[] arr = PublicCls.switJWD(m.LATITUDE, m.LONGITUDE);
                    m.LATITUDE = arr[0];
                    m.LONGITUDE = arr[1];
                    //******************计算坐标偏移量 End
                }
                m.HEIGHT = dt.Rows[i]["HEIGHT"].ToString();
                m.ELECTRIC = dt.Rows[i]["ELECTRIC"].ToString();
                m.SPEED = dt.Rows[i]["SPEED"].ToString();
                m.DIRECTION = dt.Rows[i]["DIRECTION"].ToString();
                m.SBTIME = dt.Rows[i]["SBTIME"].ToString();
                if (string.IsNullOrEmpty(m.SBTIME) == false)
                    m.SBTIME = PublicClassLibrary.ClsSwitch.SwitTM(m.SBTIME);
                m.NOTE = dt.Rows[i]["NOTE"].ToString();


                result.Add(m);
            }
            //dtHUser.Clear();
            //dtHUser.Dispose();
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
