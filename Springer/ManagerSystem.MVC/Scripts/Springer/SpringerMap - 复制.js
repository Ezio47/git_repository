
/// <reference path="../_references.js" />

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
    map.centerAndZoom(geometry, 13);

}




//取出最新经纬度定位（中间表--实时定位）
function getLonLat(uidstr) {
    map.graphics.clear();
    graphicLayer.clear();
    $(".bottomDiv").css("display", "none");
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetRealAjax",
        data: { uidstr: uidstr },
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.length > 0) {
                    for (var i = 0; i < data.length; i++) {
                        // ptPosition(data[i].LONGITUDE, data[i].LATITUDE)
                        showInfoWindows(data[i]);
                        //alert(data[i].LONGITUDE + "====" + data[i].LATITUDE);
                        //ptPositioncom(data[i].LONGITUDE, data[i].LATITUDE);
                    }
                }
            }

        }
    });
}

//infowindows
function showInfoWindows(obj) {
    var attributes = {
        "护林员": obj.HNAME,
        "电话": obj.PHONE,
        "机构": obj.ORGNAME,
        "电量": obj.ELECTRIC,
        "上报时间": obj.SBTIME,
        "经度": parseFloat(obj.LONGITUDE).toFixed(3),
        "纬度": parseFloat(obj.LATITUDE).toFixed(3)
    };

    var point = new esri.geometry.geographicToWebMercator(new esri.geometry.Point({
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
    //var symbol = new esri.symbol.PictureMarkerSymbol("../Images/person.ico", 16, 19);
    //var geometry = new esri.geometry.Point(obj.LONGITUDE, obj.LATITUDE);
    //var graphic = new esri.Graphic(geometry, symbol);
    var html = "<p>联系电话：${电话}<br/>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<br/>电量:${电量}<br/>当前上报时间:${上报时间}</p><p>" +
"<a href='javascript:void(0)'  onClick=\"detailInfo('" + obj.USERID + "','" + obj.HNAME + "')\">详细</a>&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"RealLocation('" + obj.PHONE + "','')\">实时轨迹</a>&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"showhisdiv('" + obj.USERID + "')\">历史轨迹</a>&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"GetFRLinePoints('" + obj.USERID + "','')\">巡检线</a>&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"GetFRRailPoints('" + obj.USERID + "')\">管护区</a></p>";
    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("护林员:${护林员} 部门: ${机构}");
    infoTemplate.setContent(html);
    var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);
    graphicLayer.add(graphic);

    //点标签
    // var font = new esri.symbol.Font("10px", new esri.symbol.Font.STYLE_NORMAL, new esri.symbol.Font.VARIANT_NORMAL, new esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    var font = new esri.symbol.Font();
    font.setSize("10pt");
    font.setFamily("微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol(obj.HNAME);
    textSymbol.setColor(new dojo.Color("#0036C4"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);

    //var extent = map.extent;
    //if (!extent.contains(graphic.geometry)) {
    map.centerAndZoom(graphic.geometry, 15);
    //}
    //闪烁
    Twinkleshow();
}




var oldx, oldy, time;
//实时轨迹
function RealLocation(phone, uid) {
    map.graphics.clear();
    graphicLayer.clear();
    $('#btnConOver').show();//结束按钮显示
    $('#btnCon').hide();
    hisi = 100000000;//设置历史轨迹回放索引最大值，解决轨迹回放过程中实时定位时继续轨迹回放的功能
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetRealDataAjax",
        data: {
            phone: phone, time: '', uid: uid
        },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                map.graphics.clear();
                graphicLayer.clear();
                var datalsit = obj.DataList;

                for (var i = 0; i < datalsit.length; i++) {
                    if (i != datalsit.length - 1) {
                        var str = "时间：" + datalsit[i].SBTIME;
                        if (i != 0) {
                            str = datalsit[i].SBTIME.substring(10, datalsit[i].SBTIME.length);
                        }
                        ptPositionPerReal(datalsit[i].LONGITUDE, datalsit[i].LATITUDE, str);//点
                        drawLine(datalsit[i].LONGITUDE, datalsit[i].LATITUDE, datalsit[i + 1].LONGITUDE, datalsit[i + 1].LATITUDE);//线
                    }
                    else {
                        var str = datalsit[i].SBTIME.substring(10, datalsit[i].SBTIME.length);
                        oldx = datalsit[i].LONGITUDE;// 经度
                        oldy = datalsit[i].LATITUDE;//纬度
                        time = datalsit[i].SBTIME;
                        ptPositionPerReal(datalsit[i].LONGITUDE, datalsit[i].LATITUDE, str);
                    }
                }
                intervalMethod = self.setInterval("RealLocationInterval('" + phone + "','" + uid + "')", 5000);

            }
            //else {
            //    layer.alert('没有实时轨迹数据！', { icon: 5 });
            //}
        }
    });

}
//定时执行实时轨迹
function RealLocationInterval(phone, uid) {
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetRealDataAjax",
        data: {
            phone: phone, time: time, uid: uid
        },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalsit = obj.DataList;
                //console.info(datalsit);
                if (datalsit.length > 0) {
                    for (var i = 0; i < datalsit.length; i++) {
                        var str = "时间：" + datalsit[i].SBTIME;
                        ptPositionPerReal(datalsit[i].LONGITUDE, datalsit[i].LATITUDE, str);
                        drawLine(oldx, oldy, datalsit[i].LONGITUDE, datalsit[i].LATITUDE);
                        oldx = datalsit[i].LONGITUDE;// 经度
                        oldy = datalsit[i].LATITUDE;//纬度
                        time = datalsit[i].SBTIME;
                    }
                }
            }
            //else {
            //    layer.alert('没有实时轨迹数据！', { icon: 5 });
            //}
        }
    });

}

