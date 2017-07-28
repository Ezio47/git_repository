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

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 通知公告类
    /// </summary>
    public class T_SYS_NOTICECls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(T_SYS_NOTICE_Model m)
        {
            if (m.opMethod == "Add")
            {
                SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYS_NOTICE.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYS_NOTICE.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);

            }
            if (m.opMethod == "Del")
            {
                SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.T_SYS_NOTICE.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");


        }

        #endregion

        #region 获取单条信息
        /// <summary>
        /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static T_SYS_NOTICE_Model getModel(T_SYS_NOTICE_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_NOTICE.getDT(sw);
            T_SYS_NOTICE_Model m = new T_SYS_NOTICE_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.INFOID = dt.Rows[i]["INFOID"].ToString();
                m.INFOTITLE = dt.Rows[i]["INFOTITLE"].ToString();
                m.INFOCONTENT = dt.Rows[i]["INFOCONTENT"].ToString();
                m.INFOURL = dt.Rows[i]["INFOURL"].ToString();
                m.FBTIME =PublicClassLibrary.ClsSwitch.SwitTM(dt.Rows[i]["FBTIME"].ToString());
                m.LABLE = dt.Rows[i]["LABLE"].ToString();
                m.NUM = dt.Rows[i]["NUM"].ToString();
                m.INFOTYPE = dt.Rows[i]["INFOTYPE"].ToString();
                m.INFOUSERID = dt.Rows[i]["INFOUSERID"].ToString();
                m.SYSFLAG = dt.Rows[i]["SYSFLAG"].ToString();
            }
            return m;
        }

        #endregion

        #region 获取分页信息
        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<T_SYS_NOTICE_Model> getListPagerModel(T_SYS_NOTICE_SW sw, out int total)
        {
            var result = new List<T_SYS_NOTICE_Model>();

            DataTable dt = BaseDT.T_SYS_NOTICE.getDT(sw, out total);//列表

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T_SYS_NOTICE_Model m = new T_SYS_NOTICE_Model();
                m.INFOTITLE = dt.Rows[i]["INFOTITLE"].ToString();
                m.FBTIME = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["FBTIME"].ToString());
                m.LOGINUSERNAME = dt.Rows[i]["LOGINUSERNAME"].ToString();
                m.INFOID = dt.Rows[i]["INFOID"].ToString();
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion
       
    }
}
