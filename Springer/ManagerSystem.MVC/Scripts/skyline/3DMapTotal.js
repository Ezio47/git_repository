var sg = new Object();
sg.sgmap = 'sgmap';//div ID
//sg.flyURL = "http://36.7.68.79:9000/SkylineFly/index.FLY";//三维默认加载地址
sg.flyURL = "http://36.7.68.79:8020/SkylineFly/index.FLY";//沈晨机器fly文件
var adrHost = 'http://' + window.location.host;//"http://localhost:33844";
var iTime = null;
//===
var type = 0;
var realLocTempGroupID = null;
sg.isOnloadSuccse = false;
//var sgworld = null;
var playersName = [];
var playersInfo = {};
var pLayerTree = [];

var groupName = "标绘";
var groupID = null;
//var treeDate = [{ "id": "加油站", "text": "加油站", "checked": false },
//           { "id": "村驻地", "text": "村驻地", "checked": true },
//           { "id": "地级市驻地", "text": "地级市驻地", "checked": true },
//           { "id": "河流", "text": "河流", "checked": false },
//           //{ "id": "其它道路", "text": "其它道路", "checked": false },
//           { "id": "省道", "text": "省道", "checked": false },
//           { "id": "水系面", "text": "水系面", "checked": false },
//           { "id": "铁路", "text": "铁路", "checked": false },
//           { "id": "停车场", "text": "停车场", "checked": false },
//           { "id": "县道", "text": "县道", "checked": false },
//           { "id": "县界", "text": "县界", "checked": false },
//           { "id": "乡镇村道", "text": "乡镇村道", "checked": false },
//           { "id": "乡镇驻地", "text": "乡镇驻地", "checked": false },
//           { "id": "乡镇边界", "text": "乡镇边界", "checked": false },
//           { "id": "资源", "text": "资源", "checked": false },
//           { "id": "装备", "text": "装备", "checked": false },
//           { "id": "车辆", "text": "车辆", "checked": false },
//           { "id": "营房", "text": "营房", "checked": false },
//           { "id": "瞭望台", "text": "瞭望台", "checked": false },
//           { "id": "宣传碑牌", "text": "宣传碑牌", "checked": false },
//           { "id": "中继站", "text": "中继站", "checked": false },
//           { "id": "监测站", "text": "监测站", "checked": false },
//           { "id": "因子采集站", "text": "因子采集站", "checked": false },
//           { "id": "消防队伍", "text": "消防队伍", "checked": false },
//           { "id": "隔离带", "text": "隔离带", "checked": false },
//           { "id": "防火通道", "text": "防火通道", "checked": false },
//           { "id": "其他设施", "text": "其他设施", "checked": false },
//           { "id": "政府机关", "text": "政府机关", "checked": false },
//           { "id": "森林火险等级区划图", "text": "森林火险等级区划图", "checked": false },
//           { "id": "公益林", "text": "公益林", "checked": false }]
//;

