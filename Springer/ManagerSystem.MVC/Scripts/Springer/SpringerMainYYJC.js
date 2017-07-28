/// <reference path="../_references.js" />
//fttype 火情类型
//红外相机 = 1,
//   卫星热点 = 2,
//   人工报警 = 3,
//   电子报警 = 4,
//   护林员火情上报 = 5
//火情监测
function FireAjax(type) {
    $.ajax({
        type: "Post",
        url: "/MainYJJC/GetModelListBy",
        data: { type: type },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#divwxmsg').empty();
                $('#divwxmsg').html(obj.Msg);
                //if (type == "2") {//卫星
                //    $('#divwxmsg').empty();
                //    $('#divwxmsg').html(obj.Msg);
                //}
                //if (type == "3") {//人工（电话）
                //    $('#divphonemsg').empty();
                //    $('#divphonemsg').html(obj.Msg);
                //}
                //if (type == "4") {//电子（视频）
                //    $('#divelecmsg').empty();
                //    $('#divelecmsg').html(obj.Msg);

                //}
                //if (type == "5") {//护林员上报
                //    $('#divhlmsg').empty();
                //    $('#divhlmsg').html(obj.Msg);
                //}
                //if (type == "6") {//飞机巡护
                //    $('#divplanemsg').empty();
                //    $('#divplanemsg').html(obj.Msg);
                //}
            }
            else {
                layer.alert('获取火情监测信息出错！', { icon: 5 });
            }
        }
    });
}
//市（州）局签收 id 为监测火情id ftype 火情来源
function CityQS(id, ftype, freshtype) {
    $.ajax({
        type: "Post",
        url: '/JCFireInfo/CityQSMethod',
        data: { jcfid: id },
        dataType: "json",
        async: false, //默认为true 异步 
        success: function (str) {
            if (str.Success) {
                layer.alert("签收成功", function (index) {
                    if (freshtype == "") {
                        FireAjax(ftype);//ajax更新状态
                    } else {
                        window.location.reload();//页面重载
                    }
                    layer.close(index);
                });
            }
            else {
                layer.msg(str.Msg);
            }
        }
    });
}
//市（州）局签收 单位选择（当火情监测表中机构码为市级别，则需要下发单位）
function CityQSOrgSelect(id, ftype, freshtype) {
    // QSSXJOrgSelect(id, ftype, freshtype);
    $.ajax({
        type: "Post",
        url: '/JCFireInfo/getSJQSSelect',
        data: { jcfid: id },
        dataType: "json",
        async: false, //默认为true 异步 
        success: function (str) {
            layer.open({
                type: 1,
                title: '核查单位选择',
                area: ['380px', '200px'],
                content: str.Msg,//注意，如果str是object，那么需要字符拼接。 $('#divselect'),
                shadeClose: false,
                btn: ["派发", "取消"],
                yes: function (index, layero) {
                    //do something
                    var val = $('input:radio[name="radhc"]:checked').val();
                    if (val == "0") {
                        orgno = $('#QSselect').val();
                        if ($.trim(orgno) == "") {
                            layer.alert("核查单位不可为空", { icon: 5 });
                            return false;
                        }
                    }
                    else {
                        var p = $('#txtperson').val();
                        if ($.trim(p) == "") {
                            layer.alert("本单位人员不可为空", { icon: 5 });
                            return false;
                        }
                    }
                    cityQS(id, orgno, val, ftype, freshtype);
                    layer.close(index); //如果设定了yes回调，需进行手工关闭
                },
                cancel: function (index) {
                    layer.closeAll();
                }
            });
        }
    });
}

