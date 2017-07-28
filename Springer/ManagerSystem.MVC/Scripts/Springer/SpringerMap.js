
/// <reference path="../_references.js" />

//经纬度定位
ptPosition = function (x, y) {
    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(12);
    //pointSymbol.setColor(new dojo.Color([0, 255, 0, 0.25]));
    pointSymbol.setColor(new dojo.Color("#FF3300"));
    var geometry = new esri.geometry.Point(x, y);
    var graphic = new esri.Graphic(geometry, pointSymbol);
    map.graphics.add(graphic);
    map.centerAndZoom(geometry, 13);

}

//取出最新经纬度定位（中间表--实时定位）
var Maxindex;
var multipoint;
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
                    Maxindex = data.length;
                    var mpJson = "{";
                    var strLonLat = "[";
                    for (var i = 0; i < data.length; i++) {
                        // ptPosition(data[i].LONGITUDE, data[i].LATITUDE)
                        //ptPositioncom(data[i].LONGITUDE, data[i].LATITUDE);
                        showInfoWindows(data[i]);
                        strLonLat += "[" + parseFloat(data[i].LONGITUDE) + "," + parseFloat(data[i].LATITUDE) + "],";
                    }
                    //多点定位自动缩放
                    if (data.length > 1) {
                        strLonLat = strLonLat.substring(0, strLonLat.length - 1);
                        strLonLat += "]";
                        mpJson += " \"points\"" + ":" + strLonLat + "}";
                        multipoint = new esri.geometry.Multipoint($.parseJSON(mpJson));
                        var extent = multipoint.getExtent().expand(1.6);
                        map.setExtent(extent);
                    }
                }
            }
        }
    });
}

//infowindows
function showInfoWindows(obj) {
    map.infoWindow.hide();
    var attributes = {
        "护林员": obj.HNAME,
        "电话": obj.PHONE,
        "机构": obj.ORGNAME,
        "电量": obj.ELECTRIC,
        "上报时间": obj.SBTIME,
        "HSTATE": obj.HSTATE,
        "ISOUTRAIL": obj.ISOUTRAIL,
        "ORILONGITUDE": parseFloat(obj.ORILONGITUDE).toFixed(3),
        "ORILATITUDE": parseFloat(obj.ORILATITUDE).toFixed(3),
        "经度": parseFloat(obj.LONGITUDE).toFixed(3),
        "纬度": parseFloat(obj.LATITUDE).toFixed(3)
    };

    //var point = new esri.geometry.geographicToWebMercator(new esri.geometry.Point({
    //    "x": obj.LONGITUDE,
    //    "y": obj.LATITUDE,
    //    "spatialReference": {
    //        "wkid": 4326
    //    }
    //}));
    //var point = new esri.geometry.Point(parseFloat(obj.LONGITUDE), parseFloat(obj.LATITUDE));
    //var symbol = new esri.symbol.SimpleMarkerSymbol();
    //symbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    //symbol.setSize(12);
    //symbol.setColor(new dojo.Color("#FF3300"));
    var url = "../Images/icon_zaixian.ico";
    var fontcolor = "#008000";//绿字
    if (obj.HSTATE == "0") {//0 表示离线 1 表示在线
        fontcolor = "#FF3300";//红字
       // url = "../Images/icon_lixian.ico";
        url = "../Images/lixian.png";
    }
    else if (obj.ISOUTRAIL == "1") {//出围 0 表未出围 1 表示出围
        fontcolor = "#DAA520";//橙字
       // url = "../Images/icon_chuwei.ico";
        url = "../Images/chuwei.png";
    }
    else if (obj.HSTATE == "1") {
        fontcolor = "#008000";//绿字 
        //url = "../Images/icon_zaixian.ico"
        url = "../Images/zaixian.png";
    }


    var symbol = new esri.symbol.PictureMarkerSymbol(url, 16, 19);
    var point = new esri.geometry.Point(parseFloat(obj.LONGITUDE), parseFloat(obj.LATITUDE));
    //var graphic = new esri.Graphic(point, symbol);
    var str = obj.HNAME + "[" + obj.PHONE + "]";
    var html = "<p>联系电话：${电话}<br/>经度:${ORILONGITUDE}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${ORILATITUDE}<br/>电量:${电量}<br/>当前上报时间:${上报时间}</p><p>" +
"<a href='javascript:void(0)'  onClick=\"detailInfo('" + obj.USERID + "','" + obj.HNAME + "')\">详细</a>&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"RealLocation('" + obj.PHONE + "','')\">实时轨迹</a>&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"showhisdiv('" + obj.USERID + "','" + str + "')\">历史轨迹</a>&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"GetFRLinePoints('" + obj.USERID + "','')\">巡检线</a>&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"GetFRRailPoints('" + obj.USERID + "')\">责任区</a></p>";
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
    textSymbol.setColor(new dojo.Color(fontcolor));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);
    if (Maxindex == "1") {
        map.centerAndZoom(point, 18);
    }
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

                //for (var i = 0; i < datalsit.length; i++) {
                //    if (i != datalsit.length - 1) {
                //        var str = "时间：" + datalsit[i].SBTIME;
                //        if (i != 0) {
                //            str = datalsit[i].SBTIME.substring(10, datalsit[i].SBTIME.length);
                //        }
                //        ptPositionPerReal(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);//点
                //        drawLine(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), parseFloat(datalsit[i + 1].LONGITUDE), parseFloat(datalsit[i + 1].LATITUDE));//线
                //    }
                //    else {
                //        var str = datalsit[i].SBTIME.substring(10, datalsit[i].SBTIME.length);
                //        oldx = parseFloat(datalsit[i].LONGITUDE);// 经度
                //        oldy = parseFloat(datalsit[i].LATITUDE);//纬度
                //        time = datalsit[i].SBTIME;
                //        ptPositionPerReal(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);
                //    }
                //}
                var i = datalsit.length - 1;
                var str = "时间：" + datalsit[i].SBTIME;
                ptPositionPerReal(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);//点
                time = datalsit[i].SBTIME;
                oldx = parseFloat(datalsit[i].LONGITUDE);// 经度
                oldy = parseFloat(datalsit[i].LATITUDE);//纬度
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
    var str = "";
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
                        str = "时间：" + datalsit[i].SBTIME;
                        //检索两点距离和时间
                        $.ajax({
                            type: "Post",
                            url: "/MapCommon/GetLngLatBetweenTime",
                            data: { lng1: oldx, lat1: oldy, lng2: datalsit[i].LONGITUDE, lat2: datalsit[i].LATITUDE, t1: time, t2: datalsit[i].SBTIME },
                            dataType: "json",
                            async: false,
                            success: function (obj) {
                                if (obj != null && obj.Success) {
                                    str = "历时（" + obj.Msg + "小时)";
                                }
                                else {
                                    ptPositionPerReal(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);
                                    drawLine(oldx, oldy, parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE));
                                    oldx = parseFloat(datalsit[i].LONGITUDE);// 经度
                                    oldy = parseFloat(datalsit[i].LATITUDE);//纬度
                                    time = datalsit[i].SBTIME;
                                }
                            }
                        });
                        //var str = "时间：" + datalsit[i].SBTIME;
                        //ptPositionPerReal(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE), str);
                        //drawLine(oldx, oldy, parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE));
                        //oldx = parseFloat(datalsit[i].LONGITUDE);// 经度
                        //oldy = parseFloat(datalsit[i].LATITUDE);//纬度
                        //time = datalsit[i].SBTIME;
                    }
                }
            }
            //else {
            //    layer.alert('没有实时轨迹数据！', { icon: 5 });
            //}
        }
    });

}