var treeDate = [{
    'id': 0,
    'text': '图层控制',
    "checked": false,
    'children': [
           {
               "id": 1, "text": "点图层", "state": "closed", 'children': [
                            //{ "id": 111, "text": "地级市驻地", "checked": false },
                            { 'id': 112, 'text': '加油站', 'checked': false },
                            { "id": 113, "text": "村驻地", "checked": false },
                            { "id": 114, "text": "村标记", "checked": false },
                            { "id": 115, "text": "山标记", "checked": false },
                            //{ "id": 116, "text": "县驻地", "checked": false },
                            { "id": 117, "text": "乡镇驻地", "checked": false },
                            //{ "id": 118, "text": "装备", "checked": false },
                            //{ "id": "车辆", "text": "车辆", "checked": true },
                            { "id": 119, "text": "营房", "checked": false },
                            { "id": 120, "text": "仓库", "checked": false },
                            { "id": 121, "text": "瞭望台", "checked": false },
                            { "id": 122, "text": "宣传碑牌", "checked": false },
                            { "id": 123, "text": "中继站", "checked": false },
                            { "id": 124, "text": "监测站", "checked": false },
                            { "id": 125, "text": "因子采集站", "checked": false },
                            { "id": 126, "text": "消防队伍", "checked": false },
                            { "id": 127, "text": "其他设施", "checked": false },
                            { "id": 128, "text": "政府机关", "checked": false },
                            { "id": 129, "text": "停车场", "checked": false }]
           },
           {
               "id": 2, "text": "线图层", "state": "closed", 'children': [
                         //{ "id": 211, "text": "省道", "checked": false },
                         { "id": 212, "text": "河流", "checked": false },
                         //{ "id": 213, "text": "铁路", "checked": false },
                         //{ "id": 214, "text": "县道", "checked": false },
                         //{ "id": 215, "text": "县界", "checked": false },
                         //{ "id": 216, "text": "州界", "checked": false },
                         //{ "id": "越南", "text": "越南", "checked": true },
                         //{ "id": "其它道路", "text": "其它道路", "checked": false },
                         //{ "id": "周边市县", "text": "周边市县", "checked": true },
                         //{ "id": 217, "text": "乡镇边界", "checked": false },
                         //{ "id": 218, "text": "乡镇村道", "checked": false },
                         { "id": 219, "text": "隔离带", "checked": false },
                         { "id": 220, "text": "防火通道", "checked": false },
                         { "id": 221, "text": "责任路线", "checked": false }]
           },
            {
                "id": 3, "text": "面图层", "state": "closed", 'children': [
                         { "id": 311, "text": "责任区", "checked": false },
                         //{ "id": 312, "text": "水系面", "checked": false },
                         { "id": 313, "text": "林下烧除", "checked": false },
                         { "id": 314, "text": "资源", "checked": false },
                         //{ "id": 315, "text": "森林火险等级区划图", "checked": false },
                         { "id": 316, "text": "公益林", "checked": false }]
            }
    ] 
}];
//三维地图初始化函数
$(document).ready(function () {
    var sgworld = document.getElementById('SGWorld');
    if (sgworld == null) {
        CreateSGWord(sg.flyURL);
        //$('#typeid').combotree('loadData', treeDate);
    }
    //清除cookie
    if (document.cookie != '') {
        var arrCookie = document.cookie.split('; ');
        var arrLength = arrCookie.length;
        var expireDate = new Date();
        expireDate.setDate(expireDate.getDate() - 1);
        for (var i = 0; i < arrLength; i++) {
            var str = arrCookie[i].split('=')[0];
            document.cookie = str + '=' + ';expires=' + expireDate.toGMTString();
        }
    }
});
//页面初始化函数
function CreateSGWord(fly) {
    var sgmap = document.getElementById(sg.sgmap);
    var sgworld = document.getElementById('SGWorld');
    if (sgworld == null) {
        sgworld = document.createElement('object');
        sgworld.id = 'SGWorld';
        sgworld.name = 'SGWorld';
        sgworld.classid = 'CLSID:3a4f91b1-65a8-11d5-85c1-0001023952c1';
        //CLSID:3a4f9192-65a8-11d5-85c1-0001023952c1
        sgworld.style.display = "block";
        sgmap.appendChild(sgworld);
        sgworld.AttachEvent("OnLoadFinished", OnProjectLoadFinished);
        sgworld.Project.Open(fly);
    }
}
var LAYERID;
//三维地图加载结束
function OnProjectLoadFinished() {
    var sgworld = CreateSGObj();
    sgworld.AttachEvent("OnFrame", OnFrame);
    //groupID = sgworld.ProjectTree.CreateGroup(groupName, 0);
    if (sgworld.ProjectTree.FindItem("加油站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("加油站"), false);
    if (sgworld.ProjectTree.FindItem("村驻地") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("村驻地"), false);
    if (sgworld.ProjectTree.FindItem("河流") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("河流"), false);
    //if (sgworld.ProjectTree.FindItem("其它道路") != 0)
    //    sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("其它道路"), false);
    if (sgworld.ProjectTree.FindItem("省道") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("省道"), false);
    if (sgworld.ProjectTree.FindItem("水系面") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("水系面"), false);
    if (sgworld.ProjectTree.FindItem("铁路") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("铁路"), false);
    if (sgworld.ProjectTree.FindItem("停车场") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("停车场"), false);
    if (sgworld.ProjectTree.FindItem("县道") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("县道"), false);
    if (sgworld.ProjectTree.FindItem("乡镇村道") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("乡镇村道"), false);
    if (sgworld.ProjectTree.FindItem("乡镇边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("乡镇边界"), false);
    if (sgworld.ProjectTree.FindItem("越南") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("越南"), false);
    if (sgworld.ProjectTree.FindItem("周边市县") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("周边市县"), false);
    if (sgworld.ProjectTree.FindItem("资源") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("资源"), false);
    if (sgworld.ProjectTree.FindItem("装备") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("装备"), false);
    //if (sgworld.ProjectTree.FindItem("车辆") != 0)
    //    sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("车辆"), false);
    if (sgworld.ProjectTree.FindItem("营房") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("营房"), false);
    if (sgworld.ProjectTree.FindItem("仓库") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("仓库"), false);
    if (sgworld.ProjectTree.FindItem("瞭望台") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("瞭望台"), false);
    if (sgworld.ProjectTree.FindItem("宣传碑牌") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("宣传碑牌"), false);
    if (sgworld.ProjectTree.FindItem("中继站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("中继站"), false);
    if (sgworld.ProjectTree.FindItem("监测站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("监测站"), false);
    if (sgworld.ProjectTree.FindItem("因子采集站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("因子采集站"), false);
    if (sgworld.ProjectTree.FindItem("隔离带") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("隔离带"), false);
    if (sgworld.ProjectTree.FindItem("防火通道") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("防火通道"), false);
    if (sgworld.ProjectTree.FindItem("消防队伍") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("消防队伍"), false);
    if (sgworld.ProjectTree.FindItem("其他设施") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("其他设施"), false);
    if (sgworld.ProjectTree.FindItem("政府机关") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("政府机关"), false);
    if (sgworld.ProjectTree.FindItem("高速") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("高速"), false);
    if (sgworld.ProjectTree.FindItem("国道") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("国道"), false);
    if (sgworld.ProjectTree.FindItem("市区一级道路") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("市区一级道路"), false);
    if (sgworld.ProjectTree.FindItem("市区二级道路") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("市区二级道路"), false);
    if (sgworld.ProjectTree.FindItem("银行") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("银行"), false);
    if (sgworld.ProjectTree.FindItem("医院") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("医院"), false);
    if (sgworld.ProjectTree.FindItem("学校") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("学校"), false);
    if (sgworld.ProjectTree.FindItem("居民小区") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("居民小区"), false);
    if (sgworld.ProjectTree.FindItem("宾馆酒店") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("宾馆酒店"), false);
    //if (sgworld.ProjectTree.FindItem("火烧面") != 0)
    //    sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("火烧面"), false);
    if (sgworld.ProjectTree.FindItem("林下烧除") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("林下烧除"), false);
    if (sgworld.ProjectTree.FindItem("责任路线") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("责任路线"), false);
    if (sgworld.ProjectTree.FindItem("责任区") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("责任区"), false);
    if (sgworld.ProjectTree.FindItem("村标记") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("村标记"), false);
    if (sgworld.ProjectTree.FindItem("山标记") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("山标记"), false);
    if (sgworld.ProjectTree.FindItem("专业队伍") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("专业队伍"), false);
    if (sgworld.ProjectTree.FindItem("半专业队伍") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("半专业队伍"), false);
    if (sgworld.ProjectTree.FindItem("应急队伍") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("应急队伍"), false);
    if (sgworld.ProjectTree.FindItem("群众队伍") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("群众队伍"), false);
    if (sgworld.ProjectTree.FindItem("永久性宣传碑牌") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("永久性宣传碑牌"), false);
    if (sgworld.ProjectTree.FindItem("临时性宣传碑牌") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("临时性宣传碑牌"), false);
    if (sgworld.ProjectTree.FindItem("钢构营房") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("钢构营房"), false);
    if (sgworld.ProjectTree.FindItem("砖混营房") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("砖混营房"), false);
    if (sgworld.ProjectTree.FindItem("钢混营房") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("钢混营房"), false);
    if (sgworld.ProjectTree.FindItem("钢构瞭望台") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("钢构瞭望台"), false);
    if (sgworld.ProjectTree.FindItem("砖混瞭望台") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("砖混瞭望台"), false);
    if (sgworld.ProjectTree.FindItem("钢混瞭望台") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("钢混瞭望台"), false);
    if (sgworld.ProjectTree.FindItem("有线监测站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("有线监测站"), false);
    if (sgworld.ProjectTree.FindItem("无线监测站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("无线监测站"), false);
    if (sgworld.ProjectTree.FindItem("有线因子采集站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("有线因子采集站"), false);
    if (sgworld.ProjectTree.FindItem("无线因子采集站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("无线因子采集站"), false);
    if (sgworld.ProjectTree.FindItem("便道防火通道") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("便道防火通道"), false);
    if (sgworld.ProjectTree.FindItem("林区防火通道") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("林区防火通道"), false);
    if (sgworld.ProjectTree.FindItem("生物隔离带") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("生物隔离带"), false);
    if (sgworld.ProjectTree.FindItem("生土隔离带") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("生土隔离带"), false);
    if (sgworld.ProjectTree.FindItem("火烧线") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("火烧线"), false);
    if (sgworld.ProjectTree.FindItem("重点林区") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("重点林区"), false);
    if (sgworld.ProjectTree.FindItem("有林地") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("有林地"), false);
    if (sgworld.ProjectTree.FindItem("荒山") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("荒山"), false);
    if (sgworld.ProjectTree.FindItem("灌丛林") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("灌丛林"), false);
    if (sgworld.ProjectTree.FindItem("短波中继站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("短波中继站"), false);
    if (sgworld.ProjectTree.FindItem("超短波中继站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("超短波中继站"), false);
    if (sgworld.ProjectTree.FindItem("微波中继站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("微波中继站"), false);
    if (sgworld.ProjectTree.FindItem("规划生物隔离带") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("规划生物隔离带"), false);
    if (sgworld.ProjectTree.FindItem("规划生物隔离带") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("规划生物隔离带"), false);
    if (sgworld.ProjectTree.FindItem("屏边县生态修复") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("屏边县生态修复"), false);
    if (sgworld.ProjectTree.FindItem("个旧市营造林") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("个旧市营造林"), false);
    if (sgworld.ProjectTree.FindItem("河口市营造林") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("河口市营造林"), false);
    if (sgworld.ProjectTree.FindItem("河口市储备林") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("河口市储备林"), false);
    if (sgworld.ProjectTree.FindItem("河口市保育区") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("河口市保育区"), false);
    if (sgworld.ProjectTree.FindItem("河口市封山育林") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("河口市封山育林"), false);
    if (sgworld.ProjectTree.FindItem("蒙自市保育区") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("蒙自市保育区"), false);
    if (sgworld.ProjectTree.FindItem("电子监控") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("电子监控"), false);
    if (sgworld.ProjectTree.FindItem("红外相机") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("红外相机"), false);
    if (sgworld.ProjectTree.FindItem("火情档案") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("火情档案"), false);
    if (sgworld.ProjectTree.FindItem("森林火险等级区划图") != 0) {
        if (type == 0) {
            sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("森林火险等级区划图"), true);
        }
        else {
            sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("森林火险等级区划图"), false);
        }
    }
    if (sgworld.ProjectTree.FindItem("公益林") != 0) {
        if (type != 1) {
            sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("公益林"), false);
        }
    }
    //边界
    if (sgworld.ProjectTree.FindItem("个旧边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("个旧边界"), false);
    if (sgworld.ProjectTree.FindItem("蒙自边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("蒙自边界"), false);
    if (sgworld.ProjectTree.FindItem("金平边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("金平边界"), false);
    if (sgworld.ProjectTree.FindItem("屏边边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("屏边边界"), false);
    if (sgworld.ProjectTree.FindItem("石屏边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("石屏边界"), false);
    if (sgworld.ProjectTree.FindItem("河口边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("河口边界"), false);
    if (sgworld.ProjectTree.FindItem("弥勒边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("弥勒边界"), false);
    if (sgworld.ProjectTree.FindItem("红河边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("红河边界"), false);
    if (sgworld.ProjectTree.FindItem("元阳边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("元阳边界"), false);
    if (sgworld.ProjectTree.FindItem("建水边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("建水边界"), false);
    if (sgworld.ProjectTree.FindItem("泸西边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("泸西边界"), false);
    if (sgworld.ProjectTree.FindItem("开远边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("开远边界"), false);
    if (sgworld.ProjectTree.FindItem("绿春边界") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("绿春边界"), false);
    var AllNAME = $("#AllNAME").val();
    var Allarr = AllNAME.split(',');
    for (var i = 0; i < Allarr.length; i++) {
        if (sgworld.ProjectTree.FindItem(Allarr[i]) != 0)
            sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem(Allarr[i]), false);
    }

    var TreeData = $("#TreeData").val();
    if (sgworld.ProjectTree.FindItem(TreeData) != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem(TreeData), false);
    var LAYERNAME = $("#LAYERNAME").val();
    var DEFAULTCH = $("#DEFAULTCH").val();
    var arrayLAYERNAME = LAYERNAME.split(',');//ISDEFAULTCH
    var arrayDEFAULTCH = DEFAULTCH.split(',');
    var data = new Array();
    for (var i = 0; i < arrayLAYERNAME.length; i++) {
        data[i] = new Array(); //将每一个子元素又定义为数组
        data[i] = arrayLAYERNAME[i] + ":" + arrayDEFAULTCH[i];
    }
    for (var j = 0; j < data.length; j++) {
        var sgworld = new CreateSGObj();
        var tempdata = data[j].split(':');
        if (tempdata[1] == "1") {
            var name = tempdata[0];
            var ItemID = sgworld.ProjectTree.FindItem(name);
            if (ItemID != 0) {
                sgworld.ProjectTree.SetVisibility(ItemID, true);
            }
        }
    }
    //LAYERID = $("#LAYERID").val();
    var value = $('#lonlathide').val();
    if (value != "") {
        LocationOnMap(value);
    }
    var xcenter = $("#xcenter").val();
    var ycenter = $("#ycenter").val();
    var scale = $("#scale").val();
    if (xcenter == null && ycente == null) {
        //
    } else {
        movetoCountry1(xcenter, ycenter, scale)
    }
}

//根据用户所属单位定位地图展示地区
function LocationOnMap(value) {
    //检索点 
    if (value!=null) {
        var jd = value.split(',')[0];
        var wd = value.split(',')[1];
        var CountryDistance = value.split(',')[2];
        var CountryLine = value.split(',')[3];
        var CityValue = CountryLine + "边界";
        var sgworld = CreateSGObj();
        if (sgworld.ProjectTree.FindItem(CityValue) != 0)
            sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem(CityValue), true);
        movetoCountry(jd, wd, CountryDistance);
    } else {
        return false;
    }
    
}

//定位到x,y坐标
function movetoCountry(x, y, CountryDistance) {
    delRealLocTemp();
    var sgworld = CreateSGObj();
    if (x == null || x == '' || y == null || y == '') {
        alert("无坐标信息！");
        return false;
    }
    else {
        var pos = getPositionCountry(x, y, CountryDistance);
        sgworld.Navigate.FlyTo(pos);
    }
}
//二维转三维定位
function movetoCountry1(x, y, CountryDistance) {
    delRealLocTemp();
    var sgworld = CreateSGObj();
    if (x == null || x == '' || y == null || y == '') {
        //alert("无坐标信息！");
        return false;
    }
    else {
        var pos = getPositionCountry(x, y, CountryDistance);
        sgworld.Navigate.FlyTo(pos);
    }
}

//根据x,y获取三维位置
function getPositionCountry(x, y, CountryDistance) {
    var sgworld = CreateSGObj();
    var dXCoord = x;//经度
    var dYCoord = y;//纬度
    var dAltitude = 0.0;
    var eAltitudeTypeCode = 2; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90;
    var dRoll = 90;
    var dDistance = CountryDistance;
    return sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
}

//定位按钮start//
//取出最新经纬度定位（中间表--实时定位）
function getLonLat(uidstr) {
    if (iTime != null) {
        window.clearTimeout(iTime);
    }
    delRealLocTemp();
    var sgworld = CreateSGObj();
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetRealAjax",
        data: { uidstr: uidstr, maptype: "Skyline" },
        dataType: "json",
        success: function (data) {
            if (data != null) {
                var cVerticesArray = [];//存储点集合
                if (data.length > 0) {
                    var fontcolor = 0x0000ff00;//绿色
                    var url = "/Images/icon_zaixian.ico";
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].HSTATE == "0") {//0 表示离线 1 表示在线
                            fontcolor = 0x000000ff;//红字
                            // url = "/Images/icon_lixian.ico";
                            url = "/Images/lixian.png";
                        }
                        else if (data[i].ISOUTRAIL == "1") {//出围 0 表未出围 1 表示出围
                            fontcolor = 0x0020A5DA;//橙字#DAA520
                            // url = "/Images/icon_chuwei.ico";
                            url = "/Images/chuwei.png";
                        }
                        else if (data[i].HSTATE == "1") {
                            fontcolor = 0x0000ff00;//绿字
                            // url = "/Images/icon_zaixian.ico";
                            url = "/Images/zaixian.png";
                        }

                        //将坐标点循环放入数组，用于创建面图层
                        cVerticesArray.push(data[i].LONGITUDE);
                        cVerticesArray.push(data[i].LATITUDE);
                        cVerticesArray.push(0);

                        var hid = data[i].USERID;
                        var lable = realPositioning(parseFloat(data[i].LONGITUDE).toFixed(3), parseFloat(data[i].LATITUDE).toFixed(3), data[i].HNAME, url, fontcolor);
                        var message = sgworld.Creator.CreatePopupMessage("护林员：" + data[i].HNAME, adrHost + "/EmergencyHand/HuserInfoIndex?hid=" + hid, 0, 0, 600, 240, -1);
                        lable.Message.MessageID = message.ID;
                        if (data.length == 1) {
                            sgworld.Navigate.JumpTo(lable);
                        }
                    }
                    if (cVerticesArray.length > 3) {
                        cRing = sgworld.Creator.GeometryCreator.CreateLineStringGeometry(cVerticesArray);
                        var color = sgworld.Creator.CreateColor(255, 0, 0);
                        cPolyline = sgworld.Creator.CreatePolyline(cRing, color, 2, 0);
                        cPolyline.LineStyle.Pattern = lineType;
                        cPolyline.LineStyle.Width = 0;
                        cPolyline.Visibility.Show = false;
                        sgworld.Navigate.JumpTo(cPolyline);
                    }
                }
                else {
                    alert('暂无坐标信息，无法定位！');
                }
            }
            else {
                alert('暂无坐标信息，无法定位！');
            }
        }
    });
}

function getLonLatNoDel(uidstr) {
    if (iTime != null) {
        window.clearTimeout(iTime);
    }
    // delRealLocTemp();
    var sgworld = CreateSGObj();
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetRealAjax",
        data: { uidstr: uidstr, maptype: "Skyline" },
        dataType: "json",
        success: function (data) {
            if (data != null) {
                var cVerticesArray = [];//存储点集合
                if (data.length > 0) {
                    var fontcolor = 0x0000ff00;//绿色
                    var url = "/Images/icon_zaixian.ico";
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].HSTATE == "0") {//0 表示离线 1 表示在线
                            fontcolor = 0x000000ff;//红字
                            // url = "/Images/icon_lixian.ico";
                            url = "/Images/lixian.png";
                        }
                        else if (data[i].ISOUTRAIL == "1") {//出围 0 表未出围 1 表示出围
                            fontcolor = 0x0020A5DA;//橙字#DAA520
                            // url = "/Images/icon_chuwei.ico";
                            url = "/Images/chuwei.png";
                        }
                        else if (data[i].HSTATE == "1") {
                            fontcolor = 0x0000ff00;//绿字
                            // url = "/Images/icon_zaixian.ico";
                            url = "/Images/zaixian.png";
                        }

                        //将坐标点循环放入数组，用于创建面图层
                        cVerticesArray.push(data[i].LONGITUDE);
                        cVerticesArray.push(data[i].LATITUDE);
                        cVerticesArray.push(0);

                        var hid = data[i].USERID;
                        var lable = realPositioning(parseFloat(data[i].LONGITUDE).toFixed(3), parseFloat(data[i].LATITUDE).toFixed(3), data[i].HNAME, url, fontcolor);
                        var message = sgworld.Creator.CreatePopupMessage("护林员：" + data[i].HNAME, adrHost + "/EmergencyHand/HuserInfoIndex?hid=" + hid, 0, 0, 475, 175, -1);
                        lable.Message.MessageID = message.ID;
                        if (data.length == 1) {
                            sgworld.Navigate.JumpTo(lable);
                        }
                    }
                    if (cVerticesArray.length > 3) {
                        cRing = sgworld.Creator.GeometryCreator.CreateLineStringGeometry(cVerticesArray);
                        var color = sgworld.Creator.CreateColor(255, 0, 0);
                        cPolyline = sgworld.Creator.CreatePolyline(cRing, color, 2, 0);
                        cPolyline.LineStyle.Pattern = lineType;
                        cPolyline.LineStyle.Width = 0;
                        cPolyline.Visibility.Show = false;
                        sgworld.Navigate.JumpTo(cPolyline);
                    }
                }
                else {
                    alert('暂无坐标信息，无法定位！');
                }
            }
            else {
                alert('暂无坐标信息，无法定位！');
            }
        }
    });
}
//end

//根据x,y获取三维位置
function getPosition(x, y) {
    var sgworld = CreateSGObj();
    // alert("x===" + x + "===y====" + y);
    var dXCoord = x;//经度
    var dYCoord = y;//纬度
    var dAltitude = 0.0;
    var eAltitudeTypeCode = 0; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90.0;
    var dRoll = 0.0;
    var dDistance = 2000;
    return sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
}
//定位car到x,y坐标
function movetocar(x, y, imagename) {
    if (iTime != null) {
        window.clearTimeout(iTime);
    }
    delRealLocTemp();
    var sgworld = CreateSGObj();
    if (x == null || x == '') {
        alert("无坐标信息！");
    }
    else {
        var circle = createImageNameCircle(x, y, "", imagename);
        sgworld.Navigate.FlyTo(circle);
    }
}

//实时轨迹start//
var oldx, oldy, time;
var hisi = 0;
//实时轨迹
function RealLocation(phone, uid) {
    delRealLocTemp();
    var sgworld = CreateSGObj();
    clearIntervalFun();//清除定时事件
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetRealDataAjax",
        data: {
            phone: phone, time: '', uid: uid, maptype: "Skyline"
        },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalsit = obj.DataList;
                var i = datalsit.length - 1;
                var str = "时间：" + datalsit[i].SBTIME;
                var lable = createImageCircle(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);
                sgworld.Navigate.FlyTo(lable);
                time = datalsit[i].SBTIME;
                oldx = datalsit[i].LONGITUDE;
                oldy = datalsit[i].LATITUDE;
                iTime = self.setInterval("RealLocationInterval('" + phone + "','" + uid + "')", 5000);
            }
            else {
                alert('暂时没有实时轨迹数据！');
            }
        }
    });
}


//定时执行实时轨迹
function RealLocationInterval(phone, uid) {
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetRealDataAjax",
        data: {
            phone: phone, time: time, uid: uid, maptype: "Skyline"
        },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalsit = obj.DataList;
                //console.info(datalsit);
                if (datalsit.length > 0) {
                    for (var i = 0; i < datalsit.length; i++) {
                        var str = "时间：" + datalsit[i].SBTIME;
                        var lable = createImageCircle(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);
                        CreatePolyline(oldx, oldy, parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE));
                        oldx = parseFloat(datalsit[i].LONGITUDE);// 经度
                        oldy = parseFloat(datalsit[i].LATITUDE);//纬度
                        time = datalsit[i].SBTIME;
                        var sgworld = CreateSGObj();
                        sgworld.Navigate.FlyTo(lable);
                    }
                }
            }
        }
    });
}
//end

//历史轨迹start//
var hisi = 0;//历史轨迹索引
gogps = function () {
    go();
    if (hisi < datalist.length) {
        iTime = window.setTimeout("gogps()", 3000);
    } else {
        window.clearTimeout(iTime);
        hisi = 0;
        //datalist = null;
    }
}

//历史轨迹回放
var datalist;
function hisgjPlay(uid, time, i) {
    if (iTime != null) {
        window.clearTimeout(iTime);
    }
    delRealLocTemp();
    var starttime = $('#timepickerStart').val();//开始时分
    var endtime = $('#timepickerEnd').val();//结束时分
    if ($.trim(starttime) == "") {
        alert('开始时分不可为空！');
        $('#timepickerStart').focus();
        return false;
    }
    if ($.trim(endtime) == "") {
        alert('结束时分不可为空！');
        $('#timepickerEnd').focus();
        return false;
    }
    //播放显示
    $('[id^=divplay_]').css('display', 'none');
    $('#divplay_' + i).css('display', 'block');
    document.getElementById('playst_' + i).innerText = '暂停';

    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetHisLogLatAjax",
        data: { uid: uid, time: time, starttime: starttime, endtime: endtime, maptype: "Skyline" },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                // $('#pausebtn').attr("disabled", false);//暂停设置可用
                //map.graphics.clear();
                //graphicLayer.clear();
                datalist = obj.DataList;
                hisi = 0;
                gogps();
                // CollapseStatus = 0;//收缩
                //hideInfoWin();
            }
            else {
                alert('没有实时轨迹数据！');
            }
        }
    });
}

//暂停/开始
var ss = 0;
var bo = true;
function pauseAndStart(i) {
    //alert(bo);
    if (bo == true) {
        ss = hisi;
        hisi = 100000000;//设置最大值，停止循环
        bo = false;
        drawpausefun();//暂停
        document.getElementById('playst_' + i).innerText = '开始';
        // $('#overbtn').attr('disabled', true);//禁用
    }
    else {
        hisi = ss;
        bo = true;
        drawstartfun();//开始
        document.getElementById('playst_' + i).innerText = '暂停';
        //$('#overbtn').attr('disabled', false);//禁用
    }
}
//开始
var t;
function drawstartfun() {
    //console.info(hisi);
    go();
    if (hisi < datalist.length) {
        t = window.setTimeout("drawstartfun()", 3000);
    }
}
//暂停
function drawpausefun() {
    window.clearTimeout(t);

}

//结束
function drawOver() {
    if (datalist != null) {
        for (var i = hisi; i < datalist.length; i++) {
            go();
        }
    }
}

///Js 时间间隔计算(间隔小时)
function GetDateDiff(startDate, endDate) {
    var startTime = new Date(Date.parse(startDate.replace(/-/g, "/"))).getTime();
    var endTime = new Date(Date.parse(endDate.replace(/-/g, "/"))).getTime();
    var dates = Math.abs((startTime - endTime)) / (1000 * 60 * 60);
    return dates;
}
//历史轨迹
go = function () {
    var sgworld = CreateSGObj();
    var str = "";
    if (datalist != null) {
        if (hisi < datalist.length) {
            if (hisi == 0) {
                str = "时间：" + datalist[hisi].SBTIME;//+ "  电量:" + datalist[hisi].ELECTRIC;
            }
            else {
                str = datalist[hisi].SBTIME.substring(10, datalist[hisi].SBTIME.length);//+ "  电量:" + datalist[hisi].ELECTRIC;;
            }
            if (hisi == 0) {
                createImageCircleImage(parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE), str, "start");
            }

            if (hisi != 0) {

                var timespan = GetDateDiff(datalist[hisi - 1].SBTIME, datalist[hisi].SBTIME).toFixed(2);
                if (parseFloat(timespan) > 0.17) {//间隔时间超过10分钟
                    str += "（历时" + timespan + "时）";
                }
                if (hisi == datalist.length - 1) {
                    var lable = createImageCircleImage(parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE), str, "end");
                    CreatePolyline(parseFloat(datalist[hisi - 1].LONGITUDE), parseFloat(datalist[hisi - 1].LATITUDE), parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE));
                    sgworld.Navigate.FlyTo(lable);
                }
                else {
                    var lable = createImageCircle(parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE), str);
                    CreatePolyline(parseFloat(datalist[hisi - 1].LONGITUDE), parseFloat(datalist[hisi - 1].LATITUDE), parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE));
                    sgworld.Navigate.FlyTo(lable);
                }
            }
            hisi++;
        }
    }
}
//end//

//title:标题url
//url:地址
//x:弹出框位置x
//x:弹出框位置x
//width:宽度
//height:高度
function showPopuopByUrl(title, url, x, y, width, height) {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage(title, url, x, y, width, height);
    var popup = sgworld.Window.ShowPopup(Message);
}
//title:标题url
//url:地址
//x:弹出框位置x
//x:弹出框位置x
//width:宽度
//height:高度
function removePopuopByUrl(title, url, x, y, width, height) {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage(title, url, x, y, width, height);
    var popup = sgworld.Window.RemovePopup(Message);
}

//创建轨迹点//定位
function createImageLoc(x, y, lableText) {
    if (iTime != null) {
        window.clearTimeout(iTime);
    }
    var sgworld = CreateSGObj();
    var cLabelStyle = getHlyLableStyle();
    var pos = getPosition(x, y);
    var lable = sgworld.Creator.CreateLabel(pos, lableText, adrHost + "/Images/location.png", cLabelStyle, getRealLocTemp());
    lable.Position.AltitudeType = 2;
    sgworld.Navigate.FlyTo(lable);
    return lable;
}

//创建轨迹点//定位 可以转换图层
function createImageLocConvert(x, y, lableText, cid) {
    if (iTime != null) {
        window.clearTimeout(iTime);
    }
    var sgworld = CreateSGObj();
    var cLabelStyle = getHlyLableStyle();
    var pos = getPosition(x, y);
    var lable = sgworld.Creator.CreateLabel(pos, lableText, adrHost + "/Images/location.png", cLabelStyle, getRealLocTemp());
    lable.Position.AltitudeType = 2;
    var message = sgworld.Creator.CreatePopupMessage("查看", adrHost + "/SkylineManger/DataCollectDetailViewIndex?cid=" + cid, 0, 0, 550, 280, -1);
    lable.Message.MessageID = message.ID;
    sgworld.Navigate.FlyTo(lable);
    return lable;
}
var state = "";

var arrow = null;
var position1 = null; var x2 = null;
var position2 = null; var y2 = null;

var polygon = null;
var cVerticesArray = [];
var cRing = null;
var cPolygonGeometry = null;
var cPolygon = null;
var cPolyline = null;
var isFirst = false;

function OnFrame() {
    var sgworld = CreateSGObj();
    //画箭头
    var mouseInfo = sgworld.Window.GetMouseInfo();
    var cursorCoord1 = sgworld.Window.CenterPixelToWorld();
    var cursorCoord = sgworld.Window.PixelToWorld(mouseInfo.X, mouseInfo.Y);
    var currentPos = sgworld.Navigate.GetPosition(0);//获取高度
    if (state == "CreateCircle") {
        position1 = cursorCoord.Position;
        var distance = sgworld.CoordServices.GetDistance(position1.X, position1.Y, position2.X, position2.Y);
        Circle.Radius = distance;
    }
    if (state == "CreateRectangle") {
        position1 = cursorCoord.Position;
        var distance = sgworld.CoordServices.GetDistance(position1.X, position1.Y, position2.X, position2.Y);
        Rectangle.Width = distance;
        Rectangle.Depth = distance / 2;
    }
    if (state == "CreateArrow") {
        position1 = cursorCoord.Position;
        var distance = sgworld.CoordServices.GetDistance(position1.X, position1.Y, position2.X, position2.Y);
        arrow.TailX = position2.X;
        arrow.TailY = position2.Y;
        arrow.HeadX = position1.X;
        arrow.HeadY = position1.Y;
    }
    if (state == "CreatePolygon") {
        if (cVerticesArray.length > 3) {
            try {
                position1 = cursorCoord.Position;
                cVerticesArray.push(position1.X);
                cVerticesArray.push(position1.Y);
                cVerticesArray.push(position1.Distance);
                cRing = sgworld.Creator.GeometryCreator.CreateLinearRingGeometry(cVerticesArray);
                cPolygonGeometry = sgworld.Creator.GeometryCreator.CreatePolygonGeometry(cRing, null);
                cPolygon.Geometry = cPolygonGeometry;
                cVerticesArray.pop();
                cVerticesArray.pop();
                cVerticesArray.pop();
            } catch (e) {
                //
            } 
        }
    }
    if (state == "CreateLine") {
        if (isFirst == true) {
            try {
                position1 = cursorCoord.Position;
                cVerticesArray.push(position1.X);
                cVerticesArray.push(position1.Y);
                cVerticesArray.push(position1.Distance);
                cRing = sgworld.Creator.GeometryCreator.CreateLineStringGeometry(cVerticesArray);
                try {
                    cPolyline = sgworld.Creator.CreatePolyline(cRing, colorLine, 2, getRealLocTemp());
                } catch (e) {
                    delRealLocTemp();
                    cPolyline = sgworld.Creator.CreatePolyline(cRing, colorLine, 2, getRealLocTemp());
                }    
                cPolyline.LineStyle.Pattern = lineType;
                cPolyline.LineStyle.Width = widthLine;
                isFirst = false;
            } catch (e) {
                //
            }  
        }
        else {
            position1 = cursorCoord.Position;
            if (cVerticesArray[cVerticesArray.length - 1] != position1.Distance || cVerticesArray[cVerticesArray.length - 2] != position1.Y || cVerticesArray[cVerticesArray.length - 3] == position1.X) {
                cVerticesArray.push(position1.X);
                cVerticesArray.push(position1.Y);
                cVerticesArray.push(position1.Distance);
            }
            cRing = sgworld.Creator.GeometryCreator.CreateLineStringGeometry(cVerticesArray);
            cPolyline.Geometry = cRing;
        }
    }
    $("#CenterX").val(cursorCoord1.Position.X.toFixed(6));
    $("#CenterY").val(cursorCoord1.Position.Y.toFixed(6));
    $("#CenterZ").val(currentPos.Altitude.toFixed(2));
    $("#sbxx").html("经度：" + cursorCoord.Position.X.toFixed(6) + "/" + jwdtodfm(cursorCoord.Position.X.toFixed(6)) + "，纬度：" + cursorCoord.Position.Y.toFixed(6) + "/" + jwdtodfm(cursorCoord.Position.Y.toFixed(6)) + "，高程：" + cursorCoord.Position.Altitude.toFixed(2) + "米");

}
//应急处置========================
//定位
var globeX;
var globeY;
var imagesHuoInfo = null;
function moveto(x, y, title, jcid) {
    delRealLocTemp();
    var sgworld = CreateSGObj();
    jcfid = jcid;
    globeX = x;
    globeY = y;

    if (x == null || x == '') {
        alert("无坐标信息！");
        retun;
    }
    var dXCoord = x;
    var dYCoord = y;
    var dAltitude = 0.0;
    var eAltitudeTypeCode = 0; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90.0;
    var dRoll = 0.0;
    var dDistance = 5000;
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);

    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    cLabelStyle.TextAlignment = "Top";
    cLabelStyle.BackgroundColor.abgrColor = 0x000000FF;
    // var images = sgworld.Creator.CreateImageLabel(position, adrHost + "/Images/skyline/situation/5.png", cLabelStyle);
    //images.Tooltip.Text = "经度:" + x + ",纬度:" + y;
    imagesHuoInfo = null;
    if (imagesHuoInfo == null) {
        imagesHuoInfo = sgworld.Creator.CreateLabel(position, title, adrHost + "/Images/skyline/situation/5.png", cLabelStyle, getRealLocTemp());
        imagesHuoInfo.Position.AltitudeType = 2;
    }
    else {
        imagesHuoInfo.Position.X = x;
        imagesHuoInfo.Position.Y = y;
        imagesHuoInfo.Position.AltitudeType = 2;
    }
    imagesHuoInfo.Tooltip.Text = "经度:" + x + ",纬度:" + y;
    var message = sgworld.Creator.CreatePopupMessage("火情信息", adrHost + "/MapCommon/FireBasicInfo?jcfid=" + jcfid, 0, 0, 630, 270, -1);
    imagesHuoInfo.Message.MessageID = message.ID;
    sgworld.Navigate.FlyTo(imagesHuoInfo);
    var num = 0;
    //ajax 获取fly文件
    $.ajax({
        type: 'post',
        url: '/EmergencyHand/GetFlyFireList',
        data: {
            jcfid: jcfid
        },
        dataType: 'json',
        success: function (obj) {
            if (obj != null && obj.Success) {
                var html = "";
                var datalist = obj.DataList;
                for (var i = 0; i < datalist.length; i++) {
                    html += "<li class='' style=\"cursor: pointer;\"  onclick='onloadFlie(\"" + adrHost + "/UploadFile/FlaFile/" + datalist[i].PLOTTINGFILENAME + "\")'><span class='span2_02'></span>第"
                        + (++num) + "阶段：" + datalist[i].PLOTTINGTITLE + "<a class='cor_ff7 padd_10' onclick='removeFile(\"" + datalist[i].JC_FIRE_PLOTTINGID + "\")'>删除</a></li>";
                }
                html += "<li><span></span>当前火点总共有（" + num + "）个阶段标会" + "</li>";
                $('#flydiv').html(html);
            }
            else {
                alert("获取Fly文件出错");
            }
        }
    });
    // GetFeatureAll(x, y, 1);
    //AroundQuery();
}
//获取周边分析图层标绘临时组
function getRealLocTemp_Range() {
    var sgworld = CreateSGObj();
    if (realLocTempGroupID == null) {
        return realLocTempGroupID = sgworld.ProjectTree.CreateGroup("RealLocTemp_Range", 0);
    }
    else {
        return realLocTempGroupID;
    }
}

//清除周边分析图层标绘临时组
function delRealLocTemp_Range() {
    var sgworld = CreateSGObj();
    var id = sgworld.ProjectTree.FindItem("RealLocTemp_Range");
    sgworld.ProjectTree.DeleteItem(id);
    realLocTempGroupID = null;
}
//获取周边分析护林员标绘临时组
function getRealLocTemp_Range1() {
    var sgworld = CreateSGObj();
    if (realLocTempGroupID == null) {
        return realLocTempGroupID = sgworld.ProjectTree.CreateGroup("RealLocTemp_Range1", 0);
    }
    else {
        return realLocTempGroupID;
    }
}

//清除周边分析护林员标绘临时组
function delRealLocTemp_Range1() {
    var sgworld = CreateSGObj();
    var id = sgworld.ProjectTree.FindItem("RealLocTemp_Range1");
    sgworld.ProjectTree.DeleteItem(id);
    realLocTempGroupID = null;
}
//周边图层颜色
function getRangeVisibility(x, y, distance) {
    var sgworld = CreateSGObj();
    delRealLocTemp_Range();
    var dXCoord = x;
    var dYCoord = y;
    var dAltitude = 0.0;
    var eAltitudeTypeCode = 2; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90.0;
    var dRoll = 0.0;
    var dDistance = 5000;
    var color = sgworld.Creator.CreateColor(255, 0, 0);
    color.SetAlpha(0.5);
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
    var Circle = sgworld.Creator.CreateCircle(position, 90, color, color, getRealLocTemp_Range());
    Circle.Radius = distance*1000;
}
//周边护林员颜色
function getRangeVisibility1(x, y, distance) {
    var sgworld = CreateSGObj();
    delRealLocTemp_Range1();
    var dXCoord = x;
    var dYCoord = y;
    var dAltitude = 0.0;
    var eAltitudeTypeCode = 2; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90.0;
    var dRoll = 0.0;
    var dDistance = 5000;
    var color = sgworld.Creator.CreateColor(0, 255, 0);
    color.SetAlpha(0.5);
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
    var Circle = sgworld.Creator.CreateCircle(position, 90, color, color, getRealLocTemp_Range1());
    Circle.Radius = distance * 1000;
}
//周边分析
function AroundQuery(TuCengFlag) {
    LAYERID = $("#LAYERID").val();
    var sgworld = CreateSGObj();
    delRealLocTemp_Around();//清楚周边查询结果定位-除火点
    var flagstr;
    var jd = $('#arroundjd').val();
    var wd = $('#arroundwd').val();
    if (jd != "" && wd != "") {
        var values = $("#typeid").combotree("getValues");//图层选择
        var disval = $("#disInput").val();//周边距离
        getRangeVisibility(jd, wd, disval)
        if (disval != "") {
            if (values == "") {
                if (TuCengFlag == '') {
                    var arr = LAYERID.split(',');
                    flagstr = arr.join(',');
                } else {
                    var arr = TuCengFlag.split(',');
                    flagstr = arr.join(',');
                }
                
            } else {
                flagstr = values.join(',');
            }
            var gu = flagstr.split(',');
            for (var i in gu) {
                if (gu[i] == '114')
                    flagstr = flagstr +','+ '115';
            }
            var treeData = new Array();
            //加载层
            var index = layer.load(0, { offset: ['350px', '80px'], shade: 0.5 }); //0代表加载的风格，支持0-2
            $.ajax({
                type: "Post",
                url: "/EmergencyHand/GetAroundLayersInfo",
                data: { disval: disval, flagstr: flagstr, jd: jd, wd: wd },
                dataType: "json",
                success: function (data) {
                    layer.close(index);
                    if (data != null) {
                        if (data.length <= 0) {
                            alert("未查询到结果");
                        }
                        else {
                            // alert(datalist.length); 
                            for (var i = 0; i < data.length; i++) {
                                var childrenfather = new Object();
                                var childrenTreeData = new Array();
                                if (data[i].LayerName == null || data[i].LayerName == "") {
                                    continue;
                                }
                                var strtitle = data[i].LayerName + "(" + data[i].DataList.length + "条结果)";
                                // alert(strtitle);
                                childrenfather.text = strtitle;
                                //alert(strtitle);
                                if (data[i].DataList.length > 0) {
                                    var cVerticesArray = [];//存储点集合
                                    for (var j = 0; j < data[i].DataList.length; j++) {
                                        var datalist = data[i].DataList;
                                        var disValue = disval * 1000;
                                        var value = sgworld.CoordServices.GetDistance(parseFloat(jd), parseFloat(wd), parseFloat(datalist[j].Display_X), parseFloat(datalist[j].Display_Y));
                                        //if (value <= disValue) {
                                        var dis = value / 1000;
                                        var obj = new Object();
                                        obj.text = datalist[j].Name + "(" + dis.toFixed(2) + "千米)";
                                        obj.x = datalist[j].Display_X;
                                        obj.y = datalist[j].Display_Y;
                                        //将坐标点循环放入数组，用于创建面图层
                                        cVerticesArray.push(datalist[j].Display_X);
                                        cVerticesArray.push(datalist[j].Display_Y);
                                        cVerticesArray.push(0);
                                        var dbType = datalist[j].DBTYPE;
                                        var dbid = datalist[j].ID;
                                        var type = datalist[j].TYPE;
                                        var lnglatstrs = datalist[j].LNGLATSTRS;
                                        var flagLayer = datalist[j].Flag;
                                        var category = datalist[j].CATEGORY;
                                        obj.flag = true;
                                        //画坐标定位图片
                                        var ImagesPath = "/Images/skyline/hhz/";
                                        var imageLabel = creatImage(datalist[j].Display_X, datalist[j].Display_Y, ImagesPath + datalist[j].ImageUrl, datalist[j].Name);
                                        imageLabel.Tooltip.Text = datalist[j].Name;
                                        obj.imageName = datalist[j].Name;
                                        //定位图片点击弹出属性信息
                                        if (dbType != null && dbType != "") {
                                            var url = null;
                                            if (dbType != "100") {
                                                if (dbType == "101") {
                                                    url = adrHost + "/PEST/MonitoringStationDataSee?PEST_MONITORINGSTATIONID=" + dbid;
                                                } else {
                                                    url = adrHost + "/Mapcommon/PopViewIndex?dbid=" + dbid;
                                                    url = url + "&dbType=" + dbType;
                                                }
                                            }
                                            else if (dbType == "100") {
                                                url = adrHost + "/PublicForest/PopDetailIndex?objid=" + dbid;
                                            }
                                            var message = sgworld.Creator.CreatePopupMessage("【" + datalist[j].Name + "】信息：", url, 0, 0, 400, 370, -1);
                                            imageLabel.Message.MessageID = message.ID;
                                        }

                                        //解析线和面坐标字符串
                                        var str = lnglatstrs;
                                        var ar = [];
                                        while (str.length != 0) {
                                            var num = str.indexOf(")");//查找第一个出现)
                                            if (num == -1) {
                                                break;//如果找不到')'跳出
                                            }
                                            var strtemp = str.substring(0, num);//找到)之前的字符串
                                            var num1 = strtemp.lastIndexOf("(");//找到)之前的(
                                            if (num1 != num - 1)//如果（）没有任何值不查找
                                            {
                                                ar.push(strtemp.substring(num1 + 1, strtemp.length));//将（）里一组坐标放入数组存储
                                            }
                                            str = str.substring(num + 2, str.length);//获取)之后字符串继续循环
                                        }
                                        //循环数组画线和面
                                        for (var n = 0; n < ar.length; n++) {
                                            var array = [];//用于存储全局定位画线坐标点
                                            //解析每组坐标点，按照画线画面数组格式存入
                                            var strs = ar[n].split(',');
                                            for (var k = 0; k < strs.length; k++) {
                                                var strss = $.trim(strs[k]).split(' ');
                                                array.push(strss[0]);
                                                array.push(strss[1]);
                                                array.push(0);
                                            }
                                            if (type == "1")//线
                                            {
                                                //画线
                                                var color = sgworld.Creator.CreateColor(255, 0, 0);
                                                var lineStringGeometry = sgworld.Creator.GeometryCreator.CreateLineStringGeometry(array);
                                                var line = sgworld.Creator.CreatePolyline(lineStringGeometry, color, 2, getRealLocTemp_Around());
                                                line.LineStyle.Width = 10;
                                                line.LineStyle.Color.SetAlpha(0.3);
                                            }
                                            else if (type == "2") {
                                                //画面
                                                var color = sgworld.Creator.CreateColor(255, 140, 0);
                                                try {
                                                    var lineStringGeometry = sgworld.Creator.GeometryCreator.CreateLinearRingGeometry(array);
                                                    var polygonGeometry = sgworld.Creator.GeometryCreator.CreatePolygonGeometry(lineStringGeometry, null);
                                                    var cPolygon = sgworld.Creator.CreatePolygon(polygonGeometry, color, color, 2, getRealLocTemp_Around());
                                                } catch (e) {
                                                    //
                                                }
                                                cPolygon.FillStyle.Color.SetAlpha(0.2);
                                                cPolygon.LineStyle.Color.SetAlpha(0.3);
                                                cPolygon.Position.AltitudeType = 2;
                                                //查找面中心点，标上文字
                                                //var cLabelStyle = sgworld.Creator.CreateLabelStyle();
                                                //cLabelStyle.TextAlignment = "Top";
                                                //cLabelStyle.TextColor.abgrColor = 0x66FFFFFF;
                                                //var iPoint = polygonGeometry.Centroid;
                                                //var position = creatPosition(iPoint.X, iPoint.Y);
                                                //var imagesHuoInfo = sgworld.Creator.CreateLabel(position, datalist[j].Name, "", cLabelStyle, getRealLocTemp_Around());
                                            }
                                        }
                                        childrenTreeData.push(obj);
                                    }
                                    //火情坐标点
                                    cVerticesArray.push(jd);
                                    cVerticesArray.push(wd);
                                    cVerticesArray.push(0);
                                    //将所有坐标点画一个隐藏的线，然后定位到这个线，用于全局显示
                                    if (cVerticesArray.length > 3) {
                                        cRing = sgworld.Creator.GeometryCreator.CreateLineStringGeometry(cVerticesArray);
                                        var color = sgworld.Creator.CreateColor(255, 0, 0);
                                        cPolyline = sgworld.Creator.CreatePolyline(cRing, color, 2, 0);
                                        cPolyline.LineStyle.Pattern = lineType;
                                        cPolyline.LineStyle.Width = 0;
                                        cPolyline.Visibility.Show = false;
                                        sgworld.Navigate.JumpTo(cPolyline);
                                    }
                                }
                                //alert(treeData);
                                childrenfather.children = childrenTreeData;
                                treeData.push(childrenfather);
                                //console.info(treeData);
                            }
                        }
                    }
                    $('#tt').tree({
                        data: treeData,
                        onClick: function (node) {
                            if (node.flag == true) {
                                //点击定位并且标记文字
                                var cLabelStyle = sgworld.Creator.CreateLabelStyle();
                                cLabelStyle.TextAlignment = "Top";
                                cLabelStyle.TextColor.abgrColor = 0x66FFFFFF;
                                var position = creatPosition(node.x, node.y);
                                var tempLable = sgworld.Creator.CreateTextLabel(position, "", cLabelStyle, getRealLocTemp_Around());//node.text
                                tempLable.Position.AltitudeType = 2;
                                //var imageLable = sgworld.ProjectTree.FindItem("RealLocTemp_Around\\" + node.imageName);
                                sgworld.Navigate.FlyTo(tempLable);
                            }
                        }
                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.close(index);
                    alert(textStatus + "超时");
                }
            });
        }
        else {
            if (values == "") {
                alert("请选择周边分析图层。");
            }
            else if (disval == "") {
                alert("请输入周边距离。");
            }
            $('#tt').empty();

        }
    }
    else {
        alert("周边距离分析无定点经纬度。");
    }
}

function GetFeatureAll(x, y, changeFlag) {
    var sgworld = CreateSGObj();
    var layers = $("#typeid").combotree("getValues");

    var treeData = new Array();
    for (var tt = 0 ; tt < layers.length; tt++) {
        var ItemID = sgworld.ProjectTree.FindItem(layers[tt]);
        var obj = sgworld.ProjectTree.GetLayer(ItemID);
        var pIFeatureGroup = obj.FeatureGroups(0);

        var childrenfather = new Object();

        var childrenTreeData = new Array();


        for (var i = 0; i < pIFeatureGroup.Count; i++) {
            var x1, y1, name;
            var pIFeature = pIFeatureGroup.Item(i);
            for (var j = 0; j < pIFeature.FeatureAttributes.Count; j++) {
                var pIFeatureAttribute = pIFeature.FeatureAttributes.Item(j);
                if (pIFeatureAttribute.Name.toUpperCase() == "DISPLAY_X") {
                    x1 = pIFeatureAttribute.Value;
                }
                if (pIFeatureAttribute.Name.toUpperCase() == "DISPLAY_Y") {
                    y1 = pIFeatureAttribute.Value;
                }
                if (pIFeatureAttribute.Name.toUpperCase() == "NAME") {
                    name = pIFeatureAttribute.Value;
                }

            }
            var disVal = $("#disInput").val() * 1000;

            if (sgworld.CoordServices.GetDistance(x, y, x1, y1) < disVal) {
                var dis = sgworld.CoordServices.GetDistance(x, y, x1, y1) / 1000;
                var obj = new Object();
                obj.text = name + "(" + dis.toFixed(2) + "千米)";
                obj.x = x1;
                obj.y = y1;
                obj.flag = true;
                childrenTreeData.push(obj);
            }
        }

        childrenfather.text = layers[tt] + "(" + childrenTreeData.length + "条结果)";

        childrenfather.children = childrenTreeData;
        treeData.push(childrenfather);
    }


    $('#tt').tree({
        data: treeData,
        onClick: function (node) {
            if (node.flag == true)
                moveto1(node.x, node.y, node.text);
        }
    });
    if (changeFlag == true)
        //changeTab1(4);
        menuOclick('8');
}

function moveto1(x, y, title) {
    //delRealLocTemp();
    var sgworld = CreateSGObj();
    if (x == null || x == '') {
        alert("无坐标信息！");
        retun;
    }
    var dXCoord = x;
    var dYCoord = y;
    var dAltitude = 0;
    var eAltitudeTypeCode = 2; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90.0;
    var dRoll = 0.0;
    var dDistance = 5000;

    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    cLabelStyle.TextAlignment = "Top";
    cLabelStyle.TextColor.abgrColor = 0x66FF0000;
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
    var imagesHuoInfo = sgworld.Creator.CreateLabel(position, title, adrHost + "/Images/location.png", cLabelStyle, getRealLocTemp());
    imagesHuoInfo.Position.AltitudeType = 2;
    sgworld.Navigate.FlyTo(imagesHuoInfo);
}
//火情属性预案
function DangYAShow(title, Src, w, h) {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage(title, Src, 0, 0, w, h);
    var popup = sgworld.Window.ShowPopup(Message);
}

function qrueyHTML_dw() {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('坐标定位', adrHost + "/EmergencyHand/Nr", 0, 0, 298, 133);
    var popup = sgworld.Window.ShowPopup(Message);
}
function qrueyHTML_qr() {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('查询', adrHost + "/EmergencyHand/Qurey", 0, 0, 350, 400);
    var popup = sgworld.Window.ShowPopup(Message);
}
function qrueyHTML_tckz() {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('图层控制', adrHost + "/EmergencyHand/Tckz", 0, 0, 250, 338);
    var popup = sgworld.Window.ShowPopup(Message);
}
function qrueyHTML_draw() {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('绘图', adrHost + "/EmergencyHand/Draw", 0, 0, 123, 290);
    var popup = sgworld.Window.ShowPopup(Message);
}
function qrueyHTML_Image() {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('出图', adrHost + "/EmergencyHand/GenerateImages", 0, 0, 1000, 450);
    var popup = sgworld.Window.ShowPopup(Message);
}
//电量查询
function qrueyHTML_power(uid) {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('电量查询', adrHost + "/EmergencyHand/Power?uid=" + uid , 120, 190, 700, 230);
    var popup = sgworld.Window.ShowPopup(Message);
}
//火险等级查看
function qrueyHTML_PopFireLevel() {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('火险等级查看', adrHost + "/BaseCommon/FireLevelIQuery", 0, 0, 700, 400);
    var popup = sgworld.Window.ShowPopup(Message);
}
//短信响应
function qrueyHTML_msgShow() {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('响应措施', adrHost + "/BaseCommon/MessageSendIndex", -100, -100, 1000, 520);
    var popup = sgworld.Window.ShowPopup(Message);
}
//预警响应
function qrueyHTML_PopYjXy() {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('预警响应', adrHost + "/BaseCommon/PopYjxyIndex", -200, -110, 1100, 540);
    var popup = sgworld.Window.ShowPopup(Message);
}
//护林员在线，离线，出围弹出界面
function qrueyHTML_showHlyPerson(obj, titlestr) {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage(titlestr, adrHost + '/MainDefault/HlyOnLineIndex?obj=' + obj, 0, 0, 800, 400);
    var popup = sgworld.Window.ShowPopup(Message);
}
//用户在线、不在线情况
function qrueyHTML_showPerson(obj, str) {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage(str, adrHost + '/MainDefault/OnLineIndex?obj=' + obj, 0, 0, 800, 400);
    var popup = sgworld.Window.ShowPopup(Message);
}
//图层定位根据名称查询图层
function qrueyByName11() {
    var name = $("#name").val();
    if ($.trim(name) == "") {
        alert("请输入名称");
        $('#name').focus()
        return false;
    }
    GetFeatureAllBYName($.trim(name));
}

//获取图层
function GetFeatureAllBYName(str) {
    var sgworld = CreateSGObj();
    var strResult = "";
    var arr = [];
    var layers = null;
    layers = $("#cc").combobox("getValues");
    if (layers.length == 0) {
        var tt = $("#cc").combobox("getData");
        var arrvalue = [];
        for (var i = 0; i < tt.length; i++) {
            arrvalue.push(tt[i].value);
        }
        layers = arrvalue;
    }
    for (var tt = 0 ; tt < layers.length; tt++) {
        var ItemID = sgworld.ProjectTree.FindItem(layers[tt]);
        var obj = sgworld.ProjectTree.GetLayer(ItemID);
        var pIFeatureGroup = obj.FeatureGroups(0);
        for (var i = 0; i < pIFeatureGroup.Count; i++) {
            var x1, y1, name;
            var pIFeature = pIFeatureGroup.Item(i);
            for (var j = 0; j < pIFeature.FeatureAttributes.Count; j++) {
                var pIFeatureAttribute = pIFeature.FeatureAttributes.Item(j);
                if (pIFeatureAttribute.Name.toUpperCase() == "DISPLAY_X") {
                    x1 = pIFeatureAttribute.Value;
                }
                if (pIFeatureAttribute.Name.toUpperCase() == "DISPLAY_Y") {
                    y1 = pIFeatureAttribute.Value;
                }
                if (pIFeatureAttribute.Name.toUpperCase() == "NAME") {
                    name = pIFeatureAttribute.Value;
                }

            }
            if (name.indexOf(str) > -1 || name == str || str == '') {
                var obj = new Object();
                obj.code = name;
                obj.x = x1;
                obj.y = y1;
                obj.layerName = layers[tt];
                arr.push(obj);
            }
        }
    }
    if (arr.length == 0) {
        alert("没有查询到相关信息,请重新查询！")
    }
    $("#dg").datagrid("loadData", arr);
}
//查询结果输出操作按钮
function formatOper(val, row, index) {
    return '<a href="javascript:void(0)" class="easyui-linkbutton" onclick="dw(' + index + ')">定位</a>';
}
function dw(index) {
    $('#dg').datagrid('selectRow', index);// 关键在这里  
    var row = $('#dg').datagrid('getSelected');
    moveto(row.x, row.y);
}

///地图定位-画点
function movetoMap(x, y, title, jcfid) {
    delRealLocTemp();
    var sgworld = CreateSGObj();
    if (x == null || x == '') {
        alert("无坐标信息！");
        retun;
    }
    if (y == null || y == '') {
        alert("无坐标信息！");
        retun;
    }
    var dXCoord = x;
    var dYCoord = y;
    var dAltitude = 0;
    var eAltitudeTypeCode = 2; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90.0;
    var dRoll = 0.0;
    var dDistance = 5000;
    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    cLabelStyle.TextAlignment = "Top";
    cLabelStyle.BackgroundColor.abgrColor = 0x000000FF;
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
    var imagesHuoInfo = sgworld.Creator.CreateLabel(position, title, adrHost + "/Images/skyline/situation/5.png", cLabelStyle, getRealLocTemp());
    imagesHuoInfo.Position.AltitudeType = 2;
    imagesHuoInfo.Tooltip.Text = "经度:" + x + ",纬度:" + y;
    var message = sgworld.Creator.CreatePopupMessage("火情信息", adrHost + "/MapCommon/FireBasicInfo?jcfid=" + jcfid, 0, 0, 650, 270, -1);
    imagesHuoInfo.Message.MessageID = message.ID;
    sgworld.Navigate.JumpTo(imagesHuoInfo);
}
//画面--林火蔓延
function createPolygon_hqmy(cVerticesArray) {
    var sgworld = CreateSGObj();
    // delRealLocTemp_hqmy(); 
    delRealLocTemp();
    var arr = [];
    for (var i = 0 ; i < cVerticesArray.length; i++) {
        var dXCoord = cVerticesArray[i].JD;
        var dYCoord = cVerticesArray[i].WD;
        var dAltitude = 0.0;
        var eAltitudeTypeCode = 0; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
        var dYaw = 0.0;
        var dPitch = -90.0;
        var dRoll = 0.0;
        var dDistance = 100;
        var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
        arr.push(position);
    }
    var cPolygonGeometry1 = sgworld.Creator.GeometryCreator.CreatePolygonGeometry(arr, null);
    var color = sgworld.Creator.CreateColor(255, 0, 0);
    color.SetAlpha(0.5);
    var cPolygon1 = sgworld.Creator.CreatePolygon(cPolygonGeometry1, color, color, 2, getRealLocTemp());
    cPolygon1.Position.AltitudeType = 2;
    sgworld.Navigate.FlyTo(cPolygon1);

}

//===========标绘==========
//画圆
var Circle = null;
var position1 = null;
var position2 = null;
var state = "";
function d_CreateCircle(Style) {
    var sgworld = new CreateSGObj();
    d_CreateArrow_Style = Style;
    sgworld.Window.SetInputMode(1);
    sgworld.AttachEvent("OnLButtonDown", d_CreateCircle_OnLButtonDown);
    sgworld.AttachEvent("OnLButtonUp", d_CreateCircle_OnLButtonUp);
}

function d_CreateCircle_OnLButtonDown() {
    var sgworld = new CreateSGObj();
    state = "CreateCircle";
    var mouseInfo = sgworld.Window.GetMouseInfo();
    var cursorCoord = sgworld.Window.PixelToWorld(mouseInfo.X, mouseInfo.Y);
    position2 = cursorCoord.Position;
    var color = sgworld.Creator.CreateColor(255, 0, 0);
    color.SetAlpha(0.5);
    try {
        Circle = sgworld.Creator.CreateCircle(position2, 90, color, color, getRealLocTemp());
    } catch (e) {
        delRealLocTemp();
        Circle = sgworld.Creator.CreateCircle(position2, 90, color, color, getRealLocTemp());
    }
   
    Circle.Position.AltitudeType = 2;
    sgworld.DetachEvent("OnLButtonDown", d_CreateCircle_OnLButtonDown);
}
function d_CreateCircle_OnLButtonUp() {
    var sgworld = new CreateSGObj();
    state = "";
    sgworld.Window.SetInputMode(0);
    sgworld.DetachEvent("OnLButtonUp", d_CreateCircle_OnLButtonUp);
}
//画矩形
var Rectangle = null;
var position1 = null;
var position2 = null;
var state = "";
function d_CreateRectangle(Style) {
    var sgworld = new CreateSGObj();
    d_CreateArrow_Style = Style;
    sgworld.Window.SetInputMode(1);
    sgworld.AttachEvent("OnLButtonDown", d_CreateRectangle_OnLButtonDown);
    sgworld.AttachEvent("OnLButtonUp", d_CreateRectangle_OnLButtonUp);
}

function d_CreateRectangle_OnLButtonDown() {
    var sgworld = new CreateSGObj();
    state = "CreateRectangle";
    var mouseInfo = sgworld.Window.GetMouseInfo();
    var cursorCoord = sgworld.Window.PixelToWorld(mouseInfo.X, mouseInfo.Y);
    position2 = cursorCoord.Position;
    var color = sgworld.Creator.CreateColor(255, 0, 0);
    color.SetAlpha(0.5);
    try {
        Rectangle = sgworld.Creator.CreateRectangle(position2, 600, 800, color, color, getRealLocTemp());
    } catch (e) {
        delRealLocTemp();
        Rectangle = sgworld.Creator.CreateRectangle(position2, 600, 800, color, color, getRealLocTemp());
    }
    Rectangle.Position.AltitudeType = 2;
    sgworld.DetachEvent("OnLButtonDown", d_CreateRectangle_OnLButtonDown);
}

function d_CreateRectangle_OnLButtonUp() {
    var sgworld = new CreateSGObj();
    state = "";
    sgworld.Window.SetInputMode(0);
    sgworld.DetachEvent("OnLButtonUp", d_CreateRectangle_OnLButtonUp);
}
//画线
var lineType = 0xFFFFFFFF;
var colorLine = "0xFF0000";
var widthLine = 2;
function d_CreateLine(_lineType, _colorLine, _widthLine) {
    var sgworld = CreateSGObj();
    colorLine = _colorLine;
    lineType = _lineType;
    widthLine = _widthLine;
    //clearEvent();
    cVerticesArray = [];
    sgworld.Window.SetInputMode(1);
    // sgworld.Window.SetAutoIntensity
    //AltitudeType

    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_d_CreateLine);
    sgworld.AttachEvent("OnLButtonUp", OnLButtonUp_d_CreateLine);

}

function OnLButtonDown_d_CreateLine(Flags, X, Y) {
    var sgworld = CreateSGObj();
    isFirst = true;
    state = "CreateLine";
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_d_CreateLine);
}
function OnLButtonUp_d_CreateLine(Flags, X, Y) {
    var sgworld = CreateSGObj();
    state = "";
    sgworld.Window.SetInputMode(0);
    sgworld.DetachEvent("OnLButtonUp", OnLButtonUp_d_CreateLine);
    var lenth = 0;
    for (var i = 6; i < cVerticesArray.length; i++) {
        lenth = lenth + sgworld.CoordServices.GetDistance(cVerticesArray[i - 6], cVerticesArray[i - 5], cVerticesArray[i - 3], cVerticesArray[i - 2]);
    }
    try {
        cPolyline.Tooltip.Text = "线的长度：" + lenth.toFixed(2) + "米";
    } catch (e) {
        //
    }
    // $("#xx").html("线的长度：" + lenth.toFixed(2) + "米");
}
//画箭头_start
var d_CreateArrowStylte = 1;
function d_CreateArrow(style) {
    d_CreateArrowStylte = style;
    //clearEvent();
    var sgworld = CreateSGObj();
    sgworld.Window.SetInputMode(1);
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_d_CreateArrow);
    sgworld.AttachEvent("OnLButtonUp", OnLButtonUp_d_CreateArrow);
}
function OnLButtonDown_d_CreateArrow(Flags, X, Y) {
    var sgworld = CreateSGObj();
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);
    position1 = cursorCoord.Position;
    position2 = cursorCoord.Position;
    var color = sgworld.Creator.CreateColor(255, 0, 0);
    color.SetAlpha(0.5);
    try {
        arrow = sgworld.Creator.CreateArrow(position2, 0, d_CreateArrowStylte, color, color, getRealLocTemp());
    } catch (e) {
        delRealLocTemp();
        arrow = sgworld.Creator.CreateArrow(position2, 0, d_CreateArrowStylte, color, color, getRealLocTemp());
    }
    
    arrow.Position.AltitudeType = 2;
    state = "CreateArrow";
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_d_CreateArrow);
}
function OnLButtonUp_d_CreateArrow(Flags, X, Y) {
    state = "";
    var sgworld = CreateSGObj();
    sgworld.Window.SetInputMode(0);
    sgworld.DetachEvent("OnLButtonUp", OnLButtonUp_d_CreateArrow);
}
//画面

function d_CreatePolygon() {
    //clearEvent();
    var sgworld = CreateSGObj();
    sgworld.Window.SetInputMode(1);
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_d_CreatePolygon);
    sgworld.AttachEvent("OnRButtonDown", OnRButtonDown_d_CreatePolygon);
    cVerticesArray = [];
    cRing = sgworld.Creator.GeometryCreator.CreateLinearRingGeometry(cVerticesArray);
    cPolygonGeometry = sgworld.Creator.GeometryCreator.CreatePolygonGeometry(cRing, null);
    var color = sgworld.Creator.CreateColor(255, 0, 0);
    color.SetAlpha(0.5);
    try {
        cPolygon = sgworld.Creator.CreatePolygon(cPolygonGeometry, color, color, 2, getRealLocTemp());
    } catch (e) {
        delRealLocTemp();
        cPolygon = sgworld.Creator.CreatePolygon(cPolygonGeometry, color, color, 2, getRealLocTemp());
    }
    cPolygon.Position.AltitudeType = 2;
}
function OnLButtonDown_d_CreatePolygon(Flags, X, Y) {
    state = "CreatePolygon";
    var sgworld = CreateSGObj();
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);
    position1 = cursorCoord.Position;
    cVerticesArray.push(position1.X);
    cVerticesArray.push(position1.Y);
    cVerticesArray.push(position1.Distance);
    try {
        cRing = sgworld.Creator.GeometryCreator.CreateLinearRingGeometry(cVerticesArray);
        cPolygonGeometry = sgworld.Creator.GeometryCreator.CreatePolygonGeometry(cRing, null);
        cPolygon.Geometry = cPolygonGeometry;
    } catch (e) {
        //
    }
}
function OnRButtonDown_d_CreatePolygon(Flags, X, Y) {
    var sgworld = CreateSGObj();
    sgworld.Window.SetInputMode(0);
    state = "";
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_d_CreatePolygon);
    sgworld.DetachEvent("OnRButtonDown", OnRButtonDown_d_CreatePolygon);
    var nameid = sgworld.ProjectTree.FindItem(cPolygon.TreeItem.Name);
    // alert(nameid);
    //sgworld.ProjectTree.EditItem(nameid, 2);
}
function clearEvent() {
    var sgworld = CreateSGObj();
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_d_CreatePolygon);
    sgworld.DetachEvent("OnRButtonDown", OnRButtonDown_d_CreatePolygon);
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_d_CreateArrow);
    sgworld.DetachEvent("OnLButtonUp", OnLButtonUp_d_CreateArrow);
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_d_CreateLine);
    sgworld.DetachEvent("OnLButtonUp", OnLButtonUp_d_CreateLine);
}

var editState = 0;
function selectMove(state) {
    editState = state;
    var sgworld = CreateSGObj();
    sgworld.Window.SetInputMode(1);
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_d_selectMove);
    sgworld.AttachEvent("OnRButtonDown", OnRButtonDown_d_selectMove);
}


function OnLButtonDown_d_selectMove(Flags, X, Y) {
    var sgworld = CreateSGObj();
    sgworld.Window.SetInputMode(0);
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_d_selectMove);
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);
    var objID = cursorCoord.ObjectID;
    var type = cursorCoord.Type;
    var obj = sgworld.Creator.GetObject(objID);
    var itemID = sgworld.ProjectTree.FindItem("标绘\\" + obj.TreeItem.Name);
    sgworld.ProjectTree.EditItem(itemID, editState);
}
function OnRButtonDown_d_selectMove(Flags, X, Y) {
    var sgworld = CreateSGObj();
    sgworld.DetachEvent("OnRButtonDown", OnRButtonDown_d_selectMove);
    sgworld.ProjectTree.EndEdit();
}
function moveFree() {
    var sgworld = CreateSGObj();
    sgworld.Window.SetInputMode(0);
}
function excCommand(m, n) {
    var sgworld = CreateSGObj();
    sgworld.Command.Execute(m, n);
}

function d_CreateEffect() {
    // sgworld.AttachEvent("OnLButtonDown", OnLButtonDownEffect);
    var sgworld = CreateSGObj();
    var itemID = sgworld.ProjectTree.FindItem("Bonfire ##3859");

    var objID = sgworld.ProjectTree.GetTerraObjectID(itemID);
    //sgworld.Navigate.FlyTo(objID);
    var obj = sgworld.Creator.GetObject(objID);
}
function OnLButtonDownEffect(Flags, X, Y) {
    // sgworld.DetachEvent("OnLButtonDown", OnLButtonDownEffect);

}

//火（已灭未灭火标绘）
function createImagesHUOLayer() {
    layer.open({
        type: 1,
        skin: 'layui-layer-rim', //加上边框
        offset: ['380px', '10px'],
        title: false,
        area: ['200px', '70px'], //宽高
        content: "<div>经度:<input id='JD'width='120'/><br/>纬度:<input id='WD' width='120'/><br/><input type='button' width='80' onclick='HDSB()' value='标绘' /></div>"
    });

}

var lableText;
function createTextLable(text) {
    lableText = text;
    var sgworld = CreateSGObj();
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_createLable);

}
function OnLButtonDown_createLable(Flags, X, Y) {
    var sgworld = CreateSGObj();
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_createLable);
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);   //把屏幕坐标转化为地理坐标
    var position = cursorCoord.Position;
    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    try {
        var images = sgworld.Creator.CreateTextLabel(position, lableText, cLabelStyle, getRealLocTemp());
    } catch (e) {
        delRealLocTemp();
        var images = sgworld.Creator.CreateTextLabel(position, lableText, cLabelStyle, getRealLocTemp());
    }
}

