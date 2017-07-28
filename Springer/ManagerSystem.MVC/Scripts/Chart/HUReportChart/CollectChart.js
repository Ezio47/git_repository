var ORGNAMEArr = [];
var Collect1 = [];
var Collect2 = [];
var Collect3 = [];
var Collect4 = [];
function setCollectMap(TopEchart, CollectCount) {
    var CollectChartoption = {
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
                if (CollectCount.indexOf("1") > -1) {
                    legends.push("建筑物");
                }
                if (CollectCount.indexOf("2") > -1) {
                    legends.push("消防设施");
                }
                if (CollectCount.indexOf("3") > -1) {
                    legends.push("道路");
                }
                if (CollectCount.indexOf("4") > -1) {
                    legends.push("可燃物载量");
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
                name: '数量'
            }
        ],
        series: function () {
            var serie = [];
            if (CollectCount.indexOf("1") > -1) {
                var item =
                    {
                        name: '建筑物',
                        type: 'bar',
                        data: Collect1
                    }
                serie.push(item);
            }

            if (CollectCount.indexOf("2") > -1) {
                var item = {
                    name: '消防设施',
                    type: 'bar',
                    data: Collect2
                }
                serie.push(item);
            }
            if (CollectCount.indexOf("3") > -1) {
                var item = {
                    name: '道路',
                    type: 'bar',
                    data: Collect3
                }
                serie.push(item);
            }
            if (CollectCount.indexOf("4") > -1) {
                var item = {
                    name: '可燃物载量',
                    type: 'bar',
                    data: Collect4
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    CollectChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    CollectChart.setOption(CollectChartoption);
}

function getCollectCount(TopEchart, CollectCount, DateBegin, DateEnd) {
    CollectChart.showLoading();
    ORGNAMEArr = [];
    Collect1 = [];
    Collect2 = [];
    Collect3 = [];
    Collect4 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetCollectData",
        data:
            {
                TopEchart: TopEchart,
                CollectCount: CollectCount,
                DateBegin: DateBegin,
                DateEnd: DateEnd,
            },
        dataType: "json",
        success: function (obj) {
            CollectChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var data1 = parseInt(content.CollectType1Count);
                            var data2 = parseInt(content.CollectType2Count);
                            var data3 = parseInt(content.CollectType3Count);
                            var data4 = parseInt(content.CollectType4Count);
                            Collect1.push(data1);
                            Collect2.push(data2);
                            Collect3.push(data3);
                            Collect4.push(data4);
                        });

                        setCollectMap(TopEchart, CollectCount);
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
            CollectChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

