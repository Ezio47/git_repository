var ORGNAMEArr = [];
var Hotetype1 = [];
var Hotetype2 = [];
var Hotetype3 = [];
var Hotetype4 = [];
var Hotetype5 = [];
var Hotetype6 = [];
var Hotetype7 = [];
var Hotetype8 = [];
var Hotetype9 = [];
var Hotetype10= [];
var Hotetype11 = [];
var Hotetype12 = [];
var Firelevel1 = [];
var Firelevel2= [];
var Firelevel3 = [];
var Firelevel4 = [];
var FireFrom1 = [];
var FireFrom2 = [];
var FireFrom3 = [];
var FireFrom4 = [];
var FireFrom5 = [];
var FireFrom6 = [];
var FireFrom7 = [];
//绘图 队伍人数
function setArchivalCountMap(TopEchart, Hotetype, Firelevel, Firefrom) {
    var Archivaloption = {
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
                if (Hotetype.indexOf("1") > -1) {
                    legends.push("林火");
                }
                if (Hotetype.indexOf("2") > -1) {
                    legends.push("草原火");
                }
                if (Hotetype.indexOf("3") > -1) {
                    legends.push("计划烧除");
                }
                if (Hotetype.indexOf("4") > -1) {
                    legends.push("农用火");
                }
                if (Hotetype.indexOf("5") > -1) {
                    legends.push("炼山");
                }
                if (Hotetype.indexOf("6") > -1) {
                    legends.push("灌木火");
                }
                if (Hotetype.indexOf("7") > -1) {
                    legends.push("工矿用火");
                }
                if (Hotetype.indexOf("8") > -1) {
                    legends.push("境外火");
                }
                if (Hotetype.indexOf("9") > -1) {
                    legends.push("未找到");
                }
                if (Hotetype.indexOf("10") > -1) {
                    legends.push("核查中");
                }
                if (Hotetype.indexOf("11") > -1) {
                    legends.push("荒火");
                }
                if (Hotetype.indexOf("12") > -1) {
                    legends.push("其他");
                }
                if (Firelevel.indexOf("1") > -1) {
                    legends.push("一般(VI级)");
                }
                if (Firelevel.indexOf("2") > -1) {
                    legends.push("较大(III级)");
                }
                if (Firelevel.indexOf("3") > -1) {
                    legends.push("重大(II级)");
                }
                if (Firelevel.indexOf("4") > -1) {
                    legends.push("特别重大(I级)");
                }
                if (Firefrom.indexOf("1") > -1) {
                    legends.push("红外相机");
                }
                if (Firefrom.indexOf("2") > -1) {
                    legends.push("电话报警");
                }
                if (Firefrom.indexOf("3") > -1) {
                    legends.push("卫星热点");
                }
                if (Firefrom.indexOf("4") > -1) {
                    legends.push("电子监控");
                }
                if (Firefrom.indexOf("5") > -1) {
                    legends.push("护林员火情");
                }
                if (Firefrom.indexOf("6") > -1) {
                    legends.push("无人机巡护");
                }
                if (Firefrom.indexOf("50") > -1) {
                    legends.push("历史补录");
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
                name: '档案数'
            }
        ],
        series: function () {
            var serie = [];
            if (Hotetype.indexOf("1") > -1) {
                var item =
                    {
                        name: '林火',
                        type: 'bar',
                        stack:'热点类别',
                        data: Hotetype1
                    }
                serie.push(item);
            }

            if (Hotetype.indexOf("2") > -1) {
                var item = {
                    name: '草原火',
                    type: 'bar',
                    stack: '热点类别',
                    data: Hotetype2
                }
                serie.push(item);
            }
            if (Hotetype.indexOf("3") > -1) {
                var item = {
                    name: '计划烧除',
                    type: 'bar',
                    stack: '热点类别',
                    data: Hotetype3
                }
                serie.push(item);
            }
            if (Hotetype.indexOf("4") > -1) {
                var item = {
                    name: '农用火',
                    type: 'bar',
                    stack: '热点类别',
                    data: Hotetype4
                }
                serie.push(item);
            }
            if (Hotetype.indexOf("5") > -1) {
                var item = {
                    name: '炼山',
                    type: 'bar',
                    stack: '热点类别',
                    data: Hotetype5
                }
                serie.push(item);
            }
            if (Hotetype.indexOf("6") > -1) {
                var item = {
                    name: '灌木火',
                    type: 'bar',
                    stack: '热点类别',
                    data: Hotetype6
                }
                serie.push(item);
            }
            if (Hotetype.indexOf("7") > -1) {
                var item = {
                    name: '工矿用火',
                    type: 'bar',
                    stack: '热点类别',
                    data: Hotetype7
                }
                serie.push(item);
            }
            if (Hotetype.indexOf("8") > -1) {
                var item = {
                    name: '境外火',
                    type: 'bar',
                    stack: '热点类别',
                    data: Hotetype8
                }
                serie.push(item);
            }
            if (Hotetype.indexOf("9") > -1) {
                var item = {
                    name: '未找到',
                    type: 'bar',
                    stack: '热点类别',
                    data: Hotetype9
                }
                serie.push(item);
            }
            if (Hotetype.indexOf("10") > -1) {
                var item = {
                    name: '核查中',
                    type: 'bar',
                    stack: '热点类别',
                    data: Hotetype10
                }
                serie.push(item);
            }
            if (Hotetype.indexOf("11") > -1) {
                var item = {
                    name: '荒火',
                    type: 'bar',
                    stack: '热点类别',
                    data: Hotetype11
                }
                serie.push(item);
            }
            if (Hotetype.indexOf("12") > -1) {
                var item = {
                    name: '其他',
                    type: 'bar',
                    stack: '热点类别',
                    data: Hotetype12
                }
                serie.push(item);
            }
            if (Firelevel.indexOf("1") > -1) {
                var item = {
                    name: '一般(VI级)',
                    type: 'bar',
                    data: Firelevel1
                }
                serie.push(item);
            }
            if (Firelevel.indexOf("2") > -1) {
                var item = {
                    name: '较大(III级)',
                    type: 'bar',
                    data: Firelevel2
                }
                serie.push(item);
            }
            if (Firelevel.indexOf("3") > -1) {
                var item = {
                    name: '重大(II级)',
                    type: 'bar',
                    data: Firelevel3
                }
                serie.push(item);
            }
            if (Firelevel.indexOf("4") > -1) {
                var item = {
                    name: '特别重大(I级)',
                    type: 'bar',

                    data: Firelevel4
                }
                serie.push(item);
            }
            if (Firefrom.indexOf("1") > -1) {
                var item = {
                    name: '红外相机',
                    type: 'bar',
                    data: FireFrom1
                }
                serie.push(item);
            }
            if (Firefrom.indexOf("2") > -1) {
                var item = {
                    name: '电话报警',
                    type: 'bar',
                    data: FireFrom2
                }
                serie.push(item);
            }
            if (Firefrom.indexOf("3") > -1) {
                var item = {
                    name: '卫星热点',
                    type: 'bar',
                    data: FireFrom3
                }
                serie.push(item);
            }
            if (Firefrom.indexOf("4") > -1) {
                var item = {
                    name: '电子监控',
                    type: 'bar',
                    data: FireFrom4
                }
                serie.push(item);
            }
            if (Firefrom.indexOf("5") > -1) {
                var item = {
                    name: '护林员火情',
                    type: 'bar',
                    data: FireFrom5
                }
                serie.push(item);
            }
            if (Firefrom.indexOf("6") > -1) {
                var item = {
                    name: '无人机巡护',
                    type: 'bar',
                    data: FireFrom6
                }
                serie.push(item);
            }
            if (Firefrom.indexOf("50") > -1) {
                var item = {
                    name: '历史补录',
                    type: 'bar',
                    data: FireFrom7
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    ArchivalChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    ArchivalChart.setOption(Archivaloption);
}

//队伍个数
function getArchivalCount(TopEchart, Hotetype, Firelevel, Firefrom) {
    ArchivalChart.showLoading();//start loading
     ORGNAMEArr = [];
     Hotetype1 = [];
     Hotetype2 = [];
     Hotetype3 = [];
     Hotetype4 = [];
     Hotetype5 = [];
     Hotetype6 = [];
     Hotetype7 = [];
     Hotetype8 = [];
     Hotetype9 = [];
     Hotetype10 = [];
     Hotetype11 = [];
     Hotetype12 = [];
     Firelevel1 = [];
     Firelevel2 = [];
     Firelevel3 = [];
     Firelevel4 = [];
     FireFrom1 = [];
     FireFrom2 = [];
     FireFrom3 = [];
     FireFrom4 = [];
     FireFrom5 = [];
     FireFrom6 = [];
     FireFrom7 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetArchivalChartSourceData",
        data:
            {
                TopEchart: TopEchart,
                Hotetype: Hotetype,
                Firelevel: Firelevel,
                Firefrom: Firefrom,
            },
        dataType: "json",
        success: function (obj) {
            ArchivalChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            //var CommunicationwayNum1Count = parseInt(content.Communicationway1Count);
                            //var CommunicationwayNum2Count = parseInt(content.Communicationway2Count);
                            //var CommunicationwayNum3Count = parseInt(content.Communicationway3Count);
                            //var UsestateNum1Count = parseInt(content.Usestate1Count);
                            //var UsestateNum2Count = parseInt(content.Usestate2Count);
                            //var UsestateNum3Count = parseInt(content.Usestate3Count);
                            //var ManagerstateNum1Count = parseInt(content.Managerstate1Count);
                            //var ManagerstateNum2Count = parseInt(content.Managerstate2Count);
                            //var ManagerstateNum3Count = parseInt(content.Managerstate3Count);
                            Hotetype1.push(parseInt(content.Hotetype1Count));
                            Hotetype2.push(parseInt(content.Hotetype2Count));
                            Hotetype3.push(parseInt(content.Hotetype3Count));
                            Hotetype4.push(parseInt(content.Hotetype4Count));
                            Hotetype5.push(parseInt(content.Hotetype5Count));
                            Hotetype6.push(parseInt(content.Hotetype6Count));
                            Hotetype7.push(parseInt(content.Hotetype7Count));
                            Hotetype8.push(parseInt(content.Hotetype8Count));
                            Hotetype9.push(parseInt(content.Hotetype9Count));
                            Hotetype10.push(parseInt(content.Hotetype10Count));
                            Hotetype11.push(parseInt(content.Hotetype11Count));
                            Hotetype12.push(parseInt(content.Hotetype12Count));
                            Firelevel1.push(parseInt(content.FireLevelCount));
                            Firelevel2.push(parseInt(content.FireLevel2Count));
                            Firelevel3.push(parseInt(content.FireLevel3Count));
                            Firelevel4.push(parseInt(content.FireLevel4Count));
                            FireFrom1.push(parseInt(content.FireFrom1Count));
                            FireFrom2.push(parseInt(content.FireFrom2Count));
                            FireFrom3.push(parseInt(content.FireFrom3Count));
                            FireFrom4.push(parseInt(content.FireFrom4Count));
                            FireFrom5.push(parseInt(content.FireFrom5Count));
                            FireFrom6.push(parseInt(content.FireFrom6Count));
                            FireFrom7.push(parseInt(content.FireFrom7Count));
                        });
                        console.info(FireFrom2);
                        setArchivalCountMap(TopEchart, Hotetype, Firelevel, Firefrom);
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
            ArchivalChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

