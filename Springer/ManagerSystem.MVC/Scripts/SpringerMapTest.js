/// <reference path="_references.js" />

var arrayObj = new Array();
var arrstrlon = "";
var arrstrlat = "";

dojoConfig = {
    parseOnLoad: true,
    packages: [{
        name: 'bdlib',
        location: this.location.pathname.replace(/\/[^/]+$/, "") + "/js/bdlib"
    }]
};

var infowin, colse, title, content;
var width = 300, height = 230;
var closeInfoWin = function (evt) {
    infowin = document.getElementById("infowin");
    infowin.style.display = "none";
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
 "dojo/dom-construct", "esri/dijit/InfoWindow", "esri/layers/GraphicsLayer",
 "dojo/domReady!"],
 function (Map, GoogleLayer, FeatureLayer, Point, SimpleFillSymbol, SimpleLineSymbol, Color, parser, Draw, SimpleMarkerSymbol, Graphic, Scalebar, OverviewMap,
     domConstruct, InfoWindow, GraphicsLayer) {
     //parser.parse();

     map = new Map("map", { logo: true });
     var basemap = new GoogleLayer();
     map.addLayer(basemap);

     var pt = new Point(117.27, 31.84);
     map.centerAndZoom(pt, 6);

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
     // ptcounntSplit();

     //if (icount > 0) {
     //    for (var i = 0; i < icount; i++) {


     //        // ptPosition(oldx,oldy);
     //    }
     //}
     getLonLat();
     ////给地图控件添加视图变化监听事件
     // dojo.connect(map, "onExtentChange", showExtent);
     //给地图控件添加载鼠标移动监听事件
     dojo.connect(map, "onMouseMove", showCoordinates);
     //给地图控件添加载鼠标拖拽监听事件
     dojo.connect(map, "onMouseDrag", showCoordinates);


     //infowindow
     infowin = document.getElementById("infowin");
     colse = document.getElementById("close");
     title = document.getElementById("title");
     content = document.getElementById("content");
     //鼠标单击
     // featurelayercity.on("click", leftClick);
     // dojo.connect(map, "click", leftClick);
     //showInfoWindows();
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


ptcounntSplit = function () {
    list = ptcount.split("/");
    icount = list.length;
}

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
                        //ptPosition(data[i].LONGITUDE, data[i].LATITUDE)
                        // arrayObj.push(data[i]);
                        showInfoWindows(data[i]);
                    }
                }
            }
        }
    });
}

function showInfoWindows(obj) {
    var graphicLayer = new esri.layers.GraphicsLayer();
    map.addLayer(graphicLayer);
    var attributes = {
        "护林员": obj.HNAME,
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
    var infoTemplate = new esri.InfoTemplate("护林员:${护林员}", "当前所在的经度:${经度},纬度:${纬度},电量:${电量},当前上报时间:${上报时间}");
    var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);
    graphicLayer.add(graphic);
}

//时间转换
function ChangeDateFormat(val) {
    if (val != null) {
        var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
        //月份为0-11，所以+1，月份小于10时补个0
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        var currentHour = date.getHours() < 10 ? "0" + date.getHours() : date.getHours();
        var currentMin = date.getMinutes() < 10 ? "0" + date.getMinutes() : date.getMinutes();
        var currentSec = date.getSeconds() < 10 ? "0" + date.getSeconds() : date.getSeconds();
        return date.getFullYear() + "年" + month + "月" + currentDate + "日  " + currentHour + ":" + currentMin + ":" + currentSec;
    }
    return "";
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

function leftClick(evt) {
    infowin.style.display = "none";

    var strtitle = "城市名称"
    var strcontent = "****是一座美丽的城市<br><br>****是一座好看的城市<br><br>****是一座富饶的城市<br><br>****是一座漂亮的城市";

    infowin.style.left = (evt.clientX - width / 2) + "px";
    infowin.style.top = (evt.clientY - height - 50) + "px";
    infowin.style.position = "absolute";
    infowin.style.width = width + "px";
    infowin.style.height = height + "px";
    infowin.style.display = "block";

    title.innerHTML = strtitle;
    content.innerHTML = strcontent;
}

