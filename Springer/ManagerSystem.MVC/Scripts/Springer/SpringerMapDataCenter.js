/// <reference path="../_references.js" />

//清除上一次的画图内容
function clearMap() {
    //清除上一次的画图内容
    map.graphics.clear();
    graphicLayer.clear()

}


//定位(设施)
function getFacilityLocal(id, flag) {
    $.ajax({
        type: "Post",
        url: "/DataCenter/GetFACILITYModel",
        data: { id: id, flag: flag },
        dataType: "json",
        success: function (obj) {
            //console.info(obj);
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                if (datalist.length > 0) {
                    for (var i = 0; i < datalist.length; i++) {
                        var jd = datalist[i].JD;
                        var wd = datalist[i].WD;
                        if ($.trim(jd) == "" || $.trim(wd) == "") {
                            clearMap();
                            layer.msg("当前位置缺失经纬度，无法定位");
                            return false;
                        }
                        else {
                            if (flag == "1") {
                                clearMap();
                            }
                            //定位 地图展示
                            showMapInfoWindows(datalist[i]);
                        }
                    }
                }
                else {
                    clearMap();
                    layer.msg("Ajax未查询到数据");
                }

            }
            else {
                clearMap();
                layer.msg("Ajax查询参数错误");
            }
        }
    });
}

//定位(装备)
function getEquipLocal(id, flag) {
    $.ajax({
        type: "Post",
        url: "/DataCenter/GetEQUIPModel",
        data: { id: id, flag: flag },
        dataType: "json",
        success: function (obj) {
            //console.info(obj);
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                if (datalist.length > 0) {
                    for (var i = 0; i < datalist.length; i++) {
                        var jd = datalist[i].JD;
                        var wd = datalist[i].WD;
                        if ($.trim(jd) == "" || $.trim(wd) == "") {
                            clearMap();
                            layer.msg("当前位置缺失经纬度，无法定位");
                            return false;
                        }
                        else {
                            if (flag == "1") {
                                clearMap();
                            }
                            //定位 地图展示
                            showMapInfoWindows(datalist[i]);
                        }
                    }
                }
                else {
                    clearMap();
                    layer.msg("未查询到数据");
                }

            }
            else {
                clearMap();
                layer.msg("Ajax查询参数错误");
            }
        }
    });
}

//定位(装备)
function getResourceLocal(id, flag) {
    $.ajax({
        type: "Post",
        url: "/DataCenter/GetRESOURCEModel",
        data: { id: id, flag: flag },
        dataType: "json",
        success: function (obj) {
            //console.info(obj);
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                if (datalist.length > 0) {
                    for (var i = 0; i < datalist.length; i++) {
                        var jd = datalist[i].JD;
                        var wd = datalist[i].WD;
                        if ($.trim(jd) == "" || $.trim(wd) == "") {
                            clearMap();
                            layer.msg("当前位置缺失经纬度，无法定位");
                            return false;
                        }
                        else {
                            if (flag == "1") {
                                clearMap();
                            }
                            //定位 地图展示
                            showMapInfoWindows(datalist[i]);
                        }
                    }
                }
                else {
                    clearMap();
                    layer.msg("Ajax未查询到数据");
                }

            }
            else {
                clearMap();
                layer.msg("Ajax查询参数错误");
            }
        }
    });
}


//定位(专业队伍)
function getArmyLocal(id, flag, type) {
    clearMap();
    $.ajax({
        type: "Post",
        url: "/DataCenter/GetPROTEAMModel",
        data: { id: id, flag: flag, type: type },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                var datalist = obj.DataList;
                if (datalist.length > 0) {
                    if (type == "1" || type == "2") {
                        var jd = datalist[0].JD;
                        var wd = datalist[0].WD;
                        if ($.trim(jd) == "" || $.trim(wd) == "") {
                            clearMap();
                            layer.msg("当前位置缺失经纬度，无法定位");
                            return false;
                        }
                        ptPerson(datalist[0]);
                        showPersonInfo(id, flag, type);
                    }
                    else {
                        if (type == "4") {
                            showPersonInfo(id, flag, type);
                        }
                        for (var i = 0; i < datalist.length; i++) {
                            var jd = datalist[i].JD;
                            var wd = datalist[i].WD;
                            if ($.trim(jd) == "" || $.trim(wd) == "") {
                                // clearMap();
                                layer.msg("当前存在位置缺失经纬度，无法定位");
                                return false;
                            }
                            else {
                                if (flag == "1") {
                                    clearMap();
                                }
                                if (type == "4") {
                                    ptEquip(datalist[i], datalist[i].WATCHNAME);
                                }
                                else {
                                    //定位 地图展示
                                    showMapInfoTeamWindows(datalist[i]);
                                }
                            }
                        }
                    }
                }
                else {
                    clearMap();
                    layer.msg("未查询到数据");
                }

            }
            else {
                clearMap();
                layer.msg("Ajax查询参数错误");
            }
        }
    });
}

