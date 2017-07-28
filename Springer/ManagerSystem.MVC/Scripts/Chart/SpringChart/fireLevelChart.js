//火险等级图
// 基于准备好的dom，初始化echarts实例  
var levelChart = echarts.init(document.getElementById('mainfirelevel'));
var levelData = [];
// JS
getFileLevel();

//绘图
function setFireLevelMap() {
    var levleoption = {
        title: {
            text: '红河州火险等级',
            subtext: '当前火险等级时间：' + new Date().Format("yyyy年MM月DD日"),
            x: 'center'
        },
        tooltip: {
            trigger: 'item',
            formatter: '{b}<br/>等级：{c}级'
        },
        toolbox: {
            show: true,
            orient: 'vertical',
            left: 'right',
            top: 'center',
            feature: {
                dataView: { readOnly: false },
                restore: {},
                saveAsImage: {}
            }
        },
        visualMap: {
            min: 1,
            max: 5,
            text: ['高', '低'],
            realtime: false,
            calculable: true,
            inRange: {
                color: ['green', 'blue', 'yellow', 'orange', 'red']
            }
        },
        series: [{
            type: 'map',
            map: 'honghe',
            itemStyle: {
                normal: { label: { show: true } },
                emphasis: { label: { show: true } }
            },
            data: levelData
        }]
    };

    // 使用刚指定的配置项和数据显示图表。
    levelChart.setOption(levleoption);
}

function getFileLevel() {
    levelChart.showLoading();//start loading
    $.ajax({
        type: "Post",
        url: "/BIgDataShow/GetFireLevelData",
        data: {},
        dataType: "json",
        success: function (obj) {
            levelChart.hideLoading();
            if (obj != null) {
                if (obj.Success) {
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                       // console.info(datalist);
                        $.each(datalist, function (index, content) {
                            var dName = content.name;
                            var dValue = content.value;
                            var oneData = {};
                            var oneData = { name: dName, value: dValue };
                            levelData.push(oneData);
                        });
                        setFireLevelMap();
                    }
                    else {
                        layer.msg("火险等级暂无最新数据！", { icon: 5 });
                    }
                }
                else {
                    layer.msg("火险等级暂无最新数据！", { icon: 5 });
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            levelChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        }
    })
}

