var ORGNAMEArr = [];
var Report1 = [];
var Report2 = [];
var Report3 = [];
var Report4 = [];
function setReportMap(TopEchart, ReportCount) {
    var ReportChartoption = {
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
                if (ReportCount.indexOf("1") > -1) {
                    legends.push("火情");
                }
                if (ReportCount.indexOf("2") > -1) {
                    legends.push("病虫害");
                }
                if (ReportCount.indexOf("3") > -1) {
                    legends.push("盗砍盗伐");
                }
                if (ReportCount.indexOf("4") > -1) {
                    legends.push("安全隐患");
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
            if (ReportCount.indexOf("1") > -1) {
                var item =
                    {
                        name: '火情',
                        type: 'bar',
                        data: Report1
                    }
                serie.push(item);
            }

            if (ReportCount.indexOf("2") > -1) {
                var item = {
                    name: '病虫害',
                    type: 'bar',
                    data: Report2
                }
                serie.push(item);
            }
            if (ReportCount.indexOf("3") > -1) {
                var item = {
                    name: '盗砍盗伐',
                    type: 'bar',
                    data: Report3
                }
                serie.push(item);
            }
            if (ReportCount.indexOf("4") > -1) {
                var item = {
                    name: '安全隐患',
                    type: 'bar',
                    data: Report4
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    ReportChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    ReportChart.setOption(ReportChartoption);
}

function getReportCount(TopEchart, ReportCount, DateBegin, DateEnd) {
    ReportChart.showLoading();
    ORGNAMEArr = [];
    Report1 = [];
    Report2 = [];
    Report3 = [];
    Report4 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetReportData",
        data:
            {
                TopEchart: TopEchart,
                ReportCount: ReportCount,
                DateBegin: DateBegin,
                DateEnd: DateEnd,
            },
        dataType: "json",
        success: function (obj) {
            ReportChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var data1 = parseInt(content.ReportType1Count);
                            var data2 = parseInt(content.ReportType2Count);
                            var data3 = parseInt(content.ReportType3Count);
                            var data4 = parseInt(content.ReportType4Count);
                            Report1.push(data1);
                            Report2.push(data2);
                            Report3.push(data3);
                            Report4.push(data4);
                        });
                        setReportMap(TopEchart, ReportCount);
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
            ReportChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

