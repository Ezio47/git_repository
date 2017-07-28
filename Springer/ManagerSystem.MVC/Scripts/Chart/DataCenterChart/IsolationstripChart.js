var ORGNAMEArr = [];
var UseType1 = [];
var UseTypeLE1 = [];
var UseType2 = [];
var UseTypeLE2 = [];
var UseType3 = [];
var UseTypeLE3 = [];
var ManTypeLE1 = [];
var ManTypeLE2 = [];
var ManTypeLE3 = [];
var ManType1 = [];
var ManType2 = [];
var ManType3 = [];
var IsType1 = [];
var IsTypeLE1 = [];
var IsType2 = [];
var IsTypeLE2 = [];
var IsType3 = [];
var IsTypeLE3 = [];
var IsType4 = [];
var IsTypeLE4 = [];
var IsType5 = [];
var IsTypeLE5 = [];
function  setIsolationstripCountMap(TopEchart, Isolationtype, Usestate, Managerstate) {
    var Isolationstripoption = {
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

                if (Isolationtype.indexOf("1") > -1 ) {
                    legends.push("生物");
                }
                if (Isolationtype.indexOf("2") > -1 ) {
                    legends.push("生土");
                }
                if (Isolationtype.indexOf("3") > -1 ) {
                    legends.push("火烧线");
                }
                if (Isolationtype.indexOf("4") > -1 ) {
                    legends.push("林下烧除");
                }
                if (Isolationtype.indexOf("5") > -1 ) {
                    legends.push("规划生物隔离带");
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
                name: '隔离带数'
            }
        ],
        series: function () {
            var serie = [];
            
                if (Isolationtype.indexOf("1") > -1) {
                    var item =
                        {
                            name: '生物',
                            type: 'bar',
                            data: IsType1
                        }
                    serie.push(item);
                }
                if (Isolationtype.indexOf("2") > -1) {
                    var item = {
                        name: '生土',
                        type: 'bar',
                        data: IsType2
                    }
                    serie.push(item);
                }
                if (Isolationtype.indexOf("3") > -1) {
                    var item = {
                        name: '火烧线',
                        type: 'bar',
                        data: IsType3
                    }
                    serie.push(item);
                }
                if (Isolationtype.indexOf("4") > -1) {
                    var item = {
                        name: '林下烧除',
                        type: 'bar',
                        data: IsType4
                    }
                    serie.push(item);
                }
                if (Isolationtype.indexOf("5") > -1) {
                    var item = {
                        name: '规划生物隔离带',
                        type: 'bar',
                        data: IsType5
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
    IsolationstripChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    IsolationstripChart.setOption(Isolationstripoption);
}


function setIsolationstripCountMap1(TopEchart, Isolationtype, Usestate, Managerstate, Option) {
    var Isolationstripoption1 = {
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

                if (Isolationtype.indexOf("1") > -1) {
                    legends.push("生物");
                }
                if (Isolationtype.indexOf("2") > -1 ) {
                    legends.push("生土");
                }
                if (Isolationtype.indexOf("3") > -1 ) {
                    legends.push("火烧线");
                }
                if (Isolationtype.indexOf("4") > -1 ) {
                    legends.push("林下烧除");
                }
                if (Isolationtype.indexOf("5") > -1 ) {
                    legends.push("规划生物隔离带");
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
                if (Managerstate.indexOf("3") > -1) {
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
                name: '长度(米)'
            }
        ],
        series: function () {
            var serie = [];
            
                if (Isolationtype.indexOf("1") > -1) {
                    var item =
                        {
                            name: '生物',
                            type: 'bar',
                            data: IsTypeLE1
                        }
                    serie.push(item);
                }
                if (Isolationtype.indexOf("2") > -1) {
                    var item = {
                        name: '生土',
                        type: 'bar',
                        data: IsTypeLE2
                    }
                    serie.push(item);
                }
                if (Isolationtype.indexOf("3") > -1) {
                    var item = {
                        name: '火烧线',
                        type: 'bar',
                        data: IsTypeLE3
                    }
                    serie.push(item);
                }
                if (Isolationtype.indexOf("4") > -1) {
                    var item = {
                        name: '林下烧除',
                        type: 'bar',
                        data: IsTypeLE4
                    }
                    serie.push(item);
                }
                if (Isolationtype.indexOf("5") > -1) {
                    var item = {
                        name: '规划生物隔离带',
                        type: 'bar',
                        data: IsTypeLE5
                    }
                    serie.push(item);
                }
            
                if (Usestate.indexOf("1") > -1) {
                    var item = {
                        name: '在用',
                        type: 'bar',
                        data: UseTypeLE1
                    }
                    serie.push(item);
                }
                if (Usestate.indexOf("2") > -1) {
                    var item = {
                        name: '规划',
                        type: 'bar',
                        data: UseTypeLE2
                    }
                    serie.push(item);
                }
                if (Usestate.indexOf("3") > -1) {
                    var item = {
                        name: '报废',
                        type: 'bar',
                        data: UseTypeLE3
                    }
                    serie.push(item);
                }
            
           
                if (Managerstate.indexOf("1") > -1) {
                    var item = {
                        name: '未维护',
                        type: 'bar',
                        data: ManTypeLE1
                    }
                    serie.push(item);
                }
                if (Managerstate.indexOf("2") > -1) {
                    var item = {
                        name: '维护',
                        type: 'bar',
                        data: ManTypeLE2
                    }
                    serie.push(item);
                }
                if (Managerstate.indexOf("3") > -1) {
                    var item = {
                        name: '新建',
                        type: 'bar',
                        data: ManTypeLE3
                    }
                    serie.push(item);
                }
            
            return serie;
        }()
    }
    IsolationstripChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    IsolationstripChart.setOption(Isolationstripoption1);
}
//个数
function getIsolationstrip(TopEchart, Isolationtype, Usestate, Managerstate, Option) {
    IsolationstripChart.showLoading();//start loading
     ORGNAMEArr = [];
     UseType1 = [];
     UseTypeLE1 = [];
     UseType2 = [];
     UseTypeLE2 = [];
     UseType3 = [];
     UseTypeLE3 = [];
     ManTypeLE1 = [];
     ManTypeLE2 = [];
     ManTypeLE3 = [];
     ManType1 = [];
     ManType2 = [];
     ManType3 = [];
     IsType1 = [];
     IsTypeLE1 = [];
     IsType2 = [];
     IsTypeLE2 = [];
     IsType3 = [];
     IsTypeLE3 = [];
     IsType4 = [];
     IsTypeLE4 = [];
     IsType5 = [];
     IsTypeLE5 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetISOLATIONTYPEChartSourceData",
        data:
            {
                TopEchart: TopEchart,
                Isolationtype: Isolationtype,
                Usestate: Usestate,
                Managerstate: Managerstate,
            },
        dataType: "json",
        success: function (obj) {
            IsolationstripChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            IsType1.push(Number(content.IsolationstripType1Count));
                            IsTypeLE1.push(Number(content.IsolationstripTypeLength1Count));
                            IsType2.push(Number(content.IsolationstripType2Count));
                            IsTypeLE2.push(Number(content.IsolationstripTypeLength2Count));
                            IsType3.push(Number(content.IsolationstripType3Count));
                            IsTypeLE3.push(Number(content.IsolationstripTypeLength3Count));
                            IsType4.push(Number(content.IsolationstripType4Count));
                            IsTypeLE4.push(Number(content.IsolationstripTypeLength4Count));
                            IsType5.push(Number(content.IsolationstripType5Count));
                            IsTypeLE5.push(Number(content.IsolationstripTypeLength5Count));
                            UseType1.push(Number(content.Usestate1Count));
                            UseTypeLE1.push(Number(content.UsestateLength1Count));
                            UseType2.push(Number(content.Usestate2Count));
                            UseTypeLE2.push(Number(content.UsestateLength2Count));
                            UseType3.push(Number(content.Usestate3Count));
                            UseTypeLE3.push(Number(content.UsestateLength3Count));
                            ManType1.push(Number(content.Managerstate1Count));
                            ManTypeLE1.push(Number(content.ManagerstateLength1Count));
                            ManType2.push(Number(content.Managerstate2Count));
                            ManTypeLE2.push(Number(content.ManagerstateLength2Count));
                            ManType3.push(Number(content.Managerstate3Count));
                            ManTypeLE3.push(Number(content.ManagerstateLength3Count));
                        });
                        if (Option=="1") {
                            setIsolationstripCountMap(TopEchart, Isolationtype, Usestate, Managerstate);
                        }
                        if (Option == "2") {
                            setIsolationstripCountMap1(TopEchart, Isolationtype, Usestate, Managerstate);
                        }
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
            IsolationstripChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