var _imagesUrl = null;
function createImagesHUO1(imagesUrl) {
    var sgworld = CreateSGObj();
    _imagesUrl = imagesUrl;
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_createImages1);

}

function OnLButtonDown_createImages1(Flags, X, Y) {
    var sgworld = CreateSGObj();
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);   //把屏幕坐标转化为地理坐标
    var position = cursorCoord.Position;
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_createImages1);
    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    try {
        var images = sgworld.Creator.CreateImageLabel(position, adrHost + "/Images/" + _imagesUrl, cLabelStyle, getRealLocTemp());
    } catch (e) {
        delRealLocTemp();
        var images = sgworld.Creator.CreateImageLabel(position, adrHost + "/Images/" + _imagesUrl, cLabelStyle, getRealLocTemp());
    }

}
function createImagesHUO(imagesUrl) {
    var sgworld = CreateSGObj();
    _imagesUrl = imagesUrl;
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_createImages);

}

function OnLButtonDown_createImages(Flags, X, Y) {
    var sgworld = CreateSGObj();
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);   //把屏幕坐标转化为地理坐标
    var position = cursorCoord.Position;
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_createImages);
    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    try {
        var images = sgworld.Creator.CreateImageLabel(position, adrHost + "/Images/skyline/" + _imagesUrl, cLabelStyle, getRealLocTemp());
    } catch (e) {
        delRealLocTemp();
        var images = sgworld.Creator.CreateImageLabel(position, adrHost + "/Images/skyline/" + _imagesUrl, cLabelStyle, getRealLocTemp());
    }
   
}

