var ORGNAMEArr = [];
var ReType1 = [];
var ReArea1 = [];
var ReType2 = [];
var ReArea2 = [];
var ReType3 = [];
var ReArea3 = [];
var ReType4 = [];
var ReArea4 = [];
var AgeType1 = [];
var AgeArea1 = [];
var AgeType2 = [];
var AgeArea2 = [];
var AgeType3 = [];
var AgeArea3 = [];
var AgeType4 = [];
var AgeArea4 = [];
var AgeType5 = [];
var AgeArea5 = [];
var OrType1 = [];
var OrArea1 = [];
var OrType2 = [];
var OrArea2 = [];
var BuType1 = [];
var BuArea1 = [];
var BuType2 = [];
var BuArea2 = [];
var TrType1 = [];
var TrArea1 = [];
var TrType2 = [];
var TrArea2 = [];
var TrType3 = [];
var TrArea3 = [];
function setResourseCountMap(TopEchart, Resourcetype, Agetype, Originttype, Burntype, Treetype) {
    var Resourseoption = {
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

                if (Resourcetype.indexOf("1") > -1) {
                    legends.push("重点林区");
                }
                if (Resourcetype.indexOf("2") > -1) {
                    legends.push("有林地");
                }
                if (Resourcetype.indexOf("3") > -1) {
                    legends.push("荒山");
                }
                if (Resourcetype.indexOf("4") > -1) {
                    legends.push("灌丛林");
                }
                if (Agetype.indexOf("1") > -1) {
                    legends.push("幼龄林");
                }
                if (Agetype.indexOf("2") > -1) {
                    legends.push("中龄林");
                }
                if (Agetype.indexOf("3") > -1) {
                    legends.push("近熟林");
                }
                if (Agetype.indexOf("4") > -1) {
                    legends.push("成熟林");
                }
                if (Agetype.indexOf("5") > -1) {
                    legends.push("过熟林");
                }
                if (Originttype.indexOf("1") > -1) {
                    legends.push("天然");
                }
                if (Originttype.indexOf("2") > -1) {
                    legends.push("人工");
                }
                if (Burntype.indexOf("1") > -1) {
                    legends.push("易燃");
                }
                if (Burntype.indexOf("2") > -1) {
                    legends.push("不易燃");
                }
                if (Treetype.indexOf("1") > -1) {
                    legends.push("针叶林");
                }
                if (Treetype.indexOf("2") > -1) {
                    legends.push("阔叶林");
                }
                if (Treetype.indexOf("3") > -1) {
                    legends.push("混交林");
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
                name: '资源数'
            }
        ],
        series: function () {
            var serie = [];
            if (Resourcetype.indexOf("1") > -1) {
                var item =
                    {
                        name: '重点林区',
                        type: 'bar',
                        data: ReType1
                    }
                serie.push(item);
            }
            if (Resourcetype.indexOf("2") > -1) {
                var item = {
                    name: '有林地',
                    type: 'bar',
                    data: ReType2
                }
                serie.push(item);
            }
            if (Resourcetype.indexOf("3") > -1) {
                var item = {
                    name: '荒山',
                    type: 'bar',
                    data: ReType3
                }
                serie.push(item);
            }
            if (Resourcetype.indexOf("4") > -1) {
                var item = {
                    name: '灌丛林',
                    type: 'bar',
                    data: ReType4
                }
                serie.push(item);
            }

            if (Agetype.indexOf("1") > -1) {
                var item = {
                    name: '幼龄林',
                    type: 'bar',
                    data: AgeType1
                }
                serie.push(item);
            }
            if (Agetype.indexOf("2") > -1) {
                var item = {
                    name: '中龄林',
                    type: 'bar',
                    data: AgeType2
                }
                serie.push(item);
            }
            if (Agetype.indexOf("3") > -1) {
                var item = {
                    name: '近熟林',
                    type: 'bar',
                    data: AgeType3
                }
                serie.push(item);
            }
            if (Agetype.indexOf("4") > -1) {
                var item = {
                    name: '成熟林',
                    type: 'bar',
                    data: AgeType4
                }
                serie.push(item);
            }
            if (Agetype.indexOf("5") > -1) {
                var item = {
                    name: '过熟林',
                    type: 'bar',
                    data: AgeType5
                }
                serie.push(item);
            }

            if (Originttype.indexOf("1") > -1) {
                var item = {
                    name: '天然',
                    type: 'bar',
                    data: OrType1
                }
                serie.push(item);
            }
            if (Originttype.indexOf("2") > -1) {
                var item = {
                    name: '人工',
                    type: 'bar',
                    data: OrType2
                }
                serie.push(item);
            }
            if (Treetype.indexOf("1") > -1) {
                var item = {
                    name: '针叶林',
                    type: 'bar',
                    data: TrType1
                }
                serie.push(item);
            }
            if (Treetype.indexOf("2") > -1) {
                var item = {
                    name: '阔叶林',
                    type: 'bar',
                    data: TrType2
                }
                serie.push(item);
            }
            if (Treetype.indexOf("3") > -1) {
                var item = {
                    name: '混交林',
                    type: 'bar',
                    data: TrType3
                }
                serie.push(item);
            }

            if (Burntype.indexOf("1") > -1) {
                var item = {
                    name: '易燃',
                    type: 'bar',
                    data: BuType1
                }
                serie.push(item);
            }
            if (Burntype.indexOf("2") > -1) {
                var item = {
                    name: '不易燃',
                    type: 'bar',
                    data: BuType2
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    ResourseChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    ResourseChart.setOption(Resourseoption);
}

function setResourseCountMap1(TopEchart, Resourcetype, Agetype, Originttype, Burntype, Treetype) {
    var Resourseoption = {
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

                if (Resourcetype.indexOf("1") > -1) {
                    legends.push("重点林区");
                }
                if (Resourcetype.indexOf("2") > -1) {
                    legends.push("有林地");
                }
                if (Resourcetype.indexOf("3") > -1) {
                    legends.push("荒山");
                }
                if (Resourcetype.indexOf("4") > -1) {
                    legends.push("灌丛林");
                }
                if (Agetype.indexOf("1") > -1) {
                    legends.push("幼龄林");
                }
                if (Agetype.indexOf("2") > -1) {
                    legends.push("中龄林");
                }
                if (Agetype.indexOf("3") > -1) {
                    legends.push("近熟林");
                }
                if (Agetype.indexOf("4") > -1) {
                    legends.push("成熟林");
                }
                if (Agetype.indexOf("5") > -1) {
                    legends.push("过熟林");
                }
                if (Originttype.indexOf("1") > -1) {
                    legends.push("天然");
                }
                if (Originttype.indexOf("2") > -1) {
                    legends.push("人工");
                }
                if (Burntype.indexOf("1") > -1) {
                    legends.push("易燃");
                }
                if (Burntype.indexOf("2") > -1) {
                    legends.push("不易燃");
                }
                if (Treetype.indexOf("1") > -1) {
                    legends.push("针叶林");
                }
                if (Treetype.indexOf("2") > -1) {
                    legends.push("阔叶林");
                }
                if (Treetype.indexOf("3") > -1) {
                    legends.push("混交林");
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
                name: '面积(公顷)'
            }
        ],
        series: function () {
            var serie = [];
            if (Resourcetype.indexOf("1") > -1) {
                var item =
                    {
                        name: '重点林区',
                        type: 'bar',
                        data: ReArea1
                    }
                serie.push(item);
            }
            if (Resourcetype.indexOf("2") > -1) {
                var item = {
                    name: '有林地',
                    type: 'bar',
                    data: ReArea2
                }
                serie.push(item);
            }
            if (Resourcetype.indexOf("3") > -1) {
                var item = {
                    name: '荒山',
                    type: 'bar',
                    data: ReArea3
                }
                serie.push(item);
            }
            if (Resourcetype.indexOf("4") > -1) {
                var item = {
                    name: '灌丛林',
                    type: 'bar',
                    data: ReArea4
                }
                serie.push(item);
            }

            if (Agetype.indexOf("1") > -1) {
                var item = {
                    name: '幼龄林',
                    type: 'bar',
                    data: AgeArea1
                }
                serie.push(item);
            }
            if (Agetype.indexOf("2") > -1) {
                var item = {
                    name: '中龄林',
                    type: 'bar',
                    data: AgeArea2
                }
                serie.push(item);
            }
            if (Agetype.indexOf("3") > -1) {
                var item = {
                    name: '近熟林',
                    type: 'bar',
                    data: AgeArea3
                }
                serie.push(item);
            }
            if (Agetype.indexOf("4") > -1) {
                var item = {
                    name: '成熟林',
                    type: 'bar',
                    data: AgeArea4
                }
                serie.push(item);
            }
            if (Agetype.indexOf("5") > -1) {
                var item = {
                    name: '过熟林',
                    type: 'bar',
                    data: AgeArea5
                }
                serie.push(item);
            }

            if (Originttype.indexOf("1") > -1) {
                var item = {
                    name: '天然',
                    type: 'bar',
                    data: OrArea1
                }
                serie.push(item);
            }
            if (Originttype.indexOf("2") > -1) {
                var item = {
                    name: '人工',
                    type: 'bar',
                    data: OrArea2
                }
                serie.push(item);
            }
            if (Treetype.indexOf("1") > -1) {
                var item = {
                    name: '针叶林',
                    type: 'bar',
                    data: TrArea1
                }
                serie.push(item);
            }
            if (Treetype.indexOf("2") > -1) {
                var item = {
                    name: '阔叶林',
                    type: 'bar',
                    data: TrArea2
                }
                serie.push(item);
            }
            if (Treetype.indexOf("3") > -1) {
                var item = {
                    name: '混交林',
                    type: 'bar',
                    data: TrArea3
                }
                serie.push(item);
            }

            if (Burntype.indexOf("1") > -1) {
                var item = {
                    name: '易燃',
                    type: 'bar',
                    data: BuArea1
                }
                serie.push(item);
            }
            if (Burntype.indexOf("2") > -1) {
                var item = {
                    name: '不易燃',
                    type: 'bar',
                    data: BuArea2
                }
                serie.push(item);
            }
            return serie;
        }()
    }
    ResourseChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    ResourseChart.setOption(Resourseoption);
}
//个数
function getResourseCount(TopEchart, Resourcetype, Agetype, Originttype, Burntype, Treetype, Option) {
    ResourseChart.showLoading();//start loading
    ORGNAMEArr = [];
    ReType1 = [];
    ReArea1 = [];
    ReType2 = [];
    ReArea2 = [];
    ReType3 = [];
    ReArea3 = [];
    ReType4 = [];
    ReArea4 = [];
    AgeType1 = [];
    AgeArea1 = [];
    AgeType2 = [];
    AgeArea2 = [];
    AgeType3 = [];
    AgeArea3 = [];
    AgeType4 = [];
    AgeArea4 = [];
    AgeType5 = [];
    AgeArea5 = [];
    OrType1 = [];
    OrArea1 = [];
    OrType2 = [];
    OrArea2 = [];
    BuType1 = [];
    BuArea1 = [];
    BuType2 = [];
    BuArea2 = [];
    TrType1 = [];
    TrArea1 = [];
    TrType2 = [];
    TrArea2 = [];
    TrType3 = [];
    TrArea3 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetResourseChartSourceData",
        data:
            {
                TopEchart: TopEchart,
                Resourcetype: Resourcetype,
                Agetype: Agetype,
                Originttype: Originttype,
                Burntype: Burntype,
                Treetype: Treetype,
            },
        dataType: "json",
        success: function (obj) {
            ResourseChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var date1 = Number(content.Resourcetype1Count);
                            var date2 = Number(content.Resourcetype2Count);
                            var date3 = Number(content.Resourcetype3Count);
                            var date4 = Number(content.Resourcetype4Count);
                            var date5 = Number(content.Agetype1Count);
                            var date6 = Number(content.Agetype2Count);
                            var date7 = Number(content.Agetype3Count);
                            var date8 = Number(content.Agetype4Count);
                            var date9 = Number(content.Agetype5Count);
                            var date10 = Number(content.Originttype1Count);
                            var date11 = Number(content.Originttype2Count);
                            var date12 = Number(content.Burntype1Count);
                            var date13 = Number(content.Burntype2Count);
                            var date14 = Number(content.Treetype1Count);
                            var date15 = Number(content.Treetype2Count);
                            var date16 = Number(content.Treetype3Count);
                            ReType1.push(date1);
                            ReArea1.push(content.ResourcetypeArea1Count);
                            ReType2.push(date2);
                            ReArea2.push(content.ResourcetypeArea2Count);
                            ReType3.push(date3);
                            ReArea3.push(content.ResourcetypeArea3Count);
                            ReType4.push(date4);
                            ReArea4.push(content.ResourcetypeArea4Count);
                            AgeType1.push(date5);
                            AgeArea1.push(content.AgetypeArea1Count);
                            AgeType2.push(date6);
                            AgeArea2.push(content.AgetypeArea2Count);
                            AgeType3.push(date7);
                            AgeArea3.push(content.AgetypeArea3Count);
                            AgeType4.push(date8);
                            AgeArea4.push(content.AgetypeArea4Count);
                            AgeType5.push(date9);
                            AgeArea5.push(content.AgetypeArea5Count);
                            OrType1.push(date10);
                            OrArea1.push(content.OriginttypeArea1Count);
                            OrType2.push(date11);
                            OrArea2.push(content.OriginttypeArea2Count);
                            BuType1.push(date12);
                            BuArea1.push(content.BurntypeArea1Count);
                            BuType1.push(date13);
                            BuArea2.push(content.BurntypeArea2Count);
                            TrType1.push(date14);
                            TrArea1.push(content.TreetypeArea1Count);
                            TrType2.push(date15);
                            TrArea2.push(content.TreetypeArea2Count);
                            TrType3.push(date16);
                            TrArea3.push(content.TreetypeArea3Count);
                        });
                        if (Option == "2") {
                            setResourseCountMap(TopEchart, Resourcetype, Agetype, Originttype, Burntype, Treetype);
                        }
                        if (Option == "1") {
                            setResourseCountMap1(TopEchart, Resourcetype, Agetype, Originttype, Burntype, Treetype);
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
            ResourseChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