//历史轨迹
function showhisdiv(uid) {
    $('#txtStartTime').val(getLocalTime(7))
    $('#txtEndTime').val(getLocalTime(0))
    $('#divhisgj').empty();
    $('#gj').show();
    $('#userid').val(uid);
    searchHisData();
}

//查询历史轨迹记录列表
function searchHisData() {
    // $("#divhisgj").slideDown();
    $('#gj').css("height", '280px');
    CollapseStatus = 0;
    var starttime = $('#txtStartTime').val();
    var endtime = $('#txtEndTime').val();
    var uid = $('#userid').val();
    if ($.trim(starttime) == "" || $.trim(endtime) == "") {
        layer.alert('时间不能为空！', {
            icon: 2
        });
        return false;
    }
    var bo = checkEndTime(starttime, endtime);
    if (bo == false) {
        layer.alert('开始时间不能大于结束时间！', { icon: 2 });
        return false;
    }
    if ($.trim(uid) == "") {
        layer.alert('护林员ID传参失败！', {
            icon: 2
        });
        return false;
    }
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetHisGJAjax",
        data: { uid: uid, starttime: starttime, endtime: endtime },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#divhisgj').empty();
                $('#divhisgj').html(obj.Msg);
            }
            else {
                layer.alert('执行失败！', { icon: 5 });
            }
        }
    });
}

//历史估计回放
var datalist;
function hisgjPlay(uid, time) {
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetHisLogLatAjax",
        data: { uid: uid, time: time },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                map.graphics.clear();
                graphicLayer.clear();
                datalist = obj.DataList;
                hisi = 0;
                gogps();
                CollapseStatus = 0;//收缩
                hideInfoWin();
            }
            else {
                layer.alert('没有实时轨迹数据！', {
                    icon: 5
                });
            }
        }
    });
}

var hisi = 0;//历史轨迹索引
gogps = function () {
    go();
    if (hisi < datalist.length) {
        window.setTimeout("gogps()", 2000);
    } else {
        window.clearTimeout("gogps()");
        hisi = 0;
        datalist = null;
    }
}

//历史轨迹
go = function () {
    var str = "";
    if (hisi == 0) {
        str = "时间：" + datalist[hisi].SBTIME + "  电量:" + datalist[hisi].ELECTRIC;
    }
    else {
        str = datalist[hisi].SBTIME.substring(10, datalist[hisi].SBTIME.length) + "  电量:" + datalist[hisi].ELECTRIC;;
    }
    ptPositionPerReal(datalist[hisi].LONGITUDE, datalist[hisi].LATITUDE, str);
    if (hisi != 0) {
        ptPositionPerReal(datalist[hisi].LONGITUDE, datalist[hisi].LATITUDE, str);
        drawLine(datalist[hisi - 1].LONGITUDE, datalist[hisi - 1].LATITUDE, datalist[hisi].LONGITUDE, datalist[hisi].LATITUDE);
    }
    hisi++;
}

//管护区
function GetFRRailPoints(id) {
    var ring = [];
    $.ajax({
        type: "Post",
        url: "/System/GetFRUserRots",
        data: { id: id, type: '1' },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                for (var i = 0; i < datalist.length; i++) {
                    var latlng = new esri.geometry.Point(datalist[i].LONGITUDE, datalist[i].LATITUDE);
                    ring.push(latlng);
                }
                var polygonSymbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_DASHDOTDOT, new dojo.Color([255, 0, 0]), 3), new dojo.Color([255, 255, 0, 0.25]));
                //获取面的点
                var polygon = new esri.geometry.Polygon(new esri.SpatialReference({ wkid: 4326 }));
                polygon.addRing(ring);
                var graphic = new esri.Graphic(polygon, polygonSymbol);
                map.graphics.add(graphic);

            }
        }
    });
}

//巡检线--获取采集点
function GetFRLinePoints(id)
{
    var ring=[];
    $.ajax({
        type: "Post",
        url: "/System/GetFRUserRots",
        data: { id:id,type: '0' },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                for (var i = 0; i < datalist.length; i++) {
                    var latlng=new esri.geometry.Point(datalist[i].LONGITUDE, datalist[i].LATITUDE);
                    ring.push(latlng);
                }
                var lineSymbol  = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID,new dojo.Color([255,0,0]),5);
                //获取线的点
                var polyline = new esri.geometry.Polyline(new esri.SpatialReference({wkid:4326}));
                polyline.addPath(ring);
                var graphic = new esri.Graphic(polyline,lineSymbol);
                map.graphics.add(graphic);
            }
        }
    });
}

//弃用
goq = function () {
    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(10);
    pointSymbol.setColor(new dojo.Color("#FF3300"));
    var x = datalist[hisi].LONGITUDE;
    var y = datalist[hisi].LATITUDE;
    var geometry = new esri.geometry.Point(x, y);

    var graphic = new esri.Graphic(geometry, pointSymbol);
    map.graphics.add(graphic);

    var extent = map.extent;
    if (!extent.contains(geometry)) {
        map.centerAt(geometry);
    }
    if (hisi > 0) {
        var oldx = datalist[hisi - 1].LONGITUDE;
        var oldy = datalist[hisi - 1].LATITUDE;
        var polyline = new esri.geometry.Polyline({
            "paths": [
              [
                [oldx, oldy],
                [x, y]
              ]
            ]
        });
        var polylineSymbol = new esri.symbol.SimpleLineSymbol();
        map.graphics.add(new esri.Graphic(polyline, polylineSymbol));
    }
    hisi++;
}