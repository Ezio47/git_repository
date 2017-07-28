//获取url 参数
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

///Js 时间间隔计算(间隔小时)
function GetDateDiff(startDate, endDate) {
    var startTime = new Date(Date.parse(startDate.replace(/-/g, "/"))).getTime();
    var endTime = new Date(Date.parse(endDate.replace(/-/g, "/"))).getTime();
    var dates = Math.abs((startTime - endTime)) / (1000 * 60 * 60);
    return dates;
}

function CurentTime() {
    var now = new Date();

    var year = now.getFullYear();       //年
    var month = now.getMonth() + 1;     //月
    var day = now.getDate();            //日

    var hh = now.getHours();            //时
    var mm = now.getMinutes();          //分
    var ss = now.getSeconds();//秒
    var clock = year + "-";

    if (month < 10)
        clock += "0";

    clock += month + "-";

    if (day < 10)
        clock += "0";

    clock += day + " ";

    if (hh < 10)
        clock += "0";

    clock += hh + ":";
    if (mm < 10) clock += '0';
    clock += mm;
    clock += ":";

    if (day < 10)
        clock += "0";
    clock += ss;
    return (clock);
}

function isIE() { //ie?
    if (!!window.ActiveXObject || "ActiveXObject" in window)
        return true;
    else
        alert("请使用IE打开");
    return false;
}

//时间大小判断
function checkEndTime(startTime, endTime) {
    var start = new Date(startTime.replace("-", "/").replace("-", "/"));
    var end = new Date(endTime.replace("-", "/").replace("-", "/"));
    if (end < start) {
        return false;
    }
    return true;
}

//获取系统时间
function getLocalTime(i) {
    var time;
    var d = new Date();
    time = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
    if (i != 0) {
        var n = new Date(d.getTime() - 86400000 * i);
        time = n.getFullYear() + "-" + (n.getMonth() + 1) + "-" + n.getDate();
    }
    return time;
}


/*获取当前日期*/
function getCurrentDateTime() {
    var d = new Date();
    var year = d.getFullYear();
    var month = d.getMonth() + 1;
    var date = d.getDate();
    var week = d.getDay();
    /*时分秒*/
    /*var hours = d.getHours(); 
    var minutes = d.getMinutes(); 
    var seconds = d.getSeconds(); 
    var ms = d.getMilliseconds();*/
    var curDateTime = year;
    if (month > 9)
        curDateTime = curDateTime + "年" + month;
    else
        curDateTime = curDateTime + "年0" + month;
    if (date > 9)
        curDateTime = curDateTime + "月" + date + "日";
    else
        curDateTime = curDateTime + "月0" + date + "日";
    /*if (hours > 9) 
    curDateTime = curDateTime + " " + hours; 
    else 
    curDateTime = curDateTime + " 0" + hours; 
    if (minutes > 9) 
    curDateTime = curDateTime + ":" + minutes; 
    else 
    curDateTime = curDateTime + ":0" + minutes; 
    if (seconds > 9) 
    curDateTime = curDateTime + ":" + seconds; 
    else 
    curDateTime = curDateTime + ":0" + seconds;*/
    var weekday = "";
    if (week == 0)
        weekday = "星期日";
    else if (week == 1)
        weekday = "星期一";
    else if (week == 2)
        weekday = "星期二";
    else if (week == 3)
        weekday = "星期三";
    else if (week == 4)
        weekday = "星期四";
    else if (week == 5)
        weekday = "星期五";
    else if (week == 6)
        weekday = "星期六";
    curDateTime = curDateTime + " " + weekday;
    return curDateTime;
}



// 本地时钟
function clockon() {
    var now = new Date();
    var year = now.getFullYear(); // getFullYear getYear
    var month = now.getMonth();
    var date = now.getDate();
    var day = now.getDay();
    var hour = now.getHours();
    var minu = now.getMinutes();
    var sec = now.getSeconds();
    var week;
    month = month + 1;
    if (month < 10)
        month = "0" + month;
    if (date < 10)
        date = "0" + date;
    if (hour < 10)
        hour = "0" + hour;
    if (minu < 10)
        minu = "0" + minu;
    if (sec < 10)
        sec = "0" + sec;
    var arr_week = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    week = arr_week[day];
    var time = "";
    time = year + "年" + month + "月" + date + "日" + " " + hour + ":" + minu
			+ ":" + sec + " " + week;

    $("#bgclock").html(time);
    var timer = setTimeout("clockon()", 200);
}



