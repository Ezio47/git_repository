/// <reference path="_references.js" />
//护林员详细
function detailInfo(uid, name) {
    $.ajax({
        type: "Post",
        url: "/RealSupervision/GetIPSUserAjax",
        data: { userid: uid },
        dataType: "json",
        success: function (data) {
            if (data != null && data.Success) {
                //页面层
                layer.open({
                    type: 1,
                    closeBtn: 2,
                    shift: 4,
                    title: "【" + name + "】详细信息",
                    //skin: 'layui-layer-lan', //加上边框
                    area: ['550px', '300px'], //宽高
                    content: data.Msg
                });
            }
            else {
                layer.alert('返回结果发生了错误！', { icon: 5 });
            }
        }
    });
}