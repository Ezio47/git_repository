/// <reference path="../_references.js" />

//layer.open({
//    type: 1,
//    title: 'test',
//    content: '传入任意的文本或html' //这里content是一个普通的String
//});

//Ajax获取 (火情处理)
function getAjaxFileInfo(url, firetype, id) {
    $.post(url, { firetype: firetype, id: id }, function (str) {
        layer.open({
            type: 1,
            title: '火情信息管理---【' + str.Url + '】',
            area: ['850px', '480px'],
            content: str.Msg,//注意，如果str是object，那么需要字符拼接。
            shadeClose: false,
            btn: ['保存', "取消"],
            yes: function (index) {
                var fireid = $('#fireid').val();//记录编号
                var firewxno = $('#firewxno').val(); //卫星编号
                var firehotno = $('#firehotno').val(); //热点编号
                var firename = $('#firename').val();//火情名称
                var firefrom = $('#firefrom').val();//火情来源
                var area = $('#area').val();//面积
                var firetime = $('#firetime').val();//起火时间
                var jd = $('#jd').val();//经度
                var wd = $('#wd').val();//纬度
                var firedl = $('#firedl').val(); //地类
                var fireyy = $('#fireyy').val();//烟云
                var firejxhqus = $('#firejxhqus').val();//连续火
                var fireaddress = $('#fireaddress').val();//火灾发生地
                var firenote = $('#firenote').val();  //备注
                var checktype = $('input[name="identity"]:checked ').val();// 获取被选中按钮的值
                var bo = checkFireInfo();
                if (true) {
                    $.ajax({
                        type: "Post",
                        url: "/JCFireInfo/ConvertFireInfo",
                        data: {
                            fireid: fireid, firewxno: firewxno, firehotno: firehotno, firename: firename, firefrom: firefrom,
                            area: area, firetime: firetime, jd: jd, wd: wd, firedl: firedl, fireyy: fireyy, firejxhqus: firejxhqus,
                            fireaddress: fireaddress, firenote: firenote, checktype: checktype, firetype: firetype
                        },
                        dataType: "json",
                        success: function (obj) {
                            if (obj != null && obj.Success) {
                                layer.closeAll();
                                if (firetype == "1") {//红外相机
                                    searchPhotoData();
                                }
                                if (firetype == "4") {//电子监控
                                    searchMonitorData();
                                }

                                layer.msg(obj.Msg, { time: 2000 });
                            }
                            else {
                                layer.alert(obj.Msg, { icon: 5 });
                            }
                        }
                    });
                }
            },
            cancel: function (index) {
                layer.closeAll();
            }
        });
    });

};

//转火情check
function checkFireInfo() {
    var firename = $('#firename').val();//火情名称
    var fireaddress = $('#fireaddress').val();//火灾发生地
    if ($.trim(firename) == "") {
        layer.alert('火情名称不可为空', { icon: 2 });
        $('#firename').focus();
        return false;
    }
    if ($.trim(fireaddress) == "") {
        layer.alert('火灾发生地不可为空', { icon: 2 });
        $('#fireaddress').focus();
        return false;
    }
    return true;
}

