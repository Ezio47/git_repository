var sg = new Object();

sg.sgmap = 'sgmap';//div ID
//sg.flyURL = "E:\\index1.FLY";//三维默认加载地址-杨岩机器
sg.flyURL = "http://36.7.68.79:9000/SkylineFly/index.FLY";//三维默认加载地址-杨岩机器
var adrHost ='http://'+window.location.host;// "http://localhost:33844";

var realLocTempGroupID = null;
var type = 0;
var iTime = null;



//页面初始化函数
function CreateSGWord(fly) {
    var sgmap = document.getElementById(sg.sgmap);
    var sgworld = document.getElementById('SGWorld');
    if (sgworld == null) {
        sgworld = document.createElement('object');
        sgworld.id = 'SGWorld';
        sgworld.name = 'SGWorld';
        sgworld.classid = 'CLSID:3a4f91b1-65a8-11d5-85c1-0001023952c1';
        sgworld.style.display = "block";
        sgmap.appendChild(sgworld);

    }
    sgworld.Project.Open(fly);
    sgworld.AttachEvent("OnLoadFinished", OnProjectLoadFinished);

}

//三维地图加载结束
function OnProjectLoadFinished() {
    var sgworld = CreateSGObj();
    sgworld.AttachEvent("OnFrame", OnFrame);
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
    //if (sgworld.ProjectTree.FindItem("乡镇边界") != 0)
    //    sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("乡镇边界"), false);
    if (sgworld.ProjectTree.FindItem("越南") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("越南"), false);
    if (sgworld.ProjectTree.FindItem("周边市县") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("周边市县"), false);
    if (sgworld.ProjectTree.FindItem("资源") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("资源"), false);
    if (sgworld.ProjectTree.FindItem("装备") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("装备"), false);
    //if (sgworld.ProjectTree.FindItem("车辆") != 0)
    //     sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("车辆"), false);
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
    var value = $('#lonlathide').val();
    if ((value != "") && (value != undefined)) {
        LocationOnMap(value);
    }
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

//根据用户所属单位定位地图展示地区
function LocationOnMap(value) {
    //检索点 
    var jd = value.split(',')[0];
    var wd = value.split(',')[1];
    var CountryDistance = value.split(',')[2];
    var CountryLine = value.split(',')[3];
    var CityValue = CountryLine + "边界";
    var sgworld = CreateSGObj();
    if (sgworld.ProjectTree.FindItem(CityValue) != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem(CityValue), true);
    movetoCountry(jd, wd, CountryDistance);
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


function OnFrame() {
    var sgworld = CreateSGObj();
    //画箭头
    var mouseInfo = sgworld.Window.GetMouseInfo();
    var cursorCoord = sgworld.Window.PixelToWorld(mouseInfo.X, mouseInfo.Y);
    $("#sbxx").html("经度：" + cursorCoord.Position.X.toFixed(6) + "/" + jwdtodfm(cursorCoord.Position.X.toFixed(6)) + "，纬度：" + cursorCoord.Position.Y.toFixed(6) + "/" + jwdtodfm(cursorCoord.Position.Y.toFixed(6)) + "，高程：" + cursorCoord.Position.Altitude.toFixed(2) + "米");

}
//地图定位代码
var _imagesUrl = null;
function createImagesHUO(imagesUrl) {
    var sgworld = CreateSGObj();
    _imagesUrl = imagesUrl;
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_createImages);

}

function OnLButtonDown_createImages(Flags, X, Y) {
    var sgworld = CreateSGObj();
    //delRealLocTemp_map();
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);   //把屏幕坐标转化为地理坐标
    var position = cursorCoord.Position;
    $("#dwjd").val(cursorCoord.Position.X.toFixed(6) + "/" + jwdtodfm(cursorCoord.Position.X.toFixed(6)));
    $("#dwwd").val(cursorCoord.Position.Y.toFixed(6) + "/" + jwdtodfm(cursorCoord.Position.Y.toFixed(6)));
    $("#dwgc").val(cursorCoord.Position.Altitude.toFixed(2) + "米");
    //sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_createImages);
    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    try {
        var images = sgworld.Creator.CreateImageLabel(position, adrHost + "/Images/" + _imagesUrl, cLabelStyle, getRealLocTemp_map());
    } catch (e) {
        delRealLocTemp_map();
        var images = sgworld.Creator.CreateImageLabel(position, adrHost + "/Images/" + _imagesUrl, cLabelStyle, getRealLocTemp_map());
    } 
}
function DELcreateImages() {
    var sgworld = CreateSGObj();
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_createImages);
}
function Cancel_createImages1() {
    Cancel_createImages();
    createImagesHUO('location.png')
}