/*获取当前农历*/
function showCal() {
    var D = new Date();
    var yy = D.getFullYear();
    var mm = D.getMonth() + 1;
    var dd = D.getDate();
    var ww = D.getDay();
    var ss = parseInt(D.getTime() / 1000);
    if (yy < 100) yy = "19" + yy;
    return GetLunarDay(yy, mm, dd);
}

//定义全局变量 
var CalendarData = new Array(100);
var madd = new Array(12);
var tgString = "甲乙丙丁戊己庚辛壬癸";
var dzString = "子丑寅卯辰巳午未申酉戌亥";
var numString = "一二三四五六七八九十";
var monString = "正二三四五六七八九十冬腊";
var weekString = "日一二三四五六";
var sx = "鼠牛虎兔龙蛇马羊猴鸡狗猪";
var cYear, cMonth, cDay, TheDate;
CalendarData = new Array(0xA4B, 0x5164B, 0x6A5, 0x6D4, 0x415B5, 0x2B6, 0x957, 0x2092F, 0x497, 0x60C96, 0xD4A, 0xEA5, 0x50DA9, 0x5AD, 0x2B6, 0x3126E, 0x92E, 0x7192D, 0xC95, 0xD4A, 0x61B4A, 0xB55, 0x56A, 0x4155B, 0x25D, 0x92D, 0x2192B, 0xA95, 0x71695, 0x6CA, 0xB55, 0x50AB5, 0x4DA, 0xA5B, 0x30A57, 0x52B, 0x8152A, 0xE95, 0x6AA, 0x615AA, 0xAB5, 0x4B6, 0x414AE, 0xA57, 0x526, 0x31D26, 0xD95, 0x70B55, 0x56A, 0x96D, 0x5095D, 0x4AD, 0xA4D, 0x41A4D, 0xD25, 0x81AA5, 0xB54, 0xB6A, 0x612DA, 0x95B, 0x49B, 0x41497, 0xA4B, 0xA164B, 0x6A5, 0x6D4, 0x615B4, 0xAB6, 0x957, 0x5092F, 0x497, 0x64B, 0x30D4A, 0xEA5, 0x80D65, 0x5AC, 0xAB6, 0x5126D, 0x92E, 0xC96, 0x41A95, 0xD4A, 0xDA5, 0x20B55, 0x56A, 0x7155B, 0x25D, 0x92D, 0x5192B, 0xA95, 0xB4A, 0x416AA, 0xAD5, 0x90AB5, 0x4BA, 0xA5B, 0x60A57, 0x52B, 0xA93, 0x40E95);
madd[0] = 0;
madd[1] = 31;
madd[2] = 59;
madd[3] = 90;
madd[4] = 120;
madd[5] = 151;
madd[6] = 181;
madd[7] = 212;
madd[8] = 243;
madd[9] = 273;
madd[10] = 304;
madd[11] = 334;

function GetBit(m, n) {
    return (m >> n) & 1;
}
//农历转换 
function e2c() {
    TheDate = (arguments.length != 3) ? new Date() : new Date(arguments[0], arguments[1], arguments[2]);
    var total, m, n, k;
    var isEnd = false;
    var tmp = TheDate.getYear();
    if (tmp < 1900) {
        tmp += 1900;
    }
    total = (tmp - 1921) * 365 + Math.floor((tmp - 1921) / 4) + madd[TheDate.getMonth()] + TheDate.getDate() - 38;

    if (TheDate.getYear() % 4 == 0 && TheDate.getMonth() > 1) {
        total++;
    }
    for (m = 0; ; m++) {
        k = (CalendarData[m] < 0xfff) ? 11 : 12;
        for (n = k; n >= 0; n--) {
            if (total <= 29 + GetBit(CalendarData[m], n)) {
                isEnd = true; break;
            }
            total = total - 29 - GetBit(CalendarData[m], n);
        }
        if (isEnd) break;
    }
    cYear = 1921 + m;
    cMonth = k - n + 1;
    cDay = total;
    if (k == 12) {
        if (cMonth == Math.floor(CalendarData[m] / 0x10000) + 1) {
            cMonth = 1 - cMonth;
        }
        if (cMonth > Math.floor(CalendarData[m] / 0x10000) + 1) {
            cMonth--;
        }
    }
}

