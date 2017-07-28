/// <reference path="../_references.js" />
function getJcCameraLonLat() {
    $('#divsearch').show();
    map.graphics.clear();
    graphicLayer.clear();
    $.ajax({
        type: "Post",
        url: "/EarlyMonitor/GetCamareaInfo",
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                for (var i = 0; i < datalist.length; i++) {
                    // ptPosition(datalist[i].LONGITUDE, datalist[i].LATITUDE);
                    showInfoWindows(datalist[i]);
                    //  console.info(datalist[i]);
                }
            }

        }
    });

}

//infowindows
function showInfoWindows(obj) {
    map.infoWindow.hide();
    var attributes = {
        "序号": obj.INFRAREDCAMERAID,
        "设备名称": obj.INFRAREDCAMERANAME,
        "设备电话": obj.PHONE,
        "机构": obj.ORGNAME,
        "地址": obj.ADDRESS,
        "经度": parseFloat(obj.JD).toFixed(3),
        "纬度": parseFloat(obj.WD).toFixed(3)
    };

    var symbol = new esri.symbol.PictureMarkerSymbol("../Images/JC/jc1.ico", 20, 19);
    var point = new esri.geometry.Point(parseFloat(obj.JD), parseFloat(obj.WD));
    var html = "<p>所属机构:${机构}<br/>设备电话：${设备电话}<br/>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<br/>地址:${地址}</p>";
    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("设备名称: ${设备名称}");
    infoTemplate.setContent(html);
    var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);
    graphicLayer.add(graphic);
    //点标签
    // var font = new esri.symbol.Font("10px", new esri.symbol.Font.STYLE_NORMAL, new esri.symbol.Font.VARIANT_NORMAL, new esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    var font = new esri.symbol.Font();
    font.setSize("10pt");
    font.setFamily("微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol(obj.INFRAREDCAMERANAME);
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

//图片检索Check
function photoTimeCheck() {
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

//图片检索方法
function searchPhotoData() {
    $('#divsearch').css("height", '280px');
    PhotoCollapseStatus = 0;
    var bo = photoTimeCheck();
    if (bo == true) {
        $.ajax({
            type: "Post",
            url: "/EarlyMonitor/GetPhotoListHtmlAjax",
            data: { txtStartTime: $('#txtStartTime').val(), txtEndTime: $('#txtEndTime').val(), status: $('#status').val() },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    $('#divinfo').empty();
                    $('#divinfo').html(obj.Msg);
                }
                else {
                    layer.alert('检索图片检索信息失败！', { icon: 5 });
                }
            }
        });
    }
}

//删除图片
function removePhoto(smid) {
    layer.confirm('是否删除该图片?', { icon: 3, title: '提示' }, function (index) {
        $.ajax({
            type: "Post",
            url: "/EarlyMonitor/DeletCameraPhoto",
            data: { smid: smid },
            dataType: "json",
            success: function (obj) {
                if (obj != null && obj.Success) {
                    layer.msg('该图片删除成功！', { icon: 6, time: 2000 });
                    searchPhotoData();
                    map.graphics.clear();
                    graphicLayer.clear();
                }
                else {
                    layer.alert('该图片删除失败！', { icon: 5 });
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