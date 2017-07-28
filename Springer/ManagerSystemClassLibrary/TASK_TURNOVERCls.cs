using ManagerSystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 系统管理_任务流转
    /// </summary>
    public class TASK_TURNOVERCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">护林员模型</param>
        /// <returns></returns>
        public static Message Manager(T_IPSFR_USER_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.T_IPSFR_USER.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");
        } 
        #endregion
    }
}