//历史轨迹s
function showhisdiv(uid, uname) {
    $('#txtStartTime').val(getLocalTime(7))
    $('#txtEndTime').val(getLocalTime(0))
    $('#divhisgj').empty();
    $('#gj').show();
    $('#userid').val(uid);
    $('#username').val(uname);
    //play show
    $('#playdiv').show();
    //maptool hide
    $('#maptool').hide();
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
    var uname = $('#username').val();
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
                $('#lsgjtitle').empty();
                $('#divhisgj').html(obj.Msg);
                $('#lsgjtitle').html(uname);
            }
            else {
                layer.alert('执行失败！', { icon: 5 });
            }
        }
    });
}

//历史估计回放
var hisi = 0;//历史轨迹索引
var iTime;
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

var datalist;
function hisgjPlay(uid, time) {
    if (iTime != "") {
        window.clearTimeout(iTime);
    }
    var starttime = $('#timepickerStart').val();//开始时分
    var endtime = $('#timepickerEnd').val();//结束时分
    if ($.trim(starttime) == "") {
        layer.msg('开始时分不可为空！', {
            icon: 5
        });
        $('#timepickerStart').focus();
        return false;
    }
    if ($.trim(endtime) == "") {
        layer.msg('结束时分不可为空！', {
            icon: 5
        });
        $('#timepickerEnd').focus();
        return false;
    }
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetHisLogLatAjax",
        data: { uid: uid, time: time, starttime: starttime, endtime: endtime },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#pausebtn').attr("disabled", false);//暂停设置可用
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


//暂停/开始
var ss = 0;
var bo = true;
function pauseAndStart() {
    if (bo == true) {
        ss = hisi;
        hisi = 100000000;//设置最大值，停止循环
        bo = false;
        drawpausefun();//暂停
        $('#pausebtn').val('开始');
        //$('#overbtn').attr('style', 'display:none');//禁用
        $("#overbtn").css('display', 'none');
    }
    else {
        hisi = ss;
        bo = true;
        drawstartfun();//开始
        $('#pausebtn').val('暂停');
        //$('#overbtn').attr('style', 'display:block');//禁用
        $("#overbtn").css('display', 'inline-block');
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
    //alert(hisi);
    $('#pausebtn').attr("disabled", true);//暂停设置不可用
    if (datalist != null) {
        for (var i = hisi; i < datalist.length; i++) {
            go();
        }
    }
}


//历史轨迹
//var lngst;
//var latst;
//var tst;
//var index;
//var strAndtim = "";
go = function () {
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
                ptPositionWithPicStr(parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE), "start", str);
                // ptPositionPerReal(parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE), str);
                //lngst = datalist[hisi].LONGITUDE;
                //latst = datalist[hisi].LATITUDE;
                //tst = datalist[hisi].SBTIME;
                //index = 0;
            }
            if (hisi != 0) {

                var timespan = GetDateDiff(datalist[hisi - 1].SBTIME, datalist[hisi].SBTIME).toFixed(2);
                if (parseFloat(timespan) > 0.17) {//间隔时间超过10分钟
                    str += "（历时" + timespan + "时）";
                }
                if (hisi == datalist.length - 1) {
                    ptPositionWithPicStr(parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE), "end", str);
                    drawLine(parseFloat(datalist[hisi - 1].LONGITUDE), parseFloat(datalist[hisi - 1].LATITUDE), parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE));
                }
                else {
                    ptPositionPerReal(parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE), str);
                    drawLine(parseFloat(datalist[hisi - 1].LONGITUDE), parseFloat(datalist[hisi - 1].LATITUDE), parseFloat(datalist[hisi].LONGITUDE), parseFloat(datalist[hisi].LATITUDE));
                }
            }
            hisi++;
        }
    }

}

