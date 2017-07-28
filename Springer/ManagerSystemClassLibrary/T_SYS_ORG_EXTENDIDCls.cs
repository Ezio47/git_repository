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
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OAModel;
using Teleware.Utility;
using Teleware.Security;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 获取T_SYS_ORG_EXTENDID模型
    /// </summary>
    public class T_SYS_ORG_EXTENDIDCls
    {
        /// <summary>
        /// 通过getModel获取模型
        /// </summary>
        public static T_SYS_ORG_EXTENDID_Model getModel(T_SYS_ORG_EXTENDID_SW sw)
        {
            DataTable dt = BaseDT.T_SYS_ORG_EXTENDID.getDT(sw);
            T_SYS_ORG_EXTENDID_Model m = new T_SYS_ORG_EXTENDID_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DGXLAYERNAME = dt.Rows[i]["DGXLAYERNAME"].ToString();
                m.GYLLAYERNAME = dt.Rows[i]["GYLLAYERNAME"].ToString();
            }
            return m;
        }
    }
}
