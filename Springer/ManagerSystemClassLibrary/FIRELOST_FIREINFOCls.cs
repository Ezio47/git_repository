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
    /// 灾损_火情基本信息表
    /// </summary>
    public class FIRELOST_FIREINFOCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(FIRELOST_FIREINFO_Model m)
        {
            if (m.opMethod == "Init")
            {
                Message msg = BaseDT.FIRELOST_FIREINFO.Init(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Save")
            {
                Message msg = BaseDT.FIRELOST_FIREINFO.Save(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                Message msg = BaseDT.FIRELOST_FIREINFO.Del(m);
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            return new Message(false, "无效操作", "");
        }

        #endregion

        #region 获取单条数据
        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static FIRELOST_FIREINFO_Model getModel(FIRELOST_FIREINFO_SW sw)
        {
            DataTable dt = BaseDT.FIRELOST_FIREINFO.getDT(sw);//列表
            FIRELOST_FIREINFO_Model m = new FIRELOST_FIREINFO_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.TOTALAREA = dt.Rows[i]["TOTALAREA"].ToString();
                m.TOTALPERSON = dt.Rows[i]["TOTALPERSON"].ToString();
                m.TOTALXJL = dt.Rows[i]["TOTALXJL"].ToString();
                m.FIREAREA = dt.Rows[i]["FIREAREA"].ToString();
                m.FIRELOSEAREA = dt.Rows[i]["FIRELOSEAREA"].ToString(); ;
                m.XJLLOSE = dt.Rows[i]["XJLLOSE"].ToString();
                m.CASUALTYCOUNT = dt.Rows[i]["CASUALTYCOUNT"].ToString();
                m.BUILDINGLOSECOUNT = dt.Rows[i]["BUILDINGLOSECOUNT"].ToString();
                m.MACHINERYLOSECOUNT = dt.Rows[i]["MACHINERYLOSECOUNT"].ToString();
                m.TOTALAREAJWDLIST = dt.Rows[i]["TOTALAREAJWDLIST"].ToString();
                m.FIREAREAJWDLIST = dt.Rows[i]["FIREAREAJWDLIST"].ToString();
                m.FIRELOSEAREAJWDLIST = dt.Rows[i]["FIRELOSEAREAJWDLIST"].ToString();
                m.LOSSCOUNT = dt.Rows[i]["LOSSCOUNT"].ToString();
                m.FORESTRESOURCELOSSRATIO = dt.Rows[i]["FORESTRESOURCELOSSRATIO"].ToString();
                m.AVGLOSSPERCATITAVALUE = dt.Rows[i]["AVGLOSSPERCATITAVALUE"].ToString();
                m.WOODLANDLOSSAVGVALUE = dt.Rows[i]["WOODLANDLOSSAVGVALUE"].ToString();
                m.FIRESUPPEFFECTTHAN = dt.Rows[i]["FIRESUPPEFFECTTHAN"].ToString();
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
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_FIREINFO_Model> getListModel(FIRELOST_FIREINFO_SW sw)
        {
            var result = new List<FIRELOST_FIREINFO_Model>();
            DataTable dt = BaseDT.FIRELOST_FIREINFO.getDT(sw);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_FIREINFO_Model m = new FIRELOST_FIREINFO_Model();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.TOTALAREA = dt.Rows[i]["TOTALAREA"].ToString();
                m.TOTALPERSON = dt.Rows[i]["TOTALPERSON"].ToString();
                m.TOTALXJL = dt.Rows[i]["TOTALXJL"].ToString();
                m.FIREAREA = dt.Rows[i]["FIREAREA"].ToString();
                m.FIRELOSEAREA = dt.Rows[i]["FIRELOSEAREA"].ToString(); ;
                m.XJLLOSE = dt.Rows[i]["XJLLOSE"].ToString();
                m.CASUALTYCOUNT = dt.Rows[i]["CASUALTYCOUNT"].ToString();
                m.BUILDINGLOSECOUNT = dt.Rows[i]["BUILDINGLOSECOUNT"].ToString();
                m.MACHINERYLOSECOUNT = dt.Rows[i]["MACHINERYLOSECOUNT"].ToString();
                m.TOTALAREAJWDLIST = dt.Rows[i]["TOTALAREAJWDLIST"].ToString();
                m.FIREAREAJWDLIST = dt.Rows[i]["FIREAREAJWDLIST"].ToString();
                m.FIRELOSEAREAJWDLIST = dt.Rows[i]["FIRELOSEAREAJWDLIST"].ToString();
                m.LOSSCOUNT = dt.Rows[i]["LOSSCOUNT"].ToString();
                m.FORESTRESOURCELOSSRATIO = dt.Rows[i]["FORESTRESOURCELOSSRATIO"].ToString();
                m.AVGLOSSPERCATITAVALUE = dt.Rows[i]["AVGLOSSPERCATITAVALUE"].ToString();
                m.WOODLANDLOSSAVGVALUE = dt.Rows[i]["WOODLANDLOSSAVGVALUE"].ToString();
                m.FIRESUPPEFFECTTHAN = dt.Rows[i]["FIRESUPPEFFECTTHAN"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<FIRELOST_FIREINFO_Model> getListModel(FIRELOST_FIREINFO_SW sw, out int total)
        {
            var result = new List<FIRELOST_FIREINFO_Model>();
            DataTable dt = BaseDT.FIRELOST_FIREINFO.getDT(sw, out total);//列表
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FIRELOST_FIREINFO_Model m = new FIRELOST_FIREINFO_Model();
                m.FIRELOST_FIREINFOID = dt.Rows[i]["FIRELOST_FIREINFOID"].ToString();
                m.JCFID = dt.Rows[i]["JCFID"].ToString();
                m.TOTALAREA = dt.Rows[i]["TOTALAREA"].ToString();
                m.TOTALPERSON = dt.Rows[i]["TOTALPERSON"].ToString();
                m.TOTALXJL = dt.Rows[i]["TOTALXJL"].ToString();
                m.FIREAREA = dt.Rows[i]["FIREAREA"].ToString();
                m.FIRELOSEAREA = dt.Rows[i]["FIRELOSEAREA"].ToString(); ;
                m.XJLLOSE = dt.Rows[i]["XJLLOSE"].ToString();
                m.CASUALTYCOUNT = dt.Rows[i]["CASUALTYCOUNT"].ToString();
                m.BUILDINGLOSECOUNT = dt.Rows[i]["BUILDINGLOSECOUNT"].ToString();
                m.MACHINERYLOSECOUNT = dt.Rows[i]["MACHINERYLOSECOUNT"].ToString();
                m.TOTALAREAJWDLIST = dt.Rows[i]["TOTALAREAJWDLIST"].ToString();
                m.FIREAREAJWDLIST = dt.Rows[i]["FIREAREAJWDLIST"].ToString();
                m.FIRELOSEAREAJWDLIST = dt.Rows[i]["FIRELOSEAREAJWDLIST"].ToString();
                m.LOSSCOUNT = dt.Rows[i]["LOSSCOUNT"].ToString();
                m.FORESTRESOURCELOSSRATIO = dt.Rows[i]["FORESTRESOURCELOSSRATIO"].ToString();
                m.AVGLOSSPERCATITAVALUE = dt.Rows[i]["AVGLOSSPERCATITAVALUE"].ToString();
                m.WOODLANDLOSSAVGVALUE = dt.Rows[i]["WOODLANDLOSSAVGVALUE"].ToString();
                m.FIRESUPPEFFECTTHAN = dt.Rows[i]["FIRESUPPEFFECTTHAN"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取最大灾损火情序号
        /// <summary>
        /// 获取最大灾损火情序号
        /// </summary>
        /// <returns></returns>
         public static int GetMaxFIREINFOID()
        {
            return BaseDT.FIRELOST_FIREINFO.GetMaxFIREINFOID();
        }
        #endregion
    }
}
