//火险分布图
//var fireData = [

// { name: "qq11", value: [121.15, 31.89, 'wewe'] },
// { name: "qq3", value: [109.781327, 39.608266, 'wewe11'] },
// { name: "qq2", value: [120.38, 37.35, '来源'] },
// { name: "q1q", value: [122.207216, 29.985295, 'sddsd'] }

//];

//var fireData = [{ "name": "2017-06-26护林员报警火情", "value": [117.19101921, 31.83757272] },
//    { "name": "2017-06-23护林员报警火情", "value": [117.177782, 31.820897] }];

var fireData = [];
var bmap;
var app = {};
// 初始化echarts示例mapChart
var mapChart = echarts.init(document.getElementById('mainfiredistribute'));


getFireDistributionDataInfo();
//绘图setMapChart() 
function setMapChart() {
    mapChart.setOption(option = {
        title: {
            text: '红河火情分布点',
            subtext: '截止时间:' + CurentTime(),
            x: 'center',
            subtextStyle: {
                color: '#ff9900'
            }
            ,
            textStyle: {
                color: '#ff6600'
            }
        },
        //toolbox: {
        //    show: true,
        //    orient: 'vertical',
        //    x: 'right',
        //    y: 'center',
        //    feature: {
        //        mark: { show: true },
        //        dataView: { show: true, readOnly: false },
        //        restore: { show: true },
        //        saveAsImage: { show: true }
        //    }
        //},
        tooltip: {
            trigger: 'item',
            formatter: "名称：{b}<br/>经纬度:{c}"
        },
        animation: false,
        bmap: {
            center: [103.41, 23.36],
            zoom: 8,
            // 是否开启拖拽缩放，可以只设置 'scale' 或者 'move'
            roam: true
            // 百度地图的自定义样式，见 http://developer.baidu.com/map/jsdevelop-11.htm
            // mapStyle: {}
        },
        //visualMap: {
        //    show: false,
        //    top: 'top',
        //    min: 0,
        //    max: 5,
        //    seriesIndex: 0,
        //    calculable: true,
        //    inRange: {
        //        color: ['blue', 'blue', 'green', 'yellow', 'red']
        //    }
        //},
        series: [{
            // type: 'heatmap',
            type: 'scatter',
            coordinateSystem: 'bmap',
            data: fireData,
            // hoverAnimation: true,
            symbol: 'image://../images/fireAlarm.gif',
            symbolSize: 20,
            label: {
                normal: {
                    formatter: '{b}',
                    position: 'left',
                    //position: ['50%', '50%'],
                    show: false,
                    textStyle: {
                        color: "red"

                    }
                },
                emphasis: {
                    show: true
                }
            },
            itemStyle: {
                normal: {
                    color: '#f4e925',

                }
            },
            markPoint: {

            }
        }]
    });
    if (!app.inNode) {
        // 添加百度地图插件
        bmap = mapChart.getModel().getComponent('bmap').getBMap();
        bmap.setMapType(BMAP_HYBRID_MAP);
        //bmap.centerAndZoom(new BMap.Point(103.41, 23.36), 8);  // 初始化地图,设置中心点坐标和地图级别
        bmap.addControl(new BMap.MapTypeControl());
        // 添加带有定位的导航控件
        var navigationControl = new BMap.NavigationControl({
            // 靠左上角位置
            anchor: BMAP_ANCHOR_TOP_LEFT,
            // LARGE类型
            type: BMAP_NAVIGATION_CONTROL_LARGE,
            // 启用显示定位
            enableGeolocation: false
        });
        bmap.addControl(navigationControl);
 
        //bmap.setCurrentCity("红河");
        // bmap.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放
    }
}

///gps坐标转百度坐标 todo
function convertZB(ggPoint) {
    var convertor = new BMap.Convertor();
    var pointArr = [];
    pointArr.push(ggPoint);
    convertor.translate(pointArr, 1, 5, translateCallback)
}

//坐标转换完之后的回调函数
translateCallback = function (data) {
    if (data.status === 0) {
        // console.info(data.points[0]);
        //lng
        //lat
    }
}

//获取行政区划边界
function getBoundary(name, weight, color) {
    if (color == "") {
        color = "#ff0000";
    }
    var bdary = new BMap.Boundary();
    bdary.get(name, function (rs) {       //获取行政区域
        //这里是用户自己的函数。  
        //console.info(rs);
        var count = rs.boundaries.length; //行政区域的点有多少个
        for (var i = 0; i < count; i++) {
            var ply = new BMap.Polygon(rs.boundaries[i], { strokeWeight: weight, strokeColor: color, fillColor: "", strokeStyle: "solid" }); //建立多边形覆盖物
            bmap.addOverlay(ply);  //添加覆盖物
            bmap.setViewport(ply.getPath());    //调整视野   
        }
    });
}

//获取火情数据来源
function getFireDistributionDataInfo() {
    mapChart.showLoading();//start loading
    $.ajax({
        type: "Post",
        url: "/BigDataShow/GetFireDisTributionData",
        data: {},
        //async: false,
        dataType: "json",
        success: function (obj) {
            if (obj != null) {
                if (obj.Success) {
                    mapChart.hideLoading();
                    var datalist = obj.DataList;
                    if (datalist != null && datalist.length > 0) {
                        $.each(datalist, function (index, content) {
                            var jwd = [];
                            var dName = content.FIRENAME;
                            var jd = parseFloat(content.JD);
                            jwd.push(jd);
                            var wd = parseFloat(content.WD);
                            jwd.push(wd);
                            var ggPoint = new BMap.Point(jd, wd);
                            convertZB(ggPoint);
                            var oneData = {};
                            var oneData = { name: dName, value: jwd };
                            fireData.push(oneData);
                        });
                    }
                    setMapChart();
                    getBoundary("蒙自", 3, "#0066cc");
                    getBoundary("个旧", 3, "#0066cc");
                    getBoundary("元阳", 3, "#0066cc");
                    getBoundary("绿春", 3, "#0066cc");
                    getBoundary("泸西", 3, "#0066cc");
                    getBoundary("红河州", 5, "");


                }
                else {
                    layer.msg("请检查是否登录！", { icon: 5 });
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            mapChart.hideLoading();
            layer.msg("处理出现错误!状态码：" + textStatus, { icon: 5 });
        },
    });
}