//地图展示MapShowinfowindows
function showMapInfoWindows(obj) {
    map.infoWindow.hide();
    var attributes = {
        "设施名称": obj.FACINAME,
        "备注": obj.MARK,
        "经度": parseFloat(obj.JD).toFixed(3),
        "纬度": parseFloat(obj.WD).toFixed(3)
    };
    var html = "<p>设施名称：${设施名称}<br/>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<br/>备注:${备注}</p>";
    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("基本信息");
    infoTemplate.setContent(html);

    //var point = new esri.geometry.Point(parseFloat(obj.JC_FireData.JD), parseFloat(obj.JC_FireData.WD));
    //var url = "../Images/Report/mapshow.ico";
    //var symbol = new esri.symbol.PictureMarkerSymbol(url, 16, 19);
    //var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);

    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(10);
    pointSymbol.setColor(new dojo.Color("#FF3300"))
    var point = new esri.geometry.Point(parseFloat(obj.JD), parseFloat(obj.WD));
    var graphic = new esri.Graphic(point, pointSymbol, attributes, infoTemplate);
    graphicLayer.add(graphic);
    //text
    var font = new esri.symbol.Font();
    font.setSize("10pt");
    font.setFamily("微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol(obj.FACINAME);
    textSymbol.setColor(new dojo.Color("yellow"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);

    map.centerAndZoom(point, 14);
    //var extent = map.extent;
    //if (!extent.contains(point)) {
    //    map.centerAndZoom(point, 13);
    //}
    //闪烁
    Twinkleshow();
}


//个人定位
function ptPerson(obj) {

    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(10);
    pointSymbol.setColor(new dojo.Color("#FF3300"));
    var point = new esri.geometry.Point(parseFloat(obj.JD), parseFloat(obj.WD));
    var graphic = new esri.Graphic(point, pointSymbol);
    map.graphics.add(graphic);
    map.centerAndZoom(point, 12);
}

//设施定位
function ptEquip(obj, title) {

    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(10);
    pointSymbol.setColor(new dojo.Color("#FF3300"));
    var point = new esri.geometry.Point(parseFloat(obj.JD), parseFloat(obj.WD));
    var graphic = new esri.Graphic(point, pointSymbol);
    map.graphics.add(graphic);

    //text
    var font = new esri.symbol.Font();
    font.setSize("10pt");
    font.setFamily("微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol(title);
    textSymbol.setColor(new dojo.Color("yellow"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);

    map.centerAndZoom(point, 12);

}

//地图展示专业队伍MapShowinfowindows
function showMapInfoTeamWindows(obj) {
    map.infoWindow.hide();
    var attributes = {
        "队伍名称": obj.PROTEAMNAME,
        "城市": obj.CAPACITY,
        "领导人": obj.LEADER,
        "联系方式": obj.LINKWAY,
        "经度": parseFloat(obj.JD).toFixed(3),
        "纬度": parseFloat(obj.WD).toFixed(3)
    };
    var html = "<p>队伍名称：${队伍名称}<br/>城市:${城市}<br/>领导人:${领导人}&nbsp;&nbsp;联系方式：${联系方式}<br/>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}</p>";
    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("基本信息");
    infoTemplate.setContent(html);

    //var point = new esri.geometry.Point(parseFloat(obj.JC_FireData.JD), parseFloat(obj.JC_FireData.WD));
    //var url = "../Images/Report/mapshow.ico";
    //var symbol = new esri.symbol.PictureMarkerSymbol(url, 16, 19);
    //var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);

    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(10);
    pointSymbol.setColor(new dojo.Color("#FF3300"))
    var point = new esri.geometry.Point(parseFloat(obj.JD), parseFloat(obj.WD));
    var graphic = new esri.Graphic(point, pointSymbol, attributes, infoTemplate);
    graphicLayer.add(graphic);
    //text
    var font = new esri.symbol.Font();
    font.setSize("10pt");
    font.setFamily("微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol(obj.PROTEAMNAME);
    textSymbol.setColor(new dojo.Color("yellow"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);

    map.centerAndZoom(point, 14);
    //var extent = map.extent;
    //if (!extent.contains(point)) {
    //    map.centerAndZoom(point, 13);
    //}
    //闪烁
    Twinkleshow();
}

//个人信息展示
function showPersonInfo(id, flag, type) {
    $.ajax({
        type: "post",
        url: "/DataCenter/GetPersons",
        data: { id: id, flag: flag, type: type },
        dataType: "json",
        success: function (data) {
            if (data != null) {
                if (data.Success) {
                    layer.open({
                        type: 1,
                        title: '个人信息',
                        area: ['550px', '380px'], //宽高
                        content: data.Msg //注意，如果str是object，那么需要字符拼接。
                    });
                } else {
                    layer.msg(data.Msg, { icon: 2 });
                }
            }
            else {
                layer.msg('出错', { icon: 2 });
            }
        }

    });

}

//档案  火险预案等级
function showYAFireLevelInfo(id, type) {
    $.ajax({
        type: "post",
        url: "/DataCenter/GetYALevel",
        data: { id: id, type: type },
        dataType: "json",
        success: function (data) {
            if (data != null && data.Success) {
                var datalist = data.DataList;
                if (datalist.length > 0) {
                    for (var i = 0; i < datalist.length; i++) {
                        console.info(datalist[i]);
                        var jd = datalist[i].JD;
                        var wd = datalist[i].WD;
                        if ($.trim(jd) == "" || $.trim(wd) == "") {
                            layer.msg("当前位置缺失经纬度，无法定位");
                            return false;
                        }
                        showMapInfoFireLevelWindows(datalist[i]);
                    }
                }
                else {
                    clearMap();
                    layer.msg("未查询到数据");
                }
            }
        }

    });
}

//火险预案等级信息showMapInfoFireLevelWindows
function showMapInfoFireLevelWindows(obj) {
    map.infoWindow.hide();
    var attributes = {
        "火情名称": obj.FIRENAME,
        "发生时间": obj.FIRETIME,
        "结束时间": obj.FIREENDTIME,
        "过火面积": obj.GHMJ,
        "过火林地面积": obj.GHLDMJ,
        "受害森林面积": obj.SHSLMJ,
        "人员伤": obj.RYS,
        "人员亡": obj.RYW,
        "是否敏感时段": obj.MGSDNAME,
        "是否重点区域": obj.ZDQYNAME,
        "损失级别": obj.SSJB,
        "火灾级别": obj.FIRELEVEL,
        "是否已灭": obj.ISOUTFIRENAME,
        "经度": parseFloat(obj.JD).toFixed(3),
        "纬度": parseFloat(obj.WD).toFixed(3)
    };
    var html = "<p>经度:${经度}&nbsp;&nbsp;&nbsp;&nbsp;纬度:${纬度}<br/>发生时间：${发生时间}&nbsp;&nbsp;结束时间:${结束时间}<br/>过火面积:${过火面积}平方千米&nbsp;&nbsp;过火林地面积：${过火林地面积}平方千米<br/>"
    + "是否已灭:${是否已灭}&nbsp;&nbsp;是否敏感时段:${是否敏感时段}&nbsp;&nbsp;是否重点区域：${是否重点区域}<br/>损失级别:${损失级别}&nbsp;&nbsp;&nbsp;&nbsp;火灾级别:${火灾级别}</p>";
    var infoTemplate = new esri.InfoTemplate();
    infoTemplate.setTitle("名称：${火情名称}");
    infoTemplate.setContent(html);
    map.infoWindow.resize(350, 568);
    //var point = new esri.geometry.Point(parseFloat(obj.JC_FireData.JD), parseFloat(obj.JC_FireData.WD));
    //var url = "../Images/Report/mapshow.ico";
    //var symbol = new esri.symbol.PictureMarkerSymbol(url, 16, 19);
    //var graphic = new esri.Graphic(point, symbol, attributes, infoTemplate);

    var pointSymbol = new esri.symbol.SimpleMarkerSymbol();
    pointSymbol.setOutline = new esri.symbol.SimpleLineSymbol(esri.symbol.SimpleLineSymbol.STYLE_SOLID, new dojo.Color([255, 0, 0]), 1);
    pointSymbol.setSize(10);
    pointSymbol.setColor(new dojo.Color("#FF3300"))
    var point = new esri.geometry.Point(parseFloat(obj.JD), parseFloat(obj.WD));
    var graphic = new esri.Graphic(point, pointSymbol, attributes, infoTemplate);
    graphicLayer.add(graphic);
    //text
    var font = new esri.symbol.Font();
    font.setSize("10pt");
    font.setFamily("微软雅黑");
    var textSymbol = new esri.symbol.TextSymbol(obj.FIRENAME);
    textSymbol.setColor(new dojo.Color("yellow"));
    textSymbol.setFont(font);
    textSymbol.setOffset(0, 10);
    var graphicText = new esri.Graphic(graphic.geometry, textSymbol);
    map.graphics.add(graphicText);

    map.centerAndZoom(point, 14);
    //var extent = map.extent;
    //if (!extent.contains(point)) {
    //    map.centerAndZoom(point, 13);
    //}
    //闪烁
    Twinkleshow();
}