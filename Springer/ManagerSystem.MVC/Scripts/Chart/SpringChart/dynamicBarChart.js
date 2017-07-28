//动态柱状图--护林员监测

// 基于准备好的dom，初始化echarts实例  护林员
var hlyChart = echarts.init(document.getElementById('mainhly'));
var dataonline = [];//在线人员
var dataunline = [];//离线人员
var dataoutline = [];//出围人员
var hlyoption;
var index = 0;
getHlyStateCount();
//绘图
//function setBarChart() {
// 指定护林员图表的配置项和数据
hlyoption = {
    title: {
        text: '护林员实时监控',
        textStyle: {
            color: 'green',
            fontStyle: 'oblique',
            fontSize: 15
        }
    },
    tooltip: {
        trigger: 'axis',
        axisPointer: {            // 坐标轴指示器，坐标轴触发有效
            type: 'line'        // 默认为直线，可选为：'line' | 'shadow'
        }
    },
    legend: {
        data: ['在线', '离线', '出围']
    },
    color: ['green', 'red', '#cbce1a'],
    toolbox: {
        show: true,
        feature: {
            magicType: {
                type: ['line', 'stack', 'tiled']
            },
            dataView: { readOnly: false },
            restore: {},
            saveAsImage: {}
        }
    },
    dataZoom: {
        show: true,
        start: 0,
        end: 100
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
            boundaryGap: true,
            data: (function () {
                var now = new Date();
                var res = [];
                var len = 10;
                while (len--) {
                    res.unshift(now.toLocaleTimeString().replace(/^\D*/, ''));
                    now = new Date(now - 2000);
                }
                return res;
            })()
        }
    ],
    yAxis: [
        {
            type: 'value',
            name: '实时人数'
        }
    ],
    series: [
        {
            name: '在线',
            type: 'bar',
            label: {
                normal: {
                    show: true,
                    position: 'top',
                    formatter: '{c}'
                }
            },
            data: dataonline
        },
        {
            name: '离线',
            type: 'bar',
            label: {
                normal: {
                    show: true,
                    position: 'top',
                    formatter: '{c}'
                }
            },
            data: dataunline
        },
        {
            name: '出围',
            type: 'bar',
            label: {
                normal: {
                    show: true,
                    position: 'top',
                    formatter: '{c}'
                }
            },
            data: dataoutline
        }
    ]
};
//}

//需要定时的操作
function intervalHlyFun() {
    //  alert('1');
    //var data0 = option.series[0].data;
    // data0.shift();
    //data0.push(Math.round(Math.random() * 1000));
    getHlyStateCount();
    if (index > 10) {
        var axisData = (new Date()).toLocaleTimeString().replace(/^\D*/, '');
        hlyoption.xAxis[0].data.shift();
        hlyoption.xAxis[0].data.push(axisData);
    }

    // 使用刚指定的配置项和数据显示图表。
    hlyChart.setOption(hlyoption);
}

//获取在线 离线 出围 人员数
function getHlyStateCount() {
    hlyChart.showLoading();//start loading
    $.ajax({
        type: "Post",
        url: "/MainDefault/GetHlyInfoCountJson",
        data: {},
        //async: false,
        dataType: "json",
        success: function (obj) {
            if (obj != null) {
                if (obj.Success) {
                    var data = obj.Data;
                    if (data != null) {
                        dataonline.push(data.LineInCount);
                        dataunline.push(data.LineOutCount);
                        dataoutline.push(data.LineOutRouteCount);
                        //alert(dataunline.length);
                        if (dataonline.length > 20) {
                            dataonline.remove(0);
                        }
                        if (dataunline.length > 20) {
                            dataunline.remove(0);
                        }
                        if (dataoutline.length > 20) {
                            dataoutline.remove(0);
                        }
                    }
                    hlyChart.hideLoading();
                    //setBarChart();
                    //InervalFun();
                    //alert("前===" + index);
                    index++;
                    // alert("后===" + index);

                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            hlyChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        },
    });
}
//定时执行

setInterval(intervalHlyFun, 4100);
//function InervalFun() {
//    alert(index);
//    if (index == 1) {
//        setInterval(intervalHlyFun, 4100);
//    }
//}