function GetcDateString() {
    var tmp = "";
    /*显示农历年：（ 如：甲午(马)年 ）*/
    /*tmp+=tgString.charAt((cYear-4)%10); 
    tmp+=dzString.charAt((cYear-4)%12); 
    tmp+="("; 
    tmp+=sx.charAt((cYear-4)%12); 
    tmp+=")年 ";*/
    if (cMonth < 1) {
        tmp += "(闰)";
        tmp += monString.charAt(-cMonth - 1);
    } else {
        tmp += monString.charAt(cMonth - 1);
    }
    tmp += "月";
    tmp += (cDay < 11) ? "初" : ((cDay < 20) ? "十" : ((cDay < 30) ? "廿" : "三十"));
    if (cDay % 10 != 0 || cDay == 10) {
        tmp += numString.charAt((cDay - 1) % 10);
    }
    return tmp;
}

function GetLunarDay(solarYear, solarMonth, solarDay) {
    //solarYear = solarYear<1900?(1900+solarYear):solarYear; 
    if (solarYear < 1921 || solarYear > 2020) {
        return "";
    } else {
        solarMonth = (parseInt(solarMonth) > 0) ? (solarMonth - 1) : 11;
        e2c(solarYear, solarMonth, solarDay);
        return GetcDateString();
    }
}

function trimStart(trimStr) {
    if (!trimStr) { return this; }
    var temp = this;
    while (true) {
        if (temp.substr(0, trimStr.length) != trimStr) {
            break;
        }
        temp = temp.substr(trimStr.length);
    }
    return temp;
}
function trimEnd(trimStr) {
    if (!trimStr) { return this; }
    var temp = this;
    while (true) {
        if (temp.substr(temp.length - trimStr.length, trimStr.length) != trimStr) {
            break;
        }
        temp = temp.substr(0, temp.length - trimStr.length);
    }
    return temp;
}
function trim(trimStr) {
    var temp = trimStr;
    if (!trimStr) { temp = " "; }
    return this.trimStart(temp).trimEnd(temp);
}
//function sleep(numberMillis) {
//    var now = new Date();
//    var exitTime = now.getTime() + numberMillis;
//    while (true) {
//        now = new Date();
//        if (now.getTime() > exitTime) return;
//    }
//}
//$(".date-picker").datepicker({
//    monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
//    monthNamesShort: ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二'],   //月份名称简称，用于选择月份时显示
//    dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
//    dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
//    dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],   //日期名称简称
//    dateFormat: 'yy-mm-dd',   //选中日期后，已这个格式显示
//    changeMonth: true,     //可以选择月份
//    changeYear: true,     //可以选择年份
//    firstDay: 1,         //0为已周日作为一周开始，1为周一作为一周开始，默认是0
//    isRTL: false         //是否从右到左排列
//});

//$.datepicker.regional['zh-CN'] = {
//    clearText: '清除',
//    clearStatus: '清除已选日期',
//    closeText: '关闭',
//    closeStatus: '不改变当前选择',
//    prevText: '<<font face="宋体">上月',
//    prevStatus: '显示上月',
//    prevBigText: '<<',
//    prevBigStatus: '显示上一年',
//    nextText: '下月>',
//    nextStatus: '显示下月',
//    nextBigText: '>>',
//    nextBigStatus: '显示下一年',
//    currentText: '今天',
//    currentStatus: '显示本月',
//    monthNames: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
//    monthNamesShort: ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二'],
//    monthStatus: '选择月份',
//    yearStatus: '选择年份',
//    weekHeader: '周',
//    weekStatus: '年内周次',
//    dayNames: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
//    dayNamesShort: ['周日', '周一', '周二', '周三', '周四', '周五', '周六'],
//    dayNamesMin: ['日', '一', '二', '三', '四', '五', '六'],
//    dayStatus: '设置 DD 为一周起始',
//    dateStatus: '选择 m月 d日, DD',
//    dateFormat: 'yy-mm-dd',
//    firstDay: 1,
//    initStatus: '请选择日期',
//    isRTL: false
//};