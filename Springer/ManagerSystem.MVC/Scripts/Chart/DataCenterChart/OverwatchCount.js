var ORGNAMEArr = [];
var OverwatchType1 = [];
var OverwatchType2 = [];
var OverwatchType3 = [];

//绘图 营房的数量
function setOverwatchCountMap(DictValue) {
    var Overwatchoption = {
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
                    legends.push("钢构");
                }
                if (DictValue.indexOf("2") > -1) {
                    legends.push("砖混");
                }
                if (DictValue.indexOf("3") > -1) {
                    legends.push("钢混");
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
                name: '瞭望台数'
            }
        ],
        series: function () {
            var serie = [];
            if (DictValue.indexOf("1") > -1) {
                var item =
                    {
                        name: '钢构',
                        type: 'bar',
                        data: OverwatchType1
                    }
                serie.push(item);
            }
            if (DictValue.indexOf("2") > -1) {
                var item = {
                    name: '砖混',
                    type: 'bar',
                    data: OverwatchType2
                }
                serie.push(item);
            }
            if (DictValue.indexOf("3") > -1) {
                var item = {
                    name: '钢混',
                    type: 'bar',
                    data: OverwatchType3
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    OVERWATCHChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    OVERWATCHChart.setOption(Overwatchoption);
}

//营房个数
function getOVERWATCHCount(TopEchart, DictValue) {
    OVERWATCHChart.showLoading();//start loading
     ORGNAMEArr = [];
    OverwatchType1 = [];
     OverwatchType2 = [];
     OverwatchType3 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetOVERWATCHChartSourceData",
        data:
            {
                TopEchart: TopEchart,
                DictValue: DictValue
            },
        dataType: "json",
        success: function (obj) {
            OVERWATCHChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var OverwatchNum1Count = parseInt(content.OVERWATCHType1Count);
                            var OverwatchNum2Count = parseInt(content.OVERWATCHType2Count);
                            var OverwatchNum3Count = parseInt(content.OVERWATCHType3Count);
                            OverwatchType1.push(OverwatchNum1Count);
                            OverwatchType2.push(OverwatchNum2Count);
                            OverwatchType3.push(OverwatchNum3Count);
                        });
                        setOverwatchCountMap(DictValue);//瞭望台数

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
            OVERWATCHChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

