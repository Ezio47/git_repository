var sg = new Object();

sg.flyURL = "http://36.7.68.79:9000/SkylineFly/index.FLY";//三维默认加载地址-杨岩机器
//sg.flyURL = "E:\\index.FLY";//三维默认加载地址-尹世翔本机
var adrHost = 'http://' + window.location.host;//"http://localhost:33844";
//var skylineSaveAdress = "C:/Users//yayn/AppData/Roaming/Skyline/TerraExplorer";//保存地址-杨岩机器
var skylineSaveAdress = "C:/Users/yinshixiang/AppData/Roaming/Skyline/TerraExplorer";//保存地址-尹世翔机器

sg.sgmap = 'sgmap';//div ID
sg.isOnloadSuccse = false;
var sgworld = null;
var playersName = [];
var playersInfo = {};
var pLayerTree = [];

var groupName = "标绘";
var groupID = null;
var treeDate = [{ "id": "加油站", "text": "加油站", "checked": false },
           { "id": "村驻地", "text": "村驻地", "checked": true },
           { "id": "地级市驻地", "text": "地级市驻地", "checked": true },
           { "id": "河流", "text": "河流", "checked": false },
           //{ "id": "其它道路", "text": "其它道路", "checked": false },
           { "id": "省道", "text": "省道", "checked": false },
           { "id": "水系面", "text": "水系面", "checked": false },
           { "id": "铁路", "text": "铁路", "checked": false },
           { "id": "停车场", "text": "停车场", "checked": false },
           { "id": "县道", "text": "县道", "checked": false },
           { "id": "县界", "text": "县界", "checked": false },
           { "id": "乡镇村道", "text": "乡镇村道", "checked": false },
           { "id": "乡镇驻地", "text": "乡镇驻地", "checked": false },
           { "id": "乡镇边界", "text": "乡镇边界", "checked": false },
           { "id": "资源", "text": "资源", "checked": false },
           { "id": "装备", "text": "装备", "checked": false },
           { "id": "车辆", "text": "车辆", "checked": false },
           { "id": "营房", "text": "营房", "checked": false },
           { "id": "瞭望台", "text": "瞭望台", "checked": false },
           { "id": "宣传碑牌", "text": "宣传碑牌", "checked": false },
           { "id": "中继站", "text": "中继站", "checked": false },
           { "id": "监测站", "text": "监测站", "checked": false },
           { "id": "因子采集站", "text": "因子采集站", "checked": false },
           { "id": "消防队伍", "text": "消防队伍", "checked": false },
           { "id": "隔离带", "text": "隔离带", "checked": false },
           { "id": "防火通道", "text": "防火通道", "checked": false },
           { "id": "其他设施", "text": "其他设施", "checked": false },
           { "id": "政府机关", "text": "政府机关", "checked": false },
           { "id": "森林火险等级区划图", "text": "火险等级", "checked": false },
           { "id": "公益林", "text": "公益林", "checked": false }];
//三维地图初始化函数
$(document).ready(function () {
    //var $tmpMenu = $(), $indexMenuDiv = $(".indexMenuDiv"), $indexMenus = $indexMenuDiv.children("ul").children(), $indexSecMenuDiv = $(".indexSecMenuDiv");
    //$indexSecMenuDiv.children().hide();
    ////一级菜单点击
    //$indexMenuDiv.on("click", "li", function () {
    //    var id = "sec" + $(this).attr("id");
    //    $indexMenus.filter(".sel").removeClass("sel");
    //    $(this).addClass("sel");
    //    $tmpMenu.hide();
    //    $tmpMenu = $("#" + id).show();
    //    $("#" + id).children().first().click();
    //});
    //////二级菜单点击
    ////$indexSecMenuDiv.on("click", "li", function () {
    ////    $(".indexIframe>iframe").attr("src", $(this).attr("src"));
    ////});
    //$indexMenus.eq(1).click();
    //ShowFolderFileList();




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
    $('#easyui-combotree').combotree('loadData', treeDate);


});


