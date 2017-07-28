/// <reference path="../_references.js" />
var hotState;
function getHot(state) {
    map.graphics.clear();
    graphicLayer.clear();
    hotState = state;
    $('#txtHotStartTime').val('');//开始时间
    $('#txtHotEndTime').val('');//结束时间
    $('#hotstatus').val('0');//未处理状态

    //显示热点查询
    $('#divhot').css("display", "block").siblings(".bottomDiv").css("display", "none");
    $('#divhot').css("height", '280px');


    getHotPoint(hotState);
    //查询
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetHotPointAjax",
        data: { txtHotStartTime: $('#txtHotStartTime').val(), txtHotEndTime: $('#txtHotEndTime').val(), hotstatus: $('#hotstatus').val() },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#divhotinfo').empty();
                $('#divhotinfo').html(obj.Msg);
            }
            else {
                layer.alert('检索热点信息失败！', { icon: 5 });
            }
        }
    });
}

function getHotPoint(state) {
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetHotPontListAjax",
        data: { state: state },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                for (var i = 0; i < datalist.length; i++) {
                    showHotInfoWindows(datalist[i]);
                }
            }
        }
    });
}

//热点主键ID定位
function getLocaHot(hotid) {
    map.graphics.clear();
    graphicLayer.clear();
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetHotPoinInfoAJax",
        data: { hotid: hotid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#divhot').css("height", '50px');//热点收缩
                var data = obj.Data;
                // ptPosition(data.JD, data.WD);
                showHotInfoWindows(data);
                // console.info(data);
            }
        }
    });
}
//热点infowwindows
function showHotInfoWindows(obj) {
    //console.info(obj);
    map.infoWindow.hide();
    var status = "未处理";
    if (obj.MANSTATE == "1") {
        status = "已处理";
    }
    var attributes = {
        "卫星编号": obj.WXBH,
        "热点编号": obj.DQRDBH,
        "子火点": obj.XS,
        "发生地": obj.ZQWZ,
        "预报员": obj.HLY,
        "面积": obj.RSMJ,
        "上报时间": obj.SBSJ,
        "状态": status,
        "经度": parseFloat(obj.JD).toFixed(3),
        "纬度": parseFloat(obj.WD).toFixed(3)
    };
    //var point = new esri.geometry.geographicToWebMercator(new esri.geometry.Point({
    //    "x": parseFloat(obj.JD),
    //    "y": parseFloat(obj.WD),
    //    "spatialReference": {
    //        "wkid": 4326
    //    }
    //}));
    var point = new esri.geometry.Point(parseFloat(obj.JD), parseFloat(obj.WD));
    var symbol = new esri.symbol.SimpleMarkerSymbol();
    symbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    symbol.setSize(12);
    symbol.setColor(new dojo.Color("#FF3300"));
    //var symbol = new esri.symbol.PictureMarkerSymbol("../Images/person.ico", 16, 19);
    //var geometry = new esri.geometry.Point(obj.LONGITUDE, obj.LATITUDE);
    //var graphic = new esri.Graphic(geometry, symbol);

    var managerstr = "&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)'  onClick=\"hotManager(" + obj.HOTSID + " )\">管理</a>";
    var removestr = "&nbsp;&nbsp;&nbsp;<a href='javascript:void(0)' onClick=\"deleteHot('" + obj.HOTSID + "','')\">删除</a>";
    //权限控制
    //003001001	管理  003001002	删除  003001003	编辑
    if (rights.indexOf("001003001") < 0) {
        managerstr = "";
    }
    if (rights.indexOf("001003002") < 0) {
        removestr = "";
    }

    var html = "<p>卫星编号：${卫星编号}<br/>发生地：${发生地}<br/>预报员:${预报员}<br/>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<br/>当前上报时间:${上报时间}<br/>当前状态：${状态}</p><p>" +
"<a href='javascript:void(0)'  onClick=\"hotSee(" + obj.HOTSID + " )\">查看</a>" + managerstr + removestr + "</p>";
    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("热点编号:${热点编号} ");
    infoTemplate.setContent(html);
    var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);
    graphicLayer.add(graphic);

    //点标签
    //var font = new esri.symbol.Font("10px", esri.symbol.Font.STYLE_NORMAL, esri.symbol.Font.VARIANT_NORMAL, esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    var font = new esri.symbol.Font();
    font.setSize("10pt");
    font.setFamily("微软雅黑");
    var str = "热点编号:" + obj.DQRDBH;
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

}

//热点管理
function hotManager(hotid) {
    getHotInfo(hotid);
    hotSave(hotid);
}


