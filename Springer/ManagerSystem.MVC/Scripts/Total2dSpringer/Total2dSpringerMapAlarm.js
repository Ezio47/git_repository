/// <reference path="../_references.js" />
var alarmState;
function getAlarm(p,liclass) {
    map.graphics.clear();
    graphicLayer.clear();
    alarmState = p;
    $('#txtAlarmStartTime').val('');//开始时间
    $('#txtAlarmEndTime').val('');//结束时间
    $('#status').val('0');//未处理状态

    $('#hlyssjg ul li').removeClass("licur");
    $('#' + liclass).addClass("licur");

    //显示报警查询
    $('#divalarmsearch').css("display", "block").siblings(".bottomDiv").css("display", "none");
    $('#divalarmsearch').css("height", '280px');
    $("#txtAlarmStartTime").val(new Date().format("yyyy-MM-dd"));
    $("#txtAlarmEndTime").val(new Date().format("yyyy-MM-dd"));
    getAlarmList(alarmState, $("#txtAlarmStartTime").val(), $("#txtAlarmEndTime").val());

    //检索
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetAlarmAjax",
        data: { txtAlarmStartTime: $("#txtAlarmStartTime").val(), txtAlarmEndTime: $("#txtAlarmEndTime").val(), status: $('#status').val() },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#divalarminfo').empty();
                $('#divalarminfo').html(obj.Msg);

            }
            else {
                layer.alert('检索一键报警信息失败！', { icon: 5 });
            }
        }
    });
}


function getAlarmList(state, starttime, endtime) {
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetAlarmListAjax",
        data: { state: state, starttime: starttime, endtime: endtime },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                for (var i = 0; i < datalist.length; i++) {
                    // ptPosition(datalist[i].LONGITUDE, datalist[i].LATITUDE);
                    showAlarmInfoWindows(datalist[i]);
                    //console.info(datalist[i]);
                }
            }
        }
    });
}

//报警主键ID定位
function getLocaAlarm(alarmid) {
    map.graphics.clear();
    graphicLayer.clear();
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetLocaAlarmAjax",
        data: { alarmid: alarmid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#divalarmsearch').css("height", '50px');//热点收缩
                var data = obj.Data;
                // ptPosition(datalist[i].LONGITUDE, datalist[i].LATITUDE);
                showAlarmInfoWindows(data);
                // console.info(data);
            }
        }
    });
}

