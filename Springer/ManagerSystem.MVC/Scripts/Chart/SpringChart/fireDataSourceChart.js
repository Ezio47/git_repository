//饼图来源chart --火情数据来源图
var fireChart = echarts.init(document.getElementById("mainfire"));
var datatuli = [];//图例
var datafire = [];//数据

getFireDataCount();

setInterval(getFireDataCount, 12000);

//绘图
function setPieChart() {
    //火情来源数据
    var fireoption = {
        title: {
            text: '火情数据来源',
            subtext: '截止时间:' + CurentTime(),
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            orient: 'vertical',
            left: 'left',
            data: datatuli
        },
        toolbox: {
            show: true,
            feature: {
                dataView: { readOnly: false },
                saveAsImage: {}
            }
        },
        series: [
            {
                name: '火情来源',
                type: 'pie',
                radius: '55%',
                center: ['50%', '60%'],
                data: datafire,
                label: {
                    normal: {
                        show: true,
                        formatter: '{b}: {c}'
                    }
                },
                itemStyle: {
                    emphasis: {
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                }
            }
        ],
        animationType: 'scale',
        animationEasing: 'elasticOut',
        animationDelay: function (idx) {
            return Math.random() * 200;
        }
    };
    // 使用刚指定的配置项和数据显示图表。
    fireChart.setOption(fireoption);
}


//获取火情数据来源
function getFireDataCount() {
    fireChart.showLoading();//start loading
    $.ajax({
        type: "Post",
        url: "/BigDataShow/GetFireSourceData",
        data: {},
        // async: false,
        dataType: "json",
        success: function (obj) {
            if (obj != null) {
                if (obj.Success) {
                    fireChart.hideLoading();
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var dName = content.name;
                            var dValue = parseInt(content.value);
                            var oneData = {};
                            var oneData = { name: dName, value: dValue };
                            datafire.push(oneData);
                            datatuli.push(dName);
                        });
                    }
                    setPieChart();
                    datafire = [];
                    datatuli = [];
                }
                else {
                    layer.msg("【火情数据来源】饼图出现错误,请检查是否登录！", { icon: 5 });
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            fireChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        },
    });
}
