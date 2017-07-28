var ORGNAMEArr = [];
var Onstate1 = [];
var Onstate2 = [];
var Sextype1 = [];
var Sextype2 = [];
//绘图 队伍人数
function setHUCountMap(TopEchart, Sextype, Onstate) {
    var HUoption = {
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
                if (Sextype.indexOf("0") > -1) {
                    legends.push("男");
                }
                if (Sextype.indexOf("1") > -1) {
                    legends.push("女");
                }
                if (Onstate.indexOf("1") > -1) {
                    legends.push("固职");
                }
                if (Onstate.indexOf("2") > -1) {
                    legends.push("兼职");
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
                name: '护林员数'
            }
        ],
        series: function () {
            var serie = [];
            if (Sextype.indexOf("0") > -1) {
                var item =
                    {
                        name: '男',
                        type: 'bar',
                        data: Sextype2
                    }
                serie.push(item);
            }

            if (Sextype.indexOf("1") > -1) {
                var item = {
                    name: '女',
                    type: 'bar',
                    data: Sextype1
                }
                serie.push(item);
            }
            if (Onstate.indexOf("1") > -1) {
                var item = {
                    name: '固职',
                    type: 'bar',
                    data: Onstate1
                }
                serie.push(item);
            }
            if (Onstate.indexOf("2") > -1) {
                var item = {
                    name: '兼职',
                    type: 'bar',
                    data: Onstate2
                }
                serie.push(item);
            }
          
            
            return serie;
        }()
    }
    HUChart.clear();
    // 使用刚指定的配置项和数据显示图表。
    HUChart.setOption(HUoption);
}

//队伍个数
function getHUCount(TopEchart, Sextype, Onstate) {
    HUChart.showLoading();
     ORGNAMEArr = [];
     Onstate1 = [];
     Onstate2 = [];
     Sextype1 = [];
    Sextype2 = [];
    $.ajax({
        type: "Post",
        url: "/DataCenterDataShow/GetHUSourceData",
        data:
            {
                TopEchart: TopEchart,
                Sextype: Sextype,
                Onstate: Onstate,
            },
        dataType: "json",
        success: function (obj) {
            HUChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var ORGNAME = content.ORGName;
                            ORGNAMEArr.push(ORGNAME);
                            var data1 = parseInt(content.Sex0Count);
                            var data2 = parseInt(content.Sex1Count);
                            var data3 = parseInt(content.Onstate0Count);
                            var data4 = parseInt(content.Onstate1Count);
                            Sextype1.push(data1);
                            Sextype2.push(data2);
                            Onstate1.push(data3);
                            Onstate2.push(data4);
                        });
                        setHUCountMap(TopEchart, Sextype, Onstate);//车辆数
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
            HUChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