var orgno = "";
//（县局）签收派发核查单位选择
function QSSXJOrgSelect(id, ftype, freshtype) {
    $.ajax({
        type: "Post",
        url: '/JCFireInfo/getSXJQSSelect',
        data: { jcfid: id },
        dataType: "json",
        async: false, //默认为true 异步 
        success: function (str) {
            layer.open({
                type: 1,
                title: '核查单位选择',
                area: ['380px', '200px'],
                content: str.Msg,//注意，如果str是object，那么需要字符拼接。 $('#divselect'),
                shadeClose: false,
                btn: ["派发", "取消"],
                yes: function (index, layero) {
                    //do something
                    var val = $('input:radio[name="radhc"]:checked').val();
                    if (val == "0") {
                        orgno = $('#QSselect').val();
                        if ($.trim(orgno) == "") {
                            layer.alert("核查单位不可为空", { icon: 5 });
                            return false;
                        }
                    }
                    else {
                        var p = $('#txtperson').val();
                        if ($.trim(p) == "") {
                            layer.alert("本单位人员不可为空", { icon: 5 });
                            return false;
                        }
                    }
                    contyQS(id, orgno, val, ftype, freshtype);
                    layer.close(index); //如果设定了yes回调，需进行手工关闭
                },
                cancel: function (index) {
                    layer.closeAll();
                }
            });
        }
    });
}
//县局 签收（电话报警----当前火情监测表中的机构码为乡镇时，县级别签收无需单位选择）
function QSSXJOrg(id, ftype, freshtype) {
    $.ajax({
        type: "Post",
        url: '/JCFireInfo/ContyOnlyQSMethod',
        data: { jcfid: id },
        dataType: "json",
        async: false, //默认为true 异步 
        success: function (str) {
            if (str.Success) {
                layer.alert("签收派发成功", function (index) {
                    if (freshtype == "") {
                        FireAjax(ftype);//ajax更新状态
                    }
                    else {
                        window.location.reload();//页面重载
                    }
                    layer.close(index);
                });
            }
            else {
                layer.msg(str.Msg);
            }
        }
    })
}


//市级签收 派发
function cityQS(id, orgno, type, ftype, freshtype) {
    $.ajax({
        type: "Post",
        url: '/JCFireInfo/CityQSPFMethod',
        data: { jcfid: id, type: type, orgno: orgno },
        dataType: "json",
        async: false, //默认为true 异步 
        success: function (str) {
            if (str.Success) {
                layer.alert("签收派发成功", function (index) {
                    if (freshtype == "") {
                        FireAjax(ftype);//ajax更新状态
                    }
                    else {
                        window.location.reload();//页面重载
                    }
                    layer.close(index);
                });
            }
            else {
                layer.msg(str.Msg);
            }
        }
    });
}
//县级签收 id jcfid orgno 为下级单位 type 0 下级单位 1 为本单位 ftype 为热点来源 freshtype 为刷新类型
function contyQS(id, orgno, type, ftype, freshtype) {
    $.ajax({
        type: "Post",
        url: '/JCFireInfo/ContyQSMethod',
        data: { jcfid: id, type: type, orgno: orgno },
        dataType: "json",
        async: false, //默认为true 异步 
        success: function (str) {
            if (str.Success) {
                layer.alert("签收派发成功", function (index) {
                    if (freshtype == "") {
                        FireAjax(ftype);//ajax更新状态
                    }
                    else {
                        window.location.reload();//页面重载
                    }
                    layer.close(index);
                });
            }
            else {
                layer.msg(str.Msg);
            }
        }
    });
}



//(乡镇签收)派发核查单位选择
function QSXZJOrgSelect(id, ftype, freshtype) {
    $.ajax({
        type: "Post",
        url: '/JCFireInfo/getXZJQSSelect',
        data: { jcfid: id },
        dataType: "json",
        async: false, //默认为true 异步 
        success: function (str) {
            layer.open({
                type: 1,
                title: '人员核查选择',
                area: ['380px', '200px'],
                content: str.Msg,//注意，如果str是object，那么需要字符拼接。 $('#divselect'),
                shadeClose: false,
                btn: ["派发", "取消"],
                yes: function (index, layero) {
                    var person = $('#txtperson').val();
                    if ($.trim(person) == "") {
                        layer.alert("护林员不可为空", { icon: 5 });
                        return false;
                    }
                    XzQS(id, ftype, freshtype);
                    layer.close(index); //如果设定了yes回调，需进行手工关闭
                },
                cancel: function (index) {
                    layer.closeAll();
                }
            });
        }
    })
}

