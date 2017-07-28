var ORGNAMEArr = [];
var CampType1 = [];
var CampType2 = [];
var CampType3 = [];

//绘图 营房的数量
function setCampCountMap(DictValue) {
    var Campoption = {
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
                name: '营房数'
            }
        ],
        series: function () {
            var serie = [];
            if (DictValue.indexOf("1") > -1) {
                var item =
                    {
                        name: '钢构',
                        type: 'bar',
                        data: CampType1
                    }
                serie.push(item);
            }
            if (DictValue.indexOf("2") > -1) {
                var item = {
                    name: '砖混',
                    type: 'bar',
                    data: CampType2
                }
                serie.push(item);
            }
            if (DictValue.indexOf("3") > -1) {
                var item = {
                    name: '钢混',
                    type: 'bar',
                    data: CampType3
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    CampChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    CampChart.setOption(Campoption);
}

//营房个数
function getCampCount(TopEchart, DictValue) {
    CampChart.showLoading();//start loading
    ORGNAMEArr = [];
    CampType1 = [];
    CampType2 = [];
    CampType3 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetCampChartSourceData",
        data:
            {
                TopEchart: TopEchart,
                DictValue: DictValue
            },
        dataType: "json",
        success: function (obj) {
            CampChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var CampNum1Count = parseInt(content.CampType1Count);
                            var CampNum2Count = parseInt(content.CampType2Count);
                            var CampNum3Count = parseInt(content.CampType3Count);
                            CampType1.push(CampNum1Count);
                            CampType2.push(CampNum2Count);
                            CampType3.push(CampNum3Count);
                        });
                        setCampCountMap(DictValue);//车辆数

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
            CampChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

