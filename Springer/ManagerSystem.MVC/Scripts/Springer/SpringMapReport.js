/// <reference path="../_references.js" />
var titlename;
var reportType;
var reportState;
function getReport(type, state, title) {
    map.graphics.clear();
    graphicLayer.clear();
    $('#tid').val(type);//隐藏上报类型
    reportState = state;
    reportType = type;
    $('#txtReportStartTime').val('');//开始时间
    $('#txtReportEndTime').val('');//结束时间
    $('#reportstatus').val('0');//未处理状态
    $('#divreport h4').html(title);//标题
    titlename = title;
    //显示上报查询
    $('#divreport').css("display", "block").siblings(".bottomDiv").css("display", "none");;
    $('#divreport').css("height", '280px');
    ReportCollapseStatus = 0;

    getReportData(reportType, reportState, '', '');
    //检索
    $.ajax({
        type: "Post",
        url: "/DataReport/GetReportDataListAjax",
        data: { state: state, starttime: '', endtime: '', tid: type },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#divreportinfo').empty();
                $('#divreportinfo').html(obj.Msg);

            }
            else {
                layer.alert('检索信息失败！', { icon: 5 });
            }
        }
    });

}

//数据上报infowindows
function showReportInfoWindows(obj) {
    map.infoWindow.hide();
    var status = "未处理";
    if (obj.MANSTATE == "1") {
        status = "已处理";
    }
    var attributes = {
        "护林员": obj.HName,
        "类型": titlename,
        "电话": obj.PHONE,
        "机构": obj.OrgNoName,
        "上报时间": obj.REPORTTIME,
        "状态": status,
        "上报描述": obj.COLLECTNAME,
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
    //var symbol = new esri.symbol.SimpleMarkerSymbol();
    //symbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    //symbol.setSize(12);
    //symbol.setColor(new dojo.Color("#FF3300"));
    //var url = "../Images/fire.ico";
    //if (obj.SYSTYPEVALUE == "2") {
    //    url = "../Images/chong.ico";
    //}
    //if (obj.SYSTYPEVALUE == "3") {
    //    url = "../Images/futou.ico";
    //}
    var url = "../Images/Report/report" + obj.SYSTYPEVALUE + ".ico";
    var symbol = new esri.symbol.PictureMarkerSymbol(url, 16, 19);
    //var geometry = new esri.geometry.Point(obj.LONGITUDE, obj.LATITUDE);
    //var graphic = new esri.Graphic(geometry, symbol);

    var managerstr = "&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)'  onClick=\"reportManager(" + obj.REPORTID + " )\">管理</a>";
    var removestr = "&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"deleteReport('" + obj.REPORTID + "','')\">删除</a>";

    //权限控制
    //003001001	管理  003001002	删除  003001003	编辑
    var tid = $('#tid').val();
    if (tid.length == "2") {
        tid = "0" + tid;
    }
    else if (tid.length == "1") {
        tid = "00" + tid;
    }
    if (rights.indexOf("002" + tid + "001") < 0) {
        managerstr = "";
    }
    if (rights.indexOf("002" + tid + "002") < 0) {
        removestr = "";
    }

    var html = "<p>上报类型：${类型}<br/>联系电话：${电话}<br/>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<br/>当前上报时间:${上报时间}<br/>当前状态：${状态}</p><p>" +
"<a href='javascript:void(0)'  onClick=\"reportSee(" + obj.REPORTID + " )\">查看</a>" + managerstr + removestr + "</p>";
    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("部门: ${机构} 上报人:${护林员} ");
    infoTemplate.setContent(html);

    var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);
    graphicLayer.add(graphic);
    //点标签
    font = new esri.symbol.Font("10px", esri.symbol.Font.STYLE_NORMAL, esri.symbol.Font.VARIANT_NORMAL, esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    var str = obj.HName;
    var textSymbol = new esri.symbol.TextSymbol(str);
    textSymbol.setColor(new dojo.Color("#0036C4"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);
    map.centerAndZoom(point, 16);
    //var extent = map.extent;
    //if (!extent.contains(point)) {
    //    map.centerAndZoom(point, 13);
    //}
    //闪烁
    Twinkleshow();
}

//上报数据管理
function reportManager(reportid) {
    getReportInfo(reportid);
    reportSave(reportid);
}

//上报信息
function getReportInfo(reportid) {
    var fileurl = "../Images/photo.png";
    $.ajax({
        type: "Post",
        url: "/DataReport/GetReportDataInfoAjax",
        data: { reportid: reportid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var data = obj.Data;
                $('#hname').text(data.HName);
                $('#phone').text(data.PHONE);
                if (data.UPLOADURL != null && $.trim(data.UPLOADURL) != "") {
                    fileurl = data.UPLOADURL;
                }
                $('#address').val(data.ADDRESS);
                $('#reportdescribe').val(data.COLLECTNAME);
                $('#reportresult').val(data.MANRESULT);
                //if (data.MANTIME != "" && data.MANTIME != null) {
                //    $('#tbtime').val(data.MANTIME);
                //}
                //if (data.ManUserName != "" && data.ManUserName != null) {
                //    $('#tbperson').val(data.ManUserName);
                //}
                $('#fileManagerid').html('');
                $('#reportmanalayerphotos').html('');
                if (data.UPLOADTYPE == "1") {
                    $('#reportmanalayerphotos').html("<img style=\"height:100px\"  src=" + fileurl + " />");
                }
                else if (data.UPLOADTYPE == "2") {
                    var html = " <video width=\"350\" height=\"200\" controls autoplay> <source src=" + fileurl + "  type=\"video/mp4\"></video>";
                    $('#fileManagerid').html(html);
                }
                else if (data.UPLOADTYPE == "3") {
                    var html = "  <audio  style=\"width:230px;\"  controls> <source src=" + fileurl + "  type=\"audio/mpeg\"></audio>";
                    $('#fileManagerid').html(html);

                }
            }
            else {
                layer.alert('获取报警信息失败！', { icon: 5 });
            }
        }
    });


}

//上报信息保存
function reportSave(reportid) {
    layer.open({
        type: 1,
        title: '上报信息管理',
        //offset: ["200px", ''],
        area: ['600px', '450px'],
        content: $('#divreportmanager'),
        shade: 0,
        shadeClose: false,
        btn: ['保存', "取消"],
        yes: function (index) {
            var bo = checkReportInfo();
            if (bo == true) {
                $.ajax({
                    type: "Post",
                    url: "/DataReport/SaveReportDataAjax",
                    data: { reportid: reportid, address: $('#address').val(), describe: $('#reportdescribe').val(), result: $('#reportresult').val() },
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
    });
}

//
function checkReportInfo() {
    var a = $('#address').val();
    var b = $('#reportresult').val();
    if ($.trim(a) == "") {
        layer.alert("地址不可为空！", { icon: 2 });
        return false;
    }
    if ($.trim(b) == "") {
        layer.alert("处理结果不可为空！", { icon: 2 });
        return false;
    }
    return true;
}

function checkReportSerach() {
    var state = $('#reportstatus').val();
    var starttime = $('#txtReportStartTime').val();
    var endtime = $('#txtReportEndTime').val();
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

//查询上报数据
function searchReportData() {
    $('#divreport').css("height", '280px');
    ReportCollapseStatus = 0;
    var bo = checkReportSerach();
    if (bo == true) {
        $.ajax({
            type: "Post",
            url: "/DataReport/GetReportDataListAjax",
            data: { state: $('#reportstatus').val(), starttime: $('#txtReportStartTime').val(), endtime: $('#txtReportEndTime').val(), tid: $('#tid').val() },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    $('#divreportinfo').empty();
                    $('#divreportinfo').html(obj.Msg);

                }
                else {
                    layer.alert('检索信息失败！', { icon: 5 });
                }
            }
        });
    }
}

//上报定位
function getLocaReport(reportid) {
    map.graphics.clear();
    graphicLayer.clear();
    $.ajax({
        type: "Post",
        url: "/DataReport/GetReportDataInfoAjax",
        data: { reportid: reportid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#divreport').css("height", '50px');//收缩
                var data = obj.Data;
                // ptPositionPic(datalist[i].LONGITUDE, datalist[i].LATITUDE, "");
                showReportInfoWindows(data);
                //console.info(data);
                // console.info(datalist[i]);
            }
        }
    });
}

function getReportData(type, state, starttime, endtime) {
    $.ajax({
        type: "Post",
        url: "/DataReport/GetReportDataAjax",
        data: { type: type, state: state, starttime: starttime, endtime: endtime },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList; 
                for (var i = 0; i < datalist.length; i++) {
                    if (datalist[i].LONGITUD != null && datalist[i].LATITUDE != null) {
                        showReportInfoWindows(datalist[i]);
                    }
                }
            }
        }
    });
}

//删除上报数据
function deleteReport(reportid) {

    layer.confirm('是否删除该上报点?', { icon: 3, title: '提示' }, function (index) {
        $.ajax({
            type: "Post",
            url: "/DataReport/DeleteReportDataAjax",
            data: { reportid: reportid },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    layer.msg('上报点删除成功！', { icon: 6, time: 2000 });

                    map.graphics.clear();
                    graphicLayer.clear();

                    getReportData(reportType, reportState, '', '');

                    searchReportData();
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

//数据上报查看
function reportSee(rid) {
    var fileurl = "../Images/photo.png";
    $.ajax({
        type: "Post",
        url: "/DataReport/GetReportDataInfoAjax",
        data: { reportid: rid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var data = obj.Data;
                // console.info(data);
                $('#sbname').text(data.HName);
                $('#sbphone').text(data.PHONE);
                var status = "";
                if (data.MANSTATE == "1") {
                    status = "已处理";
                }
                else {
                    status = "未处理";
                }
                $('#reporttstate').text(status);
                $('#sbaddress').val(data.ADDRESS);
                $('#sbdescribe').val(data.COLLECTNAME);
                $('#sbresult').val(data.MANRESULT);
                $('#reportbtime').text(data.SBTIME);
                $('#reporperson').val(data.ManUserName);
                if (data.UPLOADURL == "" || data.UPLOADURL != null) {
                    fileurl = data.UPLOADURL;
                }
                $('#fileReportSeeid').html('');
                $('#reportlayerphotos').html('');
                if (data.UPLOADTYPE == "1") {
                    $('#reportlayerphotos').html("<img style=\"height:100px\"  src=" + fileurl + " />");
                }
                else if (data.UPLOADTYPE == "2") {
                    var html = " <video width=\"350\" height=\"200\" controls autoplay> <source src=" + fileurl + "  type=\"video/mp4\"></video>";
                    $('#fileReportSeeid').html(html);
                }
                else if (data.UPLOADTYPE == "3") {
                    var html = "  <audio  style=\"width:230px;\"  controls> <source src=" + fileurl + "  type=\"audio/mpeg\"></audio>";
                    $('#fileReportSeeid').html(html);

                }



            }
        }
    });

    layer.open({
        type: 1,
        title: '上报信息查看',
        area: ['600px', '500px'],
        content: $('#divreportsee'),
        shade: 0,
        shadeClose: false,
        btn: ["取消"],
        cancel: function (index) {
            layer.closeAll();
        }
    });

}