//=====标绘保存=======
var jcfid = null;

function flySaveAs() {
    var sgworld = CreateSGObj();
    var myDate = new Date();
    var mytime = myDate.getFullYear().toString() + (myDate.getMonth() + 1).toString() + myDate.getDate().toString() + myDate.getHours().toString() + myDate.getMinutes().toString() + myDate.getSeconds().toString();
    //var fullPath = sgworld.Project.SaveAs(mytime);
    //var fullPath = sgworld.ProjectTree.SaveAsFly(mytime, getRealLocTemp());
    var fullPath;
    try {
        fullPath = sgworld.ProjectTree.SaveAsFly(mytime, getRealLocTemp());
        //alert("The polygons were successfully saved to: " + fullPath);
    }
    catch (e) {
        alert("Error: 保存出错\r\nDescription:" + e.Description);
    }
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");
    var returnValue = window.showModalDialog("/EmergencyHand/UploadFlyIndex?localurl=" + fullPath + "&jcfid=" + jcfid, "", "dialogWidth=650px;dialogHeight =300px;status=no;scroll=no;center=yes;edge=sunken;")
    var num = 0;
    //ajax 获取fly文件
    if (jcfid != "" && jcfid != null) {
        $.ajax({
            type: 'post',
            url: '/EmergencyHand/GetFlyFireList',
            data: {
                jcfid: jcfid
            },
            dataType: 'json',
            success: function (obj) {
                if (obj != null && obj.Success) {
                    var html = "";
                    var datalist = obj.DataList;
                    for (var i = 0; i < datalist.length; i++) {
                        html += "<li class='' style=\"cursor: pointer;\"  onclick='onloadFlie(\"" + adrHost + "/UploadFile/FlaFile/" + datalist[i].PLOTTINGFILENAME + "\")'><span class='span2_02'></span>第"
                            + (++num) + "阶段：" + datalist[i].PLOTTINGTITLE + "<a class='cor_ff7 padd_10' onclick='removeFile(\"" + datalist[i].JC_FIRE_PLOTTINGID + "\")'>删除</a></li>";
                    }
                    html += "<li><span></span>当前火点总共有（" + num + "）个阶段标会" + "</li>";
                    $('#flydiv').html(html);
                }
                else {
                    alert("获取Fly文件出错");
                }
            }
        });
    }

}
//还原
function ShowFolderFileListInit() {
    var sgworld = CreateSGObj();
    sgworld.Project.Open(sg.flyURL);
}