//Ajax获取html (火情反馈与审核)
function getAjaxFKFireInfo(url, jcfid, ftype, statetype, refreshtype) {
    //$.post(url, { jcfid: jcfid }, function (str) {
    $.ajax({
        type: "Post",
        url: url,
        data: { jcfid: jcfid },
        dataType: "json",
        //async: false, //默认为true 异步 
        success: function (str) {
            layer.open({
                type: 1,
                title: '火情反馈',
                //area: ['900px', '550px'],
                area: ['70%','75%'],
                content: str.Msg,//注意，如果str是object，那么需要字符拼接。
                shadeClose: false,
                btn: ['反馈', "取消"],
                success: function (layero, index) {
                    setReport();
                },
                yes: function (index) {
                    var bo = checkFkFireInfo();
                    if (bo) {
                        var dl = $('#dl').val();//地类
                        var forestname = $('#forestname').val();//林区名称
                        var forestfiretype = $('#forestfiretype').val();//林火类别
                        var fueltype = $('#fueltype').val();//可燃物类别
                        var hottype = $('#hottype').val();//热点类别
                        var checktime = $('#checktime').val();//核查日期
                        var yy = $('#yy').val();//烟云
                        var jxhqsj = $('#jxhqsj').val();//是否连续
                        var firebegintime = $('#firebegintime').val();//起火时间
                        var fireendtime = $('#fireendtime').val(); //灭火时间
                        var ckbo = $('#chk').is(':checked');//是否已灭
                        var chk = "0";
                        if (ckbo) {
                            chk = "1";
                        }
                        var shyj = $('input[name="shyj"]:checked').val();//0 审核未通过 1 审核通过
                        var txtreson = "";
                        if (shyj == "0") {
                            txtreson = $('#shyjyy').val();//不通过意见
                        }
                        var burnedarea = $('#burnedarea').val();//过火面积
                        var overdoarea = $('#overdoarea').val();//过山林地面积
                        var lostforestarea = $('#lostforestarea').val();//受害森林面积
                        var fireintro = $('#fireintro').val();//情况简介
                        var elselossintro = $('#elselossintro').val();//其他损失情况
                        var hqaddress = $('#hqaddress').val();//实际发生地
                        var hqjd = $('#hqjd').val();//实际经度
                        var hqwd = $('#hqwd').val();//实际纬度
                        $.ajax({
                            type: "Post",
                            url: "/MainYJJC/FKMethod",
                            data: {
                                jcid: jcfid, statetype: statetype, dl: dl, forestname: forestname, forestfiretype: forestfiretype,
                                fueltype: fueltype, hottype: hottype, checktime: checktime, yy: yy, jxhqsj: jxhqsj, firebegintime: firebegintime,
                                fireendtime: fireendtime, chk: chk, burnedarea: burnedarea, overdoarea: overdoarea, lostforestarea: lostforestarea,
                                fireintro: fireintro, elselossintro: elselossintro, shyj: shyj, txtreson: txtreson, hqaddress: hqaddress, hqjd: hqjd,
                                hqwd: hqwd
                            },
                            dataType: "json",
                            //async: false, //默认为true 异步 
                            success: function (obj) {
                                if (obj != null && obj.Success) {
                                    layer.msg('操作成功！', { icon: 6, time: 2000 });
                                    //if (statetype == "4") {
                                    //    layer.msg('反馈成功！', { icon: 6, time: 2000 });//乡镇反馈
                                    //}
                                    //else if (statetype == "5") {
                                    //    layer.msg('审核不通过！', { icon: 6, time: 2000 });//县审核不通过
                                    //}
                                    //else if (statetype == "6") {
                                    //    layer.msg('审核通过！', { icon: 6, time: 2000 });//县审核通过
                                    //}
                                    //else if (statetype == "7") {
                                    //    layer.msg('审核不通过！', { icon: 6, time: 2000 });//市审核不通过
                                    //}
                                    //else if (statetype == "8") {
                                    //    layer.msg('审核通过！', { icon: 6, time: 2000 });//市审核通过
                                    //}
                                    layer.close(index); //如果设定了yes回调，需进行手工关闭
                                }
                                else {
                                    layer.alert(obj.Msg, { icon: 5 });
                                }
                                if (refreshtype == "") {
                                    FireAjax(ftype);//重载
                                }
                                else {
                                    window.location.reload();//页面重载
                                }

                            }
                        });
                    }
                },
                cancel: function (index) {
                    layer.closeAll();
                }
            });
        }
    });
}
//火情反馈check
function checkFkFireInfo() {
    var reg = /^[1-9]\d*\.\d*|0\.\d*[1-9]\d*|0?\.0+|0$/;//非负整数
    var regnum = /^[0-9]*$/;//验证数字

    //实际发生地点
    var hqaddress = $('#hqaddress').val();
    if ($.trim(hqaddress) == "") {
        layer.alert('火情实际发生地址不可为空', { icon: 2 });
        $('#hqaddress').focus();
        return false;
    }
    //实际经纬度
    var hqjd = $('#hqjd').val();
    if ($.trim(hqjd) == "") {
        layer.alert('经度不可为空', { icon: 2 });
        $('#hqjd').focus();
        return false;
    }
    var hqwd = $('#hqwd').val();
    if ($.trim(hqwd) == "") {
        layer.alert('纬度不可为空', { icon: 2 });
        $('#hqwd').focus();
        return false;
    }

    //林区名称
    var forestname = $('#forestname').val();
    if ($.trim(forestname) == "") {
        layer.alert('林区名称不可为空', { icon: 2 });
        $('#forestname').focus();
        return false;
    }
    //核查日期
    var checktime = $('#checktime').val();
    if ($.trim(checktime) == "") {
        layer.alert('核查日期不可为空', { icon: 2 });
        $('#checktime').focus();
        return false;
    }
    //起火时间<//灭火时间 checkEndTime
    var startTime = $('#firebegintime').val();//起火时间
    var endTime = $('#fireendtime').val(); //灭火时间
    if ($.trim(startTime) != "" && $.trim(endTime) != "") {
        var bo = checkEndTime(startTime, endTime);
        if (bo == false) {
            layer.alert('起火时间小于灭火时间', { icon: 2 });
            return false;
        }
    }
    var burnedarea = $('#burnedarea').val();//过火面积
    var overdoarea = $('#overdoarea').val();//过山林地面积
    var lostforestarea = $('#lostforestarea').val();//受害森林面积
    if ($.trim(burnedarea) != "") {//过火面积
        if (!reg.test(burnedarea) && !regnum.test(burnedarea)) {
            $('#burnedarea').focus();
            layer.alert('输入过火面积数值有误', { icon: 2 });
            return false;
        }
    }
    if ($.trim(overdoarea) != "") {//过山林地面积
        if (!reg.test(overdoarea) && !regnum.test(overdoarea)) {
            $('#overdoarea').focus();
            layer.alert('输入过山林地面积数值有误', { icon: 2 });
            return false;
        }
    }
    if ($.trim(lostforestarea) != "") {//受害森林面积
        if (!reg.test(lostforestarea) && !regnum.test(lostforestarea)) {
            $('#lostforestarea').focus();
            layer.alert('输入受害森林面积数值有误', { icon: 2 });
            return false;
        }
    }
    //情况简介
    var fireintro = $('#fireintro').val();
    if ($.trim(fireintro) == "") {
        layer.alert('情况简介不可为空', { icon: 2 });
        $('#fireintro').focus();
        return false;
    }
    //审核意见
    var shyj = $('input[name="shyj"]:checked').val();//0 审核未通过 1 审核通过
    if (shyj == "0") {
        var txt = $('#shyjyy').val();
        if ($.trim(txt) == "") {
            $('#shyjyy').focus();
            layer.alert('审核不通过，意见不可为空', { icon: 2 });
            return false;
        }
    }
    else {
        var val = $('#hottype').val();
        if (val == "1" || val == "6" || val == "10") {
            var txtname = $('#txtFileName').val();
            if ($.trim(txtname) == "") {
                layer.alert('请上传火情报告', { icon: 5 });
                return false;
            }
        }
    }

    return true;
}

