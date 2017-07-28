
//经纬度转换，经纬度转数字
function jsw1tojsw2(obj) {
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

Date.prototype.format = function (format) {
    var date = {
        "M+": this.getMonth() + 1,
        "d+": this.getDate(),
        "h+": this.getHours(),
        "m+": this.getMinutes(),
        "s+": this.getSeconds(),
        "q+": Math.floor((this.getMonth() + 3) / 3),
        "S+": this.getMilliseconds()
    };
    if (/(y+)/i.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + '').substr(4 - RegExp.$1.length));
    }
    for (var k in date) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1
                   ? date[k] : ("00" + date[k]).substr(("" + date[k]).length));
        }
    }
    return format;
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

//显示地图范围
function showExtent(extent) {
    var s = "地图范围：<br/>XMin:" + extent.xmin + "<br/>YMin:" + extent.ymin + "<br/>XMax:" + extent.xmax + "<br/>YMax:" + extent.ymax;
    dojo.byId("info").innerHTML = s;
}
//显示鼠标坐标
function showCoordinates(event) {
    // console.info(event);
    var mp = event.mapPoint;
    var mp2 = event.screenPoint;
    // var normalizedVal = new esri.geometry.xyToLngLat(mp.x, mp.y);//google地图转换坐标
    dojo.byId("info2").innerHTML = "经度：" + parseFloat(mp.x).toFixed(3) + " &nbsp;&nbsp;&nbsp;纬度：" + parseFloat(mp.y).toFixed(3);
    // dojo.byId("info2").innerHTML = "经度：" + parseFloat(normalizedVal[0]).toFixed(3) + " &nbsp;&nbsp;&nbsp;纬度：" + parseFloat(normalizedVal[1]).toFixed(3);
    //  console.info(normalizedVal);
    //dojo.byId("info2").innerHTML = "地理坐标：" + parseFloat(normalizedVal[0]).toFixed(3) + "," + parseFloat(normalizedVal[1]).toFixed(3) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;屏幕坐标：" +mp2.x + "," + mp2.y;
}
//鼠标坐标(是否转换 type 为1 表示需要转换坐标。Type 为1 表示需要将投影坐标转成地理坐标)
function showChangeCoordinates(event, type, lonLatType) {
    // console.info(event);
    var mp = event.mapPoint;
    var mp2 = event.screenPoint;
    if (type == "1") {
        var normalizedVal = new esri.geometry.xyToLngLat(mp.x, mp.y);//google地图转换坐标
        if (lonLatType == "1") {
            dojo.byId("info2").innerHTML = "经度：" + LonLatDuFenMiao("'" + normalizedVal[0] + "'") + " &nbsp;&nbsp;&nbsp;纬度：" + LonLatDuFenMiao("'" + normalizedVal[1] + "'");
        }
        else {
            dojo.byId("info2").innerHTML = "经度：" + parseFloat(normalizedVal[0]).toFixed(3) + " &nbsp;&nbsp;&nbsp;纬度：" + parseFloat(normalizedVal[1]).toFixed(3);
        }
    }
    else {
        if (lonLatType == "1") {
            dojo.byId("info2").innerHTML = "经度：" + LonLatDuFenMiao("'" + mp.x + "'") + " &nbsp;&nbsp;&nbsp;纬度：" + LonLatDuFenMiao("'" + mp.y + "'");
        }
        else {
            dojo.byId("info2").innerHTML = "经度：" + parseFloat(mp.x).toFixed(3) + " &nbsp;&nbsp;&nbsp;纬度：" + parseFloat(mp.y).toFixed(3);
        }
    }
    //dojo.byId("info2").innerHTML = "地理坐标：" + parseFloat(normalizedVal[0]).toFixed(3) + "," + parseFloat(normalizedVal[1]).toFixed(3) + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;屏幕坐标：" +mp2.x + "," + mp2.y;
}

