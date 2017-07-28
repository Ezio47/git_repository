var ORGNAMEArr = [];
var UseType1 = [];
var UseType2 = [];
var UseType3 = [];
var ManType1 = [];
var ManType2 = [];
var ManType3 = [];
var TRType1 = [];
var TRType2 = [];
function setFactorcollectstationCountMap(TopEchart, Transfermodetype, Usestate, Managerstate) {
    var Factorcollectstationoption = {
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
                if (Transfermodetype.indexOf("1") > -1 ) {
                    legends.push("有线");
                }
                if (Transfermodetype.indexOf("2") > -1 ) {
                    legends.push("无线");
                }
                if (Usestate.indexOf("1") > -1 ) {
                    legends.push("在用");
                }
                if (Usestate.indexOf("2") > -1 ) {
                    legends.push("储存");
                }
                if (Usestate.indexOf("3") > -1 ) {
                    legends.push("报废");
                }
                if (Managerstate.indexOf("1") > -1 ) {
                    legends.push("未维护");
                }
                if (Managerstate.indexOf("2") > -1 ) {
                    legends.push("维护");
                }
                if (Managerstate.indexOf("3") > -1 ) {
                    legends.push("新建");
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
                name: '因子采集站数'
            }
        ],
        series: function () {
            var serie = [];
            if (Transfermodetype.indexOf("1") > -1 ) {
                var item =
                    {
                        name: '有线',
                        type: 'bar',
                        stack: '因子采集站',
                        data: TRType1
                    }
                serie.push(item);
            }

            if (Transfermodetype.indexOf("2") > -1 ) {
                var item = {
                    name: '无线',
                    type: 'bar',
                    stack: '因子采集站',
                    data: TRType2
                }
                serie.push(item);
            }
            if (Usestate.indexOf("1") > -1 ) {
                var item = {
                    name: '在用',
                    type: 'bar',
                    stack: '因子采集站',
                    data: UseType1
                }
                serie.push(item);
            }
            if (Usestate.indexOf("2") > -1 ) {
                var item = {
                    name: '储存',
                    type: 'bar',
                    stack: '因子采集站',
                    data: UseType2
                }
                serie.push(item);
            }
            if (Usestate.indexOf("3") > -1 ) {
                var item = {
                    name: '报废',
                    type: 'bar',
                    stack: '因子采集站',
                    data: UseType3
                }
                serie.push(item);
            }
            if (Managerstate.indexOf("1") > -1 ) {
                var item = {
                    name: '未维护',
                    type: 'bar',
                    stack: '因子采集站',
                    data: ManType1
                }
                serie.push(item);
            }
            if (Managerstate.indexOf("2") > -1 ) {
                var item = {
                    name: '维护',
                    type: 'bar',
                    stack: '因子采集站',
                    data: ManType2
                }
                serie.push(item);
            }
            if (Managerstate.indexOf("3") > -1 ) {
                var item = {
                    name: '新建',
                    type: 'bar',
                    stack: '因子采集站',
                    data: ManType3
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    FactorcollectstationChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    FactorcollectstationChart.setOption(Factorcollectstationoption);
}


function getFactorcollectstationCount(TopEchart, Transfermodetype, Usestate, Managerstate) {
    FactorcollectstationChart.showLoading();//start loading
    ORGNAMEArr = [];
    UseType1 = [];
    UseType2 = [];
    UseType3 = [];
    ManType1 = [];
    ManType2 = [];
    ManType3 = [];
    TRType1 = [];
    TRType2 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetFactorcollectstationChartSourceData",
        data:
            {
                TopEchart: TopEchart,
                Transfermodetype: Transfermodetype,
                Usestate: Usestate,
                Managerstate: Managerstate,
            },
        dataType: "json",
        success: function (obj) {
            FactorcollectstationChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var CommunicationwayNum1Count = parseInt(content.Transfermode1Count);
                            var CommunicationwayNum2Count = parseInt(content.Transfermode2Count);
                            var UsestateNum1Count = parseInt(content.Usestate1Count);
                            var UsestateNum2Count = parseInt(content.Usestate2Count);
                            var UsestateNum3Count = parseInt(content.Usestate3Count);
                            var ManagerstateNum1Count = parseInt(content.Managerstate1Count);
                            var ManagerstateNum2Count = parseInt(content.Managerstate2Count);
                            var ManagerstateNum3Count = parseInt(content.Managerstate3Count);
                            TRType1.push(CommunicationwayNum1Count);
                            TRType2.push(CommunicationwayNum2Count);
                            UseType1.push(UsestateNum1Count);
                            UseType2.push(UsestateNum2Count);
                            UseType3.push(UsestateNum3Count);
                            ManType1.push(ManagerstateNum1Count);
                            ManType2.push(ManagerstateNum2Count);
                            ManType3.push(ManagerstateNum3Count);
                        });
                        setFactorcollectstationCountMap(TopEchart, Transfermodetype, Usestate, Managerstate);
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
            FactorcollectstationChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

