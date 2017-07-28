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
    /// 有害生物监测点
    /// </summary>
    public class YHSWJCDCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型Message</returns>
        public static Message Manager(YHSWJCD_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msg = BaseDT.SDE.YHSWJCD.Add(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Mdy")
            {
                Message msg = BaseDT.SDE.YHSWJCD.Mdy(m);
                if (msg.Success == false)
                    return new Message(msg.Success, msg.Msg, "");
                return new Message(msg.Success, msg.Msg, msg.Url);
            }
            if (m.opMethod == "Del")
            {
                return BaseDT.SDE.YHSWJCD.Del(m);
            }
            return new Message(false, "无效操作", "");
        }

        #endregion
    }
}