//清除实时定位标绘临时组信息
function delRealLocTemp() {
    var id = sgworld.ProjectTree.FindItem(groupID);
    var id1 = sgworld.ProjectTree.FindItem(getRealLocTemp());
    sgworld.ProjectTree.DeleteItem(id);
    sgworld.ProjectTree.DeleteItem(id1);
    groupID = sgworld.ProjectTree.CreateGroup(groupName, 0);
 
}


function mainboardimgClick() {
    if ($(this).attr("flag") == "1") {
        $(this).css("border", "1px solid #cccccc");
        $(this).attr("flag", "0");
        sgworld.DetachEvent("OnLButtonDown", OnLButtonDown);
    }
    else {
        $(this).css("border", "1px solid red").siblings().css("border", "1px solid #cccccc");
        $(this).attr("flag", "1").siblings().css("flag", "0");
        doDraw();
    }
}




//三维地图加载结束
function OnProjectLoadFinished() {

    $("#mainboard img").bind("click", mainboardimgClick);


    sgworld.AttachEvent("OnFrame", OnFrame);
    getBasicInfo();

    groupID = sgworld.ProjectTree.CreateGroup(groupName, 0);
    if (sgworld.ProjectTree.FindItem("加油站") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("加油站"), false);
    if (sgworld.ProjectTree.FindItem("村驻地") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("村驻地"), false);
    if (sgworld.ProjectTree.FindItem("河流") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("河流"), false);
    if (sgworld.ProjectTree.FindItem("其它道路") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("其它道路"), false);
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
    if (sgworld.ProjectTree.FindItem("资源") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("资源"), false);
    if (sgworld.ProjectTree.FindItem("装备") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("装备"), false);
    //if (sgworld.ProjectTree.FindItem("车辆") != 0)
    //    sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("车辆"), false);
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
    if (sgworld.ProjectTree.FindItem("消防队伍") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("消防队伍"), false);
    if (sgworld.ProjectTree.FindItem("其他设施") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("其他设施"), false);
    if (sgworld.ProjectTree.FindItem("政府机关") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("政府机关"), false);
    if (sgworld.ProjectTree.FindItem("森林火险等级区划图") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("森林火险等级区划图"), false)
    if (sgworld.ProjectTree.FindItem("公益林") != 0)
        sgworld.ProjectTree.SetVisibility(sgworld.ProjectTree.FindItem("公益林"), false)


}
function OnSGWorldMessage(MessageID, SourceObjectID) {
    alert(MessageID);
    alert(SourceObjectID);
}
function OnRButtonDown111(Flags, X, Y, p) {

    //alert(Flags);
    //alert(X);
    //alert(Y);
    p = false;
}


function OnContainerChanged(Operation, ContainerItem) {
    alert("Operation:" + Operation + ",ContainerItem:" + ContainerItem);
}
function OnSGWorldMessage(MessageID, SourceObjectID) {
    alert("MessageID:" + MessageID + ",SourceObjectID:" + SourceObjectID);
}
function OnInputModeChanged(NewMode) {

    //alert(NewMode);
    //alert("NewMode:" + NewMode);

}
function OnRButtonDown(Flags, X, Y, bHandled) {
    bHandled = true;
}
//获取基本信息
function getBasicInfo() {
    if (sgworld != null) {
        var proTree = sgworld.ProjectTree;
        var rootItem = proTree.GetNextItem(proTree.RootID, 18);
        bulidTreeFirst(rootItem);
    }
}

