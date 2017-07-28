/// <reference path="../_references.js" />
//采集数据js
var titlename;
var pointtype = "";//点类型
var collectType;
var collectState;
function getCollect(type, state, title,liclass) {
    map.graphics.clear();
    graphicLayer.clear();
    collectState = state;
    collectType = type;
    $('#cid').val(type);//隐藏上报类型
    $('#txtCollectStartTime').val('');//开始时间
    $('#txtCollectEndTime').val('');//结束时间
    $('#collectstatus').val('0');//未处理状态

    $('#hlysjcj ul li').removeClass("licur");
    $('#'+liclass).addClass("licur");

    $('#divcollect h4').html(title);//标题
    titlename = title;
    //显示采集查询
    $('#divcollect').css("display", "block").siblings(".bottomDiv").css("display", "none");;
    $('#divcollect').css("height", '280px');
    CollecttCollapseStatus = 0;

    //获取采集类型 0 点 1线 2 面
    $.ajax({
        type: "Post",
        url: "/DataCollect/GetDicDataAjax",
        data: { TYPEID: "4", TID: type },
        dataType: "json",
        async: false,//同步
        success: function (obj) {
            if (obj != null) {
                pointtype = obj.STANDBY1;
            }
        }
    });

    getCollectData(collectType, collectState, '', '');

    //检索
    $.ajax({
        type: "Post",
        url: "/DataCollect/GetCollectDataHtmlAjax",
        data: { state: state, strarttime: '', endtime: '', cid: $('#cid').val() },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#divcollectinfo').empty();
                $('#divcollectinfo').html(obj.Msg);
            }
            else {
                layer.alert('获取采集点失败！', { icon: 5 });
            }
        }
    });
}


function getCollectData(type, state, starttime, endtime) {
    //检索点
    $.ajax({
        type: "Post",
        url: "/DataCollect/GetCollectDataListAjax",
        data: { type: type, state: state, starttime: starttime, endtime: endtime },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                for (var i = 0; i < datalist.length; i++) {
                    // ptPosition(datalist[i].LONGITUDE, datalist[i].LATITUDE);
                    showCollectInfoWindows(datalist[i]);
                    // console.info(datalist[i]);
                }
            }
        }
    });
}