//Alarminfowindows
function showAlarmInfoWindows(obj) {
    map.infoWindow.hide();
    var status = "未处理";
    if (obj.MANSTATE == "1") {
        status = "已处理";
    }
    var attributes = {
        "护林员": obj.HName,
        "电话": obj.PHONE,
        "机构": obj.OrgNoName,
        "报警时间": obj.ALARMTIME,
        "状态": status,
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
    var symbol = new esri.symbol.SimpleMarkerSymbol();
    symbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    symbol.setSize(12);
    symbol.setColor(new dojo.Color("#FF3300"));
    //var symbol = new esri.symbol.PictureMarkerSymbol("../Images/person.ico", 16, 19);
    //var geometry = new esri.geometry.Point(obj.LONGITUDE, obj.LATITUDE);
    //var graphic = new esri.Graphic(geometry, symbol);

    var managerstr = "&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)'  onClick=\"alarmManager(" + obj.ALARMID + " )\">管理</a>";
    var removestr = "&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"deleteAlarm('" + obj.ALARMID + "','')\">删除</a>";
    //权限控制
    //003001001	管理  003001002	删除  003001003	编辑
    if (rights.indexOf("030103001") < 0) {
        managerstr = "";
    }
    if (rights.indexOf("030103002") < 0) {
        removestr = "";
    }



    var html = "<p>联系电话：${电话}<br/>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<br/>当前报警时间:${报警时间}<br/>当前状态：${状态}</p><p>" +
"<a href='javascript:void(0)'  onClick=\"alarmSee(" + obj.ALARMID + " )\">查看</a>" + managerstr + removestr + "</p>";
    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("报警人:${护林员} 部门: ${机构}");
    infoTemplate.setContent(html);

    var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);
    graphicLayer.add(graphic);
    //点标签
    font = new esri.symbol.Font("10px", esri.symbol.Font.STYLE_NORMAL, esri.symbol.Font.VARIANT_NORMAL, esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    var str = obj.HName + "  " + obj.ALARMTIME;
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

//一键报警管理信息
function alarmManager(p) {
    getAlarmInfo(p);
    alarmSave(p);
}

//一键报警信息保存
function alarmSave(p) {
    layer.open({
        type: 1,
        title: '报警信息管理',
        area: ['500px', '320px'],
        content: $('#divalarm'),
        shadeClose: false,
        shade: 0,
        btn: ['保存', "取消"],
        yes: function (index) {
            var bo = checkAlarmInfo();
            if (bo == true) {
                $.ajax({
                    type: "Post",
                    url: "/RealSupervision/SaveAlarmInfoAjax",
                    data: { alarmid: p, bjcontent: $('#bjcontent').val(), tbresult: $('#tbresult').val() },
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

//一键报警信息
function getAlarmInfo(p) {
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetAlarmInfoAjax",
        data: { alarmid: p },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var data = obj.Data;
                $('#bjperson').text(data.HName);
                $('#bjtime').text(data.ALARMTIME);
                $('#bjcontent').val(data.ALARMCONTENT);
                $('#tbresult').val(data.MANRESULT);
                //if (data.MANTIME != "" && data.MANTIME != null) {
                //    $('#tbtime').val(data.MANTIME);
                //}
                //if (data.ManUserName != "" && data.ManUserName != null) {
                //    $('#tbperson').val(data.ManUserName);
                //}
            }
            else {
                layer.alert('获取报警信息失败！', { icon: 5 });
            }
        }
    });

}

//一键报警信息Check
function checkAlarmInfo() {
    var a = $('#bjcontent').val();
    var b = $('#tbresult').val();
    if ($.trim(a) == "") {
        layer.alert("报警内容不可为空！", { icon: 2 });
        return false;
    }
    if ($.trim(b) == "") {
        layer.alert("填报结果不可为空！", { icon: 2 });
        return false;
    }
    return true;
}

//一键报警检索条件Check
function checkAlarmSerach() {
    var starttime = $('#txtAlarmStartTime').val();
    var endtime = $('#txtAlarmEndTime').val();
    var c = $('#status').val();

    //if ($.trim(starttime) == "") {
    //    layer.alert("开始时间不可为空！", { icon: 2 });
    //    return false;
    //}
    //if ($.trim(endtime) == "") {
    //    layer.alert("结束时间不可为空！", { icon: 2 });
    //    return false;
    //}
    if ($.trim(c) == "") {
        layer.alert("处理结果不可为空！", { icon: 2 });
        return false;
    }
    var bo = checkEndTime(starttime, endtime);
    if (bo == false) {
        layer.alert('开始时间不能大于结束时间！', { icon: 2 });
        return false;
    }
    return true;

}

//一键报警信息检索列表
function searchAlarmData() {
    $('#divalarmsearch').css("height", '280px');
    AlarmCollapseStatus = 0;
    var bo = checkAlarmSerach();
    if (bo == true) {
        $.ajax({
            type: "Post",
            url: "/RealSupervision/GetAlarmAjax",
            data: { txtAlarmStartTime: $('#txtAlarmStartTime').val(), txtAlarmEndTime: $('#txtAlarmEndTime').val(), status: $('#status').val() },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    $('#divalarminfo').empty();
                    $('#divalarminfo').html(obj.Msg);

                }
                else {
                    layer.alert('检索一键报警信息失败！', { icon: 5 });
                }
            }
        });
    }

}


//删除报警点
function deleteAlarm(alarmid) {
    layer.confirm('是否删除该报警点?', { icon: 3, title: '提示' }, function (index) {
        $.ajax({
            type: "Post",
            url: "/RealSupervision/DelteAlarmAjax",
            data: { alarmid: alarmid },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    layer.msg('报警点删除成功！', { icon: 6, time: 2000 });
                    map.graphics.clear();
                    graphicLayer.clear();
                    getAlarmList(alarmState, '', '');
                    searchAlarmData();
                    //getAlarm('0');
                }
                else {
                    layer.alert('报警点删除失败！', { icon: 5 });
                }
            }
        });
        layer.close(index);
    });

}

//报警查看
function alarmSee(alarmid) {
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetAlarmInfoAjax",
        data: { alarmid: alarmid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var data = obj.Data;
                console.info(data);
                $('#bjnameSee').text(data.HName);
                $('#bjphoneSee').text(data.PHONE);
                $('#alarmtimeSee').text(data.ALARMTIME);
                $('#bjcontentSee').val(data.ALARMCONTENT);
                $('#bjresultSee').val(data.MANRESULT);
                $('#bjaddressSee').val(data.ADDRESS);
                var str = "未处理";
                if (data.MANSTATE == "1") {
                    str = "已处理";
                }
                $('#alarmstateSee').text(str);
                if (data.MANTIME != "" && data.MANTIME != null) {
                    $('#alarmtbtime').text(data.MANTIME);
                }
                if (data.ManUserName != "" && data.ManUserName != null) {
                    $('#alarmperson').val(data.ManUserName);
                }
            }
            else {
                layer.alert('获取报警信息失败！', { icon: 5 });
            }
        }
    });
    layer.open({
        type: 1,
        title: '报警信息查看',
        area: ['650px', '380px'],
        shade: 0,
        content: $('#divalarmsee'),
        shadeClose: false,
        btn: ["取消"],
        cancel: function (index) {
            layer.closeAll();
        }
    });
}