//删除fly文件记录
function removeFile(id) {
    var num = 0;
    if (confirm("确定要删除此次标绘吗？")) {
        $.ajax({
            type: 'post',
            url: '/EmergencyHand/RemoveFlyFire',
            data: {
                id: id
            },
            dataType: 'json',
            success: function (obj) {
                if (obj != null && obj.Success) {
                    alert("删除成功");
                    //ajax 获取fly文件
                    $.ajax({
                        type: 'post',
                        url: '/EmergencyHand/GetFlyFireList',
                        data: {
                            jcfid: jcfid
                        },
                        dataType: 'json',
                        success: function (obj) {
                            if (obj != null && obj.Success) {
                                var html = "";
                                var datalist = obj.DataList;
                                for (var i = 0; i < datalist.length; i++) {
                                    html += "<li class='' onclick='onloadFlie(\"" + adrHost + "/UploadFile/FlaFile/" + datalist[i].PLOTTINGFILENAME + "\")'><span class='span2_02'></span>第"
                                        + (++num) + "阶段：" + datalist[i].PLOTTINGTITLE + "<a class='cor_ff7 padd_10' onclick='removeFile(\"" + datalist[i].JC_FIRE_PLOTTINGID + "\")'>删除</a></li>";
                                }
                                html += "<li><span></span>当前火点总共有（" + num + "）个阶段标会" + "</li>";
                                $('#flydiv').html(html);
                            }
                            else {
                                alert("获取Fly文件出错");
                            }
                        }
                    });
                }
                else {
                    alert("删除Fly文件出错");
                }
            }
        });
    }
    window.event.cancelBubble = true;
}