///审核意见radio
function shyjChange() {
    //审核意见
    var shyj = $('input[name="shyj"]:checked').val();//0 审核未通过 1 审核通过
    if (shyj == "0") {
        $('#shyjdiv').show();
        $('#shyjyy').focus();
        $('#trreport').hide();
    }
    else {
        $('#shyjdiv').hide();
        $('#trreport').show();
    }
}

///设置火情报告上传显示与否
function setReport() {
    var value = $('#hottype').val();
    if (value == "1" || value == "6" || value == "10") {
        $('#trreport').show();
    } else {
        $('#trreport').hide();
    }
}

//增加火情报告上传附件
function upload(id) {
    var formData = new FormData($("#uploadForm")[0]);
    formData.append('jcfid', id);
    $.ajax({
        type: "post",
        url: "/JCFireInfo/DocUpload",
        data: formData,
        dataType: "json",
        async: false,
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data.Success) {
                $('#txtFileName').val(data.Msg);
                document.getElementById('lblInfo').innerText = "文件上传成功";
            }
            else {
                layer.msg(data.Msg, { icon: 5 });
            }
        },
        error: function (data) {
            layer.msg("上传失败", { icon: 5 });
        }
    });
}
//增加获取报告上传附件 ajaxsubmit 写法
function CheckUploadType() {
    var strs = new Array(); //定义一数组
    var doc = $("#uploadify").val();
    strs = doc.split('.');
    var suffix = strs[strs.length - 1];

    if (suffix != 'doc' && suffix != 'docx') {
        layer.alert("你选择的不是文档格式,请选择文档！", { icon: 5 });
        var obj = document.getElementById('uploadify');
        obj.outerHTML = obj.outerHTML; //这样清空，在IE8下也能执行成功
        //obj.select(); document.selection.clear(); 好像这种方法也可以清空 input file 的value值，不过我没测试
    }
}


