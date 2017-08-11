using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;

namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 单位管理
    /// </summary>
    public class T_SYS_ORG
    {
        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(T_SYS_ORGModel m)
        {
            if (isExists(new T_SYS_ORGSW { ORGNO = m.ORGNO, }) == true)
                return new Message(false, "添加失败，该组织机构码已存在！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO T_SYS_ORG(ORGNO, ORGNAME, ORGDUTY, LEADER, AREACODE, SYSFLAG,ORGJC,JD, WD,COMMANDNAME,WEATHERJC,POSTCODE,DUTYTELL,FAX,WXJC,ADDRESS )");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.ORGNO));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORGNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORGDUTY));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LEADER));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.AREACODE));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ORGJC));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.COMMANDNAME));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WEATHERJC));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.POSTCODE));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.DUTYTELL));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.FAX));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.WXJC));
            sb.AppendFormat(",{0}", ClsSql.saveNullField(m.ADDRESS));
            sb.AppendFormat(")");
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", m.returnUrl);
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", m.returnUrl);
        }

        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(T_SYS_ORGModel m)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("UPDATE T_SYS_ORG");
            sb.AppendFormat(" set ");
            sb.AppendFormat("ORGNO='{0}'", ClsSql.EncodeSql(m.ORGNO));
            sb.AppendFormat(",ORGNAME='{0}'", ClsSql.EncodeSql(m.ORGNAME));
            sb.AppendFormat(",ORGDUTY='{0}'", ClsSql.EncodeSql(m.ORGDUTY));
            sb.AppendFormat(",LEADER='{0}'", ClsSql.EncodeSql(m.LEADER));
            sb.AppendFormat(",AREACODE='{0}'", ClsSql.EncodeSql(m.AREACODE));
            sb.AppendFormat(",SYSFLAG='{0}'", ClsSql.EncodeSql(m.SYSFLAG));
            sb.AppendFormat(",ORGJC='{0}'", ClsSql.EncodeSql(m.ORGJC));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",COMMANDNAME={0}", ClsSql.saveNullField(m.COMMANDNAME));
            sb.AppendFormat(",WEATHERJC={0}", ClsSql.saveNullField(m.WEATHERJC));
            sb.AppendFormat(",POSTCODE={0}", ClsSql.saveNullField(m.POSTCODE));
            sb.AppendFormat(",DUTYTELL={0}", ClsSql.saveNullField(m.DUTYTELL));
            sb.AppendFormat(",FAX={0}", ClsSql.saveNullField(m.FAX));
            sb.AppendFormat(",WXJC={0}", ClsSql.saveNullField(m.WXJC));
            sb.AppendFormat(",ADDRESS={0}", ClsSql.saveNullField(m.ADDRESS));
            sb.AppendFormat(" where ORGNO='{0}'", ClsSql.EncodeSql(m.ORGNO));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", m.returnUrl);
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", m.returnUrl);
        }

        #endregion

        #region 护林员参数修改
        /// <summary>
        /// 修改护林员参数
        /// </summary>
        /// <param name="m">对象</param>
        /// <returns></returns>
        public static Message update(T_SYS_ORGModel m)
        {
            StringBuilder sb = new StringBuilder();
            var orgNo = m.ORGNO;
            sb.AppendFormat("UPDATE T_SYS_ORG");
            sb.AppendFormat(" set ");
            sb.AppendFormat("MOBILEPARAMLIST='{0}'", ClsSql.EncodeSql(m.MOBILEPARAMLIST));
            if (orgNo.Substring(4, 11) == "00000000000")//获取所有市的
            {
                var str = orgNo.Substring(0, 4) + "%";
                sb.AppendFormat(" where ORGNO LIKE '{0}'", str);
            }
            else if (orgNo.Substring(6, 9) == "000000000")//获取所有县的
            {
                var str = orgNo.Substring(0, 6) + "%";
                sb.AppendFormat(" where ORGNO LIKE '{0}'", str);
            }
            else if (orgNo.Substring(9,6)=="0000000")//获取所有乡镇的
            {
                var str = orgNo.Substring(0, 9) + "%";
                sb.AppendFormat(" where ORGNO LIKE '{0}'", str);
            }
            else{
                sb.AppendFormat(" where ORGNO='{0}'", ClsSql.EncodeSql(m.ORGNO));
            }
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", m.returnUrl);
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", m.returnUrl);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(T_SYS_ORGModel m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete T_SYS_ORG");
            sb.AppendFormat(" where ORGNO= '{0}'", ClsSql.EncodeSql(m.ORGNO));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", m.returnUrl);
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", m.returnUrl);
        }

        #endregion

        #region 判断记录是否存在
        /// <summary>
        /// 判断记录是否存在
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>true存在 false不存在</returns>
        public static bool isExists(T_SYS_ORGSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select 1 from T_SYS_ORG where 1=1");
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
                sb.AppendFormat(" and ORGNO='{0}'", ClsSql.EncodeSql(sw.ORGNO));
            return DataBaseClass.JudgeRecordExists(sb.ToString());
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DataTable getDT(T_SYS_ORGSW sw)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ORGNO, ORGNAME, ORGDUTY, LEADER, AREACODE, ORGJC,SYSFLAG,JD, WD,COMMANDNAME,WEATHERJC,POSTCODE,DUTYTELL,FAX,MOBILEPARAMLIST,WXJC,ADDRESS");
            sb.AppendFormat(" FROM      T_SYS_ORG");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.SYSFLAG) == false)
                sb.AppendFormat(" AND SYSFLAG = '{0}'", sw.SYSFLAG);
            if (string.IsNullOrEmpty(sw.ORGNO) == false)
                sb.AppendFormat(" AND ORGNO = '{0}'", ClsSql.EncodeSql(sw.ORGNO));
            if (string.IsNullOrEmpty(sw.AREACODE) == false)
                sb.AppendFormat(" AND AREACODE='{0}'", ClsSql.EncodeSql(sw.AREACODE));
            if (string.IsNullOrEmpty(sw.ORGNAME) == false)
                sb.AppendFormat(" AND ORGNAME like '%{0}%'", ClsSql.EncodeSql(sw.ORGNAME));
            if (string.IsNullOrEmpty(sw.TopORGNO) == false)
            {
                if (sw.TopORGNO.Substring(4, 11) == "00000000000")//获取所有市的
                    sb.AppendFormat(" AND SUBSTRING(ORGNO,1,4) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 4)));
                else if (sw.TopORGNO.Substring(6, 9) == "000000000")//获取所有县的
                    sb.AppendFormat(" AND SUBSTRING(ORGNO,1,6) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 6)));
                else if (sw.TopORGNO.Substring(9, 6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND SUBSTRING(ORGNO,1,9) = '{0}'", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 9)));
                else
                    sb.AppendFormat(" AND ORGNO = '{0}'", ClsSql.EncodeSql(sw.TopORGNO));
            }
            if (string.IsNullOrEmpty(sw.TopSXORGNO) == false)
            {
                if (sw.TopSXORGNO.Substring(4, 11) == "00000000000")//获取所有市下面的县
                    sb.AppendFormat(" AND (SUBSTRING(ORGNO,1,4)='{0}' and SUBSTRING(ORGNO,5,2)<>'00' and SUBSTRING(ORGNO,7,3) = '000')", ClsSql.EncodeSql(sw.TopSXORGNO.Substring(0, 4)));
                else
                    sb.AppendFormat(" AND (SUBSTRING(ORGNO,1,6)='{0}' and SUBSTRING(ORGNO,7,3) = '000')", ClsSql.EncodeSql(sw.TopSXORGNO.Substring(0, 6)));
            }
            if (string.IsNullOrEmpty(sw.TopEchartORGNO) == false)
            {
                if (sw.TopEchartORGNO.Substring(4, 11) == "00000000000")//获取所有市下面的县以及市
                    sb.AppendFormat(" AND (SUBSTRING(ORGNO,1,4)='{0}' and SUBSTRING(ORGNO,7,3) = '000')", ClsSql.EncodeSql(sw.TopEchartORGNO.Substring(0, 4)));
                else if (sw.TopEchartORGNO.Substring(6, 9) == "000000000")//获取县以及县下面的镇
                    sb.AppendFormat(" AND (SUBSTRING(ORGNO,1,6)='{0}')", ClsSql.EncodeSql(sw.TopEchartORGNO.Substring(0, 6)));
                else if (sw.TopEchartORGNO.Substring(9, 6) == "000000")//获取所有乡镇的
                    sb.AppendFormat(" AND SUBSTRING(ORGNO,1,9) = '{0}'", ClsSql.EncodeSql(sw.TopEchartORGNO.Substring(0, 9)));
                else
                    sb.AppendFormat(" AND ORGNO='{0}'", ClsSql.EncodeSql(sw.TopEchartORGNO));
            }
            if (!string.IsNullOrEmpty(sw.GetContyORGNOByCity))//市获取所有县
                sb.AppendFormat(" AND (SUBSTRING(ORGNO,1,4)='{0}' and SUBSTRING(ORGNO,5,2)<>'00' and SUBSTRING(ORGNO,7,9) = '000000000')", ClsSql.EncodeSql(sw.GetContyORGNOByCity.Substring(0, 4)));
            if (!string.IsNullOrEmpty(sw.GetXZOrgNOByConty)) //县获取所有乡镇
                sb.AppendFormat(" AND (SUBSTRING(ORGNO,1,6)='{0}' and SUBSTRING(ORGNO,7,9) != '000000000')", ClsSql.EncodeSql(sw.GetXZOrgNOByConty.Substring(0, 6)));
            if (string.IsNullOrEmpty(sw.OnlyGetShi) == false) //只获取市
                sb.AppendFormat(" AND (SUBSTRING(ORGNO,1,4)='{0}' and SUBSTRING(ORGNO,5,11) = '00000000000')", ClsSql.EncodeSql(sw.OnlyGetShi.Substring(0, 4)));
            if (string.IsNullOrEmpty(sw.OnlyGetShiXian) == false) //只获取市、县
                sb.AppendFormat(" AND (SUBSTRING(ORGNO,1,4)='{0}' and SUBSTRING(ORGNO,7,9) = '000000000')", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 4)));
            if (string.IsNullOrEmpty(sw.OnlyGetXianXZ) == false) //只获取县、乡镇
                sb.AppendFormat(" AND (SUBSTRING(ORGNO,1,6)='{0}' and SUBSTRING(ORGNO,7,9) = '000000000')", ClsSql.EncodeSql(sw.TopORGNO.Substring(0, 6)));
            if (sw.IsEnableCUN == "1")
            {

            }
            else
            {
                sb.AppendFormat(" AND SUBSTRING(ORGNO,10,15) = '{0}'", "000000");

            }
            sb.AppendFormat(" ORDER BY ORGNO");
            DataSet ds = DataBaseClass.FullDataSet(sb.ToString());
            return ds.Tables[0];
        }
        #endregion

        #region 根据编码获取名称
        /// <summary>
        /// 根据编码获取名称
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="value"></param>
        /// <returns>参见模型</returns>
        public static string getName(DataTable dt, string value)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(value))
                return "";
            string str = "";
            DataRow[] dr = dt.Select("ORGNO='" + value + "'");
            if (dr.Length > 0)
                str = dr[0]["ORGNAME"].ToString();
            return str;
        }

        /// <summary>
        /// 根据编码获取名称
        /// </summary>
        /// <param name="value">编码</param>
        /// <returns></returns>
        public static string getName(string value)
        {
            string sql = " Select ORGNAME from T_SYS_ORG where ORGNO='" + value + "'";
            return DataBaseClass.ReturnSqlField(sql);
        }

        #endregion

        #region 根据编码获取所属县市名称
        /// <summary>
        /// 根据编码获取所属县市名称
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="value"></param>
        /// <returns>参见模型</returns>
        public static string getSXName(DataTable dt, string value)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(value) || value.Length != 15)
                return "";
            string str = "";
            string ogntrans = "";
            if (value.Substring(6, 9) == "00000000000")
            {
                ogntrans = value;
            }
            else
            {
                ogntrans = value.Substring(0, 6) + "000000000";
            }
            DataRow[] dr = dt.Select("ORGNO='" + ogntrans + "'");
            if (dr.Length > 0)
                str = dr[0]["ORGNAME"].ToString();
            return str;
        }

        #endregion

        #region 根据多个编码获取名称组合
        /// <summary>
        /// 根据编码获取名称
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="value"></param>
        /// <returns>参见模型</returns>
        public static string getNames(DataTable dt, string value)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(value))
                return "";
            string str = "";
            string[] arr = value.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                DataRow[] dr = dt.Select("ORGNO='" + arr[i] + "'");
                if (dr.Length > 0)
                {
                    str += (string.IsNullOrEmpty(str)) ? dr[0]["ORGNAME"].ToString() : "," + dr[0]["ORGNAME"].ToString();
                }
            }
            return str;
        }

        #endregion

        #region 根据编码获取行政区划编码
        /// <summary>
        /// 根据编码获取行政区划编码
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="value"></param>
        /// <returns>参见模型</returns>

        public static string getAreaCode(DataTable dt, string value)
        {
            if (dt == null)
                return "";
            if (string.IsNullOrEmpty(value))
                return "";
            string str = "";
            DataRow[] dr = dt.Select("ORGNO='" + value + "'");
            if (dr.Length > 0)
                str = dr[0]["AREACODE"].ToString();
            return str;
        }

        #endregion

        #region 根据机构名称返回机构编码
        /// <summary>
        /// 根据机构名称返回机构编码
        /// </summary>
        /// <param name="name"></param>
        /// <returns>参见模型</returns>
        public static string getCodeByName(string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT    ORGNO");
            sb.AppendFormat(" FROM      T_SYS_ORG");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(name))
                return "";

            sb.AppendFormat(" AND ORGNAME = '{0}'", name.Trim());
            return DataBaseClassLibrary.DataBaseClass.ReturnSqlField(sb.ToString());

        }

        #endregion

        #region 根据县名和乡镇名称获取乡镇名称编码
        /// <summary>
        /// 根据县名和乡镇名称获取乡镇名称编码
        /// </summary>
        /// <param name="xianName">县名</param>
        ///<param name="zhenName">乡镇</param>
        /// <returns></returns>
        public static string getOrgNoByXianNameAndZhenName(string xianName, string zhenName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT ORGNO FROM      T_SYS_ORG where SUBSTRING(ORGNO,1,6)=(SELECT SUBSTRING(ORGNO,1,6) FROM      T_SYS_ORG where ORGNAME='{0}') and ORGNAME='{1}'", xianName, zhenName);
            return DataBaseClass.ReturnSqlField(sb.ToString());
        }
        #endregion
    }
}