//载入文件
function onloadFlie(path) {
    delRealLocTemp();
    var sgworld = CreateSGObj();
    var id = sgworld.ProjectTree.FindItem(groupName);
    sgworld.ProjectTree.DeleteItem(id);
    // groupID = sgworld.ProjectTree.CreateGroup(groupName, 0);
    sgworld.ProjectTree.LoadFlyLayer(path, getRealLocTemp());
    //sgworld.Project.Open(path);
}

//样式设置=====================================================
//定位到护林员护林员字TextLable样式
function realPositioning(x, y, lableText, Imageurl, fontcolor) {
    var sgworld = CreateSGObj();
    var cLabelStyle = getDiffHlyLableStyle(fontcolor);
    var pos = getPosition(x, y);
    // Imageurl = "/Images/big_adm.png";
    var lable = sgworld.Creator.CreateLabel(pos, lableText, adrHost + Imageurl, cLabelStyle, getRealLocTemp());
    lable.Position.AltitudeType = 2;
    return lable;
}

//获取护林员字TextLable样式
function getHlyLableStyle() {
    var sgworld = CreateSGObj();
    if (sgworld != null) {
        var lableStyle = sgworld.Creator.CreateLabelStyle();
        lableStyle.FontSize = 10;//文字大小
        lableStyle.Scale = 1000;//每个像素的尺寸
        lableStyle.TextOnImage = false;//图像是否在文字上面
        lableStyle.TextAlignment = "Top";
        lableStyle.TextColor.abgrColor = 0x00FFFFFF;
        lableStyle.BackgroundColor.abgrColor = 0x000000FF;//红色 0x0000ff00
        //0x0000ff00 绿色  0x0000ffff 黄色
        return lableStyle;
    }
}

