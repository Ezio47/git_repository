
var ORGNAMEArr = [];
var ORGNAMEArr1 = [];
var armytype1Data = [];
var armytype2Data = [];
var armytype3Data = [];
var armytype4Data = [];
var ArmyType1 = [];
var ArmyType2 = [];
var ArmyType3 = [];
var ArmyType4 = [];

//绘图 队伍人数
function setArmyCountMap(DictValue) {
    var Armyoption = {
        title: {
            text: '',
        },
        tooltip: {
            trigger: 'axis',
            axisPointer:
                {
                    type: 'shadow'
                }
        },
        legend: {
            data: function () {
                var legends = [];
                if (DictValue.indexOf("1") > -1) {
                    legends.push("专业队伍");
                }
                if (DictValue.indexOf("2") > -1) {
                    legends.push("半专业队伍");
                }
                if (DictValue.indexOf("3") > -1) {
                    legends.push("应急队伍");
                }
                if (DictValue.indexOf("4") > -1) {
                    legends.push("群众队伍");
                }
                return legends;
            }()
        },
        toolbox: {
            show: true,
            left: 'right',
            feature: {
                magicType: {
                    type: ['line', 'stack', 'tiled']
                },
                dataView: { readOnly: false },
                restore: {},
                saveAsImage: {}
            }
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: [
       {
           type: 'category',
           data: ORGNAMEArr,
           axisLabel: {
               interval: 0,
               rotate: 30
           }
       }
        ],
        yAxis: [
            {
                type: 'value',
                name: '人数'
            }
        ],
        series: function () {
            var serie = [];
            if (DictValue.indexOf("1") > -1) {
                var item =
                    {
                        name: '专业队伍',
                        type: 'bar',
                        data: armytype1Data
                    }
                serie.push(item);
            }

            if (DictValue.indexOf("2") > -1) {
                var item = {
                    name: '半专业队伍',
                    type: 'bar',
                    data: armytype2Data
                }
                serie.push(item);
            }
            if (DictValue.indexOf("3") > -1) {
                var item = {
                    name: '应急队伍',
                    type: 'bar',
                    data: armytype3Data
                }
                serie.push(item);
            }
            if (DictValue.indexOf("4") > -1) {
                var item = {
                    name: '群众队伍',
                    type: 'bar',
                    data: armytype4Data
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    armyChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    armyChart.setOption(Armyoption);
}

//队伍个数
function setArmyCount1Map(DictValue) {
    var Armyoption1 = {
        title: {
            text: '',
        },
        tooltip: {
            trigger: 'axis',
            axisPointer:
                {
                    type: 'shadow'
                }
        },
        legend: {
            data: function () {
                var legends = [];
                if (DictValue.indexOf("1") > -1) {
                    legends.push("专业队伍");
                }
                if (DictValue.indexOf("2") > -1) {
                    legends.push("半专业队伍");
                }
                if (DictValue.indexOf("3") > -1) {
                    legends.push("应急队伍");
                }
                if (DictValue.indexOf("4") > -1) {
                    legends.push("群众队伍");
                }
                return legends;
            }()
        },
        toolbox: {
            show: true,
            left: 'right',
            feature: {
                magicType: {
                    type: ['line', 'stack', 'tiled']
                },
                dataView: { readOnly: false },
                restore: {},
                saveAsImage: {}
            }
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: [
       {
           type: 'category',
           data: ORGNAMEArr1,
           axisLabel: {
               interval: 0,
               rotate: 30
           }
       }
        ],
        yAxis: [
            {
                type: 'value',
                name: '个数'
            }
        ],
        series: function () {
            var serie = [];
            if (DictValue.indexOf("1") > -1) {
                var item =
                    {
                        name: '专业队伍',
                        type: 'bar',
                        data: ArmyType1
                    }
                serie.push(item);
            }

            if (DictValue.indexOf("2") > -1) {
                var item = {
                    name: '半专业队伍',
                    type: 'bar',
                    data: ArmyType2
                }
                serie.push(item);
            }
            if (DictValue.indexOf("3") > -1) {
                var item = {
                    name: '应急队伍',
                    type: 'bar',
                    data: ArmyType3
                }
                serie.push(item);
            }
            if (DictValue.indexOf("4") > -1) {
                var item = {
                    name: '群众队伍',
                    type: 'bar',
                    data: ArmyType4
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    armyChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    armyChart.setOption(Armyoption1);
}
function getArmyCount(TopEchart, DictValue) {
    armyChart.showLoading();//start loading
    ORGNAMEArr = [];
    armytype1Data = [];
    armytype2Data = [];
    armytype3Data = [];
    armytype4Data = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetArmySourceData",
        data:
            {
                TopEchart: TopEchart,
                DictValue: DictValue
            },
        dataType: "json",
        // async:false,
        success: function (obj) {
            armyChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var ArmyMem1Count = parseInt(content.ArmyMem1Count);
                            var ArmyMem2Count = parseInt(content.ArmyMem2Count);
                            var ArmyMem3Count = parseInt(content.ArmyMem3Count);
                            var ArmyMem4Count = parseInt(content.ArmyMem4Count);
                            armytype1Data.push(ArmyMem1Count);
                            armytype2Data.push(ArmyMem2Count);
                            armytype3Data.push(ArmyMem3Count);
                            armytype4Data.push(ArmyMem4Count);
                        });
                        setArmyCountMap(DictValue);//队伍个数
                    }
                    else {
                        layer.msg("暂无最新数据！", { icon: 5 });
                    }
                }
                else {
                    layer.msg("暂无最新数据！", { icon: 5 });
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            armyChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

function getArmyCount1(TopEchart, DictValue) {
    ORGNAMEArr1 = [];
    ArmyType1 = [];
    armytype2 = [];
    armytype3 = [];
    armytype4 = [];
    armyChart.showLoading();//start loading
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetArmySourceData",
        data:
            {
                TopEchart: TopEchart,
                DictValue: DictValue
            },
        dataType: "json",
        success: function (obj) {
            armyChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME1 = content.ORGName;
                            ORGNAMEArr1.push(ORGNAME1);
                            var ArmyTypeNum1Count = parseInt(content.ArmyType1Count);
                            var ArmyTypeNum2Count = parseInt(content.ArmyType2Count);
                            var ArmyTypeNum3Count = parseInt(content.ArmyType3Count);
                            var ArmyTypeNum4Count = parseInt(content.ArmyType4Count);
                            ArmyType1.push(ArmyTypeNum1Count);
                            ArmyType2.push(ArmyTypeNum2Count);
                            ArmyType3.push(ArmyTypeNum3Count);
                            ArmyType4.push(ArmyTypeNum4Count);
                        });
                        setArmyCount1Map(DictValue);//队伍个数
                    }
                    else {
                        layer.msg("暂无最新数据！", { icon: 5 });
                    }
                }
                else {
                    layer.msg("暂无最新数据！", { icon: 5 });
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            armyChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}
