using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 群组
    /// </summary>
   public class E_GROUPCls
   {
       #region 增、删、改
       /// <summary>
       /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(E_GROUP_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.E_GROUP.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.E_GROUP.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.E_GROUP.Del(m);
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
        public static E_GROUP_Model getModel(E_GROUP_SW sw)
        {
            var result = new List<E_GROUP_Model>();

            DataTable dt = BaseDT.E_GROUP.getDT(sw);//列表
            E_GROUP_Model m = new E_GROUP_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.EGROUPID = dt.Rows[i]["EGROUPID"].ToString();
                m.EGROUPUSERID = dt.Rows[i]["EGROUPUSERID"].ToString();
                m.EGROUPMEMBERLIST = dt.Rows[i]["EGROUPMEMBERLIST"].ToString();
                m.EGROUPNAME = dt.Rows[i]["EGROUPNAME"].ToString();
                m.EGROUPTYPE = dt.Rows[i]["EGROUPTYPE"].ToString();
                m.EGROUPPHONELIST = dt.Rows[i]["EGROUPPHONELIST"].ToString();
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
        public static IEnumerable<E_GROUP_Model> getModelList(E_GROUP_SW sw)
        {
            var result = new List<E_GROUP_Model>();
            DataTable dt = BaseDT.E_GROUP.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                E_GROUP_Model m = new E_GROUP_Model();
                m.EGROUPID = dt.Rows[i]["EGROUPID"].ToString();
                m.EGROUPUSERID = dt.Rows[i]["EGROUPUSERID"].ToString();
                m.EGROUPMEMBERLIST = dt.Rows[i]["EGROUPMEMBERLIST"].ToString();
                m.EGROUPNAME = dt.Rows[i]["EGROUPNAME"].ToString();
                m.EGROUPTYPE = dt.Rows[i]["EGROUPTYPE"].ToString();
                m.EGROUPPHONELIST = dt.Rows[i]["EGROUPPHONELIST"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取带分页列表
        /// <summary>
        /// 获取收藏短信
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<E_GROUP_Model> getModelListpager(E_GROUP_SW sw, out int total)
        {
            total = 0;
            var result = new List<E_GROUP_Model>();
            DataTable dt = BaseDT.E_GROUP.getDT(sw, out total);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                E_GROUP_Model m = new E_GROUP_Model();
                m.EGROUPID = dt.Rows[i]["EGROUPID"].ToString();
                m.EGROUPUSERID = dt.Rows[i]["EGROUPUSERID"].ToString();
                m.EGROUPMEMBERLIST = dt.Rows[i]["EGROUPMEMBERLIST"].ToString();
                m.EGROUPNAME = dt.Rows[i]["EGROUPNAME"].ToString();
                m.EGROUPTYPE = dt.Rows[i]["EGROUPTYPE"].ToString();
                m.EGROUPPHONELIST = dt.Rows[i]["EGROUPPHONELIST"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
    }
}
