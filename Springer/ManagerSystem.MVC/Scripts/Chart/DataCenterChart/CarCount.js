var ORGNAMEArr = [];
var CarType1 = [];
var CarType2 = [];
var CarType3 = [];
var CarType4 = [];
var CarType5 = [];
//绘图 队伍人数
function setCarCountMap(DictValue) {
    var Caroption = {
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
                    legends.push("指挥车");
                }
                if (DictValue.indexOf("2") > -1) {
                    legends.push("运兵车");
                }
                if (DictValue.indexOf("3") > -1) {
                    legends.push("供水车");
                }
                if (DictValue.indexOf("4") > -1) {
                    legends.push("通讯车");
                }
                if (DictValue.indexOf("5") > -1) {
                    legends.push("宣传车");
                }
                return legends;
            }()
        },
        toolbox: {
            show: true,
            left: 'right',
            feature: {
                magicType: {
                    type: ['line','tiled']
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
                name: '车辆数'
            }
        ],
        series: function () {
            var serie = [];
            if (DictValue.indexOf("1") > -1)
            {
                var item =
                    {
                        name: '指挥车',
                        type: 'bar',
                        data: CarType1
                    }
                serie.push(item);
            }

            if (DictValue.indexOf("2") > -1) {
                var item = {
                    name: '运兵车',
                    type: 'bar',
                    data: CarType2
                }
                serie.push(item);
            }
            if (DictValue.indexOf("3") > -1) {
                var item = {
                    name: '供水车',
                    type: 'bar',
                    data: CarType3
                }
                serie.push(item);
            }
            if (DictValue.indexOf("4") > -1) {
                var item = {
                    name: '通讯车',
                    type: 'bar',
                    data: CarType4
                }
                serie.push(item);
            }
            if (DictValue.indexOf("5") > -1) {
                var item = {
                    name: '宣传车',
                    type: 'bar',
                    data: CarType5
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    CarChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    CarChart.setOption(Caroption);
}

//队伍个数
function getCarCount(TopEchart, DictValue) {
    CarChart.showLoading();//start loading
    ORGNAMEArr = [];
    CarType1 = [];
    CarType2 = [];
    CarType3 = [];
    CarType4= [];
    CarType5 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetCarChartSourceData",
        data:
            {
                TopEchart: TopEchart,
                DictValue: DictValue
            },
        dataType: "json",
        success: function (obj) {
            CarChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var CarNum1Count = parseInt(content.CarType1Count);
                            var CarNum2Count = parseInt(content.CarType2Count);
                            var CarNum3Count = parseInt(content.CarType3Count);
                            var CarNum4Count = parseInt(content.CarType4Count);
                            var CarNum5Count = parseInt(content.CarType5Count);
                            CarType1.push(CarNum1Count);
                            CarType2.push(CarNum2Count);
                            CarType3.push(CarNum3Count);
                            CarType4.push(CarNum4Count);
                            CarType5.push(CarNum5Count);
                        });
                        setCarCountMap(DictValue);//车辆数
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
            CarChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