function bulidTreeFirst(current) {
    playersName = [];
    playersInfo = {};
    pLayerTree = [];
    while (current) {
        var proTree = sgworld.ProjectTree;
        var fchild = { 'text': '', 'children': [], 'leaf': true };
        var itemName = proTree.GetItemName(current);
        //判断子项是否是一个组（为什么判断是否为组呢？因为skyline的数据结构是这样组织的，结构如下：  
        //子项-组-图层等）  
        if (proTree.IsGroup(current)) {
            fchild.text = itemName;
            if (proTree.IsLayer(current)) {//叶子节点  
                var name = proTree.GetItemName(current);
                var layer = proTree.getLayer(current);
                playersName.push(name);
                playersInfo[name] = layer;
                fchild.text = name;
            } else {
                fchild.leaf = false;
                var childItem = proTree.GetNextItem(current, 11);
                buildTreeChild(childItem, fchild);
            }
            pLayerTree.push(fchild);
        }
        else {
            fchild.text = itemName;
            pLayerTree.push(fchild);
        }
        current = proTree.GetNextItem(current, 13);
    }
    $('#easyui-tree').tree({
        data: eval(this.pLayerTree),
        onClick: treeClick
    });
}
function treeClick(node) {
    var itemID = sgworld.ProjectTree.FindItem(node.text)
    var objID = sgworld.ProjectTree.GetTerraObjectID(itemID);
    sgworld.Navigate.FlyTo(objID);

}
function buildTreeChild(current, parent) {
    try {
        while (current) {
            var proTree = sgworld.ProjectTree;
            var fchild = { 'text': '', 'children': [], 'leaf': true };
            //获取树节点的名称  
            var itemName = proTree.GetItemName(current);
            if (proTree.IsGroup(current)) {
                fchild.text = itemName;
                if (proTree.IsLayer(current)) {//叶子节点  
                    var name = proTree.GetItemName(current);
                    var layer = proTree.getLayer(current);
                    playersName.push(name);
                    playersInfo[name] = layer;
                    fchild.text = name;
                } else {
                    fchild.leaf = false;
                    var childItem = proTree.GetNextItem(current, 11);
                    buildTreeChild(childItem, fchild);
                }
                parent.children.push(fchild);
            }
            else {
                fchild.text = itemName;
                parent.children.push(fchild);
            }
            //13:获取其兄弟节点  
            current = proTree.GetNextItem(current, 13);
        }
    }
    catch (e) {
        alert(e.message);
    }
}
function drawCircle() {
    sgworld.Command.Execute(1012, 1);
}
function aaaa() {
    $('#p1').panel('open');
}
function doDraw() {
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown);
}

//画imagesLable点击事件
function OnLButtonDown(Flags, X, Y) {
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);   //把屏幕坐标转化为地理坐标
    var position = cursorCoord.Position;
    var imagesName = $('#imagesName').val();
    var iamgesUrl = $("img[flag='1']").attr("src");
    $("img[flag='1']").css("border", "1px solid #cccccc");
    $("img[flag='1']").attr("flag", "0");
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown);
    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    var images = sgworld.Creator.CreateImageLabel(position, adrHost + iamgesUrl, cLabelStyle, groupID);
    // sgworld.Navigate.FlyTo(images);
    //sgworld.DetachEvent("OnLButtonDown", OnLButtonDown);
    //  var message = sgworld.Creator.CreateMessage(5, imagesName, 0);
    // images.Message.MessageID = message.ID;   
}


var _imagesUrl = null;
function createImagesHUO(imagesUrl) {
    _imagesUrl = imagesUrl;
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_createImages);

}

