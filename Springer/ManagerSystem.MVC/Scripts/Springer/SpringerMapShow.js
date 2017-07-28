/// <reference path="../_references.js" />
//定位
function getLocal(id) {
    map.graphics.clear();
    graphicLayer.clear();
    $.ajax({
        type: "Post",
        url: "/MapCommon/GetMapDataInfoAjax",
        data: { jcfid: id },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var data = obj.Data;
                if ($.trim(data.JC_FireData.JD) == "" || $.trim(data.JC_FireData.WD) == "") {
                    layer.msg("经纬度为空,无法定位!", { time: 2000 });
                    return false;
                }
                //ptPosition(parseFloat(data.JC_FireData.JD), parseFloat(data.JC_FireData.WD));
                // console.info(data);
                showMapInfoWindows(data);
            }
        }
    });
}

//地图展示MapShowinfowindows
function showMapInfoWindows(obj) {
    map.infoWindow.hide();
    var attributes = {
        "火情来源": obj.FIRESOURCENAME,
        "发生区域": obj.JC_FireData.ZQWZ,
        "反馈情况": obj.FKNAME,
        "接收时间": obj.JC_FireData.RECEIVETIME,
        "热点类别": obj.HOTETYPENAME,
        "是否连续": obj.LXNAME,
        "情况简报": obj.JC_FireFKData.FIREINTRO,
        "经度": parseFloat(obj.JC_FireFKData.JD).toFixed(3),
        "纬度": parseFloat(obj.JC_FireFKData.WD).toFixed(3)
    };

    var point = new esri.geometry.Point(parseFloat(obj.JC_FireFKData.JD), parseFloat(obj.JC_FireFKData.WD));

    var url = "../Images/Report/mapshow.ico";
    var symbol = new esri.symbol.PictureMarkerSymbol(url, 16, 19);

    var html = "<p>火情来源：${火情来源}<br/>发生区域：${发生区域}<br/>反馈情况：${反馈情况}<br/>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<br/>接收时间:${接收时间}<br/>热点类别：${热点类别}" +
        "<br/>是否连续：${是否连续}<br/>情况简报：${情况简报}</p>";
    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("火情反馈信息");
    infoTemplate.setContent(html);

    var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);
    graphicLayer.add(graphic);
    //点标签
    //font = new esri.symbol.Font("10px", esri.symbol.Font.STYLE_NORMAL, esri.symbol.Font.VARIANT_NORMAL, esri.symbol.Font.WEIGHT_BOLD, "微软雅黑");
    //var str = obj.HName;
    //var textSymbol = new esri.symbol.TextSymbol(str);
    //textSymbol.setColor(new dojo.Color("#0036C4"));
    //textSymbol.setFont(font);
    //textSymbol.setOffset(0, 10);
    //var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    //map.graphics.add(graphicText);
    map.centerAndZoom(point, 12);
    //var extent = map.extent;
    //if (!extent.contains(point)) {
    //    map.centerAndZoom(point, 13);
    //}
    //闪烁
    Twinkleshow();
}