//采集数据类型
function showCollectInfoWindows(obj) {
    map.infoWindow.hide();
    var status = "未处理";
    if (obj.MANSTATE == "1") {
        status = "已处理";
    }

    var attributes = {
        "护林员": obj.HName,
        "电话": obj.Phone,
        "机构": obj.OrgNoName,
        "采集时间": obj.COLLECTTIME,
        "状态": status,
        "类型": titlename,
        "经度": parseFloat(obj.LONGITUDE).toFixed(3),
        "纬度": parseFloat(obj.LATITUDE).toFixed(3)
    };
    //var point = esri.geometry.geographicToWebMercator(new esri.geometry.Point({
    //    "x": obj.LONGITUDE,
    //    "y": obj.LATITUDE,
    //    "spatialReference": {
    //        "wkid": 4326
    //    }
    //}));
    var point = new esri.geometry.Point(parseFloat(obj.LONGITUDE), parseFloat(obj.LATITUDE));
    //  alert(obj.LONGITUDE);
    //var symbol = new esri.symbol.SimpleMarkerSymbol();
    //symbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    //symbol.setSize(12);
    //symbol.setColor(new dojo.Color("#FF3300"));
    //var url = "../Images/builder.ico";
    //if (obj.SYSTYPEVALUE == "2") {
    //    url = "../Images/xiaofang.ico";
    //}
    //if (obj.SYSTYPEVALUE == "3") {
    //    url = "../Images/road.ico";
    //}
    var url = "../Images/Collect/collect" + obj.SYSTYPEVALUE + ".ico";
    var symbol = new esri.symbol.PictureMarkerSymbol(url, 16, 19);
    //var geometry = new esri.geometry.Point(obj.LONGITUDE, obj.LATITUDE);
    //var graphic = new esri.Graphic(geometry, symbol);

    var managerstr = "&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)'  onClick=\"collectManager(" + obj.COLLECTID + ")\">管理</a>";
    var viewstr = "&nbsp;&nbsp;&nbsp;<a id=\"drawmap\" href='javascript:void(0)' onClick=\"drawCollect(" + obj.COLLECTID + "," + pointtype + ")\">绘图</a>";
    var editstr = "&nbsp;&nbsp;&nbsp;<a id=\"drawmap\" href='/DataCollect/EditMapCollect?id=" + obj.COLLECTID + "&type=" + pointtype + "'  target=\"_blank\">编辑</a>";
    var removestr = "&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"deleteCollect('" + obj.COLLECTID + "')\">删除</a>";
    var convertLinestr = "<a href='javascript:void(0)' onClick=\"ConvertType(" + obj.COLLECTID + "," + obj.HID + ",0)\">转巡检线</a>";
    var convertPoloystr = "&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"ConvertType(" + obj.COLLECTID + "," + obj.HID + ",1)\">转责任区</a>";
    //只有道路 才可以转换巡检线和责任区
    if (obj.SYSTYPEVALUE != "3") {
        convertLinestr = "";
        convertPoloystr = "";
    }
    if (pointtype == "0") {
        viewstr = "";//采集点非点集合不显示绘图
    }
    //权限控制
    //003001001	管理  003001002	删除  003001003	编辑
    var tid = $('#cid').val();
    if (tid.length == "2") {
        tid = "3" + tid;
    }
    else if (tid.length == "1") {
        tid = "30" + tid;
    }
    if (rights.indexOf("030" + tid + "001") < 0) {
        managerstr = "";
    }
    if (rights.indexOf("030" + tid + "002") < 0) {
        removestr = "";
    }
    if (rights.indexOf("030" + tid + "003") < 0) {
        editstr = "";
    }
    var html = "<p>数据类型：${类型}<br/>联系电话：${电话}<br/>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<br/>当前采集时间:${采集时间}<br/>当前状态：${状态}</p><p>" +
"<a href='javascript:void(0)' onClick=\"collectSee(" + obj.COLLECTID + ")\">查看</a>" + viewstr + managerstr
+ removestr + editstr + "</p><p>" + convertLinestr + convertPoloystr + "</p>";

    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("部门: ${机构}  采集人:${护林员} ");
    infoTemplate.setContent(html);

    var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);
    graphicLayer.add(graphic);
    //点标签
    font = new esri.symbol.Font("10px", esri.symbol.Font.STYLE_NORMAL, esri.symbol.Font.VARIANT_NORMAL, esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    var str = obj.COLLECTNAME;
    var textSymbol = new esri.symbol.TextSymbol(str);
    textSymbol.setColor(new dojo.Color("#0036C4"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);
    map.centerAndZoom(point, 16);
    //var extent = map.extent;
    //if (!extent.contains(point)) {
    //    map.centerAndZoom(point, 14);
    //}
    //闪烁
    Twinkleshow();
}

var detailcid;
//采集信息管理
function collectManager(cid) {
    $.ajax({
        type: "Post",
        url: "/DataCollect/GetCollectModelInfoAjax",
        data: { cid: cid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var data = obj.Data;
                $('#collectdescribem').val(data.COLLECTNAME);
                $('#collectresult').val(data.MANRESULT);
                $('#collectmanstate').val(data.MANSTATE);
                $('#collecttypeid').val(data.SYSTYPEVALUE);
            }
            else {
                layer.alert('采集信息查看失败！', { icon: 5 });
            }
        }
    });
    layer.open({
        type: 1,
        title: '采集信息管理',
        //offset: ["200px", ''],
        area: ['500px', '300px'],
        content: $('#divcollectmanager'),
        shade: 0,
        shadeClose: false,
        btn: ['保存', "取消"],
        yes: function (index) {
            var bo = checkSaveCollect();
            if (bo == true) {
                $.ajax({
                    type: "Post",
                    url: "/DataCollect/SaveCollectDataAjax",
                    data: { cid: cid, describe: $('#collectdescribem').val(), result: $('#collectresult').val(), typeid: $('#collecttypeid').val(), state: $('#collectmanstate').val() },
                    dataType: "json",
                    success: function (obj) {
                        if (obj != null && obj.Success) {
                            layer.closeAll();
                            layer.msg(obj.Msg, { time: 2000 });
                        }
                        else {
                            layer.alert(obj.Msg, { icon: 5 });
                        }
                    }
                });
            }
        },
        cancel: function (index) {
            layer.closeAll();
        }
        //, btn3: function (index, layero)
        //{
        //    alert(index);
        //    console.info(layero);
        //}
    });
}

function checkSaveCollect() {
    var a = $('#collectresult').val();
    var b = $('#collectdescribem').val();
    if ($.trim(b) == "") {
        layer.alert('采集描述不能为空！', { icon: 2 });
        return false;
    }
    if ($.trim(a) == "") {
        layer.alert('处理结果不能为空！', { icon: 2 });
        return false;
    }

    return true;
}