function OnLButtonDown_createImages(Flags, X, Y) {
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);   //把屏幕坐标转化为地理坐标
    var position = cursorCoord.Position;
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_createImages);
    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    var images = sgworld.Creator.CreateImageLabel(position, adrHost + "/Images/skyline/" + _imagesUrl, cLabelStyle, groupID);
}
function ziyouhuaxian() {
    sgworld.Command.Execute(1149, "C:\Program Files (x86)\Skyline\TerraExplorer Pro\Tools\FreeHand\Freehand.html");
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
    //画箭头
    var mouseInfo = sgworld.Window.GetMouseInfo();
    var cursorCoord = sgworld.Window.PixelToWorld(mouseInfo.X, mouseInfo.Y);
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
        }
    }
    if (state == "CreateLine") {
        if (isFirst == true) {
            position1 = cursorCoord.Position;
            cVerticesArray.push(position1.X);
            cVerticesArray.push(position1.Y);
            cVerticesArray.push(position1.Distance);
            cRing = sgworld.Creator.GeometryCreator.CreateLineStringGeometry(cVerticesArray);
            cPolyline = sgworld.Creator.CreatePolyline(cRing, colorLine, 2, groupID);
            cPolyline.LineStyle.Pattern = lineType;
            cPolyline.LineStyle.Width = widthLine;
            isFirst = false;
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
    $("#sbxx").html("经度：" + cursorCoord.Position.X.toFixed(6) + "，纬度：" + cursorCoord.Position.Y.toFixed(6) + "，高程：" + cursorCoord.Position.Altitude.toFixed(2) + "米");

}
//画线
var lineType = 0xFFFFFFFF;
var colorLine = "0xFF0000";
var widthLine = 2;
function d_CreateLine(_lineType, _colorLine, _widthLine) {
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
    isFirst = true;
    state = "CreateLine";
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_d_CreateLine);
}
function OnLButtonUp_d_CreateLine(Flags, X, Y) {
    state = "";
    sgworld.Window.SetInputMode(0);
    sgworld.DetachEvent("OnLButtonUp", OnLButtonUp_d_CreateLine);
    var lenth = 0;
    for (var i = 6; i < cVerticesArray.length; i++) {
        lenth = lenth + sgworld.CoordServices.GetDistance(cVerticesArray[i - 6], cVerticesArray[i - 5], cVerticesArray[i - 3], cVerticesArray[i - 2]);
    }
    cPolyline.Tooltip.Text = "线的长度：" + lenth.toFixed(2) + "米";
    // $("#xx").html("线的长度：" + lenth.toFixed(2) + "米");


}
//画箭头_start
var d_CreateArrowStylte = 1;
function d_CreateArrow(style) {
    d_CreateArrowStylte = style;
    //clearEvent();
    sgworld.Window.SetInputMode(1);
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_d_CreateArrow);
    sgworld.AttachEvent("OnLButtonUp", OnLButtonUp_d_CreateArrow);
}
function OnLButtonDown_d_CreateArrow(Flags, X, Y) {
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);
    position1 = cursorCoord.Position;
    position2 = cursorCoord.Position;
    var color = sgworld.Creator.CreateColor(255, 0, 0);
    color.SetAlpha(0.5);
    arrow = sgworld.Creator.CreateArrow(position2, 0, d_CreateArrowStylte, color, color, groupID);
    arrow.Position.AltitudeType = 2;
    state = "CreateArrow";
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_d_CreateArrow);
}
function OnLButtonUp_d_CreateArrow(Flags, X, Y) {
    state = "";
    sgworld.Window.SetInputMode(0);
    sgworld.DetachEvent("OnLButtonUp", OnLButtonUp_d_CreateArrow);
}
//画面

function d_CreatePolygon() {
    //clearEvent();
    sgworld.Window.SetInputMode(1);
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_d_CreatePolygon);
    sgworld.AttachEvent("OnRButtonDown", OnRButtonDown_d_CreatePolygon);
    cVerticesArray = [];
    cRing = sgworld.Creator.GeometryCreator.CreateLinearRingGeometry(cVerticesArray);
    cPolygonGeometry = sgworld.Creator.GeometryCreator.CreatePolygonGeometry(cRing, null);
    var color = sgworld.Creator.CreateColor(255, 0, 0);
    color.SetAlpha(0.5);
    cPolygon = sgworld.Creator.CreatePolygon(cPolygonGeometry, color, color, 2, groupID);
    cPolygon.Position.AltitudeType = 2;
}
function OnLButtonDown_d_CreatePolygon(Flags, X, Y) {
    state = "CreatePolygon";
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);
    position1 = cursorCoord.Position;
    cVerticesArray.push(position1.X);
    cVerticesArray.push(position1.Y);
    cVerticesArray.push(position1.Distance);
    cRing = sgworld.Creator.GeometryCreator.CreateLinearRingGeometry(cVerticesArray);
    cPolygonGeometry = sgworld.Creator.GeometryCreator.CreatePolygonGeometry(cRing, null);
    cPolygon.Geometry = cPolygonGeometry;
}
function OnRButtonDown_d_CreatePolygon(Flags, X, Y) {
    sgworld.Window.SetInputMode(0);
    state = "";
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_d_CreatePolygon);
    sgworld.DetachEvent("OnRButtonDown", OnRButtonDown_d_CreatePolygon);
    var nameid = sgworld.ProjectTree.FindItem(cPolygon.TreeItem.Name);
    // alert(nameid);
    //sgworld.ProjectTree.EditItem(nameid, 2);
}
function clearEvent() {

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
    sgworld.Window.SetInputMode(1);
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_d_selectMove);
    sgworld.AttachEvent("OnRButtonDown", OnRButtonDown_d_selectMove);
}