//经纬度===度分秒
function LonLatDuFenMiao(db) {
    var lonarry = db.split(".");
    //度
    var degree = lonarry[0].replace("'", "");
    lonarry[1] = "0." + lonarry[1];
    //分
    var min = parseInt(parseFloat(lonarry[1]) * 60);
    var ys = "0." + (parseFloat(lonarry[1]) * 60).toLocaleString().split(".")[1];
    //秒
    var sec = parseInt(parseFloat(ys) * 60);
    var degrees = "" + degree + "°" + min + "′" + sec + "″";
    return degrees;
}

//经纬度定位
ptPositionPic = function (x, y, url) {

    if ($.trim(url) == "") {
        url = "../Images/builder.ico";
    }
    var pointSymbol = new esri.symbol.PictureMarkerSymbol(url, 20, 23);
    var geometry = new esri.geometry.Point(x, y);
    var graphic = new esri.Graphic(geometry, pointSymbol);
    //var infoTemplate = new esri.InfoTemplate();
    //infoTemplate.setTitle("hello world");
    //infoTemplate.setContent("hello world说说撒 hello world");
    //graphic.setInfoTemplate(infoTemplate);
    map.graphics.add(graphic);
    map.centerAndZoom(geometry, 13);

}

//清除上一次的画图内容
function clearMap() {
    //清除上一次的画图内容
    map.graphics.clear();

}