//热点信息处理保存
function hotSave(hotid) {
    layer.open({
        type: 1,
        title: '热点处理',
        area: ['550px', '300px'],
        content: $('#divhotmanager'),
        shade:0,
        shadeClose: false,
        btn: ['保存', "取消"],
        yes: function (index) {
            var bo = checkHotInfo();
            if (bo == true) {
                $.ajax({
                    type: "Post",
                    url: "/RealSupervision/SaveHotPointInfoAjax",
                    data: { hotid: hotid, hotresult: $('#hotresult').val() },
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

//热点信息信息
function getHotInfo(hotid) {
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetHotPoinInfoAJax",
        data: { hotid: hotid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var data = obj.Data;
                // console.info(data);
                $('#wxbh').val(data.WXBH);
                $('#sbtime').text(data.SBSJ);
                $('#rdbh').val(data.DQRDBH);
                $('#rdmj').val(data.RSMJ);
                $('#xs').text(data.XS);
                $('#hotresult').val(data.MANRESULT);
                //if (data.MANTIME != "" && data.MANTIME != null) {
                //    $('#tbtime').val(data.MANTIME);
                //}
                //if (data.ManUserName != "" && data.ManUserName != null) {
                //    $('#tbperson').val(data.ManUserName);
                //}
            }
            else {
                layer.alert('获取热点信息失败！', { icon: 5 });
            }
        }
    });

}

//热点check
function checkHotInfo() {
    var txtresult = $('#hotresult').val();
    if ($.trim(txtresult) == "") {
        layer.alert("处理结果不可为空！", { icon: 2 });
        return false;
    }
    return true;
}


//检索热点
function searchHotData() {
    $('#divhot').css("height", '280px');
    HotCollapseStatus = 0;
    var bo = checkHotSerach();
    if (bo == true) {
        $.ajax({
            type: "Post",
            url: "/RealSupervision/GetHotPointAjax",
            data: { txtHotStartTime: $('#txtHotStartTime').val(), txtHotEndTime: $('#txtHotEndTime').val(), hotstatus: $('#hotstatus').val() },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    $('#divhotinfo').empty();
                    $('#divhotinfo').html(obj.Msg);
                }
                else {
                    layer.alert('检索热点信息失败！', { icon: 5 });
                }
            }
        });
    }


}

//检索条件Check
function checkHotSerach() {

    var starttime = $('#txtHotStartTime').val();
    var endtime = $('#txtHotEndTime').val();
    var state = $('#hotstatus').val();

    //if ($.trim(starttime) == "") {
    //    layer.alert("开始时间不可为空！", { icon: 2 });
    //    return false;
    //}
    //if ($.trim(endtime) == "") {
    //    layer.alert("结束时间不可为空！", { icon: 2 });
    //    return false;
    //}
    if ($.trim(state) == "") {
        layer.alert("处理状态不可为空！", { icon: 2 });
        return false;
    }
    var bo = checkEndTime(starttime, endtime);
    if (bo == false) {
        layer.alert('开始时间不能大于结束时间！', { icon: 2 });
        return false;
    }
    return true;

}

//删除热点
function deleteHot(hotid) {
    layer.confirm('是否删除该热点?', { icon: 3, title: '提示' }, function (index) {
        $.ajax({
            type: "Post",
            url: "/RealSupervision/DelteHotAjax",
            data: { hotid: hotid },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    layer.msg('热点删除成功！', { icon: 6, time: 2000 });
                    map.graphics.clear();
                    graphicLayer.clear();
                    getHotPoint(hotState);
                    searchHotData();
                    //getHot('0');
                }
                else {
                    layer.alert('热点删除失败！', { icon: 5 });
                }
            }
        });
        layer.close(index);
    });
}

//查看
function hotSee(hotid) {
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetHotPoinInfoAJax",
        data: { hotid: hotid },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var data = obj.Data;
                //console.info(data);
                $('#wxbhSee').val(data.WXBH);
                $('#sbtimeSee').text(data.SBSJ);
                $('#rdbhSee').val(data.DQRDBH);
                $('#rdmjSee').val(data.RSMJ);
                $('#xsSee').text(data.XS);
                $('#hotaddress').text(data.ZQWZ);
                $('#hothly').text(data.HLY);
                $('#hotdl').text(data.DL);
                $('#hotresultSee').val(data.MANRESULT);
                $('#hotyy').text(data.YY);
                $('#hotfire').text(data.JXHQSJ);
                var str = "未处理";
                if (data.MANSTATE == "1") {
                    str = "已处理";
                }
                $('#hottstate').text(str);

                if (data.MANTIME != "" && data.MANTIME != null) {
                    $('#hottbtime').text(data.MANTIME);
                }
                if (data.ManUserName != "" && data.ManUserName != null) {
                    $('#hotperson').val(data.ManUserName);
                }
                $('#hotimg').attr("src", "/RealSupervision/GetPicture?fjbh=" + data.FJBH);//图片
                $('#imghref').attr("href", "/RealSupervision/GetPicture?fjbh=" + data.FJBH);
            }
            else {
                layer.alert('获取热点信息失败！', { icon: 5 });
            }
        }
    });

    layer.open({
        type: 1,
        title: '热点信息查看',
        area: ['600px', '550px'],
        shade:0,
        content: $('#divhotsee'),
        shadeClose: false,
        btn: ["取消"],
        cancel: function (index) {
            layer.closeAll();
        }
    });

}