//采集信息查看
function collectSee(cid) {
    $.ajax({
        type: "Post",
        url: "/DataCollect/GetCollectModelInfoAjax",
        data: { cid: cid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var data = obj.Data;
                console.info(data);
                $('#cjname').text(data.HName);
                $('#cjphone').text(data.Phone);
                if (data.MANSTATE == "1") {
                    $('#collectstate').text("已处理");
                }
                else {
                    $('#collectstate').text("未处理");
                }
                $('#collectdescribe').text(data.COLLECTNAME);


                if (data.MANTIME != "" && data.MANTIME != null) {
                    $('#collecttbtime').text(data.MANTIME);
                }
                if (data.MANUSERID != "" && data.MANUSERID != null) {
                    $('#collectperson').val(data.ManUserName);
                }
            }
            else {
                layer.alert('采集信息查看失败！', { icon: 5 });
            }
        }
    });
    getCollectInfo(cid);//采集点个数
    detailcid = cid;
    getPhotoInfo(cid);
    layer.open({
        type: 1,
        title: '采集信息查看',
        area: ['600px', '400px'],
        content: $('#divcollectsee'),
        shade: 0,
        shadeClose: false,
        btn: ["取消"],
        cancel: function (index) {
            layer.closeAll();
        }
    });

}

//采集信息
function getCollectInfo(cid) {
    $.ajax({
        type: "Post",
        url: "/DataCollect/GetCollectInfoAjax",
        data: { cid: cid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                $('#cjcount').text(datalist.length);
                if (datalist.length == "1") {

                    $('#point').text("经度：" + parseFloat(datalist[0].LONGITUDE).toFixed(3) + "     纬度：" + parseFloat(datalist[0].LATITUDE).toFixed(3));
                    $('#cjxx').css("display", "none");
                }
                else {
                    $('#point').css("display", "none");
                    $('#cjxx').css("display", "block");
                }
            }
            else {
                layer.alert('获取采集信息失败！', { icon: 5 });
            }
        }
    });

}

//获取采集图片信息
function getPhotoInfo(cid) {
    var imgurl = "../Images/photo.png";
    $.ajax({
        type: "Post",
        url: "/DataCollect/GetPhotoInfoAjax",
        data: { cid: cid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                if (datalist.length > 0) {
                    if (datalist[0].UPLOADURL != null && $.trim(datalist[0].UPLOADURL) != "") {
                        imgurl = datalist[0].UPLOADURL;
                    }
                }
                $('#imgcollect').attr('src', imgurl);
            }
            else {
                layer.alert('获取采集图片失败！', { icon: 5 });
            }
        }
    });
}

//采集点详细
function detailinfo() {
    detailhtml();
    layer.open({
        type: 1,
        title: '采集坐标点详细',
        //offset: ["200px", ''],
        area: ['500px', '430px'],
        content: $('#datadetail'),
        shade: 0,
        shadeClose: false,
        btn: ["取消"],
        cancel: function (index) {
            layer.close(index);
        }
    });

}
//详细
function detailhtml() {

    $.ajax({
        type: "Post",
        url: "/DataCollect/GetCollectDetailAjax",
        data: { cid: detailcid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#datadetail').html(obj.Msg);
            }
            else {
                layer.alert('获取采集点失败！', { icon: 5 });
            }
        }
    });
}

//绘图
function drawCollect(cid, type) {
    $.ajax({
        type: "Post",
        url: "/DataCollect/GetCollectInfoAjax",
        data: { cid: cid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                map.graphics.clear();
                graphicLayer.clear();
                var datalist = obj.DataList;
                var ring = [];
                if (type == "2") {
                    for (var i = 0; i < datalist.length; i++) {
                        var latlng = new esri.geometry.Point(parseFloat(datalist[i].LONGITUDE), parseFloat(datalist[i].LATITUDE));
                        ring.push(latlng);
                    }
                    var latlng = new esri.geometry.Point(parseFloat(datalist[0].LONGITUDE), parseFloat(datalist[0].LATITUDE));
                    ring.push(latlng);
                    drawPolygon(ring);
                    return;
                }
                for (var i = 0; i < datalist.length; i++) {
                    if (i != datalist.length - 1) {
                        var str = "时间：" + datalist[i].COLLECTTIME
                        ptPositionPerReal(parseFloat(datalist[i].LONGITUDE), parseFloat(datalist[i].LATITUDE), str);
                        drawLine(parseFloat(datalist[i].LONGITUDE), parseFloat(datalist[i].LATITUDE), parseFloat(datalist[i + 1].LONGITUDE), parseFloat(datalist[i + 1].LATITUDE));
                    }
                    else {
                        var str = "时间：" + datalist[i].COLLECTTIME
                        oldx = parseFloat(datalist[i].LONGITUDE);// 经度
                        oldy = parseFloat(datalist[i].LATITUDE);//纬度
                        ptPositionPerReal(parseFloat(datalist[i].LONGITUDE), parseFloat(datalist[i].LATITUDE), str);
                    }
                }
            }
            else {
                layer.alert('获取采集点失败！', { icon: 5 });
            }
        }
    });

}


