var ORGNAMEArr = [];
var UseType = [];
var UseType1 = [];
var UseType2 = [];
var UseType3 = [];
var ManType = [];
var ManType1 = [];
var ManType2 = [];
var ManType3 = [];
var FlType = [];
var FlType1 = [];
var FlType2 = [];
var FUType = [];
var FUType1 = [];
var FUType2 = [];
//绘图 队伍人数
function setFirechannelCountMap(TopEchart, Fireleveltype, Usestate, Managerstate, Fireusetype) {
    var Firechanneloption = {
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
                
                    if (Fireleveltype.indexOf("1") > -1 ) {
                        legends.push("便道");
                    }
                    if (Fireleveltype.indexOf("2") > -1 ) {
                        legends.push("林区道路");
                    }
                    if (Fireusetype.indexOf("1") > -1 ) {
                        legends.push("人行道");
                    }
                    if (Fireusetype.indexOf("2") > -1 ) {
                        legends.push("车行道");
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
        calculable: true,
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
                name: '防火通道数'
            }
        ],
        series: function () {
            var serie = [];
           
                if (Fireleveltype.indexOf("1") > -1) {
                    var item =
                        {
                            name: '便道',
                            type: 'bar',
                            data: FlType1
                        }
                    serie.push(item);
                }
                if (Fireleveltype.indexOf("2") > -1) {
                    var item = {
                        name: '林区道路',
                        type: 'bar',
                        data: FlType2
                    }
                    serie.push(item);
                }
                if (Fireusetype.indexOf("1") > -1) {
                    var item = {
                        name: '人行道',
                        type: 'bar',
                        data: FUType1
                    }
                    serie.push(item);
                }
                if (Fireusetype.indexOf("2") > -1) {
                    var item = {
                        name: '车行道',
                        type: 'bar',
                        data: FUType2
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
                        name: '储存',
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
            
                if (Managerstate.indexOf("1") > -1) {
                    var item = {
                        name: '未维护',
                        type: 'bar',
                        data: ManType1
                    }
                    serie.push(item);
                }
                if (Managerstate.indexOf("2") > -1) {
                    var item = {
                        name: '维护',
                        type: 'bar',
                        data: ManType2
                    }
                    serie.push(item);
                }
                if (Managerstate.indexOf("3") > -1) {
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
    FirechannelChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    FirechannelChart.setOption(Firechanneloption);
}

//个数
function getFirechannelCount(TopEchart, Fireleveltype, Usestate, Managerstate, Fireusetype) {
    FirechannelChart.showLoading();//start loading
    ORGNAMEArr = [];
    UseType = [];
    UseType1 = [];
    UseType2 = [];
    UseType3 = [];
    ManType = [];
    ManType1 = [];
    ManType2 = [];
    ManType3 = [];
    FlType = [];
    FlType1 = [];
    FlType2 = [];
    FUType = [];
    FUType1 = [];
    FUType2 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetFirechannelChartSourceData",
        data:
            {
                TopEchart: TopEchart,
                Fireleveltype: Fireleveltype,
                Usestate: Usestate,
                Managerstate: Managerstate,
                Fireusetype: Fireusetype,
            },
        dataType: "json",
        success: function (obj) {
            FirechannelChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var FLNum1Count = Number(content.Fireleveltype1Count);
                            var FLNum2Count = Number(content.Fireleveltype2Count);
                            var FUNum1Count = Number(content.Fireusetypetype1Count);
                            var FUNum2Count = Number(content.Fireusetypetype2Count);
                            var UsestateNum1Count = Number(content.Usestate1Count);
                            var UsestateNum2Count = Number(content.Usestate2Count);
                            var UsestateNum3Count = Number(content.Usestate3Count);
                            var ManagerstateNum1Count = Number(content.Managerstate1Count);
                            var ManagerstateNum2Count = Number(content.Managerstate2Count);
                            var ManagerstateNum3Count = Number(content.Managerstate3Count);
                            FlType1.push(FLNum1Count);
                            FlType2.push(FLNum2Count);
                            FUType1.push(FUNum1Count);
                            FUType2.push(FUNum2Count);
                            UseType1.push(UsestateNum1Count);
                            UseType2.push(UsestateNum2Count);
                            UseType3.push(UsestateNum3Count);
                            ManType1.push(ManagerstateNum1Count);
                            ManType2.push(ManagerstateNum2Count);
                            ManType3.push(ManagerstateNum3Count);
                        });
                        setFirechannelCountMap(TopEchart, Fireleveltype, Usestate, Managerstate, Fireusetype);
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
            FirechannelChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

