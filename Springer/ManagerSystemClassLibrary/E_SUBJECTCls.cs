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
    /// 主题表
    /// </summary>
    public class E_SUBJECTCls
    {
        #region 增、删
        /// <summary>
        /// 增、删
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(E_SUBJECT_Model m)
        {
            if (m.opMethod == "Add")//保存为草稿
            {
                //没有附件
                if (string.IsNullOrEmpty(m.EMAILID) == true)
                {
                    Message msg = BaseDT.E_SUBJECT.Add(m);
                    //获取主题ID
                    string EMAILid = BaseDT.E_SUBJECT.getID(new E_SUBJECT_SW { EMAILSENDUSERID = m.EMAILSENDUSERID, EMAILTIME = m.EMAILTIME });
                    if (msg.Success == true)
                        return new Message(msg.Success, "草稿保存成功!", m.returnUrl);
                    else
                        return new Message(msg.Success, "草稿保存失败,请重试!", m.returnUrl);
                }
                //有附件的情况更新库
                if (string.IsNullOrEmpty(m.EMAILID) == false)
                {
                    Message msg = BaseDT.E_SUBJECT.Mdy(m);
                    if (msg.Success == true)
                        return new Message(msg.Success, "草稿保存成功!", m.returnUrl);
                    else
                        return new Message(msg.Success, "草稿保存失败,请重试!", m.returnUrl);
                }
            }
            if (m.opMethod == "AddToSend")//草稿直接发送
            {
                #region 草稿直接发送
                BaseDT.E_SUBJECT.Mdy(m);//主题表 修改为已发送
                if (string.IsNullOrEmpty(m.EMAILRECUSERLIST) == false)
                {
                    //接收信息表中录入数据 接收人
                    string[] arr = m.EMAILRECUSERLIST.Split(',');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        BaseDT.E_RECEIVE.Add(new E_RECEIVE_Model
                        {
                            BYEMAILID = m.EMAILID,
                            RECEIVETYPE = "0",   //接收人类别
                            RECEIVEUSERID = arr[i],//接收人序号
                            EMAILRECEIVESTATUS = "0",//接收状态
                            EMAILSENDTIME = m.EMAILTIME
                        });
                    }
                }
                if (string.IsNullOrEmpty(m.EMAILCOPYUSERLIST) == false)
                {
                    //接收信息表中录入数据 抄送人 
                    string[] arr = m.EMAILCOPYUSERLIST.Split(',');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        BaseDT.E_RECEIVE.Add(new E_RECEIVE_Model
                        {
                            BYEMAILID = m.EMAILID,
                            RECEIVETYPE = "1",   //接收人类别
                            RECEIVEUSERID = arr[i],//接收人序号
                            EMAILRECEIVESTATUS = "0",//接收状态
                            EMAILSENDTIME = m.EMAILTIME
                        });
                    }
                }
                if (string.IsNullOrEmpty(m.EMAILSECRETUSERLIST) == false)
                {
                    //接收信息表中录入数据 密送人 
                    string[] arr = m.EMAILSECRETUSERLIST.Split(',');
                    for (int i = 0; i < arr.Length; i++)
                    {
                        BaseDT.E_RECEIVE.Add(new E_RECEIVE_Model
                        {
                            BYEMAILID = m.EMAILID,
                            RECEIVETYPE = "2",   //接收人类别
                            RECEIVEUSERID = arr[i],//接收人序号
                            EMAILRECEIVESTATUS = "0",//接收状态
                            EMAILSENDTIME = m.EMAILTIME
                        });
                    }
                } 
                #endregion

                #region 发送短信
                string name = SystemCls.getCookieInfo().trueName;
                string time = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now.ToString());
                string title = m.EMAILTITLE;
                string userlist = m.EMAILRECUSERLIST + "," + m.EMAILCOPYUSERLIST + "," + m.EMAILSECRETUSERLIST;
                string[] crr = userlist.Split(',');
                string[] drr = crr.Where(s => !string.IsNullOrEmpty(s)).ToArray();//去掉空的用户id
                string userlist1 = "";//定义一个去掉空的uselist
                for (int i = 0; i < drr.Length; i++)
                {
                    if (i == drr.Length - 1)
                    {
                        userlist1 += drr[i];
                    }
                    else
                    {
                        userlist1 += drr[i] + ",";
                    }
                }
                string phone = "";//通过userlist获取手机号码
                string phone1 = "";//定义一个去掉空的号码
                if (string.IsNullOrEmpty(userlist1) == false)
                {
                    phone = BaseDT.T_SYSSEC_IPSUSER.getphone(new T_SYSSEC_IPSUSER_SW { USERID = userlist1 });
                    string[] jrr = phone.Split(',');
                    string[] brr = jrr.Where(s => !string.IsNullOrEmpty(s)).ToArray();//去掉空的手机号码
                    List<string> list = new List<string>();
                    foreach (string eachString in brr)//去掉重复的电话号码
                    {
                        if (!list.Contains(eachString))
                            list.Add(eachString);
                    }
                    brr = list.ToArray();
                    for (int i = 0; i < brr.Length; i++)
                    {
                        if (i == brr.Length - 1)
                        {
                            phone1 += brr[i];
                        }
                        else
                        {
                            phone1 += brr[i] + ",";
                        }
                    }
                }
                string content = "您好:" + name + "在" + time + "给您发一封主题「" + title + "」邮件,请登陆森林保护信息指挥系统查阅!";
                SmsHelp.SmsCom.SendMsg(content, phone1);
                return new Message(true, "发送成功!", m.returnUrl); 
                #endregion
            }
            if (m.opMethod == "Send")//发送
            {
                //没有附件
                if (string.IsNullOrEmpty(m.EMAILID) == true)
                {
                    //未保存为草稿，直接发送
                    BaseDT.E_SUBJECT.Add(m);//主题表ADD
                    //获取主题ID
                    string EMAILid = BaseDT.E_SUBJECT.getID(new E_SUBJECT_SW { EMAILSENDUSERID = m.EMAILSENDUSERID, EMAILTIME = m.EMAILTIME });
                    if (string.IsNullOrEmpty(m.EMAILRECUSERLIST) == false)
                    {
                        //接收信息表中录入数据 接收人
                        string[] arr = m.EMAILRECUSERLIST.Split(',');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            BaseDT.E_RECEIVE.Add(new E_RECEIVE_Model
                            {
                                BYEMAILID = EMAILid,
                                RECEIVETYPE = "0",   //接收人类别
                                RECEIVEUSERID = arr[i],//接收人序号
                                EMAILRECEIVESTATUS = "0",//接收状态
                                EMAILSENDTIME = m.EMAILTIME
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(m.EMAILCOPYUSERLIST) == false)
                    {
                        //接收信息表中录入数据 抄送人 
                        string[] arr = m.EMAILCOPYUSERLIST.Split(',');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            BaseDT.E_RECEIVE.Add(new E_RECEIVE_Model
                            {
                                BYEMAILID = EMAILid,
                                RECEIVETYPE = "1",   //接收人类别
                                RECEIVEUSERID = arr[i],//接收人序号
                                EMAILRECEIVESTATUS = "0",//接收状态
                                EMAILSENDTIME = m.EMAILTIME
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(m.EMAILSECRETUSERLIST) == false)
                    {
                        //接收信息表中录入数据 密送人 
                        string[] arr = m.EMAILSECRETUSERLIST.Split(',');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            BaseDT.E_RECEIVE.Add(new E_RECEIVE_Model
                            {
                                BYEMAILID = EMAILid,
                                RECEIVETYPE = "2",   //接收人类别
                                RECEIVEUSERID = arr[i],//接收人序号
                                EMAILRECEIVESTATUS = "0",//接收状态
                                EMAILSENDTIME = m.EMAILTIME
                            });
                        }
                    }
                    //发送短信
                    string name = SystemCls.getCookieInfo().trueName;
                    string time = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now.ToString());
                    string title = m.EMAILTITLE;
                    string userlist = m.EMAILRECUSERLIST + "," + m.EMAILCOPYUSERLIST + "," + m.EMAILSECRETUSERLIST;
                    string[] crr = userlist.Split(',');
                    string[] drr = crr.Where(s => !string.IsNullOrEmpty(s)).ToArray();//去掉空的用户id
                    string userlist1 = "";//定义一个去掉空的uselist
                    for (int i = 0; i < drr.Length; i++)
                    {
                        if (i == drr.Length - 1)
                        {
                            userlist1 += drr[i];
                        }
                        else
                        {
                            userlist1 += drr[i] + ",";
                        }
                    }
                    string phone = "";//通过userlist获取手机号码
                    string phone1 = "";//定义一个去掉空的号码
                    if (string.IsNullOrEmpty(userlist1) == false)
                    {
                        phone = BaseDT.T_SYSSEC_IPSUSER.getphone(new T_SYSSEC_IPSUSER_SW { USERID = userlist1 });
                        string[] jrr = phone.Split(',');
                        string[] brr = jrr.Where(s => !string.IsNullOrEmpty(s)).ToArray();//去掉空的手机号码
                        List<string> list = new List<string>();
                        foreach (string eachString in brr)//去掉重复的电话号码
                        {
                            if (!list.Contains(eachString))
                                list.Add(eachString);
                        }
                        brr = list.ToArray();
                        for (int i = 0; i < brr.Length; i++)
                        {
                            if (i == brr.Length - 1)
                            {
                                phone1 += brr[i];
                            }
                            else
                            {
                                phone1 += brr[i] + ",";
                            }
                        }
                    }
                    string content = "您好:" + name + "在" + time + "给您发一封主题「" + title + "」邮件,请登陆森林保护信息指挥系统查阅!";
                    SmsHelp.SmsCom.SendMsg(content, phone1);
                    return new Message(true, "发送成功", m.returnUrl);
                }
                //有附件的情况更新库
                if (string.IsNullOrEmpty(m.EMAILID) == false)
                {
                    BaseDT.E_SUBJECT.Mdy(m);
                    if (string.IsNullOrEmpty(m.EMAILRECUSERLIST) == false)
                    {
                        //接收信息表中录入数据 接收人
                        string[] arr = m.EMAILRECUSERLIST.Split(',');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            BaseDT.E_RECEIVE.Add(new E_RECEIVE_Model
                            {
                                BYEMAILID = m.EMAILID,
                                RECEIVETYPE = "0",   //接收人类别
                                RECEIVEUSERID = arr[i],//接收人序号
                                EMAILRECEIVESTATUS = "0",//接收状态
                                EMAILSENDTIME = m.EMAILTIME
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(m.EMAILCOPYUSERLIST) == false)
                    {
                        //接收信息表中录入数据 抄送人 
                        string[] arr = m.EMAILCOPYUSERLIST.Split(',');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            BaseDT.E_RECEIVE.Add(new E_RECEIVE_Model
                            {
                                BYEMAILID = m.EMAILID,
                                RECEIVETYPE = "1",   //接收人类别
                                RECEIVEUSERID = arr[i],//接收人序号
                                EMAILRECEIVESTATUS = "0",//接收状态
                                EMAILSENDTIME = m.EMAILTIME
                            });
                        }
                    }
                    if (string.IsNullOrEmpty(m.EMAILSECRETUSERLIST) == false)
                    {
                        //接收信息表中录入数据 密送人 
                        string[] arr = m.EMAILSECRETUSERLIST.Split(',');
                        for (int i = 0; i < arr.Length; i++)
                        {
                            BaseDT.E_RECEIVE.Add(new E_RECEIVE_Model
                            {
                                BYEMAILID = m.EMAILID,
                                RECEIVETYPE = "2",   //接收人类别
                                RECEIVEUSERID = arr[i],//接收人序号
                                EMAILRECEIVESTATUS = "0",//接收状态
                                EMAILSENDTIME = m.EMAILTIME
                            });
                        }
                        //发送短信
                        string name = SystemCls.getCookieInfo().trueName;
                        string time = PublicClassLibrary.ClsSwitch.SwitTM(DateTime.Now.ToString());
                        string title = m.EMAILTITLE;
                        string userlist = m.EMAILRECUSERLIST + "," + m.EMAILCOPYUSERLIST + "," + m.EMAILSECRETUSERLIST;
                        string[] crr = userlist.Split(',');
                        string[] drr = crr.Where(s => !string.IsNullOrEmpty(s)).ToArray();//去掉空的用户id
                        string userlist1 = "";//定义一个去掉空的uselist
                        for (int i = 0; i < drr.Length; i++)
                        {
                            if (i == drr.Length - 1)
                            {
                                userlist1 += drr[i];
                            }
                            else
                            {
                                userlist1 += drr[i] + ",";
                            }
                        }
                        string phone = "";//通过userlist获取手机号码
                        string phone1 = "";//定义一个去掉空的号码
                        if (string.IsNullOrEmpty(userlist1) == false)
                        {
                            phone = BaseDT.T_SYSSEC_IPSUSER.getphone(new T_SYSSEC_IPSUSER_SW { USERID = userlist1 });
                            string[] jrr = phone.Split(',');
                            string[] brr = jrr.Where(s => !string.IsNullOrEmpty(s)).ToArray();//去掉空的手机号码
                            List<string> list = new List<string>();
                            foreach (string eachString in brr)//去掉重复的电话号码
                            {
                                if (!list.Contains(eachString))
                                    list.Add(eachString);
                            }
                            brr = list.ToArray();
                            for (int i = 0; i < brr.Length; i++)
                            {
                                if (i == brr.Length - 1)
                                {
                                    phone1 += brr[i];
                                }
                                else
                                {
                                    phone1 += brr[i] + ",";
                                }
                            }
                        }
                        string content = "您好:" + name + "在" + time + "给您发一封主题「" + title + "」邮件,请登陆森林保护信息指挥系统查阅!";
                        SmsHelp.SmsCom.SendMsg(content, phone1);
                    }
                    return new Message(true, "发送成功!", m.returnUrl);
                }
            }
            if (m.opMethod == "Mdy") //草稿箱的时候修改
            {
                Message msg = BaseDT.E_SUBJECT.Mdy(m);
                return new Message(msg.Success, msg.Msg, m.returnUrl);
            }
            if (m.opMethod == "SendMdy") //涉及到多条记录的修改；已发送的列表
            {
                Message msg = BaseDT.E_SUBJECT.SendMdy(m);
                return new Message(msg.Success, msg.Msg, m.returnUrl);
            }
            if (m.opMethod == "Del")
            {
                Message msgUser = BaseDT.E_SUBJECT.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, m.returnUrl);
            }
            return new Message(false, "无效操作!", "");
        }
        #endregion

        #region 增加获取主键
        /// <summary>
        /// 增加获取主键
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string AddReturn(E_SUBJECT_Model m)
        {
            return BaseDT.E_SUBJECT.AddReturn(m);
        }
        #endregion

        #region 获取某一邮件信息
        /// <summary>
        /// 获取某一邮件信息
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static E_SUBJECT_Model getModel(E_SUBJECT_SW sw)
        {
            DataTable dt = BaseDT.E_SUBJECT.getDT(sw);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            E_SUBJECT_Model m = new E_SUBJECT_Model();
            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.EMAILID = dt.Rows[i]["EMAILID"].ToString();
                m.EMAILTITLE = dt.Rows[i]["EMAILTITLE"].ToString();
                m.EMAILSTATUS = dt.Rows[i]["EMAILSTATUS"].ToString();
                m.EMAILSENDUSERID = dt.Rows[i]["EMAILSENDUSERID"].ToString();
                //m.EMAILSENDUSERName = BaseDT.T_SYSSEC_USER.getName(dtUser, m.EMAILSENDUSERID);
                //m.EMAILSENDUSERName = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.EMAILSENDUSERID);
                m.EMAILSENDUSERName = T_SYSSEC_IPSUSERCls.getUserCombString(new T_SYSSEC_IPSUSER_SW { USERID = m.EMAILSENDUSERID, formatUserStr = "<font color=red>[userName]</font><[orgName]>", splitUserStr = "," });
                m.EMAILCONTENT = dt.Rows[i]["EMAILCONTENT"].ToString();
                m.EMAILTIME = ClsSwitch.SwitTM(dt.Rows[i]["EMAILTIME"].ToString());
                m.EMAILRECUSERLIST = dt.Rows[i]["EMAILRECUSERLIST"].ToString();//收件人
                //m.EMAILRECUSERNameLIST = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.EMAILRECUSERLIST);
                m.EMAILRECUSERNameLIST = T_SYSSEC_IPSUSERCls.getUserCombString(new T_SYSSEC_IPSUSER_SW { USERID = m.EMAILRECUSERLIST, formatUserStr = "<font color=red>[userName]</font><[orgName]>", splitUserStr = "," });
                if (string.IsNullOrEmpty(m.EMAILRECUSERLIST))
                    m.EMAILSECRETUSERLIST = "0";
                m.EMAILRECUSERLIST1 = T_SYSSEC_IPSUSERCls.getUserCombString(new T_SYSSEC_IPSUSER_SW { USERID = m.EMAILRECUSERLIST, formatUserStr = "[userName]", splitUserStr = "," });
                m.EMAILCOPYUSERLIST = dt.Rows[i]["EMAILCOPYUSERLIST"].ToString();//抄送人
                if (string.IsNullOrEmpty(m.EMAILCOPYUSERLIST))
                    m.EMAILCOPYUSERLIST = "0";
                m.EMAILCOPYUSERNameLIST = T_SYSSEC_IPSUSERCls.getUserCombString(new T_SYSSEC_IPSUSER_SW { USERID = m.EMAILCOPYUSERLIST, formatUserStr = "<font color=red>[userName]</font><[orgName]>", splitUserStr = "," });
                m.EMAILCOPYUSERLIST1 = T_SYSSEC_IPSUSERCls.getUserCombString(new T_SYSSEC_IPSUSER_SW { USERID = m.EMAILCOPYUSERLIST, formatUserStr = "[userName]", splitUserStr = "," });
                //m.EMAILCOPYUSERNameLIST = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.EMAILCOPYUSERLIST);
                m.EMAILSECRETUSERLIST = dt.Rows[i]["EMAILSECRETUSERLIST"].ToString();//密送人
                if (string.IsNullOrEmpty(m.EMAILSECRETUSERLIST))
                    m.EMAILSECRETUSERLIST = "0";
                m.EMAILSECRETUSERNameLIST = T_SYSSEC_IPSUSERCls.getUserCombString(new T_SYSSEC_IPSUSER_SW { USERID = m.EMAILSECRETUSERLIST, formatUserStr = "<font color=red>[userName]</font><[orgName]>", splitUserStr = "," });
                m.EMAILSECRETUSERLIST1 = T_SYSSEC_IPSUSERCls.getUserCombString(new T_SYSSEC_IPSUSER_SW { USERID = m.EMAILSECRETUSERLIST, formatUserStr = "[userName]", splitUserStr = "," });
                m.FileModel = E_FILECls.getListModel(new E_File_SW { BYEMAILID = m.EMAILID });
            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return m;
        }
        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<E_SUBJECT_Model> getListModel(E_SUBJECT_SW sw)
        {
            DataTable dt = BaseDT.E_SUBJECT.getDT(sw);//列表
            var result = new List<E_SUBJECT_Model>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                E_SUBJECT_Model m = new E_SUBJECT_Model();
                m.EMAILID = dt.Rows[i]["EMAILID"].ToString();
                m.EMAILTITLE = dt.Rows[i]["EMAILTITLE"].ToString();
                m.EMAILSTATUS = dt.Rows[i]["EMAILSTATUS"].ToString();
                m.EMAILSENDUSERID = dt.Rows[i]["EMAILSENDUSERID"].ToString();
                m.EMAILCONTENT = dt.Rows[i]["EMAILCONTENT"].ToString();
                m.EMAILRECUSERLIST = dt.Rows[i]["EMAILRECUSERLIST"].ToString();
                m.EMAILCOPYUSERLIST = dt.Rows[i]["EMAILCOPYUSERLIST"].ToString();
                m.EMAILSECRETUSERLIST = dt.Rows[i]["EMAILSECRETUSERLIST"].ToString();
                m.EMAILTIME = ClsSwitch.SwitTM(dt.Rows[i]["EMAILTIME"].ToString());
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            return result;
        }
        #endregion

        #region 获取列表分页
        /// <summary>
        /// 获取列表分页
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public static IEnumerable<E_SUBJECT_Model> getListModelPager(E_SUBJECT_SW sw, out int total)
        {
            var result = new List<E_SUBJECT_Model>();
            DataTable dt = BaseDT.E_SUBJECT.getDT(sw, out total);
            DataTable dtUser = BaseDT.T_SYSSEC_USER.getDT(new T_SYSSEC_IPSUSER_SW { });
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                E_SUBJECT_Model m = new E_SUBJECT_Model();
                m.EMAILID = dt.Rows[i]["EMAILID"].ToString();
                m.EMAILTITLE = dt.Rows[i]["EMAILTITLE"].ToString();
                m.EMAILSTATUS = dt.Rows[i]["EMAILSTATUS"].ToString();
                m.EMAILSENDUSERID = dt.Rows[i]["EMAILSENDUSERID"].ToString();
                //m.EMAILSENDUSERName = BaseDT.T_SYSSEC_USER.getName(dtUser, m.EMAILSENDUSERID);
                m.EMAILSENDUSERName = T_SYSSEC_IPSUSERCls.getUserCombString(new T_SYSSEC_IPSUSER_SW { USERID = m.EMAILSENDUSERID, formatUserStr = "<font color=red>[userName]</font><[orgName]>", splitUserStr = "," });
                m.EMAILCONTENT = dt.Rows[i]["EMAILCONTENT"].ToString();
                m.EMAILTIME = ClsSwitch.SwitTM(dt.Rows[i]["EMAILTIME"].ToString());
                m.EMAILRECUSERLIST = dt.Rows[i]["EMAILRECUSERLIST"].ToString();//发送人
                //m.EMAILRECUSERNameLIST = T_SYSSEC_IPSUSERCls.getUserCombString(new T_SYSSEC_IPSUSER_SW { USERID = m.EMAILRECUSERLIST, formatUserStr = "[userName]<[orgName]>", splitUserStr = "," });// BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.EMAILRECUSERLIST);
                m.EMAILRECUSERNameLIST = T_SYSSEC_IPSUSERCls.getUserCombString(new T_SYSSEC_IPSUSER_SW { USERID = m.EMAILRECUSERLIST, formatUserStr = "<font color=red>[userName]</font><[orgName]>", splitUserStr = "," });
                m.EMAILCOPYUSERLIST = dt.Rows[i]["EMAILCOPYUSERLIST"].ToString();//抄送人
                m.EMAILCOPYUSERNameLIST = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.EMAILCOPYUSERLIST);
                m.EMAILSECRETUSERLIST = dt.Rows[i]["EMAILSECRETUSERLIST"].ToString();//密送人
                m.EMAILSECRETUSERNameLIST = BaseDT.T_SYSSEC_USER.getNameByUserList(dtUser, m.EMAILSECRETUSERLIST);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtUser.Clear();
            dtUser.Dispose();
            return result;
        }
        #endregion

        #region 通过id获取匹配
        /// <summary>
        /// 通过id获取匹配
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static string getOptionByEMAILID(E_SUBJECT_SW sw)
        {
            StringBuilder sb = new StringBuilder();
            E_SUBJECT_Model m = getModel(sw);
            string userList = "";
            if (string.IsNullOrEmpty(sw.getReceiveType) == true || sw.getReceiveType == "0")
                userList = m.EMAILRECUSERLIST;
            if (sw.getReceiveType == "1")
                userList = m.EMAILCOPYUSERLIST;
            if (sw.getReceiveType == "2")
                userList = m.EMAILSECRETUSERLIST;
            if (string.IsNullOrEmpty(userList) == false)
            {
                userList = "," + userList + ",";//,1,2,3,11,
                IEnumerable<T_SYSSEC_IPSUSER_Pager_Model> UM = T_SYSSEC_IPSUSERCls.getListModel(new T_SYSSEC_IPSUSER_SW { });
                foreach (var v in UM)
                {
                    if (userList.Contains("," + v.USERID + ","))
                        sb.AppendFormat("<option value=\"{0}\" selected='selected'>{1}</option>", v.USERID, v.USERNAME + "<" + v.ORGNAME + ">");
                    else
                        sb.AppendFormat("<option value=\"{0}\">{1}</option>", v.USERID, v.USERNAME + "<" + v.ORGNAME + ">");
                }
            }
            return sb.ToString();
        }
        #endregion
    }
}