function OnLButtonDown_d_selectMove(Flags, X, Y) {
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
    sgworld.DetachEvent("OnRButtonDown", OnRButtonDown_d_selectMove);
    sgworld.ProjectTree.EndEdit();
}
function moveFree() {
    sgworld.Window.SetInputMode(0);
}
function excCommand(m, n) {
    sgworld.Command.Execute(m, n);
}

function d_CreateEffect() {
    // sgworld.AttachEvent("OnLButtonDown", OnLButtonDownEffect);
    var itemID = sgworld.ProjectTree.FindItem("Bonfire ##3859");

    var objID = sgworld.ProjectTree.GetTerraObjectID(itemID);
    //sgworld.Navigate.FlyTo(objID);
    var obj = sgworld.Creator.GetObject(objID);


}
function OnLButtonDownEffect(Flags, X, Y) {
    // sgworld.DetachEvent("OnLButtonDown", OnLButtonDownEffect);

}
function play() {
    sgworld.DateTime.TimeRangeStart = "2015-12-25";
    sgworld.DateTime.TimeRangeStart = "2016-2-4";

}

var globeX;
var globeY;
var imagesHuoInfo = null;
function moveto(x, y, title, jcid) {
    delRealLocTemp();
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
    // var images = sgworld.Creator.CreateImageLabel(position, adrHost + "/Images/skyline/situation/5.png", cLabelStyle);
    //images.Tooltip.Text = "经度:" + x + ",纬度:" + y;
    if (imagesHuoInfo == null) {
        imagesHuoInfo = sgworld.Creator.CreateLabel(position, title, adrHost + "/Images/skyline/situation/5.png", cLabelStyle, groupID);
        imagesHuoInfo.Position.AltitudeType = 2;
    }
    else {
        imagesHuoInfo.Position.X = x;
        imagesHuoInfo.Position.Y = y;
        imagesHuoInfo.Position.AltitudeType = 2;
    }
    imagesHuoInfo.Tooltip.Text = "经度:" + x + ",纬度:" + y;
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
                    html += "<li class='title' style=\"cursor: pointer;\"  onclick='onloadFlie(\"" + adrHost + "/UploadFile/FlaFile/" + datalist[i].PLOTTINGFILENAME + "\")'>第"
                        + (++num) + "次标绘(" + datalist[i].PLOTTINGTIME + ")<a onclick='removeFile(\"" + datalist[i].JC_FIRE_PLOTTINGID + "\")'>删除</a></li>";
                }
                $('#flydiv').html(html);
            }
            else {
                alert("获取Fly文件出错");
            }
        }
    });
    GetFeatureAll(x, y, 1);


}

function createImagesHUOLayer() {
    layer.open({
        type: 1,
        skin: 'layui-layer-rim', //加上边框
        offset: ['380px', '10px'],
        title: false,
        area: ['200px', '70px'], //宽高
        content: "<div>经度:<input id='JD'width='120'/><br/>纬度:<input id='WD' width='120'/><br/><input type='button' width='80' onclick='HDSB()' value='标绘' /></div>",

    });

}
function HDSB() {

    var dXCoord = $("#JD").val();
    var dYCoord = $("#WD").val();
    var dAltitude = 100.0;
    var eAltitudeTypeCode = 0; //AltitudeTypeCode.ATC_TERRAIN_RELATIVE;
    var dYaw = 0.0;
    var dPitch = -90.0;
    var dRoll = 0.0;
    var dDistance = 5000;
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);

    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    var images = sgworld.Creator.CreateImageLabel(position, adrHost + "/Images/skyline/situation/5.jpg", cLabelStyle, groupID);
    images.Tooltip.Text = "经度:" + $("#JD").val() + ",纬度:" + $("#WD").val();

    sgworld.Navigate.FlyTo(images);
    layer.closeAll();

}
function changeTab4() {
    $("#tab1").css("visibility", "hidden");
    $("#tab2").css("visibility", "hidden");
    $("#tab4").css("visibility", "visible");

}
function changeTab1() {
    $("#tab4").css("visibility", "hidden");
    $("#tab2").css("visibility", "hidden");
    $("#tab1").css("visibility", "visible");

}
function changeTab2() {
    $("#tab4").css("visibility", "hidden");
    $("#tab1").css("visibility", "hidden");
    $("#tab2").css("visibility", "visible");
}
function GetFeatureAll(x, y, changeFlag) {

    var layers = $("#easyui-combotree").combotree("getValues");

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
                moveto1(node.x, node.y);
        }
    });
    if (changeFlag == true)
        //changeTab1(4);
        menuOclick('3');
}


