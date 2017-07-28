using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagerSystem.MVC.HelpCom
{
    public enum EnumType
    {
        红外相机 = 1,
        卫星监控 = 2,
        电话报警 = 3,
        电子监控 = 4,
        瞭望护林员 = 5,
        无人机巡护 = 6

    }

    /// <summary>
    /// 数据中心资源等对应表
    /// </summary>
    public enum DbCenterSourceType
    {
        DC_ARMY = 1,
        DC_RESOURCE_NEW = 2,
        DC_UTILITY_CAMP = 3,
        DC_EQUIP_NEW = 4,
        DC_UTILITY_OVERWATCH = 5,
        DC_UTILITY_PROPAGANDASTELE = 6,
        DC_UTILITY_RELAY = 7,
        DC_UTILITY_MONITORINGSTATION = 8,
        DC_UTILITY_FACTORCOLLECTSTATION = 9,
        DC_UTILITY_ISOLATIONSTRIP = 10,
        DC_UTILITY_FIRECHANNEL = 11,
        DC_REPOSITORY = 12,
        JC_INFRAREDCAMERA_BASICINFO = 13
    }

    /// <summary>
    /// 图层类型
    /// </summary>
    public enum LayerType { 
        乡镇驻地 = 0,
        村驻地= 1,
        加油站=2,
        责任路线=3,
        责任区=4,
        消防队伍=5,
        政府机关=6,
        资源=7,
        仓库=8,
        装备=9,
        营房=10,
        停车场=11,
        瞭望台=12,
        宣传碑牌=13,
        中继站=14,
        监测站=15,
        因子采集站=16,
        隔离带=17,
        防火通道=18
    }

}