///护林员颜色切换
function getDiffHlyLableStyle(fontcolor) {
    var sgworld = CreateSGObj();
    if (sgworld != null) {
        var lableStyle = sgworld.Creator.CreateLabelStyle();
        lableStyle.FontSize = 10;//文字大小
        lableStyle.Scale = 1000;//每个像素的尺寸
        lableStyle.TextOnImage = false;//图像是否在文字上面
        lableStyle.TextAlignment = "Top";
        // lableStyle.TextColor.abgrColor = 0x00FFFFFF;
        lableStyle.BackgroundColor.abgrColor = fontcolor;//红色
        //0x0000ff00 绿色  0x0000ffff 黄色
        return lableStyle;
    }
}


//创建轨迹点//画圆
function createImageCircle(x, y, lableText) {
    var sgworld = CreateSGObj();
    var cLabelStyle = getHlyLableStyle();
    var pos = getPosition(x, y);
    var lable = sgworld.Creator.CreateLabel(pos, lableText, adrHost + "/Images/circle.png", cLabelStyle, getRealLocTemp());
    lable.Position.AltitudeType = 2;
    sgworld.Navigate.FlyTo(lable);
    return lable;
}

//创建线
function CreatePolyline(x1, y1, x2, y2) {
    var sgworld = CreateSGObj();
    var cVerticesArray = [];
    cVerticesArray.push(x1);
    cVerticesArray.push(y1);
    cVerticesArray.push(100);
    cVerticesArray.push(x2);
    cVerticesArray.push(y2);
    cVerticesArray.push(100);
    var cRing = sgworld.Creator.GeometryCreator.CreateLineStringGeometry(cVerticesArray);
    var colorLine = 0x000000ff;
    var cPolyline = sgworld.Creator.CreatePolyline(cRing, colorLine, 2, getRealLocTemp());
    // cPolyline.LineStyle.Pattern = 0xF00FF00F; 
    cPolyline.LineStyle.Width = 1;
}
//创建轨迹点//画圆 起点终点
function createImageCircleImage(x, y, lableText, imagename) {
    var sgworld = CreateSGObj();
    var cLabelStyle = getHlyLableStyle();
    var pos = getPosition(x, y);
    var lable = sgworld.Creator.CreateLabel(pos, lableText, adrHost + "/Images/" + imagename + ".ico", cLabelStyle, getRealLocTemp());
    lable.Position.AltitudeType = 2;
    sgworld.Navigate.FlyTo(lable);
    return lable;
}