//周边分析-查询
//x 经度，Y纬度,dis距离，layerName查询图层名称，name
function qrueyLayer(x, y, dis, layerName, nameStr) {
    var treeData = [];
    var strResult = "";
    var strResult2 = "";
    var ItemID = sgworld.ProjectTree.FindItem(layerName);
    var childrenfather = new Object();
    childrenfather.text = layerName;
    var childrenTreeData = new Array();
    childrenfather.children = childrenTreeData;
    treeData.push(childrenfather);
    $('#tt').tree({
        data: treeData,
        onCheck: function (node, checked) {

        }
    });


    var obj = sgworld.ProjectTree.GetLayer(ItemID);
    var pIFeatureGroup = obj.FeatureGroups(0);
    for (var i = 0; i < pIFeatureGroup.Count; i++) {
        var x1, y1, name;
        var pIFeature = pIFeatureGroup.Item(i);
        for (var j = 0; j < pIFeature.FeatureAttributes.Count; j++) {
            var pIFeatureAttribute = pIFeature.FeatureAttributes.Item(j);
            ///strResult = strResult + "<td>" + pIFeatureAttribute.Name + "*" + pIFeatureAttribute.Value + "</td>";
            if (pIFeatureAttribute.Name == "DISPLAY_X") {
                x1 = pIFeatureAttribute.Value;
            }
            if (pIFeatureAttribute.Name == "DISPLAY_Y") {
                y1 = pIFeatureAttribute.Value;
            }
            if (pIFeatureAttribute.Name == "NAME") {
                name = pIFeatureAttribute.Value;
            }

        }
        //alert(sgworld.CoordServices.GetDistance(x, y, x1, y1));
        if (sgworld.CoordServices.GetDistance(x, y, x1, y1) < dis && (name.indexOf(nameStr) > 0 || name == nameStr || nameStr == '')) {

            strResult2 = strResult2 + "<li class='title' onclick='moveto1(" + x1 + "," + y1 + ")'>" + name + "(" + sgworld.CoordServices.GetDistance(x, y, x1, y1) + "米)</li>";
        }
    }


    $("#zhoubian").html(strResult2);
}

var lableText;
function createTextLable(text) {
    lableText = text;
    sgworld.AttachEvent("OnLButtonDown", OnLButtonDown_createLable);

}

function OnLButtonDown_createLable(Flags, X, Y) {
    sgworld.DetachEvent("OnLButtonDown", OnLButtonDown_createLable);
    var cursorCoord = sgworld.Window.PixelToWorld(X, Y);   //把屏幕坐标转化为地理坐标
    var position = cursorCoord.Position;
    var cLabelStyle = sgworld.Creator.CreateLabelStyle();
    var images = sgworld.Creator.CreateTextLabel(position, lableText, cLabelStyle, groupID);
}

function moveto1(x, y) {
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
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
    sgworld.Navigate.FlyTo(position);
}