///地图定位
function setPoint(jd, wd) {
    //iframe层-父子操作
    layer.open({
        type: 2,
        title: '地图位置设置',
        btn: ['保存', '关闭'],
        area: ['72%', '75%'],
        fix: false, //不固定
        maxmin: true,
        content: '/MapCommon/GetMapPontIndex?jd=' + jd + "&wd=" + wd,
        yes: function (index, layero) { //或者使用btn1
            //按钮【按钮一】的回调
            var ptxt = layer.getChildFrame("p", index);
            var arr = ptxt.html().split(',');
            var jd = parseFloat(arr[0]).toFixed(6);
            var wd = parseFloat(arr[1]).toFixed(6);
            $('#hqjd').val(jd);
            $('#hqwd').val(wd);
            $.ajax({
                type: "Post",
                url: "/MapCommon/GetAddressAjax",
                data: { jd: jd, wd: wd },
                dataType: "json",
                success: function (obj) {
                    if (obj != null && obj.Success) {
                        $('#hqaddress').val(obj.Msg);
                    }
                    else {
                        layer.msg('未获取到地址');
                    }
                }
            });
            layer.close(index);
        }, cancel: function (index) { //或者使用btn2
            layer.close(index);
        }
    });
    //layer.full(index);
}

///火情报告
function FireReport(ID) {
    $.ajax({
        url: '/DataCenter/getFIREReport',
        data: {
            JCFID: ID,
        },
        type: 'post',
        success: function (data, ioArgs) {
            var ar = eval('(' + data + ')');
            if (ar.Success) {
                //$('#tablereport').html(ar.tableInfo);
                layer.open({
                    type: 1,
                    title: '火情报告',
                    area: ['950px', '400px'],
                    shade: 0,
                    content: ar.tableInfo
                });
            }
            else {
                alert(ar.Msg);
            }
        },
        error: function (err, ioArgs) {
            layer.msg('获取异常，请刷新重试');
        }
    });
}

//火情报告编辑
function FireReportEdit() {
    layer.open({
        type: 2,
        title: '在线编辑',
        area: ['950px', '560px'],
        maxmin: true,
        //shade: 0,
        content: '/OfficeManager/OfficeEditAspxIndex',
        btn: ['报告上传', '取消'],
        yes: function (index, layero) { //或者使用btn1
            alert('22');
            layer.close(index);

        }
    });

}
//function FireFK(id, orgno) {
//    $.ajax({
//        type: "Post",
//        url: "/MainYJJC/QSMethod",
//        data: { jcid: id, orgno: orgno },
//        dataType: "json",
//        success: function (obj) {
//            if (obj != null && obj.Success) {
//                layer.msg('签收成功！', { icon: 6, time: 2000 });
//            }
//            else {
//                layer.alert(obj.Msg, { icon: 5 });
//            }
//        }
//    });
//}