//经纬度定位
ptPositionTitle = function (x, y, title) {
    //清除上一次的画图内容
    map.graphics.clear();
    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(10);
    pointSymbol.setColor(new dojo.Color("#FF3300"))
    var geometry = new esri.geometry.Point(x, y);
    var graphic = new esri.Graphic(geometry, pointSymbol);
    map.graphics.add(graphic);
    map.centerAndZoom(geometry, 15);

    var font = new esri.symbol.Font();
    font.setSize("10pt");
    font.setFamily("微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol(title);
    textSymbol.setColor(new dojo.Color("yellow"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);

    //闪烁
    Twinkleshow();
}

//经纬度定位
ptPosition = function (x, y) {
    //清除上一次的画图内容
    map.graphics.clear();
    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(10);
    pointSymbol.setColor(new dojo.Color("#FF3300"))
    var geometry = new esri.geometry.Point(x, y);
    var graphic = new esri.Graphic(geometry, pointSymbol);
    map.graphics.add(graphic);
    map.centerAndZoom(geometry, 12);

    //闪烁
    Twinkleshow();
}

//经纬度定位等级
ptPostionAndLevel = function (x, y, level) {
    //清除上一次的画图内容
    map.graphics.clear();
    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(10);
    pointSymbol.setColor(new dojo.Color("#FF3300"))
    var geometry = new esri.geometry.Point(x, y);
    var graphic = new esri.Graphic(geometry, pointSymbol);
    map.graphics.add(graphic);
    map.centerAndZoom(geometry, level);

    //闪烁
    Twinkleshow();
}
//经纬度定位 test
ptPositioncom = function (x, y) {

    // var graphicLayer = new esri.layers.GraphicsLayer();
    //  map.addLayer(graphicLayer);

    var attributes = {
        "经度": x,
        "纬度": y
    };
    //var point = new esri.geometry.geographicToWebMercator(new esri.geometry.Point({
    //    "x":x,
    //    "y": y,
    //    "spatialReference": {
    //        "wkid": 4326
    //    }
    //}));
    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(10);
    //pointSymbol.setColor(new dojo.Color([0, 255, 0, 0.25]));
    pointSymbol.setColor(new dojo.Color("#FF3300"));
    // var geometry = new esri.geometry.Point(110.6058, 33.7971);
    var point = new esri.geometry.Point(x, y);
    // var graphic = new esri.Graphic(geometry, pointSymbol);

    //var infoTemplate = new esri.InfoTemplate();
    //infoTemplate.setTitle("hello world");
    //infoTemplate.setContent("hello world说说撒 hello world");
    //graphic.setInfoTemplate(infoTemplate);
    var html = "<p>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<p>" +
   " <a href='javascript:void(0)' onClick=\"deletePoint()\">ddssds</a></p>";
    var ss = "<p >ewew</p>";
    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("围栏点管理");
    infoTemplate.setContent(html);
    //graphic.setInfoTemplate(infoTemplate);

    //map.graphics.add(graphic);
    var graphic = new esri.Graphic(point, pointSymbol, attributes, infoTemplate);
    graphicLayer.add(graphic);


    //点标签
    // var font = new esri.symbol.Font("10px", new esri.symbol.Font.STYLE_NORMAL, new esri.symbol.Font.VARIANT_NORMAL, new esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    var font = new esri.symbol.Font();
    font.setSize("10pt");
    font.setFamily("微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol("hello");
    textSymbol.setColor(new dojo.Color("#0036C4"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);

    var extent = map.extent;
    if (!extent.contains(graphic.geometry)) {
        map.centerAndZoom(graphic.geometry, 10);
    }

}


//经纬度定位图片
ptPositionWithPicStr = function (x, y, imagename, str) {
    var url = "../Images/" + imagename + ".ico";
    var pointSymbol = new esri.symbol.PictureMarkerSymbol(url, 20, 25);
    var geometry = new esri.geometry.Point(x, y);
    var graphic = new esri.Graphic(geometry, pointSymbol);
    map.graphics.add(graphic);
    //点标签
    var font = new esri.symbol.Font("10px", esri.symbol.Font.STYLE_NORMAL, esri.symbol.Font.VARIANT_NORMAL, esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol(str);
    textSymbol.setColor(new dojo.Color("#FFA500"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);


    map.centerAndZoom(geometry, 18);
}


//实时轨迹
ptPositionPerReal = function (x, y, str) {

    var symbol = new esri.symbol.SimpleMarkerSymbol();
    symbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    symbol.setSize(12);
    symbol.setColor(new dojo.Color("#FF3300"));
    var geometry = new esri.geometry.Point(x, y);
    var graphic = new esri.Graphic(geometry, symbol);
    map.graphics.add(graphic);

    //点标签
    var font = new esri.symbol.Font("10px", esri.symbol.Font.STYLE_NORMAL, esri.symbol.Font.VARIANT_NORMAL, esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol(str);
    textSymbol.setColor(new dojo.Color("#FFA500"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);
    map.centerAndZoom(geometry, 18);
    var extent = map.extent;
    if (!extent.contains(geometry)) {
        map.centerAndZoom(geometry, 18);
    }
}


//划线
drawLine = function (oldx, oldy, x, y) {
    var polyline = new esri.geometry.Polyline({
        "paths": [
          [
            [oldx, oldy],
            [x, y]
          ]
        ]
    });

    var polylineSymbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SHORTDASHDOTDOT, new dojo.Color([255,
0, 0, 0.75]), 4);
    map.graphics.add(new esri.Graphic(polyline, polylineSymbol));
    //var pt = new esri.geometry.Point(parseFloat(oldx), parseFloat(oldy));
    //map.centerAndZoom(pt, 14);
}

drawPolygon = function (ring) {

    var polygonSymbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 3), new dojo.Color([255, 255, 0, 0.25]));
    //var polygonSymbol = new esri.symbol.SimpleFillSymbol();
    //获取面的点
    var polygon = new esri.geometry.Polygon(new esri.SpatialReference({ wkid: 4326 }));
    polygon.addRing(ring);
    var graphic = new esri.Graphic(polygon, polygonSymbol);
    map.graphics.add(graphic);

}



//图片展示
function imgshow(id) {
    //调用示例
    layer.ready(function () { //为了layer.ext.js加载完毕再执行
        layer.photos({
            area: ['500px', '500px'],
            shift: 1,
            //shade: [0.8, '#393D49'],
            // time: 3000,
            closeBtn: 2,
            photos: '#' + id
        });
    });
}

//闪烁
var second = 0;//闪烁次数
Twinkleshow = function () {
    showhide();
    if (second < 2) {
        window.setTimeout("Twinkleshow()", 500);
    } else {
        window.clearTimeout("Twinkleshow()");
        second = 0;
    }
}
function showhide() {
    graphicLayer.hide();
    setTimeout(function () {
        graphicLayer.show();
    }, 200);
    second++;
}