var ORGNAMEArr = [];
var UseType1 = [];
var UseType2 = [];
var UseType3 = [];
var ManType1 = [];
var ManType2 = [];
var ManType3 = [];
var PAType1 = [];
var PAType2 = [];
var STType1 = [];
var STType2 = [];
//绘图 队伍人数
function setPropagandasteleCountMap(TopEchart, Propagandasteletype, Usestate, Managerstate, Structure) {
    var Propagandasteleoption = {
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
                if (Propagandasteletype.indexOf("1") > -1 ) {
                    legends.push("永久性");
                }
                if (Propagandasteletype.indexOf("2") > -1 ) {
                    legends.push("临时性");
                }
                if (Structure.indexOf("1") > -1 ) {
                    legends.push("钢构");
                }
                if (Structure.indexOf("2") > -1 ) {
                    legends.push("砖混");
                }
                if (Structure.indexOf("3") > -1 ) {
                    legends.push("钢混");
                }
                if (Usestate.indexOf("1") > -1 ) {
                    legends.push("在用");
                }
                if (Usestate.indexOf("2") > -1 ) {
                    legends.push("规划");
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
                name: '宣传碑牌数'
            }
        ],
        series: function () {
            var serie = [];
            if (Propagandasteletype.indexOf("1") > -1 ) {
                var item =
                    {
                        name: '永久性',
                        type: 'bar',
                        data: PAType1
                    }
                serie.push(item);
            }

            if (Propagandasteletype.indexOf("2") > -1 ) {
                var item = {
                    name: '临时性',
                    type: 'bar',
                    data: PAType2
                }
                serie.push(item);
            }
            if (Structure.indexOf("1") > -1 ) {
                var item = {
                    name: '钢构',
                    type: 'bar',
                    data: STType1
                }
                serie.push(item);
            }
            if (Structure.indexOf("2") > -1 ) {
                var item = {
                    name: '砖混',
                    type: 'bar',
                    data: STType2
                }
                serie.push(item);
            }
            if (Structure.indexOf("3") > -1 ) {
                var item = {
                    name: '钢混',
                    type: 'bar',
                    data: STType3
                }
                serie.push(item);
            }
            if (Usestate.indexOf("1") > -1 ) {
                var item = {
                    name: '在用',
                    type: 'bar',
                    data: UseType1
                }
                serie.push(item);
            }
            if (Usestate.indexOf("2") > -1 ) {
                var item = {
                    name: '规划',
                    type: 'bar',
                    data: UseType2
                }
                serie.push(item);
            }
            if (Usestate.indexOf("3") > -1 ) {
                var item = {
                    name: '报废',
                    type: 'bar',
                    data: UseType3
                }
                serie.push(item);
            }
            if (Managerstate.indexOf("1") > -1 ) {
                var item = {
                    name: '未维护',
                    type: 'bar',
                    data: ManType1
                }
                serie.push(item);
            }
            if (Managerstate.indexOf("2") > -1 ) {
                var item = {
                    name: '维护',
                    type: 'bar',
                    data: ManType2
                }
                serie.push(item);
            }
            if (Managerstate.indexOf("3") > -1 ) {
                var item = {
                    name: '新建',
                    type: 'bar',
                    data: ManType3
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    PropagandasteleChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    PropagandasteleChart.setOption(Propagandasteleoption);
}

//个数
function getPropagandasteleCount(TopEchart, Propagandasteletype, Usestate, Managerstate, Structure) {
    PropagandasteleChart.showLoading();//start loading
    ORGNAMEArr = [];
    UseType1 = [];
    UseType2 = [];
    UseType3 = [];
    ManType1 = [];
    ManType2 = [];
    ManType3 = [];
     PAType1 = [];
    PAType2 = [];
     STType1 = [];
     STType2 = [];
     STType3 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetPropagandasteleChartSourceData",
        data:
            {
                TopEchart: TopEchart,
                Propagandasteletype: Propagandasteletype,
                Usestate: Usestate,
                Managerstate: Managerstate,
                Structure: Structure,
            },
        dataType: "json",
        success: function (obj) {
            PropagandasteleChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var FLNum1Count = parseInt(content.Propagandasteletyp1Count);
                            var FLNum2Count = parseInt(content.Propagandasteletyp2Count);
                            var FUNum1Count = parseInt(content.Structuretyp1Count);
                            var FUNum2Count = parseInt(content.Structuretyp2Count);
                            var FUNum3Count = parseInt(content.Structuretyp3Count);
                            var UsestateNum1Count = parseInt(content.Usestate1Count);
                            var UsestateNum2Count = parseInt(content.Usestate2Count);
                            var UsestateNum3Count = parseInt(content.Usestate3Count);
                            var ManagerstateNum1Count = parseInt(content.Managerstate1Count);
                            var ManagerstateNum2Count = parseInt(content.Managerstate2Count);
                            var ManagerstateNum3Count = parseInt(content.Managerstate3Count);
                            PAType1.push(FLNum1Count);
                            PAType2.push(FLNum2Count);
                            STType1.push(FUNum1Count);
                            STType2.push(FUNum2Count);
                            STType3.push(FUNum3Count);
                            UseType1.push(UsestateNum1Count);
                            UseType2.push(UsestateNum2Count);
                            UseType3.push(UsestateNum3Count);
                            ManType1.push(ManagerstateNum1Count);
                            ManType2.push(ManagerstateNum2Count);
                            ManType3.push(ManagerstateNum3Count);
                        });
                        console.info(STType1);
                        console.info(STType2);
                        console.info(STType3);
                        setPropagandasteleCountMap(TopEchart, Propagandasteletype, Usestate, Managerstate, Structure);
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
            PropagandasteleChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

