//邮箱验证
function CheckMail(mail) {
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (filter.test(mail)) return true;
    else {
        return false;
    }
}
//验证数字带小数点
function CheckNUM(number) {
    var filter = /^\d+(\.\d+)?$/
    if (filter.test($.trim(number))) return true;
    else {
        return false;
    }
}
//验证数字带小数点(最多保留2位有效小数)
function CheckNUM2(number) {
    var filter = /^\d+\.?\d{0,2}$/
    if (filter.test($.trim(number))) return true;
    else {
        return false;
    }
}
//验证数字整数
function CheckINT(number) {
    var filter = /^\+?[1-9][0-9]*$/;
    if (filter.test($.trim(number))) return true;
    else {
        return false;
    }
}
//手机号码验证
function checkPhone(phone) {
    var filter = /^1[3|4|5|7|8]\d{9}$/;
    if (!(filter.test(phone))) {
        return false;
    }
    else {
        return true;
    }
}
//邮编验证
function checkPost(post) {
    var filter = /^[1-9][0-9]{5}$/;
    if (!(filter.test(post))) {
        return false;
    }
    else {
        return true;
    }
}
//传真验证
function checkFax(fax) {
    var filter = /^(\d{3,4}-)?\d{7,8}$/;
    if (!(filter.test(fax))) {
        return false;
    }
    else {
        return true;
    }
}
///验证字符串长度不能超过一百
function checkStr(str) {
    var filter = /^\S{1,50}$/;
    if (!(filter.test(str))) {
        return false;
    }
    else {
        return true;
    }
}
//固定电话验证
function checkTel(tel) {
    // var filter = /^(\(\d{3,4}\)|\d{3,4}-|\s)?\d{7,14}$/;
    var filter = /^((0\d{2,3}-\d{7,8})|(1[3584]\d{9}))$/;
    //var filter = /\d{3}-\d{8}|\d{4}-\d{7}/;
    if (!(filter.test(tel))) {
        return false;
    }
    else {
        return true;
    }
}
//固身份证号码验证
function checkIDCard(card) {
    var filter = /(^\d{15}$)|(^\d{17}([0-9]|X)$)/;
    if (filter.test(card)) return true;
    else {
        return false;
    }
}
// 判断输入是否是一个数字--(数字包含小数)--
function isNumber(str) {
    return !isNaN(str);
}
// 判断输入是否是一个整数
function isInt(str) {
    var result = str.match(/^(-|\+)?\d+$/);
    if (result == null) return false;
    return true;
}
// 返回字符串的实际长度, 一个汉字算2个长度
function strLen(str) {
    return str.replace(/[^\x00-\xff]/g, "**").length;
}
// 判断输入是否是有效的长日期格式 - "YYYY-MM-DD HH:MM:SS" || "YYYY/MM/DD HH:MM:SS"
function isDatetime(str) {
    var result = str.match(/^(\d{4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})$/);
    if (result == null) return false;
    var d = new Date(result[1], result[3] - 1, result[4], result[5], result[6], result[7]);
    return (d.getFullYear() == result[1] && (d.getMonth() + 1) == result[3] && d.getDate() == result[4] && d.getHours() == result[5] && d.getMinutes() == result[6] && d.getSeconds() == result[7]);
}
// 检查是否为 YYYY-MM-DD || YYYY/MM/DD 的日期格式
function isDate(str) {
    var result = str.match(/^(\d{4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
    if (result == null) return false;
    var d = new Date(result[1], result[3] - 1, result[4]);
    return (d.getFullYear() == result[1] && d.getMonth() + 1 == result[3] && d.getDate() == result[4]);
}