using DataBaseClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ManagerSystemModel.SDEModel;

namespace ManagerSystemClassLibrary
{
    /// <summary>
    /// 数据中心_车辆
    /// </summary>
    public class DC_CARCls
    {
        #region 增、删、改
        /// <summary>
        /// 增、删、改
        /// </summary>
        /// <param name="m">参见模型</param>
        /// <returns>参见模型</returns>
        public static Message Manager(DC_CAR_Model m)
        {
            if (m.opMethod == "Add")
            {
                //SystemCls.LogSave("3", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_CAR.Add(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            if (m.opMethod == "Mdy")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_CAR.Mdy(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "MdyJWD")
            {
                //SystemCls.LogSave("4", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_CAR.MdyJWD(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);

            }
            if (m.opMethod == "Del")
            {
                //SystemCls.LogSave("5", "通知公告:" + m.INFOTITLE, ClsStr.getModelContent(m));
                Message msgUser = BaseDT.DC_CAR.Del(m);
                return new Message(msgUser.Success, msgUser.Msg, msgUser.Url);
            }
            return new Message(false, "无效操作", "");


        }

        #endregion

        #region 获取单条
        /// <summary>
        /// 根据查询条件获取某一条用户信息记录，用于修改、删除、用户登录验证
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <returns>参见模型</returns>
        public static DC_CAR_Model getModel(DC_CAR_SW sw)
        {
            var result = new List<DC_CAR_Model>();

            DataTable dt = BaseDT.DC_CAR.getDT(sw);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt33 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "33" });//数据中心车辆类型
            DC_CAR_Model m = new DC_CAR_Model();

            if (dt.Rows.Count > 0)
            {
                int i = 0;
                m.DC_CAR_ID = dt.Rows[i]["DC_CAR_ID"].ToString();
                m.CARTYPE = dt.Rows[i]["CARTYPE"].ToString();
                m.CARTYPEName = BaseDT.T_SYS_DICT.getName(dt33, m.CARTYPE);
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                //m.BUYYEAR = dt.Rows[i]["BUYYEAR"].ToString();
                m.BUYPRICE = dt.Rows[i]["BUYPRICE"].ToString();
                m.PLATENUM = dt.Rows[i]["PLATENUM"].ToString();
                m.DRIVER = dt.Rows[i]["DRIVER"].ToString();
                m.CONTACTS = dt.Rows[i]["CONTACTS"].ToString();
                m.GPSEQUIP = dt.Rows[i]["GPSEQUIP"].ToString();
                m.GPSTELL = dt.Rows[i]["GPSTELL"].ToString();
                m.STOREADDR = dt.Rows[i]["STOREADDR"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.BUYYEAR = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUYYEAR"].ToString());
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt33.Clear();
            dt33.Dispose();
            return m;
        }

        #endregion

        #region 获取列表
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sw"></param>
        /// <returns></returns>
        public static IEnumerable<DC_CAR_Model> getModelList(DC_CAR_SW sw)
        {
            var result = new List<DC_CAR_Model>();
            DataTable dt = BaseDT.DC_CAR.getDT(sw);//列表
            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt33 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "33" });//数据中心车辆类型
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_CAR_Model m = new DC_CAR_Model();
                m.DC_CAR_ID = dt.Rows[i]["DC_CAR_ID"].ToString();
                m.CARTYPE = dt.Rows[i]["CARTYPE"].ToString();
                m.CARTYPEName = BaseDT.T_SYS_DICT.getName(dt33, m.CARTYPE);
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                //m.BUYYEAR =dt.Rows[i]["BUYYEAR"].ToString();
                m.BUYPRICE = dt.Rows[i]["BUYPRICE"].ToString();
                m.PLATENUM = dt.Rows[i]["PLATENUM"].ToString();
                m.DRIVER = dt.Rows[i]["DRIVER"].ToString();
                m.CONTACTS = dt.Rows[i]["CONTACTS"].ToString();
                m.GPSEQUIP = dt.Rows[i]["GPSEQUIP"].ToString();
                m.GPSTELL = dt.Rows[i]["GPSTELL"].ToString();
                m.STOREADDR = dt.Rows[i]["STOREADDR"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.BUYYEAR = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUYYEAR"].ToString());
                m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt33.Clear();
            dt33.Dispose();
            return result;
        }

        #endregion

