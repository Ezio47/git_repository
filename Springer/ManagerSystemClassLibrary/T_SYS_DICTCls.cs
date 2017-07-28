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

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 数据字典管理
    /// </summary>
    public class T_SYS_DICTCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_SYS_DICTModel m)
        {
            if (m.opMethod == "Add")
            {
                return BaseDT.T_SYS_DICT.Add(m);
            }
            if (m.opMethod == "Mdy")
            {
                return BaseDT.T_SYS_DICT.Mdy(m);
            }
            if (m.opMethod == "Del")
            {
                return BaseDT.T_SYS_DICT.Del(m);
            }
            return new Message(false, "无效操作", "");
        }
        #endregion

        #region 字典类别下拉框
        /// <summary>
        /// 字典类别下拉框
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getSelectOption(T_SYS_DICTTYPE_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_SYS_DICT.getDICTFLAGDT(sw);
            if (sw.isShowAll == "1")
                sb.AppendFormat("<option value=\"\">{0}</option>", "--所有--");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (sw.DICTTYPEID == dt.Rows[i]["DICTTYPEID"].ToString())
                    sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", dt.Rows[i]["DICTTYPEID"].ToString(), dt.Rows[i]["DICTTYPENAME"].ToString());
                else
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", dt.Rows[i]["DICTTYPEID"].ToString(), dt.Rows[i]["DICTTYPENAME"].ToString());
            }
            return sb.ToString();
        }
        #endregion

        #region 字典名称下拉框
        /// <summary>
        /// 字典名称下拉框
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static string getSelectOption(T_SYS_DICTSW sw)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = sw.DICTTYPEID, DICTTYPENAME = sw.DICTTYPENAME, DICTFLAG = sw.DICTFLAG, STANDBY1 = sw.STANDBY1 });
            if (sw.isShowAll == "1")
                sb.AppendFormat("<option value=\"\">{0}</option>", "--所有--");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string text = dt.Rows[i]["DICTNAME"].ToString();
                if (sw.STANDBY1InName == "1")
                    text = dt.Rows[i]["STANDBY1"].ToString() + "-" + text;
                if (sw.DICTVALUE == dt.Rows[i]["DICTVALUE"].ToString())
                    sb.AppendFormat("<option value=\"{0}\" selected>{1}</option>", dt.Rows[i]["DICTVALUE"].ToString(), text);
                else
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>", dt.Rows[i]["DICTVALUE"].ToString(), text);
            }
            return sb.ToString();
        }
        #endregion

        #region 获取字典类别单条记录
        /// <summary>
        /// 获取字典类别单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYS_DICTTYPE_Model getTypeModel(T_SYS_DICTTYPE_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_DICT.getDICTFLAGDT(sw);
            T_SYS_DICTTYPE_Model m = new T_SYS_DICTTYPE_Model();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                m.DICTTYPEID = dt.Rows[i]["DICTTYPEID"].ToString();
                m.DICTTYPERID = dt.Rows[i]["DICTTYPERID"].ToString();
                m.DICTTYPENAME = dt.Rows[i]["DICTTYPENAME"].ToString();
                m.SYSFLAG = dt.Rows[i]["SYSFLAG"].ToString();
                m.ISMAN = dt.Rows[i]["ISMAN"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.DICTTYPEListModel = getTypeListModel(new T_SYS_DICTTYPE_SW { DICTTYPERID = m.DICTTYPEID }).ToList();
                if (m.DICTTYPEListModel.Count() > 0)
                {
                    foreach (var type in m.DICTTYPEListModel)
                    {
                        type.DICTListModel = getListModel(new T_SYS_DICTSW { DICTTYPEID = type.DICTTYPEID }).ToList();
                    }
                }
                m.DICTListModel = getListModel(new T_SYS_DICTSW { DICTTYPEID = m.DICTTYPEID }).ToList();
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取字典类别数据列表
        /// <summary>
        /// 获取字典类别数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYS_DICTTYPE_Model> getTypeListModel(T_SYS_DICTTYPE_SW sw)
        {
            var result = new List<T_SYS_DICTTYPE_Model>();
            DataTable dt = BaseDT.T_SYS_DICT.getDICTFLAGDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_DICTTYPE_Model m = new T_SYS_DICTTYPE_Model();
                m.DICTTYPEID = dt.Rows[i]["DICTTYPEID"].ToString();
                m.DICTTYPENAME = dt.Rows[i]["DICTTYPENAME"].ToString();
                m.SYSFLAG = dt.Rows[i]["SYSFLAG"].ToString();
                m.ISMAN = dt.Rows[i]["ISMAN"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        #endregion

        #region 获取字典单条记录
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYS_DICTModel getModel(T_SYS_DICTSW sw)
        {
            DataTable dt = BaseDT.T_SYS_DICT.getDT(sw);
            T_SYS_DICTModel m = new T_SYS_DICTModel();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                //数据库表字段
                m.DICTID = dt.Rows[i]["DICTID"].ToString();
                m.DICTTYPEID = dt.Rows[i]["DICTTYPEID"].ToString();
                m.DICTNAME = dt.Rows[i]["DICTNAME"].ToString();
                m.DICTVALUE = dt.Rows[i]["DICTVALUE"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.STANDBY1 = dt.Rows[i]["STANDBY1"].ToString();
                m.STANDBY2 = dt.Rows[i]["STANDBY2"].ToString();
                m.STANDBY3 = dt.Rows[i]["STANDBY3"].ToString();
                m.STANDBY4 = dt.Rows[i]["STANDBY4"].ToString();
                //扩充字段
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }

        #endregion

        #region 获取字典数据列表
        /// <summary>
        /// 获取字典数据列表
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYS_DICTModel> getListModel(T_SYS_DICTSW sw)
        {
            var result = new List<T_SYS_DICTModel>();
            DataTable dt = BaseDT.T_SYS_DICT.getDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_DICTModel m = new T_SYS_DICTModel();
                m.DICTID = dt.Rows[i]["DICTID"].ToString();
                m.DICTTYPEID = dt.Rows[i]["DICTTYPEID"].ToString();
                m.DICTNAME = dt.Rows[i]["DICTNAME"].ToString();
                m.DICTVALUE = dt.Rows[i]["DICTVALUE"].ToString();
                m.ORDERBY = dt.Rows[i]["ORDERBY"].ToString();
                m.STANDBY1 = dt.Rows[i]["STANDBY1"].ToString();
                m.STANDBY2 = dt.Rows[i]["STANDBY2"].ToString();
                m.STANDBY3 = dt.Rows[i]["STANDBY3"].ToString();
                m.STANDBY4 = dt.Rows[i]["STANDBY4"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取字典类别名称
        /// <summary>
        /// 获取字典类别名称
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static string getDicTypeName(T_SYS_DICTTYPE_SW sw)
        {
            return BaseDT.T_SYS_DICT.getDicTypeName(sw);
        }
        #endregion

        #region 获取字典名称
        /// <summary>
        /// 获取字典名称
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns></returns>
        public static string getDicName(T_SYS_DICTSW sw)
        {
            return BaseDT.T_SYS_DICT.getName(sw);
        }
        #endregion

        #region 获取字典值字符
        /// <summary>
        ///  获取字典值字符串,英文逗号分割
        /// </summary>
        /// <param name="_list">字典列表</param>
        public static string getDicValueStr(List<T_SYS_DICTModel> _list)
        {
            string str = "";
            for (int i = 0; i < _list.Count; i++)
            {
                if (i != _list.Count - 1)
                    str += _list[i].DICTVALUE + ",";
                else
                    str += _list[i].DICTVALUE;
            }
            return str;
        }
        #endregion

        #region 获取字典类别值字符
        /// <summary>
        ///  获取字典类别值字符,英文逗号分割
        /// </summary>
        /// <param name="_list">字典类别列表</param>
        public static string getDicTypeValueStr(List<T_SYS_DICTTYPE_Model> _list)
        {
            string str = "";
            for (int i = 0; i < _list.Count; i++)
            {
                if (i != _list.Count - 1)
                    str += _list[i].DICTTYPEID + ",";
                else
                    str += _list[i].DICTTYPEID;
            }
            return str;
        }
        #endregion

        #region 树形菜单
        /// <summary>
        /// 类别树形菜单
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>string</returns>
        public static string getTypeTree(T_SYS_DICTTYPE_SW sw)
        {
            JArray childobjArray = new JArray();
            DataTable dt = BaseDT.T_SYS_DICT.getDICTFLAGDT(sw);
            if (string.IsNullOrEmpty(sw.DICTTYPERID))
                sw.DICTTYPERID = "0";
            JArray JA = getTreeChild(sw, dt, sw.DICTTYPERID);//, dctypetopid);
            dt.Clear();
            dt.Dispose();
            return JsonConvert.SerializeObject(JA);
        }

        /// <summary>
        /// 获取类别子菜单
        /// </summary>
        /// <param name="sw">sw</param>
        /// <param name="dt">dt</param>
        /// <param name="DICTTYPERID">DICTTYPERID</param>
        /// <returns></returns>
        private static JArray getTreeChild(T_SYS_DICTTYPE_SW sw, DataTable dt, string DICTTYPERID)
        {
            JArray childobjArray = new JArray();
            DataRow[] dr = dt.Select("DICTTYPERID = " + DICTTYPERID + "", "ORDERBY");
            for (int i = 0; i < dr.Length; i++)
            {
                string DICTTYPEID = dr[i]["DICTTYPEID"].ToString();
                DataRow[] dr1 = dt.Select("DICTTYPERID=" + dr[i]["DICTTYPEID"].ToString());
                if (dr1.Count() > 0)
                    DICTTYPEID = "";
                JObject root = new JObject { { "id", DICTTYPEID }, { "text", dr[i]["DICTTYPENAME"].ToString() }, { "state", "open" }, };
                root.Add("children", getTreeChild(sw, dt, dr[i]["DICTTYPEID"].ToString()));
                childobjArray.Add(root);
            }
            return childobjArray;
        }

        #endregion
    }
}