//创建轨迹点//画圆//图标
function createImageNameCircle(x, y, lableText, imageName) {
    var sgworld = CreateSGObj();
    var cLabelStyle = getHlyLableStyle();
    var pos = getPosition(x, y);
    var lable = sgworld.Creator.CreateLabel(pos, lableText, adrHost + "/Images/" + imageName + ".png", cLabelStyle, getRealLocTemp());
    lable.Position.AltitudeType = 2;
    sgworld.Navigate.FlyTo(lable);
    return lable;
}
///清除定时事件
function clearIntervalFun() {
    if (iTime != null) {
        clearInterval(iTime);
    }
}

//调用自带菜单
function excCommand(m, n) {
    var sgworld = CreateSGObj();
    sgworld.Command.Execute(m, n);
}
//绘图页面清除
function delDraw() {
    delRealLocTemp();
    var sgworld = CreateSGObj();
    sgworld.Command.Execute(1033, 0);
}
//选中
var editState = 0;
function selectMove(state) {
    editState = state;
    var sgworld = CreateSGObj();
    sgworld.Window.SetInputMode(1);
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_d_selectMove);
    sgworld.AttachEvent("OnRButtonDown", OnRButtonDown_d_selectMove);
}
//获取实时定位标绘临时组_坐标定位
function getRealLocTemp_zb() {
    var sgworld = CreateSGObj();
    if (realLocTempGroupID == null) {
        return realLocTempGroupID = sgworld.ProjectTree.CreateGroup("RealLocTemp_zuobiao", 0);
    }
    else {
        return realLocTempGroupID;
    }
}

//清除实时定位标绘临时组信息_坐标定位
function delRealLocTemp_zb() {
    var sgworld = CreateSGObj();
    var id = sgworld.ProjectTree.FindItem("RealLocTemp_zuobiao");
    sgworld.ProjectTree.DeleteItem(id);
    realLocTempGroupID = null;
}
//获取实时定位标绘临时组_地图定位
function getRealLocTemp_map() {
    var sgworld = CreateSGObj();
    if (realLocTempGroupID == null) {
        return realLocTempGroupID = sgworld.ProjectTree.CreateGroup("RealLocTemp_map", 0);
    }
    else {
        return realLocTempGroupID;
    }
}

//清除实时定位标绘临时组信息_地图定位
function delRealLocTemp_map() {
    var sgworld = CreateSGObj();
    var id = sgworld.ProjectTree.FindItem("RealLocTemp_map");
    sgworld.ProjectTree.DeleteItem(id);
    realLocTempGroupID = null;
}
//获取实时定位标绘临时组
function getRealLocTemp() {
    var sgworld = CreateSGObj();
    if (realLocTempGroupID == null) {
        return realLocTempGroupID = sgworld.ProjectTree.CreateGroup("RealLocTemp", 0);
    }
    else {
        return realLocTempGroupID;
    }
}

//清除实时定位标绘临时组信息
function delRealLocTemp() {
    var sgworld = CreateSGObj();
    var id = sgworld.ProjectTree.FindItem("RealLocTemp");
    //alert(id + "id");
    sgworld.ProjectTree.DeleteItem(id);
    realLocTempGroupID = null;
}
//获取实时定位标绘临时组_周边分析
function getRealLocTemp_Around() {
    var sgworld = CreateSGObj();
    if (realLocTempGroupID == null) {
        return realLocTempGroupID = sgworld.ProjectTree.CreateGroup("RealLocTemp_Around", 0);
    }
    else {
        return realLocTempGroupID;
    }
}

//清除实时定位标绘临时组信息_周边分析
function delRealLocTemp_Around() {
    var sgworld = CreateSGObj();
    var id = sgworld.ProjectTree.FindItem("RealLocTemp_Around");
    sgworld.ProjectTree.DeleteItem(id);
    realLocTempGroupID = null;
}


function CreateSGObj() {

    var obj = document.getElementById('SGWorld');
    if (obj == null) {
        obj = document.createElement('object');
        obj.name = "sgworld";
        obj.id = "sgworld";
        obj.classid = "CLSID:3a4f91b1-65a8-11d5-85c1-0001023952c1";
    }
    return obj;

}

//创建个点
function creatPosition(x, y) {
    var sgworld = CreateSGObj();
    var dXCoord = x;
    var dYCoord = y;
    var dAltitude = 0;
    var eAltitudeTypeCode = 2; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90.0;
    var dRoll = 0.0;
    var dDistance = 500;
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
    return position;
}

//创建lable
function creatImage(x, y, imgUrl, name) {

    //Images/location.png
    var sgworld = CreateSGObj();
    if (x == null || x == '') {
        alert("无坐标信息！");
        retun;
    }
    var dXCoord = x;
    var dYCoord = y;
    var dAltitude = 0;
    var eAltitudeTypeCode = 2; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90.0;
    var dRoll = 0.0;
    var dDistance = 5000;

    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    cLabelStyle.TextAlignment = "Bottom";
    cLabelStyle.FontSize = 8;
    cLabelStyle.TextOnImage = false;
    cLabelStyle.TextColor.abgrColor = 0xFFFFFFFF;
    cLabelStyle.BackgroundColor.abgrColor = 0x000000;
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
    var imagesLable = sgworld.Creator.CreateLabel(position, name, adrHost + imgUrl, cLabelStyle, getRealLocTemp_Around(), name);
    return imagesLable;
}


//坐标转度分秒 
function jwdtodfm(x) {
    var obj = new Object();
    var d = parseInt(x);
    var df = x - d;
    var f = parseInt(df * 60);
    var m = (df * 60 - f) * 60;
    obj.d = d;
    obj.f = f;
    obj.m = m;
    return obj.d + "°" + obj.f + "′" + parseInt(obj.m) + "″";
}