        #region 获取车辆JsonString
        /// <summary>
        ///异步获取json字符串（车辆）
        /// </summary>
        /// <param name="orgno">当前用户机构码</param>
        /// <param name="idorgno">选择机构码</param>
        /// <returns></returns>
        public static string GetJsonStrCar(string orgno, string idorgno)
        {
            DataTable dtCar = BaseDT.DC_CAR.getDT(new DC_CAR_SW());
            JArray jObjects = new JArray();
            if (string.IsNullOrEmpty(idorgno))
            {
                List<T_SYS_ORGModel> orgList = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { ORGNO = orgno }).ToList();//获取机构
                if (orgList.Any())
                {
                    var bo = PublicCls.OrgIsShi(orgno);
                    if (bo)
                    {
                        List<T_SYS_ORGModel> citylist = orgList.Where(p => p.ORGNO.EndsWith("00000")).ToList();//市级别
                        foreach (var city in citylist)
                        {
                            var root = getJobejctCar(dtCar,city);
                            jObjects.Add(root);
                        }
                    }

                    var bb = PublicCls.OrgIsXian(orgno);
                    if (bb)
                    {
                        List<T_SYS_ORGModel> countylist = orgList.Where(p => p.ORGNO.EndsWith("000")).ToList();//县级别
                        foreach (var county in countylist)
                        {
                            var root = getJobejctCar(dtCar,county);
                            jObjects.Add(root);
                        }
                    }
                    var bx = PublicCls.OrgIsZhen(orgno);
                    if (bx)
                    {
                        List<T_SYS_ORGModel> towerlist = orgList.Where(p => !p.ORGNO.EndsWith("000")).ToList();//乡镇级别
                        foreach (var tower in towerlist)
                        {
                            var root = getJobejctCar(dtCar,tower);
                            jObjects.Add(root);
                        }
                    }
                }
            }
            else
            {
                List<T_SYS_ORGModel> orgList = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopORGNO = idorgno }).ToList();//获取机构
                var bo = PublicCls.OrgIsShi(idorgno);//如果选择市，则列出所有县
                if (bo)
                {
                    List<T_SYS_ORGModel> countylist = orgList.Where(p => p.ORGNO.EndsWith("000") && p.ORGNO != idorgno).ToList();
                    foreach (var county in countylist)
                    {
                        var root = getJobejctCar(dtCar,county);
                        jObjects.Add(root);
                    }
                    List<DC_CAR_Model> citycarlist = DC_CARCls.getModelList(new DC_CAR_SW { BYORGNO = idorgno }).Where(p => p.BYORGNO == idorgno).ToList();//市（车辆）
                    if (citycarlist.Any())
                    {
                        foreach (var car in citycarlist)
                        {
                            var obj = getLastJobjectCar(car);
                            jObjects.Add(obj);
                        }
                    }
                }
                var bb = PublicCls.OrgIsXian(idorgno);//如果选择县，则列出所有乡镇
                if (bb)
                {
                    List<T_SYS_ORGModel> towerlist = orgList.Where(p => !p.ORGNO.EndsWith("000") && p.ORGNO != idorgno).ToList();
                    foreach (var tower in towerlist)
                    {
                        var root = getJobejctCar(dtCar,tower);
                        jObjects.Add(root);
                    }
                    List<DC_CAR_Model> countycarlist = DC_CARCls.getModelList(new DC_CAR_SW { BYORGNO = idorgno }).Where(p => p.BYORGNO == idorgno).ToList();//县（车辆）
                    if (countycarlist.Any())
                    {
                        foreach (var car in countycarlist)
                        {
                            var obj = getLastJobjectCar(car);
                            jObjects.Add(obj);
                        }
                    }
                }
                var bx = PublicCls.OrgIsZhen(idorgno);//如果选择乡镇，则列出所有乡镇的车辆
                if (bx)
                {
                    List<DC_CAR_Model> towercarlist = DC_CARCls.getModelList(new DC_CAR_SW { BYORGNO = idorgno }).ToList();//乡镇（车辆）
                    if (towercarlist.Any())
                    {
                        foreach (var car in towercarlist)
                        {
                            var obj = getLastJobjectCar(car);
                            jObjects.Add(obj);
                        }
                    }
                }
            }
            return JsonConvert.SerializeObject(jObjects);
        }

        /// <summary>
        /// 根据机构编码获取该机构下车辆数
        /// </summary>
        /// <param name="dtCar"></param>
        /// <param name="orgNo"></param>
        /// <returns></returns>
        public static string GetCarCount(DataTable dtCar,string orgNo)
        {            
            string count = "0";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("(");
            if (PublicCls.OrgIsShi(orgNo))
            {
                count = dtCar.Compute("count(BYORGNO)", "substring(BYORGNO,1,4)='" + orgNo.Substring(0, 4) + "'").ToString();
            }
            else if (PublicCls.OrgIsXian(orgNo))
            {
                count = dtCar.Compute("count(BYORGNO)", "substring(BYORGNO,1,6)='" + orgNo.Substring(0, 6) + "'").ToString();
            }
            else if (PublicCls.OrgIsZhen(orgNo))
            {
                count = dtCar.Compute("count(BYORGNO)", "BYORGNO='" + orgNo + "'").ToString();
            }
            sb.AppendFormat("{0}", count);
            sb.AppendFormat(")");
            return sb.ToString();
        }
        #endregion

        #region 获取分页
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="sw">参见模型</param>
        /// <param name="total">记录总数</param>
        /// <returns>参见模型</returns>
        public static IEnumerable<DC_CAR_Model> getModelList(DC_CAR_SW sw, out int total)
        {
            var result = new List<DC_CAR_Model>();

            DataTable dt = BaseDT.DC_CAR.getDT(sw, out total);//列表

            DataTable dtORG = BaseDT.T_SYS_ORG.getDT(new T_SYS_ORGSW { SYSFLAG = ConfigCls.getSystemFlag() });//获取单位
            DataTable dt33 = BaseDT.T_SYS_DICT.getDT(new T_SYS_DICTSW { DICTTYPEID = "33" });//数据中心车辆类型

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DC_CAR_Model m = new DC_CAR_Model();

                m.DC_CAR_ID = dt.Rows[i]["DC_CAR_ID"].ToString();
                m.CARTYPE = dt.Rows[i]["CARTYPE"].ToString();
                m.CARTYPEName = BaseDT.T_SYS_DICT.getName(dt33, m.CARTYPE);
                m.NUMBER = dt.Rows[i]["NUMBER"].ToString();
                m.NAME = dt.Rows[i]["NAME"].ToString();
                m.BYORGNO = dt.Rows[i]["BYORGNO"].ToString();
                //m.BUYYEAR =dt.Rows[i]["BUYYEAR"].ToString();
                m.BUYPRICE = dt.Rows[i]["BUYPRICE"].ToString();
                m.PLATENUM = dt.Rows[i]["PLATENUM"].ToString();
                m.DRIVER = dt.Rows[i]["DRIVER"].ToString();
                m.CONTACTS = dt.Rows[i]["CONTACTS"].ToString();
                m.GPSEQUIP = dt.Rows[i]["GPSEQUIP"].ToString();
                m.GPSTELL = dt.Rows[i]["GPSTELL"].ToString();
                m.STOREADDR = dt.Rows[i]["STOREADDR"].ToString();
                m.MARK = dt.Rows[i]["MARK"].ToString();
                m.JD = dt.Rows[i]["JD"].ToString();
                m.WD = dt.Rows[i]["WD"].ToString();
                m.BUYYEAR = PublicClassLibrary.ClsSwitch.SwitDate(dt.Rows[i]["BUYYEAR"].ToString());
                if (m.BYORGNO.Substring(6, 3) != "000")
                {
                    m.ORGName = BaseDT.T_SYS_ORG.getName(dtORG, m.BYORGNO);

                }
                m.ORGXSName = BaseDT.T_SYS_ORG.getSXName(dtORG, m.BYORGNO);
                result.Add(m);
            }
            dt.Clear();
            dt.Dispose();
            dtORG.Clear();
            dtORG.Dispose();
            dt33.Clear();
            dt33.Dispose();
            return result;
        }
        #endregion

        #region 车辆上传
        /// <summary>
        /// 护林员上传
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public static void CarUpload(string filePath)
        {
            HSSFWorkbook hssfworkbook;
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            int rowCount = sheet.LastRowNum;
            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {

                IRow row = sheet.GetRow(i);
                string[] arr = new string[12];
                for (int k = 0; k < arr.Length; k++)
                {
                    if (k != 6)
                        arr[k] = row.GetCell(k) == null ? "" : row.GetCell(k).ToString();//循环获取每一单元格内容
                    else
                        arr[k] = row.GetCell(k).DateCellValue.ToString("yyyy-MM-dd");
                }
                DC_CAR_Model m = new DC_CAR_Model();
                //单位	车辆类型	名称	编号	号牌	存储地点	购买年份 购买价格 驾驶员 联系方式 经度 纬度
                if (string.IsNullOrEmpty(arr[0]) || string.IsNullOrEmpty(arr[2]))
                {
                    continue;
                }
                m.BYORGNO = BaseDT.T_SYS_ORG.getCodeByName(arr[0]);
                m.NAME = arr[2];
                m.NUMBER = arr[3];
                m.PLATENUM = arr[4];
                m.STOREADDR = arr[5];
                m.BUYYEAR = arr[6];
                if (m.BUYYEAR == "9999-12-31")
                    m.BUYYEAR = "1900-01-01";
                m.BUYPRICE = arr[7];
                m.DRIVER = arr[8];
                m.CONTACTS = arr[9];
                string jd = arr[10];
                string wd = arr[11];
                if (string.IsNullOrEmpty(jd) == false && string.IsNullOrEmpty(wd) == false)
                {
                    double[] brr = ClsPositionTrans.GpsTransform(double.Parse(wd), double.Parse(jd), "1");
                    m.JD = brr[1].ToString();
                    m.WD = brr[0].ToString();
                }
                if (arr[1].Trim() == "指挥车")//装备类型
                {
                    m.CARTYPE = "1";
                }
                else if (arr[1].Trim() == "运兵车")
                {
                    m.CARTYPE = "2";
                }
                else if (arr[1].Trim() == "供水车")
                {
                    m.CARTYPE = "3";
                }
                else if (arr[1].Trim() == "通讯车")
                {
                    m.CARTYPE = "4";
                }
                else if (arr[1].Trim() == "宣传车")
                {
                    m.CARTYPE = "5";
                }
                else
                {
                    m.CARTYPE = "1";
                }
                BaseDT.DC_CAR.Add(m);

                string a = row.GetCell(0).ToString();
                string a1 = row.GetCell(1).ToString();
                string a2 = row.GetCell(2).ToString();

            }

        }
        #endregion

        #region Private
        /// <summary>
        /// 获取上级的tree
        /// </summary>
        /// <param name="dtCar"></param>
        /// <param name="Org"></param>
        /// <returns></returns>
        private static JObject getJobejctCar(DataTable dtCar,T_SYS_ORGModel Org)
        {
            JObject root = new JObject { { "id", Org.ORGNO }, { "text", Org.ORGNAME + GetCarCount(dtCar, Org.ORGNO) }, { "state", "closed" } };
            return root;
        }

        /// <summary>
        /// 获取最低级的tree
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public static JObject getLastJobjectCar(DC_CAR_Model car)
        {
            var model = T_SYS_DICTCls.getListModel(new T_SYS_DICTSW { DICTTYPEID = "33", DICTVALUE = car.CARTYPE }).FirstOrDefault();
            var name = car.NAME + "【" + model.DICTNAME + "】";
            //getPosition
            StringBuilder sb = new StringBuilder();
            string carimage = "car_" + car.CARTYPE;
            //偏移量计算
            //  double[] arr = ClsPositionUtil.gcj_To_Gps84(double.Parse(car.WD), double.Parse(car.JD));
            if (!string.IsNullOrEmpty(car.JD) || !string.IsNullOrEmpty(car.WD))
            {

                double[] arr = ClsPositionTrans.GpsTransform(double.Parse(car.WD), double.Parse(car.JD), ConfigCls.getSDELonLatTransform());
                if (arr.Length > 0)
                {
                    car.JD = arr[1].ToString();
                    car.WD = arr[0].ToString();
                }
                //string sb = "<a onClick="">" + name + "</a>";
                sb.AppendFormat("<font onClick='movetocar(\"{0}\",\"{1}\",\"{2}\")'>{3}</font>", car.JD, car.WD, carimage, name);
            }
            else
            {
                sb.AppendFormat("<font onClick='alert(\"缺少经纬度无法定位。\")'>{0}</font>", name);
            }
            JObject root = new JObject
                        {
                           {"id",car.DC_CAR_ID}//ORGNO
                          ,{"text",sb.ToString()} 
                         };
            return root;
        }
        #endregion

        #region 统计当前用户下的车辆数量
        /// <summary>
        ///统计当前用户下的车辆数量
        /// </summary>
        /// <returns></returns>
        public static string getCount()
        {
            var Count = "";
            Count = BaseDT.DC_CAR.getNum(new DC_CAR_SW{BYORGNO=SystemCls.getCurUserOrgNo()} );
            return Count;
        }
        #endregion
    }
}
