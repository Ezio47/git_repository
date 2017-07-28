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

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 收件箱
    /// </summary>
    public class E_RECEIVE_Cls
    {
        #region 增、删

        /// <summary>
        /// 增、删
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(E_RECEIVE_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.E_RECEIVE.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Mdy")
            {
                Message msgUser = BaseDT.E_RECEIVE.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "SendMdy")
            {
                Message msgUser = BaseDT.E_RECEIVE.SendMdy(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.E_RECEIVE.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<E_RECEIVE_Model> getListModel(E_RECEIVE_SW sw)
        {
            DataTable dt = BaseDT.E_RECEIVE.getDT(sw);//列表
            var result = new List<E_RECEIVE_Model>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                E_RECEIVE_Model m = new E_RECEIVE_Model();
                m.ERID = dt.Rows[i]["ERID"].ToString();
                m.BYEMAILID = dt.Rows[i]["BYEMAILID"].ToString();
                m.RECEIVETYPE = dt.Rows[i]["RECEIVETYPE"].ToString();
                m.EMAILRECEIVESTATUS = dt.Rows[i]["EMAILRECEIVESTATUS"].ToString();
                m.EMAILSENDTIME = ClsSwitch.SwitTM(dt.Rows[i]["EMAILSENDTIME"].ToString());
                m.EMAILRECEIVETIME = ClsSwitch.SwitTM(dt.Rows[i]["EMAILRECEIVETIME"].ToString());
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 查看某一邮件信息
        /// <summary>
        /// 查看某一邮件信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static E_RECEIVE_Model getModel(E_RECEIVE_SW sw)
        {
            DataTable dt = BaseDT.E_RECEIVE.getDT(sw);//列表
            E_RECEIVE_Model m = new E_RECEIVE_Model();
            if(dt.Rows.Count>0)
            {
                int i = 0;
                m.ERID = dt.Rows[i]["ERID"].ToString();
                m.BYEMAILID = dt.Rows[i]["BYEMAILID"].ToString();
                m.RECEIVETYPE = dt.Rows[i]["RECEIVETYPE"].ToString();
                m.EMAILRECEIVESTATUS = dt.Rows[i]["EMAILRECEIVESTATUS"].ToString();
                m.EMAILSENDTIME = ClsSwitch.SwitTM(dt.Rows[i]["EMAILSENDTIME"].ToString());
                m.EMAILRECEIVETIME = ClsSwitch.SwitTM(dt.Rows[i]["EMAILRECEIVETIME"].ToString());
                m.SubjectModel = E_SUBJECTCls.getModel(new E_SUBJECT_SW { EMAILID = m.BYEMAILID });
                m.FileModel = E_FILECls.getListModel(new E_File_SW { BYEMAILID = m.BYEMAILID });
                if (m.EMAILRECEIVESTATUS == "0")//未读
                {
                    BaseDT.E_RECEIVE.Mdy(new E_RECEIVE_Model { 
                        EMAILRECEIVESTATUS="1",
                        EMAILRECEIVETIME=ClsSwitch.SwitTM(DateTime.Now),
                        ERID = m.ERID 
                    });
                }
            }
            dt.Clear();
            dt.Dispose();
            return m;
        }
        #endregion

        #region 获取分页列表
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<E_RECEIVE_Model> getListModel(E_RECEIVE_SW sw, out int total)
        {
            DataTable dt = BaseDT.E_RECEIVE.getDT(sw, out total);//列表
            var result = new List<E_RECEIVE_Model>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                E_RECEIVE_Model m = new E_RECEIVE_Model();
                m.ERID = dt.Rows[i]["ERID"].ToString();
                m.BYEMAILID = dt.Rows[i]["BYEMAILID"].ToString();
                m.RECEIVETYPE = dt.Rows[i]["RECEIVETYPE"].ToString();
                m.EMAILRECEIVESTATUS = dt.Rows[i]["EMAILRECEIVESTATUS"].ToString();
                m.EMAILSENDTIME = ClsSwitch.SwitTM(dt.Rows[i]["EMAILSENDTIME"].ToString());
                m.EMAILRECEIVETIME = ClsSwitch.SwitTM(dt.Rows[i]["EMAILRECEIVETIME"].ToString());
                m.SubjectModel = E_SUBJECTCls.getModel(new E_SUBJECT_SW { EMAILID = m.BYEMAILID }); 
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 统计未读的邮件数量
        /// <summary>
        ///统计未读的邮件数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.E_RECEIVE.getNum(new E_RECEIVE_SW{EMAILRECEIVESTATUS= "0",RECEIVEUSERID = SystemCls.getUserID()});
            return Count;
        }
        #endregion
    }
}
