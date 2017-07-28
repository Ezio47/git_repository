var ORGNAMEArr = [];
var Line1 = [];
var Line2 = [];
var Line3 = [];
var Point1 = [];
var Point2 = [];
var Point3 = [];
function setPaCountMap(TopEchart, LineCount, PointCount) {
    var PatrolRouteoption = {
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
                if (LineCount.indexOf("1") > -1) {
                    legends.push("巡检");
                }
                if (LineCount.indexOf("2") > -1) {
                    legends.push("未巡检");
                }
                if (LineCount.indexOf("3") > -1) {
                    legends.push("巡检率(%)");
                }
                if (PointCount.indexOf("1") > -1) {
                    legends.push("完成");
                }
                if (PointCount.indexOf("2") > -1) {
                    legends.push("未完成");
                }
                if (PointCount.indexOf("3") > -1) {
                    legends.push("完成率(%)");
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
            if (LineCount.indexOf("1") > -1) {
                var item =
                    {
                        name: '巡检',
                        type: 'bar',
                        data: Line1
                    }
                serie.push(item);
            }

            if (LineCount.indexOf("2") > -1) {
                var item = {
                    name: '未巡检',
                    type: 'bar',
                    data: Line2
                }
                serie.push(item);
            }
            if (LineCount.indexOf("3") > -1) {
                var item = {
                    name: '巡检率(%)',
                    type: 'bar',
                    data: Line3
                }
                serie.push(item);
            }
            if (PointCount.indexOf("1") > -1) {
                var item = {
                    name: '完成',
                    type: 'bar',
                    data: Point1
                }
                serie.push(item);
            }
            if (PointCount.indexOf("2") > -1) {
                var item = {
                    name: '未完成',
                    type: 'bar',
                    data: Point2
                }
                serie.push(item);
            }
            if (PointCount.indexOf("3") > -1) {
                var item = {
                    name: '完成率(%)',
                    type: 'bar',
                    data: Point3
                }
                serie.push(item);
            }

            return serie;
        }()
    }
    PatrolRouteChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    PatrolRouteChart.setOption(PatrolRouteoption);
}

function getPatrolRouteCount(TopEchart, LineCount, PointCount, DateBegin, DateEnd) {
    PatrolRouteChart.showLoading();
    ORGNAMEArr = [];
    Line1 = [];
    Line2 = [];
    Line3 = [];
    Point1 = [];
    Point2 = [];
    Point3 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetPatrolRouteData",
        data:
            {
                TopEchart: TopEchart,
                LineCount: LineCount,
                PointCount: PointCount,
                DateBegin: DateBegin,
                DateEnd: DateEnd,
            },
        dataType: "json",
        success: function (obj) {
            PatrolRouteChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var data1 = parseInt(content.LineCount0);
                            var data2 = parseInt(content.LineCount1);
                            var data3 = parseInt(content.LineCount2);
                            var data4 = parseInt(content.PointCount0);
                            var data5 = parseInt(content.PointCount1);
                            var data6 = parseInt(content.PointCount2);
                            Line1.push(data1);
                            Line2.push(data2);
                            Line3.push(data3);
                            Point1.push(data4);
                            Point2.push(data5);
                            Point3.push(data6);
                        });
                        setPaCountMap(TopEchart, LineCount, PointCount);
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
            PatrolRouteChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