function Cancel_createImages() {
    $("#dwjd").val("");
    $("#dwwd").val("");
    $("#dwgc").val("");
    try {
        delRealLocTemp_map();
    } catch (e) {
        //
    }
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

//获取护林员字TextLable样式
function getHlyLableStyle() {
    var sgworld = CreateSGObj();
    if (sgworld != null) {
        var lableStyle = sgworld.Creator.CreateLabelStyle();
        lableStyle.FontSize = 10;//文字大小
        lableStyle.Scale = 1000;//每个像素的尺寸
        lableStyle.TextOnImage = false;//图像是否在文字上面
        lableStyle.TextAlignment = "Top";
        lableStyle.TextColor.abgrColor = 0x000000FF;
        lableStyle.BackgroundColor.abgrColor = 0x00FFFFFF;
        return lableStyle;
    }
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
        lableStyle.BackgroundColor.abgrColor = 0x000000FF;
        return lableStyle;
    }
}
//根据x,y获取三维位置
function getPosition(x, y) {
    var sgworld = CreateSGObj();
    // alert("x===" + x + "===y====" + y);
    var dXCoord = x;//测试
    var dYCoord = y;//测试
    var dAltitude = 0.0;
    var eAltitudeTypeCode = 0; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90.0;
    var dRoll = 0.0;
    var dDistance = 2000;
    return sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
}
//定位到护林员护林员字TextLable样式
function realPositioning(x, y, lableText) {
    var sgworld = CreateSGObj();
    var cLabelStyle = getHlyLableStyle();
    var pos = getPosition(x, y);
    var lable = sgworld.Creator.CreateLabel(pos, lableText, adrHost + "/Images/big_adm.png", cLabelStyle, getRealLocTemp());
    lable.Position.AltitudeType = 2;
    return lable;
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
//创建轨迹点//定位
function createImageLoc(x, y, lableText) {
    if (iTime != "") {
        window.clearTimeout(iTime);
    }
    var sgworld = CreateSGObj();
    var cLabelStyle = getHlyLableStyle();
    var pos = getPosition(x, y);
    var lable = sgworld.Creator.CreateLabel(pos, lableText, adrHost + "/Images/location.png", cLabelStyle, getRealLocTemp_zb());
    lable.Position.AltitudeType = 2;
    sgworld.Navigate.FlyTo(lable);
    return lable;
}
//创建轨迹点//画圆
function createCircle(x, y) {
    var sgworld = CreateSGObj();
    var pos = getPosition(x, y);
    var circle = sgworld.Creator.CreateCircle(pos, 10, 0x00000000, 0x000000ff);
    circle.FillStyle.Color.SetAlpha(1);
    circle.Position.AltitudeType = 2;
    sgworld.Navigate.FlyTo(circle);
    return circle;
}
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

//定位按钮start//
//取出最新经纬度定位（中间表--实时定位）
function getLonLat(uidstr) {
    if (iTime != null) {
        window.clearTimeout(iTime);
    }
    var sgworld = CreateSGObj();
    delRealLocTemp();
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetRealAjax",
        data: { uidstr: uidstr, maptype: "Skyline" },
        dataType: "json",
        success: function (data) {

            if (data != null) {
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        var lable = realPositioning(data[i].LONGITUDE, data[i].LATITUDE, data[i].HNAME);
                        if (i == data.length - 1) {
                            sgworld.Navigate.FlyTo(lable);
                        }
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

//实时轨迹start//
var oldx, oldy, time;
//实时轨迹
function RealLocation(phone, uid) {
    sgworld = CreateSGObj();
    delRealLocTemp();

    //map.graphics.clear();
    // graphicLayer.clear();
    //$('#btnConOver').show();//结束按钮显示
    //$('#btnCon').hide();
    hisi = 100000000;//设置历史轨迹回放索引最大值，解决轨迹回放过程中实时定位时继续轨迹回放的功能
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
                //for (var i = 0; i < datalsit.length; i++) {
                //    if (i != datalsit.length - 1) {
                //        var str = "时间：" + datalsit[i].SBTIME;
                //        str = datalsit[i].SBTIME.substring(10, datalsit[i].SBTIME.length);
                //        var lable = createImageCircle(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);
                //        if (i != 0) {

                //            CreatePolyline(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), parseFloat(datalsit[i + 1].LONGITUDE), parseFloat(datalsit[i + 1].LATITUDE));
                //        }

                //        // ptPositionPerReal(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);//点
                //        // drawLine(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), parseFloat(datalsit[i + 1].LONGITUDE), parseFloat(datalsit[i + 1].LATITUDE));//线
                //    }
                //    else {
                //        var str = datalsit[i].SBTIME.substring(10, datalsit[i].SBTIME.length);
                //        oldx = parseFloat(datalsit[i].LONGITUDE);// 经度
                //        oldy = parseFloat(datalsit[i].LATITUDE);//纬度
                //        time = datalsit[i].SBTIME;
                //        var lable = createImageCircle(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);
                //        sgworld.Navigate.FlyTo(lable);
                //    }
                //}
                var i = datalsit.length - 1;
                var str = "时间：" + datalsit[i].SBTIME;
                var lable = createImageCircle(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);
                sgworld.Navigate.FlyTo(lable);
                time = datalsit[i].SBTIME;
                oldx = parseFloat(datalsit[i].LONGITUDE);// 经度
                oldy = parseFloat(datalsit[i].LATITUDE);//纬度
                iTime = self.setInterval("RealLocationInterval('" + phone + "','" + uid + "')", 5000);
            }
            else {
                //layer.alert('没有实时轨迹数据！', { icon: 5 });
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
                        // ptPositionPerReal(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);
                        CreatePolyline(oldx, oldy, parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE));
                        //drawLine(oldx, oldy, parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE));
                        oldx = parseFloat(datalsit[i].LONGITUDE);// 经度
                        oldy = parseFloat(datalsit[i].LATITUDE);//纬度
                        time = datalsit[i].SBTIME;
                        var sgworld = CreateSGObj();
                        sgworld.Navigate.FlyTo(lable);
                    }
                }
            }
            //else {
            //    layer.alert('没有实时轨迹数据！', { icon: 5 });
            //}
        }
    });


}
//end

