/// <reference path="_references.js" />
dojoConfig = {
    parseOnLoad: true,
    packages: [{
        name: 'bdlib',
        location: this.location.pathname.replace(/\/[^/]+$/, "") + "/js/bdlib"
    }]
};
var nav;
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
     //parser.parse();

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
 

 })


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

//查询
function searchCollect(x) {
    map.graphics.clear();
    $.ajax({
        type: "POST",
        url: "/DataCollect/GetCollectAjax",
        data: { Hid: x },
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        ptPositionPic(data[i].LONGITUDE, data[i].LATITUDE);
                        alert(data[i].LONGITUDE);
                    }
                }
                else {
                    alert('没有查询到数据。');
                }
            }
            else {
                alert('没有数据。');
            }
        }
    });
};
