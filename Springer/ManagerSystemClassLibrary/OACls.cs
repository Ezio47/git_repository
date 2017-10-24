using OAModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagerSystemClassLibrary.BaseDT;
using DataBaseClassLibrary;
using PublicClassLibrary;
using ManagerSystemModel;
using System.Net;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// OA操作公共方法
    /// </summary>
    public class OACls
    {
        private static OAService.WebServiceSoapClient client = new OAService.WebServiceSoapClient();

        /// <summary>
        /// 判断用户是否已开通OA
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>true：存在;false:不存在</returns>
        public static bool isExists(string userId)
        {
            bool flage = false;
            flage = client.isExists(userId);
            return flage;
        }

        /// <summary>
        /// 开通OA账号
        /// </summary>
        /// <param name="mlist">用户列表</param>
        /// <returns></returns>
        public static Message OpenUsers(List<USERModel> mlist)
        {
            bool flage = false;
            string jsonUsers = ClsJson.EntityToJSON(mlist);
            flage = client.OpenUsers(jsonUsers);
            if (flage == true)
            {
                string useridList = "";
                for (int i = 0; i < mlist.Count; i++)
                {
                    if (i == mlist.Count - 1)
                        useridList = useridList + mlist[i].USERID;
                    else
                        useridList = useridList + mlist[i].USERID + ",";
                }
                //更新元数据库OA账号状态
                Message msgUser = T_SYSSEC_USER.MdyIsOpenOA(useridList, "1");
                return new Message(flage, "开通成功!", "");
            }
            return new Message(flage, "无效操作", "");
        }

        /// <summary>
        /// 禁用OA账号
        /// </summary>
        /// <param name="userIdList">用户ID集合,英文逗号分割</param>
        /// <returns></returns>
        public static Message CloseUsers(string userIdList)
        {
            bool flage = false;
            flage = client.CloseUsers(userIdList);
            if (flage == true)
            {
                //更新元数据库OA账号状态
                Message msgUser = T_SYSSEC_USER.MdyIsOpenOA(userIdList, "0");
                return new Message(flage, "禁用成功!", "");
            }
            return new Message(flage, "无效操作", "");
        }

        /// <summary>
        /// 初始化OA账号密码
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static Message InitPwd(string userId, string pwd)
        {
            bool flage = false;
            flage = client.InitPwd(userId, pwd);
            if (flage == true)
            {
                return new Message(flage, "初始化成功!", "");
            }
            return new Message(flage, "无效操作", "");
        }

        /// <summary>
        /// 获取OA部门信息[部门ID,主管ID,组织架构ID]
        /// </summary>
        /// <param name="sysORGNO">防火系统组织机构ID</param>
        ///  <param name="sysDeptId">防火系统部门ID</param>
        /// <returns></returns>
        public static string[] GetDeptInfo(string sysORGNO, string sysDeptId)
        {
            string[] info = new string[3];
            string OADeptID = BaseDT.T_SYS_Dept_OADept.GetDeptID(sysORGNO, sysDeptId);
            if (OADeptID != "")
            {
                info = client.FindDeptInfo(OADeptID).ToArray();
            }
            return info;
        }

        /// <summary>
        /// 获取OA办公报价数[待办,短信]
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public static int[] GetOfficeNum(string userid)
        {
            int[] nums = new int[2];
            nums = client.GetOfficeNum(userid).ToArray();
            return nums;
        }

        /// <summary>
        /// 获取OA部门下拉框列表
        /// </summary>
        /// <returns></returns>
        public static string GetDeptOption()
        {
            return client.GetDeptOption();
        }

        /// <summary>
        /// 防火系统部门与OA系统部门关联
        /// </summary>
        /// <param name="m">m</param>
        /// <returns></returns>
        public static Message DeptMap(T_SYS_Dept_OADept_Model m)
        {
            return BaseDT.T_SYS_Dept_OADept.DeptMap(m);           
        }

        /// <summary>
        /// 获取OA部门编号
        /// </summary>
        /// <param name="sysORGNO">防火系统组织机构ID</param>
        /// <param name="sysDeptIdList">防火系统部门ID集合,以英文逗号分隔</param>
        /// <returns>以英文逗号分隔的OA部门编码,若没有则用'无'代替</returns>
        public static string FindOADeptBySysDept(string sysORGNO, string sysDeptIdList)
        {
            return BaseDT.T_SYS_Dept_OADept.FindOADeptBySysDept(sysORGNO, sysDeptIdList);
        }
    }
}
