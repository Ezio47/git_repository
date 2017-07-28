using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerSystemModel
{
    /// <summary>
    /// 数据中心队伍统计模型
    /// </summary>
    public class DC_ARMYCount_Model
    {
        /// <summary>
        /// 队伍id
        /// </summary>
        public string DC_ARMY_ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 队伍统计
        /// </summary>
        public string ARMYCount { get; set; }
        /// <summary>
        /// 队伍名称
        /// </summary>
        public string ARMYTYPEName { get; set; }
        /// <summary>
        /// 队伍类型
        /// </summary>
        public string ARMYTYPE { get; set; }
        /// <summary>
        /// 人数统计
        /// </summary>
        public string MEMBERCount { get; set; }
        /// <summary>
        /// 队伍类型统计
        /// </summary>
        public IEnumerable<DC_ARMY_TypeCountModel> TypeCountModel { get; set; }
    }
    /// <summary>
    /// 队伍类型统计Model
    /// </summary>
    public class DC_ARMY_TypeCountModel
    {
        /// <summary>
        /// 队伍类型值
        /// </summary>
        public string DICTVALUE { get; set; }
        /// <summary>
        /// 队伍类型名称
        /// </summary>
        public string DICTNAME { get; set; }
        /// <summary>
        /// 队伍类型统计
        /// </summary>
        public string ARMYTYPECount { get; set; }
        /// <summary>
        /// 队伍人员类型统计
        /// </summary>
        public string MEMBERTYPECount { get; set; }
    }
    
    /// <summary>
    /// 数据中心资源统计模型
    /// </summary>
    public class DC_RESOURCECount_Model
    {
        /// <summary>
        /// 资源id
        /// </summary>
        public string DC_RESOURCE_NEW_ID { get; set; }
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string ORGNo { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 资源名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public string RESOURCETYPE { get; set; }
        /// <summary>
        /// 资源类型统计
        /// </summary>
        public string RESOURCETYPECount { get; set; }
        /// <summary>
        /// 林龄类别
        /// </summary>
        public string AGETYPE { get; set; }
        /// <summary>
        /// 林龄类别统计
        /// </summary>
        public string AGETYPECount { get; set; }
        /// <summary>
        /// 起源类型
        /// </summary>
        public string ORIGINTYPE { get; set; }
        /// <summary>
        /// 起源类型统计
        /// </summary>
        public string ORIGINTYPECount { get; set; }
        /// <summary>
        /// 可燃类型
        /// </summary>
        public string BURNTYPE { get; set; }
        /// <summary>
        /// 可燃类型统计
        /// </summary>
        public string BURNTYPECount { get; set; }
        /// <summary>
        /// 林木类型
        /// </summary>
        public string TREETYPE { get; set; }
        /// <summary>
        /// 林木类型统计
        /// </summary>
        public string TREETYPECount { get; set; }
        /// <summary>
        /// 面积统计
        /// </summary>
        public string AREACount { get; set; }
        /// <summary>
        /// 资源类型统计模型
        /// </summary>
        public IEnumerable<RESOURCETYPECountModel> RESOURCETYPECountModel { get; set; }
        /// <summary>
        /// 林龄类别统计模型
        /// </summary>
        public IEnumerable<AGETYPECountModel> AGETYPECountModel { get; set; }
        /// <summary>
        /// 起源类型统计模型
        /// </summary>
        public IEnumerable<ORIGINTYPECountModel> ORIGINTYPECountModel { get; set; }
        /// <summary>
        /// 可燃类型统计模型
        /// </summary>
        public IEnumerable<BURNTYPECountModel> BURNTYPECountModel { get; set; }
        /// <summary>
        /// 林木类型统计模型
        /// </summary>
        public IEnumerable<TREETYPECountModel> TREETYPECountModel { get; set; }

    }
    /// <summary>
    /// 资源类型统计模型
    /// </summary>
    public class RESOURCETYPECountModel
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string RESOURCETYPEVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string RESOURCETYPENAME { get; set; }
        /// <summary>
        /// 资源类型统计
        /// </summary>
        public string DICTRESOURCETYPECount { get; set; }
        /// <summary>
        /// 资源类型下面积统计
        /// </summary>
        public string AREATYPE1Count { get; set; }
       
    }
    /// <summary>
    /// 林龄类别统计模型
    /// </summary>
    public class AGETYPECountModel
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string AGETYPEVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string AGETYPENAME { get; set; }
        /// <summary>
        /// 林龄类别统计
        /// </summary>
        public string DICTAGETYPECount { get; set; }
        /// <summary>
        /// 林龄类别类型下面积统计
        /// </summary>
        public string AREATYPE2Count { get; set; }

    }
    /// <summary>
    /// 起源类型统计模型
    /// </summary>
    public class ORIGINTYPECountModel
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string ORIGINTYPEVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string ORIGINTYPENAME { get; set; }
        /// <summary>
        /// 林龄类别统计
        /// </summary>
        public string DICORIGINTYPECount { get; set; }
        /// <summary>
        /// 起源类型类型下面积统计
        /// </summary>
        public string AREATYPE3Count { get; set; }

    }
    /// <summary>
    /// 可燃类型统计模型
    /// </summary>
    public class BURNTYPECountModel
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string BURNTYPEVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string BURNTYPENAME { get; set; }
        /// <summary>
        /// 可燃类型统计
        /// </summary>
        public string DICTBURNTYPETYPECount { get; set; }
        /// <summary>
        /// 可燃类型下面积统计
        /// </summary>
        public string AREATYPE4Count { get; set; }
        

    }
    /// <summary>
    /// 林木类型统计模型
    /// </summary>
    public class TREETYPECountModel
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string TREETYPEVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TREETYPENAME { get; set; }
        /// <summary>
        /// 可燃类型统计
        /// </summary>
        public string DICTTREETYPECount { get; set; }
        /// <summary>
        /// 林木类型下面积统计
        /// </summary>
        public string AREATYPE5Count { get; set; }

    }
    /// <summary>
    /// 数据中心装备统计模型
    /// </summary>
    public class DC_EQUIPCount_Model
    {
        /// <summary>
        /// 装备id
        /// </summary>
        public string DC_EQUIP_NEW_ID { get; set; }
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 装备名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 装备类型
        /// </summary>
        public string EQUIPTYPE { get; set; }
        /// <summary>
        /// 装备类型统计
        /// </summary>
        public string EQUIPTYPECount { get; set; }
        /// <summary>
        /// 使用现状
        /// </summary>
        public string USESTATE { get; set; }
        /// <summary>
        /// 使用现状统计
        /// </summary>
        public string USESTATECount { get; set; }
        /// <summary>
        /// 装备类型统计模型
        /// </summary>
        public IEnumerable<EQUIPTYPECountModel> EQUIPTYPECountModel { get; set; }
        /// <summary>
        /// 使用现状统计模型
        /// </summary>
        public IEnumerable<USESTATECountModel> USESTATECountModel { get; set; }     
    }
    /// <summary>
    /// 装备类型统计模型
    /// </summary>
    public class EQUIPTYPECountModel
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string EQUIPTYPEVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string EQUIPTYPENAME { get; set; }
        /// <summary>
        /// 装备类型统计
        /// </summary>
        public string DICTEQUIPTYPECount { get; set; }

    }
    /// <summary>
    /// 使用形状统计模型
    /// </summary>
    public class USESTATECountModel
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string USESTATEVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string USESTATENAME { get; set; }
        /// <summary>
        /// 使用现状统计
        /// </summary>
        public string USESTATECount { get; set; }
        /// <summary>
        /// 使用现状长度统计
        /// </summary>
        public string USESTATELENGTHCount { get; set; }

    }
    /// <summary>
    /// 数据中心车辆统计模型
    /// </summary>
    public class DC_CARCount_Model
    {
        /// <summary>
        /// 车辆id
        /// </summary>
        public string DC_CAR_ID { get; set; }
        /// <summary>
        /// 组织机构码
        /// </summary>
        public string BYORGNO { get; set; }
        /// <summary>
        /// 组织机构名
        /// </summary>
        public string ORGName { get; set; }
        /// <summary>
        /// 车辆名称
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 车辆类型
        /// </summary>
        public string CARTYPE { get; set; }
        /// <summary>
        /// 车辆类型统计
        /// </summary>
        public string CARTYPECount { get; set; }       
        /// <summary>
        /// 装备类型统计模型
        /// </summary>
        public IEnumerable<CARTYPECountModel> CARTYPECountModel { get; set; }
      
    }
    /// <summary>
    /// 车辆类型统计模型
    /// </summary>
    public class CARTYPECountModel
    {
        /// <summary>
        /// 类型值
        /// </summary>
        public string CARTYPEVALUE { get; set; }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string CARTYPENAME { get; set; }
        /// <summary>
        /// 车辆类型统计
        /// </summary>
        public string CARTYPECount { get; set; }

    }
}