//平滑的线
function drawCollectLine(cid) {
    var ring = [];
    $.ajax({
        type: "Post",
        url: "/DataCollect/GetCollectInfoAjax",
        data: { cid: cid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                map.graphics.clear();
                graphicLayer.clear();
                var datalsit = obj.DataList;
                for (var i = 0; i < datalsit.length; i++) {
                    var latlng = new esri.geometry.Point(parseFloat(datalsit[i].LONGITUDE), parseFloat(datalsit[i].LATITUDE));
                    ring.push(latlng);
                }
                var lineSymbol = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SHORTDASHDOTDOT, new dojo.Color([255, 0, 0]), 5);
                //获取线的点
                var polyline = new esri.geometry.Polyline(new esri.SpatialReference({ wkid: 4326 }));
                polyline.addPath(ring);
                var graphic = new esri.Graphic(polyline, lineSymbol);
                map.graphics.add(graphic);
            }
            else {
                layer.alert('获取采集点失败！', { icon: 5 });
            }
        }
    });

}

//采集数据检索
function searchCollectData() {
    $('#divcollect').css("height", '280px');
    CollectCollapseStatus = 0;
    var bo = checkSearchCollect();
    if (bo == true) {
        $.ajax({
            type: "Post",
            url: "/DataCollect/GetCollectDataHtmlAjax",
            data: { state: $('#collectstatus').val(), strarttime: $('#txtCollectStartTime').val(), endtime: $('#txtCollectEndTime').val(), cid: $('#cid').val() },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    $('#divcollectinfo').empty();
                    $('#divcollectinfo').html(obj.Msg);
                }
                else {
                    layer.alert('获取采集点失败！', { icon: 5 });
                }
            }
        });
    }
}

function checkSearchCollect() {
    var state = $('#collectstatus').val();
    var starttime = $('#txtCollectStartTime').val();
    var endtime = $('#txtCollectEndTime').val();
    //if ($.trim(starttime) == "") {
    //    layer.alert("开始时间不可为空！", { icon: 2 });
    //    return false;
    //}
    //if ($.trim(endtime) == "") {
    //    layer.alert("结束时间不可为空！", { icon: 2 });
    //    return false;
    //}
    if ($.trim(state) == "") {
        layer.alert("处理结果不可为空！", { icon: 2 });
        return false;
    }
    if ($.trim(starttime) != "" && $.trim(endtime) != "") {
        var bo = checkEndTime(starttime, endtime);
        if (bo == false) {
            layer.alert('开始时间不能大于结束时间！', { icon: 2 });
            return false;
        }
    }

    return true;
}

//定位
function getLocaCollect(cid) {
    map.graphics.clear();
    graphicLayer.clear();
    $.ajax({
        type: "Post",
        url: "/DataCollect/GetCollectModelDataAjax",
        data: { cid: cid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#divcollect').css("height", '50px');//收缩
                var datalist = obj.DataList;
                if (datalist != null && datalist.length > 0) {
                    // console.info(data[0]);
                    showCollectInfoWindows(datalist[0]);
                }
                else {
                    layer.msg("未查询到值", { time: 2000 });
                }
            }
        }
    });
}

//删除采集点
function deleteCollect(cid) {
    layer.confirm('是否删除该上报点?', { icon: 3, title: '提示' }, function (index) {
        $.ajax({
            type: "Post",
            url: "/DataCollect/DeleteCollectDataAjax",
            data: { cid: cid },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    layer.msg('采集点删除成功！', { icon: 6, time: 2000 });
                    map.graphics.clear();
                    graphicLayer.clear();
                    getCollectData(collectType, collectState, '', '');
                    searchCollectData();
                    //getAlarm('0');
                }
                else {
                    layer.alert('上报点删除失败！', { icon: 5 });
                }
            }
        });
        layer.close(index);
    });
}

///巡检线 责任区转换
function ConvertType(cid, hid, htype) {
    //alert("cid" + cid + "hid" + hid + "htype" + htype);
    var str = "是否转换为";
    if (htype == "0") {
        str += "该护林员巡检线";
    }
    else if (htype == "1") {
        str += "该护林员责任区";
    }
    layer.confirm(str + '?', { icon: 3, title: '提示' }, function (index) {
        $.ajax({
            type: "Post",
            url: "/DataCollect/ConvertHlyLineArea",
            data: { cid: cid, hid: hid, htype: htype },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    layer.msg('采集点转换成功！', { icon: 6, time: 1000 });
                }
                else {
                    layer.alert('采集点转换成功失败！', { icon: 5 });
                }
            }
        });
        layer.close(index);
    });
}

//编辑
function editToolbar() {

}