var myArray = new Array()
function ShowFolderFileList() {
    var fso, f, f1, fc, s;
    fso = new ActiveXObject("Scripting.FileSystemObject");
    f = fso.GetFolder(skylineSaveAdress);
    fc = new Enumerator(f.files);
    s = "";
    for (; !fc.atEnd() ; fc.moveNext()) {
        var name = String(fc.item());
        var str = name.split(".");
        if (str[str.length - 1] == "fly") {
            var fileArr = str[0].split("\\");
            myArray.push(name);
            s += "<li class='title' onclick='onloadFlie(" + (myArray.length - 1) + ")'>" + fileArr[fileArr.length - 1] + "</li>";
        }
    }
    $("#biaohuiLoadID").append(s);
}
var jcfid = null;

function flySaveAs() {

    var myDate = new Date();
    var mytime = myDate.getFullYear().toString() + (myDate.getMonth() + 1).toString() + myDate.getDate().toString() + myDate.getHours().toString() + myDate.getMinutes().toString() + myDate.getSeconds().toString();
    //var fullPath = sgworld.Project.SaveAs(mytime);
    fullPath = sgworld.ProjectTree.SaveAsFly(mytime, groupID);
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");
    fullPath = fullPath.replace("\\", "/");

    //  alert(fullPath);

    // window.open("/EmergencyHand/UploadFlyIndex?localurl=" + fullPath + "&jcfid=" + jcfid, '保存标绘文件', "height=300, width=520, top=15, left=30,toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no")
    var returnValue = window.showModalDialog("/EmergencyHand/UploadFlyIndex?localurl=" + fullPath + "&jcfid=" + jcfid, "", "dialogWidth=650px;dialogHeight =300px;status=no;scroll=no;center=yes;edge=sunken;")
    //if (returnValue == "True")  //当直接点模态窗体的X关闭的时候不刷新主页面 
    //{
    //    alert(returnValue + "111");
    //}
    //else {
    //    alert(returnValue + "2222");
    //}
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
                        html += "<li class='title' style=\"cursor: pointer;\"  onclick='onloadFlie(\"" + adrHost + "/UploadFile/FlaFile/" + datalist[i].PLOTTINGFILENAME + "\")'>第"
                            + (++num) + "次标绘(" + datalist[i].PLOTTINGTIME + ")<a onclick='removeFile(\"" + datalist[i].JC_FIRE_PLOTTINGID + "\")'>删除</a></li>";
                    }
                    $('#flydiv').html(html);
                }
                else {
                    alert("获取Fly文件出错");
                }
            }
        });
    }

    //$.ajax({
    //    type: 'post',
    //    url: '/EmergencyHand/UploadFileFromLocal',
    //    data: {
    //        localpath: fullPath,
    //        jcfid: jcfid
    //    },
    //    dataType: 'json',
    //    success: function (obj) {
    //        if (obj != null && obj.Success) {
    //            alert(obj.Msg);
    //            //ajax 获取fly文件
    //            $.ajax({
    //                type: 'post',
    //                url: '/EmergencyHand/GetFlyFireList',
    //                data: {
    //                    jcfid: jcfid
    //                },
    //                dataType: 'json',
    //                success: function (obj) {
    //                    if (obj != null && obj.Success) {
    //                        var html = "";
    //                        var datalist = obj.DataList;
    //                        for (var i = 0; i < datalist.length; i++) {
    //                            html += "<li class='title' onclick='onloadFlie(\"" + adrHost + "/UploadFile/FlaFile/" + datalist[i].PLOTTINGFILENAME + "\")'>第" + datalist[i].PLOTTINGTITLE + "次标绘(" + datalist[i].PLOTTINGTIME + ")</li>";
    //                        }
    //                        $('#flydiv').html(html);
    //                    }
    //                    else {
    //                        alert("获取Fly文件出错");
    //                    }
    //                }
    //            });

    //        }
    //        else {
    //            alert(obj.Msg);
    //        }
    //        // $("#biaohuiLoadID").append("<li class='title' onclick='onloadFlie(\"" + fullPath + "\")'>" + mytime + "</li>");
    //    }
    //});

}
function ShowFolderFileListInit() {
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
                                    html += "<li class='title' onclick='onloadFlie(\"" + adrHost + "/UploadFile/FlaFile/" + datalist[i].PLOTTINGFILENAME + "\")'>第"
                                        + (++num) + "次标绘(" + datalist[i].PLOTTINGTIME + ")<a onclick='removeFile(\"" + datalist[i].JC_FIRE_PLOTTINGID + "\")'>删除</a></li>";
                                }
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


