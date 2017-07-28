var ORGNAMEArr = [];
var UseType1 = [];
var UseType2 = [];
var UseType3 = [];
var EQType1 = [];
var EQType2 = [];
var EQType3 = [];
var EQType4 = [];
var EQType5 = [];
var EQType6 = [];
function setEquipCountMap(TopEchart, Equiptype, Usestate) {
    var Equipoption = {
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
                if (Equiptype.indexOf("1") > -1) {
                    legends.push("扑救类");
                }
                if (Equiptype.indexOf("2") > -1) {
                    legends.push("阻隔类");
                }
                if (Equiptype.indexOf("3") > -1) {
                    legends.push("防护类");
                }
                if (Equiptype.indexOf("4") > -1) {
                    legends.push("通讯类");
                }
                if (Equiptype.indexOf("5") > -1) {
                    legends.push("户外类");
                }
                if (Equiptype.indexOf("5") > -1) {
                    legends.push("运输类");
                }
                if (Usestate.indexOf("1") > -1) {
                    legends.push("在用");
                }
                if (Usestate.indexOf("2") > -1) {
                    legends.push("规划");
                }
                if (Usestate.indexOf("3") > -1) {
                    legends.push("报废");
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
            if (Equiptype.indexOf("1") > -1) {
                var item =
                    {
                        name: '扑救类',
                        type: 'bar',
                        data: EQType1
                    }
                serie.push(item);
            }

            if (Equiptype.indexOf("2") > -1) {
                var item = {
                    name: '阻隔类',
                    type: 'bar',
                    data: EQType2
                }
                serie.push(item);
            }
            if (Equiptype.indexOf("3") > -1) {
                var item = {
                    name: '防护类',
                    type: 'bar',
                    data: EQType3
                }
                serie.push(item);
            }
            if (Equiptype.indexOf("4") > -1) {
                var item = {
                    name: '通讯类',
                    type: 'bar',
                    data: EQType4
                }
                serie.push(item);
            }
            if (Equiptype.indexOf("5") > -1) {
                var item = {
                    name: '户外类',
                    type: 'bar',
                    data: EQType5
                }
                serie.push(item);
            }
            if (Equiptype.indexOf("6") > -1) {
                var item = {
                    name: '运输类',
                    type: 'bar',
                    data: EQType6
                }
                serie.push(item);
            }
            if (Usestate.indexOf("1") > -1) {
                var item = {
                    name: '在用',
                    type: 'bar',
                    data: UseType1
                }
                serie.push(item);
            }
            if (Usestate.indexOf("2") > -1) {
                var item = {
                    name: '规划',
                    type: 'bar',
                    data: UseType2
                }
                serie.push(item);
            }
            if (Usestate.indexOf("3") > -1) {
                var item = {
                    name: '报废',
                    type: 'bar',
                    data: UseType3
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    EquipChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    EquipChart.setOption(Equipoption);
}
function getEquipCount(TopEchart, Equiptype, Usestate) {
    EquipChart.showLoading();//start loading
    ORGNAMEArr = [];
    UseType1 = [];
    UseType2 = [];
    UseType3 = [];
    EQType1 = [];
    EQType2 = [];
    EQType3 = [];
    EQType4 = [];
    EQType5 = [];
    EQType6 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetEquipSourceData",
        data:
            {
                TopEchart: TopEchart,
                Equiptype: Equiptype,
                Usestate: Usestate,
            },
        dataType: "json",
        success: function (obj) {
            EquipChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var CommunicationwayNum1Count = parseInt(content.Equiptyp1Count);
                            var CommunicationwayNum2Count = parseInt(content.Equiptyp2Count);
                            var CommunicationwayNum3Count = parseInt(content.Equiptyp3Count);
                            var CommunicationwayNum4Count = parseInt(content.Equiptyp4Count);
                            var CommunicationwayNum5Count = parseInt(content.Equiptyp5Count);
                            var CommunicationwayNum6Count = parseInt(content.Equiptyp6Count);
                            var UsestateNum1Count = parseInt(content.Usestate1Count);
                            var UsestateNum2Count = parseInt(content.Usestate2Count);
                            var UsestateNum3Count = parseInt(content.Usestate3Count);
                            EQType1.push(CommunicationwayNum1Count);
                            EQType2.push(CommunicationwayNum2Count);
                            EQType3.push(CommunicationwayNum3Count);
                            EQType4.push(CommunicationwayNum4Count);
                            EQType5.push(CommunicationwayNum5Count);
                            EQType6.push(CommunicationwayNum6Count);
                            UseType1.push(UsestateNum1Count);
                            UseType2.push(UsestateNum2Count);
                            UseType3.push(UsestateNum3Count);
                        });
                        setEquipCountMap(TopEchart, Equiptype, Usestate);
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
            EquipChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

