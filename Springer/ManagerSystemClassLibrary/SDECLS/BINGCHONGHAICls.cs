using ManagerSystemModel;
using ManagerSystemModel.SDEModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.SDECLS
{
    /// <summary>
    /// 三维--病虫害
    /// </summary>
    public class BINGCHONGHAICls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(BINGCHONGHAI_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.SDE.BINGCHONGHAI.Add(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.SDE.BINGCHONGHAI.Mdy(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                return BaseDT.SDE.BINGCHONGHAI.Del(m);
            }
            return new Message(false, "无效操作", "");
        }

        #endregion

        #region 获取最大空间库编号
        /// <summary>
        /// 获取最大空间库编号
        /// </summary>
        /// <returns></returns>
        public static int GetMaxOBJECTID()
        {
            return BaseDT.SDE.BINGCHONGHAI.GetMaxOBJECTID();
        }
        #endregion
    }
}