//历史轨迹start//
var hisi = 0;//历史轨迹索引
gogps = function () {
    go();
    if (hisi < datalist.length) {
        iTime = window.setTimeout("gogps()", 2000);
    } else {
        window.clearTimeout(iTime);
        hisi = 0;
        //datalist = null;
    }
}

//历史估计回放
var datalist;
function hisgjPlay(uid, time) {
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
function pauseAndStart() {
    if (bo == true) {
        ss = hisi;
        hisi = 100000000;//设置最大值，停止循环
        bo = false;
        // drawpausefun();//暂停
        // $('#pausebtn').val('开始');
        // $('#overbtn').attr('disabled', true);//禁用
    }
    else {
        hisi = ss;
        bo = true;
        // drawstartfun();//开始
        // $('#pausebtn').val('暂停');
        //$('#overbtn').attr('disabled', false);//禁用
    }
}
//开始
var t;
function drawstartfun() {
    //console.info(hisi);
    go();
    if (hisi < datalist.length) {
        t = window.setTimeout("drawstartfun()", 2000);
    }
}
//暂停
function drawpausefun() {
    window.clearTimeout(t);

}

//结束
function drawOver() {
    //alert(hisi);
    //$('#pausebtn').attr("disabled", true);//暂停设置不可用
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
                //createImageCircle(parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE), str);
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

//start//
//获取火线等级数据
function loadDj() {
    $.ajax({
        type: "Post",
        url: "/EarlyMonitor/GetFireLevelList",
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {

                var datalist = obj.DataList;
                loadHxdj(datalist);
                ///for (var i = 0; i < datalist.length; i++) {
                ///    if (datalist[i].DANGERCLASS >= 5) {
                //   buf.push(datalist[i]);
                //    }
                // }

            }


        }
    });
}
//图层加载火线等级数据
function loadHxdj(arr) {
    var sgworld = CreateSGObj();
    var id = sgworld.ProjectTree.FindItem("森林火险等级区划图");
    var obj = SGWorld.ProjectTree.GetLayer(id);
    var pIFeatureGroup = obj.FeatureGroups(0);
    for (var i = 0; i < pIFeatureGroup.Count; i++) {
        var pIFeature = pIFeatureGroup.Item(i);
        for (var j = 0; j < pIFeature.FeatureAttributes.Count; j++) {
            var pIFeatureAttribute = pIFeature.FeatureAttributes.Item(j);
            if (pIFeatureAttribute.Name == "NAME") {

                for (var k = 0; k < arr.length; k++) {
                    if (arr[k].TOWNNAME == pIFeatureAttribute.Value) {

                        var color = getHXDJColor(arr[k].DANGERCLASS);
                        var cPolygon = sgworld.Creator.CreatePolygon(pIFeature.Geometry, 0x00000000, color, 2, 0);
                        cPolygon.FillStyle.Color.abgrColor = color;
                        cPolygon.FillStyle.Color.SetAlpha(0.5);
                        cPolygon.LineStyle.Color.SetAlpha(0.5);
                    }
                }
            }
        }
    }
}
//根据等级获取颜色
function getHXDJColor(level) {
    if (level == 1)
    { return 0x00008800; }
    if (level == 2)
    { return 0x00FF0000; }
    if (level == 3)
    { return 0x0000F9F9; }
    if (level == 4)
    { return 0x0000A5FF; }
    if (level == 5)
    { return 0x000000FF; }
}
//end//


//定位到x,y坐标
function moveto(x, y) {
    delRealLocTemp();
    var sgworld = CreateSGObj();
    if (x == null || x == '') {
        alert("无坐标信息！");
    }
    else {

        //var circle = createCircle(x, y);

        var circle = createImageCircle(x, y, "");
        sgworld.Navigate.FlyTo(circle);
    }
}
//定位到x,y坐标
function moveto(x, y, title) {
    delRealLocTemp();
    var sgworld = CreateSGObj();
    if (x == null || x == '') {
        alert("无坐标信息！");
    }
    else {

        var circle = createImageCircle(x, y, title);
        sgworld.Navigate.FlyTo(circle);
    }
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

        //var circle = createCircle(x, y);

        var circle = createImageNameCircle(x, y, "", imagename);
        sgworld.Navigate.FlyTo(circle);
    }
}
//定位到x,y坐标
function movetoLoc(x, y) {
    var sgworld = CreateSGObj();
    delRealLocTemp_zb();
    if (x == null || x == '') {
        alert("无坐标信息！");
    }
    else {

        //var circle = createCircle(x, y);

        var circle = createImageLoc(x, y, "");
        sgworld.Navigate.FlyTo(circle);
    }
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
function Load() {
    document.getElementById('btnhidden').focus();    //加载时，设置焦点
    if (document.all) {
        window.document.onclick = new Function("return onClick(event);"); //当点击MenuPage.html时，再次聚焦
    } else {
        window.document.body.setAttribute("onclick", "return onClick(event);");
    }
}
//根据标题删除弹出框
function removePopuo(caption) {
    var sgworld = CreateSGObj();
    sgworld.Window.RemovePopupByCaption(caption);

}
//图层查询定位时间
function onClick(ev) {
    ev = ev || window.event;
    var target = ev.target || ev.srcElement;
    if (target && target.id && target.id == "PanelBox") {
    }
    else {
        if (target.tagName == 'INPUT') {
            if (target.role == 'checkbox') {
                document.getElementById('btnhidden').focus();    //当点击checkbox的时候，再次聚焦
            }
        }
        else { document.getElementById('btnhidden').focus(); }
    }

}
//图层定位
function dingwei() {
    var jd, wd;
    if ($("input[name=typeRadio]:checked").val() == 0) {
        jd = $("#jd").val();
        wd = $("#wd").val();
    }
    else {
        var obj = new Object();
        obj.d = $("#jd_d").val();
        obj.f = $("#jd_f").val();
        obj.m = $("#jd_m").val();
        jd = jsw1tojsw2(obj);
        obj.d = $("#wd_d").val();
        obj.f = $("#wd_f").val();
        obj.m = $("#wd_m").val();
        wd = jsw1tojsw2(obj);
    }
    movetoLoc(jd, wd);
}

//图层定位根据名称查询图层
function qrueyByName() {
    var name = $("#name").val();
    if ($.trim(name) == "") {
        alert("请输入名称");
        $('#name').focus()
        return false;
    }
    else {
        var flagstr = $("#cc").combobox("getValues");
        if (flagstr != "") {
            flagstr = flagstr.join(',');
        }
        else {
            var arr = new Array(); //数组定义标准形式
            $("#cc option").each(function () {
                var val = $(this).val(); //获取单个value
                arr.push(val);
            });
            flagstr = arr.join(',');
        }

        $.ajax({
            type: "Post",
            url: "/EmergencyHand/GetQueryLayerInfos",
            data: { name: name, flagstr: flagstr },
            dataType: "json",
            success: function (data) {
                if (data != null && data.Success) {
                    var datalist = data.DataList;
                    if (datalist.length <= 0) {
                        alert("未查询到结果");
                    }
                }
                $("#dg").datagrid("loadData", datalist);
            }
        });
    }
    // GetFeatureAll($.trim(name));
}

//获取图层
function GetFeatureAll(str) {
    var sgworld = CreateSGObj();
    var strResult = "";
    var arr = [];
    var layers = $("#cc").combobox("getValues");

    if (layers.length == 0) {
        var tt = $("#cc").combobox("getData");
        var arrvalue = [];
        for (var i = 0; i < tt.length; i++) {
            arrvalue.push(tt[i].value);
        }
        layers = arrvalue;
    }
    for (var tt = 0 ; tt < layers.length; tt++) {
        //var selectText = $("#cc").combobox("getValue");
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
                if (pIFeatureAttribute.Name.toUpperCase() == "DISPALY_X") {
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
    moveto(row.Display_X, row.Display_Y, row.Name);
}
//测试按钮
function test(node, checked) {

    var sgworld = CreateSGObj();
    var ItemID = sgworld.ProjectTree.FindItem(node.text);
}
//经纬度转换，经纬度转数字
function jsw1tojsw2(obj) {
    //return parseFloat(obj.d) + "°" + parseFloat(obj.f / 60) + "′" + parseFloat(obj.m / 60 / 60) + "″";
    return parseFloat(obj.d) + parseFloat(obj.f / 60) + parseFloat(obj.m / 60 / 60);
}
//经纬度转换，数字转经纬度
function jsw2tojsw1(x) {
    var obj = new Object();
    var d = parseInt(x);
    var df = x - d;
    var f = parseInt(df * 60);
    var m = (df * 60 - f) * 60;
    obj.d = d;
    obj.f = f;
    obj.m = m;
    return obj;
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

//调用自带菜单
function excCommand(m, n) {
    var sgworld = CreateSGObj();
    sgworld.Command.Execute(m, n);
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
//公益林table点击事件
function onClickGYL(objId, stx, sty) {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('查询', adrHost + "/PublicForest/PopDetailIndex?objid=" + objId, 0, 0, 300, 450);
    var popup = sgworld.Window.ShowPopup(Message);
    moveto(stx, sty);
    if (sgworld.ProjectTree.FindItem("公益林") != 0) {
        if (type == 1) {
            sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("公益林"), true);
        }
    }
    if (sgworld.ProjectTree.FindItem("有害生物采集面") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("有害生物采集面"), false);
    if (sgworld.ProjectTree.FindItem("野生动物") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生动物"), false);
    if (sgworld.ProjectTree.FindItem("野生动物范围") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生动物范围"), false);
    if (sgworld.ProjectTree.FindItem("野生植物") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生植物"), false);
}
//野生动物table点击事件
function onClickYSDW(objId, stx, sty) {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('查询', adrHost + "/DataCenter/WILD_ANIMALTotalIndex?tablename=WILD_ANIMALDISTRIBUTE&ID=" + objId, 0, 0, 600, 400);
    var popup = sgworld.Window.ShowPopup(Message);
    var arr = gcj02towgs84(stx, sty);//火星坐标系装wgs84
    var jd = arr[0];
    var wd = arr[1];
    moveto(jd, wd);
    if (sgworld.ProjectTree.FindItem("野生动物") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生动物"), true);
    if (sgworld.ProjectTree.FindItem("有害生物采集面") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("有害生物采集面"), false);
    if (sgworld.ProjectTree.FindItem("野生动物范围") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生动物范围"), true);
    if (sgworld.ProjectTree.FindItem("野生植物") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生植物"), false);
    if (sgworld.ProjectTree.FindItem("公益林") != 0) 
        gworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("公益林"), false);
    
}
//野生植物table点击事件
function onClickYSZW(objId, stx, sty) {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('查询', adrHost + "/DataCenter/WILD_ANIMALTotalIndex?tablename=WILD_BOTANYDISTRIBUTE&ID=" + objId, 0, 0, 600, 400);
    var popup = sgworld.Window.ShowPopup(Message);
    var arr = gcj02towgs84(stx, sty);//火星坐标系装wgs84
    var jd = arr[0];
    var wd = arr[1];
    moveto(jd, wd);
    if (sgworld.ProjectTree.FindItem("野生植物") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生植物"), true);
    if (sgworld.ProjectTree.FindItem("有害生物采集面") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("有害生物采集面"), false);
    if (sgworld.ProjectTree.FindItem("野生动物") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生动物"), false);
    if (sgworld.ProjectTree.FindItem("野生动物范围") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生动物范围"), false);
    if (sgworld.ProjectTree.FindItem("公益林") != 0) 
        gworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("公益林"), false);
}
//有害生物table点击事件
function onClickYHSW(objId, stx, sty) {
    var sgworld = CreateSGObj();
    var Message = sgworld.Creator.CreatePopupMessage('查询', adrHost + "/PEST/PESTCollectDataSee?DataId=" + objId, 0, 0, 650, 420);
    var popup = sgworld.Window.ShowPopup(Message);
    var arr = gcj02towgs84(stx, sty);//火星坐标系装wgs84
    var jd = arr[0];
    var wd = arr[1];
    moveto(jd, wd);
    if (sgworld.ProjectTree.FindItem("有害生物采集面") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("有害生物采集面"), true); 
    if (sgworld.ProjectTree.FindItem("野生植物") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生植物"), false);
    if (sgworld.ProjectTree.FindItem("野生动物") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生动物"), false);
    if (sgworld.ProjectTree.FindItem("野生动物范围") != 0) 
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("野生动物范围"), false);
    if (sgworld.ProjectTree.FindItem("公益林") != 0) 
        gworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("公益林"), false);
}
///清除定时事件
function clearIntervalFun() {
    if (iTime != null) {
        clearInterval(iTime);
    }
}

/**
 * Created by Wandergis on 2015/7/8.
 * 提供了百度坐标（BD09）、国测局坐标（火星坐标，GCJ02）、和WGS84坐标系之间的转换
 */
//定义一些常量
var x_PI = 3.14159265358979324 * 3000.0 / 180.0;
var PI = 3.1415926535897932384626;
var a = 6378245.0;
var ee = 0.00669342162296594323;
/**
 * 百度坐标系 (BD-09) 与 火星坐标系 (GCJ-02)的转换
 * 即 百度 转 谷歌、高德
 * @param bd_lon
 * @param bd_lat
 * @returns {*[]}
 */
function bd09togcj02(bd_lon, bd_lat) {
    var x_pi = 3.14159265358979324 * 3000.0 / 180.0;
    var x = bd_lon - 0.0065;
    var y = bd_lat - 0.006;
    var z = Math.sqrt(x * x + y * y) - 0.00002 * Math.sin(y * x_pi);
    var theta = Math.atan2(y, x) - 0.000003 * Math.cos(x * x_pi);
    var gg_lng = z * Math.cos(theta);
    var gg_lat = z * Math.sin(theta);
    return [gg_lng, gg_lat]
}
/**
 * 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换
 * 即谷歌、高德 转 百度
 * @param lng
 * @param lat
 * @returns {*[]}
 */
function gcj02tobd09(lng, lat) {
    var z = Math.sqrt(lng * lng + lat * lat) + 0.00002 * Math.sin(lat * x_PI);
    var theta = Math.atan2(lat, lng) + 0.000003 * Math.cos(lng * x_PI);
    var bd_lng = z * Math.cos(theta) + 0.0065;
    var bd_lat = z * Math.sin(theta) + 0.006;
    return [bd_lng, bd_lat]
}
/**
 * WGS84转GCj02
 * @param lng
 * @param lat
 * @returns {*[]}
 */
function wgs84togcj02(lng, lat) {
    if (out_of_china(lng, lat)) {
        return [lng, lat]
    }
    else {
        var dlat = transformlat(lng - 105.0, lat - 35.0);
        var dlng = transformlng(lng - 105.0, lat - 35.0);
        var radlat = lat / 180.0 * PI;
        var magic = Math.sin(radlat);
        magic = 1 - ee * magic * magic;
        var sqrtmagic = Math.sqrt(magic);
        dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * PI);
        dlng = (dlng * 180.0) / (a / sqrtmagic * Math.cos(radlat) * PI);
        var mglat = parseFloat(lat) + parseFloat(dlat);
        var mglng = parseFloat(lng) + parseFloat(dlng);
        return [mglng, mglat]
    }
}
/**
 * GCJ02 转换为 WGS84
 * @param lng
 * @param lat
 * @returns {*[]}
 */
function gcj02towgs84(lng, lat) {
    if (out_of_china(lng, lat)) {
        return [lng, lat]
    }
    else {
        var dlat = transformlat(lng - 105.0, lat - 35.0);
        var dlng = transformlng(lng - 105.0, lat - 35.0);
        var radlat = lat / 180.0 * PI;
        var magic = Math.sin(radlat);
        magic = 1 - ee * magic * magic;
        var sqrtmagic = Math.sqrt(magic);
        dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * PI);
        dlng = (dlng * 180.0) / (a / sqrtmagic * Math.cos(radlat) * PI);
        mglat = parseFloat(lat) + parseFloat(dlat);
        mglng = parseFloat(lng) + parseFloat(dlng);
        var jd = parseFloat(lng * 2) - parseFloat(mglng);
        var wd = parseFloat(lat * 2) - parseFloat(mglat);
        return [jd, wd]
    }
}
function transformlat(lng, lat) {
    var ret = -100.0 + 2.0 * lng + 3.0 * lat + 0.2 * lat * lat + 0.1 * lng * lat + 0.2 * Math.sqrt(Math.abs(lng));
    ret += (20.0 * Math.sin(6.0 * lng * PI) + 20.0 * Math.sin(2.0 * lng * PI)) * 2.0 / 3.0;
    ret += (20.0 * Math.sin(lat * PI) + 40.0 * Math.sin(lat / 3.0 * PI)) * 2.0 / 3.0;
    ret += (160.0 * Math.sin(lat / 12.0 * PI) + 320 * Math.sin(lat * PI / 30.0)) * 2.0 / 3.0;
    return ret
}
function transformlng(lng, lat) {
    var ret = 300.0 + lng + 2.0 * lat + 0.1 * lng * lng + 0.1 * lng * lat + 0.1 * Math.sqrt(Math.abs(lng));
    ret += (20.0 * Math.sin(6.0 * lng * PI) + 20.0 * Math.sin(2.0 * lng * PI)) * 2.0 / 3.0;
    ret += (20.0 * Math.sin(lng * PI) + 40.0 * Math.sin(lng / 3.0 * PI)) * 2.0 / 3.0;
    ret += (150.0 * Math.sin(lng / 12.0 * PI) + 300.0 * Math.sin(lng / 30.0 * PI)) * 2.0 / 3.0;
    return ret
}
/**
 * 判断是否在国内，不在国内则不做偏移
 * @param lng
 * @param lat
 * @returns {boolean}
 */
function out_of_china(lng, lat) {
    return (lng < 72.004 || lng > 137.8347) || ((lat < 0.8293 || lat > 55.8271) || false);
}