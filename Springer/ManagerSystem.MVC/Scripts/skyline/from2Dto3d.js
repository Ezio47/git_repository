var sg = new Object();
sg.flyURL = "http://36.7.68.79:8020/SkylineFly/index8020.FLY";//三维默认加载地址
var adrHost = 'http://' + window.location.host;// "http://localhost:33844";
var sgworld = null;
sg.sgmap = 'sgmap';//div ID

$(document).ready(function () {
    var flyPath = sg.flyURL;
    var sgmap = document.getElementById(sg.sgmap);
    sgworld = document.getElementById('SGWorld');
    if (sgworld == null) {
        sgworld = document.createElement('object');
        sgworld.id = 'SGWorld';
        sgworld.name = 'SGWorld';
        sgworld.classid = 'CLSID:3a4f91b1-65a8-11d5-85c1-0001023952c1';
        sgmap.appendChild(sgworld);
    }
    sgworld.AttachEvent("OnLoadFinished", OnProjectLoadFinished);
    sgworld.Project.Open(flyPath);

});
//三维地图加载结束
function OnProjectLoadFinished() {
    var sgworld = new CreateSGObj()
    if (sgworld.ProjectTree.FindItem("森林火险等级区划图") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("森林火险等级区划图"), false);
    if (sgworld.ProjectTree.FindItem("资源") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("资源"), false);
    if (sgworld.ProjectTree.FindItem("装备") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("装备"), false);
    if (sgworld.ProjectTree.FindItem("营房") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("营房"), false);
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
    if (sgworld.ProjectTree.FindItem("消防队伍") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("消防队伍"), false);
    if (sgworld.ProjectTree.FindItem("其他设施") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("其他设施"), false);
    if (sgworld.ProjectTree.FindItem("政府机关") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("政府机关"), false);
    if (sgworld.ProjectTree.FindItem("公益林") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("公益林"), false);
    if (sgworld.ProjectTree.FindItem("仓库") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("仓库"), false);
    if (sgworld.ProjectTree.FindItem("责任路线") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("责任路线"), false);
    if (sgworld.ProjectTree.FindItem("责任区") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("责任区"), false);
    if (sgworld.ProjectTree.FindItem("林下烧除") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("林下烧除"), false);
    if (sgworld.ProjectTree.FindItem("村标记") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("村标记"), false);
    if (sgworld.ProjectTree.FindItem("山标记") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("山标记"), false);
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
    if (sgworld.ProjectTree.FindItem("有害生物监测点") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("有害生物监测点"), false);
   
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
        //var sgworld = new CreateSGObj();
        var tempdata = data[j].split(':');
        if (tempdata[1] == "1") {
            var name = tempdata[0];
            var ItemID = sgworld.ProjectTree.FindItem(name);
            if (ItemID != 0) {
                sgworld.ProjectTree.SetVisibility(ItemID, true);
            }
        }
    }
    //检索点
    var jcfidvalue = $("#hidjcfid").val();
    $.ajax({
        type: "Post",
        url: "/MapCommon/GetMapDataListInfoAjax",
        data: { jcfid: jcfidvalue },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                for (var i = 0; i < datalist.length; i++) {
                    moveto(datalist[i].JC_FireData.JD, datalist[i].JC_FireData.WD, datalist[i].JC_FireData.WXBH);
                }
            }
        }
    });
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

//title:标题url
//url:地址
//Left:弹出框位置Left
//Top:弹出框位置Top
//width:宽度
//height:高度
function showPopuopByUrl(title, url, x, y, width, height) {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage(title, url, x, y, width, height);
    var popup = sgworld.Window.ShowPopup(Message);
}

function movetoposition(x, y) {


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
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
    sgworld.Navigate.FlyTo(position);
}

function moveto(x, y, title) {


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
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
    var imagesHuoInfo = sgworld.Creator.CreateLabel(position, title, adrHost + "/Images/skyline/situation/5.png", cLabelStyle, 0);
    imagesHuoInfo.Position.AltitudeType = 2;
    sgworld.Navigate.FlyTo(imagesHuoInfo);
}