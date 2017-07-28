/// <reference path="_references.js" />


var nav;

dojoConfig = {
    parseOnLoad: true,
    packages: [{
        name: 'bdlib',
        location: this.location.pathname.replace(/\/[^/]+$/, "") + "/js/bdlib"
    }]
};

var map, toolbar;
var list;
var icount;
var ptcount = "33.7971/120.4058, 32.5971/120.2058, 31.2971/120.0058";
require(["esri/map",
 "bdlib/GoogleLayer",
 "esri/layers/FeatureLayer",
 "esri/geometry/Point",
 "esri/symbols/SimpleFillSymbol",
 "esri/symbols/SimpleLineSymbol",
 "dojo/_base/Color", "dojo/parser", "esri/toolbars/draw", "esri/symbols/SimpleMarkerSymbol", "esri/graphic", "esri/dijit/Scalebar", "esri/dijit/OverviewMap",
 "esri/toolbars/navigation", "dijit/registry", "esri/symbols/TextSymbol",
 "dojo/domReady!"],
 function (Map, GoogleLayer, FeatureLayer, Point, SimpleFillSymbol, SimpleLineSymbol, Color, parser, Draw, SimpleMarkerSymbol, Graphic, Scalebar, OverviewMap, Navigation
     , registry, TextSymbol) {
     // parser.parse();

     map = new Map("map", { logo: true, sliderStyle: "large" });
     var basemap = new GoogleLayer();
     map.addLayer(basemap);

     var pt = new Point(117.18182, 31.8448);
     map.centerAndZoom(pt, 13);

     nav = new esri.toolbars.Navigation(map);

     //比例尺
     var scalebar = new Scalebar({
         map: map,
         attachTo: "bottom-center"
     });
     ////鹰眼
     //var overviewMapDijit = new OverviewMap({
     //    map: map,
     //    attachTo: "bottom-right",
     //    color: " #D84E13",
     //    visible: true,
     //    opacity: .40
     //});
     //overviewMapDijit.startup();
     //取最新经纬度定位
     getLonLat();
     ////给地图控件添加视图变化监听事件
     // dojo.connect(map, "onExtentChange", showExtent);
     //给地图控件添加载鼠标移动监听事件
     dojo.connect(map, "onMouseMove", showCoordinates);
     //给地图控件添加载鼠标拖拽监听事件
     dojo.connect(map, "onMouseDrag", showCoordinates);

     //home
     //var home = new HomeButton({
     //    map: map
     //}, "HomeButton");
     //home.startup();


 });

//经纬度定位
ptPosition = function (x, y) {
    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(12);
    //pointSymbol.setColor(new dojo.Color([0, 255, 0, 0.25]));
    pointSymbol.setColor(new dojo.Color("#FF3300"));
    // var geometry = new esri.geometry.Point(110.6058, 33.7971);
    var geometry = new esri.geometry.Point(x, y);
    var graphic = new esri.Graphic(geometry, pointSymbol);
    map.graphics.add(graphic);
    // map.centerAndZoom(geometry, 10);
}

//全局
function full() {
    nav.zoomToFullExtent();
}
//下一视图
function next() {
    nav.zoomToNextExtent();
}
//上一视图
function pre() {
    nav.zoomToPrevExtent();
}
//放大
function zoomin() {
    nav.activate(esri.toolbars.Navigation.ZOOM_IN);
}
//缩小
function zoomout() {
    nav.activate(esri.toolbars.Navigation.ZOOM_OUT);
}
//移动
function pan() {
    nav.activate(esri.toolbars.Navigation.PAN);
}

ptcounntSplit = function () {
    list = ptcount.split("/");
    icount = list.length;
}

//取出最新经纬度
getLonLat = function () {
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetRealAjax",
        data: { orgname: '' },
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        // ptPosition(data[i].LONGITUDE, data[i].LATITUDE)
                        showInfoWindows(data[i]);
                    }
                }
            }
        }
    });
}

//显示地图范围
function showExtent(extent) {
    var s = "地图范围：<br/>XMin:" + extent.xmin + "<br/>YMin:" + extent.ymin + "<br/>XMax:" + extent.xmax + "<br/>YMax:" + extent.ymax;
    dojo.byId("info").innerHTML = s;
}
//显示鼠标坐标
function showCoordinates(event) {
    var mp = event.mapPoint;
    var mp2 = event.screenPoint;
    dojo.byId("info2").innerHTML = "地理坐标：" + mp.x + "," + mp.y + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;屏幕坐标：" + mp2.x + "," + mp2.y;
}


//infowindows
function showInfoWindows(obj) {
    var graphicLayer = new esri.layers.GraphicsLayer();
    map.addLayer(graphicLayer);
    var attributes = {
        "护林员": obj.HNAME,
        "机构": obj.HORGNAME,
        "电量": obj.ELECTRIC,
        "上报时间": ChangeDateFormat(obj.SBTIME),
        "经度": obj.LONGITUDE,
        "纬度": obj.LATITUDE
    };
    var point = esri.geometry.geographicToWebMercator(new esri.geometry.Point({
        "x": obj.LONGITUDE,
        "y": obj.LATITUDE,
        "spatialReference": {
            "wkid": 4326
        }
    }));

    var symbol = new esri.symbol.SimpleMarkerSymbol();
    symbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    symbol.setSize(12);
    symbol.setColor(new dojo.Color("#FF3300"));
    var infoTemplate = new esri.InfoTemplate("护林员:${护林员} ${机构}", "经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<br/>电量:${电量}<br/>当前上报时间:${上报时间}");
    var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);
    graphicLayer.add(graphic);
    //点标签
    font = new esri.symbol.Font("10px", esri.symbol.Font.STYLE_NORMAL, esri.symbol.Font.VARIANT_NORMAL, esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol(obj.HNAME);
    textSymbol.setColor(new dojo.Color("#0036C4"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);
}