//责任区
function GetFRRailPoints(id) {
    var ring = [];
    $.ajax({
        type: "Post",
        url: "/System/GetFRUserRots",
        data: { id: id, type: '1' },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var data = obj.Data;
                if (data != null) {
                    var datalist = data.DataList;
                    var arr = [];
                    if (datalist.length > 0) {
                        for (var j = 0; j < datalist.length; j++) {
                            var ring = [];
                            var ss = datalist[j];
                            var str = "";
                            for (var i = 0; i < ss.length; i++) {
                                var lng = ss[i].LONGITUDE;
                                var lat = ss[i].LATITUDE;
                                if (!isNaN(lng) && !isNaN(lat)) {
                                    var latlng = new esri.geometry.Point(parseFloat(lng), parseFloat(lat));
                                    ring.push(latlng);
                                    str += lng + "," + lat + "|";
                                }
                                var polygonSymbol = new esri.symbol.SimpleFillSymbol(esri.symbol.SimpleFillSymbol.STYLE_SOLID, new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_DASHDOTDOT, new dojo.Color([255, 0, 0]), 3), new dojo.Color([255, 255, 0, 0.25]));
                                //获取面的点
                                var polygon = new esri.geometry.Polygon(new esri.SpatialReference({ wkid: 4326 }));
                                polygon.addRing(ring);
                            }
                            arr.push(str);
                            var graphic = new esri.Graphic(polygon, polygonSymbol);
                            map.graphics.add(graphic);
                            var point = new esri.geometry.Point(parseFloat(ss[0].LONGITUDE), parseFloat(ss[0].LATITUDE));
                            map.centerAndZoom(point, 14);
                        }
                    }
                }
            } else {
                layer.alert('没有责任区！', {
                    icon: 5
                });
            }
        }
    });
}

//巡检线--获取采集点
function GetFRLinePoints(id) {
    var ring = [];
    $.ajax({
        type: "Post",
        url: "/System/GetFRUserRots",
        data: { id: id, type: '0' },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var data = obj.Data;
                if (data != null) {
                    var datalist = data.DataList;
                    var arr = [];
                    if (datalist.length > 0) {
                        for (var j = 0; j < datalist.length; j++) {
                            var ring = [];
                            var ss = datalist[j];
                            var str = "";
                            for (var i = 0; i < ss.length; i++) {
                                var lng = ss[i].LONGITUDE;
                                var lat = ss[i].LATITUDE;
                                if (!isNaN(lng) && !isNaN(lat)) {
                                    var latlng = new esri.geometry.Point(parseFloat(lng), parseFloat(lat));
                                    ring.push(latlng);
                                    str += lng + "," + lat + "|";
                                }
                                var lineSymbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 5);
                                //获取线的点
                                var polyline = new esri.geometry.Polyline(new esri.SpatialReference({ wkid: 4326 }));
                                polyline.addPath(ring);

                            }
                            arr.push(str);
                            var graphic = new esri.Graphic(polyline, lineSymbol);
                            map.graphics.add(graphic);
                            var point = new esri.geometry.Point(parseFloat(ss[0].LONGITUDE), parseFloat(ss[0].LATITUDE));
                            map.centerAndZoom(point, 14);
                        }
                    }
                }
            } else {
                layer.alert('没有巡检线！', {
                    icon: 5
                });
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