using ManagerSystem.MVC.HelpCom;
using ManagerSystem.MVC.Models;
using ManagerSystemClassLibrary;
using ManagerSystemModel;
using ManagerSystemSearchWhereModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagerSystem.MVC.Controllers
{
    public class DataCenterDataShowController : Controller
    {
        //
        // GET: /DataCenterDataShow/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取组织机构信息jason
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEchartOrgnoJson()
        {
            MessageListObject ms = new MessageListObject(false, null);
            string TopEchartORGNO = Request.Params["TopEchartORGNO"];
            var list = T_SYS_ORGCls.getListModel(new T_SYS_ORGSW { TopEchartORGNO = TopEchartORGNO });
            ms = new MessageListObject(true, list);
            return Json(ms);
        }

        #region 队伍的图表展示
        /// <summary>
        /// 队伍统计的数据图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetArmySourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string DictValue = Request.Params["DictValue"];
            var result = new List<ArmyCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_ARMY_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<DC_ARMY_TypeCountModel> typeModel;
            var arr = DictValue.Split(',');
            var dataList = DataCenterCountCls.getModelCount(sw, out typeModel);
            if (dataList.Any())
            {
              
                    var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);

                    if (list.Any())
                    {
                        foreach (var item in list)
                        {
                            ArmyCount_Model m = new ArmyCount_Model();
                            //var arr = DictValue.Split(',');
                            if (arr.Contains("1"))//专业队伍人数
                                m.ArmyMem1Count = item.TypeCountModel.Where(p => p.DICTVALUE == "1").FirstOrDefault().MEMBERTYPECount;
                            m.ArmyType1Count = item.TypeCountModel.Where(p => p.DICTVALUE == "1").FirstOrDefault().ARMYTYPECount;
                            if (arr.Contains("2"))//半专业队伍人数     
                                m.ArmyMem2Count = item.TypeCountModel.Where(p => p.DICTVALUE == "2").FirstOrDefault().MEMBERTYPECount;
                            m.ArmyType2Count = item.TypeCountModel.Where(p => p.DICTVALUE == "2").FirstOrDefault().ARMYTYPECount;
                            if (arr.Contains("3"))//应急队伍人数     
                                m.ArmyMem3Count = item.TypeCountModel.Where(p => p.DICTVALUE == "3").FirstOrDefault().MEMBERTYPECount;
                            m.ArmyType3Count = item.TypeCountModel.Where(p => p.DICTVALUE == "3").FirstOrDefault().ARMYTYPECount;
                            if (arr.Contains("4"))//群众队伍人数     
                                m.ArmyMem4Count = item.TypeCountModel.Where(p => p.DICTVALUE == "4").FirstOrDefault().MEMBERTYPECount;
                            m.ArmyType4Count = item.TypeCountModel.Where(p => p.DICTVALUE == "4").FirstOrDefault().ARMYTYPECount;
                            m.ORGNo = item.ORGNo;
                            m.ORGName = item.ORGName;
                            m.ArmyMemCount = item.MEMBERCount;
                            m.ArmyTypeCount = item.ARMYCount;
                            result.Add(m);
                  
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 装备的图表展示
        /// <summary>
        /// 装备的图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEquipSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string Equiptype = Request.Params["Equiptype"];
            string Usestate = Request.Params["Usestate"];
            var result = new List<EquipnewCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_EQUIP_NEW_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<EQUIPTYPECountModel> EQUIPTYPECountModel;
            IEnumerable<USESTATECountModel> USESTATECountModel;
            var dataList = DataCenterCountCls.getEQUIPModelCount(sw, out EQUIPTYPECountModel, out USESTATECountModel);
            if (dataList.Any())
            {
                    var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                    if (list.Any())
                    {
                        foreach (var item in list)
                        {
                            EquipnewCount_Model m = new EquipnewCount_Model();
                            if (Equiptype.Contains("1") || string.IsNullOrEmpty(Equiptype))
                                m.Equiptyp1Count = item.EQUIPTYPECountModel.Where(p => p.EQUIPTYPEVALUE == "1").FirstOrDefault().DICTEQUIPTYPECount;
                            if (Equiptype.Contains("2") || string.IsNullOrEmpty(Equiptype))
                                m.Equiptyp2Count = item.EQUIPTYPECountModel.Where(p => p.EQUIPTYPEVALUE == "2").FirstOrDefault().DICTEQUIPTYPECount;
                            if (Equiptype.Contains("3") || string.IsNullOrEmpty(Equiptype))
                                m.Equiptyp3Count = item.EQUIPTYPECountModel.Where(p => p.EQUIPTYPEVALUE == "3").FirstOrDefault().DICTEQUIPTYPECount;
                            if (Equiptype.Contains("4") || string.IsNullOrEmpty(Equiptype))
                                m.Equiptyp4Count = item.EQUIPTYPECountModel.Where(p => p.EQUIPTYPEVALUE == "4").FirstOrDefault().DICTEQUIPTYPECount;
                            if (Equiptype.Contains("5") || string.IsNullOrEmpty(Equiptype))
                                m.Equiptyp5Count = item.EQUIPTYPECountModel.Where(p => p.EQUIPTYPEVALUE == "5").FirstOrDefault().DICTEQUIPTYPECount;
                            if (Equiptype.Contains("6") || string.IsNullOrEmpty(Equiptype))
                                m.Equiptyp6Count = item.EQUIPTYPECountModel.Where(p => p.EQUIPTYPEVALUE == "6").FirstOrDefault().DICTEQUIPTYPECount;
                            if (Usestate.Contains("1") || string.IsNullOrEmpty(Usestate))
                                m.Usestate1Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "1").FirstOrDefault().USESTATECount;
                            if (Usestate.Contains("2") || string.IsNullOrEmpty(Usestate))
                                m.Usestate2Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "2").FirstOrDefault().USESTATECount;
                            if (Usestate.Contains("3") || string.IsNullOrEmpty(Usestate))
                                m.Usestate3Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "3").FirstOrDefault().USESTATECount;
                            m.ORGNo = item.BYORGNO;
                            m.ORGName = item.ORGName;
                            result.Add(m);
                        }
                    }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 车辆的图表展示
        /// <summary>
        /// 车辆统计的数据图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCarChartSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string DictValue = Request.Params["DictValue"];
            var result = new List<CarCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_CAR_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<CARTYPECountModel> typeModel;
            var arr = DictValue.Split(',');
            var dataList = DataCenterCountCls.getCARModelCount(sw, out typeModel);
            if (dataList.Any())
            {
                
                    var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                    if (list.Any())
                    {
                        foreach (var item in list)
                        {
                            CarCount_Model m = new CarCount_Model();
                            if (arr.Contains("1"))//指挥车车辆数
                                m.CarType1Count = item.CARTYPECountModel.Where(p => p.CARTYPEVALUE == "1").FirstOrDefault().CARTYPECount;
                            if (arr.Contains("2"))//运兵车车辆数 
                                m.CarType2Count = item.CARTYPECountModel.Where(p => p.CARTYPEVALUE == "2").FirstOrDefault().CARTYPECount;
                            if (arr.Contains("3"))//供水车辆数  
                                m.CarType3Count = item.CARTYPECountModel.Where(p => p.CARTYPEVALUE == "3").FirstOrDefault().CARTYPECount;
                            if (arr.Contains("4"))//通讯车车辆数
                                m.CarType4Count = item.CARTYPECountModel.Where(p => p.CARTYPEVALUE == "4").FirstOrDefault().CARTYPECount;
                            if (arr.Contains("5"))//宣传车辆数   
                                m.CarType5Count = item.CARTYPECountModel.Where(p => p.CARTYPEVALUE == "5").FirstOrDefault().CARTYPECount;
                            m.ORGNo = item.BYORGNO;
                            m.ORGName = item.ORGName;
                            result.Add(m);
                        }
                    }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 营房的图表展示
        /// <summary>
        /// 营房统计的数据图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCampChartSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string DictValue = Request.Params["DictValue"];
            var result = new List<CampCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_UTILITY_CAMP_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<STRUCTURETYPECountModel> typeModel;
            var arr = DictValue.Split(',');
            var dataList = DataCenterCountCls.getCAMPModelCount(sw, out typeModel);
            if (dataList.Any())
            {
                var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        CampCount_Model m = new CampCount_Model();
                        if (arr.Contains("1"))//钢构数量
                            m.CampType1Count = item.STRUCTURETYPECountModel.Where(p => p.STRUCTURETYPEVALUE == "1").FirstOrDefault().STRUCTURETYPECount;
                        if (arr.Contains("2"))//砖混
                            m.CampType2Count = item.STRUCTURETYPECountModel.Where(p => p.STRUCTURETYPEVALUE == "2").FirstOrDefault().STRUCTURETYPECount;
                        if (arr.Contains("3"))//钢混
                            m.CampType3Count = item.STRUCTURETYPECountModel.Where(p => p.STRUCTURETYPEVALUE == "3").FirstOrDefault().STRUCTURETYPECount;
                        m.ORGNo = item.BYORGNO;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 瞭望台的图表展示
        /// <summary>
        /// 瞭望台的图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetOVERWATCHChartSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string DictValue = Request.Params["DictValue"];
            var result = new List<OVERWATCHCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_UTILITY_OVERWATCH_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<TYPECountModel> typeModel;
            var arr = DictValue.Split(',');
            var dataList = DataCenterCountCls.getOVERWATCHModelCount(sw, out typeModel);
            if (dataList.Any())
            {

                var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        OVERWATCHCount_Model m = new OVERWATCHCount_Model();
                        if (arr.Contains("1"))//钢构数量
                            m.OVERWATCHType1Count = item.TYPECountModel.Where(p => p.STRUCTURETYPEVALUE == "1").FirstOrDefault().STRUCTURETYPECount;
                        if (arr.Contains("2"))//砖混
                            m.OVERWATCHType2Count = item.TYPECountModel.Where(p => p.STRUCTURETYPEVALUE == "2").FirstOrDefault().STRUCTURETYPECount;
                        if (arr.Contains("3"))//钢混
                            m.OVERWATCHType3Count = item.TYPECountModel.Where(p => p.STRUCTURETYPEVALUE == "3").FirstOrDefault().STRUCTURETYPECount;
                        m.ORGNo = item.BYORGNO;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 中继站的图表展示
        /// <summary>
        /// 中继站的图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRelayChartSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string Communicationway = Request.Params["Communicationway"];
            string Usestate = Request.Params["Usestate"];
            string Managerstate = Request.Params["Managerstate"];
            var result = new List<RelayCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_UTILITY_RELAY_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<COMMUNICATIONWAYCountModel> COMMUNICATIONWAYCountModel;
            var dataList = DataCenterCountCls.getRELAYModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out COMMUNICATIONWAYCountModel);
            if (dataList.Any())
            {

                var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        RelayCount_Model m = new RelayCount_Model();
                        if (Communicationway.Contains("1") || string.IsNullOrEmpty(Communicationway))//短波数量
                            m.Communicationway1Count = item.COMMUNICATIONWAYCountModel.Where(p => p.COMMUNICATIONWAYVALUE == "1").FirstOrDefault().DICTCOMMUNICATIONWAYCount;
                        if (Communicationway.Contains("2") || string.IsNullOrEmpty(Communicationway))//超短波数量
                            m.Communicationway2Count = item.COMMUNICATIONWAYCountModel.Where(p => p.COMMUNICATIONWAYVALUE == "2").FirstOrDefault().DICTCOMMUNICATIONWAYCount;
                        if (Communicationway.Contains("3") || string.IsNullOrEmpty(Communicationway))//微波数量
                            m.Communicationway3Count = item.COMMUNICATIONWAYCountModel.Where(p => p.COMMUNICATIONWAYVALUE == "3").FirstOrDefault().DICTCOMMUNICATIONWAYCount;
                        if (Usestate.Contains("1") || string.IsNullOrEmpty(Usestate))//在用数量
                            m.Usestate1Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "1").FirstOrDefault().USESTATECount;
                        if (Usestate.Contains("2") || string.IsNullOrEmpty(Usestate))//规划数量
                            m.Usestate2Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "2").FirstOrDefault().USESTATECount;
                        if (Usestate.Contains("3") || string.IsNullOrEmpty(Usestate))//报废数量
                            m.Usestate3Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "3").FirstOrDefault().USESTATECount;
                        if (Managerstate.Contains("1") || string.IsNullOrEmpty(Managerstate))//未维护数量
                            m.Managerstate1Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "1").FirstOrDefault().MANAGERSTATCount;
                        if (Managerstate.Contains("2") || string.IsNullOrEmpty(Managerstate))//维护数量
                            m.Managerstate2Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "2").FirstOrDefault().MANAGERSTATCount;
                        if (Managerstate.Contains("3") || string.IsNullOrEmpty(Managerstate))//新建数量
                            m.Managerstate3Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "3").FirstOrDefault().MANAGERSTATCount;
                        m.ORGNo = item.BYORGNO;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 防火通道的图表展示
        /// <summary>
        /// 防火通道的图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFirechannelChartSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string Fireleveltype = Request.Params["Fireleveltype"];
            string Fireusetype = Request.Params["Fireusetype"];
            string Usestate = Request.Params["Usestate"];
            string Managerstate = Request.Params["Managerstate"];
            var result = new List<FirechannelCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_UTILITY_FIRECHANNEL_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<FIRECHANNELLEVELTYPECountModel> FIRECHANNELLEVELTYPECountModel;
            IEnumerable<FIRECHANNELUSERTYPECountModel> FIRECHANNELUSERTYPECountModel;
            var dataList = DataCenterCountCls.getFIRECHANNELModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out FIRECHANNELLEVELTYPECountModel, out FIRECHANNELUSERTYPECountModel);
            if (dataList.Any())
            {

                var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        FirechannelCount_Model m = new FirechannelCount_Model();
                        if (Fireleveltype.Contains("1") || string.IsNullOrEmpty(Fireleveltype))//便道数量
                            m.Fireleveltype1Count = item.FIRECHANNELLEVELTYPECountModel.Where(p => p.FIRECHANNELLEVELTYPEVALUE == "1").FirstOrDefault().FIRECHANNELLEVELTYPECount;
                        if (Fireleveltype.Contains("2") || string.IsNullOrEmpty(Fireleveltype))//林区道路数量
                            m.Fireleveltype2Count = item.FIRECHANNELLEVELTYPECountModel.Where(p => p.FIRECHANNELLEVELTYPEVALUE == "2").FirstOrDefault().FIRECHANNELLEVELTYPECount;
                        if (Fireusetype.Contains("1") || string.IsNullOrEmpty(Fireusetype))//人行道数量
                            m.Fireusetypetype1Count = item.FIRECHANNELUSERTYPECountModel.Where(p => p.FIRECHANNELUSERTYPEVALUE == "1").FirstOrDefault().FIRECHANNELUSERTYPECount;
                        if (Fireusetype.Contains("2") || string.IsNullOrEmpty(Fireusetype))//车行道数量
                            m.Fireusetypetype2Count = item.FIRECHANNELUSERTYPECountModel.Where(p => p.FIRECHANNELUSERTYPEVALUE == "2").FirstOrDefault().FIRECHANNELUSERTYPECount;
                        if (Usestate.Contains("1") || string.IsNullOrEmpty(Usestate))//在用数量
                            m.Usestate1Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "1").FirstOrDefault().USESTATECount;
                        if (Usestate.Contains("2") || string.IsNullOrEmpty(Usestate))//规划数量
                            m.Usestate2Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "2").FirstOrDefault().USESTATECount;
                        if (Usestate.Contains("3") || string.IsNullOrEmpty(Usestate))//报废数量
                            m.Usestate3Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "3").FirstOrDefault().USESTATECount;
                        if (Managerstate.Contains("1") || string.IsNullOrEmpty(Managerstate))//未维护数量
                            m.Managerstate1Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "1").FirstOrDefault().MANAGERSTATCount;
                        if (Managerstate.Contains("2") || string.IsNullOrEmpty(Managerstate))//维护数量
                            m.Managerstate2Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "2").FirstOrDefault().MANAGERSTATCount;
                        if (Managerstate.Contains("3") || string.IsNullOrEmpty(Managerstate))//新建数量
                            m.Managerstate3Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "3").FirstOrDefault().MANAGERSTATCount;
                        m.ORGNo = item.BYORGNO;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 宣传碑图表展示
        /// <summary>
        /// 宣传碑图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPropagandasteleChartSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string Propagandasteletype = Request.Params["Propagandasteletype"];
            string Structure = Request.Params["Structure"];
            string Usestate = Request.Params["Usestate"];
            string Managerstate = Request.Params["Managerstate"];
            var result = new List<PropagandasteleCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_UTILITY_PROPAGANDASTELE_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<PROPAGANDASTELETYPECountModel> PROPAGANDASTELETYPECountModel;
            IEnumerable<STRUCTURETYPECountModel> STRUCTURETYPECountModel;
            var dataList = DataCenterCountCls.getPROPAGANDASTELEModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out PROPAGANDASTELETYPECountModel, out STRUCTURETYPECountModel);
            if (dataList.Any())
            {
                var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        PropagandasteleCount_Model m = new PropagandasteleCount_Model();
                        if (Propagandasteletype.Contains("1") || string.IsNullOrEmpty(Propagandasteletype))//永久性数量
                            m.Propagandasteletyp1Count = item.PROPAGANDASTELETYPECountModel.Where(p => p.PROPAGANDASTELETYPEVALUE == "1").FirstOrDefault().DICTPROPAGANDASTELETYPECount;
                        if (Propagandasteletype.Contains("2") || string.IsNullOrEmpty(Propagandasteletype))//永久性数量
                            m.Propagandasteletyp2Count = item.PROPAGANDASTELETYPECountModel.Where(p => p.PROPAGANDASTELETYPEVALUE == "2").FirstOrDefault().DICTPROPAGANDASTELETYPECount;
                        if (Structure.Contains("1") || string.IsNullOrEmpty(Structure))//钢构数量
                            m.Structuretyp1Count = item.STRUCTURETYPECountModel.Where(p => p.STRUCTURETYPEVALUE == "1").FirstOrDefault().STRUCTURETYPECount;
                        if (Structure.Contains("2") || string.IsNullOrEmpty(Structure))//砖混数量
                            m.Structuretyp2Count = item.STRUCTURETYPECountModel.Where(p => p.STRUCTURETYPEVALUE == "2").FirstOrDefault().STRUCTURETYPECount;
                        if (Structure.Contains("3") || string.IsNullOrEmpty(Structure))//钢混数量
                            m.Structuretyp3Count = item.STRUCTURETYPECountModel.Where(p => p.STRUCTURETYPEVALUE == "3").FirstOrDefault().STRUCTURETYPECount;
                        if (Usestate.Contains("1") || string.IsNullOrEmpty(Usestate))//在用数量
                            m.Usestate1Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "1").FirstOrDefault().USESTATECount;
                        if (Usestate.Contains("2") || string.IsNullOrEmpty(Usestate))//规划数量
                            m.Usestate2Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "2").FirstOrDefault().USESTATECount;
                        if (Usestate.Contains("3") || string.IsNullOrEmpty(Usestate))//报废数量
                            m.Usestate3Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "3").FirstOrDefault().USESTATECount;
                        if (Managerstate.Contains("1") || string.IsNullOrEmpty(Managerstate))//未维护数量
                            m.Managerstate1Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "1").FirstOrDefault().MANAGERSTATCount;
                        if (Managerstate.Contains("2") || string.IsNullOrEmpty(Managerstate))//维护数量
                            m.Managerstate2Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "2").FirstOrDefault().MANAGERSTATCount;
                        if (Managerstate.Contains("3") || string.IsNullOrEmpty(Managerstate))//新建数量
                            m.Managerstate3Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "3").FirstOrDefault().MANAGERSTATCount;
                        m.ORGNo = item.BYORGNO;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 监测站的图表展示
        /// <summary>
        /// 监测站的图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMonitoringstationChartSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string Transfermodetype = Request.Params["Transfermodetype"];
            string Usestate = Request.Params["Usestate"];
            string Managerstate = Request.Params["Managerstate"];
            var result = new List<MonitoringstationCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_UTILITY_MONITORINGSTATION_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<TRANSFERMODETYPECountModel> TRANSFERMODETYPECountModel;
            var dataList = DataCenterCountCls.getMONITORINGSTATIONModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out TRANSFERMODETYPECountModel);
            if (dataList.Any())
            {

                var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        MonitoringstationCount_Model m = new MonitoringstationCount_Model();
                        if (Transfermodetype.Contains("1") || string.IsNullOrEmpty(Transfermodetype))//有线数量
                            m.Transfermode1Count = item.TRANSFERMODETYPECountModel.Where(p => p.TRANSFERMODETYPEVALUE == "1").FirstOrDefault().DICTTRANSFERMODETYPECount;
                        if (Transfermodetype.Contains("2") || string.IsNullOrEmpty(Transfermodetype))//无线数量
                            m.Transfermode2Count = item.TRANSFERMODETYPECountModel.Where(p => p.TRANSFERMODETYPEVALUE == "2").FirstOrDefault().DICTTRANSFERMODETYPECount;
                        if (Usestate.Contains("1") || string.IsNullOrEmpty(Usestate))//在用数量
                            m.Usestate1Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "1").FirstOrDefault().USESTATECount;
                        if (Usestate.Contains("2") || string.IsNullOrEmpty(Usestate))//规划数量
                            m.Usestate2Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "2").FirstOrDefault().USESTATECount;
                        if (Usestate.Contains("3") || string.IsNullOrEmpty(Usestate))//报废数量
                            m.Usestate3Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "3").FirstOrDefault().USESTATECount;
                        if (Managerstate.Contains("1") || string.IsNullOrEmpty(Managerstate))//未维护数量
                            m.Managerstate1Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "1").FirstOrDefault().MANAGERSTATCount;
                        if (Managerstate.Contains("2") || string.IsNullOrEmpty(Managerstate))//维护数量
                            m.Managerstate2Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "2").FirstOrDefault().MANAGERSTATCount;
                        if (Managerstate.Contains("3") || string.IsNullOrEmpty(Managerstate))//新建数量
                            m.Managerstate3Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "3").FirstOrDefault().MANAGERSTATCount;
                        m.ORGNo = item.BYORGNO;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 因子采集站的图表展示
        /// <summary>
        /// 因子采集站的图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFactorcollectstationChartSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string Transfermodetype = Request.Params["Transfermodetype"];
            string Usestate = Request.Params["Usestate"];
            string Managerstate = Request.Params["Managerstate"];
            var result = new List<FactorcollectstationCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_UTILITY_FACTORCOLLECTSTATION_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<TRANSFERMODETYPECountModel> TRANSFERMODETYPECountModel;
            var dataList = DataCenterCountCls.getFACTORCOLLECTSTATIONModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out TRANSFERMODETYPECountModel);
            if (dataList.Any())
            {

                var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        FactorcollectstationCount_Model m = new FactorcollectstationCount_Model();
                        if (Transfermodetype.Contains("1") || string.IsNullOrEmpty(Transfermodetype))//有线数量
                            m.Transfermode1Count = item.TRANSFERMODETYPECountModel.Where(p => p.TRANSFERMODETYPEVALUE == "1").FirstOrDefault().DICTTRANSFERMODETYPECount;
                        if (Transfermodetype.Contains("2") || string.IsNullOrEmpty(Transfermodetype))//无线数量
                            m.Transfermode2Count = item.TRANSFERMODETYPECountModel.Where(p => p.TRANSFERMODETYPEVALUE == "2").FirstOrDefault().DICTTRANSFERMODETYPECount;
                        if (Usestate.Contains("1") || string.IsNullOrEmpty(Usestate))//在用数量
                            m.Usestate1Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "1").FirstOrDefault().USESTATECount;
                        if (Usestate.Contains("2") || string.IsNullOrEmpty(Usestate))//规划数量
                            m.Usestate2Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "2").FirstOrDefault().USESTATECount;
                        if (Usestate.Contains("3") || string.IsNullOrEmpty(Usestate))//报废数量
                            m.Usestate3Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "3").FirstOrDefault().USESTATECount;
                        if (Managerstate.Contains("1") || string.IsNullOrEmpty(Managerstate))//未维护数量
                            m.Managerstate1Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "1").FirstOrDefault().MANAGERSTATCount;
                        if (Managerstate.Contains("2") || string.IsNullOrEmpty(Managerstate))//维护数量
                            m.Managerstate2Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "2").FirstOrDefault().MANAGERSTATCount;
                        if (Managerstate.Contains("3") || string.IsNullOrEmpty(Managerstate))//新建数量
                            m.Managerstate3Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "3").FirstOrDefault().MANAGERSTATCount;
                        m.ORGNo = item.BYORGNO;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 资源的图表展示
        /// <summary>
        /// 资源的图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetResourseChartSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string Agetype = Request.Params["Agetype"];
            string Resourcetype = Request.Params["Resourcetype"];
            string Originttype = Request.Params["Originttype"];
            string Burntype = Request.Params["Burntype"];
            string Treetype = Request.Params["Treetype"];
            var result = new List<ResourseCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_RESOURCE_NEW_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<RESOURCETYPECountModel> RESOURCETYPECountModel;
            IEnumerable<AGETYPECountModel> AGETYPECountModel;
            IEnumerable<ORIGINTYPECountModel> ORIGINTYPECountModel;
            IEnumerable<BURNTYPECountModel> BURNTYPECountModel;
            IEnumerable<TREETYPECountModel> TREETYPECountModel;
            var dataList = DataCenterCountCls.getRESOURCEModelCount(sw, out RESOURCETYPECountModel, out AGETYPECountModel, out ORIGINTYPECountModel, out BURNTYPECountModel, out TREETYPECountModel);
            if (dataList.Any())
            {
                var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        ResourseCount_Model m = new ResourseCount_Model();
                        if (Resourcetype.Contains("1"))
                            m.Resourcetype1Count = item.RESOURCETYPECountModel.Where(p => p.RESOURCETYPEVALUE == "1").FirstOrDefault().DICTRESOURCETYPECount;
                        m.ResourcetypeArea1Count = item.RESOURCETYPECountModel.Where(p => p.RESOURCETYPEVALUE == "1").FirstOrDefault().AREATYPE1Count;
                        if (Resourcetype.Contains("2"))
                            m.Resourcetype2Count = item.RESOURCETYPECountModel.Where(p => p.RESOURCETYPEVALUE == "2").FirstOrDefault().DICTRESOURCETYPECount;
                        m.ResourcetypeArea2Count = item.RESOURCETYPECountModel.Where(p => p.RESOURCETYPEVALUE == "2").FirstOrDefault().AREATYPE1Count;
                        if (Resourcetype.Contains("3"))
                            m.Resourcetype3Count = item.RESOURCETYPECountModel.Where(p => p.RESOURCETYPEVALUE == "3").FirstOrDefault().DICTRESOURCETYPECount;
                        m.ResourcetypeArea3Count = item.RESOURCETYPECountModel.Where(p => p.RESOURCETYPEVALUE == "3").FirstOrDefault().AREATYPE1Count;
                        if (Resourcetype.Contains("4"))
                            m.Resourcetype4Count = item.RESOURCETYPECountModel.Where(p => p.RESOURCETYPEVALUE == "4").FirstOrDefault().DICTRESOURCETYPECount;
                        m.ResourcetypeArea4Count = item.RESOURCETYPECountModel.Where(p => p.RESOURCETYPEVALUE == "4").FirstOrDefault().AREATYPE1Count;
                        if (Agetype.Contains("1"))
                            m.Agetype1Count = item.AGETYPECountModel.Where(p => p.AGETYPEVALUE == "1").FirstOrDefault().DICTAGETYPECount;
                        m.AgetypeArea1Count = item.AGETYPECountModel.Where(p => p.AGETYPEVALUE == "1").FirstOrDefault().AREATYPE2Count;
                        if (Agetype.Contains("2"))
                            m.Agetype2Count = item.AGETYPECountModel.Where(p => p.AGETYPEVALUE == "2").FirstOrDefault().DICTAGETYPECount;
                        m.AgetypeArea2Count = item.AGETYPECountModel.Where(p => p.AGETYPEVALUE == "2").FirstOrDefault().AREATYPE2Count;
                        if (Agetype.Contains("3"))
                            m.Agetype3Count = item.AGETYPECountModel.Where(p => p.AGETYPEVALUE == "3").FirstOrDefault().DICTAGETYPECount;
                        m.AgetypeArea3Count = item.AGETYPECountModel.Where(p => p.AGETYPEVALUE == "3").FirstOrDefault().AREATYPE2Count;
                        if (Agetype.Contains("4"))
                            m.Agetype4Count = item.AGETYPECountModel.Where(p => p.AGETYPEVALUE == "4").FirstOrDefault().DICTAGETYPECount;
                        m.AgetypeArea4Count = item.AGETYPECountModel.Where(p => p.AGETYPEVALUE == "4").FirstOrDefault().AREATYPE2Count;
                        if (Agetype.Contains("5"))
                            m.Agetype5Count = item.AGETYPECountModel.Where(p => p.AGETYPEVALUE == "5").FirstOrDefault().DICTAGETYPECount;
                        m.AgetypeArea5Count = item.AGETYPECountModel.Where(p => p.AGETYPEVALUE == "5").FirstOrDefault().AREATYPE2Count;
                        if (Originttype.Contains("1"))
                            m.Originttype1Count = item.ORIGINTYPECountModel.Where(p => p.ORIGINTYPEVALUE == "1").FirstOrDefault().DICORIGINTYPECount;
                        m.OriginttypeArea1Count = item.ORIGINTYPECountModel.Where(p => p.ORIGINTYPEVALUE == "1").FirstOrDefault().AREATYPE3Count;
                        if (Originttype.Contains("2"))
                            m.Originttype2Count = item.ORIGINTYPECountModel.Where(p => p.ORIGINTYPEVALUE == "2").FirstOrDefault().DICORIGINTYPECount;
                        m.OriginttypeArea2Count = item.ORIGINTYPECountModel.Where(p => p.ORIGINTYPEVALUE == "2").FirstOrDefault().AREATYPE3Count;
                        if (Burntype.Contains("1"))
                            m.Burntype1Count = item.BURNTYPECountModel.Where(p => p.BURNTYPEVALUE == "1").FirstOrDefault().DICTBURNTYPETYPECount;
                        m.BurntypeArea1Count = item.BURNTYPECountModel.Where(p => p.BURNTYPEVALUE == "1").FirstOrDefault().AREATYPE4Count;
                        if (Burntype.Contains("2"))
                            m.Burntype2Count = item.BURNTYPECountModel.Where(p => p.BURNTYPEVALUE == "2").FirstOrDefault().DICTBURNTYPETYPECount;
                        m.BurntypeArea2Count = item.BURNTYPECountModel.Where(p => p.BURNTYPEVALUE == "2").FirstOrDefault().AREATYPE4Count;
                        if (Treetype.Contains("1"))
                            m.Treetype1Count = item.TREETYPECountModel.Where(p => p.TREETYPEVALUE == "1").FirstOrDefault().DICTTREETYPECount;
                        m.TreetypeArea1Count = item.TREETYPECountModel.Where(p => p.TREETYPEVALUE == "1").FirstOrDefault().AREATYPE5Count;
                        if (Treetype.Contains("2"))
                            m.Treetype2Count = item.TREETYPECountModel.Where(p => p.TREETYPEVALUE == "2").FirstOrDefault().DICTTREETYPECount;
                        m.TreetypeArea2Count = item.TREETYPECountModel.Where(p => p.TREETYPEVALUE == "2").FirstOrDefault().AREATYPE5Count;
                        if (Treetype.Contains("3"))
                            m.Treetype3Count = item.TREETYPECountModel.Where(p => p.TREETYPEVALUE == "3").FirstOrDefault().DICTTREETYPECount;
                        m.TreetypeArea3Count = item.TREETYPECountModel.Where(p => p.TREETYPEVALUE == "3").FirstOrDefault().AREATYPE5Count;
                        m.ORGNo = item.ORGNo;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 隔离带的图表展示
        /// <summary>
        /// 隔离带的图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetISOLATIONTYPEChartSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string ISOLATIONTYPE = Request.Params["ISOLATIONTYPE"];
            string Usestate = Request.Params["Usestate"];
            string Managerstate = Request.Params["Managerstate"];
            var result = new List<IsolationstripCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new DC_UTILITY_ISOLATIONSTRIP_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<USESTATECountModel> USESTATECountModel;
            IEnumerable<MANAGERSTATECountModel> MANAGERSTATECountModel;
            IEnumerable<ISOLATIONTYPECountModel> ISOLATIONTYPECountModel;
            var dataList = DataCenterCountCls.getISOLATIONSTRIPModelCount(sw, out USESTATECountModel, out MANAGERSTATECountModel, out ISOLATIONTYPECountModel);
            if (dataList.Any())
            {
                var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                if (list.Any())
                {
                    foreach (var item in list)
                    {
                        IsolationstripCount_Model m = new IsolationstripCount_Model();
                        if (ISOLATIONTYPE.Contains("1") || string.IsNullOrEmpty(ISOLATIONTYPE))
                            m.IsolationstripType1Count = item.ISOLATIONTYPECountModel.Where(p => p.ISOLATIONTYPEVALUE == "1").FirstOrDefault().DICTISOLATIONTYPECount;
                        m.IsolationstripTypeLength1Count = item.ISOLATIONTYPECountModel.Where(p => p.ISOLATIONTYPEVALUE == "1").FirstOrDefault().DICTISOLATIONTYPELENGTHCount;
                        if (ISOLATIONTYPE.Contains("2") || string.IsNullOrEmpty(ISOLATIONTYPE))
                            m.IsolationstripType2Count = item.ISOLATIONTYPECountModel.Where(p => p.ISOLATIONTYPEVALUE == "2").FirstOrDefault().DICTISOLATIONTYPECount;
                        m.IsolationstripTypeLength2Count = item.ISOLATIONTYPECountModel.Where(p => p.ISOLATIONTYPEVALUE == "2").FirstOrDefault().DICTISOLATIONTYPELENGTHCount;
                        if (ISOLATIONTYPE.Contains("3") || string.IsNullOrEmpty(ISOLATIONTYPE))
                            m.IsolationstripType3Count = item.ISOLATIONTYPECountModel.Where(p => p.ISOLATIONTYPEVALUE == "3").FirstOrDefault().DICTISOLATIONTYPECount;
                        m.IsolationstripTypeLength3Count = item.ISOLATIONTYPECountModel.Where(p => p.ISOLATIONTYPEVALUE == "3").FirstOrDefault().DICTISOLATIONTYPELENGTHCount;
                        if (ISOLATIONTYPE.Contains("4") || string.IsNullOrEmpty(ISOLATIONTYPE))
                            m.IsolationstripType4Count = item.ISOLATIONTYPECountModel.Where(p => p.ISOLATIONTYPEVALUE == "4").FirstOrDefault().DICTISOLATIONTYPECount;
                        m.IsolationstripTypeLength4Count = item.ISOLATIONTYPECountModel.Where(p => p.ISOLATIONTYPEVALUE == "4").FirstOrDefault().DICTISOLATIONTYPELENGTHCount;
                        if (ISOLATIONTYPE.Contains("5") || string.IsNullOrEmpty(ISOLATIONTYPE))
                            m.IsolationstripType5Count = item.ISOLATIONTYPECountModel.Where(p => p.ISOLATIONTYPEVALUE == "5").FirstOrDefault().DICTISOLATIONTYPECount;
                        m.IsolationstripTypeLength5Count = item.ISOLATIONTYPECountModel.Where(p => p.ISOLATIONTYPEVALUE == "5").FirstOrDefault().DICTISOLATIONTYPELENGTHCount;
                        if (Usestate.Contains("1") || string.IsNullOrEmpty(Usestate))//在用数量
                            m.Usestate1Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "1").FirstOrDefault().USESTATECount;
                        m.UsestateLength1Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "1").FirstOrDefault().USESTATELENGTHCount;
                        if (Usestate.Contains("2") || string.IsNullOrEmpty(Usestate))//规划数量
                            m.Usestate2Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "2").FirstOrDefault().USESTATECount;
                        m.UsestateLength2Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "2").FirstOrDefault().USESTATELENGTHCount;
                        if (Usestate.Contains("3") || string.IsNullOrEmpty(Usestate))//报废数量
                            m.Usestate3Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "3").FirstOrDefault().USESTATECount;
                        m.UsestateLength3Count = item.USESTATECountModel.Where(p => p.USESTATEVALUE == "3").FirstOrDefault().USESTATELENGTHCount;
                        if (Managerstate.Contains("1") || string.IsNullOrEmpty(Managerstate))//未维护数量
                            m.Managerstate1Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "1").FirstOrDefault().MANAGERSTATCount;
                        m.ManagerstateLength1Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "1").FirstOrDefault().MANAGERSTATLENGTHCount;
                        if (Managerstate.Contains("2") || string.IsNullOrEmpty(Managerstate))//维护数量
                            m.Managerstate2Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "2").FirstOrDefault().MANAGERSTATCount;
                        m.ManagerstateLength2Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "2").FirstOrDefault().MANAGERSTATLENGTHCount;
                        if (Managerstate.Contains("3") || string.IsNullOrEmpty(Managerstate))//新建数量
                            m.Managerstate3Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "3").FirstOrDefault().MANAGERSTATCount;
                        m.ManagerstateLength3Count = item.MANAGERSTATECountModel.Where(p => p.MANAGERSTATVALUE == "3").FirstOrDefault().MANAGERSTATLENGTHCount;
                        m.ORGNo = item.BYORGNO;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 护林员统计的图表展示
        /// <summary>
        /// 护林员统计的图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetHUSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string Sextype = Request.Params["Sextype"];
            string Onstate = Request.Params["Onstate"];
            var result = new List<HUReport_HUCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            T_IPSFR_USER_SW sw = new T_IPSFR_USER_SW();
            sw.TopORGNO = TopEchart;
            var dataList = HUReportCls.getHUCountModel(sw); ;
            if (dataList.Any())
            {
                if (PublicCls.OrgIsShi(TopEchart))
                {
                    var list = dataList.Where(p => p.ORGNo != null && p.ORGName.IndexOf("合计") == -1);
                    if (list.Any())
                    {
                        foreach (var item in list)
                        {
                            HUReport_HUCount_Model m = new HUReport_HUCount_Model();
                            if (Sextype.Contains("1"))
                                m.Sex0Count = item.Sex0Count;
                            if (Sextype.Contains("0"))
                                m.Sex1Count = item.Sex1Count;
                            if (Onstate.Contains("1"))
                                m.Onstate0Count = item.Onstate0Count;
                            if (Onstate.Contains("2"))
                                m.Onstate1Count = item.Onstate1Count;
                            m.ORGNo = item.ORGNo;
                            m.ORGName = item.ORGName;
                            result.Add(m);
                        }
                    }
                }
                else if (PublicCls.OrgIsXian(TopEchart))
                {
                    var list = dataList.Where(p => p.ORGNo != null && p.ORGName.IndexOf("合计") == -1);
                    foreach (var item in list)
                    {
                        HUReport_HUCount_Model m = new HUReport_HUCount_Model();
                        if (Sextype.Contains("1"))
                            m.Sex0Count = item.Sex0Count;
                        if (Sextype.Contains("0"))
                            m.Sex1Count = item.Sex1Count;
                        if (Onstate.Contains("1"))
                            m.Onstate0Count = item.Onstate0Count;
                        if (Onstate.Contains("2"))
                            m.Onstate1Count = item.Onstate1Count;
                        m.ORGNo = item.ORGNo;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                else
                {
                    var list = dataList.Where(p => p.ORGNo == TopEchart);
                    foreach (var item in list)
                    {
                        HUReport_HUCount_Model m = new HUReport_HUCount_Model();
                        if (Sextype.Contains("1"))
                            m.Sex0Count = item.Sex0Count;
                        if (Sextype.Contains("0"))
                            m.Sex1Count = item.Sex1Count;
                        if (Onstate.Contains("1"))
                            m.Onstate0Count = item.Onstate0Count;
                        if (Onstate.Contains("2"))
                            m.Onstate1Count = item.Onstate1Count;
                        m.ORGNo = item.ORGNo;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }

                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 巡检路线统计的图表展示
        /// <summary>
        /// 巡检路线统计的图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPatrolRouteData()
        {
            PatrolRouteStat_SW sw = new PatrolRouteStat_SW();
            sw.orgNo = Request.Params["TopEchart"];
            sw.TopORGNO = Request.Params["TopEchart"];
            sw.DateBegin = Request.Params["DateBegin"];
            sw.DateEnd = Request.Params["DateEnd"];
            string LineCount = Request.Params["LineCount"];
            string PointCount = Request.Params["PointCount"];
            var result = new List<HUReport_PatrolRouteStat_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var dataList = HUReportCls.getPatrolRouteStatModel(sw); ;
            if (dataList.Any())
            {
                if (PublicCls.OrgIsZhen(sw.orgNo))
                {
                    var list = dataList.Where(p => p.ORGNo == sw.orgNo);
                    foreach (var item in list)
                    {
                        HUReport_PatrolRouteStat_Model m = new HUReport_PatrolRouteStat_Model();
                        if (LineCount.Contains("1"))
                            m.LineCount0 = item.LineCount0;
                        if (LineCount.Contains("2"))
                            m.LineCount1 = item.LineCount1;
                        if (LineCount.Contains("3"))
                            m.LineCount2 = item.LineCount2;
                        if (PointCount.Contains("1"))
                            m.PointCount0 = item.PointCount0;
                        if (PointCount.Contains("2"))
                            m.PointCount1 = item.PointCount1;
                        if (PointCount.Contains("3"))
                            m.PointCount2 = item.PointCount2;
                        m.ORGNo = item.ORGNo;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                else
                {
                    var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                    if (list.Any())
                    {
                        foreach (var item in list)
                        {
                            HUReport_PatrolRouteStat_Model m = new HUReport_PatrolRouteStat_Model();
                            if (LineCount.Contains("1"))
                                m.LineCount0 = item.LineCount0;
                            if (LineCount.Contains("2"))
                                m.LineCount1 = item.LineCount1;
                            if (LineCount.Contains("3"))
                                m.LineCount2 = item.LineCount2;
                            if (PointCount.Contains("1"))
                                m.PointCount0 = item.PointCount0;
                            if (PointCount.Contains("2"))
                                m.PointCount1 = item.PointCount1;
                            if (PointCount.Contains("3"))
                                m.PointCount2 = item.PointCount2;
                            m.ORGNo = item.ORGNo;
                            m.ORGName = item.ORGName;
                            result.Add(m);
                        }
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 上报统计图表展示
        /// <summary>
        /// 上报统计图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetReportData()
        {
            T_IPSRPT_REPORT_SW sw = new T_IPSRPT_REPORT_SW();
            sw.orgNo = Request.Params["TopEchart"];
            sw.TopORGNO = Request.Params["TopEchart"];
            sw.DateBegin = Request.Params["DateBegin"];
            sw.DateEnd = Request.Params["DateEnd"];
            string ReportCount = Request.Params["ReportCount"];
            var result = new List<ReportCount_Model>();
            IEnumerable<T_IPSRPT_REPORT_TypeCountModel> typeModel;
            MessageListObject ms = new MessageListObject(false, null);
            var dataList = T_IPSRPT_REPORTCls.getModelCount(sw, out typeModel);//查询结果
            if (dataList.Any())
            {
                if (PublicCls.OrgIsZhen(sw.orgNo))
                {
                    var list = dataList.Where(p => p.ORGNo == sw.orgNo);
                    foreach (var item in list)
                    {
                        ReportCount_Model m = new ReportCount_Model();
                        if (ReportCount.Contains("1"))
                            m.ReportType1Count = item.TypeCountModel.Where(p => p.typeID == "1").FirstOrDefault().typeCount;
                        if (ReportCount.Contains("2"))
                            m.ReportType2Count = item.TypeCountModel.Where(p => p.typeID == "2").FirstOrDefault().typeCount;
                        if (ReportCount.Contains("3"))
                            m.ReportType3Count = item.TypeCountModel.Where(p => p.typeID == "3").FirstOrDefault().typeCount;
                        if (ReportCount.Contains("4"))
                            m.ReportType4Count = item.TypeCountModel.Where(p => p.typeID == "4").FirstOrDefault().typeCount;
                        m.ORGNo = item.ORGNo;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                else
                {
                    var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                    if (list.Any())
                    {
                        foreach (var item in list)
                        {
                            ReportCount_Model m = new ReportCount_Model();
                            if (ReportCount.Contains("1"))
                                m.ReportType1Count = item.TypeCountModel.Where(p => p.typeID == "1").FirstOrDefault().typeCount;
                            if (ReportCount.Contains("2"))
                                m.ReportType2Count = item.TypeCountModel.Where(p => p.typeID == "2").FirstOrDefault().typeCount;
                            if (ReportCount.Contains("3"))
                                m.ReportType3Count = item.TypeCountModel.Where(p => p.typeID == "3").FirstOrDefault().typeCount;
                            if (ReportCount.Contains("4"))
                                m.ReportType4Count = item.TypeCountModel.Where(p => p.typeID == "4").FirstOrDefault().typeCount;
                            m.ORGNo = item.ORGNo;
                            m.ORGName = item.ORGName;
                            result.Add(m);
                        }
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 采集统计图表展示
        /// <summary>
        /// 采集统计图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetCollectData()
        {
            T_IPSCOL_COLLECT_SW sw = new T_IPSCOL_COLLECT_SW();
            sw.orgNo = Request.Params["TopEchart"];
            sw.TopORGNO = Request.Params["TopEchart"];
            sw.DateBegin = Request.Params["DateBegin"];
            sw.DateEnd = Request.Params["DateEnd"];
            string CollectCount = Request.Params["CollectCount"];
            var result = new List<CollectCount_Model>();
            IEnumerable<T_IPSCOL_COLLECT_TypeCountModel> typeModel;
            MessageListObject ms = new MessageListObject(false, null);
            var dataList = T_IPSCOL_COLLECTCls.getModelCount(sw, out typeModel);//查询结果
            if (dataList.Any())
            {
                if (PublicCls.OrgIsZhen(sw.orgNo))
                {
                    var list = dataList.Where(p => p.ORGNo == sw.orgNo);
                    foreach (var item in list)
                    {
                        CollectCount_Model m = new CollectCount_Model();
                        if (CollectCount.Contains("1"))
                            m.CollectType1Count = item.TypeCountModel.Where(p => p.typeID == "1").FirstOrDefault().typeCount;
                        if (CollectCount.Contains("2"))
                            m.CollectType2Count = item.TypeCountModel.Where(p => p.typeID == "2").FirstOrDefault().typeCount;
                        if (CollectCount.Contains("3"))
                            m.CollectType3Count = item.TypeCountModel.Where(p => p.typeID == "3").FirstOrDefault().typeCount;
                        if (CollectCount.Contains("4"))
                            m.CollectType4Count = item.TypeCountModel.Where(p => p.typeID == "4").FirstOrDefault().typeCount;
                        m.ORGNo = item.ORGNo;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                else
                {
                    var list = dataList.Where(p => p.ORGName.IndexOf("合计") == -1);
                    if (list.Any())
                    {
                        foreach (var item in list)
                        {
                            CollectCount_Model m = new CollectCount_Model();
                            if (CollectCount.Contains("1"))
                                m.CollectType1Count = item.TypeCountModel.Where(p => p.typeID == "1").FirstOrDefault().typeCount;
                            if (CollectCount.Contains("2"))
                                m.CollectType2Count = item.TypeCountModel.Where(p => p.typeID == "2").FirstOrDefault().typeCount;
                            if (CollectCount.Contains("3"))
                                m.CollectType3Count = item.TypeCountModel.Where(p => p.typeID == "3").FirstOrDefault().typeCount;
                            if (CollectCount.Contains("4"))
                                m.CollectType4Count = item.TypeCountModel.Where(p => p.typeID == "4").FirstOrDefault().typeCount;
                            m.ORGNo = item.ORGNo;
                            m.ORGName = item.ORGName;
                            result.Add(m);
                        }
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion

        #region 档案统计图表展示
        /// <summary>
        /// 档案统计图表展示
        /// </summary>
        /// <returns></returns>
        public JsonResult GetArchivalChartSourceData()
        {
            string TopEchart = Request.Params["TopEchart"];
            string Hotetype = Request.Params["Hotetype"];
            string Firefrom = Request.Params["Firefrom"];
            string Firelevel = Request.Params["Firelevel"];
            var result = new List<ArchvalCount_Model>();
            MessageListObject ms = new MessageListObject(false, null);
            var sw = new JC_FIRE_SW();
            sw.TopORGNO = TopEchart;
            IEnumerable<HOTTYPECountModel> HOTTYPECountModel;
            IEnumerable<FIRELEVELCountModel> FIRELEVELCountModel;
            var dataList = DataCenterCountCls.getArchivalModelCount(sw, out HOTTYPECountModel, out FIRELEVELCountModel);
            if (dataList.Any())
            {
                if (dataList.Any())
                {
                    foreach (var item in dataList)
                    {
                        ArchvalCount_Model m = new ArchvalCount_Model();
                        if (Hotetype.Contains("1") )
                            m.Hotetype1Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "1").FirstOrDefault().DictHOTTYPECount;
                        if (Hotetype.Contains("2") )
                            m.Hotetype2Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "2").FirstOrDefault().DictHOTTYPECount;
                        if (Hotetype.Contains("3"))
                            m.Hotetype3Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "3").FirstOrDefault().DictHOTTYPECount;
                        if (Hotetype.Contains("4"))
                            m.Hotetype4Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "4").FirstOrDefault().DictHOTTYPECount;
                        if (Hotetype.Contains("5"))
                            m.Hotetype5Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "5").FirstOrDefault().DictHOTTYPECount;
                        if (Hotetype.Contains("6"))
                            m.Hotetype6Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "6").FirstOrDefault().DictHOTTYPECount;
                        if (Hotetype.Contains("7"))
                            m.Hotetype7Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "7").FirstOrDefault().DictHOTTYPECount;
                        if (Hotetype.Contains("8"))
                            m.Hotetype8Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "8").FirstOrDefault().DictHOTTYPECount;
                        if (Hotetype.Contains("9"))
                            m.Hotetype9Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "9").FirstOrDefault().DictHOTTYPECount;
                        if (Hotetype.Contains("10"))
                            m.Hotetype10Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "10").FirstOrDefault().DictHOTTYPECount;
                        if (Hotetype.Contains("11"))
                            m.Hotetype11Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "11").FirstOrDefault().DictHOTTYPECount;
                        if (Hotetype.Contains("12"))
                            m.Hotetype12Count = item.HOTTYPECountModel.Where(p => p.HOTTYPEvalue == "12").FirstOrDefault().DictHOTTYPECount;
                        if (Firelevel.Contains("1"))
                            m.FireLevelCount = item.FIRELEVELCountModel.Where(p => p.FIRELEVELvalue == "1").FirstOrDefault().DictFIRELEVELCount;
                        if (Firelevel.Contains("2"))
                            m.FireLevel2Count = item.FIRELEVELCountModel.Where(p => p.FIRELEVELvalue == "2").FirstOrDefault().DictFIRELEVELCount;
                        if (Firelevel.Contains("3"))
                            m.FireLevel3Count = item.FIRELEVELCountModel.Where(p => p.FIRELEVELvalue == "3").FirstOrDefault().DictFIRELEVELCount;
                        if (Firelevel.Contains("4"))
                            m.FireLevel4Count = item.FIRELEVELCountModel.Where(p => p.FIRELEVELvalue == "4").FirstOrDefault().DictFIRELEVELCount;
                        if (Firefrom.Contains("1"))
                            m.FireFrom1Count = item.FIREFROMCount1;
                        if (Firefrom.Contains("2"))
                            m.FireFrom2Count = item.FIREFROMCount2;
                        if (Firefrom.Contains("3"))
                            m.FireFrom3Count = item.FIREFROMCount3;
                        if (Firefrom.Contains("4"))
                            m.FireFrom4Count = item.FIREFROMCount4;
                        if (Firefrom.Contains("5"))
                            m.FireFrom5Count = item.FIREFROMCount5;
                        if (Firefrom.Contains("6"))
                            m.FireFrom6Count = item.FIREFROMCount6;
                        if (Firefrom.Contains("50"))
                            m.FireFrom7Count = item.FIREFROMCount7;
                        m.ORGNo = item.BYORGNO;
                        m.ORGName = item.ORGName;
                        result.Add(m);
                    }
                }
                ms = new MessageListObject(true, result);
            }
            return Json(ms);
        }
        #endregion
    }
}