function onloadFlie(path) {
    delRealLocTemp();
    var id = sgworld.ProjectTree.FindItem(groupName);
    sgworld.ProjectTree.DeleteItem(id);

    groupID = sgworld.ProjectTree.CreateGroup(groupName, 0);

    sgworld.ProjectTree.LoadFlyLayer(path, groupID);
    //sgworld.Project.Open(path);
}


function DangAnDingWei(x, y, tittle, Src, w, h) {
    var Message = sgworld.Creator.CreatePopupMessage(tittle, "http://www.baidu.com", 0, 0, 400, 400);
    var popup = sgworld.Window.ShowPopup(Message);
    moveto1(x, y);

}

//火情属性预案
function DangYAShow(tittle, Src, w, h) {
    var Message = sgworld.Creator.CreatePopupMessage(tittle, Src, 0, 0, w, h);
    var popup = sgworld.Window.ShowPopup(Message);
}

function qrueyHTML_dw() {
    var Message = sgworld.Creator.CreatePopupMessage('坐标定位', adrHost + "/EmergencyHand/Nr", 0, 0, 298, 133);
    var popup = sgworld.Window.ShowPopup(Message);
}
function qrueyHTML_qr() {
    var Message = sgworld.Creator.CreatePopupMessage('查询', adrHost + "/EmergencyHand/Qurey", 0, 0, 280, 345);
    var popup = sgworld.Window.ShowPopup(Message);
}
function qrueyHTML_tckz() {
    var Message = sgworld.Creator.CreatePopupMessage('图层控制', adrHost + "/EmergencyHand/Tckz", 0, 0, 250, 338);
    var popup = sgworld.Window.ShowPopup(Message);
}
//设定灭火时间
function setFireOverHtml_settimeover() {
    var Message = sgworld.Creator.CreatePopupMessage('灭火时间设定', adrHost + "/EmergencyHand/SetOverFireDateIndex", 180, 80, 300, 300);
    var popup = sgworld.Window.ShowPopup(Message);
}
function test1() {
    var rootID = sgworld.ProjectTree.RootID;
    var node = sgworld.ProjectTree.GetNextItem(rootID, 11);
    while (node != "") {
        var name = sgworld.ProjectTree.GetItemName(node);
        if (name.indexOf("Bonfire") == 0) {
            sgworld.ProjectTree.SetParent(node, groupID);
        }
        if (name.indexOf("New Polyline") == 0) {
            sgworld.ProjectTree.SetParent(node, groupID);
        }
        if (name.indexOf("New Polygon") == 0) {
            sgworld.ProjectTree.SetParent(node, groupID);
        }
        node = sgworld.ProjectTree.GetNextItem(node, 13);
    }

}
//获取实时定位标绘临时组
var realLocTempGroupID = null;
function getRealLocTemp() {
    if (realLocTempGroupID == null) {
        return sgworld.ProjectTree.CreateGroup("RealLocTemp_hqmy", 0);
    }
    else {
        return realLocTempGroupID;
    }
}
//清除实时定位标绘临时组信息
function delRealLocTemp_hqmy() {
    var id = sgworld.ProjectTree.FindItem("RealLocTemp_hqmy");
    sgworld.ProjectTree.DeleteItem(id);
    realLocTempGroupID = null;
}


///地图定位-画点
function movetoMap(x, y, title) {
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
    var position = sgworld.Creator.CreatePosition(dXCoord, dYCoord, dAltitude, eAltitudeTypeCode, dYaw, dPitch, dRoll, dDistance);
    var imagesHuoInfo = sgworld.Creator.CreateLabel(position, title, adrHost + "/Images/skyline/situation/5.png", cLabelStyle, 0);
    imagesHuoInfo.Position.AltitudeType = 2;
    sgworld.Navigate.FlyTo(imagesHuoInfo);
}
//画面--林火蔓延
function createPolygon_hqmy(cVerticesArray) {
    delRealLocTemp_hqmy();
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