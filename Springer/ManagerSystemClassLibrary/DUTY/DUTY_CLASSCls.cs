using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemClassLibrary.DUTY
{
    /// <summary>
    /// 值班管理表
    /// </summary>
    public class DUTY_CLASSCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DUTY_CLASS_Model m)
        {
            if (m.opMethod == "Add")
            {
                Message msgUser = BaseDT.Duty.DUTY_CLASS.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            else if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.Duty.DUTY_CLASS.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作", "");
        } 
        #endregion

        #region 根据班次判断是否早退 bool isEarlyOut(OD_CLASS_SW sw)
        /// <summary>
        /// 根据班次判断是否早退
        /// </summary>
        /// <param name="sw">ONDUTYCLASSID 必传 某一班次 curTime 要判断的时间 默认为空取当前时间</param>
        /// <returns>true 早退 false 早退</returns>
        public static bool isEarlyOut(DUTY_CLASS_SW sw)
        {

            DUTY_CLASS_Model m = GetModel(sw);//获取该班次信息
            if (string.IsNullOrEmpty(sw.curTime))
                sw.curTime = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);
            if (string.IsNullOrEmpty(sw.judgeDate))
                sw.judgeDate = PublicClassLibrary.ClsSwitch.SwitDate(DateTime.Now);
            //班次开始时间
            DateTime dtB = Convert.ToDateTime(PublicClassLibrary.ClsSwitch.SwitDate(sw.judgeDate) + " " + m.DUTYBEGINTIME);
            //班次结束时间
            DateTime dtE = Convert.ToDateTime(PublicClassLibrary.ClsSwitch.SwitDate(sw.judgeDate) + " " + m.DUTYENDTIME);
            if (PublicClassLibrary.ClsSwitch.compDate(dtB, dtE, "1") == false)//如果结束时间小于开始时间，则结束时间加1天，即为跨天的时间
                dtE = dtE.AddDays(1);
            if (PublicClassLibrary.ClsSwitch.compDate(sw.curTime, dtE, "1") == true)
                return true;
            else
                return false;
            //return bln;

        }
        #endregion

        #region 根据班次判断是否迟到 bool isLate(DUTY_CLASS_SW sw)
        /// <summary>
        /// 根据班次判断是否迟到
        /// </summary>
        /// <param name="sw">ONDUTYCLASSID 必传 某一班次 curTime 要判断的时间 默认为空取当前时间</param>
        /// <returns>true 迟到 false 未迟到</returns>
        public static bool isLate(DUTY_CLASS_SW sw)
        {

            DUTY_CLASS_Model m = GetModel(sw);//获取该班次信息
            if (string.IsNullOrEmpty(sw.curTime))
                sw.curTime = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now);
            //班次开始时间
            DateTime dtB = Convert.ToDateTime(PublicClassLibrary.ClsSwitch.SwitDate(sw.curTime) + " " + m.DUTYBEGINTIME);
            //班次结束时间
            DateTime dtE = Convert.ToDateTime(PublicClassLibrary.ClsSwitch.SwitDate(sw.curTime) + " " + m.DUTYENDTIME);
            if (PublicClassLibrary.ClsSwitch.compDate(dtB, dtE, "1") == false)//如果结束时间小于开始时间，则结束时间加1天，即为跨天的时间
                dtE = dtE.AddDays(1);
            if (PublicClassLibrary.ClsSwitch.compDate(sw.curTime, dtE, "1") == true)
                return false;
            else
                return true;
            //return bln;

        }
        #endregion

        #region 获取单条 IEnumerable<DUTY_CLASS_Model> GetModelList(DUTY_CLASS_SW sw)
        /// <summary>
        /// 获取单条
        /// </summary>
        /// <returns></returns>
        public static DUTY_CLASS_Model GetModel(DUTY_CLASS_SW sw)
        {
            var dt = BaseDT.Duty.DUTY_CLASS.GetDT(sw);
            DUTY_CLASS_Model m = new DUTY_CLASS_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.DUTYCLASSID = dt.Rows[i]["DUTYCLASSID"].ToString();
                m.DUTYCLASSNAME = dt.Rows[i]["DUTYCLASSNAME"].ToString();
                m.DUTYBEGINTIME = dt.Rows[i]["DUTYBEGINTIME"].ToString();
                m.DUTYENDTIME = dt.Rows[i]["DUTYENDTIME"].ToString();
            }
            return m;
        }
        #endregion

        #region 获取列表 IEnumerable<DUTY_CLASS_Model> GetModelList(DUTY_CLASS_SW sw)
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<DUTY_CLASS_Model> GetListModel(DUTY_CLASS_SW sw)
        {
            var result = new List<DUTY_CLASS_Model>();
            var dt = BaseDT.Duty.DUTY_CLASS.GetDT(sw);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DUTY_CLASS_Model m = new DUTY_CLASS_Model();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                m.DUTYCLASSID = dt.Rows[i]["DUTYCLASSID"].ToString();
                m.DUTYCLASSNAME = dt.Rows[i]["DUTYCLASSNAME"].ToString();
                m.DUTYBEGINTIME = dt.Rows[i]["DUTYBEGINTIME"].ToString();
                m.DUTYENDTIME = dt.Rows[i]["DUTYENDTIME"].ToString();
                result.Add(m);
            }
            return result;
        }
        #endregion
    }
}