//乡镇签收 id 为监测火情id ftype 火情来源
function XzQS(id, ftype, freshtype) {
    var fsperson = $('#hidtxt').val();
    var xgperson = $('#hidxgtxt').val();
    // alert("护林员" + fsperson);
    // alert("相关人员" + xgperson);
    $.ajax({
        type: "Post",
        url: '/JCFireInfo/XzQSMethod',
        data: { jcfid: id, fsperson: fsperson, xgperson: xgperson },
        dataType: "json",
        async: false, //默认为true 异步 
        success: function (str) {
            if (str.Success) {
                layer.alert("签收下派成功", function (index) {
                    if (freshtype == "") {
                        FireAjax(ftype);//ajax更新状态
                    }
                    else {
                        window.location.reload();//页面重载
                    }
                    layer.close(index);
                });
            }
            else {
                layer.msg(str.Msg);
            }
        }
    });
}
//系统人员选择
function SelctOrgPeron(orgno) {
    // $('#persontree').show();
    layer.open({
        type: 2,
        title: '单位人员选择',
        //skin: 'layui-layer-molv',
        area: ['660px', '300px'],
        zIndex: layer.zIndex,
        content: '/JCFireInfo/GetSYSUserIndex',//注意，如果str是object，那么需要字符拼接。$('#persontree'),
        shadeClose: false,
        offset: ['120px', '280px'],
        btn: ["确定", "取消"],
        yes: function (index, layero) {
            var body = layer.getChildFrame('body', index);
            var ss = body.find('input[name="txtid"]').val();
            var sn = body.find('input[name="txtname"]').val();
            $('#hidtxt').val(ss);
            $('#txtperson').val(sn);
            layer.close(index);
        },
        cancel: function (index) {
            layer.close(index);
        },
        success: function (layero, index) {
            var s = $('#hidtxt').val();
            var body = layer.getChildFrame('body', index);
            body.find('input[name="txtid"]').val(s);
        }
    });

}

//护林员人员选择
function SelctHLYPerson() {
    layer.open({
        type: 2,
        title: '护林人员选择',
        //skin: 'layui-layer-molv',
        area: ['660px', '300px'],
        zIndex: layer.zIndex,
        content: '/JCFireInfo/GetHLYUserIndex',//注意，如果str是object，那么需要字符拼接。$('#persontree'),
        shadeClose: false,
        offset: ['120px', '280px'],
        btn: ["确定", "取消"],
        yes: function (index, layero) {
            var body = layer.getChildFrame('body', index);
            var ss = body.find('input[name="txtid"]').val();
            var sn = body.find('input[name="txtname"]').val();
            $('#hidtxt').val(ss);
            $('#txtperson').val(sn);
            layer.close(index);
        },
        cancel: function (index) {
            layer.close(index);
        },
        success: function (layero, index) {
            var s = $('#hidtxt').val();
            var body = layer.getChildFrame('body', index);
            body.find('input[name="txtid"]').val(s);
        }
    });
}
//通讯录人员选择
function SelctTXLPeron(orgno) {
    layer.open({
        type: 2,
        title: '相关人员选择',
        //skin: 'layui-layer-molv',
        area: ['660px', '400px'],
        zIndex: layer.zIndex,
        content: '/JCFireInfo/GetTXLUserIndex',//注意，如果str是object，那么需要字符拼接。$('#persontree'),
        shadeClose: false,
        offset: ['100px', '280px'],
        btn: ["确定", "取消"],
        yes: function (index, layero) {
            var body = layer.getChildFrame('body', index);
            var ss = body.find('input[name="txtid"]').val();
            var sn = body.find('input[name="txtname"]').val();
            $('#hidxgtxt').val(ss);
            $('#txtxgperson').val(sn);
            layer.close(index);
        },
        cancel: function (index) {
            layer.close(index);
        },
        success: function (layero, index) {
            var s = $('#hidxgtxt').val();
            var body = layer.getChildFrame('body', index);
            body.find('input[name="txtid"]').val(s);
        }
    });

}

//(县局)签收
function QSXJ(id, ftype) {
    QS(id, '');//县局签收
    FireAjax(ftype);//重载
}

//反馈火情Html
function FkFireInfo(url, jcfid, ftype, type, refresh) {
    getAjaxFKFireInfo(url, jcfid, ftype, type, refresh);
}







//获取县级别机构单位
function GetOrgConty() {
    $.ajax({
        type: "Post",
        url: "/MainYJJC/GetQSOrg",
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                $('#QSselect').html('');
                $('#QSselect').html(obj.Msg);
            }
        }
    });
}

//签收
function QS(id, orgno) {
    $.ajax({
        type: "Post",
        url: "/MainYJJC/QSMethod",
        data: { jcid: id, orgno: orgno },
        dataType: "json",
        success: function (obj) {
            if (obj != null && obj.Success) {
                layer.msg('签收成功！', { icon: 6, time: 2000 });
            }
            else {
                layer.alert(obj.Msg, { icon: 5 });
            }
        }
    });
}

