using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using ManagerSystemSearchWhereModel;
using ManagerSystemModel;
using DataBaseClassLibrary;
using PublicClassLibrary;


namespace ManagerSystemClassLibrary.BaseDT
{
    /// <summary>
    /// 监测_电子监控基本信息表
    /// </summary>
    public class JC_MONITOR_BASICINFO
    {
        #region 增加

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Add(JC_MONITOR_BASICINFO_Model m)
        {
            if (PublicCls.OrgIsZhen(m.BYORGNO) == false)
                return new Message(false, "添加失败，所属单位必须为乡镇！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("INSERT INTO  JC_MONITOR_BASICINFO(TTBH, EMNAME, BYORGNO, JD, WD, GC, ADDRESS, IP, LOGINUSERNAME, USERPWD, XH, PP, GD, JCJL,OBJID,TEMPLATEDID,PORT,TYPE)");
            sb.AppendFormat("output inserted.EMID ");
            sb.AppendFormat("VALUES(");
            sb.AppendFormat("'{0}'", ClsSql.EncodeSql(m.TTBH));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.EMNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.BYORGNO));
            if (string.IsNullOrEmpty(m.JD))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JD));
            if (string.IsNullOrEmpty(m.WD))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.WD));
            if (string.IsNullOrEmpty(m.GC))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.GC));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.ADDRESS));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.IP));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.LOGINUSERNAME));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.USERPWD));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.XH));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PP));
            if (string.IsNullOrEmpty(m.GD))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.GD));
            if (string.IsNullOrEmpty(m.JCJL))
                sb.AppendFormat(",null");
            else
                sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.JCJL));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.OBJID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TEMPLATEDID));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.PORT));
            sb.AppendFormat(",'{0}'", ClsSql.EncodeSql(m.TYPE));
            sb.AppendFormat(")");
            string strid = DataBaseClass.ReturnSqlField(sb.ToString());
            if (!string.IsNullOrEmpty(strid))
                return new Message(true, "添加成功！", strid);
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
 
        }

        /// <summary>
        /// 添加三维库
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <param name="emid">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message AddSHIPINGJIANKONG(JC_MONITOR_BASICINFO_Model m,string emid)
        {
            if (PublicCls.OrgIsZhen(m.BYORGNO) == false)
                return new Message(false, "添加失败，所属单位必须为乡镇！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert into SHIPINGJIANKONG(OBJECTID,BYORGNO,NAME,JD,WD,Shape,ADDRESS) values(");
            sb.AppendFormat("{0},", ClsSql.saveNullField(emid));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.BYORGNO));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.EMNAME));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.JD));
            sb.AppendFormat("{0},", ClsSql.saveNullField(m.WD));
            sb.AppendFormat("{0},", m.Shape);
            sb.AppendFormat("{0})", ClsSql.saveNullField(m.ADDRESS));

            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "添加成功！", "");
            else
                return new Message(false, "添加失败，请检查各输入框是否正确！", "");
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Mdy(JC_MONITOR_BASICINFO_Model m)
        {//, XH, PP, GD, JCJL
            if (PublicCls.OrgIsZhen(m.BYORGNO) == false)
                return new Message(false, "修改失败，所属单位必须为乡镇！", "");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE JC_MONITOR_BASICINFO");
            sb.AppendFormat(" set ");
            sb.AppendFormat("TTBH='{0}'", ClsSql.EncodeSql(m.TTBH));
            sb.AppendFormat(",EMNAME='{0}'", ClsSql.EncodeSql(m.EMNAME));
            sb.AppendFormat(",BYORGNO='{0}'", ClsSql.EncodeSql(m.BYORGNO));
            if (string.IsNullOrEmpty(m.JD) == false)
                sb.AppendFormat(",JD='{0}'", ClsSql.EncodeSql(m.JD));
            if (string.IsNullOrEmpty(m.WD) == false)
                sb.AppendFormat(",WD='{0}'", ClsSql.EncodeSql(m.WD));
            if (string.IsNullOrEmpty(m.GC) == false)
                sb.AppendFormat(",GC='{0}'", ClsSql.EncodeSql(m.GC));
            sb.AppendFormat(",ADDRESS='{0}'", ClsSql.EncodeSql(m.ADDRESS));
            sb.AppendFormat(",IP='{0}'", ClsSql.EncodeSql(m.IP));
            sb.AppendFormat(",LOGINUSERNAME='{0}'", ClsSql.EncodeSql(m.LOGINUSERNAME));
            sb.AppendFormat(",USERPWD='{0}'", ClsSql.EncodeSql(m.USERPWD));
            sb.AppendFormat(",XH='{0}'", ClsSql.EncodeSql(m.XH));
            sb.AppendFormat(",PP='{0}'", ClsSql.EncodeSql(m.PP));
            if (string.IsNullOrEmpty(m.GD) == false)
                sb.AppendFormat(",GD='{0}'", ClsSql.EncodeSql(m.GD));
            if (string.IsNullOrEmpty(m.JCJL) == false)
                sb.AppendFormat(",JCJL='{0}'", ClsSql.EncodeSql(m.JCJL));
            sb.AppendFormat(",OBJID='{0}'", ClsSql.EncodeSql(m.OBJID));
            sb.AppendFormat(",TEMPLATEDID='{0}'", ClsSql.EncodeSql(m.TEMPLATEDID));
            sb.AppendFormat(",PORT='{0}'", ClsSql.EncodeSql(m.PORT));
            sb.AppendFormat(",TYPE='{0}'", ClsSql.EncodeSql(m.TYPE));
            sb.AppendFormat(" where EMID= '{0}'", ClsSql.EncodeSql(m.EMID));

            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 修改空间库
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message MdySHIPINGJIANKONG(JC_MONITOR_BASICINFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE SHIPINGJIANKONG");
            sb.AppendFormat(" set ");
            sb.AppendFormat("NAME={0}", ClsSql.saveNullField(m.EMNAME));
            sb.AppendFormat(",BYORGNO={0}", ClsSql.saveNullField(m.BYORGNO));
            sb.AppendFormat(",JD={0}", ClsSql.saveNullField(m.JD));
            sb.AppendFormat(",WD={0}", ClsSql.saveNullField(m.WD));
            sb.AppendFormat(",ADDRESS={0}", ClsSql.saveNullField(m.ADDRESS));
            sb.AppendFormat(",Shape={0}", m.Shape);
            sb.AppendFormat(" where OBJECTID= {0}", ClsSql.saveNullField(m.EMID));
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "修改成功！", "");
            else
                return new Message(false, "修改失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Del(JC_MONITOR_BASICINFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete JC_MONITOR_BASICINFO");
            sb.AppendFormat(" where EMID= '{0}'", ClsSql.EncodeSql(m.EMID));
            bool bln = DataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message DelSHIPINGJIANKONG(JC_MONITOR_BASICINFO_Model m)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("delete SHIPINGJIANKONG");
            sb.AppendFormat(" where OBJECTID= '{0}'", ClsSql.EncodeSql(m.EMID));
            bool bln = SDEDataBaseClass.ExeSql(sb.ToString());
            if (bln == true)
                return new Message(true, "删除成功！", "");
            else
                return new Message(false, "删除失败，请检查各输入框是否正确！", "");
        }

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns>参见模型</returns>
        public static DataTable getDT(JC_MONITOR_BASICINFO_SW sw)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendFormat(" FROM      JC_MONITOR_BASICINFO a");
            sb.AppendFormat(" WHERE   1=1");
            if (string.IsNullOrEmpty(sw.EMID) == false)
                sb.AppendFormat(" AND EMID = '{0}'", ClsSql.EncodeSql(sw.EMID));
            if (string.IsNullOrEmpty(sw.TTBH) == false)
                sb.AppendFormat(" AND TTBH  like '%{0}%'", ClsSql.EncodeSql(sw.TTBH));
            if (string.IsNullOrEmpty(sw.BYORGNO) == false)
            {

                if (PublicCls.OrgIsShi(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like '{0}%'", PublicCls.getShiIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsXian(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getXianIncOrgNo(sw.BYORGNO));
                }
                else if (PublicCls.OrgIsZhen(sw.BYORGNO))
                {
                    sb.AppendFormat(" and BYORGNO like  '{0}%'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }
                else
                {
                    //sb.AppendFormat(" and BYORGNO = '{0}'", PublicCls.getZhenIncOrgNo(sw.BYORGNO));
                }

            }
            string sql = "SELECT EMID, TTBH, EMNAME, BYORGNO, JD, WD, GC, ADDRESS, IP, LOGINUSERNAME, USERPWD, XH, PP, GD, JCJL,OBJID,TEMPLATEDID,PORT,TYPE"
                + sb.ToString()
                + " order by BYORGNO";
            DataSet ds = DataBaseClass.FullDataSet(sql);
            return ds.Tables[0];
        }

        #endregion
    }
}
