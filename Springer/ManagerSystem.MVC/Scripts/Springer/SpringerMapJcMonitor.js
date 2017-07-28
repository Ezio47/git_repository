/// <reference path="../_references.js" />
function getJcMonitorLonLat() {
    $('#divsearch').show();
    map.graphics.clear();
    graphicLayer.clear();
    $.ajax({
        type: "Post",
        url: "/EarlyMonitor/GetMonitorInfo",
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                for (var i = 0; i < datalist.length; i++) {
                    // ptPosition(datalist[i].LONGITUDE, datalist[i].LATITUDE);
                    showInfoMonitorWindows(datalist[i]);
                    //  console.info(datalist[i]);
                }
            }
        }
    });
}

//infowindows
function showInfoMonitorWindows(obj) {
    map.infoWindow.hide();
    var attributes = {
        "监控序号": obj.EMID,
        "塔台编码": obj.TTBH,
        "监控名称": obj.EMNAME,
        "IP": obj.IP,
        "高程": obj.GC,
        "机构": obj.ORGNAME,
        "地址": obj.ADDRESS,
        "经度": parseFloat(obj.JD).toFixed(3),
        "纬度": parseFloat(obj.WD).toFixed(3)
    };

    var symbol = new esri.symbol.PictureMarkerSymbol("../Images/JC/jc2.ico", 22, 21);
    var point = new esri.geometry.Point(parseFloat(obj.JD), parseFloat(obj.WD));
    var html = "<p>所属机构:${机构}<br/>IP：${IP}<br/>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<br/>地址:${地址}</p>";
    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("监控名称: ${监控名称}");
    infoTemplate.setContent(html);
    var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);
    graphicLayer.add(graphic);
    //点标签
    // var font = new esri.symbol.Font("10px", new esri.symbol.Font.STYLE_NORMAL, new esri.symbol.Font.VARIANT_NORMAL, new esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    var font = new esri.symbol.Font();
    font.setSize("10pt");
    font.setFamily("微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol(obj.EMNAME);
    textSymbol.setColor(new dojo.Color("#FF3300"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);
    var extent = map.extent;
    if (!extent.contains(point)) {
        map.centerAndZoom(point, 15);
    }
    //闪烁
    //  Twinkleshow();
}

//电子监控检索Check
function monitorTimeCheck() {
    var starttime = $('#txtStartTime').val();//开始时间
    var endtime = $('#txtEndTime').val();//结束时间
    if ($.trim(starttime) == "") {
        $('#txtStartTime').focus();
        layer.alert('开始时间不能为空！', { icon: 2 });
        return false;
    }
    if ($.trim(endtime) == "") {
        $('#txtEndTime').focus();
        layer.alert('结束时间不能为空！', { icon: 2 });
        return false;
    }
    var bo = checkEndTime(starttime, endtime);
    if (bo == false) {
        layer.alert('开始时间不能大于结束时间！', { icon: 2 });
        return false;
    }
    return true;
}

//电子监控检索
function searchMonitorData()
{
    $('#divsearch').css("height", '280px');
    PhotoCollapseStatus = 0;
    var bo = monitorTimeCheck();
    if (bo == true) {
        $.ajax({
            type: "Post",
            url: "/EarlyMonitor/GetMonitorListHtmlAjax",
            data: { txtStartTime: $('#txtStartTime').val(), txtEndTime: $('#txtEndTime').val(), status: $('#status').val() },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    $('#divinfo').empty();
                    $('#divinfo').html(obj.Msg);
                }
                else {
                    layer.alert('检索电子监控信息失败！', { icon: 5 });
                }
            }
        });
    }
}

//删除电子监控
function removeMonitor(imid) {
    layer.confirm('是否删除该电子监控信息?', { icon: 3, title: '提示' }, function (index) {
        $.ajax({
            type: "Post",
            url: "/EarlyMonitor/DeletJcMonitor",
            data: { imid: imid },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    layer.msg('该电子监控信息删除成功！', { icon: 6, time: 2000 });
                    searchMonitorData();
                    map.graphics.clear();
                    graphicLayer.clear();
                }
                else {
                    layer.alert('该电子监控信息删除失败！', { icon: 5 });
                }
            }
        });
        layer.close(index);
    });

}

//转为火情
function convertFire(url, type, id) {
    getAjaxFileInfo(url, type, id);
}